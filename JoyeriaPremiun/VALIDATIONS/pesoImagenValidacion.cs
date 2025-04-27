using System.ComponentModel.DataAnnotations;

namespace JoyeriaPremiun.VALIDATIONS
{
    public class pesoImagenValidacion : ValidationAttribute
    {
        private readonly int pesoMaximoEnMegaByteS;

        public pesoImagenValidacion(int pesoMaximoEnMegaByteS)
        {
            this.pesoMaximoEnMegaByteS = pesoMaximoEnMegaByteS;
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

            if (formFile.Length > pesoMaximoEnMegaByteS * 1024 * 1024)
            {
                return new ValidationResult($"el peso maximo  del archino no debe ser mayor  a {pesoMaximoEnMegaByteS}mb");
            }

            return ValidationResult.Success;
        }
    }
}