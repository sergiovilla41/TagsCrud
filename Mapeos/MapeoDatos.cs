using AutoMapper;
using Simem.AppCom.Datos.Dominio;
using Simem.AppCom.Datos.Dto;
using SImem.AppCom.Datos.Dto;
using System;

namespace Mapeos
{
    public static class MapeoDatos
    {
        public static IMapper Mapper => LazyMapeo.Value;


        private static readonly Lazy<IMapper> LazyMapeo = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MegaMenu, MegaMenuDto>().ReverseMap();
            CreateMap<GeneracionArchivo, LastDataUpdatedDto>().ReverseMap();
            CreateMap<ColumnaConjuntoDato, DataSetColumnDto>().ReverseMap();
            CreateMap<TipoVista, TipoVistaDto>().ReverseMap();
            CreateMap<Categoria, CategoriaDto>().ReverseMap();    
            CreateMap<Etiqueta, EnlaceDto>().ReverseMap();
            CreateMap<GeneracionArchivo, DatoDto>().ReverseMap();
            CreateMap<Novedad, NovedadDto>().ReverseMap();
            CreateMap<ConfiguracionPeriodicidad, PeriodicidadDto>().ReverseMap();
            CreateMap<Granularidad, GranularidadDto>().ReverseMap();
            CreateMap<EstandarizacionRegistrosFiltroDto, EstandarizacionRegistros>().ReverseMap();
            CreateMap<ConfiguracionVariable, ConfiguracionVariableDto>().ReverseMap();
            CreateMap<ConfiguracionVariable, ConfiguracionVariableDto>()
                .ForMember(dest => dest.StrFecha, opt =>  opt.MapFrom(src => src.FechaInicio.HasValue ? ((DateTime)src.FechaInicio).ToString("yyy-MM-dd") : ""))
                .ForMember(dest => dest.Vigencia, opt => opt.MapFrom(src => src.FechaFin.HasValue ? ((DateTime)src.FechaFin).ToString("yyy-MM-dd") : "Vigente")).ReverseMap();
            CreateMap<Contacto, ContactoDto>().ReverseMap();
            CreateMap<ConfiguracionVariablePrcResult, ConfiguracionVariableDto>()
    .ForMember(dest => dest.StrFecha, opt => opt.MapFrom(src => src.FechaInicio.HasValue ? ((DateTime)src.FechaInicio).ToString("yyy-MM-dd") : ""))
    .ForMember(dest => dest.Vigencia, opt => opt.MapFrom(src => src.FechaFin.HasValue ? ((DateTime)src.FechaFin).ToString("yyy-MM-dd") : "Vigente")).ReverseMap();
            CreateMap<Contacto, ContactoDto>().ReverseMap();

            CreateMap<BuscadorGeneralConjuntoDatosPrcResult, BuscadorGeneralConjuntoDatosPrcDto>();
            CreateMap<BuscadorGeneralPrcResult, BuscadorGeneralPrcDto>();
            CreateMap<CategoriasHijosPrcResult, CategoriasHijosPrcDto>();
            CreateMap<MigaPanPrcResult, MigPanPrcResultDto>();
            CreateMap<GeneracionArchivoEtiqueta, GeneracionArchivoCategoriaDto>();
            


        }
    }
}