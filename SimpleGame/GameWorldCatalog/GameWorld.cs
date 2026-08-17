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
        private const int BarricadeBlocksX = 20;
        private const int BarricadeBlocksY = 10;
        private const float BarricadeStartX = 45f;
        private const float BarricadeStartY = 420f;

        private const int BarricadesRows = 1;

        private const int BarricadesCols = 7;

        private const int EnemyCols = 9;

        private const float EnemySpeed = 30f;

        private const float BarricadeSpacing = 100f;

        private const float EnemyDropDistance = 50f;

        private float _enemyDirectionX = 1f;

        private const float WorldWidthX = 780f;

        private const float WorldHeightY = 600f;

        private const float EnemySize = 45f;

        private const float BarricadeSize = 5f;
        private const float EnemySpacingX = EnemySize / 4f;
        private const float EnemySpacingY = EnemySize / 4f;
        private const float EnemyStartX = 50f;
        private const float EnemyStartY = 30f;

        private const float startX = 350f;
        private const float startY = 510f;

        public List<Enemy> Enemies = new List<Enemy>();

        public List<Projectile> PlayerProjectiles { get; } = new();
        public List<Projectile> EnemyProjectiles { get; } = new();


        public Player Player { get; } = new Player(startX, startY);
        public List<Barricade> Barricades { get; } = new List<Barricade>();

        public int PlayerLives { get; private set; } = 3;

        public int PlayerScore { get; set; }

        public void UpdateScore(int Point)
        { PlayerScore += Point; }

        public bool IsGameOver { get; private set; } = false;

        /// <summary>
        /// Method to update world, move enemies, end the game
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Update(float deltaTime, bool leftPressed, bool rightPressed)
        {
            if (IsGameOver) return;
            if (PlayerLives == 0) { IsGameOver = true; return; }

            const float playerSpeed = 250f;
            if (leftPressed) Player.MoveX(-playerSpeed * deltaTime);
            if (rightPressed) Player.MoveX(playerSpeed * deltaTime);

            if (PlayerLives == 0 )
            { 
                IsGameOver = true;
                return;
            }

            bool needReverse = false;

            foreach(var enemy in Enemies)
            {
                if(_enemyDirectionX > 0 && enemy.X + enemy.Width >= WorldWidthX)
                {
                    needReverse = true;
                }
                if(_enemyDirectionX < 0 && enemy.X <=0)
                {
                    needReverse = true;
                }
            }

            if(needReverse)
            {
                _enemyDirectionX *= -1f;
                foreach (var enemy in Enemies)
                    enemy.MoveY(EnemyDropDistance);
            }

            foreach (var enemy in Enemies)
            {
                enemy.MoveX(EnemySpeed * deltaTime * _enemyDirectionX);
                
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
