using Core.Data.Base;
using Core.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Data.Facturacion
{
    public class fa_factura_det_Data
    {
        public List<fa_factura_det_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta)
        {
            try
            {
                List<fa_factura_det_Info> Lista;
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    Lista = (from q in Context.vwfa_factura_det
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.IdBodega == IdBodega
                             && q.IdCbteVta == IdCbteVta
                             select new fa_factura_det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdCbteVta = q.IdCbteVta,
                                 IdProducto = q.IdProducto,
                                 vt_cantidad = q.vt_cantidad,
                                 vt_DescUnitario = q.vt_DescUnitario,
                                 vt_PrecioFinal = q.vt_PrecioFinal,
                                 vt_Precio = q.vt_Precio,
                                 vt_Subtotal = q.vt_Subtotal,
                                 vt_detallexItems = q.vt_detallexItems,
                                 vt_iva = q.vt_iva,
                                 vt_PorDescUnitario = q.vt_PorDescUnitario,
                                 vt_por_iva = q.vt_por_iva,
                                 vt_total = q.vt_total,
                                 IdCentroCosto = q.IdCentroCosto,
                                 IdCod_Impuesto_Iva = q.IdCod_Impuesto_Iva,
                                 IdEmpresa_pf = q.IdEmpresa_pf,
                                 IdProforma = q.IdProforma,
                                 IdPunto_Cargo = q.IdPunto_Cargo,
                                 IdPunto_cargo_grupo = q.IdPunto_cargo_grupo,
                                 IdSucursal_pf = q.IdSucursal_pf,
                                 Secuencia = q.Secuencia,
                                 Secuencia_pf = q.Secuencia_pf,
                                 pr_descripcion = q.pr_descripcion,
                                 nom_presentacion = q.nom_presentacion,
                                 lote_num_lote = q.lote_num_lote,
                                 lote_fecha_vcto = q.lote_fecha_vcto,
                                 CantidadAnterior = q.vt_cantidad,
                                 tp_manejaInven = q.tp_ManejaInven,
                                 se_distribuye = q.se_distribuye
                                
                             }).ToList();
                }
                Lista.ForEach(V =>
                {
                    V.pr_descripcion = V.pr_descripcion + " " + V.nom_presentacion + " - " + V.lote_num_lote + " - " + (V.lote_fecha_vcto != null ? Convert.ToDateTime(V.lote_fecha_vcto).ToString("dd/MM/yyyy") : "");
                });
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<fa_factura_det_Info> get_list_rubros_x_facturar(int IdEmpresa, int IdSucursal, int IdAnio, decimal IdAlumno)
        {
            try
            {
                List<fa_factura_det_Info> Lista;
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    Lista = (from q in Context.vwaca_Matricula_Rubro_PorFacturar
                             where q.IdEmpresa == IdEmpresa
                             && q.IdAnio== IdAnio
                             && q.IdAlumno == IdAlumno
                             select new fa_factura_det_Info
                             {
                                 IdProducto = q.IdProducto,
                                 vt_cantidad = 1,
                                 pr_descripcion = q.pr_descripcion,
                                 vt_DescUnitario = 0,
                                 //vt_PrecioFinal = q.Subtotal,
                                 //vt_por_iva = q.Porcentaje,
                                 //vt_Precio = q.Subtotal,
                                 //vt_Subtotal = q.Subtotal,
                                 //vt_iva = q.ValorIVA,
                                 //vt_total = q.Total,
                                 IdCod_Impuesto_Iva = q.IdCod_Impuesto_Iva,
                                 IdMatricula = q.IdMatricula,
                                 IdPeriodo=q.IdPeriodo,
                                 IdRubro = q.IdRubro
                             }).ToList();
                }
                Lista.ForEach(V =>
                {
                    V.IdString = Convert.ToInt32(V.IdEmpresa).ToString("00") + Convert.ToInt32(V.IdMatricula).ToString("000000") + Convert.ToInt32(V.IdPeriodo).ToString("00") + Convert.ToInt32(V.IdRubro).ToString("00");
                });
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
