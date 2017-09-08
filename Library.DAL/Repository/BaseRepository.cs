using Library.DAL.EF;
using Library.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Library.DAL.Repository
{
    public class BaseRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationContext _context;
        protected readonly IDbSet<T> _entities;

        public BaseRepository(ApplicationContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public string Create(T entity)//return entity id
        {
            _entities.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public void Delete(string id)
        {
            var entity = GetById(id);
            if (entity != null)
                _entities.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public List<T> Find(Func<T, Boolean> predicate)
        {
            try
            {
                return _entities.Where(predicate).ToList();
            }
            catch
            {
                throw;
                //return new List<T>();
            }
        }

        public T GetById(string id)
        {
            return _entities.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.Where(o => true);
        }
    }
}
