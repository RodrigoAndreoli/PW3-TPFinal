using LasEmpanadas.Models;
using LasEmpanadas.Repositories;
using System.Web;
using System;
using System.Web.Mvc;


namespace LasEmpanadas.Services
{
    public class LoginService : System.Web.UI.Page
    {

        UsuarioRepository UsuarioRepo;
        private MasterEntities db;

        public LoginService()
        {
            this.db = new MasterEntities();
            this.UsuarioRepo = new UsuarioRepository(db);
        }

        public LoginService(MasterEntities db)
        {
            this.db = db;
            this.UsuarioRepo = new UsuarioRepository(db);
        }

        internal bool Login(Usuario User)
        {
            Usuario UserFromDb = UsuarioRepo.FindOneByEmail(User.Email);


            if (UserFromDb != null)
            {
                if (UserFromDb.Password.Equals(User.Password))
                {
                    Session["loggedUser"] = UserFromDb;
                    Session["idUser"] = UserFromDb.IdUsuario;
                    Session["emailUser"] = UserFromDb.Email;
                    return true;
                }
            }
             return false; 
        }

        internal int GetLoggedUserId() {
            return Convert.ToInt32(Session["idUser"]);
        }

        /// <summary>
        /// Se mantiene la sesión en el HTTP context.
        /// </summary>
        /// <param name="User"></param>
        internal void KeepInSession(Usuario User)
        {
            HttpContext.Current.Session["userLogged"] = User;
        }

    }

}