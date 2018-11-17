using LasEmpanadas.Models;
using System.Linq;

namespace LasEmpanadas.Repository
{

    public class UsuarioRepository
    {
        private MasterEntities Db = new MasterEntities();

        public Usuario FindOneByEmail(string Email)
        {
            return Db.Usuario.SingleOrDefault(Element => Element.Email == Email);
        }

    }

}