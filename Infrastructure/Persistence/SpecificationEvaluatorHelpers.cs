using Domain.Contracts;

namespace Persistence
{
    internal static class SpecificationEvaluatorHelpers
    {
        public static IQueryable<TEntity> GetQuEry<TEntity, TKey>(
        IQueryable<TEntity> inputQuery,
        ISpecifications<TEntity, TKey> spec
        )
            where TEntity : BaseEntity<TKey>
        {

        }
    }
}