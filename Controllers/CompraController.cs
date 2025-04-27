using AutoMapper;
using JoyeriaPremiun.Datos;
using JoyeriaPremiun.DTOS;
using JoyeriaPremiun.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JoyeriaPremiun.Controllers
{
    [ApiController]
    [Route("api/compras")]
    public class CompraController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CompraController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<compraGetDTO>>> Get()
        {
             var compra = await context.compras.Include(x => x.ProductosComprados)
                .ThenInclude(cp => cp.Producto)
                .ToListAsync();

           
            var  compraDtos = mapper.Map<List<compraGetDTO>>(compra);

            return compraDtos;
            
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] compraCreacionDTO compraCreacionDTO)
        {
            if (compraCreacionDTO.productos == null || !compraCreacionDTO.productos.Any())
            {
                return BadRequest("No se enviaron productos.");
            }

            var compra = mapper.Map<Compra>(compraCreacionDTO);
            compra.ProductosComprados = new List<CompraProductoS>();

            foreach (var item in compraCreacionDTO.productos)
            {
                var producto = await context.Productos.FirstOrDefaultAsync(x => x.Codigo == item.CodigoProducto);
                

                if (producto == null)
                {
                    producto = mapper.Map<Producto>(item);
                    context.Productos.Add(producto);
                    producto.Codigo = item.CodigoProducto;
                    producto.Nombre = item.NombreProducto;
                    await context.SaveChangesAsync();
                }

                var productoCompra = mapper.Map<CompraProductoS>(item);
                productoCompra.ProductoId = producto.Id;
                productoCompra.Total = item.Total;
                compra.ProductosComprados.Add(productoCompra);
                var stock = await context.compraProductos.Where(x => x.ProductoId == producto.Id).SumAsync(x => x.Cantidad);
                producto.Stock = stock+item.Cantidad;
                context.Productos.Update(producto);
            }

            context.compras.Add(compra);

            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{compraId:int}")]
        public async Task<IActionResult> Put(int compraId, [FromBody] compraPutDTO compraUpdateDTO)
        {

            if (compraId != compraUpdateDTO.Id)
            {
                return NotFound($" El ID {compraId} No coicide con el ID {compraUpdateDTO.Id}");

            }
            var compra = await context.compras
                .Include(c => c.ProductosComprados) 
                    .ThenInclude(cp => cp.Producto)  
                .FirstOrDefaultAsync(c => c.Id == compraId);
            
            if (compra == null)
            {
                return NotFound();
            }

            compra.proveedor = compraUpdateDTO.proveedor;
            compra.FechaDeCompra = DateTime.Now; 

            // Actualizar productos dentro de la compra
            foreach (var productoDto in compraUpdateDTO.ProductosComprados)
            {

               
                var compraProducto = compra.ProductosComprados
                    .FirstOrDefault(cp => cp.ProductoId == productoDto.ProductoId);

                if (compraProducto != null)
                {
                    
                    var producto = compraProducto.Producto;

                    if (producto != null)
                    {
                       
                        producto.Stock -= compraProducto.Cantidad;

                        
                        producto.Stock += productoDto.Cantidad;

                        context.Productos.Update(producto);
                    }

                    // Actualizar los detalles del producto 
                    compraProducto.Producto.Nombre = productoDto.Nombre;
                    compraProducto.Producto.Codigo = productoDto.Codigo;

                    // Actualizar cantidad y precio de compra en CompraProductoS
                    compraProducto.Cantidad = productoDto.Cantidad;
                    compraProducto.PrecioDeCompra = productoDto.PrecioDeCompra;
                    compraProducto.Total = compraProducto.Cantidad * compraProducto.PrecioDeCompra;
                }
                
            }

            await context.SaveChangesAsync();

            return NoContent();  
        }

        [HttpDelete("{compraId:int}")]
        public async Task<IActionResult> Delete(int compraId)
        {
            var compra = await context.compras
                .Include(c => c.ProductosComprados)
                .FirstOrDefaultAsync(c => c.Id == compraId);

            if (compra == null)
            {
                return NotFound($"Compra con ID {compraId} no encontrada.");
            }

            if (compra.ProductosComprados != null && compra.ProductosComprados.Any())
            {
                foreach (var item in compra.ProductosComprados)
                {
                    var producto = await context.Productos.FirstOrDefaultAsync(x => x.Id == item.ProductoId);

                    if (producto != null)
                    {
                        
                        producto.Stock -= item.Cantidad;

                        var otrasCompras = await context.compraProductos
                         .AnyAsync(cp => cp.ProductoId == producto.Id && cp.Compra.Id != compraId);



                        if (producto.Stock <= 0 && !otrasCompras)
                        {
                            
                            context.Productos.Remove(producto);
                        }
                        else
                        {
                            
                            context.Productos.Update(producto);
                        }
                    }
                }

                context.compraProductos.RemoveRange(compra.ProductosComprados);
            }

            context.compras.Remove(compra);

            await context.SaveChangesAsync();

            return NoContent();
        }









    }
}

