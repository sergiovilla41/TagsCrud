using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using Mapeos;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata;
using System.Collections;
using Microsoft.IdentityModel.Tokens;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using Simem.AppCom.Base.Utils;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Diagnostics.CodeAnalysis;
using SImem.AppCom.Datos.Dto;

namespace Simem.AppCom.Datos.Repo
{
    public class DatoRepo : IDatoRepo
    {
        private readonly DbContextSimem _baseContext;

        public DatoRepo()
        {
            _baseContext ??= new DbContextSimem();
        }

        public void AddView(Guid IdConfiguracionGeneracionArchivos)
        {
            var result = this._baseContext.GeneracionArchivo.SingleOrDefault(obj => obj.IdConfiguracionGeneracionArchivos.Equals(IdConfiguracionGeneracionArchivos));
            if (result == null)
                return;
            result.ContadorVistas++;
            this._baseContext.SaveChanges();
        }

        public void AddViewVariable(string id)
        {
            var result = this._baseContext.ResumenConjuntoDato.SingleOrDefault(x => x.IdResumenConjuntoDato.ToString().ToLower().Equals(id.ToLower()));
            if (result == null)
                return;
            result.ContadorVistas++;
            this._baseContext.SaveChanges();
        }

        public void AddDownload(Guid IdConfiguracionGeneracionArchivos)
        {
            var result = this._baseContext.GeneracionArchivo.SingleOrDefault(obj => obj.IdConfiguracionGeneracionArchivos.Equals(IdConfiguracionGeneracionArchivos));
            if (result == null)
                return;
            result.ContadorDescargas++;
            result.FechaDescarga = DateTime.Now.ToUniversalTime().AddHours(-5.0);
            this._baseContext.SaveChanges();
        }

        public void AddDownloadVariable(string id)
        {
            var result = this._baseContext.ResumenConjuntoDato.SingleOrDefault(x => x.IdResumenConjuntoDato.ToString().ToLower().Equals(id.ToLower()));
            if (result == null)
                return;
            result.ContadorDescargas++;
            this._baseContext.SaveChanges();
        }

        public async Task<List<GeneracionArchivoCategoriaDto>> GetConjuntosDatos(Paginador paginador, string? termino, Guid? categoria)
        {
            var novedades = _baseContext.GeneracionArchivo.Where(a => a.Estado)
                .Join(_baseContext.Categoria, a => a.IdCategoriaNivel1, b => b.Id, (a, b) => new GeneracionArchivo()
                {
                    IdConfiguracionGeneracionArchivos = a.IdConfiguracionGeneracionArchivos,
                    Titulo = a.Titulo,
                    CategoriaNivel1 = b,
                    FechaActualizacion = a.FechaActualizacion
                });

            if (!string.IsNullOrEmpty(termino))
            {
                novedades = novedades.Where(a => EF.Functions.Like(EF.Functions.Collate(a.Titulo ?? "", "Latin1_general_CI_AI"), "%" + EF.Functions.Collate(termino, "Latin1_general_CI_AI") + "%")
                || EF.Functions.Like(EF.Functions.Collate(a.CategoriaNivel1!.Titulo ?? "", "Latin1_general_CI_AI"), "%" + EF.Functions.Collate(termino, "Latin1_general_CI_AI") + "%")
                );
            }

            if (!(categoria == null || categoria == Guid.Empty))
            {
                novedades = novedades.Where(a => a.CategoriaNivel1!.Id.Equals(categoria));
            }

            var conjuntos = await novedades.OrderByDescending(a => a.FechaActualizacion)
                .Skip(paginador.PageIndex * paginador.PageSize)
                .Take(paginador.PageSize)
                .Select(a => new GeneracionArchivoCategoriaDto()
                {
                    IdConfiguracionGeneracionArchivos = a.IdConfiguracionGeneracionArchivos,
                    Titulo = a.Titulo,
                    TituloCategoriaNivel1 = a.CategoriaNivel1!.Titulo,
                    FechaActualizacion = a.FechaActualizacion
                })
                .ToListAsync();

            var conjuntosEtiquetas = await _baseContext.GeneracionArchivoEtiqueta.Include(a => a.Etiqueta).ToListAsync();
            List<GeneracionArchivoCategoriaDto> conjuntosEtiquetasResult = new();

            conjuntos.ForEach(a =>
            {
                List<Etiqueta> etiquetas = new();
                List<GeneracionArchivoEtiqueta> aux = new();
                conjuntosEtiquetas.ForEach(b =>
                {
                    if (b.IdConfiguracionGeneracionArchivo == a.IdConfiguracionGeneracionArchivos)
                    {
                        aux.Add(b);
                    }
                });

                aux.ForEach(conjuntoEtiqueta =>
                {
                if (conjuntoEtiqueta.Etiqueta != null)
                {
                    etiquetas.Add(conjuntoEtiqueta.Etiqueta);
                    }

                });

                conjuntosEtiquetasResult.Add(new GeneracionArchivoCategoriaDto() {
                    IdConfiguracionGeneracionArchivos = a.IdConfiguracionGeneracionArchivos,
                    Titulo = a.Titulo,
                    TituloCategoriaNivel1 = a.TituloCategoriaNivel1,
                    FechaActualizacion = a.FechaActualizacion,
                    Etiquetas = etiquetas });
            });

            return MapeoDatos.Mapper.Map<List<GeneracionArchivoCategoriaDto>>(conjuntosEtiquetas);
        }

