using AutoFixture;
using FluentAssertions;
using ShapeCalculations.Application.Contracts;
using ShapeCalculations.Models;
using System;
using Xunit;

namespace ShapeCalculations.Application.Impl.Tests.Unit
{
    public class ParserServiceTests
    {
        #region The ToCubeMethod Method

        public class TheToCubeMethod
        {
            private static readonly Fixture _fixture = new Fixture();

            [Fact]
            public void WhenInputIsNull_ThenArgumentNullExceptionIsThrown()
            {
                // Arrange
                var sut = new ParserService();

                // Act
                Action action = () => sut.ToCube(null);

                // Assert
                action.Should().Throw<ArgumentNullException>();
            }

            [Fact]
            public void WhenInputIsEmpty_ThenArgumentExceptionIsThrown()
            {
                // Arrange
                var sut = new ParserService();

                // Act
                Action action = () => sut.ToCube(string.Empty);

                // Assert
                action.Should().Throw<ArgumentException>();
            }

            [Theory]
            [InlineData("1__2__3__-1__2__3")]
            [InlineData("4__5__6__4__-5__6")]
            [InlineData("7__8__9__7__8__-9")]
            public void WhenInputContainsANegativeSize_ThenArgumentExceptionIsThrown(string input)
            {
                // Arrange
                var sut = new ParserService();

                // Act
                Action action = () => sut.ToCube(input);

                // Assert
                action.Should().Throw<ArgumentException>();
            }

            [Fact]
            public void WhenInputContainsANegativeCenterCoordinate_ThenItIsAccepted()
            {
                // Arrange
                var expectedCube = new Cube(-1, -1, -1, 1, 1, 1);

                var sut = new ParserService();

                // Act
                var result = sut.ToCube("-1__-1__-1__1__1__1");

                // Assert
                result.CenterX.Should().Be(expectedCube.CenterX);
                result.CenterY.Should().Be(expectedCube.CenterY);
                result.CenterZ.Should().Be(expectedCube.CenterZ);
                result.SizeX.Should().Be(expectedCube.SizeX);
                result.SizeY.Should().Be(expectedCube.SizeY);
                result.SizeZ.Should().Be(expectedCube.SizeZ);
            }

            [Fact]
            public void WhenInputContainsValidCoordinates_ThenTheseAreAccepted()
            {
                // Current implementation of fixture always returns positive values.
                var centerX = _fixture.Create<double>();
                var centerY = _fixture.Create<double>();
                var centerZ = _fixture.Create<double>();
                var sizeX = _fixture.Create<double>();
                var sizeY = _fixture.Create<double>();
                var sizeZ = _fixture.Create<double>();

                // Arrange
                var expectedCube = new Cube(centerX, centerY, centerZ, sizeX, sizeY, sizeZ);

                var sut = new ParserService();

                // Act
                var result = sut.ToCube($"{centerX}__{centerY}__{centerZ}__{sizeX}__{sizeY}__{sizeZ}");

                // Assert
                result.CenterX.Should().Be(expectedCube.CenterX);
                result.CenterY.Should().Be(expectedCube.CenterY);
                result.CenterZ.Should().Be(expectedCube.CenterZ);
                result.SizeX.Should().Be(expectedCube.SizeX);
                result.SizeY.Should().Be(expectedCube.SizeY);
                result.SizeZ.Should().Be(expectedCube.SizeZ);
            }

            [Theory]
            [InlineData("1.2__3__4.5__6__7.8__")]
            [InlineData("1.2__3__4.5__6__9")]
            [InlineData("__4.5__6__7.8__9__1")]
            [InlineData("1.2__3__4.5__6__")]
            [InlineData("__3__4.5__6__9")]
            [InlineData("3__4.5__6__9")]
            [InlineData("3__4.5__6__")]
            [InlineData("4.5__6__9")]
            [InlineData("__4.5__6__9")]
            [InlineData("4.5__6__")]
            [InlineData("4.5__6")]
            [InlineData("__4.5__6")]
            [InlineData("__5")]
            [InlineData("5")]
            [InlineData("5__")]
            public void WhenInputIsIncomplete_ThenArgumentExceptionIsThrown(string input)
            {
                // Arrange
                var sut = new ParserService();

                // Act
                Action action = () => sut.ToCube(input);

                // Assert
                action.Should().Throw<ArgumentException>();
            }

            [InlineData("1.2__3__4.5__6__7.8__9__11")]
            [InlineData("1.2__3__4.5__6__7.8__9__11__22")]
            [Theory]
            public void WhenInputHasTooManyPartitions_ThenArgumentExceptionIsThrown(string input)
            {
                // Arrange
                var sut = new ParserService();

                // Act
                Action action = () => sut.ToCube(input);

                // Assert
                action.Should().Throw<ArgumentException>();
            }

            [Theory]
            [InlineData("A__B__C__D__E__")]
            [InlineData("A__B__C__D__E")]
            [InlineData("__A__B__C__D__E")]
            [InlineData("A__B__C__D__")]
            [InlineData("__A__B__C__D")]
            [InlineData("A__B__C__D")]
            [InlineData("A__B__C__")]
            [InlineData("A__B__C")]
            [InlineData("__A__B__C")]
            [InlineData("A__B__")]
            [InlineData("A__B")]
            [InlineData("__A__B")]
            [InlineData("__A")]
            [InlineData("A")]
            [InlineData("A__")]
            public void WhenInputIsInvalid_ThenArgumentExceptionIsThrown(string input)
            {
                // Arrange
                var sut = new ParserService();

                // Act
                Action action = () => sut.ToCube(input);

                // Assert
                action.Should().Throw<ArgumentException>();
            }
        }

        #endregion
    }
}
