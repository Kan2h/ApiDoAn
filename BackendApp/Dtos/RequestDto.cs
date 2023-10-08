using System.ComponentModel.DataAnnotations;

namespace BackendApp.Dtos
{
    public class RequestDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
