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
    public class ACA_014_Data
    {
        public List<ACA_014_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdCatalogoParcial)
        {
            try
            {

                List<ACA_014_Info> Lista = new List<ACA_014_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Context.Database.CommandTimeout=5000;
                    var lst = Context.SPACA_014(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_014_Info
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
                            CalificacionP1 = q.CalificacionP1,
                            CalificacionP2=q.CalificacionP2,
                            CalificacionP3=q.CalificacionP3,
                            ExamenQ1=q.ExamenQ1,
                            PromedioQ1 = q.PromedioQ1,
                            PromedioFinalQ1 = q.PromedioFinalQ1,
                            EquivalenciaPromedioP1 = q.EquivalenciaPromedioP1,
                            EquivalenciaPromedioP2 = q.EquivalenciaPromedioP2,
                            EquivalenciaPromedioP3 = q.EquivalenciaPromedioP3,
                            EquivalenciaPromedioEQ1 = q.EquivalenciaPromedioEQ1,
                            EquivalenciaPromedioQ1 = q.EquivalenciaPromedioQ1,
                            CalificacionP4 = (IdCatalogoParcial== Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2)? q.CalificacionP4 : null),
                            CalificacionP5 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.CalificacionP5 :null),
                            CalificacionP6 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.CalificacionP6 : null),
                            ExamenQ2 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.ExamenQ2 :null),
                            PromedioQ2 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.PromedioQ2 : null),
                            PromedioFinalQ2 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.PromedioFinalQ2 : null),
                            ExamenSupletorio = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.ExamenSupletorio: null),
                            ExamenMejoramiento = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.ExamenMejoramiento : null),
                            ExamenGracia = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.ExamenGracia : null),
                            ExamenRemedial = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.ExamenRemedial:null),
                            CampoMejoramiento = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.CampoMejoramiento : null),
                            PromedioFinal = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.PromedioFinal :null),
                            EquivalenciaPromedioP4 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2)? q.EquivalenciaPromedioP4 : ""),
                            EquivalenciaPromedioP5 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.EquivalenciaPromedioP5 : ""),
                            EquivalenciaPromedioP6 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.EquivalenciaPromedioP6 :""),
                            EquivalenciaPromedioEQ2 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.EquivalenciaPromedioEQ2 :""),
                            EquivalenciaPromedioQ2 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.EquivalenciaPromedioQ2 : ""),
                            PromedioQuimestralFinal = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.PromedioQuimestralFinal : null),
                            EquivalenciaPromedioPF = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.EquivalenciaPromedioPF :""),

                            IdEquivalenciaPromedioP1 = q.IdEquivalenciaPromedioP1,
                            IdEquivalenciaPromedioP2 = q.IdEquivalenciaPromedioP2,
                            IdEquivalenciaPromedioP3 = q.IdEquivalenciaPromedioP3,
                            IdEquivalenciaPromedioQ1 = q.IdEquivalenciaPromedioQ1,
                            IdEquivalenciaPromedioP4 = q.IdEquivalenciaPromedioP4,
                            IdEquivalenciaPromedioP5 = q.IdEquivalenciaPromedioP5,
                            IdEquivalenciaPromedioP6 = q.IdEquivalenciaPromedioP6,
                            //IdEquivalenciaPromedioEQ2 = q.IdEquivalenciaPromedioEQ1,
                            //IdEquivalenciaPromedioEQ2 = q.IdEquivalenciaPromedioEQ2,
                            IdEquivalenciaPromedioQ2 = q.IdEquivalenciaPromedioQ2,
                            IdEquivalenciaPromedioPF = q.IdEquivalenciaPromedioPF,

                            NombreRepresentante = q.NombreRepresentante,
                            NombreInspector = q.NombreInspector,
                            IdProfesorInspector = q.IdProfesorInspector
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
