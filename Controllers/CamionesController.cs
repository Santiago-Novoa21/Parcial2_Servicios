using Parcial2_apps.Clases;
using Parcial2_apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Parcial2_apps.Controllers
{
    [RoutePrefix("api/Camiones")]
    public class CamionesController : ApiController
    {
        [HttpGet]
        [Route("ConsultarTodos")]
        public List<Camion> ConsultarTodos()
        {
            ClsCamion camion = new ClsCamion();
            return camion.ConsultarTodos();
        }

        [HttpGet]
        [Route("Consultar")]
        public Camion Consultar(string placa)
        {
            ClsCamion camion = new ClsCamion();
            return camion.Consultar(placa);
        }

        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] Camion camion)
        {
            ClsCamion clsCamion = new ClsCamion();
            clsCamion.camion = camion;
            return clsCamion.Insertar();
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] Camion camion)
        {
            ClsCamion clsCamion = new ClsCamion();
            clsCamion.camion = camion;
            return clsCamion.Actualizar();
        }

        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar([FromBody] Camion camion)
        {
            ClsCamion clsCamion = new ClsCamion();
            clsCamion.camion = camion;
            return clsCamion.Eliminar();
        }

        [HttpDelete]
        [Route("EliminarPorPlaca")]
        public string EliminarPorPlaca(string placa)
        {
            ClsCamion clsCamion = new ClsCamion();
            return clsCamion.Eliminar(placa);
        }
    }
}