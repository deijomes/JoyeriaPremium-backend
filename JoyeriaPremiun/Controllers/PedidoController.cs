using AutoMapper;
using JoyeriaPremiun.Datos;
using JoyeriaPremiun.DTOS;
using JoyeriaPremiun.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JoyeriaPremiun.Controllers
{
    [ApiController]
    [Route("api/pedidos")]
    public class PedidoController:ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PedidoController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<pedidosDTO>>> Get()
        {
            var pedidos = await context.Pedidos
                          .Include(p => p.Venta)
                          .ThenInclude(v => v.VentaProductos)
                          .ThenInclude(vp => vp.Producto)
                          .Include(p => p.Venta)
                         .ThenInclude(v => v.Usuario)
                         .ThenInclude(u => u.direcciones)
                        
                         .ToListAsync();
            var pedidoDto = mapper.Map<List<pedidosDTO>>(pedidos);
            return Ok(pedidoDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<pedidosDTO>>> Get([FromRoute] string id)
        {
            
            var usuarioExiste = await context.Users.AnyAsync(u => u.Id == id);
            if (!usuarioExiste)
            {
                return NotFound($"usuario no encontrado");
            }

           
            var pedidos = await context.Pedidos
                .Include(p => p.Venta)
                    .ThenInclude(v => v.VentaProductos)
                        .ThenInclude(vp => vp.Producto)
                .Include(p => p.Venta)
                    .ThenInclude(v => v.Usuario)
                        .ThenInclude(u => u.direcciones)
                .Include(p => p.Direccion)
                .Where(p => p.Venta.Usuario.Id == id)
                .ToListAsync();

           
            if (!pedidos.Any())
            {
                return NotFound($"El usuario con id '{id}' no tiene pedidos.");
            }

            var pedidosDTO = mapper.Map<List<pedidosDTO>>(pedidos);
            return pedidosDTO;
        }

        [HttpPut("{id}/estado")]
        public async Task<IActionResult> ActualizarEstado(int id, [FromBody] ActualizarEstadoPedidoDTO dto)
        {
            var pedido = await context.Pedidos.FindAsync(id);

            if (pedido == null)
                return NotFound(new { mensaje = "Pedido no encontrado" });

           

            pedido.Estado = dto.Estado;
            await context.SaveChangesAsync();

            return Ok();
        }



    }
}
