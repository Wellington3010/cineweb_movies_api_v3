using AutoMapper;
using cineweb_movies_api.DTO;
using cineweb_movies_api.Entities;
using System;

namespace cineweb_movies_api.Mapper
{
    public class ConfigurationMapping : Profile
    {
        public ConfigurationMapping()
        {
            CreateMap<UserMovieDTO, Filme>().ForMember(x => x.Poster, opt => opt.Ignore());
            CreateMap<CreateMovieDTO, Filme>().ForMember(x => x.Poster, opt => opt.Ignore());

            CreateMap<IngressoDTO, Ingresso>()
                .ForMember(x => x.Filme, opt => opt.Ignore())
                .ForMember(x => x.FilmeId, opt => opt.Ignore()).ReverseMap();

            CreateMap<Filme, UserMovieDTO>()
                .ForMember(x => x.Poster, opt => opt.MapFrom(y => "data:image/webp;base64," + Convert.ToBase64String(y.Poster)))
                .ForMember(x => x.QuantidadeIngressos, opt => opt.MapFrom(y => y.Ingresso.Quantidade))
                .ForMember(x => x.Preco, opt => opt.MapFrom(y => y.Ingresso.Preco));

            CreateMap<ClienteDTO, Cliente>().ForMember(x => x.IdCliente, opt => opt.Ignore()).ReverseMap();
            CreateMap<Pedido, PedidoDTO>()
                .ForMember(x => x.Titulos, opt => opt.Ignore()).ReverseMap();

            CreateMap<UpdateMovieDTO, Filme>().ForMember(x => x.Poster, opt => opt.Ignore());
            CreateMap<Filme, CreateMovieDTO>().ForMember(x => x.Poster, opt => opt.MapFrom(y => "data:image/webp;base64," + Convert.ToBase64String(y.Poster)));

            CreateMap<MovieDTO, Filme>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => Guid.Parse(x.Id)));

            CreateMap<Filme, MovieDTO>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id.ToString()))
                .ForMember(x => x.Poster, opt => opt.MapFrom(y => "data:image/webp;base64," + Convert.ToBase64String(y.Poster)))
                .ForMember(x => x.QuantidadeIngressos, opt => opt.MapFrom(y => y.Ingresso.Quantidade))
                .ForMember(x => x.Preco, opt => opt.MapFrom(y => y.Ingresso.Preco));
        }
    }
}
