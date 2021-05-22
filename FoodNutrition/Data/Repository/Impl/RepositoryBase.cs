using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.Repository.Impl
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
    {
        protected readonly ApplicationDbContext dbContext;
        public RepositoryBase(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task Add(T entity)
        {
            if (entity is null) throw new ArgumentNullException($"{nameof(Add)} entity must not be null");
            try
            {
                await dbContext.AddAsync(entity);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e) {
                throw new Exception($"{nameof(entity)} could not be saved: {e.Message}");
            }
        }

        public async Task AddRange(List<T> entities)
        {
            if (entities is null&&!entities.Any()) throw new ArgumentNullException($"{nameof(Add)} entity must not be null or empty");
            try
            {
                await dbContext.AddRangeAsync(entities);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"{nameof(entities)} could not be saved: {e.Message}");
            }
        }

        public IQueryable<T> GetAll()
        {
            try
            {
                return dbContext.Set<T>();
            }catch(Exception e)
            {
                throw new Exception($"Couldn't retrieve entities: {e.Message}");
            }
        }
    }
}
