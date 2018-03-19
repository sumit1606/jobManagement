using System;
using System.Collections.Generic;

namespace jobManagement.Models
{
    public class User
    {
        public User()
        {
            
        }

        public User(string UName, string Email)
        {
            this.UserName = UName;
            this.EmailAddress = Email;
            this.Jobs = new List<Job>();
        }

        public User(string UName, string Email, long Phone)
        {
            this.UserName = UName;
            this.EmailAddress = Email;
            this.PhoneNumber = Phone;
            this.Jobs = new List<Job>();
        }
        // getters and setters
        // Id will be auto provided by the in memory database
        public long Id { get; set; }
        public string UserName { get; set; }
        public List<Job> Jobs { get; set; }
        public string EmailAddress { get; set; }
        public long PhoneNumber { get; set; }
    }
}
