﻿using AppBackEnd.Domain.IRepositories;
using AppBackEnd.Domain.IServices;
using AppBackEnd.Domain.Models;

namespace AppBackEnd.Services
{
    public class RespuestaCuestionarioService: IRespuestaCuestionarioService
    {
        private readonly IRespuestaCuestionarioRepository _respuestaCuestionarioRepository; 
        public RespuestaCuestionarioService(IRespuestaCuestionarioRepository respuestaCuestionarioRepository)
        {
            _respuestaCuestionarioRepository = respuestaCuestionarioRepository;
        }

        public async Task<List<RespuestaCuestionario>> ListRespuestaCuestionario(int idCuestionario, int idUsuario)
        {
            return await _respuestaCuestionarioRepository.ListRespuestaCuestionario(idCuestionario, idUsuario);
        }

        public async Task SaveRespuestaCuestionario(RespuestaCuestionario respuestaCuestionario)
        {
            await _respuestaCuestionarioRepository.SaveRespuestaCuestionario(respuestaCuestionario);
        }

        public async Task<RespuestaCuestionario> BuscarRespuestaCuestionario(int idRtaCuestionario, int idUsuario)
        {
            return await _respuestaCuestionarioRepository.BuscarRespuestaCuestionario(idRtaCuestionario, idUsuario);
        }

        public async Task EliminarRespuestaCuestionario(RespuestaCuestionario respuestaCuestionario)
        {
           await _respuestaCuestionarioRepository.EliminarRespuestaCuestionario(respuestaCuestionario);
        }

        public async Task<int> GetIdCuestionarioByIdRespuesta(int idRespuestaCuestionario)
        {
            return await _respuestaCuestionarioRepository.GetIdCuestionarioByIdRespuesta(idRespuestaCuestionario);
        }

        public async Task<List<RespuestaCuestionarioDetalle>> GetListRespuestas(int idRespuestaCuestionario)
        {
            return await _respuestaCuestionarioRepository.GetListRespuestas(idRespuestaCuestionario);
        }
    }
}
