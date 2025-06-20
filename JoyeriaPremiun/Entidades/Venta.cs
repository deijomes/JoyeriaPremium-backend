﻿using System.ComponentModel.DataAnnotations;

namespace JoyeriaPremiun.Entidades
{
    public class Venta
    {
      
            public int Id { get; set; }
            [Required]
            public required string UsuarioId { get; set; }
            public DateTime FechaDeCompra { get; set; } = DateTime.Now;

            public List<VentaProducto>? VentaProductos { get; set; } = new List<VentaProducto>();
            public Usuario? Usuario { get; set; }

            public decimal total { get; set; }

            public string? PaypalOrderId { get; set; }

       
           public string Estado { get; set; } = "PENDIENTE";


    }
}

