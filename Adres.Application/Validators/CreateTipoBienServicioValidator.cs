using FluentValidation;
using Adres.Application.DTOs;

namespace Adres.Application.Validators;

public class CreateTipoBienServicioValidator : AbstractValidator<CreateTipoBienServicioDto>
{
    public CreateTipoBienServicioValidator()
    {
        RuleFor(x => x.Descripcion)
            .NotEmpty()
            .WithMessage("La descripción es requerida")
            .MaximumLength(200)
            .WithMessage("La descripción no puede exceder los 200 caracteres");
    }
} 