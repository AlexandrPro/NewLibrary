using Library.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Library.DAL.Repository
{
    public class BaseRepository<T> where T : BaseEntity
    {
        private readonly DbContext _context;
        private readonly IDbSet<T> _entities;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public void Create(T entity)
        {
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
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
            return _entities.Where(predicate).ToList();
        }

        public T GetById(int id)
        {
            return _entities.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.Where(o => true);
        }
    }
}
