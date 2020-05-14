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
    public class ACA_014_Conducta_Data
    {
        public List<ACA_014_Conducta_Info> get_list(int IdEmpresa, decimal IdMatricula, int IdCatalogoParcial)
        {
            try
            {

                List<ACA_014_Conducta_Info> Lista = new List<ACA_014_Conducta_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Context.Database.CommandTimeout=5000;
                    var lst = Context.SPACA_014_Conducta(IdEmpresa, IdMatricula).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_014_Conducta_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            LetraP1 = q.LetraP1,
                            LetraP2 = q.LetraP2,
                            LetraP3 = q.LetraP3,
                            LetraQ1 = q.LetraQ1,
                            LetraP4 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.LetraP4 : null),
                            LetraP5 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.LetraP5 : null),
                            LetraP6 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.LetraP6 : null),
                            LetraQ2 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.LetraQ2 : null),
                            LetraPF = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.LetraPF :null),
                            SecuenciaP1 = q.SecuenciaP1,
                            SecuenciaP2 = q.SecuenciaP2,
                            SecuenciaP3 = q.SecuenciaP3,
                            SecuenciaP4 = q.SecuenciaP4,
                            SecuenciaP5 = q.SecuenciaP5,
                            SecuenciaP6 = q.SecuenciaP6,
                            SecuenciaQ1=q.SecuenciaQ1,
                            SecuenciaQ2=q.SecuenciaQ2,
                            SecuenciaPF=q.SecuenciaPF
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
