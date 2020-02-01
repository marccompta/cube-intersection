using ShapeCalculations.Models;

namespace ShapeCalculations.Application.Contracts
{
    /// <summary>
    ///     Interface for a service to handle shape intersection operations
    /// </summary>
    public interface IIntersectionService
    {
        #region Public Methods

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
        GetIntersectionResponse GetIntersection(Cube cubeA, Cube cubeB);

        #endregion
    }
}
