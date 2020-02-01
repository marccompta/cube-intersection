using ShapeCalculations.Entity.Shape;
using System;

namespace ShapeCalculations.Entity.Intersector
{
    /// <summary>
    ///     Contains the algorithm to intersect two cubes.
    /// </summary>
    /// <seealso cref="IIntersector{Cube, Cube}" />
    public class CubesIntersector : IIntersector<Cube, Cube>
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="CubesIntersector"/> class.
        /// </summary>
        public CubesIntersector()
        {

        }

        #endregion

        #region Interface Implementation

        /// <summary>
        ///     Gets the intersection of two non-rotated cubes, or null if the two cubes do not intersect.
        /// </summary>
        /// <param name="cubeA">
        ///     The cube a.
        /// </param>
        /// <param name="cubeB">
        ///     The cube b.
        /// </param>
        /// <returns>
        ///     The intersection, or null otherwise.
        /// </returns>
        public IShape GetIntersection(Cube cubeA, Cube cubeB)
        {
            Cube result = null;

            var segmentsOfCubeA = GetSegments(cubeA);
            var segmentsOfCubeB = GetSegments(cubeB);

            var intersectionX = GetIntersectionSegment(segmentsOfCubeA[0], segmentsOfCubeB[0]);
            var intersectionY = GetIntersectionSegment(segmentsOfCubeA[1], segmentsOfCubeB[1]);
            var intersectionZ = GetIntersectionSegment(segmentsOfCubeA[2], segmentsOfCubeB[2]);

            if (intersectionX != null && intersectionY != null && intersectionZ != null)
            {
                result = new Cube(GetCenter(intersectionX, intersectionY, intersectionZ),
                                  GetSize(intersectionX),
                                  GetSize(intersectionY),
                                  GetSize(intersectionZ));
            }

            return result;
        }

        #endregion

        #region Private Methods

        private double GetSize(Segment segment)
        {
            return segment.Z - segment.A;
        }

        private Point3D GetCenter(Segment segmentX, Segment segmentY, Segment segmentZ)
        {
            return new Point3D((segmentX.A + segmentX.Z) / 2,
                               (segmentY.A + segmentY.Z) / 2,
                               (segmentZ.A + segmentZ.Z) / 2);
        }

        private Segment GetIntersectionSegment(Segment segmentA, Segment segmentB)
        {
            Segment result = null;

            double overlap = Math.Max(0, Math.Min(segmentA.Z, segmentB.Z) - Math.Max(segmentA.A, segmentB.A));

            if (overlap != 0)
            {
                result = new Segment(Math.Max(segmentA.A, segmentB.A), Math.Min(segmentA.Z, segmentB.Z));
            }

            return result;
        }

        private Segment[] GetSegments(Cube cube)
        {
            Segment[] result = new Segment[3];

            result[0] = new Segment(cube.Center.X - cube.SizeX / 2, cube.Center.X + cube.SizeX / 2);
            result[1] = new Segment(cube.Center.Y - cube.SizeY / 2, cube.Center.Y + cube.SizeY / 2);
            result[2] = new Segment(cube.Center.Z - cube.SizeZ / 2, cube.Center.Z + cube.SizeZ / 2);

            return result;
        }

        private class Segment
        {
            public double A { get; }

            public double Z { get; }

            public Segment(double a, double z)
            {
                A = a;
                Z = z;
            }
        }

        #endregion
    }
}
