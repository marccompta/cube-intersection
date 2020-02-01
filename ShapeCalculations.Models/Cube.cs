namespace ShapeCalculations.Models
{
    public class Cube
    {
        #region Properties

        public double CenterX { get; private set; }

        public double CenterY { get; private set; }

        public double CenterZ { get; private set; }

        public double SizeX { get; private set; }

        public double SizeY { get; private set; }

        public double SizeZ { get; private set; }

        #endregion

        #region Constructor

        public Cube(double centerX, double centerY, double centerZ,
            double sizeX, double sizeY, double sizeZ)
        {
            CenterX = centerX;
            CenterY = centerY;
            CenterZ = centerZ;
            SizeX = sizeX;
            SizeY = sizeY;
            SizeZ = sizeZ;
        }

        #endregion
    }
}
