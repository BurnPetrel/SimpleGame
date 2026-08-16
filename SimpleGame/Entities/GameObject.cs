using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace SimpleGame.Entities
{

    public enum ShapeType
    {
        Square,
        Circle,
        Rectangle,
        SmallRectangle,
        Triangle, 
        Rhombus,
        SmallCircle
    }

    public abstract class GameObject
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public ShapeType Shape { get; set; }

        public Shape Visual { get; protected set; }

        public abstract void CreateVisual();

    }
}