        public async Task<int> GetConjuntosDatosCount(Paginador paginador, string? termino, Guid? categoria)
        {
            var novedades = _baseContext.GeneracionArchivo.Where(a => a.Estado)
                .Join(_baseContext.Categoria, a => a.IdCategoriaNivel1, b => b.Id, (a, b) => new GeneracionArchivo()
                {
                    IdConfiguracionGeneracionArchivos = a.IdConfiguracionGeneracionArchivos,
                    Titulo = a.Titulo,
                    CategoriaNivel1 = b,
                    FechaActualizacion = a.FechaActualizacion
                });

            if (!string.IsNullOrEmpty(termino))
            {
                novedades = novedades.Where(a => EF.Functions.Like(EF.Functions.Collate(a.Titulo ?? "", "Latin1_general_CI_AI"), "%" + EF.Functions.Collate(termino, "Latin1_general_CI_AI") + "%")
                || EF.Functions.Like(EF.Functions.Collate(a.CategoriaNivel1!.Titulo ?? "", "Latin1_general_CI_AI"), "%" + EF.Functions.Collate(termino, "Latin1_general_CI_AI") + "%")
                );
            }

            if (!(categoria == null || categoria == Guid.Empty))
            {
                novedades = novedades.Where(a => a.CategoriaNivel1!.Id.Equals(categoria));
            }

            return await novedades.CountAsync();
        }

        [ExcludeFromCodeCoverage]
        public List<DatoDto> GetLstData()
        {
            List<DatoDto> lstReturn = new List<DatoDto>();
            try
            {
                var dbEntity = _baseContext.GeneracionArchivo.Where(w => w.Estado).ToList();
                DatoDto entityDto = new();
                foreach (var item in dbEntity)
                {
                    entityDto.IdConfiguracionGeneracionArchivos = item.IdConfiguracionGeneracionArchivos;
                    entityDto.Titulo = item.Titulo;
                    entityDto.SubTitulo = obtenerCategory(item.IdCategoria).Titulo;
                    entityDto.Descripcion = item.Descripcion;
                    entityDto.TipoVistaID = item.IdTipoVista;
                    entityDto.Estado = item.Estado;
                    entityDto.EntidadOrigen = item.EntidadOrigen;
                    entityDto.CategoriaID = item.IdCategoria;
                    entityDto.Granularidad = ObtenerGranularidad((Guid)item.IdGranularidad!).NombreGranularidad!;
                    entityDto.Periocidad = ObtenerPeriodicidad((Guid)item.IdPeriodicidad!).Periodicidad;
                    entityDto.UsuarioInsercion = item.UsuarioInsercion;
                    entityDto.FechaInsercion = item.FechaCreacion;
                    entityDto.UsuarioModificacion = item.UsuarioModificacion;
                    entityDto.FechaModificacion = item.FechaModificacion;
                    entityDto.UltimaFechaIndexado = item.UltimaFechaIndexado;
                    entityDto.UltimaFechaActualizado = item.UltimaFechaActualizado;
                    entityDto.ExtencionArchivo = GetExtencionArchivoType(item.IdTipoVista);
                    entityDto.Categoria = obtenerCategory(item.IdCategoria);
                    entityDto.TipoVista = obtenerTypeView(item.IdTipoVista);
                    entityDto.LstEnlace = ObtenerTags(item.IdConfiguracionGeneracionArchivos);
                    lstReturn.Add(entityDto);
                }
                return lstReturn;
            }
            catch (Exception)
            {
                return lstReturn;
            }
        }


        public List<EnlaceDto> ObtenerTags(Guid ConfiguracionArchivoId)
        {
            List<EnlaceDto> lstReturn = new();
            var lst = _baseContext.GeneracionArchivoEtiqueta.Select(x => new { x.EtiquetaId, x.IdConfiguracionGeneracionArchivo }).Where(x => x.IdConfiguracionGeneracionArchivo == ConfiguracionArchivoId).ToList();

            foreach (var item in lst)
            {
                EnlaceDto tag = new();
                var entityTag = _baseContext.Etiqueta.FirstOrDefault(x => x.Id == Guid.Parse(item.EtiquetaId.ToString()!));
                if (entityTag != null)
                {
                    tag.Id = entityTag.Id;
                    tag.Titulo = entityTag.Titulo;
                    lstReturn.Add(tag);
                }

            }
            return lstReturn;
        }

