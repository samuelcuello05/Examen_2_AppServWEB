using Examen_2_AppServWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examen_2_AppServWEB.Clases
{
    public class clsInfraccion
    {
        private DBExamenEntities dbExamen = new DBExamenEntities();
        
        public Infraccion infraccion { get; set; }

    
        public string Agregar(Vehiculo vehiculo)
        {
           
            try
            {
                Vehiculo veh = dbExamen.Vehiculoes.FirstOrDefault(p => p.Placa == vehiculo.Placa);

                if (veh!=null)
                {
                    dbExamen.Infraccions.Add(infraccion);
                    dbExamen.SaveChanges();
                    return $"Se agrego la infraccion al vehiculo con la siguiente placa {infraccion.PlacaVehiculo} a la base de datos";
                }
                else
                {
                    clsVehiculo clsvehiculo = new clsVehiculo();
                    clsvehiculo.Insertar();
                    dbExamen.Infraccions.Add(infraccion);
                    dbExamen.SaveChanges();
                    return $"Se agrego primero el vehiculo con la placa {vehiculo.Placa}, porque este no se encontraba en el sistema y tambien se agrego la infraccion a este vehiculo";
                }
            }
            catch (Exception ex)
            {
                return "Error agregando la infraccion " + ex.Message; //Mensaje de error
            }
        }

        public Infraccion ConsultarXId(int IdInfraccion)
        {
            Infraccion infra = dbExamen.Infraccions.FirstOrDefault(i => i.idFotoMulta == IdInfraccion);
            return infra;

        }
    }
}