using Examen_2_AppServWEB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Examen_2_AppServWEB.Clases
{
    public class clsFotoInfraccion
    {
        private DBExamenEntities1 dbExamen = new DBExamenEntities1();
 
        public string idInfraccion { get; set; }
        public List<string> Archivos { get; set; }

        public string GrabarImagenes()
        {
            try
            {

                if (Archivos.Count > 0)
                {
                    foreach (var Archivo in Archivos)
                    {
                        FotoInfraccion foto = new FotoInfraccion();
                        foto.idInfraccion = Convert.ToInt32(idInfraccion);
                        foto.NombreFoto = Archivo;
                        dbExamen.FotoInfraccions.Add(foto);
                        dbExamen.SaveChanges();


                    }
                    return "Archivo guardado correctamente";
                }
                else
                {
                    return "No se enviaron archivos para guardar";
                }
            }
            catch (Exception ex)
            {
                return "Error grabando la imagen " + ex.Message;
            }
        }


        public string EliminarImagenInfraccion(string nombreImagen)
        {
            try
            {
                FotoInfraccion foto = dbExamen.FotoInfraccions.FirstOrDefault(f => f.NombreFoto == nombreImagen);
                if (foto != null)
                {
                    dbExamen.FotoInfraccions.Remove(foto);
                    dbExamen.SaveChanges();
                    return "Foto eliminada de la base de datos.";
                }
                else
                {
                    return "No se encontró la imagen en la base de datos.";
                }
            }
            catch (Exception ex)
            {
                return "Error al eliminar la imagen de la base de datos: " + ex.Message;
            }
        }
    }
}