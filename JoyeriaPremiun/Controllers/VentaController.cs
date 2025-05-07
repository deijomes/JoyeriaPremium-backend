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


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ventaCreacionDTO ventaCreacionDTO)
        {
            
            if (ventaCreacionDTO.productos == null || !ventaCreacionDTO.productos.Any())
            {
                return BadRequest("No se enviaron productos.");
            }

           
            var venta = mapper.Map<Venta>(ventaCreacionDTO);
            venta.VentaProductos = new List<VentaProducto>(); 

           
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
            }

           
            context.ventas.Add(venta);

          
            await context.SaveChangesAsync();

            return Ok();
        }

    }
}
