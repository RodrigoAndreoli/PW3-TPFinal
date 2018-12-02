using LasEmpanadas.Models;
using LasEmpanadas.Models.DTO;
using LasEmpanadas.Repositories;
using System;
using System.Collections.Generic;


namespace LasEmpanadas.Services
{
    public class PedidoService
    {
        PedidoRepository PedidoRepo;
        MasterEntities Db;
        LoginService Loginsvc;
        UsuarioService UsuarioSvc;
        InvitacionPedidoService InvitacionPedidoSvc;
        InvitacionPedidoGustoEmpanadaUsuarioService InvitacionPedidoGustoEmpanadaUsuarioSvc;
        GustoEmpanadaService GustoEmpanadaSvc;
        EmailService EmailService = new EmailService();


        public PedidoService()
        {
            Db = new MasterEntities();
            PedidoRepo = new PedidoRepository(Db);
            Loginsvc = new LoginService(Db);
            UsuarioSvc = new UsuarioService(Db);
            InvitacionPedidoSvc = new InvitacionPedidoService(Db);
            InvitacionPedidoGustoEmpanadaUsuarioSvc = new InvitacionPedidoGustoEmpanadaUsuarioService(Db);
            GustoEmpanadaSvc = new GustoEmpanadaService(Db);
        }
        public PedidoService(MasterEntities db)
        {
            Db = db;
            PedidoRepo = new PedidoRepository(Db);
            Loginsvc = new LoginService(Db);
            UsuarioSvc = new UsuarioService(Db);
            InvitacionPedidoSvc = new InvitacionPedidoService(Db);
            InvitacionPedidoGustoEmpanadaUsuarioSvc = new InvitacionPedidoGustoEmpanadaUsuarioService(Db);
            GustoEmpanadaSvc = new GustoEmpanadaService(Db);
        }

        internal string ConfirmarGustos(InvitacionPedido ip, ConfirmarcionGustoDTO c)
        {

            Pedido p = FindOneById(ip.IdPedido);


            List<InvitacionPedidoGustoEmpanadaUsuario> i = InvitacionPedidoGustoEmpanadaUsuarioSvc.FindAllByPedido(p.IdPedido);

            String mensaje = "";
            foreach (GustosEmpanadasCantidad g in c.GustosEmpanadasCantidad)
            {
                GustoEmpanada GustoEmpanada = GustoEmpanadaSvc.FindById(g.IdGustoEmpanada);
                if (p.GustoEmpanada.Contains(GustoEmpanada))
                {
                    if (i != null && i.Count != 0)
                    {
                        InvitacionPedidoGustoEmpanadaUsuario gustoEncontrado = i.Find(x => x.IdGustoEmpanada == g.IdGustoEmpanada);
                        if (gustoEncontrado != null)
                        {
                            gustoEncontrado.Cantidad = g.Cantidad;
                            Db.SaveChanges();
                        }
                        else
                        {
                            InvitacionPedidoGustoEmpanadaUsuario gusto = new InvitacionPedidoGustoEmpanadaUsuario
                            {
                                IdGustoEmpanada = g.IdGustoEmpanada,
                                IdPedido = p.IdPedido,
                                IdUsuario = c.IdUsuario,
                                Cantidad = g.Cantidad
                            };
                            InvitacionPedidoGustoEmpanadaUsuarioSvc.Save(gusto);
                        }
                    } else
                    {
                        InvitacionPedidoGustoEmpanadaUsuario gusto = new InvitacionPedidoGustoEmpanadaUsuario
                        {
                            IdGustoEmpanada = g.IdGustoEmpanada,
                            IdPedido = p.IdPedido,
                            IdUsuario = c.IdUsuario,
                            Cantidad = g.Cantidad
                        };
                        InvitacionPedidoGustoEmpanadaUsuarioSvc.Save(gusto);
                    }
                }
                else
                {
                    mensaje = "El gusto de id: " + g.IdGustoEmpanada +" no está disponible.";                 
                }
            }
            return mensaje;
        }

        internal void FillPedidoDTO(PedidoCompletoDTO Pedido, Pedido P)
        {
            List<InvitacionPedido> i = InvitacionPedidoSvc.FindAllByPedidoId(Pedido.IdPedido);
        }

        internal void SendEmails(PedidoCompletoDTO Pedido)
        {
            EmailService.ResendEmails(Pedido);
        }


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

            ///Pedido CreatedOrder = PedidoRepo.Create(Order);

            foreach (int idGustoEmpanada in Order.GustoEmpanadaDisponibles)
            {
                Order.GustoEmpanada.Add(GustoEmpanadaSvc.FindById(idGustoEmpanada));
                ///var gustoDisponible = PedidoRepo.Db.GustoEmpanada.Find(idGustoEmpanada);
                ///CreatedOrder.GustoEmpanada.Add(gustoDisponible);
            }
            Pedido CreatedOrder = PedidoRepo.Create(Order);
            //PedidoRepo.SaveChanges();

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

        internal List<GustoEmpanada> GetGustosDisponibles(int? idPedido)
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
            List<GustoEmpanada> Gustos = GetGustosDisponibles(idPedido);
            List<InvitacionPedido> invitaciones = InvitacionPedidoSvc.FindAllByPedidoId(idPedido);

            foreach (InvitacionPedidoGustoEmpanadaUsuario i in invitacionPedidoGustos)
            {
                if (!Gustos.Contains(i.GustoEmpanada))
                    Gustos.Add(i.GustoEmpanada);
            }

            List<Usuario> Usuarios = new List<Usuario>();
            //foreach (InvitacionPedidoGustoEmpanadaUsuario i in invitacionPedidoGustos)
            foreach (InvitacionPedido i in invitaciones)
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
                CantidadEmpanadasPorGustosYUsuarios = invitacionPedidoGustos,
                invitaciones = invitaciones
            };
            return PedidoCompleto;
        }
    }

}

