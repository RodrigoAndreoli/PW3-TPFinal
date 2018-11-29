using LasEmpanadas.Models;
using LasEmpanadas.Models.DTO;
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
            return GustoEmpanadaRepository.FindAll;
        }

        public GustoEmpanada FindById(int idGustoEmpanada)
        {
            return GustoEmpanadaRepository.FindOnyById(idGustoEmpanada);
        }

        internal List<GustoEmpanadaDTO> GetAllAsView()
        {
            List<GustoEmpanada> Gustos = FindAll();
            List<GustoEmpanadaDTO> gustoEmpanadaDTO = new List<GustoEmpanadaDTO>();
            foreach (GustoEmpanada g in Gustos)
            {
                GustoEmpanadaDTO ge = new GustoEmpanadaDTO();
                ge.Id = g.IdGustoEmpanada;
                ge.Gusto = g.Nombre;
                gustoEmpanadaDTO.Add(ge);
            }
            return gustoEmpanadaDTO;
        }
    }
}