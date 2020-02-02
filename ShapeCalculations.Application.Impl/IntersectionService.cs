using ShapeCalculations.Application.Contracts;
using ShapeCalculations.Application.Impl.Extensions;
using ShapeCalculations.Entity.Intersector;
using ShapeCalculations.Models;
using System;

namespace ShapeCalculations.Application.Impl
{
    /// <summary>
    ///     Service to handle shape intersection operations
    /// </summary>
    /// <seealso cref="IIntersectionService" />
    public class IntersectionService : IIntersectionService
    {
        #region Fields

        IIntersectorRegistry _intersectorRegistry;

        #endregion

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="IntersectionService"/> class.
        /// </summary>
        /// <param name="intersectorRegistry">
        ///     The intersector registry.
        /// </param>
        public IntersectionService(IIntersectorRegistry intersectorRegistry)
        {
            _intersectorRegistry = intersectorRegistry;
        }

        #endregion

        #region Interface Implementation

        /// <summary>
        ///     Gets the intersection between two non-rotated Cubes cube A and cube B.
        /// </summary>
        /// <param name="cubeA">
        ///     The cube A.
        /// </param>
        /// <param name="cubeB">
        ///     The cube B.
        /// </param>
        /// <returns>
        /// </returns>
        public GetIntersectionResponse GetIntersection(Cube cubeA, Cube cubeB)
        {
            GetIntersectionResponse result = null;

            if(cubeA == null)
            {
                throw new ArgumentNullException(nameof(cubeA));
            }

            if(cubeB == null)
            {
                throw new ArgumentNullException(nameof(cubeB));
            }

            IIntersector<Entity.Shape.Cube, Entity.Shape.Cube> intersector = _intersectorRegistry.GetIntersector<Entity.Shape.Cube, Entity.Shape.Cube>();
            var intersection = intersector.GetIntersection(cubeA.ToDomain(), cubeB.ToDomain());

            result = intersection != null
                ? new GetIntersectionResponse
                {
                    Intersection = GetApplicationModel((Entity.Shape.Cube)intersection),
                    Intersect = true
                }
                : new GetIntersectionResponse
                {
                    Intersect = false
                };

            return result;
        }

        #endregion

        #region Private Methods

        private Cube GetApplicationModel(Entity.Shape.Cube cube)
        {
            return new Cube(
                Math.Round(cube.Center.X, 2), 
                Math.Round(cube.Center.Y, 2), 
                Math.Round(cube.Center.Z, 2), 
                Math.Round(cube.SizeX, 2), 
                Math.Round(cube.SizeY, 2), 
                Math.Round(cube.SizeZ, 2));
        }

        #endregion
    }
}
