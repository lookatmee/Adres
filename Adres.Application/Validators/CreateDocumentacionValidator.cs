using FluentValidation;
using Adres.Application.DTOs;

namespace Adres.Application.Validators;

public class CreateDocumentacionValidator : AbstractValidator<CreateDocumentacionDto>
{
    public CreateDocumentacionValidator()
    {
        RuleFor(x => x.AdquisicionId)
            .GreaterThan(0)
            .WithMessage("La adquisición es requerida");

        RuleFor(x => x.TipoDocumento)
            .NotEmpty()
            .WithMessage("El tipo de documento es requerido")
            .MaximumLength(50)
            .WithMessage("El tipo de documento no puede exceder los 50 caracteres");

        RuleFor(x => x.NumeroDocumento)
            .NotEmpty()
            .WithMessage("El número de documento es requerido")
            .MaximumLength(50)
            .WithMessage("El número de documento no puede exceder los 50 caracteres");
    }
} 