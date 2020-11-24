using Core.Data.Base;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Academico
{
    public class ACA_018_Data
    {
        public List<ACA_018_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, decimal IdAlumno, bool MostrarRetirados)
        {
            try
            {
                int IdSedeIni = IdSede;
                int IdSedeFin = IdSede == 0 ? 9999999 : IdSede;

                int IdJornadaIni = IdJornada;
                int IdJornadaFin = IdJornada == 0 ? 9999999 : IdJornada;

                int IdNivelIni = IdNivel;
                int IdNivelFin = IdNivel == 0 ? 9999999 : IdNivel;

                int IdCursoIni = IdCurso;
                int IdCursoFin = IdCurso == 0 ? 9999999 : IdCurso;

                int IdParaleloIni = IdParalelo;
                int IdParaleloFin = IdParalelo == 0 ? 9999999 : IdParalelo;

                decimal IdAlumnoIni = IdAlumno;
                decimal IdAlumnoFin = IdAlumno == 0 ? 999999999999 : IdAlumno;

                List<ACA_018_Info> Lista = new List<ACA_018_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Context.Database.CommandTimeout = 5000;
                    var lst = Context.VWACA_018.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio
                    && q.IdSede == IdSede
                    && q.IdJornada == IdJornada
                    && q.IdNivel== IdNivel &&
                    IdCursoIni <= q.IdCurso && q.IdCurso <= IdCursoFin &&
                    IdParaleloIni <= q.IdParalelo && q.IdParalelo <= IdParaleloFin &&
                    IdAlumnoIni <= q.IdAlumno && q.IdAlumno <= IdAlumnoFin &&
                    q.EsRetirado == (MostrarRetirados == true ? q.EsRetirado : false)).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_018_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdAlumno = q.IdAlumno,
                            pe_cedulaRuc = q.pe_cedulaRuc,
                            NombreAlumno = q.NombreAlumno,
                            Codigo = q.Codigo,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            Descripcion = q.Descripcion,
                            NomSede = q.NomSede,
                            NomNivel = q.NomNivel,
                            NomJornada = q.NomJornada,
                            NomCurso = q.NomCurso,
                            NomParalelo = q.NomParalelo,
                            OrdenNivel = q.OrdenNivel,
                            OrdenJornada = q.OrdenJornada,
                            OrdenCurso = q.OrdenCurso,
                            OrdenParalelo = q.OrdenParalelo,
                            EsRetirado = q.EsRetirado,
                            Fecha = q.Fecha,
                            FechaMatricula = q.Fecha.ToString("d/MM/yyy"),
                            IdPersona = q.IdPersona,
                            FechaActual = DateTime.Now.ToString("d' de 'MMMM' de 'yyyy")
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
