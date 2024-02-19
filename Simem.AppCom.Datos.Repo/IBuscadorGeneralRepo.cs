using SImem.AppCom.Datos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Repo
{
    public interface IBuscadorGeneralRepo
    {
        public BuscadorGeneralPrcDto BuscarDatos(string filtro, string tipoContenido, string tagsId);
        public BuscadorGeneralConjuntoDatosPrcDto BuscarDatosConjuntoDatos(string categoriaId, string tipoVistaId, string tagsId, string varsId, string filtro, string orden, bool privado);
        public string BuscarDatosConjuntoDatosPrivados(string email);

    }
}
