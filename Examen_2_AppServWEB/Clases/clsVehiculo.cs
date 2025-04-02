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
        private DBExamenEntities dbExamen = new DBExamenEntities();

        Vehiculo vehiculo = new Vehiculo();

        public string Insertar()
        {
            dbExamen.Vehiculoes.Add(vehiculo);
            dbExamen.SaveChanges();
            return $"Se agrego el vehiculo con la siguiente placa {vehiculo.Placa} a la base de datos";
        }

        public IQueryable ConsultarMultas(string placa)
        {
            return from V in dbExamen.Set<Vehiculo>()
                   join I in dbExamen.Set<Infraccion>()
                   on V.Placa equals I.PlacaVehiculo
                   join FI in dbExamen.Set<FotoInfraccion>()
                   on I.idFotoMulta equals FI.idInfraccion
                   where V.Placa == placa
                   select new
                   {
                       Placa = V.Placa,
                       TipoVehiculo = V.TipoVehiculo,
                       Marca = V.Marca,
                       Color = V.Color,
                       IdFotoMulta = I.idFotoMulta,
                       FechaInfraccion = I.FechaInfraccion,
                       TipoInfraccion = I.TipoInfraccion,
                       Imagen = FI.NombreFoto

                   };
        }
    }
}