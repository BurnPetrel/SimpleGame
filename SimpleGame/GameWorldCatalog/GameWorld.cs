using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using SimpleGame.Entities;

namespace SimpleGame.GameWorldCatalog
{
    /// <summary>
    /// Class to create game world, consist constant, fields, methods of game status
    /// </summary>
    public class GameWorld
    {

        private const int EnemyRows = 5;

        private const int BarricadeCount = 4;

        private const float ProjectileSpeed = 400f;
        private bool _canPlayerShoot = true;
        public List<GameObject> NewVisuals { get; } = new();
        public List<GameObject> RemovedVisuals { get; } = new();
        private const int BarricadeBlocksX = 20;
        private const int BarricadeBlocksY = 10;
        private const float BarricadeStartX = 45f;
        private const float BarricadeStartY = 420f;

        private const int BarricadesRows = 1;

        private const int BarricadesCols = 7;

        private const int EnemyCols = 9;

        private const float EnemySpeed = 15f;

        private const float BarricadeSpacing = 100f;

        private const float EnemyDropDistance = 50f;

        private float _enemyDirectionX = 1f;

        private const float WorldWidthX = 780f;

        private const float WorldHeightY = 600f;

        private const float EnemySize = 45f;

        private const float BarricadeSize = 5f;
        private const float EnemySpacingX = EnemySize / 4f;
        private const float EnemySpacingY = EnemySize / 4f;
        private const float EnemyStartX = 100f;
        private const float EnemyStartY = 20f;

        private const float startX = 350f;
        private const float startY = 510f;

        public List<Enemy> Enemies = new List<Enemy>();

        public List<Projectile>Projectiles { get; } = new();


        public Player Player { get; } = new Player(startX, startY);
        public List<Barricade> Barricades { get; } = new List<Barricade>();

        public int PlayerLives { get; private set; } = 3;

        public int PlayerScore { get; set; }

        public void UpdateScore(int Point)
        { PlayerScore += Point; }

        public bool IsGameOver { get; private set; } = false;

        private void SpawnPlayerProjectile()
        {
            var proj = new Projectile
            {
                X = Player.X + Player.Width / 2 - 4,
                Y = Player.Y - 8,
                IsPlayerBullet = true
            };

            Projectiles.Add(proj);
            NewVisuals.Add(proj);
        }


        /// <summary>
        /// Method to update world, move enemies, end the game
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Update(float deltaTime, bool leftPressed, bool rightPressed, bool spacePressed)
        {
            if (IsGameOver) return;
            if (PlayerLives == 0) { IsGameOver = true; return; }

            if (Enemies.Count == 0)
            {
                SpawnEnemies();
                _enemyDirectionX = 1f;
            }

            const float playerSpeed = 200f;
            if (leftPressed) Player.MoveX(-playerSpeed * deltaTime);
            if (rightPressed) Player.MoveX(playerSpeed * deltaTime);

            if (spacePressed && _canPlayerShoot)
            {
                SpawnPlayerProjectile();
                _canPlayerShoot = false;
            }

            if (!Projectiles.Any(p => p.IsPlayerBullet))
                _canPlayerShoot = true;

            foreach (var proj in Projectiles.ToList())
            {
                if (proj.IsPlayerBullet)
                    proj.MoveY(-ProjectileSpeed * deltaTime);
                else
                    proj.MoveY(ProjectileSpeed * deltaTime);

                if (proj.Y + proj.Height < 0 || proj.Y > WorldHeightY)
                {
                    RemovedVisuals.Add(proj);
                    Projectiles.Remove(proj);
                }
            }

            if (PlayerLives == 0)
            {
                IsGameOver = true;
                return;
            }

            bool needReverse = false;

            foreach (var enemy in Enemies)
            {
                if (_enemyDirectionX > 0 && enemy.X + enemy.Width >= WorldWidthX)
                {
                    needReverse = true;
                }
                if (_enemyDirectionX < 0 && enemy.X <= 0)
                {
                    needReverse = true;
                }
            }

            if (needReverse)
            {
                _enemyDirectionX *= -1f;
                foreach (var enemy in Enemies)
                    enemy.MoveY(EnemyDropDistance);
            }

            foreach (var enemy in Enemies)
            {
                enemy.MoveX(EnemySpeed * deltaTime * _enemyDirectionX);

            }
            CheckCollisions();

        }

        private void CheckCollisions()
        {
            var toRemove = new List<GameObject>();

            foreach (var proj in Projectiles.Where(p => p.IsPlayerBullet).ToList())
            {
                foreach (var enemy in Enemies.ToList())
                {
                    if (proj.Intersects(enemy))
                    {
                        toRemove.Add(proj);
                        toRemove.Add(enemy);
                        UpdateScore(enemy.Score);
                        break;
                    }
                }
            }

            foreach (var proj in Projectiles.Where(p => p.IsPlayerBullet).ToList())
            {
                if (toRemove.Contains(proj)) continue;

                foreach (var barricade in Barricades.ToList())
                {
                    if (proj.Intersects(barricade))
                    {
                        toRemove.Add(proj);
                        toRemove.Add(barricade);
                        break;
                    }
                }
            }

            foreach (var obj in toRemove)
            {
                RemovedVisuals.Add(obj);

                if (obj is Projectile p) Projectiles.Remove(p);
                else if (obj is Enemy e) Enemies.Remove(e);
                else if (obj is Barricade b) Barricades.Remove(b);
            }
        }


        /// <summary>
        /// Method to spawn enemies in 5 rows and 9 cols, 45 enemies
        /// </summary>
        public void SpawnEnemies()
        {
            for (int row = 0; row < EnemyRows; row++)
            {
                for (int col = 0; col < EnemyCols; col++)
                {
                    float x = EnemyStartX + col * (EnemySize + EnemySpacingX);
                    float y = EnemyStartY + row * (EnemySize + EnemySpacingY);

                    Enemy enemy = row switch
                    {
                        0 => new RhombusEnemy(),
                        1 => new CircleEnemy(),
                        2 => new CircleEnemy(),
                        3 => new SquareEnemy(),
                        4 => new SquareEnemy(),
                        _ => new SquareEnemy()
                    };

                    enemy.X = x;
                    enemy.Y = y;
                    Enemies.Add(enemy);
                    NewVisuals.Add(enemy);
                }
            }
        }

        /// <summary>
        /// Method to spawn Barricades, 4 big barricades consist 200 small barricades
        /// </summary>
        public void SpawnBarricades()
        {
            for (int b = 0; b < BarricadeCount; b++)
            {
                float barricadeOriginX = BarricadeStartX + b * (BarricadeBlocksX * BarricadeSize + BarricadeSpacing);

                for (int row = 0; row < BarricadeBlocksY; row++)
                {
                    for (int col = 0; col < BarricadeBlocksX; col++)
                    {
                        float x = barricadeOriginX + col * BarricadeSize;
                        float y = BarricadeStartY + row * BarricadeSize;

                        var block = new Barricade
                        {
                            X = x,
                            Y = y
                        };

                        Barricades.Add(block);
                    }
                }
            }
        }


    }
}
