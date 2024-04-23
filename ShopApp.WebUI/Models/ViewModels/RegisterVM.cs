using System.ComponentModel.DataAnnotations;

namespace ShopApp.WebUI.Models.ViewModels
{
    public class RegisterVM
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? RePassword { get; set; }

        public string? Email { get; set; }
    }
}

//[Required]
//public string FirstName { get; set; }

//[Required]
//public string LastName { get; set; }

//[Required]
//public string UserName { get; set; }

//[Required]
//[DataType(DataType.Password)]
//public string Password { get; set; }

//[Required]
//[DataType(DataType.Password)]
//[Compare("Password")]
//public string RePassword { get; set; }

//[Required]
//[DataType(DataType.EmailAddress)]
//public string Email { get; set; }
