using Examen_2_AppServWEB.Clases;
using Examen_2_AppServWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Examen_2_AppServWEB.Controllers
{
    

    public class Fotomulta
    {
        public Infraccion Infraccion { get; set; }
        public Vehiculo Vehiculo { get; set; }
    }

    [RoutePrefix("api/Infraccion")]
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
        public string Agregar([FromBody] Fotomulta fotomulta)
        {
            clsInfraccion infraccioncls = new clsInfraccion();
            return infraccioncls.Agregar(fotomulta.Infraccion, fotomulta.Vehiculo);
            
        }

    }
}