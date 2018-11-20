using LasEmpanadas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LasEmpanadas.Repositories
{
    public class GustoEmpanadaRepository
    {
        MasterEntities Db = new MasterEntities();

        public List<GustoEmpanada> FindAll()
        {
            return Db.GustoEmpanada.ToList();
        }
    }
}