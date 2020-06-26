using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AnioLectivo_Periodo_Data
    {
        public List<aca_AnioLectivo_Periodo_Info> getList(int IdEmpresa)
        {
            try
            {
                List<aca_AnioLectivo_Periodo_Info> Lista = new List<aca_AnioLectivo_Periodo_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.vwaca_AnioLectivo_Periodo.Where(q => q.IdEmpresa == IdEmpresa).Select(q => new aca_AnioLectivo_Periodo_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdAnio = q.IdAnio,
                        Descripcion = q.Descripcion,
                        NumPeriodos = q.NumPeriodos??0
                    }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<aca_AnioLectivo_Periodo_Info> getList_AnioCurso(int IdEmpresa)
        {
            try
            {
                List<aca_AnioLectivo_Periodo_Info> Lista = new List<aca_AnioLectivo_Periodo_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = (from ap in Context.aca_AnioLectivo_Periodo
                             join a in Context.aca_AnioLectivo on new { ap.IdEmpresa, ap.IdAnio } equals new { a.IdEmpresa, a.IdAnio }
                             where ap.IdEmpresa == IdEmpresa
                             && a.EnCurso==true
                             select new aca_AnioLectivo_Periodo_Info
                             {
                                IdEmpresa = ap.IdEmpresa,
                                IdAnio = ap.IdAnio,
                                FechaDesde = ap.FechaDesde,
                                Descripcion = a.Descripcion,
                                IdPeriodo = ap.IdPeriodo,
                                IdMes = ap.IdMes,
                                FechaHasta = ap.FechaHasta,
                                Estado = ap.Estado,
                                Procesado=ap.Procesado,
                                TotalValorFacturado = ap.TotalValorFacturado
                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_AnioLectivo_Periodo_Info> getList(int IdEmpresa, int IdAnio, bool MostrarAnulados)
        {
            try
            {
                List<aca_AnioLectivo_Periodo_Info> Lista = new List<aca_AnioLectivo_Periodo_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.aca_AnioLectivo_Periodo.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.Estado == (MostrarAnulados == true ? q.Estado : true)).Select(q => new aca_AnioLectivo_Periodo_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdPeriodo = q.IdPeriodo,
                        IdAnio = q.IdAnio,
                        IdMes = q.IdMes,
                        FechaDesde = q.FechaDesde,
                        FechaHasta = q.FechaHasta,
                        FechaProntoPago = q.FechaProntoPago,
                        Estado = q.Estado
                    }).ToList();
                }
                Lista.ForEach(v => { v.NomPeriodo = v.FechaDesde.Year.ToString("0000") + v.FechaDesde.Month.ToString("00"); });
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public aca_AnioLectivo_Periodo_Info getInfo(int IdEmpresa, int IdAnio, int IdPeriodo)
        {
            try
            {
                aca_AnioLectivo_Periodo_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_AnioLectivo_Periodo.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdPeriodo == IdPeriodo).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_AnioLectivo_Periodo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdPeriodo = Entity.IdPeriodo,
                        IdAnio = Entity.IdAnio,
                        IdMes = Entity.IdMes,
                        FechaDesde = Entity.FechaDesde,
                        FechaHasta = Entity.FechaHasta,
                        FechaProntoPago = Entity.FechaProntoPago,
                        Estado = Entity.Estado,
                        Procesado = Entity.Procesado
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(List<aca_AnioLectivo_Periodo_Info> info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    foreach (var item in info)
                    {
                        aca_AnioLectivo_Periodo Entity = Context.aca_AnioLectivo_Periodo.FirstOrDefault(q => q.IdEmpresa == item.IdEmpresa && q.IdAnio == item.IdAnio && q.IdPeriodo == item.IdPeriodo);
                        if (Entity == null)
                            return false;

                        Entity.IdMes = item.IdMes;
                        Entity.FechaDesde = item.FechaDesde;
                        Entity.FechaHasta = item.FechaHasta;
                        Entity.FechaProntoPago = item.FechaProntoPago;
                        Entity.IdUsuarioModificacion = item.IdUsuarioModificacion;
                        Entity.FechaModificacion = item.FechaModificacion = DateTime.Now;

                        Context.SaveChanges();
                    }
                    
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(aca_AnioLectivo_Periodo_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AnioLectivo_Periodo Entity = Context.aca_AnioLectivo_Periodo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio);
                    if (Entity == null)
                        return false;

                    Entity.Estado = false;
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

        public int getId(int IdEmpresa)
        {
            try
            {
                int ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_AnioLectivo_Periodo.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_AnioLectivo_Periodo.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdPeriodo) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public bool modificar_FacturacionMasiva(aca_AnioLectivo_Periodo_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AnioLectivo_Periodo Entity = Context.aca_AnioLectivo_Periodo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio && q.IdPeriodo == info.IdPeriodo);
                    if (Entity == null)
                        return false;

                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = DateTime.Now;
                    Entity.IdSucursal = info.IdSucursal;
                    Entity.IdPuntoVta = info.IdPuntoVta;
                    Entity.Procesado = true;
                    Entity.FechaProceso = DateTime.Now;
                    Entity.TotalAlumnos = info.lst_det_fact_masiva.Count();
                    Entity.TotalValorFacturado = info.lst_det_fact_masiva.Sum(q=>q.Total);

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
