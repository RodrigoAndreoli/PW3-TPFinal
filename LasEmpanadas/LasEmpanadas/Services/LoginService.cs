using LasEmpanadas.Models;
using LasEmpanadas.Repositories;
using System.Web;
using System;
using System.Web.Mvc;


namespace LasEmpanadas.Services
{
    public class LoginService : System.Web.UI.Page
    {
        
        UsuarioRepository UsuarioRepo = new UsuarioRepository();
        

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