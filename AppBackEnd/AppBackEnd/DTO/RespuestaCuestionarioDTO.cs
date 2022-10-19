using AppBackEnd.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppBackEnd.DTO
{
    public class RespuestaCuestionarioDTO
    {
        [Required]
        public string NombreParticipante { get; set; }
        public int CuestionarioId { get; set; }
        public List<RespuestaCuestionarioDetalleDTO> ListRtaCuestionarioDetalle { get; set; }
    }

    public class RespuestaCuestionarioDetalleDTO
    {
        public int RespuestaId { get; set; }
    }
}
