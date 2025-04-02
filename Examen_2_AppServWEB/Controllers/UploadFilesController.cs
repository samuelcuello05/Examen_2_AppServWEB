using Examen_2_AppServWEB.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Examen_2_AppServWEB.Controllers
{
    [RoutePrefix("api/UploadFiles")]
    public class UploadFilesController : ApiController
    {
        [HttpPost]

        public async Task<HttpResponseMessage> GrabarArchivo(HttpRequestMessage Request, string Datos, string Proceso)
        {
            clsUpload UploadFiles = new clsUpload();
            UploadFiles.request = Request;
            UploadFiles.Datos = Datos;
            UploadFiles.Proceso = Proceso;
            return await UploadFiles.GrabarArchivo(false);
        }

        [HttpGet]
        public HttpResponseMessage Get(string NombreFoto)
        {
            clsUpload upload = new clsUpload();
            return upload.ConsultarArchivo(NombreFoto);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> ActualizarArchivo(HttpRequestMessage Request, string Datos, string Proceso)
        {
            clsUpload UploadFiles = new clsUpload();
            UploadFiles.request = Request;
            UploadFiles.Datos = Datos;
            UploadFiles.Proceso = Proceso;
            return await UploadFiles.GrabarArchivo(true);
        }
    }
}