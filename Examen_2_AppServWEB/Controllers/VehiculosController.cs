using Examen_2_AppServWEB.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Examen_2_AppServWEB.Controllers
{
    [RoutePrefix("api/Vehiculos")]
    public class VehiculosController : ApiController
    {
        [HttpGet]
        [Route("ConsultaMultas")]
       
        public IQueryable ConsultaMultas(string placa)
        {
            clsVehiculo carro = new clsVehiculo();
            return carro.ConsultarMultas(placa);   
        }


    }
}