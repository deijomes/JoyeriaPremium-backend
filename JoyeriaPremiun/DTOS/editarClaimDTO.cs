using System.ComponentModel.DataAnnotations;

namespace JoyeriaPremiun.DTOS
{
    public class editarClaimDTO
    {
        [EmailAddress]
        public required string Email { get; set; }
    }
}
