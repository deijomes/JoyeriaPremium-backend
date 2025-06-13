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


    }
}
