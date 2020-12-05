using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HangFireJobs.Web.Context;
using HangFireJobs.Web.Models;
using HangFireJobs.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HangFireJobs.Web.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly JobContext _context;

        public EmployeeService(JobContext context)
        {
            _context = context;
        }

        public async Task<List<Employees>> GetAllEmployeesAsync()
            => await _context.Employees.ToListAsync();

        public void PrintAllEmployeeTitles()
        {
            var emp = _context.Employees.ToList();

            emp.ForEach(x => Console.WriteLine(x.JobTitle));
        }

        public void DisableAllEmployees()
        {
            var empList = _context.Employees.ToList();
            empList.ForEach(x => x.CurrentFlag = false);
            _context.SaveChanges();
        }
    }
}
