using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interface
{
    public interface IGenericRepository<T> 
    {

       Task<T> GetByIdAsync(int id); 
       Task< IEnumerable<T>> GetAllAsync();

        Task AddAsync(T item); 

        void Update(T item);

        void Delete(T item);  


    }
}
