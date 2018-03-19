using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using jobManagement.Models;
using System.Net;
using Microsoft.AspNetCore.JsonPatch;
using jobManagement.Interfaces;
using jobManagement.Services;
using Newtonsoft.Json;

namespace jobManagement.Controllers
{
    [Route("api/[controller]")]
    public class JobController : Controller
    {
        private readonly JobContext _context;
        private readonly JobServiceInterface JobService;

        // Creating a default in memory database
        // as soon as class is loaded and inject
        // database context into the class.
        public JobController(JobContext context, JobServiceInterface JobService)
        {
            _context = context;
            this.JobService = JobService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Job> myJobs = null;
            try
            {
                myJobs = JobService.findAllJobs();
            }
            catch (Exception)
            {
                return BadRequest("Some exception occured on server side, please try again");
            }
            return new ObjectResult(myJobs);
        }
    }
}
