using LasEmpanadas.Models;
using LasEmpanadas.Repositories;
using System;

namespace LasEmpanadas.Services
{
    public class UsuarioService
    {
        UsuarioRepository UsuarioRepo = new UsuarioRepository();

        internal void CheckEmailList(string[ ] EmailList)
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
      
            return FirstPart + '.' + SecondPart;
        }

        internal int GetIdFromEmail(string Email)
        {
            return UsuarioRepo.FindOneByEmail(Email).IdUsuario;
        }

    }

}