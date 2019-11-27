using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AnioLectivo_Data
    {
        aca_AnioLectivo_Periodo_Data odata_periodo = new aca_AnioLectivo_Periodo_Data();
        public List<aca_AnioLectivo_Info> getList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<aca_AnioLectivo_Info> Lista = new List<aca_AnioLectivo_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_AnioLectivo.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == (MostrarAnulados ? q.Estado : true)).OrderBy(q=>q.Descripcion).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_AnioLectivo_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAnio = q.IdAnio,
                            Descripcion = q.Descripcion,
                            FechaDesde = q.FechaDesde,
                            FechaHasta = q.FechaHasta,
                            EnCurso = q.EnCurso,
                            BloquearMatricula = q.BloquearMatricula,
                            Estado = q.Estado
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

        public List<aca_AnioLectivo_Info> getList_update(int IdEmpresa)
        {
            try
            {
                List<aca_AnioLectivo_Info> Lista = new List<aca_AnioLectivo_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.aca_AnioLectivo.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true && q.BloquearMatricula == false).Select(q => new aca_AnioLectivo_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdAnio = q.IdAnio,
                        Descripcion = q.Descripcion,
                        FechaDesde = q.FechaDesde,
                        FechaHasta = q.FechaHasta,
                        EnCurso = q.EnCurso,
                        BloquearMatricula = q.BloquearMatricula,
                        Estado = q.Estado
                    }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_AnioLectivo_Info getInfo(int IdEmpresa, int IdAnio)
        {
            try
            {
                aca_AnioLectivo_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_AnioLectivo.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_AnioLectivo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAnio = Entity.IdAnio,
                        Descripcion = Entity.Descripcion,
                        FechaDesde = Entity.FechaDesde,
                        FechaHasta = Entity.FechaHasta,
                        EnCurso = Entity.EnCurso,
                        BloquearMatricula = Entity.BloquearMatricula,
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

        public aca_AnioLectivo_Info getInfo_AnioAnterior(int IdEmpresa, int Anio)
        {
            try
            {
                aca_AnioLectivo_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_AnioLectivo.Where(q => q.IdEmpresa == IdEmpresa && q.FechaDesde.Year == Anio).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_AnioLectivo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAnio = Entity.IdAnio,
                        Descripcion = Entity.Descripcion,
                        FechaDesde = Entity.FechaDesde,
                        FechaHasta = Entity.FechaHasta,
                        EnCurso = Entity.EnCurso,
                        BloquearMatricula = Entity.BloquearMatricula,
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

        public aca_AnioLectivo_Info getInfo_AnioEnCurso(int IdEmpresa, int IdAnio)
        {
            try
            {
                aca_AnioLectivo_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_AnioLectivo.Where(q => q.IdEmpresa == IdEmpresa && q.Estado==true && q.EnCurso==true && (IdAnio==0 ? q.IdAnio == q.IdAnio : q.IdAnio!= IdAnio)).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_AnioLectivo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAnio = Entity.IdAnio,
                        Descripcion = Entity.Descripcion,
                        FechaDesde = Entity.FechaDesde,
                        FechaHasta = Entity.FechaHasta,
                        EnCurso = Entity.EnCurso,
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

        public int getId(int IdEmpresa)
        {
            try
            {
                int ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_AnioLectivo.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_AnioLectivo.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdAnio) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_AnioLectivo_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var IdPeriodo = 0;
                    aca_AnioLectivo Entity = new aca_AnioLectivo
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdAnio = info.IdAnio = getId(info.IdEmpresa),
                        Descripcion = info.Descripcion,
                        FechaDesde = info.FechaDesde,
                        FechaHasta = info.FechaHasta,
                        EnCurso = info.EnCurso,
                        BloquearMatricula = info.BloquearMatricula,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_AnioLectivo.Add(Entity);

                    if (info.lst_periodos.Count >0)
                    {
                        IdPeriodo = odata_periodo.getId(info.IdEmpresa);
                        foreach (var item in info.lst_periodos)
                        {
                            aca_AnioLectivo_Periodo Entity_Periodo = new aca_AnioLectivo_Periodo
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdPeriodo = IdPeriodo++,
                                IdAnio = info.IdAnio,
                                IdMes = item.IdMes,
                                FechaDesde = item.FechaDesde,
                                FechaHasta = item.FechaHasta,
                                FechaProntoPago = null,
                                Estado = true,
                                IdUsuarioCreacion = info.IdUsuarioCreacion,
                                FechaCreacion = info.FechaCreacion = DateTime.Now

                            };
                                Context.aca_AnioLectivo_Periodo.Add(Entity_Periodo);
                        }
                    }

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool modificarDB(aca_AnioLectivo_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AnioLectivo Entity = Context.aca_AnioLectivo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio);
                    if (Entity == null)
                        return false;

                    Entity.Descripcion = info.Descripcion;
                    Entity.FechaDesde = info.FechaDesde;
                    Entity.FechaHasta = info.FechaHasta;
                    Entity.EnCurso = info.EnCurso;
                    Entity.BloquearMatricula = info.BloquearMatricula;
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

        public bool anularDB(aca_AnioLectivo_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AnioLectivo Entity = Context.aca_AnioLectivo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio);
                    if (Entity == null)
                        return false;
                    Entity.Estado = info.Estado = false;
                    Entity.MotivoAnulacion = info.MotivoAnulacion;
                    Entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                    Entity.FechaAnulacion = info.FechaAnulacion = DateTime.Now;
                    Entity.EnCurso = false;
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
