using LasEmpanadas.Models;
using LasEmpanadas.Repository;
using System.Web;

namespace LasEmpanadas.Services
{
    public class LoginService
    {
        static UsuarioRepository UsuarioRepo = new UsuarioRepository();

        public bool Login(Usuario User)
        {
            Usuario UserFromDB = UsuarioRepo.FindOneByEmail(User.Email);
            if (UserFromDB != null)
            {
                if (UserFromDB.Password.Equals(User.Password))
                {
                    KeepInSession(UserFromDB);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Se mantiene la sesión en el HTTP context.
        /// </summary>
        /// <param name="usuario"></param>
        public void KeepInSession(Usuario User)
        {
            HttpContext.Current.Session["userLogged"] = User;
        }

    }

}