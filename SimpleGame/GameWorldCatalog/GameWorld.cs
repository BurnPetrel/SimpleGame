using System;
using System.Collections.Generic;
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
        private const int EnemyCols = 9;
        private const float EnemySize = 20f;
        private const float EnemySpacingX = EnemySize / 4f;
        private const float EnemySpacingY = EnemySize / 4f;
        private const float EnemyStartX = 50f;   // Отступ от левого края
        private const float EnemyStartY = 30f;   // Отступ от верха

        const float startX = 0;
        const float startY = 0;

        public List<Enemy> Enemies = new List<Enemy>();

        public List<Projectile> Projectiles = new List<Projectile>();


        public Player Player = new Player(startX, startY);

        public int PlayerLives { get; private set; } = 3;

        public int PlayerScore { get; set; }

        public void UpdateScore(int Point)
        { PlayerScore += Point; }

        public bool IsGameOver { get; private set; } = false;

        /// <summary>
        /// Method to update world, move enemies, end the game
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Update(float deltaTime)
        {

            if (IsGameOver) { return; }

            if(PlayerLives == 0 )
            { 
                IsGameOver = true;
                return;
            }

            foreach (var enemy in Enemies)
            {
                enemy.MoveX(50f * deltaTime);
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

    }
}
