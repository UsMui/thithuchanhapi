using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace thithuchanhapi.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        [StringLength(150)]
        [MinLength(2)]
        public string EmployeeName { get; set; }
        [Required]
        public DateTime EmployeeDOB { get; set; }
        [Required]
        public string EmployeeDepartment { get; set; }

        public virtual ICollection<ProjectEmployee>? ProjectEmployees { get; set; }
    }
}
