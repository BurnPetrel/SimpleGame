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
        public bool IsPlayerBullet { get; set; }

        public Projectile()
        {
            Height = 8;
            Width = 5;
            Shape = ShapeType.SmallRectangle;
        }

        public override void CreateVisual()
        {
            Visual = new Rectangle
            {
                Fill = new SolidColorBrush(IsPlayerBullet ? Colors.Red : Colors.Cyan),
                Width = Width,
                Height = Height
            };
        }

        public float MoveY(float value)
        {
            Y += value;
            return Y;
        }
    }

}