        public List<EnlaceDto> ObtenerTags2(string lsttags)
        {
            List<EnlaceDto> lstReturn;
            string[] lst = lsttags.Split(';');
            var tags = _baseContext.Etiqueta.Where(x => lst.Contains(x.Id.ToString()!.ToUpper()));
            lstReturn = MapeoDatos.Mapper.Map<List<EnlaceDto>>(tags);
            return lstReturn;
        }

        public DatoDto GetData(Guid idRegistry)
        {
            var item = _baseContext.GeneracionArchivo.Include(a => a.ColumnaDestino).Include(a => a.Novedades).FirstOrDefault(c => c.IdConfiguracionGeneracionArchivos.Equals(idRegistry));
            DatoDto entityDto = new DatoDto();
            if (item != null)
            {
                entityDto.IdConfiguracionGeneracionArchivos = item.IdConfiguracionGeneracionArchivos;
                entityDto.Titulo = item.Titulo;
                entityDto.SubTitulo = obtenerCategory(item.IdCategoria).Titulo;
                entityDto.Descripcion = item.Descripcion;
                entityDto.TipoVistaID = item.IdTipoVista;
                entityDto.Estado = item.Estado;
                entityDto.EntidadOrigen = item.EntidadOrigen;
                entityDto.CategoriaID = item.IdCategoria;
                entityDto.Granularidad = ObtenerGranularidad((Guid)item.IdGranularidad!).NombreGranularidad!;
                entityDto.Periocidad = ObtenerPeriodicidad((Guid)item.IdPeriodicidad!).Periodicidad;
                entityDto.UsuarioInsercion = item.UsuarioInsercion;
                entityDto.FechaInsercion = item.FechaCreacion;
                entityDto.UsuarioModificacion = item.UsuarioModificacion;
                entityDto.FechaModificacion = item.FechaModificacion;
                entityDto.ContadorVistas = (item.ContadorVistas != null) ? (int)item.ContadorVistas! : 0;
                entityDto.ContadorDescargas = (item.ContadorDescargas != null) ? (int)item.ContadorDescargas! : 0;
                entityDto.UltimaFechaIndexado = item.UltimaFechaIndexado;
                entityDto.UltimaFechaActualizado = item.UltimaFechaActualizado;
                entityDto.ExtencionArchivo = GetExtencionArchivoType(item.IdTipoVista);
                entityDto.Categoria = obtenerCategory(item.IdCategoria);
                entityDto.TipoVista = obtenerTypeView(item.IdTipoVista);
                entityDto.LstEnlace = ObtenerTags(item.IdConfiguracionGeneracionArchivos);
                entityDto.IdDataSet = item.IdDataSet;
                entityDto.FechaFin = item.FechaFin;
                entityDto.NovedadesCount = item.Novedades?.Count;
                entityDto.RecentNews = item.Novedades?.Where(a => a.fechaPublicacion > DateTime.Now.AddYears(-1)).ToList().Count;
                entityDto.ColumnaDestino = (item.ColumnaDestino != null) ? new()
                {
                    IdColumnaDestino = item.ColumnaDestino.IdColumnaDestino,
                    Nombre = item.ColumnaDestino.NombreColumnaDestino
                } : null;
                entityDto.URLDataHistorica = item.URLDataHistorica;
            }

            return entityDto;
        }

        [ExcludeFromCodeCoverage]
        public async Task<List<ResumenConjuntoDatoDto>> GetAllDataSets(Paginador? paginador, DateTime? fechaInicio, DateTime? fechaFin)
        {
            List<GeneracionArchivo> dataResult;
            if (paginador != null)
            {
                dataResult = await _baseContext.GeneracionArchivo.Where(x => x.Privacidad.Equals(false)).Skip(paginador.PageIndex * paginador.PageSize).Take(paginador.PageSize).ToListAsync();
            }
            else
            {
                if (fechaInicio != null && fechaFin != null)
                {
                    dataResult = await _baseContext.GeneracionArchivo.Where(x => x.Privacidad.Equals(false) && x.FechaCreacion.Date >= fechaInicio.Value.Date && x.FechaCreacion.Date <= fechaFin.Value.Date).ToListAsync();
                }
                else
                {
                    dataResult = await _baseContext.GeneracionArchivo.Where(x => x.Privacidad.Equals(false)).ToListAsync();
                }
            }

            List<ResumenConjuntoDatoDto> result = new List<ResumenConjuntoDatoDto>();
            var toDay = DateTime.Now.Date.ToString("yyyy-MM-dd");
            var archivo = await _baseContext.Archivo.ToListAsync();
            foreach (var item in dataResult)
            {
                ResumenConjuntoDatoDto data = new ResumenConjuntoDatoDto()
                {
                    IdConfiguracionGeneracionArchivos = item.IdConfiguracionGeneracionArchivos,
                    IdDataset = item.IdDataSet,
                    NombreConjuntoDatos = item.Titulo,
                    TipoPublicacion = "Público",
                    URLConexionAPI = KeyVaultManager.GetSecretValue(KeyVaultTypes.FilesUrl) + $"PublicData?startDate={toDay}&endDate={toDay}&datasetId={item.IdDataSet}",
                    URLConjuntoDatos = KeyVaultManager.GetSecretValue(KeyVaultTypes.FrontUrl) + "/datadetail/" + item.IdConfiguracionGeneracionArchivos,
                    FechaActualizacion = item.FechaActualizacion,
                    FechaPublicacion = item.FechaCreacion,
                    InicioDato = archivo.Where(x => x.IdConfiguracionGeneracionArchivo == item.IdConfiguracionGeneracionArchivos).OrderBy(x => x.FechaIndexado).Select(x => x.FechaIndexado).FirstOrDefault(),
                    FinDato = archivo.Where(x => x.IdConfiguracionGeneracionArchivo == item.IdConfiguracionGeneracionArchivos).OrderByDescending(x => x.FechaIndexado).Select(x => x.FechaIndexado).FirstOrDefault(),
                    FechaDescarga = item.FechaDescarga
                };
                result.Add(data);
            }

            return result;
        }

