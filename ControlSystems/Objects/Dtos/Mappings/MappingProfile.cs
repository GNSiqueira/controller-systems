using AutoMapper;
using ControlSystems.Objects.Dtos.Entities;
using ControlSystems.Objects.Models;

namespace ControlSystems.Objects.Dtos.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Usuario, UsuarioDTO>();

        CreateMap<UsuarioSenhaDTO, Usuario>();

        CreateMap<UsuarioUpdateDTO, Usuario>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())

            .ForMember(dest => dest.Nome, opt => opt.Condition(src => src.Nome != null))
            .ForMember(dest => dest.Email, opt => opt.Condition(src => src.Email != null))

            .ForMember(dest => dest.TipoUsuario, opt => opt.Condition(src => src.TipoUsuario.HasValue))
            .ForMember(dest => dest.Status, opt => opt.Condition(src => src.Status.HasValue))
            .ForMember(dest => dest.EmpresaId, opt => opt.Condition(src => src.EmpresaId.HasValue));
    }
}