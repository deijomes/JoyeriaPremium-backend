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

                // Crear un nuevo objeto VentaProducto y asignar los datos correspondientes
                var ventaProducto = new VentaProducto
                {
                    ProductoId = ventasProduct.ProductoId,
                    Cantidad = ventasProduct.Cantidad,
                    Producto = producto 
                };

                // Agregar el VentaProducto a la lista de productos de la venta
                venta.VentaProductos.Add(ventaProducto);
            }

            // Agregar la venta a la base de datos
            context.ventas.Add(venta);

            // Guardar los cambios en la base de datos
            await context.SaveChangesAsync();

            return Ok();
        }

    }
}
