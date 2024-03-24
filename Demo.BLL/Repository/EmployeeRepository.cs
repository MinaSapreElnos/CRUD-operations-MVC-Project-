using Demo.BLL.Interface;
using Demo.DAL.Context;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public readonly MvcAppDBContext _dBContext;
        public EmployeeRepository( MvcAppDBContext dBContext) :base(dBContext)
        {
            this._dBContext = dBContext;
        }

        public IQueryable<Employee> GetEmployeesByName(string Name)
        {
            return _dBContext.employees.Where(m => m.Name.Contains(Name));
        }
       
    }
}
