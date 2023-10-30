using FluentValidation;
using POS.Application.Dtos.DocumentType.Request;

namespace POS.Application.Validators.DocumentType
{
    public class DocumentTypeValidator: AbstractValidator<DocumentTypeRequestDto>
    {
        public DocumentTypeValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("El campo nombre no puede ser nulo.")
                .NotEmpty().WithMessage("El campo nombre no puede ser vacio.");

            RuleFor(x => x.Abbreviation)
                .NotNull().WithMessage("El campo Abreviacion no puede ser nulo.")
                .NotEmpty().WithMessage("El campo Abreviación no puede ser vacio");

        }
    }
}
