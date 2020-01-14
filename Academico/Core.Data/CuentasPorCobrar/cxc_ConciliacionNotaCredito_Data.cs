using Core.Data.Base;
using Core.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.CuentasPorCobrar
{
    public class cxc_ConciliacionNotaCredito_Data
    {
        cxc_cobro_Data odataCobro = new cxc_cobro_Data();
        public List<cxc_ConciliacionNotaCredito_Info> GetList(int IdEmpresa, DateTime FechaIni, DateTime FechaFin)
        {
            try
            {
                List<cxc_ConciliacionNotaCredito_Info> Lista = new List<cxc_ConciliacionNotaCredito_Info>();

                using (EntitiesCuentasPorCobrar db = new EntitiesCuentasPorCobrar())
                {
                    var lst = db.vwcxc_ConciliacionNotaCredito.Where(q => q.IdEmpresa == IdEmpresa && FechaIni <= q.Fecha && q.Fecha <= FechaFin).ToList();
                    foreach (var item in lst)
                    {
                        Lista.Add(new cxc_ConciliacionNotaCredito_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdConciliacion = item.IdConciliacion,
                            IdSucursal = item.IdSucursal,
                            IdBodega = item.IdBodega,
                            IdNota = item.IdNota,
                            IdCobro = item.IdCobro,
                            Fecha = item.Fecha,
                            Valor = item.Valor,
                            Observacion = item.Observacion,
                            Estado = item.Estado,
                            Referencia = item.Referencia,
                            pe_nombreCompleto = item.pe_nombreCompleto
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

        public cxc_ConciliacionNotaCredito_Info GetInfo(int IdEmpresa, decimal IdConciliacion)
        {
            try
            {
                cxc_ConciliacionNotaCredito_Info info = new cxc_ConciliacionNotaCredito_Info();

                using (EntitiesCuentasPorCobrar db = new EntitiesCuentasPorCobrar())
                {
                    var Entity = db.cxc_ConciliacionNotaCredito.Where(q => q.IdEmpresa == IdEmpresa && q.IdConciliacion == IdConciliacion).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new cxc_ConciliacionNotaCredito_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdConciliacion = Entity.IdConciliacion,
                        IdSucursal = Entity.IdSucursal,
                        IdBodega = Entity.IdBodega,
                        IdNota = Entity.IdNota,
                        IdCobro = Entity.IdCobro,
                        Fecha = Entity.Fecha,
                        Valor = Entity.Valor,
                        Observacion = Entity.Observacion,
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

        public decimal GetId(int IdEmpresa)
        {
            try
            {
                decimal id = 1;
                using (EntitiesCuentasPorCobrar db = new EntitiesCuentasPorCobrar())
                {
                    var cont = db.cxc_ConciliacionNotaCredito.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        id = db.cxc_ConciliacionNotaCredito.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdConciliacion) + 1;
                }
                return id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDB(cxc_ConciliacionNotaCredito_Info info)
        {
            try
            {
                EntitiesFacturacion dbf = new EntitiesFacturacion();
                EntitiesCuentasPorCobrar dbc = new EntitiesCuentasPorCobrar();

                #region Cabecera conciliacion
                cxc_ConciliacionNotaCredito Entity = new cxc_ConciliacionNotaCredito
                {
                    IdEmpresa = info.IdEmpresa,
                    IdConciliacion = info.IdConciliacion = GetId(info.IdEmpresa),
                    IdSucursal = info.IdSucursal,
                    IdBodega = info.IdBodega,
                    IdNota = info.IdNota,
                    IdCobro = info.IdCobro,
                    Fecha = info.Fecha,
                    Valor = info.Valor,
                    Observacion = info.Observacion,
                    Estado = true,

                    IdUsuarioCreacion = info.IdUsuarioCreacion,
                    FechaCreacion = DateTime.Now
                };
                #endregion

                #region Detalle conciliacion
                int Secuencia = 1;
                foreach (var item in info.ListaDet)
                {
                    dbc.cxc_ConciliacionNotaCreditoDet.Add(new cxc_ConciliacionNotaCreditoDet
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdConciliacion = info.IdConciliacion,
                        Secuencia = Secuencia++,
                        IdSucursal = item.IdSucursal,
                        IdBodega = item.IdBodega,
                        IdCbteVtaNota = item.IdCbteVtaNota,
                        vt_TipoDoc = item.vt_TipoDoc,
                        Valor = item.Valor
                    });
                }
                #endregion

                #region Cobro
                var Cobro = ArmarInfoCobro(info);
                if (Cobro != null)
                {


                    if (odataCobro.guardarDB(Cobro))
                    {
                        Entity.IdCobro = info.IdCobro = Cobro.IdCobro;
                    }
                    else
                        return false;
                }
                else
                    return false;
                #endregion

                #region Cruce con nota de credito
                dbf.fa_notaCreDeb_x_cxc_cobro.Add(new fa_notaCreDeb_x_cxc_cobro
                {
                    IdEmpresa_nt = info.IdEmpresa,
                    IdSucursal_nt = info.IdSucursal,
                    IdBodega_nt = info.IdBodega,
                    IdNota_nt = info.IdNota,
                    Valor_cobro = info.Valor,
                    IdEmpresa_cbr = info.IdEmpresa,
                    IdSucursal_cbr = info.IdSucursal,
                    IdCobro_cbr = info.IdCobro
                });

                Secuencia = 1;
                foreach (var item in info.ListaDet)
                {
                    dbf.fa_notaCreDeb_x_fa_factura_NotaDeb.Add(new fa_notaCreDeb_x_fa_factura_NotaDeb
                    {
                        IdEmpresa_nt = info.IdEmpresa,
                        IdSucursal_nt = info.IdSucursal,
                        IdBodega_nt = info.IdBodega,
                        IdNota_nt = info.IdNota,

                        IdEmpresa_fac_nd_doc_mod = info.IdEmpresa,
                        IdSucursal_fac_nd_doc_mod = info.IdSucursal,
                        IdBodega_fac_nd_doc_mod = info.IdBodega,
                        IdCbteVta_fac_nd_doc_mod = item.IdCbteVtaNota,
                        vt_tipoDoc = item.vt_TipoDoc,
                        Valor_Aplicado = item.Valor,
                        fecha_cruce = info.Fecha,
                        NumDocumento = item.Referencia,
                        secuencia = Secuencia++
                    });
                }
                
                #endregion

                dbc.SaveChanges();


                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public cxc_cobro_Info ArmarInfoCobro(cxc_ConciliacionNotaCredito_Info info)
        {
            try
            {
                EntitiesFacturacion dbf = new EntitiesFacturacion();
                EntitiesCuentasPorCobrar dbc = new EntitiesCuentasPorCobrar();
                cxc_cobro_Info cobro = new cxc_cobro_Info();

                var nc = dbf.fa_notaCreDeb.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdBodega == info.IdBodega && q.IdNota == info.IdNota).FirstOrDefault();
                if (nc == null)
                    return null;

                var ptovta = dbf.fa_PuntoVta.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdPuntoVta == nc.IdPuntoVta).FirstOrDefault();
                if (ptovta == null)
                    return null;

                cobro = new cxc_cobro_Info
                {
                    IdEmpresa = info.IdEmpresa,
                    IdSucursal = info.IdSucursal,
                    cr_Codigo = "NTCR" + info.IdNota.ToString("000000"),
                    IdCobro_tipo = "NTCR",
                    IdCliente = nc.IdCliente,
                    IdAlumno = nc.IdAlumno,
                    cr_TotalCobro = info.Valor,
                    cr_fecha = info.Fecha,
                    cr_fechaDocu = info.Fecha,
                    cr_fechaCobro = info.Fecha,
                    cr_observacion = info.Observacion,
                    cr_NumDocumento = "NTCR" + info.IdNota.ToString("000000"),
                    IdCaja = ptovta.IdCaja,
                    IdUsuario = info.IdUsuarioCreacion
                };

                cobro.lst_det = new List<cxc_cobro_det_Info>();
                foreach (var item in info.ListaDet)
                {
                    cobro.lst_det.Add(new cxc_cobro_det_Info
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = info.IdSucursal,
                        dc_TipoDocumento = item.vt_TipoDoc,
                        IdBodega_Cbte = item.IdBodega,
                        IdCbte_vta_nota = item.IdCbteVtaNota,
                        dc_ValorPago = item.Valor,
                        estado = "A",
                        IdCobro_tipo_det = "NTCR"                        
                    });
                }

                return cobro;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
