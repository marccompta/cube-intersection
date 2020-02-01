using ShapeCalculations.Models;

namespace ShapeCalculations.Application.Contracts
{
    /// <summary>
    ///     Interface of service for string to shape object parsing
    /// </summary>
    public interface IParserService
    {
        #region Public Methods

        /// <summary>
        ///     Given a string, returns the Cube object it descrives
        /// </summary>
        /// <param name="strCube">
        ///     A string.
        /// </param>
        /// <returns>
        ///     The cube object the strCube string represents.
        /// </returns>
        Cube ToCube(string strCube);

        #endregion
    }
}
