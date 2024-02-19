using Simem.AppCom.Base.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace Simem.AppCom.Datos.Repo
{
    public class RolConfiguracionGeneracionArchivosRepo : IRolConfiguracionGeneracionArchivosRepo
    {
        readonly DbContextSimem _baseContext;

        public RolConfiguracionGeneracionArchivosRepo()
        {
            _baseContext = new DbContextSimem();
        }

        public bool CanReadDataSet(Guid dataset, string email)
        {
            bool response = false;

            try
            {
                var Usuario = _baseContext.Usuario.Include(ur => ur.UsuarioRoles!).ThenInclude(r => r.Rol!).ThenInclude(rca => rca.RolConfiguracionGeneracionArchivos!).ThenInclude(ga => ga.GeneracionArchivo)
                    .Where(x => x.Correo.ToLower() == email).FirstOrDefault();

                if (Usuario != null &&  Usuario.FechaFin==null  &&Usuario.UsuarioRoles!.Any(x => x.Rol!.RolConfiguracionGeneracionArchivos!.Any()))
                {

                    response = Usuario.UsuarioRoles!.Any(x => x.Rol!.FechaFin == null && x.Rol.RolConfiguracionGeneracionArchivos!.Any(x => x.IdConfiguracionGeneracionArchivos == dataset && x.GeneracionArchivo!.Privacidad));

                }

            }
            catch (Exception)
            {
                //No handled execption
            }



            return response;

        }
    }
}
