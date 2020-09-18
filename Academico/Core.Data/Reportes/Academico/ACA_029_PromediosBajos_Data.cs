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
    public class ACA_029_PromediosBajos_Data
    {
        public List<ACA_029_PromediosBajos_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria, int IdCatalogoTipo)
        {
            try
            {
                List<ACA_029_PromediosBajos_Info> Lista = new List<ACA_029_PromediosBajos_Info>();
                List<ACA_029_PromediosBajos_Info> ListaFinal = new List<ACA_029_PromediosBajos_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Context.Database.CommandTimeout = 5000;
                    var lst = Context.SPACA_029_PromediosBajos(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdMateria).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_029_PromediosBajos_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            Codigo = q.Codigo,
                            pe_nombreCompleto=q.pe_nombreCompleto,
                            IdAlumno =q.IdAlumno,
                            NomMateria=q.NomMateria,
                            OrdenMateria=q.OrdenMateria,
                            OrdenMateriaGrupo=q.OrdenMateriaGrupo,
                            IdMatricula = q.IdMatricula,
                            PromedioFinalQ1=q.PromedioFinalQ1,
                            PromedioFinalQ2=q.PromedioFinalQ2,
                            PromedioMinimoPromocion=q.PromedioMinimoPromocion,
                            IdMateria=q.IdMateria,
                            PromedioQuimestral = (IdCatalogoTipo== Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ?  q.PromedioFinalQ1 : (IdCatalogoTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.PromedioFinalQ2 : null) ),
                            Causa = (IdCatalogoTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ? q.CausaQ1 : (IdCatalogoTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.CausaQ1 : null)),
                            Resolucion = (IdCatalogoTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ? q.ResolucionQ1 : (IdCatalogoTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.ResolucionQ2 : null))
                        });
                    }
                    ListaFinal = Lista.Where(q => q.PromedioQuimestral!=null && q.PromedioQuimestral < Convert.ToDecimal(q.PromedioMinimoPromocion)).ToList();
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
