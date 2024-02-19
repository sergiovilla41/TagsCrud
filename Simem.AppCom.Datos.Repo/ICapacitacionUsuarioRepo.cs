using Simem.AppCom.Datos.Dominio;
using Simem.AppCom.Datos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Repo
{
    interface ICapacitacionUsuarioRepo
    {
        Task<List<Capacitacion>> GetCapacitaciones();
    }
}
