using System;
using Microsoft.EntityFrameworkCore;
namespace jobManagement.Models
{
    public class JobContext: DbContext
    {
        public JobContext(DbContextOptions<JobContext> options)
            : base(options)
        {
        }
        public DbSet<Job> Jobs { get; set; }
    }
}

