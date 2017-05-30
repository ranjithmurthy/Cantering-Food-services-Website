using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace AutomatedTellerMachine.Models
{
    public class FakeDbSet<T> : IDbSet<T> where T : class
    {
        private readonly List<T> list = new List<T>();

        public FakeDbSet()
        {
            list = new List<T>();
        }

        public FakeDbSet(IEnumerable<T> contents)
        {
            list = contents.ToList();
        }

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion IEnumerable<T> Members

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion IEnumerable Members

        #region IDbSet<T> Members

        public T Add(T entity)
        {
            list.Add(entity);
            return entity;
        }

        public T Attach(T entity)
        {
            list.Add(entity);
            return entity;
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            throw new NotImplementedException();
        }

        public T Create()
        {
            throw new NotImplementedException();
        }

        public T Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<T> Local
        {
            get { throw new NotImplementedException(); }
        }

        public T Remove(T entity)
        {
            list.Remove(entity);
            return entity;
        }

        #endregion IDbSet<T> Members

        #region IQueryable Members

        public Type ElementType
        {
            get { return list.AsQueryable().ElementType; }
        }

        public Expression Expression
        {
            get { return list.AsQueryable().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return list.AsQueryable().Provider; }
        }

        #endregion IQueryable Members
    }
}