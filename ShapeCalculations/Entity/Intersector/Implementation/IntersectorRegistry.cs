using ShapeCalculations.Entity.Shape;
using System.Collections.Generic;
using System.Linq;

namespace ShapeCalculations.Entity.Intersector
{
    public class IntersectorRegistry : IIntersectorRegistry
    {
        #region Fields

        private readonly IEnumerable<IIntersector> _intersectors;

        #endregion

        #region Constructor

        public IntersectorRegistry(IEnumerable<IIntersector> intersectors)
        {
            _intersectors = intersectors;
        }

        #endregion

        #region Interface Implementation

        public IIntersector<ShapeA, ShapeB> GetIntersector<ShapeA, ShapeB>()
            where ShapeA : IShape
            where ShapeB : IShape
        {
            return _intersectors.OfType<IIntersector<ShapeA, ShapeB>>().FirstOrDefault();
        }

        #endregion
    }
}
