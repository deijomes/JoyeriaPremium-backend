using AutoMapper;
using JoyeriaPremiun.DTOS;
using JoyeriaPremiun.Entidades;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JoyeriaPremiun.utilidades
{
    public class autoMapperProfiles : Profile
    {
        public autoMapperProfiles()
        {
            CreateMap<Producto, productoDTO>()
             .ForMember(dest => dest.PrecioDeVenta, opt => opt.MapFrom(src => src.PrecioDeVenta));

            CreateMap<imagenCreacionDTO, ImagenProducto>();
            CreateMap<productoCreacionDTO, Producto>();

             CreateMap<ImagenProducto, imagenProductoDTO>()
            .ForMember(dest => dest.Producto, opt => opt.MapFrom(src => src.producto.Nombre));

            CreateMap<productoDescuentoCreacionDTO, ProductoDescuento>();

            CreateMap<favoritosProductoDTO, FavoritoProducto>();
            CreateMap<FavoritoProducto, favoritosDTO>();
               


            CreateMap<usuarioCreacionDTO, Usuario>();
            CreateMap<Usuario, usuarioDTO>();

            CreateMap<compraCreacionDTO, CompraProductoS>();

            CreateMap<compraCreacionDTO, Producto>();
            CreateMap<compraCreacionDTO, Compra>()
           .ForMember(dest => dest.proveedor, opt => opt.MapFrom(ent => ent.proveedor));

            CreateMap<compraDTO, Producto>();
            CreateMap<Compra, compraGetDTO>();
            CreateMap<CompraProductoS, compraProductoDTO>()
            .ForMember (dest => dest.Nombre, opt => opt.MapFrom(ent => ent.Producto.Nombre));


            CreateMap<compraDTO, CompraProductoS>()
            .ForMember(dest => dest.Cantidad, opt => opt.MapFrom(src => src.Cantidad))
            .ForMember(dest => dest.PrecioDeCompra, opt => opt.MapFrom(src => src.PrecioDeCompra));

        }
    

    }
    
}
