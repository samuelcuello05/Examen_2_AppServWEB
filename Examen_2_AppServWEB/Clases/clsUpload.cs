using Examen_2_AppServWEB.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Examen_2_AppServWEB.Clases
{
    public class clsUpload
    {
        public HttpRequestMessage Request {  get; set; }

        public string Datos { get; set; }

        public string Proceso { get; set; }


        public async Task<HttpResponseMessage> GrabarArchivo(bool Actualizar)
        {
            string RptaError = "";
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/Archivos");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                List<string> Archivos = new List<string>();
                foreach (MultipartFileData file in provider.FileData)
                {
                    string filename = file.Headers.ContentDisposition.FileName;
                    if (filename.StartsWith("\"") && filename.EndsWith("\""))
                    {
                        filename = filename.Trim('"');
                    }
                    if (filename.Contains(@"/") || filename.Contains(@"\"))
                    {
                        filename = Path.GetFileName(filename);
                    }
                    if (File.Exists(Path.Combine(root, filename)))
                    {
                        if (Actualizar)
                        {
                            //Se borra el original
                            File.Delete(Path.Combine(root, filename));
                            //Se crea el nuevo archivo con el mismo nombre
                            File.Move(file.LocalFileName, Path.Combine(root, filename));
                            //No se debe agregar en la base de datos, porque ya existe
                        }
                        else
                        {
                            //No se pueden tener archivos con el mismo nombre, es decir las imagenes tienen que tener nombres unicos
                            //Si el archivo existe, se borra el archivo temporal que se subio
                            File.Delete(file.LocalFileName);
                            //Se da una respuesta de error
                            RptaError += $"El archivo: {filename} ya existe";//"El archivo: " + filename + " ya existe";
                                                                             //return request.CreateErrorResponse(HttpStatusCode.Conflict, "El archivo ya existe");
                        }
                    }
                    else
                    {
                        Archivos.Add(filename);

                        //Se renombra el archivo
                        File.Move(file.LocalFileName, Path.Combine(root, filename));
                    }

                }
                if (Archivos.Count > 0)
                {
                    //Envia a grabar la informacion de las imagenes
                    string Respuesta = ProcesarArchivos(Archivos);
                    //Se da una respuesta de exito
                    return Request.CreateResponse(HttpStatusCode.OK, "Archivo subido con éxito");
                }
                else
                {
                    if (Actualizar)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Archivo actualizado con éxito");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.Conflict, "El(los) archivo(s) ya existe(n)");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error en cargar el archivo " + ex.Message);
            }
        }


        public HttpResponseMessage ConsultarArchivo(string NombreFoto)
        {
            try
            {
                string Ruta = HttpContext.Current.Server.MapPath("~/Archivos");
                string Archivo = Path.Combine(Ruta, NombreFoto);

                if (File.Exists(Archivo))
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    var stream = new FileStream(Archivo, FileMode.Open, FileAccess.Read);
                    response.Content = new StreamContent(stream);
                    response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = NombreFoto;
                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                    return response;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Archivo  no encontrado");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error al consultar el archivo " + ex.Message);
            }
        }

        private string ProcesarArchivos(List<string> Archivos)
        {
            switch (Proceso.ToUpper())
            {
                case "INFRACCION":
                    clsFotoInfraccion FotosInfraccion = new clsFotoInfraccion();
                    FotosInfraccion.idInfraccion = Datos;//Debe venir la información que se procesa en la base de datos, para nuestro caso, el código de la foto
                    FotosInfraccion.Archivos = Archivos;
                    return FotosInfraccion.GrabarImagenes();
                default:
                    return "Proceso no válido";
            }
        }


        public HttpResponseMessage Eliminar(string Nombrefoto)
        {
            try
            {
                string Ruta = HttpContext.Current.Server.MapPath("~/Archivos");
                string Archivo = Path.Combine(Ruta, Nombrefoto);

                if (File.Exists(Archivo))
                {
                    
                    File.Delete(Archivo);
                    return Request.CreateResponse(HttpStatusCode.OK, $"El archivo  fue eliminado correctamente.");
                   
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Archivo no se encontro");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error al consultar el archivo " + ex.Message);
            }

        }

    }
}