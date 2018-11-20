using LasEmpanadas.Models;
using LasEmpanadas.Models.DTO;
using LasEmpanadas.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LasEmpanadas.Controllers
{
    public class GustoEmpanadaController : Controller
    {
        GustoEmpanadaService GustoEmpanadaService = new GustoEmpanadaService();
        public string All()
        {
            List<GustoEmpanada> Gustos = GustoEmpanadaService.FindAll();
            List<GustoEmpanadaDTO> gustoEmpanadaDTO = new List<GustoEmpanadaDTO>();
            foreach (GustoEmpanada g in Gustos)
            {
                GustoEmpanadaDTO ge = new GustoEmpanadaDTO();
                ge.Id = g.IdGustoEmpanada;
                ge.Gusto = g.Nombre;
                gustoEmpanadaDTO.Add(ge);
            }
            string jsonArray = JsonConvert.SerializeObject(gustoEmpanadaDTO);
            return jsonArray;
        }
    }
}