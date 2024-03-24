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
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository 
    {
        private readonly MvcAppDBContext _dBContext;

        public DepartmentRepository( MvcAppDBContext dBContext) : base(dBContext)
        {
            
        }


    }
}
