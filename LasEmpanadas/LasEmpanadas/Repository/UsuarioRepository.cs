using LasEmpanadas.Models;

namespace LasEmpanadas.Repository
{

    public class UsuarioRepository
    {
        private MasterEntities db = new MasterEntities();

        public Usuario FindOne(Usuario user)
        {
            return db.Usuario.Find(user.Email);
        }
    }
}