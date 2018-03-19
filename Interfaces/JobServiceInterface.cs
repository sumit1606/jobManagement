
using System;
using System.Collections.Generic;
using jobManagement.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
namespace jobManagement.Interfaces
                       
{
    public interface JobServiceInterface
    {
        // creating the methods which will be implemented by the services
        // find list of user by id
        IEnumerable<Job> findAllJobs();
        Job findJobById(long id);
        void delete(long id);
    }
}

