using System;

namespace ShapeCalculations.Entity.Shape
{
    public class Sphere : IShape
    {
        #region Interface Implementation

        public ShapeType GetShapeType()
            => ShapeType.Sphere;

        #endregion
    }
}
