using JoyeriaPremiun.Entidades;
using JoyeriaPremiun.VALIDATIONS;

namespace JoyeriaPremiun.DTOS
{
    public class imagenCreacionDTO
    {
            
            public int ProductoId { get; set; }

            [pesoImagenValidacion( pesoMaximoEnMegaByteS:4)]
            [tipoArchivoValidacion(grupoTipoArchivo: grupoTipoArchivo.imagen)]
            public IFormFile? foto { get; set; }

           
       
    }
}
