using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace thithuchanhapi.Models
{
    [Table("Projects")]

    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        [StringLength(150)]
        [MinLength(2)]

        public string ProjectName { get; set; }

        [Required]

        public DateTime ProjectStartDate { get; set; }


        public DateTime? ProjectEndDate { get; set; }


        public virtual ICollection<ProjectEmployee>? ProjectEmployees { get; set; }
    }
}
