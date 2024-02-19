using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NUnit.Framework;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Datos.Servicios.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using EnvironmentConfig;
using Microsoft.AspNetCore.Mvc;

namespace Simem.Appcom.Datos.Controller.Test
{
    [TestClass]
    public class TypeViewControllerTest
    {
        private readonly DbContextSimem _baseContext;
        private readonly TypeViewController controller;
        private readonly TipoVistaDto typeViewData = new()
        {
            Estado = true,
            Titulo = "Hola mundo",
        };
        public TypeViewControllerTest()
        {
            Connection.ConfigureConnections();
            controller = new TypeViewController();
            _baseContext ??= new DbContextSimem();
        }
        [TestMethod]
        public async Task Test1SaveTypeView()
        {
            var request = await controller.HttpSaveTypeView(typeViewData).ConfigureAwait(true);
              Assert.IsNotNull(request);
        }

        [TestMethod]
        public async Task Test2FailSaveTypeView()
        {
            var request = await controller.HttpSaveTypeView(new TipoVistaDto() { fail = true }).ConfigureAwait(true);
            Assert.IsNotNull(request);
        }

        [TestMethod]
        public async Task Test3UpdateTypeView()
        {
            var request = await controller.HttpUpdateTypeView(typeViewData).ConfigureAwait(true);
            Assert.IsNotNull(request);
        }

        [TestMethod]
        public async Task Test4GetTypeViews()
        {
            var request = await controller.HttpGetTypeViews().ConfigureAwait(true);
            Assert.IsNotNull(request);
        }

        [TestMethod]
        public async Task Test5DeleteTypeView()
        {
            string? id = _baseContext.TipoVista.Where(w => w.Titulo == "Hola mundo").Select(s => s.Id).FirstOrDefault().ToString();
            var request = await controller.DeleteTypeView(id??"").ConfigureAwait(true);
            Assert.IsNotNull(request);
        }

        [TestMethod]
        public async Task Test6FailDeleteTypeView()
        {
            var request = await controller.DeleteTypeView("").ConfigureAwait(true);
            Assert.IsNotNull(request);
        }

        [TestMethod]
        public async Task Test7GetTypeView()
        {
            var tipoVista = _baseContext.TipoVista.FirstOrDefault();
            if (tipoVista != null)
            {
                var request = await controller.HttpGetTypeView("false",tipoVista.Id.ToString() ?? "").ConfigureAwait(true);
                Assert.IsNotNull(request);
            }
        }

        [TestMethod]
        public async Task Test8FailGetTypeView()
        {
            var tipoVista = _baseContext.TipoVista.FirstOrDefault();
            if (tipoVista != null)
            {
                var request = await controller.HttpGetTypeView("true", "YYYYYYAD").ConfigureAwait(true);
                Assert.IsNotNull(request);
            }
        }

        [TestMethod]
        public async Task Test9GetTypeViewNotContent()
        {
            var tipoVista = _baseContext.TipoVista.FirstOrDefault();
            if (tipoVista != null)
            {
                var request = await controller.HttpGetTypeView("false", "8f71cad0-821e-4f87-ad91-d59ed13845ea").ConfigureAwait(true);
                Assert.IsNotNull(request);
            }
        }

        [TestMethod]
        public async Task TestB10SaveTypeViewBadRequest()
        {
            var request = await controller.HttpSaveTypeView(null!).ConfigureAwait(true);
            var result = (StatusCodeResult)request;
            Assert.AreEqual(500, result.StatusCode);
        }

        [TestMethod]
        public async Task TestB11UpdateBadRequest()
        {
            var request = await controller.HttpUpdateTypeView(null!).ConfigureAwait(true);
            Assert.IsNotNull(request);
        }
    }
}
