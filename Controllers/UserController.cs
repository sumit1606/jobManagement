using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using jobManagement.Models;
using System.Net;
using Newtonsoft.Json;
using System.Diagnostics.Contracts;
using System.Net.Http;
using Microsoft.AspNetCore.JsonPatch;
using jobManagement.Interfaces;
using jobManagement.Services;
using Microsoft.AspNetCore.Http;

namespace jobManagement.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserServiceInterface UserService;
        private readonly JobServiceInterface JobService;
        // Creating a default in memory database
        // as soon as class is loaded and inject
        // database context into the class.
        public UserController(UserServiceInterface UserService, JobServiceInterface JobService)
        {
            this.UserService = UserService;
            this.JobService = JobService;
        }

        // default value that will be given to the user
        // as our default path is host/api/user
        // GET api/user
        /// <summary>
        /// Get the specified user by username
        /// TODO :- Make sure to add the email address clause too
        /// </summary>
        /// <returns>The get.</returns>
        /// <param name="name">Name.</param>
        [HttpGet]
        public IActionResult GetAll(String name)
        {
            User currentUser = null;
            try 
            {
                if (name == null)
                {   
                    return new ObjectResult(UserService.findAllUsers());
                } 
                else 
                {
                    currentUser = UserService.findUserbyName(name);
                    if (name == null)
                        return NotFound("No User Exists with this username");
                }
            }
            catch (Exception)
            {
                return BadRequest("Some exception occured on server side, please try again");
            }
            return new ObjectResult(currentUser);
        }
        /// <summary>
        /// Gets the User by Id
        /// GET api/user/id
        /// </summary>
        /// <returns>User if present</returns>
        /// <returns>Return 404 if user is not present </returns>
        /// <param name="id">Identifier.</param>
        [HttpGet("{id}", Name = "GetUserById")]
        public IActionResult GetById(long id)
        {
            User currentUser = null;
            // assuming that UserName and Email address are the required fields
            // if they are not present send a bad request
            try
            {
                currentUser = UserService.findUserbyId(id);
                if (currentUser == null)
                return NotFound("No such user exits, Please try with a valid id");
            }
            catch (Exception)
            {
                return BadRequest("Some exception occured on server side, please try again");
            }
            return new ObjectResult(currentUser);
        }

        /// <summary>
        /// Create the specified user and making sure that
        /// this emailaddress in unique and doesn't exist before
        /// </summary>
        /// <returns>The created User with path to its id</returns>
        /// <param name="user">User.</param>
        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            // assuming that UserName and Email address are the required fields
            // if they are not present send a bad request
              try
                {   
                    if (user == null || user.UserName == null || user.EmailAddress == null)
                    {
                        return BadRequest("Some fields are missing");
                    }
                    // if no user is found than we create a new user with the
                    // given details
                    // else return a error code
                    // with a message saying that there exists a user already with this EmailAddress
                    bool userExists = UserService.isUserExist(user.EmailAddress);
                    if (userExists)
                    {
                        return StatusCode(StatusCodes.Status409Conflict, "Sorry this email address already exists");
                    }
                    UserService.Insert(user);
                }
                catch (Exception)
                {
                    return BadRequest("Some exception occured. Try again");
                }
            // TODO :- maybe send an id of the user with the path so that it can be used for redirection
                return Ok(user);   
        }

        /// <summary>
        /// Update the specified user with the given id
        /// </summary>
        /// <returns>Ok is the object was updated</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="user">user</param>
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] User user)
        {
            try
            {   
                if (user == null || user.Id != id)
                {
                    return BadRequest("Incorrect id in the body of the post");
                }
                if (UserService.findUserbyId(id) == null)
                {
                    return NotFound("No such user exits, Please try with a valid id");
                }
                bool userExists = UserService.isUserExist(user.EmailAddress);
                User userDetails = UserService.findUserbyId(id);
                if (userExists && (!userDetails.EmailAddress.Equals(user.EmailAddress)))
                {
                    return StatusCode(StatusCodes.Status409Conflict, "Sorry this email address already exists, Please try with a new one");
                }
                UserService.Update(id, user);
            }
            catch (Exception)
            {
                return BadRequest("Some exception occured. Try again");
            }
            return StatusCode(StatusCodes.Status200OK, "User successfully updated");
        }

        /// <summary>
        /// Delete the specified id.
        /// </summary>
        /// <returns>The delete.</returns>
        /// <param name="id">Identifier.</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                var user = UserService.findUserbyId(id);
                if (user == null)
                {
                    return NotFound("No such user exits, Please try with a valid id");
                }
                UserService.delete(id);
            }
            catch (Exception)
            {
                return BadRequest("Some exception occured. Try again");
            }
            return StatusCode(StatusCodes.Status200OK, "User successfully deleted");
        }

        /// <summary>
        /// Patch the specified id and patch.
        /// </summary>
        /// <returns>The patch.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="patch">Patch.</param>

        [HttpPatch("{id}")]
        public IActionResult Patch(long id, [FromBody]JsonPatchDocument<User> patch)
        {
            try
            {   
                if (UserService.findUserbyId(id) == null)
                {
                    return NotFound("No such user exits, Please try with a valid id");
                }
                foreach (var operation in patch.Operations)
                {
                    if (operation.path.Contains("EmailAddress"))
                    {
                        String newEmailAddress = operation.value.ToString();
                        bool userExists = UserService.isUserExist(newEmailAddress);
                        User user = UserService.findUserbyId(id);
                        if (userExists && (!user.EmailAddress.Equals(newEmailAddress)))
                        {
                            return StatusCode(StatusCodes.Status409Conflict, "Sorry this email address already exists, Please try with a new one");
                        }
                    }
                }
                UserService.updatePatch(patch, id);
            }
            catch (Exception)
            {
                return BadRequest("Some exception occured. Try again");
            }
            return StatusCode(StatusCodes.Status200OK, "User Patched updated");
        }

        [HttpPatch("applyjob/{id}")]
        public IActionResult applyJobPatch(long id, [FromBody]JsonPatchDocument<User> patch)
        {
            try
            {
                if (UserService.findUserbyId(id) == null)
                {
                    return NotFound("No such user exits, Please try with a valid id");
                }
                foreach (var operation in patch.Operations)
                {
                    long jobId = (long)operation.value;
                    UserService.addJobById(id, jobId);
                }
            }
            catch (Exception)
            {
                return BadRequest("Some exception occured. Try again");
            }
            return StatusCode(StatusCodes.Status200OK, "User Patched updated");
        }

    }
}
