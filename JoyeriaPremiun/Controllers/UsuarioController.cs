using AutoMapper;
using JoyeriaPremiun.Datos;
using JoyeriaPremiun.DTOS;
using JoyeriaPremiun.Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



namespace JoyeriaPremiun.Controllers
{
   [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly UserManager<Usuario> userManager;
        private readonly IConfiguration configuration;

        public UsuarioController(UserManager<Usuario> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        

        [HttpPost("registrar")]
        public async Task<ActionResult<respuestaAutenticacionDTO>> Registrar (usuarioCreacionDTO usuarioCreacionDTO)
        {
            var usuario = new Usuario
            {
                UserName = usuarioCreacionDTO.Nombre,
                Email = usuarioCreacionDTO.Correo,
                PhoneNumber = usuarioCreacionDTO.Telefono,

            };

            var resultado = await userManager.CreateAsync(usuario, usuarioCreacionDTO.Password);

            if (resultado.Succeeded)
            {

                var respuestAutenticacion = await ConstruirToken(usuarioCreacionDTO);
                return respuestAutenticacion;

            }
            else
            { foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return ValidationProblem();
            
            }

        }


        private async Task<respuestaAutenticacionDTO> ConstruirToken(usuarioCreacionDTO  usuarioCreacionDTO)
        {
            var claims = new List<Claim>
            {
             
               new Claim("email", usuarioCreacionDTO.Correo),
               
            };

            var usuario = await userManager.FindByEmailAsync(usuarioCreacionDTO.Correo);


            var claimsDB = await userManager.GetClaimsAsync(usuario!);
            claims.AddRange(claimsDB);

           
            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]!));

            var credenciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddHours(1);

           
            var tokenDeSeguridad = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiracion, signingCredentials: credenciales);


            var token = new JwtSecurityTokenHandler().WriteToken(tokenDeSeguridad);
           

            return new respuestaAutenticacionDTO
            {
                token = token,
                expiracion = expiracion,
                userID = usuario.Id,
                usuario = usuario.UserName!
            };
        }





    }


}
