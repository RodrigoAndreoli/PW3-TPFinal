using LasEmpanadas.Models;
using LasEmpanadas.Models.DTO;
using LasEmpanadas.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace LasEmpanadas.Controllers.Api
{
    public class PedidoController : ApiController
    {
        InvitacionPedidoGustoEmpanadaUsuarioService InvitacionPedidoGustoEmpanadaUsuarioService = new InvitacionPedidoGustoEmpanadaUsuarioService();
        InvitacionPedidoService InvitacionPedidoService = new InvitacionPedidoService();
        PedidoService PedidoService = new PedidoService();

        [AcceptVerbs("POST")]
        [HttpPost]
        public JObject ConfirmarGustos(JObject jsonResult)
        {
            ConfirmarcionGustoDTO c = JsonConvert.DeserializeObject<ConfirmarcionGustoDTO>(jsonResult.ToString());
            InvitacionPedido ip = InvitacionPedidoService.FindOneByToken(c.Token);
            String error = "";

            if (ip.Completado)
            {
                return new JObject("{'Resultado':'Error','Mensaje':'El pedido ya se ha completado'}");
            }
            else
            {
                error = PedidoService.ConfirmarGustos(ip, c);
                

                return new JObject("{'Resultado':'Error','Mensaje': " + error);

            }
            Console.WriteLine(c);
        }
    }
}
