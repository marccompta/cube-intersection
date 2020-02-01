namespace ShapeCalculations.Entity.Shape
{
    public class Point3D
    {
        #region Properties

        public double X { get; private set; }

        public double Y { get; private set; }

        public double Z { get; private set; }

        #endregion

        #region Constructor

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        #endregion
    }
}