        [ExcludeFromCodeCoverage]
        public async Task<List<ResumenInventarioVariableDownloadDto>> GetAllVariables(Paginador? paginador, DateTime? fechaInicio, DateTime? fechaFin)
        {

            var previousDataResult = (from variable in _baseContext.ConfiguracionVariable
                                      join vga in _baseContext.ConfiguracionVariableGeneracionArchivo on variable.IdVariable equals vga.IdVariable into lj1
                                      from leftJoinxVga in lj1.DefaultIfEmpty()
                                      join ga in _baseContext.GeneracionArchivo on leftJoinxVga.IdConfiguracionGeneracionArchivos equals ga.IdConfiguracionGeneracionArchivos into lj2
                                      from leftJoinArchivo in lj2.DefaultIfEmpty()
                                      select new
                                      {
                                          variable.IdVariable,
                                          variable.CodVariable,
                                          variable.NombreVariable,
                                          variable.Descripcion,
                                          variable.UnidadMedida,
                                          variable.FechaCreacion,
                                          variable.FechaInicio,
                                          variable.FechaFin,
                                          leftJoinArchivo.Titulo,
                                          leftJoinArchivo.IdDataSet
                                      });
            var dataResult = await previousDataResult.ToListAsync();

            if (fechaInicio != null && fechaFin != null)
            {
                dataResult = await previousDataResult.Where(x => x.FechaCreacion.Date >= fechaInicio.Value.Date && x.FechaCreacion.Date <= fechaFin.Value.Date).ToListAsync();
            }

            List<ResumenInventarioVariableDownloadDto> result = new List<ResumenInventarioVariableDownloadDto>();
            foreach (var item in dataResult)
            {
                ResumenInventarioVariableDownloadDto data = new ResumenInventarioVariableDownloadDto()
                {
                    CodigoVariable = item.CodVariable,
                    NombreVariable = item.NombreVariable,
                    Descripcion = item.Descripcion,
                    UnidadMedida = item.UnidadMedida,
                    FechaPublicacion = item.FechaCreacion,
                    FechaInicio = item.FechaInicio,
                    FechaFin = item.FechaFin,
                    NombreDataset = item.Titulo,
                    IdDataset = item.IdDataSet
                };
                result.Add(data);
            }

            if (paginador != null)
            {
                _ = result.Skip(paginador.PageIndex * paginador.PageSize).Take(paginador.PageSize).ToList();
            }

            return result;
        }

        public async Task<DatoDto> GetDataVariable(string datasetId)
        {
            var item = _baseContext.ResumenConjuntoDato.FirstOrDefault(c => c.IdResumenConjuntoDato.ToString().ToLower().Equals(datasetId.ToLower()));
            DatoDto entityDto = new DatoDto();
            if (item != null)
            {
                entityDto.IdConfiguracionGeneracionArchivos = item.IdResumenConjuntoDato;
                entityDto.Titulo = item.Titulo;
                entityDto.SubTitulo = item.Subtitulo;
                entityDto.FechaInsercion = item.FechaCreacion;
                entityDto.ContadorVistas = (item.ContadorVistas != null) ? (int)item.ContadorVistas! : 0;
                entityDto.ContadorDescargas = (item.ContadorDescargas != null) ? (int)item.ContadorDescargas! : 0;
                entityDto.UltimaFechaActualizado = DateTime.Now;
                entityDto.IdDataSet = item.IdDataset;
                entityDto.FechaFin = item.FechaFin;
                entityDto.ColumnaDestino = new DataSetColumnDto() { Nombre = "FechaPublicacion" };

                if (item.Titulo == ResumenConjuntoTipoVista.Inventario)
                {
                    entityDto.Categoria = new CategoriaDto() { Id = Guid.Parse(ResumenConjuntoTipoVista.InventarioCategoria) };
                    entityDto.NovedadesCount = await _baseContext.Novedad.
                        Include(a => a.CategoriaNovedad).
                        Where(a => a.CategoriaNovedad!.Id.Equals(Guid.Parse(ResumenConjuntoTipoVista.InventarioCategoria)))
                        .CountAsync();

                    entityDto.RecentNews = await _baseContext.Novedad.
                        Include(a => a.CategoriaNovedad).
                        Where(a => a.CategoriaNovedad!.Id.Equals(Guid.Parse(ResumenConjuntoTipoVista.InventarioCategoria)))
                        .Where(a => a.fechaPublicacion > DateTime.Now.AddYears(-1))
                        .CountAsync();
                }
                else
                {
                    entityDto.Categoria = new CategoriaDto() { Id = Guid.Parse(ResumenConjuntoTipoVista.ConjuntoDatosCategoria) };
                    entityDto.NovedadesCount = await _baseContext.Novedad.
                        Include(a => a.CategoriaNovedad).
                        Where(a => a.CategoriaNovedad!.Id.Equals(Guid.Parse(ResumenConjuntoTipoVista.ConjuntoDatosCategoria)))
                        .CountAsync();

                    entityDto.RecentNews = await _baseContext.Novedad.
                        Include(a => a.CategoriaNovedad).
                        Where(a => a.CategoriaNovedad!.Id.Equals(Guid.Parse(ResumenConjuntoTipoVista.ConjuntoDatosCategoria)))
                        .Where(a => a.fechaPublicacion > DateTime.Now.AddYears(-1))
                        .CountAsync();
                }

            }

            return entityDto;
        }

