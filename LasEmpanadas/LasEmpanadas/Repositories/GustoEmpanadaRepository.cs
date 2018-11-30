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

        public GustoEmpanadaRepository(MasterEntities db)
        {
            Db = db;
        }

        internal List<GustoEmpanada> FindAll => Db.GustoEmpanada.ToList();

        internal GustoEmpanada FindOnyById(int idGustoEmpanada) => Db.GustoEmpanada.Find(idGustoEmpanada);

    }

}