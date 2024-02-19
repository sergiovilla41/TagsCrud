using Azure.Core;
using EnvironmentConfig;
using Microsoft.AspNetCore.Mvc;
using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Datos.Servicios.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.Appcom.Datos.Controller.Test
{
    [TestClass]
    public class ContactControllerTest
    {
        private readonly ContactController contactController;

        private readonly TypeMedioRecibido medioRecibido = new TypeMedioRecibido()
        {
            id = 19,
            title = "Electrónicos",
            numeroVia = "10"
        };

        private readonly ApiDocumentalDataRequestDto parametros = new()
        {
            datosUsuario = new()
            {
                cargo = "prueba",
                correo = "jeisson.quintero@globalmvm.com",
                direccion = "N/A",
                nombreCompleto = "Prueba Pruebas",
                numeroDocumentoIdentidad = "123456789",
                telefono = "6041234598",
                tipoDocumentoIdentidad = "CC"
            },
            asunto = "prueba",
            claseDocumental = new()
            {
                id = 49,
                title = "Comunicación Oficial"
            },
            tipoDocumento = new()
            {
                id = 108,
                title = "Recibida"
            },
            debeResponder = true,
            empresa = new()
            {
                id = 617,
                title = "PERSONA NATURAL"
            },
            dependencia = new()
            {
                id = 40,
                title = "XM Direccion Enlace y Aseguramiento del Mercado",
                codigoDependencia = "1162"
            },
            fechaComunicado = "2023-09-19T08:58:55",
            numeroComunicado = "N/A",
            observaciones = "Prueba API desde SiMEM 19/09/2023",
            pais = new()
            {
                id = 6,
                title = "Colombia"
            },
            radicador = "Prueba Pruebas",
            archivo = "UEsDBAoAAAAAAEBtM1enkmdHTAAAAEwAAAAVAAAAQXN1bnRvLUNvbWVudGFyaW8udHh0QXN1bnRvOiBQcnVlYmEgQVBJIGRlc2RlIFNpTUVNCkNvbWVudGFyaW86IFBydWViYSBBUEkgZGVzZGUgU2lNRU0gMTkvMDkvMjAyM1BLAQIUAAoAAAAAAEBtM1enkmdHTAAAAEwAAAAVAAAAAAAAAAAAAAAAAAAAAABBc3VudG8tQ29tZW50YXJpby50eHRQSwUGAAAAAAEAAQBDAAAAfwAAAAAA",
            fileExtension = ".zip"
        };


        public ContactControllerTest()
        {
            Connection.ConfigureConnections();
            contactController = new();
            parametros.medioRecibo = new List<TypeMedioRecibido>();
            parametros.medioRecibo.Add(medioRecibido);
        }

        [TestMethod]
        public async Task Test1ContactoCodigoEnviarCodigo()
        {
            var result = await contactController.HttpGetContactoCodigo("false", "ruben.munoz@globalmvm.com.co", "json quintero").ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult) || result.GetType() == typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task Test2ContactoCodigoEnviarCodigoBadRequest()
        {
            var request = await contactController.HttpGetContactoCodigo("0", "ruben.munoz@globalmvm.com.co", "json quintero").ConfigureAwait(true);
            var resultadoStatusCode = (ObjectResult)request;
            Assert.AreEqual(400, resultadoStatusCode.StatusCode);
        }

        [TestMethod]
        public void Test3ContactoDatosCrmResultData()
        {
            var request =  contactController.HttpGetContactoDatosCrm("E2023051732", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyIjoiQ0FSTE9TIEVEVUFSRE8gQkFST04gTU9SRU5PIiwiY29kZSI6IkUyMDIzMDUxNzMyIiwibmJmIjoxNjkzMjI1NjIxLCJleHAiOjE3MjQ4NDgwMjEsImlhdCI6MTY5MzIyNTYyMX0.GuVQhAoA-EMHvL1JSuEz6o38xiAEsT77zvdDnmHuIuU");
            var result = (ObjectResult)request;
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public void Test4ContactoDatosCrmResultSinData()
        {
            var request = contactController.HttpGetContactoDatosCrm("", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyIjoiQ0FSTE9TIEVEVUFSRE8gQkFST04gTU9SRU5PIiwiY29kZSI6IkUyMDIzMDUxNzMyIiwibmJmIjoxNjkzMjI1NjIxLCJleHAiOjE3MjQ4NDgwMjEsImlhdCI6MTY5MzIyNTYyMX0.GuVQhAoA-EMHvL1JSuEz6o38xiAEsT77zvdDnmHuIuU");
            var result = (ObjectResult)request;
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public void Test5ContactoDatosCrmResultTokenNoValido()
        {
            var request = contactController.HttpGetContactoDatosCrm("", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyIjoiQ0FSTE9TIEVEVUFSRE8gQkFST04gTU9SRU5PIiwiY29kZSI6IkUyMDIzMDUxNzMxIiwibmJmIjoxNjkzMjI1NzQ5LCJleHAiOjE3MjQ4NDgxNDksImlhdCI6MTY5MzIyNTc0OX0.W1VTogYr5NpgNxW05j7vMoY56OhSaDbXSuCOSUd0G3c");
            var result = (ObjectResult)request;
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public void Test6ContactoDatosCrmResultFailData()
        {
            var request = contactController.HttpGetContactoDatosCrm("", "");
            var result = (ObjectResult)request;
            Assert.AreEqual(400, result.StatusCode);
        }
          
        [TestMethod]
        public async Task Test8tContactoListadoEmpresas()
        {
            var result = await contactController.HttpGetContactoListadoEmpresas().ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task Test8tContactoContactoListadoPaises()
        {
            var result = await contactController.HttpGetContactoListadoPaises().ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task Test9tCContactoListadoSolicitantes()
        {
            var result = await contactController.HttpGetContactoListadoSolicitantes().ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task TestB10ContactoListadoTiposDocumento()
        {
            var result = await contactController.HttpGetContactoListadoTiposDocumento().ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task TestB11ContactoValidarCodigoEnviado()
        {
            
            var result = await contactController.HttpGetContactoValidarCodigo("ruben.munoz@globalmvm.com.co", "PGL9HN").ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task TestB13ContactoListadoTiposSolicitud()
        {
            var result = await contactController.HttpGetContactoListadoTiposSolicitud().ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }
    }
}
