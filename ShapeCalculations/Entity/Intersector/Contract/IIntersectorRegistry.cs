using ShapeCalculations.Entity.Shape;

namespace ShapeCalculations.Entity.Intersector
{
    /// <summary>
    ///     Defines a registry for <see cref="IIntersector" /> entities.
    /// </summary>
    public interface IIntersectorRegistry
    {
        #region Public Methods

        /// <summary>
        ///     Returns an intersector that can calculate the intersected volume of
        ///     <see>
        ///         <cref>ShapeA</cref>
        ///     </see>
        ///     and
        ///     <see>
        ///         <cref>ShapeB</cref>
        ///     </see>
        ///     shapes.
        /// </summary>
        /// <typeparam name="ShapeA">
        ///     The first shape for the calculation of the intersection volume.
        /// </typeparam>
        /// <typeparam name="ShapeB">
        ///     The second shape for the calculation of the intersection volume.
        /// </typeparam>
        /// <returns>
        ///     An appropriate intersector.
        /// </returns>
        IIntersector<ShapeA, ShapeB> GetIntersector<ShapeA, ShapeB>()
            where ShapeA : IShape
            where ShapeB : IShape;

        #endregion
    }
}
