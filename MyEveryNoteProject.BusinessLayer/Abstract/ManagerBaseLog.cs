using MyEveryNoteProject.Core.DataAccess;
using MyEveryNoteProject.DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEveryNoteProject.BusinessLayer.Abstract
{
    public abstract class ManagerBaseLog<T> : IDataAccess<T> where T : class
    {
        private RepositoryLog<T> repo = new RepositoryLog<T>();
        public virtual int Delete(T obj)
        {
            return repo.Delete(obj);
        }

        public virtual T Find(Expression<Func<T, bool>> where)
        {
            return repo.Find(where);
        }

        public virtual int Insert(T obj)
        {
            return repo.Insert(obj);
        }

        public virtual IQueryable<T> IQ_List(Expression<Func<T, bool>> where)
        {
            return repo.IQ_List(where);
        }

        public virtual List<T> List()
        {
            return repo.List();
        }

        public virtual List<T> List(Expression<Func<T, bool>> where)
        {
            return repo.List(where);
        }

        public virtual IQueryable<T> ListQueryable()
        {
            return repo.ListQueryable();
        }

        public virtual int Save()
        {
            return repo.Save();
        }

        public virtual int Update(T obj)
        {
            return repo.Update(obj);
        }
    }
}
