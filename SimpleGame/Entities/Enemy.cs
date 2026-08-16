using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SimpleGame.Entities
{
    public abstract class Enemy : GameObject
    {

        public float MoveX(float value)
        {
            return (X = X + value);
        }
        public float MoveY(float value)
        {
            return (Y = Y + value);
        }

    }

    public class SquareEnemy : Enemy
    {

        public SquareEnemy()
        {
            Height = 20;
            Width = 20;
            Shape = ShapeType.Square;
        }

        public override void CreateVisual()
        {

            Visual = new Rectangle
            {
                Width = Width,
                Height = Height,
                Fill = new SolidColorBrush(Colors.Green)
            };
        }

    }

    public class CircleEnemy : Enemy
    {

        public CircleEnemy()
        {
            Height = 20;
            Width = 20;
            Shape = ShapeType.Circle;
        }

        public override void CreateVisual()
        {

            Visual = new Ellipse
            {
                Fill = new SolidColorBrush(Colors.Yellow),
                Width = Width,
                Height = Height
            };
        }

    }

    public class RhombusEnemy : Enemy
    {

        public RhombusEnemy()
        {
            Height = 20;
            Width = 20;
            Shape = ShapeType.Rhombus;
        }

        public override void CreateVisual()
        {

            Visual = new Polygon
            {
                Points = new PointCollection
            {
                new Point(0, 0.5),
                new Point(0.5, 0),
                new Point(0.5, 1),
                new Point(1, 0.5)
            },
                Stretch = Stretch.Fill,
                Fill = new SolidColorBrush(Colors.Yellow),
                Width = Width,
                Height = Height
            };
        }

    }


}
