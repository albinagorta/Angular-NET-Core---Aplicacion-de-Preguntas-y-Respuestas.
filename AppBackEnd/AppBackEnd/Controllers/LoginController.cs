﻿using AppBackEnd.Domain.IServices;
using AppBackEnd.Domain.Models;
using AppBackEnd.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AppBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IConfiguration _config;
        public LoginController(ILoginService loginService, IConfiguration config)
        {
            _loginService = loginService;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Usuario usuario)
        {
            try
            {
                usuario.Password = Encriptar.EncriptarPassword(usuario.Password);
                var user = await _loginService.ValidateUser(usuario);
                if(user == null)
                {
                    return BadRequest(new { message = "Usuario o contraseña invalidos" });
                }
                string tokenString = JwtConfigurator.GetToken(user, _config);
                return Ok(new { token = tokenString });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
