using AutoMapper;
using JoyeriaPremiun.Datos;
using JoyeriaPremiun.DTOS;
using JoyeriaPremiun.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;



namespace JoyeriaPremiun.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        

        public UsuarioController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<usuarioDTO>>> get ()
        {
            var usuarios = await context.UsuarioS.Where(x=> x.Estado == true).ToListAsync();

            var usuarioDto = mapper.Map<List<usuarioDTO>>(usuarios);

            return usuarioDto;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult> Registrar([FromBody] usuarioCreacionDTO usuarioCreacionDTO)
        {
           
            if (await context.UsuarioS.AnyAsync(x => x.Correo == usuarioCreacionDTO.Correo))
            {
                return BadRequest("El usuario ya existe.");
            }

           
            var usuario = mapper.Map<Usuario>(usuarioCreacionDTO);

            
            usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuarioCreacionDTO.Password);
           

            context.UsuarioS.Add(usuario);
            await context.SaveChangesAsync();

            return Ok("Usuario registrado con éxito.");
        }

        [HttpPost("loguin")]
        public async Task<ActionResult> post([FromBody] LoguinDTO loguinDTO)
        {
            
            var usuario = await context.UsuarioS.FirstOrDefaultAsync(x => x.Correo == loguinDTO.Correo);
            
            if (usuario == null)
            {
                return Unauthorized();  
            }

            bool passwordValida = BCrypt.Net.BCrypt.Verify(loguinDTO.Password, usuario.Password);

            if (!passwordValida)
            {
                return Unauthorized(); 
            }

            return Ok("Usuario autenticado con éxito.");

        } 
    }
}
