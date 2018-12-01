using LasEmpanadas.Models;
using LasEmpanadas.Models.DTO;
using LasEmpanadas.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LasEmpanadas.Controllers.Api
{
    public class PedidoController : ApiController
    {
        InvitacionPedidoService InvitacionPedidoService = new InvitacionPedidoService();

        [AcceptVerbs("POST")]
        [HttpPost]
        public void ConfirmarGustos(JObject jsonResult)
        {
            ConfirmarcionGustoDTO c = JsonConvert.DeserializeObject<ConfirmarcionGustoDTO>(jsonResult.ToString());
            InvitacionPedido ip = InvitacionPedidoService.FindOneByToken(c.Token);
            if (ip.Completado)
            {
                return;
            }
            Console.WriteLine(c);
        }
    }
}
