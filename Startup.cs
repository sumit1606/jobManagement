using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using jobManagement.Models;
using jobManagement.Interfaces;
using jobManagement.Services;

namespace jobManagement
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<UserContext>(opt => opt.UseInMemoryDatabase("User"));
            //services.AddDbContext<JobContext>(opt => opt.UseInMemoryDatabase("Job"));
            services.AddMvc();
            services.AddSingleton<UserServiceInterface, UserService>();
            services.AddSingleton<JobServiceInterface, JobService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}