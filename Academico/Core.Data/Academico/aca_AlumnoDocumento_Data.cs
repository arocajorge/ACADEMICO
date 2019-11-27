using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AlumnoDocumento_Data
    {
        public List<aca_AlumnoDocumento_Info> getList(int IdEmpresa, decimal IdAlumno, bool MostrarEnArchivo)
        {
            try
            {
                List<aca_AlumnoDocumento_Info> Lista = new List<aca_AlumnoDocumento_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.vwaca_AlumnoDocumento.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno && q.EnArchivo == (MostrarEnArchivo ? true : q.EnArchivo)).Select(q => new aca_AlumnoDocumento_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdAlumno = q.IdAlumno,
                        IdDocumento = q.IdDocumento,
                        EnArchivo = q.EnArchivo,
                        NomDocumento = q.NomDocumento
                    }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_AlumnoDocumento_Info getInfo(int IdEmpresa, decimal IdAlumno, int IdDocumento)
        {
            try
            {
                aca_AlumnoDocumento_Info info;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var Entity = Context.vwaca_AlumnoDocumento.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno && q.IdDocumento == IdDocumento).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_AlumnoDocumento_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAlumno = Entity.IdAlumno,
                        IdDocumento = Entity.IdDocumento,
                        EnArchivo = Entity.EnArchivo,
                        NomDocumento = Entity.NomDocumento
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_AlumnoDocumento_Info> get_list_matricula(int IdEmpresa, int IdAnio, string IdCurso, decimal IdAlumno)
        {
            try
            {
                List<aca_AlumnoDocumento_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var IdSede = Convert.ToInt32(IdCurso.Substring(8, 4));
                    var IdNivel = Convert.ToInt32(IdCurso.Substring(12, 4));
                    var IdJornada = Convert.ToInt32(IdCurso.Substring(16, 4));
                    var IdCursoMat = Convert.ToInt32(IdCurso.Substring(20, 4));

                    Lista = (from q in Context.vwaca_AlumnoDocumento
                             join c in Context.aca_Documento
                             on new { q.IdEmpresa, q.IdDocumento } equals new { c.IdEmpresa, c.IdDocumento }
                             where q.IdEmpresa == IdEmpresa
                             && c.Estado == true
                             && q.EnArchivo ==true
                             select new aca_AlumnoDocumento_Info
                             {
                                 seleccionado = true,
                                 IdEmpresa = q.IdEmpresa,
                                 IdAlumno = q.IdAlumno,
                                 IdDocumento = q.IdDocumento,
                                 NomDocumento = q.NomDocumento
                             }).ToList();

                    var ListaDet = ((from j in Context.vwaca_AlumnoDocumento
                                     where !Context.aca_AnioLectivo_Curso_Documento.Any(n => n.IdDocumento == j.IdDocumento && n.IdEmpresa == IdEmpresa && n.IdSede == IdSede && n.IdAnio == IdAnio && n.IdNivel == IdNivel && n.IdJornada == IdJornada && n.IdCurso == IdCursoMat)
                                     select new aca_AlumnoDocumento_Info
                                     {
                                         seleccionado = false,
                                         IdEmpresa = IdEmpresa,
                                         IdAlumno = IdAlumno,
                                         IdDocumento = j.IdDocumento,
                                         NomDocumento = j.NomDocumento
                                     }).ToList());

                    Lista.AddRange((from j in Context.vwaca_AlumnoDocumento
                                    where !Context.aca_AnioLectivo_Curso_Documento.Any(n => n.IdDocumento == j.IdDocumento && n.IdEmpresa == IdEmpresa && n.IdSede == IdSede && n.IdAnio == IdAnio && n.IdNivel == IdNivel && n.IdJornada == IdJornada && n.IdCurso == IdCursoMat)
                                    select new aca_AlumnoDocumento_Info
                                    {
                                        seleccionado = false,
                                        IdEmpresa = IdEmpresa,
                                        IdAlumno = IdAlumno,
                                        IdDocumento = j.IdDocumento,
                                        NomDocumento = j.NomDocumento
                                    }).ToList());
                }

                Lista.ForEach(q=> q.IdStringDoc = Convert.ToString(q.IdDocumento));
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
