using Examen_2_AppServWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examen_2_AppServWEB.Clases
{
    public class clsFotoInfraccion
    {
        private DBExamenEntities dbExamen = new DBExamenEntities();
 
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
    }
}