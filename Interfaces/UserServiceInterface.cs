using System;
using System.Collections.Generic;
using jobManagement.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace jobManagement.Interfaces
{
    public interface UserServiceInterface
    {
        // creating the methods which will be implemented by the services
        // find list of user by id
        bool isUserExist(string email);
        IEnumerable<User> findAllUsers();
        User findUserbyName(string name);
        User findUserbyId(long id);
        // inserting a user
        User Insert(User user);
        // updating a user
        void Update(long id, User user);
        void delete(long id);
        void updatePartialUser(JsonPatchDocument<User> patch, long id);
        void addJobById(long id, JsonPatchDocument<User> patch, long jobId);
    }
}