        public CategoriaDto obtenerCategory(Guid? IdCategory)
        {
            var obj = _baseContext.Categoria.FirstOrDefault(x => x.Id.Equals(IdCategory));
            CategoriaDto cat = new CategoriaDto();
            if (obj != null)
            {
                cat.Titulo = obj.Titulo;
                cat.Id = obj.Id;
                cat.Icono = obj.Icono;
            }
            return cat;
        }


        public TipoVistaDto obtenerTypeView(Guid? IdTypeView)
        {
            var obj = _baseContext.TipoVista.FirstOrDefault(x => x.Id.Equals(IdTypeView));
            TipoVistaDto typ = new TipoVistaDto();
            if (obj != null)
            {
                typ.Titulo = obj.Titulo;
                typ.Id = obj.Id;
            }

            return typ;
        }


        public async Task<List<LastDataUpdatedDto>> GetLastUpdatedData()
        {
            List<LastDataUpdatedDto> listDataDto = new List<LastDataUpdatedDto>();
            try
            {
                var lastData = await _baseContext.GeneracionArchivo.OrderByDescending(o => o.FechaModificacion).Take(4).ToListAsync();

                foreach (var item in lastData.Where(x => x.FechaModificacion.HasValue))
                {
                    LastDataUpdatedDto dataDto = new LastDataUpdatedDto();
                    dataDto.Id = item.IdConfiguracionGeneracionArchivos!;
                    dataDto.Titulo = item.Titulo;
                    dataDto.FechaModificacion = item.FechaModificacion?.ToString("dd-MM-yyyy");
                    dataDto.Etiqueta = new List<EnlaceDto>();
                    dataDto.Icono = item.Categoria?.Icono;
                    dataDto.Subtitulo = obtenerCategory(item.Categoria!.IdCategoria).Titulo;

                    var tags = await _baseContext.GeneracionArchivoEtiqueta.Select(x => new { x.EtiquetaId, x.IdConfiguracionGeneracionArchivo }).Where(x => x.IdConfiguracionGeneracionArchivo == item.IdConfiguracionGeneracionArchivos).ToListAsync();


                    if (!tags.Any())
                        return listDataDto;

                    listDataDto.Add(dataDto);
                    foreach (var item2 in tags)
                    {
                        EnlaceDto tagDto = new EnlaceDto();
                        var tagEntity = _baseContext.Etiqueta.Where(x => x.Id == Guid.Parse(item2.EtiquetaId.ToString()!)).FirstOrDefault();
                        if (tagEntity != null)
                        {
                            tagDto.Id = tagEntity.Id;
                            tagDto.Titulo = tagEntity.Titulo;
                            tagDto.Estado = tagEntity.Estado;
                            dataDto.Etiqueta.Add(tagDto);
                        }

                    }
                }
                return listDataDto;
            }
            catch (Exception)
            {

                return listDataDto;
            }
        }

        public List<DatoDto> GetListData(string categoryIdString, string typeViewIdString, string tagsIdString, string ordenar, string textFilter)
        {
            List<Guid?> ListaId = new List<Guid?>();
            List<GeneracionArchivo> dbEntity = new List<GeneracionArchivo>();
            List<Datos.Dto.DatoDto> lstReturn = new List<DatoDto>();
            try
            {
                if (categoryIdString != null && categoryIdString != "")
                {
                    GetGuidByStringSplit(ref ListaId, categoryIdString);
                    GetDataByCategory(ref dbEntity, ListaId);
                }

                if (typeViewIdString != null && typeViewIdString != "")
                {
                    GetGuidByStringSplit(ref ListaId, typeViewIdString);
                    GetDataByTypeView(ref dbEntity, ListaId);
                }

                if (textFilter != null && textFilter != "")
                    GetDataByTextFilter(ref dbEntity, textFilter);

                MapDtoData(dbEntity, ref lstReturn);

                if (ordenar == "asc")
                    return lstReturn.OrderBy(x => x.Titulo).ToList();

                return lstReturn.OrderByDescending(x => x.Titulo).ToList();
            }
            catch (Exception)
            {
                return lstReturn;
            }

        }

