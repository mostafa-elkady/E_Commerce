using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Core.DTO
{
    public class RegisterDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required, Phone]
        public string PhoneNumber { get; set; }

        [Required, RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", ErrorMessage = "Password Must Contains 1. LowerCase Character , 2. Uppercase Character,  3. Special Character , 4. Digit")]
        public string Password { get; set; }


    }
}
