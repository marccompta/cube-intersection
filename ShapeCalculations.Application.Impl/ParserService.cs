using ShapeCalculations.Models;
using System;

namespace ShapeCalculations.Application.Contracts
{
    /// <summary>
    ///     Service for string to shape object parsing
    /// </summary>
    /// <seealso cref="IParserService" />
    public class ParserService : IParserService
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParserService"/> class.
        /// </summary>
        public ParserService()
        {

        }

        #endregion

        #region Interface Implementation

        /// <summary>
        ///     Converts to cube.
        /// </summary>
        /// <param name="strCube">
        ///     A string with the following format:
        ///     centerX__centerY__centerZ__sizeX__sizeY__sizeZ
        /// </param>
        /// <returns>
        ///     The cube object the strCube string represents.
        /// </returns>
        public Cube ToCube(string strCube)
        {
            if (strCube == null)
            {
                throw new ArgumentNullException(nameof(strCube));
            }

            Cube result = null;
            try
            {
                string[] cubeArgs = strCube.Split("__");
                if (cubeArgs.Length == 6)
                {
                    double sizeX = Convert.ToDouble(cubeArgs[3]);
                    double sizeY = Convert.ToDouble(cubeArgs[4]);
                    double sizeZ = Convert.ToDouble(cubeArgs[5]);

                    if (sizeX < 0)
                    {
                        throw new ArgumentNullException("Size on dimension X cannot be null.");
                    }

                    if (sizeY < 0)
                    {
                        throw new ArgumentNullException("Size on dimension Y cannot be null.");
                    }

                    if (sizeZ < 0)
                    {
                        throw new ArgumentNullException("Size on dimension Z cannot be null.");
                    }

                    result = new Cube(Convert.ToDouble(cubeArgs[0]),
                        Convert.ToDouble(cubeArgs[1]),
                        Convert.ToDouble(cubeArgs[2]),
                        sizeX,
                        sizeY,
                        sizeZ);
                }

                if (result == null)
                {
                    throw new ArgumentException(nameof(strCube));
                }

                return result;
            }
            catch (Exception)
            {
                throw new ArgumentException(nameof(strCube));
            }
        }

        #endregion
    }
}
