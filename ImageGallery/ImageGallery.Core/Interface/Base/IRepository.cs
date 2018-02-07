using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ImageGallery.Core.Interface.Base
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All();
        T Find(Guid id);
        T FindByExpression(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindMany(Expression<Func<T, bool>> predicate);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SaveChanges();

    }
}
