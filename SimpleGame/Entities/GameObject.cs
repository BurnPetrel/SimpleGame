using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace SimpleGame.Entities
{

    /// <summary>
    /// Enumerator for figures
    /// </summary>

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

    /// <summary>
    /// Base abstract class for entities
    /// </summary>
    /// <param name="X">X coordinate</param>
    /// <param name="Y">Y coordinate</param>
    /// <param name="Width">Width to entities</param>
    /// <param name="Height">Height to entities</param>
    /// <param name="Shape">Shape, takes from enumerator</param>
    public abstract class GameObject
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public ShapeType Shape { get; set; }

        public Shape Visual { get; protected set; }

        public abstract void CreateVisual();

        /// <summary>
        /// method to collisions between two rectangles. Simple way to collisions.
        /// </summary>
        /// <param name="other">Position of some object</param>
        /// <returns></returns>
        public bool Intersects(GameObject other)
        {
            return X < other.X + other.Width &&
                   X + Width > other.X &&
                   Y < other.Y + other.Height &&
                   Y + Height > other.Y;
        }

        public void UpdateVisualPosition()
        {
            Canvas.SetLeft(Visual, X);
            Canvas.SetRight(Visual, Y);

        }

    }
}
