using LasEmpanadas.Models;
using LasEmpanadas.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LasEmpanadas.Services
{
    public class LoginService
    {
        static UsuarioRepository UsuarioRepository = new UsuarioRepository();

        public bool Login(Usuario user)
        {
            Usuario UsuarioFromBD = UsuarioRepository.FindOne(user);
            if (UsuarioFromBD != null)
            {
                // Establecer cookies y eso, ni idea como 
                return true;
            }
            return false;
        }
    }
}