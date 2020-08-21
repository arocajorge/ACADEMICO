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
    public class ACA_013_Asistencia_Data
    {
        public List<ACA_013_Asistencia_Info> get_list(int IdEmpresa, decimal IdMatricula, int IdCatalogoParcial)
        {
            try
            {
                List < ACA_013_Asistencia_Info> Lista = new List<ACA_013_Asistencia_Info>();
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Context.Database.CommandTimeout = 5000;
                    var lst = Context.aca_MatriculaAsistencia.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_013_Asistencia_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            FaltasInjustificadas = ((IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P1)) ? q.FInjustificadaP1 :
                                                (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P2)) ? q.FInjustificadaP2 :
                                                (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P3)) ? q.FInjustificadaP3 :
                                                (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P4)) ? q.FInjustificadaP4 :
                                                (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P5)) ? q.FInjustificadaP5 :
                                                (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P6)) ? q.FInjustificadaP6 :
                                                null
                                                ),
                            FaltasJustificadas = ((IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P1)) ? q.FJustificadaP1 :
                                            (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P2)) ? q.FJustificadaP2 :
                                            (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P3)) ? q.FJustificadaP3 :
                                            (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P4)) ? q.FJustificadaP4 :
                                            (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P5)) ? q.FJustificadaP5 :
                                            (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P6)) ? q.FJustificadaP6 :
                                            null
                                            ),
                            Atrasos = ((IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P1)) ? q.AtrasosP1 :
                                            (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P2)) ? q.AtrasosP2 :
                                            (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P3)) ? q.AtrasosP3 :
                                            (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P4)) ? q.AtrasosP4 :
                                            (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P5)) ? q.AtrasosP5 :
                                            (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P6)) ? q.AtrasosP3 :
                                            null
                                            )
                        });
                    }

                    return Lista;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
