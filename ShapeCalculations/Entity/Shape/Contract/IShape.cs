namespace ShapeCalculations.Entity.Shape
{
    /// <summary>
    ///     Represents a shape
    /// </summary>
    public interface IShape
    {
        #region Public methods

        /// <summary>
        ///     Gets the type of the shape
        /// </summary>
        /// <returns>
        ///     The type of the shape.
        /// </returns>
        ShapeType GetShapeType();

        #endregion
    }
}
