using System.ComponentModel.DataAnnotations;

namespace Modules.Models

{
    public class Register
    {

        public int Id { get; set; }


        [Required(ErrorMessage ="Username is required")]
        public string UserName { get; set; }


        [Required(ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Email is invalid")]
        public string Email { get; set; }


        public string PasswordHash { get; set; }
    }
}


