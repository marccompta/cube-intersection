using AutoFixture;
using FluentAssertions;
using Moq;
using ShapeCalculations.Entity.Intersector;
using ShapeCalculations.Models;
using System;
using Xunit;

namespace ShapeCalculations.Application.Impl.Tests.Unit
{
    public class IntersectionServiceTests
    {

        #region Members and Methods

        private static readonly Fixture _fixture = new Fixture();

        #endregion

        #region The GetIntersection Method

        public class TheGetintersectionMethod
        {
            [Fact]
            public void WhenCubeAIsNull_ThenThrowArgumentNullException()
            {
                // Arrange
                var sut = new IntersectionService(null);
                var cubeB = _fixture.Create<Cube>();

                // Act
                Action action = () => sut.GetIntersection(null, cubeB);

                // Assert
                action.Should().Throw<ArgumentNullException>();
            }

            [Fact]
            public void WhenCubeBIsNull_ThenThrowArgumentNullException()
            {
                // Arrange
                var sut = new IntersectionService(null);
                var cubeA = _fixture.Create<Cube>();

                // Act
                Action action = () => sut.GetIntersection(cubeA, null);

                // Assert
                action.Should().Throw<ArgumentNullException>();
            }

            [Fact]
            public void WhenCorrectInputIsProvided_ThenMethodWorksProperlyWithItsDependencies()
            {
                // Arrange
                var cube = _fixture.Create<Entity.Shape.Cube>();

                var intersectorMock = new Mock<IIntersector<Entity.Shape.Cube, Entity.Shape.Cube>>();
                intersectorMock.Setup(i => i.GetIntersection(It.IsAny<Entity.Shape.Cube>(), It.IsAny<Entity.Shape.Cube>()))
                    .Returns(cube);

                var intersectorRegistryMock = new Mock<IIntersectorRegistry>();
                intersectorRegistryMock.Setup(reg => reg.GetIntersector<Entity.Shape.Cube, Entity.Shape.Cube>())
                    .Returns(intersectorMock.Object);

                var sut = new IntersectionService(intersectorRegistryMock.Object);

                // Act
                sut.GetIntersection(_fixture.Create<Cube>(), _fixture.Create<Cube>());

                // Assert
                intersectorMock.Verify(i => i.GetIntersection(It.IsAny<Entity.Shape.Cube>(), It.IsAny<Entity.Shape.Cube>()),
                    Times.Once(), "Method GetIntersection of intersector should be called once.");

                intersectorRegistryMock.Verify(i => i.GetIntersector<Entity.Shape.Cube, Entity.Shape.Cube>(),
                    Times.Once(), "Method GetIntersector of intersectorRegistry should be called once.");
            }

            [Fact]
            public void WhenCorrectInputIsProvidedAndIntersectionIsReturned_ThenIntersectionResponseIsProperlyMappedToApplResponse()
            {
                // Arrange
                var cube = _fixture.Create<Entity.Shape.Cube>();

                var intersectorMock = new Mock<IIntersector<Entity.Shape.Cube, Entity.Shape.Cube>>();
                intersectorMock.Setup(i => i.GetIntersection(It.IsAny<Entity.Shape.Cube>(), It.IsAny<Entity.Shape.Cube>()))
                    .Returns(cube);

                var intersectorRegistryMock = new Mock<IIntersectorRegistry>();
                intersectorRegistryMock.Setup(reg => reg.GetIntersector<Entity.Shape.Cube, Entity.Shape.Cube>())
                    .Returns(intersectorMock.Object);

                var sut = new IntersectionService(intersectorRegistryMock.Object);

                // Act
                var result = sut.GetIntersection(_fixture.Create<Cube>(), _fixture.Create<Cube>());

                // Assert
                result.Intersection.CenterX.Should().Be(cube.Center.X);
                result.Intersection.CenterY.Should().Be(cube.Center.Y);
                result.Intersection.CenterZ.Should().Be(cube.Center.Z);
                result.Intersection.SizeX.Should().Be(cube.SizeX);
                result.Intersection.SizeY.Should().Be(cube.SizeY);
                result.Intersection.SizeZ.Should().Be(cube.SizeZ);
                result.Intersect.Should().BeTrue();
            }

            [Fact]
            public void WhenCorrectInputIsProvidedAndNoIntersectionIsReturned_ThenIntersectionResponseIsProperlyMappedToApplResponse()
            {
                // Arrange
                var intersectorMock = new Mock<IIntersector<Entity.Shape.Cube, Entity.Shape.Cube>>();
                intersectorMock.Setup(i => i.GetIntersection(It.IsAny<Entity.Shape.Cube>(), It.IsAny<Entity.Shape.Cube>()))
                    .Returns((Entity.Shape.Cube) null);

                var intersectorRegistryMock = new Mock<IIntersectorRegistry>();
                intersectorRegistryMock.Setup(reg => reg.GetIntersector<Entity.Shape.Cube, Entity.Shape.Cube>())
                    .Returns(intersectorMock.Object);

                var sut = new IntersectionService(intersectorRegistryMock.Object);

                // Act
                var result = sut.GetIntersection(_fixture.Create<Cube>(), _fixture.Create<Cube>());

                // Assert
                result.Intersect.Should().BeFalse();
            }
        }

        #endregion
    }
}
