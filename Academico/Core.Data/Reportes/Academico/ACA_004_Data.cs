using Core.Data.Base;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Data.Reportes.Academico
{
    public class ACA_004_Data
    {
        public List<ACA_004_Info> Getlist(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso)
        {
            try
            {
                List<ACA_004_Info> Lista = new List<ACA_004_Info>();
                using (EntitiesReportes db = new EntitiesReportes())
                {
                    Lista = db.VWACA_004.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso).Select(q => new ACA_004_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdAnio = q.IdAnio,
                        IdSede = q.IdSede,
                        IdNivel = q.IdNivel,
                        IdJornada = q.IdJornada,
                        IdCurso = q.IdCurso,
                        NomSede = q.NomSede,
                        NomJornada = q.NomJornada,
                        NomCurso = q.NomCurso,
                        NomNivel = q.NomNivel
                        
                    }).ToList();
                }

                    return Lista;

            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
