using Parcial2_apps.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Parcial2_apps.Controllers
{
    [RoutePrefix("api/UploadFiles")]
    public class UploadFilesController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> CargarArchivo(HttpRequestMessage Request, string IdPesaje, string TipoProceso)
        {
            ClsUpload upload = new ClsUpload();
            upload.IdPesaje = IdPesaje;
            upload.TipoProceso = TipoProceso;
            upload.HttpRequest = Request;
            return await upload.ProcesarCargaArchivo();
        }
    }
}