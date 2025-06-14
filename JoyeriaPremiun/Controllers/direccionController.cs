using AutoMapper;
using Azure.Core;
using JoyeriaPremiun.Datos;
using JoyeriaPremiun.DTOS;
using JoyeriaPremiun.Entidades;
using JoyeriaPremiun.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JoyeriaPremiun.Controllers
{
    [ApiController]
    [Route("api/direccion")]
    public class direccionController:ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public direccionController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        [EndpointSummary("Obtener dirección por usuarioId")]
        public async Task<ActionResult<IEnumerable<direccionDTO>>> Get([FromRoute] string id)
        {
            var direccionUsuario = await context.direcciones
                .Include(x => x.Usuario)
                .Where(x => x.UsuarioId == id)
                .ToListAsync();

            if (!direccionUsuario.Any())
            {
                return NotFound("No se encontraron direcciones para este usuario.");
            }

            var direccionDTO = mapper.Map<List<direccionDTO>>(direccionUsuario);

            return direccionDTO;
        }

        [HttpPost]
        public async Task<IActionResult> CrearDireccion([FromBody] direccionCreacionDTO  direccionCreacionDTO)
        {
            var usuario = await context.Users.FindAsync(direccionCreacionDTO.UsuarioId);
            if (usuario == null)
            {
                return NotFound("El usuario especificado no existe.");
            }

            var direccionDTO = mapper.Map<Direccion>(direccionCreacionDTO);

            context.direcciones.Add(direccionDTO);
            await context.SaveChangesAsync();

            return Ok();

        }




        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] direccionCreacionDTO direccionCreacionDTO)
        {
            var direccionExistente = await context.direcciones.FirstOrDefaultAsync(x => x.Id == id);
            if (direccionExistente == null)
            {
                return NotFound("La dirección no existe.");
            }

            if (direccionCreacionDTO == null)
            {
                return BadRequest("No se enviaron datos.");
            }


            var direccionDTO = mapper.Map<Direccion>(direccionExistente);
            direccionExistente.Ciudad = direccionCreacionDTO.Ciudad;
            direccionExistente.Carrera = direccionCreacionDTO.Carrera;
            direccionExistente.Calle = direccionCreacionDTO.Calle;


            await context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var direccion = await context.direcciones.FirstOrDefaultAsync(c => c.Id == id);

            if (direccion == null)
            {
                return NotFound($"Direccion  no encontrada.");
            }

            context.direcciones.Remove(direccion);
            await context.SaveChangesAsync();
            return NoContent();
        }


    }
}
