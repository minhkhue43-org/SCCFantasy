using System.ComponentModel.DataAnnotations;

namespace SCCFantasy.Web.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmedPassword { get; set; }
    }
}
