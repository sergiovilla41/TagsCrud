using Simem.AppCom.Base.Interfaces;
using Simem.AppCom.Datos.Dominio;
using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Datos.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Core
{
    public class Novedad : IBaseNovedad
    {
        private readonly NovedadRepo _novedadRepo;

        public Novedad()
        {
            _novedadRepo ??= new NovedadRepo();
        }

        public async Task<List<NovedadDetail>> GetNovedadesCategoriaNovedades(Paginador paginador, string? term, Guid? category, Guid? IdGeneracionArchivo)
        {
            return await _novedadRepo.GetNovedadesCategoriaNovedades(paginador, term, category, IdGeneracionArchivo);
        }

        public async Task<NovedadDetail> GetNovedadDetail(Guid Id)
        {
            return await _novedadRepo.GetNovedadDetail(Id);
        }

        public async Task<List<CategoriaNovedad>> GetNovedadesCategories()
        {
            return await _novedadRepo.GetNovedadesCategories();
        }

        public async Task<List<GeneracionArchivo>> GetConjuntoDeDatosConNovedades()
        {
            return await _novedadRepo.GetConjuntoDeDatosConNovedades();
        }

        public async Task<int> GetNovedadesCount(Paginador paginador, string? term, Guid? category, Guid? idGeneracionArchivo)
        {
            return await _novedadRepo.GetNovedadesCount(paginador, term, category, idGeneracionArchivo);
        }
    }
}
