using System.ComponentModel.DataAnnotations;
namespace PLANETS_HOME.Models

{
    public class ViewModel
    {
        //Sign Up 
        public class SignUpViewModel
        {
            [Required(ErrorMessage = "Username is required")] //username
             public string Username { get; set; } = string.Empty;

            [Required(ErrorMessage = "Email is required")]      //email
            [EmailAddress(ErrorMessage = "Enter a valid email")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "Password is required")] //password
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;    

            [Required(ErrorMessage = "Please confirm your password")] //confirm password
            [Compare("Password", ErrorMessage = "Passwords do not match")]
            [DataType(DataType.Password)]
            public string ConfirmPassword { get; set; } = string.Empty;
        }

        //Sign In
        public class SignInViewModel
        {
            [Required(ErrorMessage = "Username is required")]
         
            public string username { get; set; } = string.Empty;

            [Required(ErrorMessage = "Password is required")]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;
        }

        //Change Password 
        public class ChangePasswordViewModel
        {
            [Required(ErrorMessage = "Current password is required")]
            [DataType(DataType.Password)]
            public string CurrentPassword { get; set; } = string.Empty;

            [Required(ErrorMessage = "New password is required")]
            [DataType(DataType.Password)]
            public string NewPassword { get; set; } = string.Empty;

            [Required(ErrorMessage = "Please confirm your new password")]
            [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
            [DataType(DataType.Password)]
            public string ConfirmNewPassword { get; set; } = string.Empty;
        }

        //Plant Form
        public class PlantFormViewModel
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "Plant name is required")]
            
            public string Name { get; set; } = string.Empty;
            public string? Category { get; set; }
            public string? LightNeed { get; set; }
            public string? WaterNeed { get; set; }
            public string? Humidity { get; set; }
            public string? Description { get; set; }
            public string? CareTips { get; set; }
        }

        // Catalog and search 
        public class CatalogViewModel
        {
            public List<Plant> Plants { get; set; } = new();
            public string? SearchQuery { get; set; }
            public string? FilterCategory { get; set; }
            public List<string> Categories { get; set; } = new();
        }

    }
}
