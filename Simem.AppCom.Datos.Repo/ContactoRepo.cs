using Microsoft.EntityFrameworkCore;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Dominio;
using Simem.AppCom.Datos.Dto;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Repo
{
    public class ContactoRepo : IContactoRepo
    {
        private readonly DbContextSimem _baseContext;

        public ContactoRepo()
        {
            _baseContext ??= new DbContextSimem();
        }

        public async Task InsertContactRequest(ContactoDto dto)
        {
            var transaction = _baseContext.Database.BeginTransaction();

            Contacto entity = new Contacto();
            entity.Id = Guid.NewGuid();
            entity.ConsecutivoCRM = dto.ConsecutivoCRM;
            entity.FechaInicio = dto.FechaInicio;
            entity.FechaFin = dto.FechaFin;
            entity.FechaCreacion = GetCurrentDateColombia();

            try
            {
                _baseContext.Contacto.Add(entity);
                await _baseContext.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task DeleteContactRequest(Guid id)
        {
            var entity = _baseContext.Contacto.FirstOrDefault(c => c.Id.Equals(id));
            var transaction = _baseContext.Database.BeginTransaction();
            try
            {
                if (entity != null)
                {
                    _baseContext.Contacto.Remove(entity);
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

        public async Task<ContactoCodigo> GetEmailRequest(string email)
        {
            ContactoCodigo code = new();

            try
            {
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                code = await _baseContext.ContactoCodigo.Where(x => x.CorreoUsuario == email).FirstOrDefaultAsync();
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
            }
            catch (Exception)
            {
                //Entidad vacía
            }

            return code!;
        }

        [ExcludeFromCodeCoverage]
        public async Task InsertCodeRequest(string email, string code)
        {
            var transaction = _baseContext.Database.BeginTransaction();

            ContactoCodigo entity = new ContactoCodigo();
            entity.Id = Guid.NewGuid();
            entity.CorreoUsuario = email;
            entity.Codigo = code;
            entity.FechaCreacion = GetCurrentDateColombia();

            try
            {
                _baseContext.ContactoCodigo.Add(entity);
                await _baseContext.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task EditCodeRequest(ContactoCodigo entidad, string codigo)
        {
            var transaction = _baseContext.Database.BeginTransaction();

            entidad.Codigo = codigo;
            entidad.FechaCreacion = GetCurrentDateColombia();

            try
            {
                _baseContext.ContactoCodigo.Update(entidad);
                await _baseContext.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        [ExcludeFromCodeCoverage]
        private static DateTime GetCurrentDateColombia()
        {
            return DateTime.Now.ToUniversalTime().AddHours(-5.0);
        }

        public List<ContactoEmpresa> GetCompanyList()
        {
            List<ContactoEmpresa> lista = new();

            try
            {
                lista = _baseContext.ContactoEmpresa.ToList();
            }
            catch (Exception)
            {
                //Lista vacía
            }

            return lista;
        }

        public List<ContactoPais> GetCountryList() 
        {
            List<ContactoPais> lista = new();

            try
            {
                lista = _baseContext.ContactoPais.ToList();
            }
            catch (Exception)
            {
                //Lista vacía
            }

            return lista;
        }

        public List<ContactoSolicitante> GetApplicantList() 
        {
            List<ContactoSolicitante> lista = new();

            try
            {
                lista = _baseContext.ContactoSolicitante.ToList();
            }
            catch (Exception)
            {
                //Lista vacía
            }

            return lista;
        }

        public List<ContactoTipoDocumento> GetDocumentTypesList() 
        {
            List<ContactoTipoDocumento> lista = new();

            try
            {
                lista = _baseContext.ContactoTipoDocumento.ToList();
            }
            catch (Exception)
            {
                //Lista vacía
            }

            return lista;
        }

        public List<ContactoTipoSolicitud> GetRequestTypesList() 
        {
            List<ContactoTipoSolicitud> lista = new();

            try
            {
                lista = _baseContext.ContactoTipoSolicitud.ToList();
            }
            catch (Exception)
            {
                //Lista vacía
            }

            return lista;
        }

        public ContactoConfiguracionAdjunto GetAttachmentConfig(string name)
        {
            ContactoConfiguracionAdjunto entity = new();

            try
            {
                entity = _baseContext.ContactoConfiguracionAdjunto.FirstOrDefault(x => x.NombreConfiguracion == name)!;
            }
            catch (Exception)
            {
                //Entity vacío
            }

            return entity;
        }
    }
}
