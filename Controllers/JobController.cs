﻿using System;
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
using Microsoft.AspNetCore.Http;

namespace jobManagement.Controllers
{
    [Route("api/[controller]")]
    public class JobController : Controller
    {
        private readonly JobServiceInterface JobService;

        // Creating a default in memory database
        // as soon as class is loaded and inject
        // database context into the class.
        public JobController(JobServiceInterface JobService)
        {
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


        /// <summary>
        /// Delete the specified id.
        /// </summary>
        /// <returns>The delete.</returns>
        /// <param name="id">id of the Job to be deleted.</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                Job j = JobService.findJobById(id);
                if (j == null)
                {
                    return NotFound("No such user exits, Please try with a valid id");
                }
                JobService.delete(id);
            }
            catch (Exception)
            {
                return BadRequest("Some exception occured. Try again");
            }
            return StatusCode(StatusCodes.Status200OK, "Job successfully deleted");
        }



    }
}
