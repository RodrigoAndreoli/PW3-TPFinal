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
        public String ConfirmarGustos(JObject jsonResult)
        {
            ConfirmarcionGustoDTO c = JsonConvert.DeserializeObject<ConfirmarcionGustoDTO>(jsonResult.ToString());
            InvitacionPedido ip = InvitacionPedidoService.FindOneByToken(c.Token);
            String message = "";

            if (ip.Completado)
            {

                JSONResponseDTO response = new JSONResponseDTO
                {
                    status = "ERROR",
                    message = "El pedido ya ha sido completado anteriormente."
                };
                return JsonConvert.SerializeObject(response);
            }
            else
            {
                message = PedidoService.ConfirmarGustos(ip, c);
                if (message != "")
                {
                    JSONResponseDTO response = new JSONResponseDTO
                    {
                        status = "ERROR",
                        message = message
                    };
                    return JsonConvert.SerializeObject(response);
                }
                else
                {
                    JSONResponseDTO response = new JSONResponseDTO
                    {
                        status = "OK",
                        message = "Gustos confirmados"
                    };
                    return JsonConvert.SerializeObject(response);
                }
            }
        }
    }
}
