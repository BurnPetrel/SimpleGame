using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SimpleGame.Entities
{

    /// <summary>
    /// Abstract class enemies, consist base methods and field to all enemies
    /// </summary>
    public abstract class Enemy : GameObject
    {

        public int Score { get; set; }

        public float MoveX(float value)
        {
            return (X = X + value);
        }
        public float MoveY(float value)
        {
            return (Y = Y + value);
        }

    }


    /// <summary>
    /// Square enemy, closest to player, then less valuable
    /// </summary>
    public class SquareEnemy : Enemy
    {

        public SquareEnemy()
        {
            Height = 45;
            Width = 45;
            Shape = ShapeType.Square;
            Score = 10;
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

    /// <summary>
    /// Circle enemy class
    /// </summary>
    public class CircleEnemy : Enemy
    {

        public CircleEnemy()
        {
            Score = 20;
            Height = 45;
            Width = 45;
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

    /// <summary>
    /// rhombus enemy class
    /// </summary>
    public class RhombusEnemy : Enemy
    {

        public RhombusEnemy()
        {
            Score = 30;
            Height = 45;
            Width = 45;
            Shape = ShapeType.Rhombus;
        }

        public override void CreateVisual()
        {

            Visual = new Polygon
            {
                Points = new PointCollection
            {
                new Point(0, 0.5),
                new Point(0.5, 1),
                new Point(1, 0.5),
                new Point(0.5, 0)
            },
                Stretch = Stretch.Fill,
                Fill = new SolidColorBrush(Colors.Pink),
                Width = Width,
                Height = Height
            };
        }

    }


}
