using AssignmentForFullStack.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentForFullStack.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext dbContext;

        public BaseRepository(ApplicationDbContext _dbContext) 
        {
            dbContext = _dbContext;
        }
        public T Add(T entity)
        {
            dbContext.Set<T>().Add(entity);
            dbContext.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            dbContext.Update(entity);
            dbContext.SaveChanges();
            return entity;
        }
        public T Delete(T entity)
        {

             dbContext.Set<T>().Remove(entity);
             dbContext.SaveChanges();
             return entity;
        }

        public IEnumerable<T> GetAll(string[] includes = null)
        {
            IQueryable<T> query = dbContext.Set<T>();

            if (includes != null)
                {
                    foreach (string include in includes)
                        query = query.Include(include);
                }

            return query.ToList();
        }

        public T GetByCode(string id)
        {
            return dbContext.Set<T>().Find(id);
        }

        public T GetById(int id)
        {
            return dbContext.Set<T>().Find(id);
        }

        public IEnumerable<T> FindAll(Func<T, bool> creteria)
        {
            return dbContext.Set<T>().Where(creteria);
        }
    }
}
