using FluentValidation;
using Adres.Application.DTOs;

namespace Adres.Application.Validators;

public class CreateUnidadAdministrativaValidator : AbstractValidator<CreateUnidadAdministrativaDto>
{
    public CreateUnidadAdministrativaValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty()
            .WithMessage("El nombre es requerido")
            .MaximumLength(100)
            .WithMessage("El nombre no puede exceder los 100 caracteres");
    }
} 