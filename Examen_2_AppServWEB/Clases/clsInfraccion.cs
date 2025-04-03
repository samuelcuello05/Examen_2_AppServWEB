using Examen_2_AppServWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net.Http;


namespace Examen_2_AppServWEB.Clases
{
    public class clsInfraccion
    {
        private DBExamenEntities dbExamen = new DBExamenEntities();


    
        public Infraccion infraccion { get; set; }

      

        /*
        public string Agregar([FromBody] Fotomulta fotomulta)
        {
          

            try
            {
                using (var transaction = dbExamen.Database.BeginTransaction())
                {
                    
                    var vehiculoEnDb = dbExamen.Vehiculoes.FirstOrDefault(p => p.Placa == fotomulta.Vehiculo.Placa);
                    if (vehiculoEnDb == null)
                    {
                        dbExamen.Vehiculoes.Add(fotomulta.Vehiculo);
                        dbExamen.SaveChanges();
                    }

                    
                    fotomulta.Infraccion.PlacaVehiculo = fotomulta.Vehiculo.Placa;
                    dbExamen.Infraccions.Add(fotomulta.Infraccion);
                    dbExamen.SaveChanges();

                    transaction.Commit();
                    
                    return  "Fotomulta registrada correctamente.";
                }
            }
            catch (Exception ex)
            {
                return  "Error: " + ex.Message;
            }
        }*/

        public Infraccion ConsultarXId(int IdInfraccion)
        {
            Infraccion infra = dbExamen.Infraccions.FirstOrDefault(i => i.idFotoMulta == IdInfraccion);
            return infra;

        }
    }
}