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
    public class InfraccionController : ApiController
    {
        [HttpGet]
        [Route("Consultar")]
        public Infraccion Consultar(int IdInfraccion)
        { 
            clsInfraccion infraccion = new clsInfraccion();
            return infraccion.ConsultarXId(IdInfraccion);
        }

        [HttpPost]
        [Route("Agregar")]
        public string Agregar([FromBody] Infraccion infraccion )
        {
            clsInfraccion infraccioncls = new clsInfraccion();
            infraccioncls.infraccion = infraccion;
            return infraccioncls.Agregar();
        }

    }
}