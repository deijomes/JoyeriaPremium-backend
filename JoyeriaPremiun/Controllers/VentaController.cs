using AutoMapper;
using JoyeriaPremiun.Datos;
using JoyeriaPremiun.DTOS;
using JoyeriaPremiun.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JoyeriaPremiun.Controllers
{
    [ApiController]
    [Route("api/ventas")]
    public class VentaController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public VentaController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

         [HttpGet]
         public async Task<ActionResult<IEnumerable<ventaDTO>>> Get()
         {
             var venta = await context.ventas
                        .Include(x => x.VentaProductos)
                        .ThenInclude(cp => cp.Producto)
                        .Include(x => x.usuario) // Suponiendo que Venta tiene una propiedad Usuario
                        .ToListAsync();


             var ventadto = mapper.Map<List<ventaDTO>>(venta);


             return ventadto;

         }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<ventaDTO>>> GetVentasPorUsuario(string usuarioId)
        {
            var ventas = await context.ventas
                .Where(v => v.usuarioId == usuarioId)
                .Include(v => v.VentaProductos)
                    .ThenInclude(vp => vp.Producto)
                .Include(v => v.usuario)
                .ToListAsync();

            if (ventas == null || ventas.Count == 0)
            {
                return NotFound($"No se encontraron ventas para el usuario con ID: {usuarioId}");
            }

            var ventasDTO = mapper.Map<List<ventaDTO>>(ventas);
            return Ok(ventasDTO);
        }





        [HttpPost]
        public async Task<ActionResult<totalVentaDTO>> Post([FromBody] ventaCreacionDTO ventaCreacionDTO)
        {
            
            if (ventaCreacionDTO.productos == null || !ventaCreacionDTO.productos.Any())
            {
                return BadRequest("No se enviaron productos.");
            }

           
            var venta = mapper.Map<Venta>(ventaCreacionDTO);
            venta.VentaProductos = new List<VentaProducto>();

            decimal totalVenta = 0;

            foreach (var ventasProduct in ventaCreacionDTO.productos)
            {
                
                var producto = await context.Productos
                    .Where(x => x.Id == ventasProduct.ProductoId)
                    .FirstOrDefaultAsync();

                
                if (producto == null)
                {
                    return NotFound($"Producto con ID {ventasProduct.ProductoId} no encontrado.");
                }

                
                if (producto.Stock < ventasProduct.Cantidad)
                {
                    return NotFound($"Stock insuficiente del producto {ventasProduct.ProductoId}.");
                }

                
                producto.Stock -= ventasProduct.Cantidad;

               
                var ventaProducto = new VentaProducto
                {
                    ProductoId = ventasProduct.ProductoId,
                    Cantidad = ventasProduct.Cantidad,
                    Producto = producto 
                };

                
                venta.VentaProductos.Add(ventaProducto);

                
                totalVenta += producto.PrecioDeVenta * ventasProduct.Cantidad;

            }

           

            venta.total = totalVenta;

            context.ventas.Add(venta);
            await context.SaveChangesAsync();
            var total = mapper.Map<totalVentaDTO>(venta);

            return  total;
        }

        [HttpDelete("{ventaId}")]
        public async Task<IActionResult> DeleteVenta(int ventaId)
        {
            var venta = await context.ventas
                .Include(v => v.VentaProductos)
                .FirstOrDefaultAsync(v => v.Id == ventaId);

            if (venta == null)
                return NotFound("Venta no encontrada.");

            
            context.ventaProductos.RemoveRange(venta.VentaProductos);

          
            context.ventas.Remove(venta);

            await context.SaveChangesAsync();

            return Ok("Venta eliminada correctamente.");
        }


    }
}
