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
    }
}
