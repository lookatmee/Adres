using AutoMapper;
using Adres.Domain.Entities;
using Adres.Application.DTOs;

namespace Adres.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Adquisicion, AdquisicionDto>();
        CreateMap<CreateAdquisicionDto, Adquisicion>();
        CreateMap<UpdateAdquisicionDto, Adquisicion>();

        CreateMap<UnidadAdministrativa, UnidadAdministrativaDto>();
        CreateMap<CreateUnidadAdministrativaDto, UnidadAdministrativa>();
        CreateMap<UpdateUnidadAdministrativaDto, UnidadAdministrativa>();

        CreateMap<TipoBienServicio, TipoBienServicioDto>();
        CreateMap<CreateTipoBienServicioDto, TipoBienServicio>();
        CreateMap<UpdateTipoBienServicioDto, TipoBienServicio>();

        CreateMap<Proveedor, ProveedorDto>();
        CreateMap<CreateProveedorDto, Proveedor>();
        CreateMap<UpdateProveedorDto, Proveedor>();

        CreateMap<Documentacion, DocumentacionDto>();
        CreateMap<HistorialCambios, HistorialCambioDto>();
    }
} 