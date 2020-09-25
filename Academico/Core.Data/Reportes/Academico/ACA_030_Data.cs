using Core.Data.Base;
using Core.Info.Helps;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Academico
{
    public class ACA_030_Data
    {
        public List<ACA_030_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdCatalogoParcialTipo, decimal IdAlumno, bool MostrarRetirados)
        {
            try
            {

                List<ACA_030_Info> Lista = new List<ACA_030_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Context.Database.CommandTimeout=5000;
                    var lst = Context.SPACA_030(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdAlumno, MostrarRetirados).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_030_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdAlumno = q.IdAlumno,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            Codigo = q.Codigo,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            CodigoParalelo = q.CodigoParalelo,
                            Descripcion = q.Descripcion,
                            NomSede = q.NomSede,
                            NomNivel = q.NomNivel,
                            NomJornada = q.NomJornada,
                            NomMateria = q.NomMateria,
                            NomCurso = q.NomCurso,
                            NomParalelo = q.NomParalelo,
                            OrdenNivel = q.OrdenNivel,
                            OrdenJornada = q.OrdenJornada,
                            OrdenCurso = q.OrdenCurso,
                            OrdenParalelo = q.OrdenParalelo,
                            OrdenMateriaGrupo = q.OrdenMateriaGrupo,
                            OrdenMateriaArea = q.OrdenMateriaArea,
                            OrdenMateria = q.OrdenMateria,
                            EsObligatorio = q.EsObligatorio,
                            NomMateriaArea = q.NomMateriaArea,
                            NomMateriaGrupo = q.NomMateriaGrupo,
                            NombreTutor = q.NombreTutor,
                            NombreInspector = q.NombreInspector,
                            P1 = IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ? q.CalificacionP1 : (IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.CalificacionP4 : null),
                            P2 = IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ? q.CalificacionP2 : (IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.CalificacionP5 : null),
                            P3 = IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ? q.CalificacionP3 : (IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.CalificacionP6 : null),
                            PROM80 = IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ? q.PorcentajeQ1 : (IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.PorcentajeQ2 : null),
                            EXAMEN = IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ? q.ExamenQ1 : (IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.ExamenQ2 : null),
                            EXA20 = IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ? q.PorcentajeEQ1 : (IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.PorcentajeEQ2 : null),
                            PROMFINAL = IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ?  q.PromedioFinalQ1 : (IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.PromedioFinalQ2 : null),
                        });
                    }
                }

                return Lista;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
