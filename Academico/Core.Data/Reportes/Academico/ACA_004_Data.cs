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
                    var lst = db.VWACA_004.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && 
                    IdSedeIni <= q.IdSede && q.IdSede <= IdSedeFin && 
                    IdNivelIni <= q.IdNivel && q.IdNivel <= IdNivelFin && 
                    IdJornadaIni <= q.IdJornada && q.IdJornada <= IdJornadaFin && 
                    IdCursoIni <= q.IdCurso && q.IdCurso <= IdCursoFin).ToList();

                    foreach (var item in lst)
                    {
                        Lista.Add(new ACA_004_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdAnio = item.IdAnio,
                            IdSede = item.IdSede,
                            IdNivel = item.IdNivel,
                            IdJornada = item.IdJornada,
                            IdCurso = item.IdCurso,
                            IdMateria = item.IdMateria,
                            Descripcion = item.Descripcion,
                            FechaDesde = item.FechaDesde,
                            FechaHasta = item.FechaHasta,
                            NomSede = item.NomSede,
                            NomNivel = item.NomNivel,
                            NomJornada = item.NomJornada,
                            OrdenJornada = item.OrdenJornada,
                            NomCurso = item.NomCurso,
                            OrdenCurso = item.OrdenCurso,
                            NomMateria = item.NomMateria,
                            NomMateriaArea = item.NomMateriaArea,
                            NomMateriaGrupo = item.NomMateriaGrupo,
                            OrdenMateria = item.OrdenMateria,
                            OrdenMateriaGrupo = item.OrdenMateriaGrupo,
                            OrdenMateriaArea = item.OrdenMateriaArea,
                            EsObligatorio = item.EsObligatorio,
                        });
                    }
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
