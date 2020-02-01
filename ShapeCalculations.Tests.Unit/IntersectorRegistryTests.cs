using FluentAssertions;
using Moq;
using ShapeCalculations.Entity.Intersector;
using ShapeCalculations.Entity.Shape;
using Xunit;

namespace ShapeCalculations.Tests.Unit
{
    public class IntersectorRegistryTests
    {
        #region The GetIntersector Method

        public class TheGetIntersectorMethod
        {
            [Fact]
            public void WhenGenericTypesMatchExistingIntersector_ThenReturnTheExpectedIntersector()
            {
                // Arrange
                var expectedIntersector1 = BuildMockIntersector<Cube, Cube>();
                var expectedIntersector2 = BuildMockIntersector<Cube, Cylinder>();
                var expectedIntersector3 = BuildMockIntersector<Cylinder, Sphere>();
                var expectedIntersector4 = BuildMockIntersector<Sphere, Sphere>();

                IIntersector[] intersectors =
                {
                    expectedIntersector1,
                    expectedIntersector2,
                    expectedIntersector3,
                    expectedIntersector4
                };

                var registry = new IntersectorRegistry(intersectors);

                // Act
                var intersector1 = registry.GetIntersector<Cube, Cube>();
                var intersector2 = registry.GetIntersector<Cube, Cylinder>();
                var intersector3 = registry.GetIntersector<Cylinder, Sphere>();
                var intersector4 = registry.GetIntersector<Sphere, Sphere>();

                // Assert
                intersector1.Should().BeSameAs(expectedIntersector1, "Intersectors should be the same instance.");
                intersector2.Should().BeSameAs(expectedIntersector2, "Intersectors should be the same instance.");
                intersector3.Should().BeSameAs(expectedIntersector3, "Intersectors should be the same instance.");
                intersector4.Should().BeSameAs(expectedIntersector4, "Intersectors should be the same instance.");
            }

            [Fact]
            public void WhenGenericTypesDoNotMatchExistingIntersector_ThenReturnNull()
            {
                // Arrange
                var intersector1 = BuildMockIntersector<Cube, Cube>();

                IIntersector[] intersectors =
                {
                    intersector1
                };

                var registry = new IntersectorRegistry(intersectors);

                // Act
                var result = registry.GetIntersector<Cylinder, Sphere>();

                // Assert
                result.Should().BeNull("Null should be returned when a matching intersector instance is not found.");
            }

            private static IIntersector<A, B> BuildMockIntersector<A, B>()
                where A : IShape
                where B : IShape
            {
                var intersectorMock = new Mock<IIntersector<A, B>>();
                var intersector = intersectorMock.Object;

                return intersector;
            }
        }

        #endregion
    }
}
