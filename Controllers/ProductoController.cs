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
       
         /*
         [HttpGet("{id}")]
         public async Task<ActionResult<ProductoDTO>> getProducto (Guid id)
         {
             var producto = await context.Productos.Include(X => X.imagenProductos)
                 .FirstOrDefaultAsync(x=> x.Id == id);

             if (producto is null)
             {
                 return NotFound();
             }

             var  productoDTO = mapper.Map<ProductoDTO>(producto);


             return productoDTO;
         }
         */

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

        /* [HttpPut("{id}")]
         public async Task<ActionResult> Put([FromRoute] Guid id, [FromBody] ProductoCreacionDTO productoCreacionDTO)
         {
             var productoExiste = await context.Productos.FirstOrDefaultAsync(x => x.Id == id);

             if (productoExiste is null)
             {
                 return NotFound();
             }


             mapper.Map(productoCreacionDTO, productoExiste);
             await context.SaveChangesAsync();

             return NoContent();
         }

         */
    }
}
