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
    public class ACA_028_ConductaBaja_Data
    {
        public List<ACA_028_ConductaBaja_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdCatalogoTipo)
        {
            try
            {
                List<ACA_028_ConductaBaja_Info> Lista = new List<ACA_028_ConductaBaja_Info>();
                List<ACA_028_ConductaBaja_Info> ListaFinal = new List<ACA_028_ConductaBaja_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Context.Database.CommandTimeout = 5000;
                    var lst = Context.SPACA_028_ConductaBaja(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_028_ConductaBaja_Info
                        {
                            Num = 1,
                            IdEmpresa = q.IdEmpresa,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            pe_nombreCompleto =q.pe_nombreCompleto,
                            CalificacionQ1 = q.CalificacionQ1,
                            CalificacionQ2 = q.CalificacionQ2,
                            CalificacionPF=q.CalificacionPF,
                            SecuenciaQ1 =q.SecuenciaQ1,
                            SecuenciaQ2=q.SecuenciaQ2,
                            SecuenciaPF=q.SecuenciaPF,
                            LetraPF=q.LetraPF,
                            LetraQ1=q.LetraQ1,
                            LetraQ2=q.LetraQ2,
                            MotivoPromedioFinalQ1=q.MotivoPromedioFinalQ1,
                            MotivoPromedioFinalQ2=q.MotivoPromedioFinalQ2,
                            MotivoPromedioFinal = q.MotivoPromedioFinal,
                            Codigo = q.Codigo,
                            IdMatricula=q.IdMatricula,
                            Calificacion = (IdCatalogoTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ? q.CalificacionQ1 : (IdCatalogoTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.CalificacionQ2 : null)),
                            Letra = (IdCatalogoTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ? q.LetraQ1 : (IdCatalogoTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.LetraQ2 : null)),
                            Motivo = (IdCatalogoTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ? q.MotivoPromedioFinalQ1 : (IdCatalogoTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.MotivoPromedioFinalQ2 : null))

                        });
                    }
                    ListaFinal = Lista.Where(q=>q.Calificacion < 4).ToList();
                }

                return ListaFinal;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
