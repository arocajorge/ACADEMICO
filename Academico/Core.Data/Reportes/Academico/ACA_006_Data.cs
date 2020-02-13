using Core.Data.Base;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Data.Reportes.Academico
{
    public class ACA_006_Data
    {
        public List<ACA_006_Info> Getlist(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso)
        {
            try
            {
                int IdSedeIni = IdSede;
                int IdSedeFin = IdSede == 0 ? 9999999 : IdSede;

                int IdNivelIni = IdNivel;
                int IdNivelFin = IdNivel == 0 ? 9999999 : IdNivel;

                int IdJornadaIni = IdJornada;
                int IdJornadaFin = IdJornada == 0 ? 9999999 : IdJornada;

                int IdCursoIni = IdCurso;
                int IdCursoFin = IdCurso == 0 ? 9999999 : IdCurso;

                List<ACA_006_Info> Lista = new List<ACA_006_Info>();
                using (EntitiesReportes db = new EntitiesReportes())
                {
                    Lista = db.VWACA_006.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && IdSedeIni <= q.IdSede && q.IdSede <= IdSedeFin && IdNivelIni <= q.IdNivel && q.IdNivel <= IdNivelFin && IdJornadaIni <= q.IdJornada && q.IdJornada <= IdJornadaFin && IdCursoIni <= q.IdCurso && q.IdCurso <= IdCursoFin).Select(q => new ACA_006_Info
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
                        NomNivel = q.NomNivel,
                        OrdenJornada = q.OrdenJornada,
                        OrdenCurso = q.OrdenCurso,
                        NomParalelo = q.NomParalelo,
                        pe_sexo = q.pe_sexo

                    }).ToList();
                }

                    return Lista;
             }
            catch (Exception EX)
            {
                throw;
            }
        }
            
    }
}

