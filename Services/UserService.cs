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
    public class UserService : UserServiceInterface
    {
        //private readonly UserContext _context;
        //private readonly JobContext jobContext;
        private readonly JobServiceInterface JobService;
        private long id = 0;
        List<User> currentUsers = new List<User>();
        public UserService(JobServiceInterface JobService)
        {
            this.JobService = JobService;
            InitializeData();
        }

        /// <summary>
        /// Initialing the static data which can be replace with a
        /// connection to database
        /// </summary>
        private void InitializeData()
        {
            User testA = new User(id++,"testA", "testA@gmail.com");
            User testB = new User(id++,"testB", "testB@gmail.com");
            User testC = new User(id++,"testC", "testC@gmail.com");
            currentUsers.Add(testA);
            currentUsers.Add(testB);
            currentUsers.Add(testC);
        }

        /// <summary>
        /// Finds the User with the given username
        /// </summary>
        /// <returns>User with the given usernae</returns>
        /// <param name="name">Name.</param>
        public User findUserbyName(string name)
        {
            // iterating over the data and check if User
            // exits otherwise returns null
            for (int i = 0; i < currentUsers.Count; i++)
            {
                if (currentUsers[i].EmailAddress == name)
                    return currentUsers[i];
            }
            return null;
        }

        /// <summary>
        /// Finds all users.
        /// </summary>
        /// <returns>List of Users</returns>
        public IEnumerable<User> findAllUsers()
        {
            return currentUsers;
        }

        /// <summary>
        /// Finds the userby ID.
        /// </summary>
        /// <returns>The found User if User is found else returns null</returns>
        /// <param name="id">User Id in the system</param>
        public User findUserbyId(long id)
        {
            for (int i = 0; i < currentUsers.Count; i++)
            {
                if (currentUsers[i].Id == id)
                    return currentUsers[i];
            }
            return null;
        }

        /// <summary>
        /// Insert the User in the static memory defined
        /// </summary>
        /// <returns>The newly added User</returns>
        /// <param name="user">User.</param>
        public User Insert (User user)
        {
            user.Id = this.id++;
            user.Jobs = new List<Job>();
            currentUsers.Add(user);
            return user;
        }

        /// <summary>
        /// Checking if there exits a User in correspond to a email address .
        /// </summary>
        /// <returns><c>true</c>, if email address exist <c>false</c> otherwise.</returns>
        /// <param name="email">Email.</param>
        public bool isUserExist (string email)
        {
            for (int i = 0; i < currentUsers.Count; i++) 
            {
                if (currentUsers[i].EmailAddress.Equals(email))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Update the specified id with the given user.
        /// </summary>
        /// <returns>Null</returns>
        /// <param name="id">Identifier of the User</param>
        /// <param name="user">New User object</param>
        public void Update(long id, User user)
        {
            User foundUser = findUserbyId(id);
            foundUser.UserName = user.UserName;
            foundUser.PhoneNumber = user.PhoneNumber;
            foundUser.EmailAddress = user.EmailAddress;
            foundUser.Jobs = user.Jobs;
        }

        /// <summary>
        /// Delete the User of specified id.
        /// </summary>
        /// <returns>Null </returns>
        /// <param name="id">Identifier.</param>
        public void delete(long id)
        {
            for (int i = 0; i < currentUsers.Count; i++) 
            {
                if (currentUsers[i].Id == id)
                    currentUsers.RemoveAt(i);
            }
        }

        /// <summary>
        /// Updates the User
        /// </summary>
        /// <param name="patch">Patch.</param>
        /// <param name="id">Identifier.</param>
        public void updatePartialUser(JsonPatchDocument<User> patch, long id)
        {
            User currUser = findUserbyId(id);
            patch.ApplyTo(currUser);
        }

        /// <summary>
        /// Adding the Job mentioned to the current user
        /// it is akin to if a user is applying to a job
        /// </summary>
        /// <returns>Nothing</returns>
        /// <param name="">User identifier.</param>
        public void addJobById(long id, JsonPatchDocument<User> patch, long jobId)
        {
            User currUser = findUserbyId(id);
            foreach (var operation in patch.Operations)
            {
                if(operation.path.Equals("/jobs"))
                {
                    operation.value = JobService.findJobById(jobId);
                }
            }
            patch.ApplyTo(currUser);
        }

    }
}

