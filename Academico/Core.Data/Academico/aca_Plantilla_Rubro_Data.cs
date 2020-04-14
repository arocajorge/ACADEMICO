using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_Plantilla_Rubro_Data
    {
        public List<aca_Plantilla_Rubro_Info> getList(int IdEmpresa, int IdAnio, int IdPlantilla)
        {
            try
            {
                List<aca_Plantilla_Rubro_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.vwaca_Plantilla_Rubro.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdPlantilla==IdPlantilla).Select(q => new aca_Plantilla_Rubro_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdAnio = q.IdAnio,
                        IdPlantilla= q.IdPlantilla,
                        IdRubro = q.IdRubro,
                        IdProducto = q.IdProducto,
                        Subtotal =q.Subtotal,
                        IdCod_Impuesto_Iva = q.IdCod_Impuesto_Iva,
                        ValorIVA = q.ValorIVA,
                        Porcentaje = q.Porcentaje,
                        Total = q.Total,
                        pr_descripcion = q.pr_descripcion,
                        NomRubro = q.NomRubro,
                        IdTipoNota_descuentoDet = q.IdTipoNota_descuentoDet,
                        Valor_descuentoDet = q.Valor_descuentoDet,
                        TipoDescuento_descuentoDet = q.TipoDescuento_descuentoDet
                    }).ToList();
                }
                Lista.ForEach(q => q.IdString = q.IdPlantilla.ToString("000000") + q.IdRubro.ToString("000000"));
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public List<aca_Plantilla_Rubro_Info> get_list_asignacion(int IdEmpresa, int IdAnio)
        //{
        //    try
        //    {
        //        List<aca_Plantilla_Rubro_Info> Lista;

        //        using (EntitiesAcademico Context = new EntitiesAcademico())
        //        {
        //            Lista = (from q in Context.aca_Plantilla_Rubro
        //                     join j in Context.aca_AnioLectivo_Rubro on new { q.IdEmpresa, q.IdAnio, q.IdRubro } equals new { j.IdEmpresa, IdAnio, j.IdRubro }
        //                     where q.IdEmpresa == IdEmpresa
        //                     && q.IdAnio == IdAnio
        //                     select new aca_Plantilla_Rubro_Info
        //                     {
        //                         seleccionado = true,
        //                         IdEmpresa = q.IdEmpresa,
        //                         IdAnio = q.IdAnio,
        //                         IdRubro = q.IdRubro,
        //                         NomRubro = j.NomRubro,
        //                         IdProducto = j.IdProducto,
        //                         IdCod_Impuesto_Iva = j.IdCod_Impuesto_Iva,
        //                         Subtotal = j.Subtotal,
        //                         Porcentaje = j.Porcentaje,
        //                         Total = j.Total
        //                     }).ToList();


        //            Lista.AddRange((from j in Context.aca_AnioLectivo_Rubro
        //                            where !Context.aca_Plantilla_Rubro.Any(n => n.IdRubro == j.IdRubro && n.IdEmpresa == IdEmpresa && n.IdAnio == IdAnio)
        //                            where j.IdEmpresa == IdEmpresa
        //                            && j.IdAnio == IdAnio
        //                            select new aca_Plantilla_Rubro_Info
        //                            {
        //                                seleccionado = false,
        //                                IdEmpresa = IdEmpresa,
        //                                IdAnio = IdAnio,
        //                                IdRubro = j.IdRubro,
        //                                NomRubro = j.NomRubro,
        //                                IdProducto = j.IdProducto,
        //                                IdCod_Impuesto_Iva = j.IdCod_Impuesto_Iva,
        //                                Subtotal = j.Subtotal,
        //                                Porcentaje = j.Porcentaje,
        //                                Total = j.Total
        //                            }).ToList());
        //        }

        //        return Lista;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}
