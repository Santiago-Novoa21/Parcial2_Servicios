using Parcial2_apps.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Parcial2_apps.Clases
{
	public class ClsPesaje
	{

        private DBExamenEntities db = new DBExamenEntities();
        public Pesaje pesaje { get; set; }

        public string Insertar()
        {
            try
            {
                pesaje.FechaPesaje = DateTime.Now;
                db.Pesajes.Add(pesaje);
                db.SaveChanges();
                return "Pesaje registrado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al registrar pesaje: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                Pesaje existente = Consultar(pesaje.id);
                if (existente == null)
                    return "Pesaje no encontrado";

                db.Pesajes.AddOrUpdate(pesaje);
                db.SaveChanges();
                return "Pesaje actualizado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al actualizar pesaje: " + ex.Message;
            }
        }

        public Pesaje Consultar(int id)
        {
            return db.Pesajes.FirstOrDefault(p => p.id == id);
        }

        public List<Pesaje> ConsultarTodos()
        {
            return db.Pesajes.OrderByDescending(p => p.FechaPesaje).ToList();
        }

        public string Eliminar(int id)
        {
            try
            {
                Pesaje p = Consultar(id);
                if (p == null) return "Pesaje no encontrado";

                db.Pesajes.Remove(p);
                db.SaveChanges();
                return "Pesaje eliminado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al eliminar: " + ex.Message;
            }
        }

        public List<Pesaje> ConsultarPorCamion(int idCamion)
        {
            return db.Pesajes
                     .Where(p => p.id == idCamion)
                     .OrderByDescending(p => p.FechaPesaje)
                     .ToList();
        }

        public string GuardarImagenesDePesaje(int idPesaje, List<string> imagenes)
        {
            try
            {
                foreach (string imagen in imagenes)
                {
                    FotoPesaje nuevaFoto = new FotoPesaje();
                    nuevaFoto.idPesaje = idPesaje;
                    nuevaFoto.ImagenVehiculo = imagen;

                    db.FotoPesajes.Add(nuevaFoto);
                    db.SaveChanges();
                }
                return "Se grabó la información en la base de datos.";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public List<object> ConsultarPesajesPorPlaca(string placa)
        {
            var resultado = (from p in db.Pesajes
                             join c in db.Camions on p.PlacaCamion equals c.Placa
                             where c.Placa == placa
                             select new
                             {
                                 Placa = c.Placa,
                                 NumeroEjes = c.NumeroEjes,
                                 Marca = c.Marca,
                                 FechaPesaje = p.FechaPesaje,
                                 PesoObtenido = p.Peso,
                                 Imagenes = db.FotoPesajes
                                              .Where(f => f.idPesaje == p.id)
                                              .Select(f => f.ImagenVehiculo)
                                              .ToList()
                             }).ToList<object>();

            return resultado;
        }

        public void ValidarYRegistrarCamion(string placa, string marca, int numeroEjes)
        {
            var camionExistente = db.Camions.FirstOrDefault(c => c.Placa == placa);

            if (camionExistente == null)
            {
                Camion nuevoCamion = new Camion
                {
                    Placa = placa,
                    Marca = marca,
                    NumeroEjes = numeroEjes
                };

                db.Camions.Add(nuevoCamion);
                db.SaveChanges();
            }
        }

    }
}