using LasEmpanadas.Models;
using LasEmpanadas.Models.DTO;
using LasEmpanadas.Repositories;
using System;
using System.Collections.Generic;


namespace LasEmpanadas.Services
{
    public class PedidoService
    {
        PedidoRepository PedidoRepo = new PedidoRepository();
        LoginService Loginsvc = new LoginService();
        UsuarioService UsuarioSvc = new UsuarioService();
        InvitacionPedidoService InvitacionPedidoSvc = new InvitacionPedidoService();
        InvitacionPedidoGustoEmpanadaUsuarioService InvitacionPedidoGustoEmpanadaUsuarioSvc = new InvitacionPedidoGustoEmpanadaUsuarioService();
        GustoEmpanadaService GustoEmpanadaSvc = new GustoEmpanadaService();

        /// <summary>
        /// Crea y guarda un nuevo pedido
        /// </summary>
        /// <param name="Order"></param>
        internal Pedido CreateOrder(Pedido Order)
        {
            //Placeholder, no tenemos sesion para levantar el idUsuario.
            Order.IdUsuarioResponsable = Loginsvc.GetLoggedUserId();
            //Inicializa el pedido en estado ABIERTO.
            Order.IdEstadoPedido = 1;
            Order.FechaCreacion = DateTime.Now;
            Order.FechaModificacion = null;
            
            Pedido CreatedOrder = PedidoRepo.Create(Order);

            //foreach (int idGustoEmpanada in Order.GustoEmpanadaDisponibles)
            //{
            //    CreatedOrder.GustoEmpanada.Add(GustoEmpanadaSvc.FindById(idGustoEmpanada));
            //}

            //CreatedOrder = PedidoRepo.Attach(CreatedOrder);

            //Chequeo la lista de emails.Si no existe, creo un usuario nuevo.
            UsuarioSvc.CheckEmailList(Order.EmailsInvitados);
            //Creo un nuevo registro en la tabla InvitacionPedido.
            InvitacionPedidoSvc.Create(Order);
            ///InvitacionPedidoGustoEmpanadaUsuarioSvc.Create(Order);
            return CreatedOrder;
        }

        internal Pedido FindOneById(int? IdPedido)
        {
           return PedidoRepo.FindOneById(IdPedido);
        }

        internal void Edit(Pedido p)
        {
            PedidoRepo.Update(p);
        }

        internal void DeleteOrder(Pedido Order)
        {
            InvitacionPedidoGustoEmpanadaUsuarioSvc.DeleteByOrder(Order);
            InvitacionPedidoSvc.DeleteByOrder(Order);
            PedidoRepo.Delete(Order);
        }

        internal Pedido BuildPedido(PedidoCompletoDTO Pedido)
        {
            Pedido p = new Pedido
            {
                Descripcion = Pedido.Descripcion,
                FechaCreacion = Pedido.FechaCreacion,
                FechaModificacion = Pedido.FechaModificacion,
                IdEstadoPedido = Pedido.IdEstadoPedido,
                IdPedido = Pedido.IdPedido,
                IdUsuarioResponsable = Pedido.IdUsuarioResponsable,
                NombreNegocio = Pedido.NombreNegocio,
                PrecioDocena = Pedido.PrecioDocena,
                PrecioUnidad = Pedido.PrecioUnidad,
            };
            return p;
        }

        internal List<Pedido> GetList()
        {
            List<Pedido> OrderList = PedidoRepo.GetAll();

            return OrderList;
        }

        internal Pedido GetPedidoById(int idPedido)
        {
            return PedidoRepo.FindOneById(idPedido);
        }

        internal List<GustoEmpanada> GetGustosDisponibles(int idPedido)
        {
            return PedidoRepo.GetGustoEmpanadasDisponibles(idPedido);
        }

        internal List<Pedido> FindPedidosByUser(int? IdUser)
        {
            return PedidoRepo.FindPedidosByUser(IdUser);
            }

        internal List<Pedido> FindAll()
        {
            return PedidoRepo.GetAll();
        }

        public PedidoCompletoDTO ObtenerPedidoCompleto(int? idPedido)
        {
            Pedido Pedido = PedidoRepo.FindOneById(idPedido);
            List<InvitacionPedidoGustoEmpanadaUsuario> invitacionPedidoGustos = InvitacionPedidoGustoEmpanadaUsuarioSvc.FindAllByPedido(idPedido);
            List<GustoEmpanada> Gustos = new List<GustoEmpanada>();
            List<InvitacionPedido> invitaciones = InvitacionPedidoSvc.FindOneByPedidoId(idPedido);

            foreach (InvitacionPedidoGustoEmpanadaUsuario i in invitacionPedidoGustos)
            {
                if (!Gustos.Contains(i.GustoEmpanada))
                    Gustos.Add(i.GustoEmpanada);
            }

            List<Usuario> Usuarios = new List<Usuario>();
            foreach (InvitacionPedidoGustoEmpanadaUsuario i in invitacionPedidoGustos)
            {
                if (!Usuarios.Contains(i.Usuario))
                    Usuarios.Add(i.Usuario);
            }

            PedidoCompletoDTO PedidoCompleto = new PedidoCompletoDTO
            {
                Descripcion = Pedido.Descripcion,
                FechaCreacion = Pedido.FechaCreacion,
                FechaModificacion = Pedido.FechaModificacion,
                gustoEmpanadas = Gustos,
                IdEstadoPedido = Pedido.IdEstadoPedido,
                IdPedido = Pedido.IdPedido,
                IdUsuarioResponsable = Pedido.IdUsuarioResponsable,
                NombreNegocio = Pedido.NombreNegocio,
                PrecioDocena = Pedido.PrecioDocena,
                PrecioUnidad = Pedido.PrecioUnidad,
                usuarios = Usuarios,
                CantidadEmpanadasPorGustosYUsuarios = invitacionPedidoGustos
            };
            return PedidoCompleto;
        }
    }

}

