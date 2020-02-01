using FluentAssertions;
using ShapeCalculations.Entity.Intersector;
using ShapeCalculations.Entity.Shape;
using Xunit;

namespace ShapeCalculations.Tests.Unit
{
    public class CubesIntersectorTests
    {
        #region The GetIntersection Method

        public class TheGetIntersectionMethod
        {
            [Theory]
            // Center with single coord with positive offset, both cubes
            [InlineData(1, 1, 1, 1, 1, 1, 10, 0, 0, 1, 1, 1)]
            [InlineData(1, 1, 1, 1, 1, 1, 0, 10, 0, 1, 1, 1)]
            [InlineData(1, 1, 1, 1, 1, 1, 0, 0, 10, 1, 1, 1)]
            [InlineData(10, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1)]
            [InlineData(0, 10, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1)]
            [InlineData(0, 0, 10, 1, 1, 1, 1, 1, 1, 1, 1, 1)]
            // Center with single coord with negative offset, both cubes
            [InlineData(1, 1, 1, 1, 1, 1, -10, 0, 0, 1, 1, 1)]
            [InlineData(1, 1, 1, 1, 1, 1, 1, -10, 1, 1, 1, 1)]
            [InlineData(1, 1, 1, 1, 1, 1, 1, 1, -10, 1, 1, 1)]
            [InlineData(-10, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1)]
            [InlineData(0, -10, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1)]
            [InlineData(0, 0, -10, 1, 1, 1, 1, 1, 1, 1, 1, 1)]
            public void WhenTheTwoCubesDoesNotOverlap_ThenReturnNull(double center1X, double center1Y, double center1Z, double size1X, double size1Y, double size1Z,
                double center2X, double center2Y, double center2Z, double size2X, double size2Y, double size2Z)
            {
                // Arrange
                Cube cubeA = new Cube(new Point3D(center1X, center1Y, center1Z), size1X, size1Y, size1Z);
                Cube cubeB = new Cube(new Point3D(center2X, center2Y, center2Z), size2X, size2Y, size2Z);

                var sut = new CubesIntersector();

                // Act
                var result = sut.GetIntersection(cubeA, cubeB);

                // Assert
                result.Should().BeNull();
            }

            [Theory]
            // Cubes are proportional
            [InlineData(0, 0, 0, 1, 1, 1,
                        0, 0, 0, 2, 2, 2,
                        0, 0, 0, 1, 1, 1)]
            [InlineData(-2.5, 1.25, -4.7, 2, 2, 2,
                        -2.5, 1.25, -4.7, 1, 1, 1,
                        -2.5, 1.25, -4.7, 1, 1, 1)]
            [InlineData(1, 1, 1, 2, 3, 4,
                        1, 1, 1, 7, 8, 9,
                        1, 1, 1, 2, 3, 4)]
            // Cubes are not proportional
            [InlineData(0, 0, 0, 2, 3, 4,
                        0, 0, 0, 4, 1, 4,
                        0, 0, 0, 2, 1, 4)]
            [InlineData(5.2, -1.3, 8.4, 5.5, 3.2, 7.2,
                        5.2, -1.3, 8.4, 5.5, 8.4, 4.3,
                        5.2, -1.3, 8.4, 5.5, 3.2, 4.3)]
            public void WhenTheTwoCubesOverlapWithSameCentre_ThenIntersectionIsProperlyCalculated(
                double center1X, double center1Y, double center1Z, double size1X, double size1Y, double size1Z,
                double center2X, double center2Y, double center2Z, double size2X, double size2Y, double size2Z,
                double centerRX, double centerRY, double centerRZ, double sizeRX, double sizeRY, double sizeRZ)
            {
                // Arrange
                Cube cubeA = new Cube(new Point3D(center1X, center1Y, center1Z), size1X, size1Y, size1Z);
                Cube cubeB = new Cube(new Point3D(center2X, center2Y, center2Z), size2X, size2Y, size2Z);

                var sut = new CubesIntersector();

                // Act
                var result = sut.GetIntersection(cubeA, cubeB);

                // Assert
                ((Cube)result).Center.X.Should().BeApproximately(centerRX, 0.01);
                ((Cube)result).Center.Y.Should().BeApproximately(centerRY, 0.01);
                ((Cube)result).Center.Z.Should().BeApproximately(centerRZ, 0.01);
                ((Cube)result).SizeX.Should().BeApproximately(sizeRX, 0.01);
                ((Cube)result).SizeY.Should().BeApproximately(sizeRY, 0.01);
                ((Cube)result).SizeZ.Should().BeApproximately(sizeRZ, 0.01);
            }

            [Theory]
            [InlineData(10, 10, 10, 2, 4, 1,
                        12, 8, 10, 4, 2, 1,
                        10.5, 8.5, 10, 1, 1, 1)]
            [InlineData(-10, -10, -10, 2, 4, 1,
                        -12, -8, -10, 4, 2, 1,
                        -10.5, -8.5, -10, 1, 1, 1)]
            [InlineData(10, -10, -1, 2, 4, 4,
                        12, -8, 1, 4, 2, 4,
                        10.5, -8.5, 0, 1, 1, 2)]
            public void WhenTheTwoCubesOverlapWithDifferentCentre_ThenIntersectionIsProperlyCalculated(
                double center1X, double center1Y, double center1Z, double size1X, double size1Y, double size1Z,
                double center2X, double center2Y, double center2Z, double size2X, double size2Y, double size2Z,
                double centerRX, double centerRY, double centerRZ, double sizeRX, double sizeRY, double sizeRZ)
            {
                // Arrange
                Cube cubeA = new Cube(new Point3D(center1X, center1Y, center1Z), size1X, size1Y, size1Z);
                Cube cubeB = new Cube(new Point3D(center2X, center2Y, center2Z), size2X, size2Y, size2Z);

                var sut = new CubesIntersector();

                // Act
                var result = sut.GetIntersection(cubeA, cubeB);

                // Assert
                ((Cube)result).Center.X.Should().BeApproximately(centerRX, 0.01);
                ((Cube)result).Center.Y.Should().BeApproximately(centerRY, 0.01);
                ((Cube)result).Center.Z.Should().BeApproximately(centerRZ, 0.01);
                ((Cube)result).SizeX.Should().BeApproximately(sizeRX, 0.01);
                ((Cube)result).SizeY.Should().BeApproximately(sizeRY, 0.01);
                ((Cube)result).SizeZ.Should().BeApproximately(sizeRZ, 0.01);
            }

            [Theory]
            [InlineData(-0.5, 0, 0, 1, 1, 1,
                        0.5, 0, 0, 1, 1, 1)]
            [InlineData(0, 0.5, 0, 1, 1, 1,
                        0, -0.5, 0, 1, 1, 1)]
            [InlineData(0, 0, 0.5, 1, 1, 1,
                        0, 0, -0.5, 1, 1, 1)]
            public void WhenTheTwoCubesAreTouchingOnAnEdge_ThenReturnNull(
                double center1X, double center1Y, double center1Z, double size1X, double size1Y, double size1Z,
                double center2X, double center2Y, double center2Z, double size2X, double size2Y, double size2Z)
            {
                // Arrange
                Cube cubeA = new Cube(new Point3D(center1X, center1Y, center1Z), size1X, size1Y, size1Z);
                Cube cubeB = new Cube(new Point3D(center2X, center2Y, center2Z), size2X, size2Y, size2Z);

                var sut = new CubesIntersector();

                // Act
                var result = sut.GetIntersection(cubeA, cubeB);

                // Assert
                ((Cube)result).Should().BeNull();
            }
        }

        #endregion
    }
}
