using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SimpleGame.Entities
{

    /// <summary>
    /// Projectile class for Player and Enemies shoot
    /// </summary>
    public class Projectile : GameObject
    {

        public Projectile()
        {
            Height = 8;
            Width = 8;
            Shape = ShapeType.SmallRectangle;
        }

        public bool IsPlayerBullet { get; set; }

        public override void CreateVisual()
        {

            Visual = new Rectangle
            {
                Fill = new SolidColorBrush(Colors.Red),
                Width = Width,
                Height = Height
            };
        }

        public float MoveY(float value)
        {
            return (Y = Y + value);
        }

    }
        
}
