

using AppBackEnd.Domain.Models;
using AppBackEnd.DTO;
using AutoMapper;

namespace AppBackEnd.Utils
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CuestionarioDTO, Cuestionario>();
            CreateMap<PreguntaDTO, Pregunta>();
            CreateMap<RespuestaDTO, Respuesta>();

            CreateMap<RespuestaCuestionarioDTO, RespuestaCuestionario>();
            CreateMap<RespuestaCuestionarioDetalleDTO, RespuestaCuestionarioDetalle>();
        }
                
    }
}
