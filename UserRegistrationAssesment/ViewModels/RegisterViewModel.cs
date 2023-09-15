using System.ComponentModel.DataAnnotations;

namespace UserRegistrationAssesment.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Usernmae is required."),MinLength(6, ErrorMessage = "Username at least 6 characters long required."), MaxLength(256)]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only letters and numbers are allowed.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Name is required."), MaxLength(256)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required."), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression("^(?:\\+88|88)?(01[3-9]\\d{8})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Adderss is required.")]
        [RegularExpression("^[a-zA-Z0-9,.:# ]*$", ErrorMessage = "Special character not allowed.")]
        public string Address { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
