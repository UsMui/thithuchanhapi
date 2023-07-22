using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace thithuchanhapi.Models
{
    [Table(" ProjectEmployees")]
    public class ProjectEmployee
    {
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string Tasks { get; set; }


        [ForeignKey("EmployeeId")]
        public Employee Employees { get; set; }

        [ForeignKey("ProjectId")]
        public Project Projects { get; set; }
    }
}
