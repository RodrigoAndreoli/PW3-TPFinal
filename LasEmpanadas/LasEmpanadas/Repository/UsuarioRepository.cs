using LasEmpanadas.Models;
using System.Linq;

namespace LasEmpanadas.Repository
{

    public class UsuarioRepository
    {
        private MasterEntities db = new MasterEntities();

        public Usuario FindOneByEmail(string Email)
        {
            return db.Usuario.SingleOrDefault(x => x.Email == Email);
        }
    }
}