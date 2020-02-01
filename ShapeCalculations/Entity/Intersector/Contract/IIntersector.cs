using ShapeCalculations.Entity.Shape;

namespace ShapeCalculations.Entity.Intersector
{
    /// <summary>
    ///     Defines an intersector.
    /// </summary>
    public interface IIntersector
    {

    }

    /// <summary>
    ///     Defines an intersector for Shapes of types
    ///     <see>
    ///         <cref>A</cref>
    ///     </see>
    ///     and
    ///     <see>
    ///         <cref>B</cref>
    ///     </see>
    /// </summary>
    /// <typeparam name="A">
    ///     The first shape for the calculation of the intersection volume.
    /// </typeparam>
    /// <typeparam name="B">
    ///     The second shape for the calculation of the intersection volume.
    /// </typeparam>
    public interface IIntersector<in A, in B> : IIntersector
        where A : IShape
        where B : IShape
    {
        /// <summary>
        ///     Gets the intersection of two shapes.
        /// </summary>
        /// <param name="shapeA">
        ///     The shape a.
        /// </param>
        /// <param name="shapeB">
        ///     The shape b.
        /// </param>
        /// <returns>
        ///     The intersection.
        /// </returns>
        IShape GetIntersection(A shapeA, B shapeB);
    }
}
