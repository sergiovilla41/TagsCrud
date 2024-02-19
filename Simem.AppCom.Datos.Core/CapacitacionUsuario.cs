using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Datos.Repo;
using Simem.AppCom.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simem.AppCom.Datos.Dominio;

namespace Simem.AppCom.Datos.Core
{
    public class CapacitacionUsuario : IBaseCapacitacionUsuario
    {
        private readonly CapacitacionUsuarioRepo repo;
        public CapacitacionUsuario()
        {
            repo ??= new CapacitacionUsuarioRepo();
        }
        public Task<List<Capacitacion>> GetCapacitaciones()
        {
            return repo.GetCapacitaciones();
        }
    }
}
