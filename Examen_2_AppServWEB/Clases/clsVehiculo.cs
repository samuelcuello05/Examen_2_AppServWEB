using Examen_2_AppServWEB.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Examen_2_AppServWEB.Clases
{
    public class clsVehiculo
    {
        private DBExamenEntities1 dbExamen = new DBExamenEntities1();

        Vehiculo vehiculo = new Vehiculo();

        public string Insertar()
        {
            dbExamen.Vehiculoes.Add(vehiculo);
            dbExamen.SaveChanges();
            return $"Se agrego el vehiculo con la siguiente placa {vehiculo.Placa} a la base de datos";
        }

        public IQueryable ConsultarMultas(string PlacaConsulta)
        {
            return from V in dbExamen.Set<Vehiculo>()
                   join I in dbExamen.Set<Infraccion>()
                   on V.Placa equals I.PlacaVehiculo
                   join FI in dbExamen.Set<FotoInfraccion>()
                   on I.idFotoMulta equals FI.idInfraccion
                   where V.Placa == PlacaConsulta
                   select new
                   {
                       placa = V.Placa,
                       tipoVehiculo = V.TipoVehiculo,
                       marca = V.Marca,
                       color = V.Color,
                       IdFotoMulta = I.idFotoMulta,
                       fechaInfraccion = I.FechaInfraccion,
                       tipoInfraccion = I.TipoInfraccion,
                       Imagen = FI.NombreFoto

                   };
        }
    }
}