        public void GetDataByCategory(ref List<GeneracionArchivo> dbEntity, List<Guid?> categoryId)
        {
            if (dbEntity.Count == 0)
            {
                dbEntity = _baseContext.GeneracionArchivo.Where(x => categoryId.Contains(x.IdCategoria)).ToList();
                return;
            }

            dbEntity = dbEntity.Where(x => categoryId.Contains(x.IdCategoria)).ToList();
        }

        public void GetDataByTypeView(ref List<GeneracionArchivo> dbEntity, List<Guid?> typeViewId)
        {
            if (dbEntity.Count == 0)
            {
                dbEntity = _baseContext.GeneracionArchivo.Where(x => typeViewId.Contains(x.IdTipoVista)).ToList();
                return;
            }

            dbEntity = dbEntity.Where(x => typeViewId.Contains(x.IdTipoVista)).ToList();
        }

        public void GetDataByTextFilter(ref List<GeneracionArchivo> dbEntity, string textFilter)
        {
            if (dbEntity.Count == 0)
            {
                dbEntity = _baseContext.GeneracionArchivo.Where(x => (x.Titulo != null && x.Titulo.ToLower().Contains(textFilter)) || (x.Descripcion != null && x.Descripcion.ToLower().Contains(textFilter.ToLower()))).ToList();
                return;
            }

            dbEntity = dbEntity.Where(x => (x.Titulo != null && x.Titulo.ToLower().Contains(textFilter)) || (x.Descripcion != null && x.Descripcion.ToLower().Contains(textFilter.ToLower()))).ToList();
        }

        [ExcludeFromCodeCoverage]
        public void MapDtoData(List<GeneracionArchivo> dbEntity, ref List<Datos.Dto.DatoDto> lstReturn)
        {
            if (dbEntity.Any())
                return;

            foreach (var item in dbEntity)
            {
                DatoDto entityDto = new DatoDto();
                entityDto.IdConfiguracionGeneracionArchivos = item.IdConfiguracionGeneracionArchivos;
                entityDto.Titulo = item.Titulo;
                entityDto.SubTitulo = obtenerCategory(item.IdCategoria).Titulo;
                entityDto.Descripcion = item.Descripcion;
                entityDto.TipoVistaID = item.IdTipoVista;
                entityDto.Estado = item.Estado;
                entityDto.EntidadOrigen = item.EntidadOrigen;
                entityDto.CategoriaID = item.IdCategoria;
                entityDto.Granularidad = ObtenerGranularidad((Guid)item.IdGranularidad!).NombreGranularidad!;
                entityDto.Periocidad = ObtenerPeriodicidad((Guid)item.IdPeriodicidad!).Periodicidad;
                entityDto.UsuarioInsercion = item.UsuarioInsercion;
                entityDto.FechaInsercion = item.FechaCreacion;
                entityDto.UsuarioModificacion = item.UsuarioModificacion;
                entityDto.FechaModificacion = item.FechaModificacion;
                entityDto.UltimaFechaIndexado = item.UltimaFechaIndexado;
                entityDto.UltimaFechaActualizado = item.UltimaFechaActualizado;
                entityDto.ExtencionArchivo = GetExtencionArchivoType(item.IdTipoVista);
                entityDto.Categoria = obtenerCategory(item.IdCategoria);
                entityDto.TipoVista = obtenerTypeView(item.IdTipoVista);
                entityDto.LstEnlace = ObtenerTags(item.IdConfiguracionGeneracionArchivos);
                entityDto.ContadorVistas = RandomNumberGenerator.GetInt32(0, 3500);
                entityDto.ContadorDescargas = RandomNumberGenerator.GetInt32(0, 3500);

                lstReturn.Add(entityDto);
            }
        }

        public static void GetGuidByStringSplit(ref List<Guid?> Lista, string splitString)
        {
            var valores = splitString.Split(',');
            foreach (var item in valores)
            {
                Guid id = Guid.Parse(item);
                Lista.Add(id);
            }
        }



        public ConjuntoDatosPaginaDto GetDataSets(string categoryId, string tagsId, string typeViewId, string textoABuscar, string ordenarPor, Paginador paginador, bool esPrivado = false)

