using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SimpleGame.Entities
{

    /// <summary>
    /// Base Player class
    /// </summary>
    public class Player : GameObject
    {

        public Player(float startX, float startY)
        {
            Height = 20;
            Width = 20;
            Shape = ShapeType.Triangle;
            X = startX;
            Y = startY;
        }

        /// <summary>
        /// Create a visual model of player
        /// </summary>
        public override void CreateVisual()
        {
            
                Visual = new Polygon
                {
                    Points = new PointCollection
            {
                new Point(0.5, 0),
                new Point(1, 1),
                new Point(0, 1)
            },
                    Stretch = Stretch.Fill,
                    Fill = new SolidColorBrush(Colors.Purple),
                    Width = Width,
                    Height = Height
                };
            }

        /// <summary>
        /// Method to move player
        /// </summary>
        /// <param name="value">The magnitude of the displacement</param>
        /// <returns></returns>
        public float MoveX(float value)
        {
            return (X = X + value);
        }

    }
}
