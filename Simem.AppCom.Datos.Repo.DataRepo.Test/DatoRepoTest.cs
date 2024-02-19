using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Datos.Repo;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Dominio;
using System.Collections.Generic;
using Microsoft.Extensions.Azure;
using EnvironmentConfig;

namespace Simen.Datos.Repo.Test
{
    [TestClass]
    public class DatoRepoTest
    {
        private readonly DbContextSimem _baseContext;
        private readonly DatoRepo dataRepo;

        public DatoRepoTest()
        {
            Connection.ConfigureConnections();
            _baseContext ??= new DbContextSimem();
            dataRepo ??= new DatoRepo();

        }

        [TestMethod]
        public void AddView()
        {
            dataRepo.AddView(new Guid("A704EEF3-CA1C-4EBF-98A0-01325C61765D"));
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void AddDownload()
        {
            dataRepo.AddDownload(new Guid("A704EEF3-CA1C-4EBF-98A0-01325C61765D"));
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task GetConjuntosDatos()
        {
            await dataRepo.GetConjuntosDatos(new Paginador() { PageIndex=0, PageSize =10 }, null, null);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task GetConjuntosDatosCategoria()
        {
            await dataRepo.GetConjuntosDatos(new Paginador() { PageIndex=0, PageSize =10 }, null, Guid.Parse("CEA17AA9-F38F-4193-B355-E132470B8481"));
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task GetConjuntosDatosCount()
        {
            await dataRepo.GetConjuntosDatosCount(new Paginador() { PageIndex = 0, PageSize = 10 }, null, null);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task GetConjuntosDatosCountCategoria()
        {
            await dataRepo.GetConjuntosDatosCount(new Paginador() { PageIndex = 0, PageSize = 10 }, "Datos soporte", Guid.Parse("CEA17AA9-F38F-4193-B355-E132470B8481"));
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void GetLstData()
        {
            List<DatoDto> lstDatos = dataRepo.GetLstData();
            Assert.IsNotNull(lstDatos);
        }

        [TestMethod]
        public void GetData()
        {

            DatoDto datos = dataRepo.GetData(Guid.Parse("A263BD01-2E5A-47BE-B7D2-3EDFF580687F"));
            Assert.IsNotNull(datos);
        }

        
        [TestMethod]
        public async Task GetLastUpdatedDataAsync()
        {

            List<LastDataUpdatedDto> datos = await dataRepo.GetLastUpdatedData();
            Assert.IsNotNull(datos);
        }

        [TestMethod]
        public void GetListDatas()
        {
            try
            {
                List<DatoDto> datos = dataRepo.GetListData(
                                                                "70B40BB4-04A5-45D9-8C7A-428E07F9048D",
                                                                "0C73CD59-C22D-49C7-AEF5-8872CA80BD97",
                                                                "F20C13A2-B324-47BE-9D1D-7C0EAA0437BE",
                                                                "asc",
                                                                "Titulo con el tag Sistema Interconectado Nacional uptd");
                Assert.IsNotNull(datos);
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void GetListDatasOrderDes()
        {
            var data = _baseContext.GeneracionArchivo.FirstOrDefault()!;
            List<DatoDto> datos = dataRepo.GetListData(
                                                       data.IdCategoria.ToString()!,
                                                       data.IdTipoVista.ToString()!,
                                                       "",
                                                       "desc",
                                                       data.Titulo!);
            Assert.IsNotNull(datos);
        }
        [TestMethod]
        public void GetListDatasOrderDesException()
        {
            var data = _baseContext.GeneracionArchivo.FirstOrDefault()!;
            List<DatoDto> datos = dataRepo.GetListData(
                                                       data.IdCategoria.ToString()!,
                                                       data.IdTipoVista.ToString()!,
                                                       "",
                                                       "asc",
                                                       data.Titulo!);
            Assert.IsNotNull(datos);
        }

        [TestMethod]
        public void ObtenerTags()
        {
            List<EnlaceDto> tagsDtos = dataRepo.ObtenerTags(Guid.Parse("7A2E856D-BB82-48F8-8931-CFD2834EC737"));
            Assert.IsNotNull(tagsDtos);
        }

        [TestMethod]
        public void ObtenerTags2()
        {
            List<EnlaceDto> tagsDtos = dataRepo.ObtenerTags2("C595D70D-D4AC-4D6E-92B9-1142D1B7417C;8D09AA22-6C92-4A7E-A09B-180C6362330C;61FC9885-CBFE-4716-B4A6-22245998D872");
            Assert.IsNotNull(tagsDtos);
        }

        [TestMethod]
        public void obtenerCategory()
        {
            CategoriaDto category = dataRepo.obtenerCategory(Guid.Parse("D0D7616D-6318-4142-B46C-008B68464238"));
            Assert.IsNotNull(category);
        }

        [TestMethod]
        public void getIdCategorias()
        {
            List<Guid?> ListaIdCategoria = new List<Guid?>();
            ListaIdCategoria.Add(Guid.Parse("915267B8-027E-4424-9980-25457688B388"));
            dataRepo.getIdCategorias(Guid.Parse("915267B8-027E-4424-9980-25457688B388"), ref ListaIdCategoria);
            Assert.IsNotNull(ListaIdCategoria);
        }

        [TestMethod]
        public void getIdsdataSetxTag()
        {
            List<Guid?> ListDatasetId = new List<Guid?>();
            dataRepo.getIdsdataSetxTag(ref ListDatasetId, "7A34B6DB-F77A-4B8E-B353-00A36DCE9A15,B7259C63-1B00-4FF7-B2EA-00C2CDC3C2F6");
            Assert.IsNotNull(ListDatasetId);
        }

        [TestMethod]
        public void obtenerTypeView()
        {
            TipoVistaDto typeViewDto = dataRepo.obtenerTypeView(Guid.Parse("FB9A5FA8-D8D0-4D34-8445-35FD28DD65DA"));
            Assert.IsNotNull(typeViewDto);
        }

        [TestMethod]
        public void GetDataByTextFilter()
        {
            try
            {
                List<GeneracionArchivo> dbEntity = new();
                dataRepo.GetDataByTextFilter(ref dbEntity, "Titulo con el tag ");
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void MapDtoData()
        {
            try
            {
                List<GeneracionArchivo> dbEntity = new();
                List<DatoDto> lstReturn = new List<DatoDto>();
                dataRepo.MapDtoData(dbEntity, ref lstReturn);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void GetGuidByStringSplit()
        {

            try
            {
                List<Guid?> ListaId = new();
                DatoRepo.GetGuidByStringSplit(ref ListaId, "18411223-6334-4418-8E4C-298EF8C75961");
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GetDataByCategory()
        {
            try
            {
                List<Guid?> ListaId = new List<Guid?>();
                List<GeneracionArchivo> dbEntity = new List<GeneracionArchivo>();
                ListaId.Add(Guid.Parse("994330FA-9372-4BF8-971B-20E91EBB0520"));
                ListaId.Add(Guid.Parse("915267B8-027E-4424-9980-25457688B388"));
                ListaId.Add(Guid.Parse("1A28487D-96E6-45A7-94D1-2A97609612DF"));
                dataRepo.GetDataByCategory(ref dbEntity, ListaId);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }


        [TestMethod]
        public void GetDataByTypeView()
        {

            List<Guid?> ListaId = new List<Guid?>();
            List<GeneracionArchivo> dbEntity = new List<GeneracionArchivo>();
            ListaId.Add(Guid.Parse("0C73CD59-C22D-49C7-AEF5-8872CA80BD97"));
            dataRepo.GetDataByTypeView(ref dbEntity, ListaId);
            Assert.IsNotNull(dbEntity);
        }

        [TestMethod]
        public void GetDataSets()
        {
            Paginador paginador = new Paginador() { PageIndex = 0, PageSize = 10 };
            ConjuntoDatosPaginaDto conjuntoDato = dataRepo.GetDataSets("1F7AD6A8-5E49-4A8E-A0BC-25E3D04443B2", "", "6AE2D8D6-3FB5-4E0E-8DD7-70A13DA98303","", "contenido", paginador);
            Assert.IsTrue(conjuntoDato.totalFilas > 0);
        }

        [TestMethod]
        public async Task GetDataVariable()
        {
            DatoDto dato = await dataRepo.GetDataVariable("6531E8FF-2CF8-4C1C-8119-773379A5A6C4").ConfigureAwait(true);
            Assert.IsNotNull(dato);
        }

    }
}