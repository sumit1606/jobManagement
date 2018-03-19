using System;
namespace jobManagement.Models
{
    public class Job
    {
        public Job()
        {
        }

        public Job(string cName, string jobDesc)
        {
            this.companyName = cName;
            this.jobDescription = jobDesc;
        }
        public long Id { get; set; }
        public string companyName { get; set; }
        public string jobDescription { get; set; }
    }
}
