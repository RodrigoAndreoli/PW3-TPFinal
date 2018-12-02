using LasEmpanadas.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LasEmpanadas.Repositories
{
    public class UsuarioRepository
    {
        MasterEntities Db = new MasterEntities();

        public UsuarioRepository(MasterEntities db)
        {
            Db = db;
        }

        internal List<Usuario> GetAll() => Db.Usuario.ToList();

        internal Usuario FindOneById(int Id) => Db.Usuario.SingleOrDefault(Element => Element.IdUsuario == Id);

        internal Usuario FindOneByEmail(string Email) => Db.Usuario.SingleOrDefault(Element => Element.Email == Email);

        internal Usuario Create(Usuario User)
        {
            Db.Usuario.Add(User);
            Db.SaveChanges();

            return User;
        }

        internal Usuario Update(Usuario User)
        {
            Usuario UserFromDb = FindOneById(User.IdUsuario);
            UserFromDb.Email = User.Email;
            UserFromDb.Password = User.Password;
            Db.SaveChanges();

            return UserFromDb;
        }

        internal void Delete(Usuario User)
        {
            Db.Usuario.Remove(User);
            Db.SaveChanges();
        }

    }

}