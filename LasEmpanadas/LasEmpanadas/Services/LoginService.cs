using LasEmpanadas.Models;
using LasEmpanadas.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LasEmpanadas.Services
{
    public class LoginService
    {
        static UsuarioRepository UsuarioRepository = new UsuarioRepository();

        public bool Login(Usuario user)
        {
            Usuario UsuarioFromDB = UsuarioRepository.FindOneByEmail(user.Email);
            if (UsuarioFromDB != null)
            {
                if (UsuarioFromDB.Password.Equals(user.Password))
                {
                    KeepInSession(UsuarioFromDB);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Se mantiene la sesión en el http context 
        /// </summary>
        /// <param name="usuario"></param>
        public void KeepInSession(Usuario usuario)
        {
            HttpContext.Current.Session["userLogged"] = usuario;
        }
    }
}