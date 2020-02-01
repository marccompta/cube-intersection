using System;

namespace ShapeCalculations.Entity.Shape
{
    public class Cylinder : IShape
    {
        #region Interface Implementation

        public ShapeType GetShapeType()
            => ShapeType.Cylinder;

        #endregion
    }
}
