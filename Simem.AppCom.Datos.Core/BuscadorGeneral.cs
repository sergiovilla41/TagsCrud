using Simem.AppCom.Base.Interfaces;
using Simem.AppCom.Datos.Repo;
using SImem.AppCom.Datos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Core
{
    public class BuscadorGeneral : IBuscadorGeneral
    {
        private readonly BuscadorGeneralRepo buscadorGeneralRepo;
        public BuscadorGeneral()
        {
            buscadorGeneralRepo ??= new BuscadorGeneralRepo();
        }
        public BuscadorGeneralPrcDto BuscarDatos(string filtro,string tipoContenido, string tagsId)
        {
            return buscadorGeneralRepo.BuscarDatos(filtro,tipoContenido, tagsId);
        }

        public BuscadorGeneralConjuntoDatosPrcDto BuscarDatosConjuntoDatos(string categoriaId, string tipoVistaId, string tagsId, string? varsId, string filtro, string orden,bool privado)
        {
            return buscadorGeneralRepo.BuscarDatosConjuntoDatos(categoriaId, tipoVistaId, tagsId, varsId, filtro, orden, privado);
        }

       
        public string BuscarDatosConjuntoDatosPrivados(string email)
        {
            return buscadorGeneralRepo.BuscarDatosConjuntoDatosPrivados(email);
        }
    }
}
