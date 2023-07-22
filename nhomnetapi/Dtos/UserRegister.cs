using System.ComponentModel.DataAnnotations;

namespace nhomnetapi.Dtos
{
    public class UserRegister
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? RoleTitle { get; set; }
        [Required]
        public string? JobTitle { get; set; }
    }
}
