using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using jobManagement.Interfaces;
using jobManagement.Models;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;

namespace jobManagement.Services
{
    public class JobService : JobServiceInterface
    {
        //private readonly JobContext _context;
        private long id = 0;
        List<Job> currentJobs = new List<Job>();
        public JobService()
        {
            InitializeData();
        }
        /// <summary>
        /// Delete the specified id from the Jobs
        /// </summary>
        /// <returns>The delete.</returns>
        /// <param name="id">Identifier.</param>
        public void delete(long id)
        {
            for (int i = 0; i < currentJobs.Count; i++) 
            {
                if (currentJobs[i].Id == id)
                    currentJobs.RemoveAt(i);
            }
        }
        /// <summary>
        /// Finds all jobs.
        /// </summary>
        /// <returns> all the jobs.</returns>
        public List<Job> findAllJobs()
        {
            return currentJobs;
        }

        /// <summary>
        /// Finds the job by identifier.
        /// </summary>
        /// <returns>The Job if present else returns null.</returns>
        /// <param name="id">Identifier.</param>
        public Job findJobById(long id)
        {
            for (int i = 0; i < currentJobs.Count; i++)
            {
                if (currentJobs[i].Id == id)
                    return currentJobs[i];
            }
            return null;
        }

        private void InitializeData()
        {
            Job jobA = new Job(id++, "Moster", "Software Developer Intern");
            Job jobB = new Job(id++, "Samsung", "Software Developer Intern");
            Job jobC = new Job(id++, "Facebook", "Software Developer Intern");
            currentJobs.Add(jobA);
            currentJobs.Add(jobB);
            currentJobs.Add(jobC);
        }
    }
}

