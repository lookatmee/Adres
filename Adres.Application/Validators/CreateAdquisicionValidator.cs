using FluentValidation;
using Adres.Application.DTOs;

namespace Adres.Application.Validators;

public class CreateAdquisicionValidator : AbstractValidator<CreateAdquisicionDto>
{
    public CreateAdquisicionValidator()
    {
        RuleFor(x => x.Presupuesto)
            .GreaterThan(0)
            .WithMessage("El presupuesto debe ser mayor a 0");

        RuleFor(x => x.Cantidad)
            .GreaterThan(0)
            .WithMessage("La cantidad debe ser mayor a 0");

        RuleFor(x => x.ValorUnitario)
            .GreaterThan(0)
            .WithMessage("El valor unitario debe ser mayor a 0");

        RuleFor(x => x.UnidadAdministrativaId)
            .GreaterThan(0)
            .WithMessage("Debe seleccionar una unidad administrativa válida");

        RuleFor(x => x.TipoBienServicioId)
            .GreaterThan(0)
            .WithMessage("Debe seleccionar un tipo de bien o servicio válido");

        RuleFor(x => x.ProveedorId)
            .GreaterThan(0)
            .WithMessage("Debe seleccionar un proveedor válido");
    }
} 