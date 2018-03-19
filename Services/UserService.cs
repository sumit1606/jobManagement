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
        private readonly UserContext _context;
        private readonly JobContext jobContext;
        public UserService(UserContext context , JobContext jobContext)
        {
            _context = context;
            this.jobContext = jobContext;
            InitializeData();
        }

        private void InitializeData()
        {
            //User testA = new User("testA", "testA@gmail.com");
            //User testB = new User("testB", "testB@gmail.com");
            //User testC = new User("testC", "testC@gmail.com");
            //List<User> intialUser = new List<User>();
            //foreach (User curr in intialUser)
            //_context.Users.Add(testA);
            //_context.Users.Add(testB);
            //_context.Users.Add(testC);
            //_context.SaveChanges();
        }

        public void addJobById(long userID, long jobId)
        {
            User user = findUserbyId(userID);
            delete(userID);
            Job currJob = jobContext.Jobs.FirstOrDefault(j => j.Id == jobId);
            if(user.Jobs == null)
            {
                List<Job> userJobs = new List<Job>();
                userJobs.Add(currJob);
                user.Jobs = userJobs;
            }
            else
            {
                List<Job> userJobs = new List<Job>();
                userJobs.Add(currJob);
                user.Jobs = userJobs;
            }
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User findUserbyId(long id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            return user;
        }
        /// <summary>
        /// Insert the User in the in memory database
        /// </summary>
        /// <param name="user">User.</param>
        public User Insert (User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public bool isUserExist (string email)
        {
            var foundUser = _context.Users.FirstOrDefault(u => u.EmailAddress == email);
            if (foundUser == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // Updating a user with the given id
        // Parameters are the id and the user
        public void Update(long id, User user)
        {
            User foundUser = _context.Users.FirstOrDefault(u => u.Id == id);
            foundUser.UserName = user.UserName;
            foundUser.PhoneNumber = user.PhoneNumber;
            foundUser.EmailAddress = user.EmailAddress;
            _context.Users.Update(foundUser);
            _context.SaveChanges();
        }

        // deleting a user with the given id
        // input id of the to be deleted user
        public void delete(long id)
        {
            User currentUser = _context.Users.FirstOrDefault(u => u.Id == id);
            _context.Users.Remove(currentUser);
            _context.SaveChanges();
        }

        // finding a user by username
        // input: username of the user
        public User findUserbyName(string name)
        {
            User currentUser = _context.Users.FirstOrDefault(u => u.UserName == name);
            return currentUser;
        }

        public IEnumerable<User> findAllUsers()
        {
            return _context.Users;
        }

        public void updatePatch(JsonPatchDocument<User> patch, long id)
        {
            User currUser = findUserbyId(id);
            patch.ApplyTo(currUser);
            _context.Users.Update(currUser);
            _context.SaveChanges();
        }

    }
}

