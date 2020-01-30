using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TesteTecnico.Domain.Entities;
using TesteTecnico.Domain.Interfaces;
using TesteTecnico.Infra.Data.Context;

namespace TesteTecnico.Infra.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SqlContext _context;

        public BaseRepository(
            SqlContext context
            ) => _context = context;

        public void Insert(T obj)
        {
            _context.Set<T>().Add(obj);
            _context.SaveChanges();
        }

        public void Remove(int id)
        {
            _context.Set<T>().Remove(Select(id));
            _context.SaveChanges();
        }

        public T Select(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IList<T> SelectAll()
        {
            return _context.Set<T>().ToList();
        }

        public void Update(T obj)
        {
            _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
