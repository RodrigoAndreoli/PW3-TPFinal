using LasEmpanadas.Models;
using LasEmpanadas.Repositories;
using System;
using System.Collections.Generic;

namespace LasEmpanadas.Services
{
    public class UsuarioService
    {
        UsuarioRepository UsuarioRepo;
        private MasterEntities db;

        public UsuarioService()
        {
            db = new MasterEntities();
            UsuarioRepo = new UsuarioRepository(db);
        }

        public UsuarioService(MasterEntities db)
        {
            this.db = db;
            UsuarioRepo = new UsuarioRepository(this.db);
        }

        internal void CheckEmailList(string[] EmailList)
        {
            foreach (string Email in EmailList)
            {
                if (UsuarioRepo.FindOneByEmail(Email) == null)
                {
                    RegisterUserFromEmail(Email);
                }
            }
        }

        internal Usuario FindOneById(int idUsuario)
        {
            return UsuarioRepo.FindOneById(idUsuario);
        }

        internal void RegisterUserFromEmail(string Email)
        {
            string GeneratedPassword = GeneratePassword(Email);

            Usuario User = new Usuario
            {
                Email = Email,
                Password = GeneratedPassword
            };

            UsuarioRepo.Create(User);
        }

        internal string GeneratePassword(string Email)
        {
            string FirstPart = Email.Split('@')[0];
            string SecondPart = DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

            //return FirstPart + '.' + SecondPart;
            return "test1234";
        }

        internal int GetIdFromEmail(string Email)
        {
            return UsuarioRepo.FindOneByEmail(Email).IdUsuario;
        }

        internal void RegisterUserFromEmailList(List<string> usuariosNuevosString)
        {
            if (usuariosNuevosString != null)
            {
                CheckEmailList(usuariosNuevosString.ToArray());
            }
        }
    }

}