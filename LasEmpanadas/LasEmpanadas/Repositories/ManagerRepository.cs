using LasEmpanadas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LasEmpanadas.Repositories
{
    public class ManagerRepository
    {
        MasterEntities Db = new MasterEntities();

        public ManagerRepository()
        {
            GustoEmpanadaRepository = new GustoEmpanadaRepository(Db);
            InvitacionPedidoGustoEmpanadaUsuarioRepository = new InvitacionPedidoGustoEmpanadaUsuarioRepository(Db);
            InvitacionPedidoRepository = new InvitacionPedidoRepository(Db);
            PedidoRepository = new PedidoRepository(Db);
            UsuarioRepository = new UsuarioRepository(Db);
        }
        public GustoEmpanadaRepository GustoEmpanadaRepository { get; set; }
        public InvitacionPedidoGustoEmpanadaUsuarioRepository InvitacionPedidoGustoEmpanadaUsuarioRepository { get; set; }
        public InvitacionPedidoRepository InvitacionPedidoRepository { get; set; }
        public PedidoRepository PedidoRepository { get; set; }
        public UsuarioRepository UsuarioRepository { get; set; }
    }
}