
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
    [RoutePrefix("api/Pesaje")]
    public class PesajeController : ApiController
    {
        [HttpGet]
        [Route("ConsultarTodos")]
        public List<Pesaje> ConsultarTodos()
        {
            ClsPesaje cls = new ClsPesaje();
            return cls.ConsultarTodos();
        }

        [HttpGet]
        [Route("Consultar")]
        public Pesaje Consultar(int id)
        {
            ClsPesaje cls = new ClsPesaje();
            return cls.Consultar(id);
        }

        [HttpGet]
        [Route("ConsultarPorCamion")]
        public List<Pesaje> ConsultarPorCamion(int idCamion)
        {
            ClsPesaje cls = new ClsPesaje();
            return cls.ConsultarPorCamion(idCamion);
        }

        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] Pesaje pesaje)
        {
            ClsPesaje cls = new ClsPesaje();
            cls.pesaje = pesaje;
            return cls.Insertar();
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] Pesaje pesaje)
        {
            ClsPesaje cls = new ClsPesaje();
            cls.pesaje = pesaje;
            return cls.Actualizar();
        }

        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar(int id)
        {
            ClsPesaje cls = new ClsPesaje();
            return cls.Eliminar(id);
        }

        [HttpGet]
        [Route("ConsultarPorPlaca")]
        public List<object> ConsultarPorPlaca(string placa)
        {
            ClsPesaje cls = new ClsPesaje();
            return cls.ConsultarPesajesPorPlaca(placa);
        }

    }
}