        {
            List<DatoDto> lstDataDto = new List<DatoDto>();
            try
            {
                var resultadoConsulta = _baseContext.GeneracionArchivo.AsQueryable();

                var nroRegistros = 0;

                if (!string.IsNullOrEmpty(categoryId))
                {
                    List<Guid?> ListaIdCategoria = new List<Guid?>();
                    ListaIdCategoria.Add(Guid.Parse(categoryId));
                    getIdCategorias(Guid.Parse(categoryId), ref ListaIdCategoria);
                    resultadoConsulta = resultadoConsulta.Where(x => ListaIdCategoria.Contains(x.IdCategoria) && x.Privacidad == esPrivado && x.Estado);

                }

                BuscarPorTags(ref resultadoConsulta, tagsId);
                BuscarPorTipoVista(ref resultadoConsulta, typeViewId, esPrivado);


                if (!string.IsNullOrEmpty(textoABuscar))
                {
                    BuscarPorTexto(ref resultadoConsulta, textoABuscar, esPrivado);
                }

                GenerarConsulta(ref resultadoConsulta, ref nroRegistros, ordenarPor, paginador, esPrivado);

                var result = resultadoConsulta;
                lstDataDto = MapeoDatos.Mapper.Map<List<DatoDto>>(result);
                generarResultados(ref lstDataDto);

                ConjuntoDatosPaginaDto resultado = new ConjuntoDatosPaginaDto
                {
                    totalFilas = nroRegistros,
                    resultadoJson = lstDataDto
                };
                return resultado;
            }
            catch (Exception)
            {
                ConjuntoDatosPaginaDto resultado = new ConjuntoDatosPaginaDto
                {
                    totalFilas = 0,
                    resultadoJson = lstDataDto
                };
                return resultado;
            }
        }

        public void getIdCategorias(Guid CategoriaId, ref List<Guid?> Lista)
        {
            var categoriaId = new SqlParameter("@CategoriaId", CategoriaId);
            var resp = _baseContext.IdGeneracionArchivos.FromSqlRaw("EXEC simem.BuscarConjuntoDatoxCategoriaId @CategoriaId", categoriaId).ToList();

            foreach (var idCategoria in resp.Select(x => x.IdConfiguracionGeneracionArchivos))
            {
                Guid id = idCategoria;
                Lista.Add(id);
            }
        }

        public void getIdsdataSetxTag(ref List<Guid?> listIdDataSets, string splitString)
        {
            try
            {
                var values = splitString.Split(',');
                List<Guid?> listIdTag = new List<Guid?>();

                foreach (var item in values)
                {
                    Guid id = Guid.Parse(item);
                    listIdTag.Add(id);
                }

                var lst = _baseContext.GeneracionArchivoEtiqueta.Select(x => new { x.EtiquetaId, x.IdConfiguracionGeneracionArchivo }).Where(x => listIdTag.Contains(x.EtiquetaId)).ToList();
                foreach (var item in lst)
                {
                    listIdDataSets.Add(item.IdConfiguracionGeneracionArchivo);
                }
            }
            catch (Exception)
            {
                //En caso de error listIdDataSets no se ve afectado
            }
        }

        [ExcludeFromCodeCoverage]
        private void BuscarPorTags(ref IQueryable<GeneracionArchivo> resultadoConsulta, string tagsId)
        {

            if (!string.IsNullOrEmpty(tagsId))
            {
                List<Guid?> ListDatasetId = new List<Guid?>();
                var values = tagsId.Split(',');
                foreach (var item in values)
                {
                    Guid id = Guid.Parse(item);
                    ListDatasetId.Add(id);
                }

                resultadoConsulta = resultadoConsulta.Join(
                    _baseContext.GeneracionArchivoEtiqueta.Where(a => ListDatasetId.Contains(a.EtiquetaId)),
                    a => a.IdConfiguracionGeneracionArchivos,
                    b => b.IdConfiguracionGeneracionArchivo,
                    (a, b) => a);
            }
        }

        private static void BuscarPorTipoVista(ref IQueryable<GeneracionArchivo> resultadoConsulta, string typeViewId, bool esPrivado)
        {
            List<Guid?> ListaTypeViewId = new List<Guid?>();
            if (!string.IsNullOrEmpty(typeViewId))
            {
                GetGuidByStringSplit(ref ListaTypeViewId, typeViewId);
                resultadoConsulta = resultadoConsulta.Where(t => ListaTypeViewId.Contains(t.IdTipoVista) && t.Privacidad == esPrivado && t.Estado);
            }

        }

        [ExcludeFromCodeCoverage]
        private void generarResultados(ref List<DatoDto> lstDataDto)
        {
            _ = lstDataDto.All(p => { p.SubTitulo = (p.CategoriaID != null || p.CategoriaID != Guid.Empty) ? obtenerCategory((Guid)p.CategoriaID!).Titulo : ""; return true; });
            _ = lstDataDto.All(c => { c.LstEnlace = (c.IdConfiguracionGeneracionArchivos != Guid.Empty) ? ObtenerTags(c.IdConfiguracionGeneracionArchivos) : new List<EnlaceDto>(); return true; });
            _ = lstDataDto.All(p => { p.Periocidad = (p.IdPeriodicidad != null || p.IdPeriodicidad != Guid.Empty) ? ObtenerPeriodicidad((Guid)p.IdPeriodicidad!).Periodicidad : ""; return true; });
            _ = lstDataDto.All(p => { p.TipoVista = (p.TipoVistaID != null || p.TipoVistaID != Guid.Empty) ? obtenerTypeView((Guid)p.TipoVistaID!) : new TipoVistaDto(); return true; });

        }


