using Simem.AppCom.Datos.Dominio;
using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Base.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Simem.AppCom.Datos.Repo
{
    public class PreguntaFrecuenteRepo : IPreguntaFrecuenteRepo
    {
        private readonly DbContextSimem _baseContext;

        public PreguntaFrecuenteRepo()
        {
            _baseContext ??= new DbContextSimem();
        }

        public async Task<List<PreguntaFrecuente>> GetPreguntasFrecuentes()
        {
           
            var dbEntity = await _baseContext.PreguntaFrecuente.ToListAsync();
            var etiquetas = await _baseContext.PreguntaFrecuenteEtiqueta.Include(a => a.Etiqueta).Include(a => a.PreguntaFrecuente).ToListAsync();
           
            for (int i = 0; i < dbEntity.Count; i++)
            {
                foreach (var etiqueta in etiquetas.Select(tag=>tag).Where(tag=>tag.PreguntaFrecuente?.IdPreguntaFrecuente == dbEntity[i].IdPreguntaFrecuente))
                {
                    if (etiqueta != null)
                    {
                        dbEntity[i].Etiquetas.Add(etiqueta.Etiqueta);
                    }
                }
            }
            return dbEntity;
        }

        public PreguntasFrecuentesDto GetPreguntasFrecuentes(int id)
        {
            var dbEntity = _baseContext.PreguntaFrecuente.FirstOrDefault(c => c.IdPreguntaFrecuente.Equals(id));
            PreguntasFrecuentesDto entityDto = new PreguntasFrecuentesDto();
            if (dbEntity != null)
            {
                entityDto.Id = dbEntity.IdPreguntaFrecuente;
                entityDto.Titulo = dbEntity.Titulo;
                entityDto.Estado = dbEntity.Estado;
                entityDto.Descripcion = dbEntity.Descripcion;
            }

            return entityDto;
        }

        public async Task NewPreguntasFrecuentes(PreguntasFrecuentesDto entityDto)
        {
            var transaction = _baseContext.Database.BeginTransaction();

            PreguntaFrecuente dbEntity = new PreguntaFrecuente();
            dbEntity.Estado = true;
            dbEntity.Titulo = entityDto.Titulo;
            dbEntity.Descripcion = entityDto.Descripcion;
            try
            {
                _baseContext.PreguntaFrecuente.Add(dbEntity);
                await _baseContext.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task DeletePreguntasFrecuentes(int id)
        {
            var dbEntity = _baseContext.PreguntaFrecuente.FirstOrDefault(c => c.IdPreguntaFrecuente.Equals(id));
            var transaction = _baseContext.Database.BeginTransaction();
            try
            {
                if (dbEntity != null)
                {
                    _baseContext.PreguntaFrecuente.Remove(dbEntity);
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

        public async Task<bool> ModifyPreguntasFrecuentes(PreguntasFrecuentesDto entityDto)
        {
            var transaction = _baseContext.Database.BeginTransaction();
            bool response = false;
            try
            {
                var dbEntity = _baseContext.PreguntaFrecuente.FirstOrDefault(x => x.IdPreguntaFrecuente.Equals(entityDto.Id));
                if (dbEntity != null)
                {
                    dbEntity.Estado = entityDto.Estado;
                    dbEntity.Titulo = entityDto.Titulo;
                    dbEntity.Descripcion = entityDto.Descripcion;
                    await _baseContext.SaveChangesAsync();
                    transaction.Commit();
                    return true;
                }

            }
            catch (Exception)
            {
                transaction.Rollback();
                return response;
            }

            return response;
        }
    }
}
