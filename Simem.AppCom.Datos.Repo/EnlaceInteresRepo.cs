using Microsoft.EntityFrameworkCore;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Dominio;
using Simem.AppCom.Datos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Repo
{
    public class EnlaceInteresRepo : IEnlaceInteresRepo
    {
        private readonly DbContextSimem _baseContext;

        public EnlaceInteresRepo()
        {
            _baseContext ??= new DbContextSimem();
        }

        public async Task<List<EnlaceInteres>> GetEnlaceInteres()
        {
            var dbEntity = await _baseContext.EnlaceInteres.ToListAsync();
            var etiquetas = await _baseContext.EnlaceInteresEtiqueta.Include(a => a.Etiqueta).Include(a => a.EnlaceInteres).ToListAsync();

            for (int i = 0; i < dbEntity.Count; i++)
            {
                foreach (var etiqueta in etiquetas.Select(tag => tag).Where(tag => tag.EnlaceInteres?.IdEnlaceInteres == dbEntity[i].IdEnlaceInteres))
                {
                    if (etiqueta != null)
                    {
                        dbEntity[i].Etiquetas!.Add(etiqueta!.Etiqueta!);
                    }
                }
            }

            return dbEntity;
        }

        public async Task<List<EnlaceInteresDto>> GetEnlaceInteres(Paginador paginador)
        {
            List<EnlaceInteresDto> lstReturn = new List<EnlaceInteresDto>();
            var dbEntity = await _baseContext
                .EnlaceInteres
                .Skip(paginador.PageSize*paginador.PageIndex)
                .Take(paginador.PageSize)
                .ToListAsync();

            foreach (var item in dbEntity)
            {
                EnlaceInteresDto entityDto = new EnlaceInteresDto();
                entityDto.Id = item.IdEnlaceInteres;
                entityDto.Titulo = item.Titulo;
                entityDto.Estado = item.Estado;
                entityDto.Descripcion = item.Descripcion;
                entityDto.Enlace = item.Enlace != null ? item.Enlace : "";
                entityDto.Icono = item.Icono != null ? item.Icono : "";

                lstReturn.Add(entityDto);
            }
            return lstReturn;
        }

        public EnlaceInteresDto GetEnlaceInteres(int id)
        {
            var dbEntity = _baseContext.EnlaceInteres.FirstOrDefault(c => c.IdEnlaceInteres.Equals(id));
            EnlaceInteresDto entityDto = new EnlaceInteresDto();
            if (dbEntity != null)
            {
                entityDto.Id = dbEntity.IdEnlaceInteres;
                entityDto.Titulo = dbEntity.Titulo;
                entityDto.Estado = dbEntity.Estado;
                entityDto.Descripcion = dbEntity.Descripcion;
                entityDto.Enlace = dbEntity.Enlace != null ? dbEntity.Enlace : "";
                entityDto.Icono = dbEntity.Icono != null ? dbEntity.Icono : "";
            }

            return entityDto;
        }

        public async Task<int> GetEnlaceInteresCount()
        {
            return await _baseContext.EnlaceInteres.CountAsync();
        }

        public async Task NewEnlaceInteres(EnlaceInteresDto entityDto)
        {
            var transaction = _baseContext.Database.BeginTransaction();

            EnlaceInteres dbEntity = new EnlaceInteres();
            dbEntity.Estado = true;
            dbEntity.Titulo = entityDto.Titulo;
            dbEntity.Descripcion = entityDto.Descripcion;
            dbEntity.Enlace = entityDto.Enlace;
            dbEntity.Icono = entityDto.Icono;
            try
            {
                _baseContext.EnlaceInteres.Add(dbEntity);
                await _baseContext.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task DeleteEnlaceInteres(int id)
        {
            var dbEntity = _baseContext.EnlaceInteres.FirstOrDefault(c => c.IdEnlaceInteres.Equals(id));
            var transaction = _baseContext.Database.BeginTransaction();
            try
            {
                if (dbEntity != null)
                {
                    _baseContext.EnlaceInteres.Remove(dbEntity);
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

        public async Task<bool> ModifyEnlaceInteres(EnlaceInteresDto entityDto)
        {
            var transaction = _baseContext.Database.BeginTransaction();
            bool response = false;
            var dbEntity = _baseContext.EnlaceInteres.FirstOrDefault(x => x.IdEnlaceInteres.Equals(entityDto.Id));

            try
            {
                if (dbEntity != null)
                {
                    dbEntity.Estado = entityDto.Estado;
                    dbEntity.Titulo = entityDto.Titulo;
                    dbEntity.Descripcion = entityDto.Descripcion;
                    dbEntity.Enlace = entityDto.Enlace;
                    dbEntity.Icono = entityDto.Icono;

                    await _baseContext.SaveChangesAsync();
                    transaction.Commit();
                    return true;
                }

            }
            catch (Exception)
            {
                transaction.Rollback();
            }

            transaction.Commit();
            return response;
        }
    }
}
