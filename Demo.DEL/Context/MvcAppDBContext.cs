using Demo.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Context
{
    public class MvcAppDBContext :IdentityDbContext<AppLication_User>
    {

        public MvcAppDBContext( DbContextOptions<MvcAppDBContext> options):base(options) { }
        
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(" server = . ; Data Base = MvcApp ; Trusted_Connecton = true ; MultipleActionResultSets = true ");
        //}

        public DbSet<Department>departments { get; set; }

        public DbSet<Employee> employees { get; set; }

    }
}
