using ShapeCalculations.Entity.Shape;

namespace ShapeCalculations.Application.Impl.Extensions
{
    internal static class CubeExtensions
    {
        /// <summary>
        ///     Converts to the equivalent domain model cube object
        /// </summary>
        /// <returns>
        ///     The equivalent domain model Cube object.
        /// </returns>
        public static Cube ToDomain(this Models.Cube cubeA)
        {
            return new Cube(
                new Point3D(cubeA.CenterX, cubeA.CenterY, cubeA.CenterZ),
                cubeA.SizeX,
                cubeA.SizeY,
                cubeA.SizeZ);
        }
    }
}
