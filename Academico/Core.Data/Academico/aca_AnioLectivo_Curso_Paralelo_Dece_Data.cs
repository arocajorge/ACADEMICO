using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AnioLectivo_Curso_Paralelo_Dece_Data
    {
        public List<aca_AnioLectivo_Curso_Paralelo_Dece_Info> getList(int IdEmpresa, int IdAnio, int IdSede, bool MostrarAnulados)
        {
            try
            {
                List<aca_AnioLectivo_Curso_Paralelo_Dece_Info> Lista = new List<aca_AnioLectivo_Curso_Paralelo_Dece_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_AnioLectivo_Curso_Paralelo_Dece.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio==IdAnio && q.Estado == (MostrarAnulados ? q.Estado : true)).OrderBy(q => new { q.OrdenJornada, q.OrdenCurso, q.OrdenParalelo }).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_AnioLectivo_Curso_Paralelo_Dece_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAnio = q.IdAnio,
                            IdSede= q.IdSede,
                            IdJornada=q.IdJornada,
                            IdNivel=q.IdNivel,
                            IdCurso=q.IdCurso,
                            IdParalelo=q.IdParalelo,
                            OrdenParalelo=q.OrdenParalelo,
                            OrdenCurso=q.OrdenCurso,
                            OrdenJornada=q.OrdenJornada,
                            NomCurso=q.NomCurso,
                            NomJornada=q.NomJornada,
                            NomParalelo=q.NomParalelo,
                            ObservacionQ1=q.ObservacionQ1,
                            ObservacionQ2=q.ObservacionQ2,
                            Estado = q.Estado??false
                        });
                    });
                }
                Lista.ForEach(q=>q.IdString = q.IdEmpresa.ToString("000") + q.IdSede.ToString("000") + q.IdAnio.ToString("000") + q.IdJornada.ToString("000") + q.IdNivel.ToString("000") + q.IdCurso.ToString("000") + q.IdParalelo.ToString("000"));
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_AnioLectivo_Curso_Paralelo_Dece_Info getInfo(int IdEmpresa, int IdAnio, int IdSede, int IdJornada, int IdNivel, int IdCurso, int IdParalelo)
        {
            try
            {
                aca_AnioLectivo_Curso_Paralelo_Dece_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_AnioLectivo_Curso_Paralelo_Dece.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio
                    && q.IdSede == IdSede && q.IdJornada == IdJornada && q.IdNivel == IdNivel && q.IdCurso == IdCurso && q.IdParalelo == IdParalelo).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_AnioLectivo_Curso_Paralelo_Dece_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAnio = Entity.IdAnio,
                        IdSede = Entity.IdSede,
                        IdJornada = Entity.IdJornada,
                        IdNivel = Entity.IdNivel,
                        IdCurso = Entity.IdCurso,
                        IdParalelo = Entity.IdParalelo,
                        ObservacionQ1 = Entity.ObservacionQ1,
                        ObservacionQ2 = Entity.ObservacionQ2,
                        Estado = Entity.Estado
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_AnioLectivo_Curso_Paralelo_Dece_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AnioLectivo_Curso_Paralelo_Dece Entity = new aca_AnioLectivo_Curso_Paralelo_Dece
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdAnio = info.IdAnio,
                        IdSede = info.IdSede,
                        IdJornada = info.IdJornada,
                        IdNivel = info.IdNivel,
                        IdCurso = info.IdCurso,
                        IdParalelo = info.IdParalelo,
                        ObservacionQ1 = info.ObservacionQ1,
                        ObservacionQ2 = info.ObservacionQ2,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_AnioLectivo_Curso_Paralelo_Dece.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_AnioLectivo_Curso_Paralelo_Dece_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AnioLectivo_Curso_Paralelo_Dece Entity = Context.aca_AnioLectivo_Curso_Paralelo_Dece.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa 
                    && q.IdAnio == info.IdAnio && q.IdSede==info.IdSede && q.IdJornada== info.IdJornada && q.IdNivel==info.IdNivel && q.IdCurso==info.IdCurso && q.IdParalelo==info.IdParalelo);
                    if (Entity == null)
                        return false;

                    Entity.ObservacionQ1 = info.ObservacionQ1;
                    Entity.ObservacionQ2 = info.ObservacionQ2;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = info.FechaModificacion = DateTime.Now;

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(aca_AnioLectivo_Curso_Paralelo_Dece_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AnioLectivo_Curso_Paralelo_Dece Entity = Context.aca_AnioLectivo_Curso_Paralelo_Dece.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa
                    && q.IdAnio == info.IdAnio && q.IdSede == info.IdSede && q.IdJornada == info.IdJornada && q.IdNivel == info.IdNivel && q.IdCurso == info.IdCurso && q.IdParalelo == info.IdParalelo);
                    if (Entity == null)
                        return false;

                    Entity.Estado = info.Estado = false;
                    Entity.MotivoAnulacion = info.MotivoAnulacion;
                    Entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                    Entity.FechaAnulacion = info.FechaAnulacion = DateTime.Now;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
