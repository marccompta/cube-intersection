namespace ShapeCalculations.Entity.Shape
{
    public class Cube : IShape
    {
        #region Properties

        public Point3D Center { get; private set; }

        public double SizeX { get; private set; }

        public double SizeY { get; private set; }

        public double SizeZ { get; private set; }

        #endregion

        #region Constructor

        public Cube(Point3D center, double sizeX, double sizeY, double sizeZ)
        {
            Center = center;
            SizeX = sizeX;
            SizeY = sizeY;
            SizeZ = sizeZ;
        }

        #endregion

        #region Interface Implementation

        public ShapeType GetShapeType()
            => ShapeType.Cube;

        #endregion
    }
}
