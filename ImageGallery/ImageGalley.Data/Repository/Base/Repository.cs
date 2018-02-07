using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ImageGallery.Core.Interface.Base;
using ImageGalley.Data.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace ImageGalley.Data.Repository.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _entities;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }
        public IQueryable<T> All()
        {
            return _entities.AsNoTracking();
        }

        public T Find(Guid id)
        {
            //return _entities.SingleOrDefault( == id);
            throw new NotImplementedException();
        }

        public T FindByExpression(Expression<Func<T, bool>> predicate)
        {
            return _entities
                .AsNoTracking()
                .SingleOrDefault(predicate);
        }

        public IQueryable<T> FindMany(Expression<Func<T, bool>> predicate)
        {
            return _entities
                .AsNoTracking()
                .Where(predicate);
        }

        public void Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                _entities.Add(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                _context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                _entities.Remove(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