        [ExcludeFromCodeCoverage]
        private void BuscarPorTexto(ref IQueryable<GeneracionArchivo> resultadoConsulta, string textoABuscar, bool esPrivado)
        {

            resultadoConsulta = resultadoConsulta.Where(x => x.Titulo!.ToLower().Contains(textoABuscar.Trim().ToLower())
                                                               || x.Categoria!.Titulo!.ToLower().Contains(textoABuscar.Trim().ToLower())
                                                               || _baseContext.Etiqueta
                                                                    .Any(tg => tg.Titulo!.ToLower().Contains(textoABuscar))
                                                               || x.Descripcion!.ToLower().Equals(textoABuscar.Trim().ToLower())
                                                               && x.Privacidad == esPrivado
                                                               );

        }

        private PeriodicidadDto ObtenerPeriodicidad(Guid idPeriodicidad)
        {
            PeriodicidadDto periodicidadDto;
            var periodicidadResult = _baseContext.Periodicidad.Where(per => per.IdPeriodicidad == idPeriodicidad).FirstOrDefault();
            periodicidadDto = MapeoDatos.Mapper.Map<PeriodicidadDto>(periodicidadResult);
            return periodicidadDto;
        }

        private GranularidadDto ObtenerGranularidad(Guid idGranularidad)
        {
            GranularidadDto granularidadDto;
            var granularidadResult = _baseContext.Granularidad.Where(gra => gra.IdGranularidad == idGranularidad).FirstOrDefault();
            granularidadDto = MapeoDatos.Mapper.Map<GranularidadDto>(granularidadResult);
            return granularidadDto;
        }

        private static void GenerarConsulta(ref IQueryable<GeneracionArchivo> resultadoConsulta, ref int nroRegistros, string ordenarPor, Paginador paginador, bool esPrivado)
        {

            paginador.PageIndex = (paginador.PageIndex == 0) ? 1 : paginador.PageIndex;
            int indicePrimerElemento = (paginador.PageIndex - 1) * paginador.PageSize;
            resultadoConsulta = resultadoConsulta.Include(t => t.TipoVista)
                                                 .Include(c => c.Categoria)
                                                 .Where(d => d.Estado && d.Privacidad == esPrivado);

            nroRegistros = resultadoConsulta.Count();

            resultadoConsulta = resultadoConsulta.Skip(indicePrimerElemento).Take(paginador.PageSize);

            switch (ordenarPor)
            {
                case "contenido":
                    OrdernamientoTipoVista(ref resultadoConsulta);
                    break;

                case "Fecha":
                    OrdernamientoFechaActualizacion(ref resultadoConsulta);
                    break;
                case "vistas":
                    OrdernamientoCantidadVisitas(ref resultadoConsulta);
                    break;
                default:
                    OrdernamientoTipoVista(ref resultadoConsulta);
                    break;
            }
        }


        private static void OrdernamientoTipoVista(ref IQueryable<GeneracionArchivo> resultadoConsulta)
        {

            resultadoConsulta = resultadoConsulta.OrderBy(t => t.TipoVista!.Titulo == "Conjunto de Datos" ? 1 : 9)
                                                        .ThenBy(t => t.TipoVista!.Titulo == "Documentos" ? 2 : 9)
                                                        .ThenBy(t => t.TipoVista!.Titulo == "Enlace externo" ? 3 : 9);
        }

        private static void OrdernamientoFechaActualizacion(ref IQueryable<GeneracionArchivo> resultadoConsulta)
        {

            resultadoConsulta = resultadoConsulta.OrderBy(f => f.FechaModificacion).OrderByDescending(f => f.FechaModificacion);

        }

        private static void OrdernamientoCantidadVisitas(ref IQueryable<GeneracionArchivo> resultadoConsulta)
        {

            resultadoConsulta = resultadoConsulta.OrderBy(v => v.ContadorVistas).OrderByDescending(v => v.ContadorVistas);
        }

        public bool isPrivateDataSet(Guid idDataSet)
        {
            var registro = _baseContext.GeneracionArchivo.Where(x => x.IdConfiguracionGeneracionArchivos == idDataSet).FirstOrDefault();

            return registro != null && registro.Privacidad;
        }

        private string GetExtencionArchivoType(Guid? idTipoVista)
        {
            if (idTipoVista == null) return ".parquet";
            TipoVista? tipoVista = _baseContext.TipoVista.FirstOrDefault(f => f.Id == idTipoVista);
            if (tipoVista == null) return ".parquet";

            return tipoVista.Titulo == "Conjuntos de datos" ? ".parquet" : ".pdf";
        }
    }
}


