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
using Examen_2_AppServWEB.Controllers;


namespace Examen_2_AppServWEB.Clases
{
    public class clsInfraccion
    {
        private DBExamenEntities1 dbExamen = new DBExamenEntities1();

        /*
        public class Fotomulta
        {
            public Infraccion Infraccion { get; set; }
            public Vehiculo Vehiculo { get; set; }
        }*/

        public Infraccion infraccion { get; set; }

        public Vehiculo vehiculo { get; set; }



        public string Agregar(Infraccion infraccionBD, Vehiculo vehiculoBD)
        {
            try
            {
                using (var transaction = dbExamen.Database.BeginTransaction())
                {

                    var vehiculos = dbExamen.Vehiculoes.FirstOrDefault(p => p.Placa == vehiculoBD.Placa);
                    if (vehiculos == null && infraccionBD.PlacaVehiculo == vehiculoBD.Placa)
                    {
                        dbExamen.Vehiculoes.Add(vehiculoBD);
                        dbExamen.SaveChanges();
                        infraccionBD.PlacaVehiculo = vehiculoBD.Placa;
                        dbExamen.Infraccions.Add(infraccionBD);
                        dbExamen.SaveChanges();
                    }
                    else
                    {
                        return "Error, la placa del vehiculo no coincide con la placa de la fotomulta.";
                    }

                        transaction.Commit();

                    return "Fotomulta registrada correctamente.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public Infraccion ConsultarXId(int IdInfraccion)
        {
            Infraccion infra = dbExamen.Infraccions.FirstOrDefault(i => i.idFotoMulta == IdInfraccion);
            return infra;

        }
    }
}