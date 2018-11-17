using LasEmpanadas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LasEmpanadas.Services
{
    public class UsuarioService
    {
        private MasterEntities db = new MasterEntities();

        //registra un usuario ingresando el mail y asigna una contraseña generica
        public void RegisterUserByEmail(string email)
        {
            Usuario usuario = new Usuario();
            usuario.Email = email;
            usuario.Password = email + DateTime.Today;
            usuario.IdUsuario = db.Usuario.Max(u => u.IdUsuario) + 1;
            db.Usuario.Add(usuario);
            db.SaveChanges();
        }

        //recibe un email y devuelve un usuario
        public Usuario FindByEmail(string email)
        {
            return db.Usuario.Where(u => u.Email == email).FirstOrDefault();
        }

        public Usuario FindById(int id)
        {
            return db.Usuario.Where(u => u.IdUsuario == id).FirstOrDefault();
        }
    }
}