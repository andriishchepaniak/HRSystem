using HR.DAL.Models;
using HR.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.DAL.Interfaces
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {

    }
}
