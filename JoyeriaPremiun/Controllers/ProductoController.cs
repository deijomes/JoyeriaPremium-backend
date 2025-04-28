using AutoMapper;
using JoyeriaPremiun.Datos;
using JoyeriaPremiun.DTOS;
using JoyeriaPremiun.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace JoyeriaPremiun.Controllers
{
    [ApiController]
    [Route("api/producto")]
    public class ProductoController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ProductoController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }



        [HttpGet]
        public async Task<IEnumerable<productoDTO>> Get()
        {
            var producto = await context.Productos.Include(X=> X.imagenProductos)
                .ToListAsync();

             var productoDto = mapper.Map<List<productoDTO>>(producto); 

            return productoDto;
        }
       
        

         [HttpPut("{productoId:int}")]
         public async Task<ActionResult<string>> Put([FromRoute] int productoId , [FromBody] productoCreacionDTO productoCreacionDTO)
         {
             var productExiste = await context.Productos.FirstOrDefaultAsync(x => x.Id  == productoId);

             if (productExiste== null)
             {
                 return BadRequest($"Producto no existente");

             }

             var product = mapper.Map<Producto>(productoCreacionDTO);
             productExiste.Categoria = product.Categoria;
             productExiste.Descripcion = product.Descripcion;
             productExiste.Precio = product.Precio;

             await context.SaveChangesAsync();
             return Ok();
         }
         


         [HttpPost("{productoId:int}/imagenes")]
         public async Task<ActionResult> AgregarImagen([FromRoute] int productoId, [FromForm] imagenCreacionDTO imagenCreacionDTO)
         {

             var producto = await context.Productos.FindAsync(productoId);

             if (producto == null)
             {
                 return NotFound("product not found");
             }


             imagenCreacionDTO.ProductoId = productoId;
             var imagen = mapper.Map<ImagenProducto>(imagenCreacionDTO);


             context.imagens.Add(imagen);
             await context.SaveChangesAsync();

             return Ok();
         }


        [HttpPost("{productoId:int}/descuento")]
        public async Task<ActionResult> post([FromRoute] int productoId, [FromBody] productoDescuentoCreacionDTO descuentoCreacionDTO)
        {
            var producto = await context.Productos.FindAsync(productoId);

            if (producto == null)
            {
                return NotFound("product not found");
            }

            descuentoCreacionDTO.ProductoId = productoId;

          
            if (descuentoCreacionDTO.Descuento == null)
            {
                return BadRequest("El descuento es obligatorio");
            }

            producto.descuento = descuentoCreacionDTO.Descuento.Value;


            var descuento = mapper.Map<ProductoDescuento>(descuentoCreacionDTO);
            

            context.ProductoDescuentos.Add(descuento);

            await context.SaveChangesAsync();

            return Ok();
        }


        [HttpPut("{descuentoId:int}/descuento")]
        public async Task<ActionResult> Put([FromRoute] int descuentoId, [FromBody] productoDescuentoCreacionDTO descuentoCreacionDTO)
        {
            var descuentoProducto = await context.ProductoDescuentos.FirstOrDefaultAsync(x => x.id == descuentoId);

            if (descuentoProducto == null)
            {
                return NotFound(new { error = "Descuento no encontrado", descuentoId });
            }

            var producto = await context.Productos.FirstOrDefaultAsync(x => x.Id == descuentoProducto.ProductoId);

            if (producto == null)
            {
                return NotFound(new { error = "Producto no encontrado", productoId = descuentoProducto.ProductoId });
            }

            if (descuentoCreacionDTO.Descuento == null)
            {
                return BadRequest(new { error = "El descuento es obligatorio" });
            }

            producto.descuento = descuentoCreacionDTO.Descuento.Value;
            descuentoProducto.Descuento = descuentoCreacionDTO.Descuento;

            context.Productos.Update(producto);
            context.ProductoDescuentos.Update(descuentoProducto);

            await context.SaveChangesAsync();

            return Ok(new { message = "Descuento actualizado correctamente" });
        }


    }
}
