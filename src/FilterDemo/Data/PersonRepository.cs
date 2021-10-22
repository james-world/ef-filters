using FilterDemo.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FilterDemo.Data
{
    public class PersonRepository
    {
        private readonly FilterDemoContext context;

        public PersonRepository(FilterDemoContext context)
        {
            this.context = context;
        }

        public Task<List<Person>> GetAllAsync(
            Dictionary<string, object[]> filters)
        {
            var query = AppendFilters(context.People, filters);
            Console.WriteLine(query.ToQueryString()); // just for debug            
            return query.ToListAsync();
        }

        private IQueryable<TEntity> AppendFilters<TEntity>(IQueryable<TEntity> set, Dictionary<string, object[]> filters)
        {
            var query = set;            
            foreach(var kv in filters)
            {
                query = AppendFilter(query, kv);
            }
            return query;
        }

        private IQueryable<TEntity> AppendFilter<TEntity>(IQueryable<TEntity> set, KeyValuePair<string, object[]> filterDef)
        {
            var entityType = typeof(TEntity);
            var entityInstance = Expression.Parameter(entityType);
            var entityProperty = entityType.GetProperty(filterDef.Key);
            var memberAccessor = Expression.Property(entityInstance, entityProperty);
            var query = set;

            var filter = filterDef.Value.Select(value =>
            {
                var valueConst = Expression.Constant(value);
                var equalityCheck = Expression.Equal(memberAccessor, valueConst);
                return equalityCheck;
            }).Aggregate((a, b) => Expression.OrElse(a, b));

            return query.Where(Expression.Lambda<Func<TEntity, bool>>(filter, entityInstance));
        }
    }
}
