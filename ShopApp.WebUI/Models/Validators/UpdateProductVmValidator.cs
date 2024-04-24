using FluentValidation;
using ShopApp.WebUI.Models.ViewModels;

namespace ShopApp.WebUI.Models.Validators
{
    public class UpdateProductVmValidator
    {
        public class UpdateProductVMValidator : AbstractValidator<UpdateProductVM>
        {
            public UpdateProductVMValidator()
            {
                RuleFor(p => p.Name).NotEmpty().WithMessage("Name Alanı Boş Geçilemez");

                RuleFor(p => p.Url).NotEmpty().WithMessage("Url Alanı Boş Geçilemez");

                RuleFor(p => p.Description).NotEmpty().WithMessage("Description Alanı Boş Geçilemez");

                RuleFor(p => p.Price).NotEmpty().WithMessage("Price Alanı Boş Geçilemez")
                                     .GreaterThan(0).WithMessage("Price alanı 0'dan büyük olmalıdır")
                                     .LessThan(100000).WithMessage("Price alanı 100000 den fazla olamaz");

            }
        }
    }
}
