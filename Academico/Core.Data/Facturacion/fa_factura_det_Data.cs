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
                                 CantidadAnterior = q.vt_cantidad,
                                 tp_manejaInven = q.tp_ManejaInven,
                                 se_distribuye = q.se_distribuye
                                
                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<fa_factura_det_Info> get_list_rubros_x_facturar(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                List<fa_factura_det_Info> Lista = new List<fa_factura_det_Info>();
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst = Context.vwaca_Matricula_Rubro_PorFacturar.Where(q => q.IdEmpresa == IdEmpresa
                             && q.IdAlumno == IdAlumno).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new fa_factura_det_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdProducto = q.IdProducto,
                            vt_cantidad = 1,
                            pr_descripcion = q.pr_descripcion,
                            vt_DescUnitario = 0,
                            vt_PrecioFinal = q.Subtotal ?? 0,
                            vt_por_iva = q.Porcentaje ?? 0,
                            vt_Precio = q.Subtotal ?? 0,
                            vt_Subtotal = q.Subtotal ?? 0,
                            vt_iva = q.ValorIVA ?? 0,
                            vt_total = q.Total ?? 0,
                            IdCod_Impuesto_Iva = q.IdCod_Impuesto_Iva,
                            IdMatricula = q.IdMatricula,
                            aca_IdPeriodo = q.IdPeriodo,
                            aca_IdRubro = q.IdRubro,
                            AplicaProntoPago = q.AplicaProntoPago,
                            IdAnio = q.IdAnio,
                            EnMatricula = q.EnMatricula,
                            FechaDesde = q.FechaDesde,
                            FechaProntoPago = q.FechaProntoPago,
                            ValorProntoPago = q.ValorProntoPago,
                            vt_detallexItems = q.DescripcionCuotas,
                            Periodo = q.Periodo,
                            IdString = q.IdEmpresa.ToString("0000") + q.IdMatricula.ToString("00000000") + q.IdPeriodo.ToString("00000000") + q.IdRubro.ToString("00000000")
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
    }
}
