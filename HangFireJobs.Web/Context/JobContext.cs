using HangFireJobs.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace HangFireJobs.Web.Context
{
    public class JobContext : DbContext
    {
        public JobContext(DbContextOptions<JobContext> options) : base(options)
        {
        }

        public DbSet<Employees> Employees { get; set; }
    }
}
