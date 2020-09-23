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
    public class ACA_029_Promedios_Data
    {
        public List<ACA_029_Promedios_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria, int IdCatalogoTipo)
        {
            try
            {
                List<ACA_029_Promedios_Info> Lista = new List<ACA_029_Promedios_Info>();
                List<ACA_029_Promedios_Info> ListaFinal = new List<ACA_029_Promedios_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Context.Database.CommandTimeout = 5000;
                    var lst = Context.SPACA_029_Promedios(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdMateria).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_029_Promedios_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            IdMateria=q.IdMateria,
                            ExamenQ2=q.ExamenQ2,
                            ExamenQ1=q.ExamenQ1,
                            PromedioFinalQ1=q.PromedioFinalQ1,
                            PromedioFinalQ2=q.PromedioFinalQ2,
                            PromedioFinal = (IdCatalogoTipo== Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ?  q.PromedioFinalQ1 : (IdCatalogoTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.PromedioFinalQ2 : null) ),
                            Examen = (IdCatalogoTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ? q.ExamenQ1 : (IdCatalogoTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.ExamenQ2 : null))
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
