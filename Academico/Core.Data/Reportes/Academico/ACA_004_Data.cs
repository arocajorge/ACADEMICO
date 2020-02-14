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
                int IdSedeIni = IdSede;
                int IdSedeFin = IdSede== 0 ? 9999999 : IdSede;

                int IdNivelIni = IdNivel;
                int IdNivelFin = IdNivel == 0 ? 9999999 : IdNivel;

                int IdJornadaIni = IdJornada;
                int IdJornadaFin = IdJornada == 0 ? 9999999 : IdJornada;

                int IdCursoIni = IdCurso;
                int IdCursoFin = IdCurso == 0 ? 9999999 : IdCurso;

                List<ACA_004_Info> Lista = new List<ACA_004_Info>();
                using (EntitiesReportes db = new EntitiesReportes())
                {
                    Lista = db.VWACA_004.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && IdSedeIni <= q.IdSede && q.IdSede <= IdSedeFin && IdNivelIni <= q.IdNivel && q.IdNivel <= IdNivelFin && IdJornadaIni <= q.IdJornada && q.IdJornada <= IdJornadaFin && IdCursoIni <= q.IdCurso && q.IdCurso <= IdCursoFin).Select(q => new ACA_004_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdAnio = q.IdAnio,
                        IdSede = q.IdSede,
                        IdNivel = q.IdNivel,
                        IdJornada = q.IdJornada,
                        IdCurso = q.IdCurso,
                        IdMateria = q.IdMateria,
                        Descripcion = q.Descripcion,
                        NomSede = q.NomSede,
                        NomJornada = q.NomJornada,
                        NomCurso = q.NomCurso,
                        NomNivel = q.NomNivel,
                        OrdenJornada = q.OrdenJornada,
                        OrdenCurso = q.OrdenCurso,
                        NomMateria = q.NomMateria,
                        NomMateriaArea = q.NomMateriaArea,
                        NomMateriaGrupo = q.NomMateriaGrupo,
                        OrdenMateria = q.OrdenMateria,
                        OrdenMateriaGrupo = q.OrdenMateriaGrupo,
                        OrdenMateriaArea = q.OrdenMateriaArea,
                        EsObligatorio = q.EsObligatorio
                    }).ToList();
                }

                    return Lista;

            }
            catch(Exception EX)
            {
                throw;
            }
        }
    }
}
