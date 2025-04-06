using AutoMapper;
using Adres.Domain.Entities;
using Adres.Application.DTOs;

namespace Adres.Application.Mappings;

public class DocumentacionProfile : Profile
{
    public DocumentacionProfile()
    {
        CreateMap<CreateDocumentacionDto, Documentacion>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.CreadoPor, opt => opt.Ignore())
            .ForMember(dest => dest.FechaModificacion, opt => opt.Ignore())
            .ForMember(dest => dest.ModificadoPor, opt => opt.Ignore())
            .ForMember(dest => dest.Adquisicion, opt => opt.Ignore());

        CreateMap<Documentacion, DocumentacionDto>();
    }
} 