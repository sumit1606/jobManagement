using System;
namespace jobManagement.Models
{
    public class JobItem
    {
        public JobItem()
        {
            
        }
        public long jobId { get; set; }
        public string companyName { get; set; }
        public bool isSelected { get; set; }
    }
}
