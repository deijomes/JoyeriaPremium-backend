using AutoMapper;
using JoyeriaPremiun.Datos;
using JoyeriaPremiun.DTOS;
using JoyeriaPremiun.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JoyeriaPremiun.Controllers
{
    [ApiController]
    [Route("api/ProductosFavoritos")]
    public class favoritosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public favoritosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{usuarioId}")]
        public async Task<ActionResult<favoritosDTO>> Get(int usuarioId)
        {
            var productos = await context.favoritos
                .Include(f => f.Productos)
                .Where(f => f.UsuarioId == usuarioId)
                .Select(f => f.Productos)
                .ToListAsync();

            if (!productos.Any())
                return NotFound("No tiene productos favoritos.");

            var productosDTO = mapper.Map<List<productoDTO>>(productos);

             

            var resultado = new favoritosDTO
            {
                
                UsuarioId = usuarioId,
                favoritos = productosDTO
            };

            return Ok(resultado);
        }





        [HttpPost]
        public async Task<ActionResult> post ([FromBody] favoritosProductoDTO favoritosProducto)
        {
            if (!await context.Productos.AnyAsync(p => p.Id == favoritosProducto.ProductoId) ||
             !await context.UsuarioS.AnyAsync(u => u.Id == favoritosProducto.UsuarioId))
            {
                return NotFound("Producto o usuario no existente.");
            }

            bool existe = await context.favoritos
                .AnyAsync(f => f.ProductoId == favoritosProducto.ProductoId && f.UsuarioId == favoritosProducto.UsuarioId);

            if (existe)
                return Conflict("Ya está en favoritos.");

            var favorito = mapper.Map<FavoritoProducto>(favoritosProducto);
            context.favoritos.Add(favorito);
            await context.SaveChangesAsync();

            return Ok();

        }

        [HttpDelete("{productoId:int}")]
        public async Task<ActionResult> Remove([FromRoute] int productoId)
        {
            var favorito = await context.favoritos
                .FirstOrDefaultAsync(x => x.ProductoId == productoId);

            if (favorito == null)
            {
                return NotFound();
            }

            context.favoritos.Remove(favorito); 

            await context.SaveChangesAsync();

            return NoContent(); 
        }




    }
}
