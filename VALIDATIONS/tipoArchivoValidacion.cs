using System.ComponentModel.DataAnnotations;

namespace JoyeriaPremiun.VALIDATIONS
{
    public class tipoArchivoValidacion:ValidationAttribute
    {
        private readonly string[] tiposValidacion;

        public tipoArchivoValidacion( string[] tiposValidacion)
        {
            this.tiposValidacion = tiposValidacion;
        }

        public tipoArchivoValidacion(grupoTipoArchivo grupoTipoArchivo)
        {
            if (grupoTipoArchivo == grupoTipoArchivo.imagen)
            {
                tiposValidacion = new string[] {"image/jpeg", "image/png", "image/gif"};
            }
            
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            IFormFile formFile = value as IFormFile;
            if (formFile == null)
            {
                return ValidationResult.Success;

            }

            if (!tiposValidacion.Contains(formFile.ContentType))
            {
                return new ValidationResult($" El formato del archivo debe ser uno de los siguintes:" +
                    $" {string.Join(",", tiposValidacion)}");
            }
            return ValidationResult.Success;
        }



    }
    
}
