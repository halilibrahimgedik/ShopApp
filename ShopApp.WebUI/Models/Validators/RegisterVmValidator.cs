using FluentValidation;
using ShopApp.WebUI.Models.ViewModels;
using System.Text.RegularExpressions;

namespace ShopApp.WebUI.Models.Validators
{
    public class RegisterVmValidator : AbstractValidator<RegisterVM>
    {
        public RegisterVmValidator()
        {
            RuleFor(x => x.FirstName).NotNull().WithMessage("Lütfen FirstName alanını doldurunuz.");
            RuleFor(x => x.LastName).NotNull().WithMessage("Lütfen LastName alanını doldurunuz.");
            RuleFor(x => x.UserName).NotNull().WithMessage("Lütfen UserName alanını doldurunuz.");
            RuleFor(x => x.Email).NotNull().EmailAddress().WithMessage("Lütfen E-mail alanını doldurunuz.");

            RuleFor(x => x.Password)
                .NotNull()
                .MinimumLength(8).WithMessage("Password en az 8 karakter içermelidir") // En az 8 karakter olmalı
                .Matches("[A-Z]") // En az bir büyük harf içermeli
                .Matches("[0-9]") // En az bir rakam içermeli
                .WithMessage("Password en az bir büyük harf ve bir rakam içermelidir.")
                .Must(password => password != null && Regex.IsMatch(password, @"[A-Z]+") && Regex.IsMatch(password, @"\d+"))
                .WithMessage("Password en az bir büyük harf ve bir rakam içermelidir.");

            RuleFor(x => x.RePassword).Equal(x => x.Password).WithMessage("Şifreler uyuşmuyor.");
        }
    }
}
