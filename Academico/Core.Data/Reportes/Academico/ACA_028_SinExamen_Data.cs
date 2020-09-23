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
    public class ACA_028_SinExamen_Data
    {
        public List<ACA_028_SinExamen_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdCatalogoTipo)
        {
            try
            {
                List<ACA_028_SinExamen_Info> Lista = new List<ACA_028_SinExamen_Info>();
                List<ACA_028_SinExamen_Info> ListaFinal = new List<ACA_028_SinExamen_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Context.Database.CommandTimeout = 5000;
                    var lst = Context.SPACA_028_SinExamen(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdCatalogoTipo).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_028_SinExamen_Info
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
                            Descripcion = q.Descripcion,
                            IdProfesor = q.IdProfesor,
                            Examen = q.Examen,
                            IdMateria=q.IdMateria,
                            Codigo=q.Codigo,
                            NombreAlumno=q.NombreAlumno
                        });
                    }
                }
                ListaFinal = Lista.Where(q=>q.Examen==null).ToList();

                return ListaFinal;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
