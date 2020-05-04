using Core.Data.Base;
using Core.Info.Academico;
using Core.Info.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AlumnoRetiro_Data
    {
        public List<aca_AlumnoRetiro_Info> getList(int IdEmpresa)
        {
            try
            {
                List<aca_AlumnoRetiro_Info> Lista = new List<aca_AlumnoRetiro_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_AlumnoRetiro.Where(q => q.IdEmpresa == IdEmpresa).OrderBy(q => q.Fecha).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_AlumnoRetiro_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAlumno = q.IdAlumno,
                            IdRetiro = q.IdRetiro,
                            IdMatricula = q.IdMatricula,
                            IdAnio = q.IdAnio,
                            Fecha = q.Fecha,
                            Observacion = q.Observacion,
                            IdCatalogoESTALU = q.IdCatalogoESTALU,
                            pe_cedulaRuc = q.pe_cedulaRuc,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            Codigo = q.Codigo,
                            Descripcion = q.Descripcion,
                            NomSede = q.NomSede,
                            NomNivel = q.NomNivel,
                            NomJornada = q.NomJornada,
                            NomCurso = q.NomCurso,
                            NomParalelo = q.NomParalelo,
                            Estado = q.Estado,
                            IdUsuarioAnulacion = q.IdUsuarioAnulacion
                        });
                    });
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_AlumnoRetiro_Info getList(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                aca_AlumnoRetiro_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.vwaca_AlumnoRetiro.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno && q.IdUsuarioAnulacion==null).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_AlumnoRetiro_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdRetiro = Entity.IdRetiro,
                        IdMatricula = Entity.IdMatricula,
                        IdAlumno = Entity.IdAlumno,
                        Fecha = Entity.Fecha,
                        Observacion = Entity.Observacion,
                        IdCatalogoESTALU = Entity.IdCatalogoESTALU,
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
        public decimal getId(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_AlumnoRetiro.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_AlumnoRetiro.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdRetiro) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_AlumnoRetiro_Info getInfo(int IdEmpresa, int IdRetiro)
        {
            try
            {
                aca_AlumnoRetiro_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.vwaca_AlumnoRetiro.Where(q => q.IdEmpresa == IdEmpresa && q.IdRetiro == IdRetiro).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_AlumnoRetiro_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdRetiro = Entity.IdRetiro,
                        IdMatricula = Entity.IdMatricula,
                        IdAnio = Entity.IdAnio,
                        IdAlumno = Entity.IdAlumno,
                        Fecha = Entity.Fecha,
                        Observacion = Entity.Observacion,
                        IdCatalogoESTALU = Entity.IdCatalogoESTALU,
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

        public bool guardarDB(aca_AlumnoRetiro_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AlumnoRetiro Entity = new aca_AlumnoRetiro
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdRetiro = info.IdRetiro = getId(info.IdEmpresa),
                        IdMatricula = info.IdMatricula,
                        IdCatalogoESTALU = info.IdCatalogoESTALU,
                        Fecha = info.Fecha,
                        Observacion = info.Observacion,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now,
                        Estado = true
                    };
                    Context.aca_AlumnoRetiro.Add(Entity);

                    aca_Alumno Entity_Alumno = Context.aca_Alumno.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno);
                    Entity_Alumno.IdCatalogoESTALU = Convert.ToInt32(cl_enumeradores.eCatalogoAcademicoAlumno.RETIRADO);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(aca_AlumnoRetiro_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AlumnoRetiro Entity = Context.aca_AlumnoRetiro.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdRetiro == info.IdRetiro);
                    if (Entity == null)
                        return false;

                    aca_Alumno Entity_Alumno = Context.aca_Alumno.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno);
                    Entity_Alumno.IdCatalogoESTALU = info.IdCatalogoESTALU;
                    Entity_Alumno.Estado = false;
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
