using Parcial2_apps.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Parcial2_apps.Clases
{
	public class ClsCamion
	{
        private DBExamenEntities db = new DBExamenEntities();

        public Camion camion { get; set; }

        public string Insertar()
        {
            try
            {
                var existe = db.Camions.Any(c => c.Placa == camion.Placa);
                if (existe)
                {
                    return "El camión ya existe en la base de datos.";
                }

                db.Camions.Add(camion);
                db.SaveChanges();
                return "Camión insertado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al insertar el camión: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                Camion cam = Consultar(camion.Placa);
                if (cam == null)
                {
                    return "El camión no existe.";
                }

                db.Camions.AddOrUpdate(camion);
                db.SaveChanges();
                return "Se actualizó el camión correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al actualizar el camión: " + ex.Message;
            }
        }

        public Camion Consultar(string placa)
        {
            return db.Camions.FirstOrDefault(c => c.Placa == placa);
        }

        public List<Camion> ConsultarTodos()
        {
            return db.Camions.OrderBy(c => c.Placa).ToList();
        }

        public string Eliminar()
        {
            try
            {
                Camion cam = Consultar(camion.Placa);
                if (cam == null)
                {
                    return "El camión no existe.";
                }

                db.Camions.Remove(cam);
                db.SaveChanges();
                return "Camión eliminado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al eliminar el camión: " + ex.Message;
            }
        }

        public string Eliminar(string placa)
        {
            try
            {
                Camion cam = Consultar(placa);
                if (cam == null)
                {
                    return "El camión no existe.";
                }

                db.Camions.Remove(cam);
                db.SaveChanges();
                return "Camión eliminado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al eliminar el camión: " + ex.Message;
            }
        }
    }
}