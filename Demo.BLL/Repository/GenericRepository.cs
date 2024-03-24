using Demo.BLL.Interface;
using Demo.DAL.Context;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MvcAppDBContext _dBContext;

        public GenericRepository( MvcAppDBContext dBContext) 
        {
            _dBContext = dBContext;
        }



        public  async Task AddAsync(T item)
        {
           await _dBContext.AddAsync(item);
            //return _dBContext.SaveChanges();
        }

        public void Delete(T item)
        {
            _dBContext.Remove(item);
            //return _dBContext.SaveChanges();
        }

        public  async Task< IEnumerable<T> > GetAllAsync()
        {
            if( typeof(T) == typeof(Employee))
            {
             return (IEnumerable<T>) await  _dBContext.employees.Include(E =>E.Department).ToListAsync();
            }

          return await _dBContext.Set<T>().ToListAsync() ;
        }


        public async Task<T> GetByIdAsync(int id)
        {
           return  await _dBContext.Set<T>().FindAsync(id);
        }


        public void Update(T item)
        {
            _dBContext.Update(item);
            //return _dBContext.SaveChanges();
        }
    }
}
