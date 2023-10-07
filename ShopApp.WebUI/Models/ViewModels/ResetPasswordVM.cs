using System.ComponentModel.DataAnnotations;

namespace ShopApp.WebUI.Models.ViewModels
{
    public class ResetPasswordVM
    {
        [Required]
        public string Token  { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage ="En az 6 karakter uzunluğunda bir şifre oluşturabilirsiniz")]

        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="şifreniz birbiriyle uyuşmuyor.")]
        public string Repassword { get; set; }
    }
}
