using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace Parcial2_apps.Clases
{
    public class ClsUpload
    {
        public string IdPesaje { get; set; }
        public string TipoProceso { get; set; }
        public HttpRequestMessage HttpRequest { get; set; }

        private List<string> nombresArchivos;

        public async Task<HttpResponseMessage> ProcesarCargaArchivo()
        {
            if (!HttpRequest.Content.IsMimeMultipartContent())
            {
                return HttpRequest.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, "No se envió el archivo correctamente.");
            }

            string rutaServidor = HttpContext.Current.Server.MapPath("~/ArchivosPesaje");

            var proveedor = new MultipartFormDataStreamProvider(rutaServidor);

            try
            {
                await HttpRequest.Content.ReadAsMultipartAsync(proveedor);

                if (proveedor.FileData.Count == 0)
                    return HttpRequest.CreateErrorResponse(HttpStatusCode.BadRequest, "No se encontraron archivos.");

                nombresArchivos = new List<string>();

                foreach (var archivoSubido in proveedor.FileData)
                {
                    string nombreOriginal = archivoSubido.Headers.ContentDisposition.FileName?.Trim('"');
                    string nombreArchivo = Path.GetFileName(nombreOriginal);

                    string rutaCompleta = Path.Combine(rutaServidor, nombreArchivo);

                    if (File.Exists(rutaCompleta))
                    {
                        File.Delete(archivoSubido.LocalFileName);
                        return HttpRequest.CreateErrorResponse(HttpStatusCode.Conflict, "El archivo ya existe en el servidor.");
                    }

                    File.Move(archivoSubido.LocalFileName, rutaCompleta);
                    nombresArchivos.Add(nombreArchivo);
                }

                string resultadoBD = RegistrarEnBaseDeDatos();
                return HttpRequest.CreateResponse(HttpStatusCode.OK, "Archivo(s) cargado(s) correctamente. " + resultadoBD);
            }
            catch (Exception ex)
            {
                return HttpRequest.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error interno: " + ex.Message);
            }
        }

        private string RegistrarEnBaseDeDatos()
        {
            switch (TipoProceso.ToUpper())
            {
                case "PESAJE":
                    ClsPesaje gestorPesaje = new ClsPesaje();
                    return gestorPesaje.GuardarImagenesDePesaje(Convert.ToInt32(IdPesaje), nombresArchivos);

                default:
                    return "Tipo de proceso no reconocido.";
            }
        }
    }
}
