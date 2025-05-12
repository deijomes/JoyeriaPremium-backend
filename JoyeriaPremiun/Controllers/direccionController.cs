using AutoMapper;
using JoyeriaPremiun.Datos;
using JoyeriaPremiun.DTOS;
using JoyeriaPremiun.Entidades;
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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<direccionDTO>>> Get([FromRoute] int id)
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


        [HttpPost("{id:int}")]
        public async Task<ActionResult> Post([FromRoute] int id, [FromBody] direccionCreacionDTO direccionCreacionDTO)
        {
            var existeUsuario = await context.UsuarioS.AnyAsync(x => x.Id == id);
            if (!existeUsuario)
            {
                return BadRequest("El usuario no existe.");
            }

            if (direccionCreacionDTO == null)
            {
                return BadRequest("No se enviaron datos.");
            }

            var direccion = mapper.Map<Direccion>(direccionCreacionDTO);
            direccion.UsuarioId = id;

            context.direcciones.Add(direccion);
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

         
            mapper.Map(direccionCreacionDTO, direccionExistente);

            await context.SaveChangesAsync();

            return NoContent();
        }


    }
}
