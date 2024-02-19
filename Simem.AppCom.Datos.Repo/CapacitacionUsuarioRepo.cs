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
    public class CapacitacionUsuarioRepo : ICapacitacionUsuarioRepo
    {
        private readonly DbContextSimem _baseContext;

        public CapacitacionUsuarioRepo()
        {
            _baseContext ??= new DbContextSimem();
        }

        public async Task<List<Capacitacion>> GetCapacitaciones()
        {
            var etiquetas = await _baseContext.CapacitacionEtiquetas.Include(a => a.Etiqueta).Include(a => a.Capacitacion).ToListAsync();
            
            var capacitaciones = await _baseContext.Capacitacion.OrderBy(a => a.Orden).ToListAsync();

            for (int i = 0; i < capacitaciones.Count; i++)
            {
                foreach (var etiqueta in etiquetas.Select(tag => tag).Where(tag => tag.Capacitacion?.Id == capacitaciones[i].Id))
                {
                    if (etiqueta != null)
                    {
                        capacitaciones[i].Etiquetas!.Add(etiqueta!.Etiqueta!);
                    }
                }
            }
            return capacitaciones;
        }
    }
}
