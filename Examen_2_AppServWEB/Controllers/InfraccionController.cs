using Examen_2_AppServWEB.Clases;
using Examen_2_AppServWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Examen_2_AppServWEB.Controllers
{
    [RoutePrefix("api/Infraccion")]

    public class Fotomulta
    {
        public Infraccion Infraccion { get; set; }
        public Vehiculo Vehiculo { get; set; }
    }

   
    public class InfraccionController : ApiController
    {
        private DBExamenEntities dbExamen = new DBExamenEntities();

        [HttpGet]
        [Route("Consultar")]
        public Infraccion Consultar(int IdInfraccion)
        { 
            clsInfraccion infraccion = new clsInfraccion();
            return infraccion.ConsultarXId(IdInfraccion);
        }

        [HttpPost]
        [Route("Agregar")]
        public string Agregar([FromBody] Fotomulta fotomulta )
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

                    return "Fotomulta registrada correctamente.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

    }
}