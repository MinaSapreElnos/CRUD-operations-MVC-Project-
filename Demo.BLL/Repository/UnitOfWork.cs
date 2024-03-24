using Demo.BLL.Interface;
using Demo.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repository
{
    public class UnitOfWork : IUnitOfWork ,IDisposable 
    {
        private readonly MvcAppDBContext dBContext;

        public IEmployeeRepository EmployeeRepository { get ; set ; }
        public IDepartmentRepository DepartmentRepository { get ; set ; }

        public UnitOfWork( MvcAppDBContext dBContext)
        {
            EmployeeRepository = new EmployeeRepository(dBContext);

            DepartmentRepository = new DepartmentRepository(dBContext);
            this.dBContext = dBContext;
        }

        public async Task< int> completeAsync()
        {
            return await dBContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            dBContext.Dispose();
        }
    }
}
