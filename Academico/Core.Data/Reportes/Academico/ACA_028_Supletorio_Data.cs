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
    public class ACA_028_Supletorio_Data
    {
        public List<ACA_028_Supletorio_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdCatalogoTipo)
        {
            try
            {
                List<ACA_028_Supletorio_Info> Lista = new List<ACA_028_Supletorio_Info>();
                List<ACA_028_Supletorio_Info> ListaFinal = new List<ACA_028_Supletorio_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Context.Database.CommandTimeout = 5000;
                    var lst = Context.SPACA_028_Supletorio(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdCatalogoTipo).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_028_Supletorio_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            IdAlumno =q.IdAlumno,
                            NomMateria=q.NomMateria,
                            OrdenMateria=q.OrdenMateria,
                            IdMatricula = q.IdMatricula,
                            PromedioMinimoPromocion=q.PromedioMinimoPromocion,
                            IdMateria=q.IdMateria,
                            NombreAlumno=q.NombreAlumno,
                            Codigo=q.Codigo,
                            Descripcion = q.Descripcion,
                            IdProfesor = q.IdProfesor,
                            PromedioFinal = q.PromedioFinal
                        });
                    }
                    ListaFinal = Lista.Where(q => q.PromedioFinal != null && q.PromedioFinal < Convert.ToDecimal(q.PromedioMinimoPromocion)).ToList();
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
