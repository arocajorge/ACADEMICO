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
    public class ACA_028_PromedioXSexo_Data
    {
        public List<ACA_028_PromedioXSexo_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdCatalogoTipo)
        {
            try
            {
                List<ACA_028_PromedioXSexo_Info> Lista = new List<ACA_028_PromedioXSexo_Info>();
                List<ACA_028_PromedioXSexo_Info> ListaFinal = new List<ACA_028_PromedioXSexo_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Context.Database.CommandTimeout = 5000;
                    var lst = Context.SPACA_028_PromedioXSexo(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_028_PromedioXSexo_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            Sexo = q.Sexo,
                            CantQ1 = q.CantQ1,
                            CantQ2=q.CantQ2,
                            PromedioFinalQ1=q.PromedioFinalQ1,
                            PromedioFinalQ2=q.PromedioFinalQ2, 
                            Cantidad = (IdCatalogoTipo== Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ?  q.CantQ1 : (IdCatalogoTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.CantQ2 : null) ),
                            PromedioFinal = (IdCatalogoTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ? q.PromedioFinalQ1 : (IdCatalogoTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.PromedioFinalQ2 : null))
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
