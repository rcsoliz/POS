using FluentValidation;
using POS.Application.Dtos.Product.Request;

namespace POS.Application.Validators.Product
{
    public class ProductValidator : AbstractValidator<ProductRequestDto>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name)
               .NotNull().WithMessage("El campo nombre no puede ser nulo")
               .NotEmpty().WithMessage("El campo nombre no puede ser vacio.");

            //RuleFor(x => x.Stock)
            //    .NotNull().WithMessage("El campo Stock no puede ser nulo")
            //    .NotEmpty().WithMessage("El campo Stock no puede ser vacio.");
        }
    }
}
