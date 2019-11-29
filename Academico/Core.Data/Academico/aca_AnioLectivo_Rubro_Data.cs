using Core.Data.Base;
using Core.Info.Academico;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AnioLectivo_Rubro_Data
    {
        public List<aca_AnioLectivo_Rubro_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa, int IdAnio)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            List<aca_AnioLectivo_Rubro_Info> Lista = new List<aca_AnioLectivo_Rubro_Info>();
            Lista = get_list(IdEmpresa, IdAnio, skip, take, args.Filter);
            return Lista;
        }

        public aca_AnioLectivo_Rubro_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args, int IdEmpresa, int IdAnio)
        {
            decimal id;
            if (args.Value == null || !decimal.TryParse(args.Value.ToString(), out id))
                return null;
            return get_info_demanda(IdEmpresa, IdAnio, Convert.ToDecimal(args.Value));
        }

        public aca_AnioLectivo_Rubro_Info get_info_demanda(int IdEmpresa, int IdAnio, decimal IdRubro)
        {
            aca_AnioLectivo_Rubro_Info info = new aca_AnioLectivo_Rubro_Info();

            using (EntitiesAcademico Contex = new EntitiesAcademico())
            {
                info = (from q in Contex.aca_AnioLectivo_Rubro
                        where q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdRubro == IdRubro
                        select new aca_AnioLectivo_Rubro_Info
                        {
                            IdRubro = q.IdRubro,
                            NomRubro = q.NomRubro,
                        }).FirstOrDefault();

            }

            return info;
        }

        public List<aca_AnioLectivo_Rubro_Info> get_list(int IdEmpresa, int IdAnio, int skip, int take, string filter)
        {
            try
            {
                List<aca_AnioLectivo_Rubro_Info> Lista = new List<aca_AnioLectivo_Rubro_Info>();
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst = (from a in Context.aca_AnioLectivo_Rubro where
                                a.IdEmpresa == IdEmpresa
                                && a.IdAnio == IdAnio
                                && (a.IdRubro.ToString() + " " + a.NomRubro).Contains(filter)
                               select new
                               {
                                   a.IdRubro,
                                   a.NomRubro
                               })
                             .OrderBy(a => a.IdRubro)
                             .Skip(skip)
                             .Take(take)
                             .ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new aca_AnioLectivo_Rubro_Info
                        {
                            IdRubro = q.IdRubro,
                            NomRubro = q.NomRubro
                        });
                    }
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_AnioLectivo_Rubro_Info> getList(int IdEmpresa, int IdAnio, bool MostrarAnulados)
        {
            try
            {
                var IdAnio_ini = IdAnio;
                var IdAnio_fin = (IdAnio == 0 ? 99999999999 : IdAnio);
                List<aca_AnioLectivo_Rubro_Info> Lista = new List<aca_AnioLectivo_Rubro_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.vwaca_AnioLectivo_Rubro.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio >= IdAnio_ini && q.IdAnio <= IdAnio_fin).Select(q => new aca_AnioLectivo_Rubro_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdRubro = q.IdRubro,
                        IdAnio = q.IdAnio,
                        NomRubro = q.NomRubro,
                        Subtotal = q.Subtotal,
                        IdCod_Impuesto_Iva = q.IdCod_Impuesto_Iva,
                        IdProducto = q.IdProducto,
                        Porcentaje = q.Porcentaje,
                        Total = q.Total,
                        ValorIVA = q.ValorIVA,
                        Descripcion = q.Descripcion
                    }).ToList();
                }
                Lista.ForEach(v => { v.IdString = v.IdEmpresa.ToString("000") + v.IdAnio.ToString("0000") + v.IdRubro.ToString("0000"); });
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_AnioLectivo_Rubro_Info getInfo(int IdEmpresa, int IdAnio, int IdRubro)
        {
            try
            {
                aca_AnioLectivo_Rubro_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_AnioLectivo_Rubro.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdRubro == IdRubro).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_AnioLectivo_Rubro_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdRubro = Entity.IdRubro,
                        AplicaProntoPago = Entity.AplicaProntoPago,
                        IdAnio = Entity.IdAnio,
                        NomRubro = Entity.NomRubro,
                        Subtotal = Entity.Subtotal,
                        IdCod_Impuesto_Iva = Entity.IdCod_Impuesto_Iva,
                        IdProducto = Entity.IdProducto,
                        Porcentaje = Entity.Porcentaje,
                        Total = Entity.Total,
                        ValorIVA = Entity.ValorIVA
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_AnioLectivo_Rubro_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AnioLectivo_Rubro Entity = new aca_AnioLectivo_Rubro
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdAnio = info.IdAnio,
                        IdRubro = info.IdRubro,
                        NomRubro = info.NomRubro,
                        AplicaProntoPago = info.AplicaProntoPago,
                        IdProducto = info.IdProducto,
                        Subtotal = info.Subtotal,
                        IdCod_Impuesto_Iva = info.IdCod_Impuesto_Iva,
                        Porcentaje = info.Porcentaje,
                        ValorIVA = info.ValorIVA,
                        Total = info.Total
                    };
                    Context.aca_AnioLectivo_Rubro.Add(Entity);

                    var lst_RubroPeriodo = Context.aca_AnioLectivo_Rubro_Periodo.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio && q.IdRubro == info.IdRubro).ToList();
                    Context.aca_AnioLectivo_Rubro_Periodo.RemoveRange(lst_RubroPeriodo);

                    if (info.lst_rubro_anio_periodo.Count > 0)
                    {
                        foreach(var item in info.lst_rubro_anio_periodo)
                        {
                            aca_AnioLectivo_Rubro_Periodo Entity_Det = new aca_AnioLectivo_Rubro_Periodo
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdAnio = info.IdAnio,
                                IdRubro = info.IdRubro,
                                IdPeriodo = item.IdPeriodo
                            };
                            Context.aca_AnioLectivo_Rubro_Periodo.Add(Entity_Det);
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

        public bool modificarDB(aca_AnioLectivo_Rubro_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AnioLectivo_Rubro Entity = Context.aca_AnioLectivo_Rubro.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio && q.IdRubro == info.IdRubro);
                    if (Entity == null)
                        return false;

                    Entity.AplicaProntoPago = info.AplicaProntoPago;
                    Entity.IdProducto = info.IdProducto;
                    Entity.Subtotal = info.Subtotal;
                    Entity.IdCod_Impuesto_Iva = info.IdCod_Impuesto_Iva;
                    Entity.Porcentaje = info.Porcentaje;
                    Entity.ValorIVA = info.ValorIVA;
                    Entity.Total = info.Total;

                    var lst_RubroPeriodo = Context.aca_AnioLectivo_Rubro_Periodo.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio && q.IdRubro == info.IdRubro).ToList();
                    Context.aca_AnioLectivo_Rubro_Periodo.RemoveRange(lst_RubroPeriodo);

                    if (info.lst_rubro_anio_periodo.Count > 0)
                    {
                        foreach (var item in info.lst_rubro_anio_periodo)
                        {
                            aca_AnioLectivo_Rubro_Periodo Entity_Det = new aca_AnioLectivo_Rubro_Periodo
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdAnio = info.IdAnio,
                                IdRubro = info.IdRubro,
                                IdPeriodo = item.IdPeriodo
                            };
                            Context.aca_AnioLectivo_Rubro_Periodo.Add(Entity_Det);
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

        public bool anularDB(aca_AnioLectivo_Rubro_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var Entity = Context.aca_AnioLectivo_Rubro.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio && q.IdRubro == info.IdRubro).FirstOrDefault();
                    if (Entity == null)
                        return false;

                    Context.aca_AnioLectivo_Rubro.Remove(Entity);

                    var lst_RubroPeriodo = Context.aca_AnioLectivo_Rubro_Periodo.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio && q.IdRubro == info.IdRubro).ToList();
                    Context.aca_AnioLectivo_Rubro_Periodo.RemoveRange(lst_RubroPeriodo);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
