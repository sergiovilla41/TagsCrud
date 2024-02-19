using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Dominio;


namespace Simem.AppCom.Datos.Repo
{
    public class TipoVistaRepo : ITipoVista
    {
        private readonly DbContextSimem _baseContext;

        public TipoVistaRepo()
        {
            _baseContext ??= new DbContextSimem();
        }

        public List<TipoVistaDto> GetTypeViews()
        {
            List<Datos.Dto.TipoVistaDto> lstReturn = new();
            var dbEntity = _baseContext.TipoVista.Where(a => a.Estado).ToList();
            foreach (var item in dbEntity)
            {
                TipoVistaDto entityDto = new TipoVistaDto();
                entityDto.Id = item.Id;
                entityDto.Titulo = item.Titulo;
                entityDto.Estado = item.Estado;
                lstReturn.Add(entityDto);
            }
            return lstReturn;
        }

        public TipoVistaDto GetTypeView(Guid idRegistry)
        {
            var dbEntity = _baseContext.TipoVista.FirstOrDefault(c => c.Id.Equals(idRegistry));
            TipoVistaDto entityDto = new TipoVistaDto();
            if (dbEntity == null)
                return entityDto;

            entityDto.Id = dbEntity.Id;
            entityDto.Titulo = dbEntity.Titulo;
            entityDto.Estado = dbEntity.Estado;

            return entityDto;
        }

        public async Task NewTypeView(TipoVistaDto entityDto)
        {
            var transaction = _baseContext.Database.BeginTransaction();

            TipoVista dbEntity = new TipoVista();
            dbEntity.Estado = true;
            dbEntity.Titulo = entityDto.Titulo;
            dbEntity.Id = Guid.NewGuid();
            try
            {
                _baseContext.TipoVista.Add(dbEntity);
                await _baseContext.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task DeleteTypeView(Guid idRegistry)
        {
            var dbEntity = _baseContext.TipoVista.FirstOrDefault(c => c.Id.Equals(idRegistry));
            var transaction = _baseContext.Database.BeginTransaction();
            try
            {
                if (dbEntity != null)
                {
                    _baseContext.TipoVista.Remove(dbEntity);
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

        public async Task<bool> ModifyTypeView(TipoVistaDto entityDto)
        {
            var transaction = _baseContext.Database.BeginTransaction();
            bool response = false;

            try
            {
                var dbEntity = _baseContext.TipoVista.FirstOrDefault(x => x.Id.Equals(entityDto.Id));
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
                return response;
            }

            return response;
        }

    }
}
