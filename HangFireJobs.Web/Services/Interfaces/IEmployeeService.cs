using System.Collections.Generic;
using System.Threading.Tasks;
using HangFireJobs.Web.Models;

namespace HangFireJobs.Web.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<Employees>> GetAllEmployeesAsync();
        void PrintAllEmployeeTitles();
        void DisableAllEmployees();
    }
}