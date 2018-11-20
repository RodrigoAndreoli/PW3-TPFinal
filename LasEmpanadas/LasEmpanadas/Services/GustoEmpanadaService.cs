using LasEmpanadas.Models;
using LasEmpanadas.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LasEmpanadas.Services
{
    public class GustoEmpanadaService
    {
        GustoEmpanadaRepository GustoEmpanadaRepository = new GustoEmpanadaRepository();

        public List<GustoEmpanada> FindAll()
        {
            return GustoEmpanadaRepository.FindAll();
        }

        public GustoEmpanada FindById(int idGustoEmpanada)
        {
            return GustoEmpanadaRepository.FindOnyById(idGustoEmpanada);
        }
    }
}