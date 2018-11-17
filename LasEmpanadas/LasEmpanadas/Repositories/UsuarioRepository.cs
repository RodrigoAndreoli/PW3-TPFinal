using LasEmpanadas.Models;
using System.Linq;

namespace LasEmpanadas.Repositories
{
    public class UsuarioRepository
    {
        MasterEntities Db = new MasterEntities();

        internal Usuario FindOneByEmail(string Email) => Db.Usuario.SingleOrDefault(Element => Element.Email == Email);

        internal Usuario FindOneById(int Id) => Db.Usuario.SingleOrDefault(Element => Element.IdUsuario == Id);

        internal void Create(Usuario User)
        {
            Db.Usuario.Add(User);
            Db.SaveChanges();
        }

        internal void Update(Usuario User)
        {
            Usuario UserFromDb = FindOneById(User.IdUsuario);
            UserFromDb.Email = User.Email;
            UserFromDb.Password = User.Password;
            Db.SaveChanges();
        }

        internal void Delete(Usuario User)
        {
            Db.Usuario.Remove(User);
            Db.SaveChanges();
        }

    }

}