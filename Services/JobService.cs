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
        private readonly JobContext _context;
        public JobService(JobContext context)
        {
            _context = context;
            InitializeData();
        }

        public void delete(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Job> findAllJobs()
        {
            return _context.Jobs.ToList();
        }

        public Job findJobById(long id)
        {
            Job currJob = _context.Jobs.FirstOrDefault(j => j.Id == id);
            return currJob;
        }

        private void InitializeData()
        {
            Job jobA = new Job("Moster", "Software Developer Intern");
            Job jobB = new Job("Samsung", "Software Developer Intern");
            Job jobC = new Job("Facebook", "Software Developer Intern");
            _context.Jobs.Add(jobA);
            _context.Jobs.Add(jobB);
            _context.Jobs.Add(jobC);
            _context.SaveChanges();
        }
    }
}

