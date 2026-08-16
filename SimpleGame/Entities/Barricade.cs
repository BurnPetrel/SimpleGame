using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SimpleGame.Entities
{
    /// <summary>
    /// Class for barricade, no projectiles
    /// </summary>
    public class Barricade : GameObject
    {
        public Barricade()
        {
            Height = 5;
            Width = 5;
            Shape = ShapeType.Rectangle;
        }

        public override void CreateVisual()
        {

            Visual = new Rectangle
            {
                Width = Width,
                Height = Height,
                Fill = new SolidColorBrush(Colors.Gray)
            };
        }


    }
}
