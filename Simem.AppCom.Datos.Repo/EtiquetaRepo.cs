using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace Simem.AppCom.Datos.Repo
{
    public class EtiquetaRepo:IEtiquetaRepo
    {
        private readonly DbContextSimem _baseContext;

        public EtiquetaRepo()
        {
            _baseContext = new DbContextSimem();
        }

        public List<EnlaceDto> GetTags()
        {
            List<Datos.Dto.EnlaceDto> lstReturn = new List<EnlaceDto>();
            var dbEntity = _baseContext.Etiqueta.Take(5).ToList();
            foreach (var item in dbEntity)
            {
                EnlaceDto entityDto = new EnlaceDto();
                entityDto.Id = item.Id;
                entityDto.Titulo = item.Titulo;
                entityDto.Estado = item.Estado;
                lstReturn.Add(entityDto);
            }
            return lstReturn;
        }

        public EnlaceDto GetTag(Guid idRegistry)
        {
            var dbEntity = _baseContext.Etiqueta.FirstOrDefault(c => c.Id.Equals(idRegistry));
            EnlaceDto entityDto = new EnlaceDto();
            if (dbEntity != null)
            {
                entityDto.Id = dbEntity.Id;
                entityDto.Titulo = dbEntity.Titulo;
                entityDto.Estado = dbEntity.Estado;
            }

            return entityDto;
        }

        public async Task NewTag(EnlaceDto entityDto)
        {
            var transaction = _baseContext.Database.BeginTransaction();

            Etiqueta dbEntity = new Etiqueta();
            dbEntity.Estado = true;
            dbEntity.Titulo = entityDto.Titulo;
            dbEntity.Id = Guid.NewGuid();
            try
            {
                _baseContext.Etiqueta.Add(dbEntity);
                await _baseContext.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task DeleteTag(Guid idRegistry)
        {
            var dbEntity = _baseContext.Etiqueta.FirstOrDefault(c => c.Id.Equals(idRegistry));
            var transaction = _baseContext.Database.BeginTransaction();
            try
            {
                if (dbEntity != null)
                {
                    _baseContext.Etiqueta.Remove(dbEntity);
                    await _baseContext.SaveChangesAsync();
                    transaction.Commit();
                }

            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<bool> ModifyTag(EnlaceDto entityDto)
        {
            var transaction = _baseContext.Database.BeginTransaction();
            bool response = false;
            var dbEntity = _baseContext.Etiqueta.FirstOrDefault(x => x.Id.Equals(entityDto.Id));

            try
            {
                if (dbEntity != null)
                {
                    dbEntity.Estado = entityDto.Estado;
                    dbEntity.Titulo = entityDto.Titulo;
                    await _baseContext.SaveChangesAsync();
                    transaction.Commit();
                    return true;
                }

            }
            catch (Exception)
            {
                transaction.Rollback();
            }

            return response;
        }
        ///Get all
        public List<ConjuntoDatosDto> GetDatosDto()
        {
            var datos = (from gae in  _baseContext.GeneracionArchivoEtiqueta
                         join e in _baseContext.Etiqueta on gae.EtiquetaId equals e.Id
                         join ga in _baseContext.GeneracionArchivo on gae.IdConfiguracionGeneracionArchivo equals ga.IdConfiguracionGeneracionArchivos
                         select new ConjuntoDatosDto
                         {
                             Id = gae.IdConfiguracionGeneracionArchivoxEtiqueta,
                             Titulo = e.Titulo,
                             Estado = e.Estado,
                             ConjuntoDeDatosAsociados = ga.Titulo
                         }).ToList();

            return datos;
        }
        //Get Id
        public List<ConjuntoDatosDto> GetDatosDtoById(Guid id)
        {
            var datos = (from gae in _baseContext.GeneracionArchivoEtiqueta
                         join e in _baseContext.Etiqueta on gae.EtiquetaId equals e.Id
                         join ga in _baseContext.GeneracionArchivo on gae.IdConfiguracionGeneracionArchivo equals ga.IdConfiguracionGeneracionArchivos
                         where gae.IdConfiguracionGeneracionArchivoxEtiqueta == id
                         select new ConjuntoDatosDto
                         {
                             Id = gae.IdConfiguracionGeneracionArchivoxEtiqueta,
                             Titulo = e.Titulo,
                             Estado = e.Estado,
                             ConjuntoDeDatosAsociados = ga.Titulo
                         }).ToList();

            return datos;
        }
        //Delete

        public void DeleteDatosById(Guid id)
        {
            
            var generacionArchivoEtiquetasAEliminar = _baseContext.GeneracionArchivoEtiqueta
                .Where(gae => gae.IdConfiguracionGeneracionArchivoxEtiqueta == id);
            _baseContext.GeneracionArchivoEtiqueta.RemoveRange(generacionArchivoEtiquetasAEliminar);

           
            var etiquetasAEliminar = _baseContext.Etiqueta
                .Where(e => e.Id == id);
            _baseContext.Etiqueta.RemoveRange(etiquetasAEliminar);

            var generacionArchivosAEliminar = _baseContext.GeneracionArchivo
                .Where(ga => ga.IdConfiguracionGeneracionArchivos == id);
            _baseContext.GeneracionArchivo.RemoveRange(generacionArchivosAEliminar);

           
            _baseContext.SaveChanges();
        }



    }

}
