using LasEmpanadas.Models;
using LasEmpanadas.Repository;
using System.Web;

namespace LasEmpanadas.Services
{
    public class LoginService
    {
        UsuarioRepository UsuarioRepo = new UsuarioRepository();

        internal bool Login(Usuario User)
        {
            Usuario UserFromDb = UsuarioRepo.FindOneByEmail(User.Email);

            if (UserFromDb != null)
            {
                if (UserFromDb.Password.Equals(User.Password))
                {
                    KeepInSession(UserFromDb);
                    return true;
                }
            }

            return false;
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