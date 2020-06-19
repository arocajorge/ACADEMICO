using Core.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data.Base;
using Core.Info.Contabilidad;
using Core.Data.Contabilidad;

namespace Core.Data.CuentasPorCobrar
{
    public class cxc_ConciliacionNotaCredito_Data
    {
        cxc_cobro_Data odataCobro = new cxc_cobro_Data();
        ct_cbtecble_Data odataCt = new ct_cbtecble_Data();
        cxc_ConciliacionNotaCreditoDet_Data odataDet = new cxc_ConciliacionNotaCreditoDet_Data();

        public List<cxc_ConciliacionNotaCredito_Info> GetList(int IdEmpresa, DateTime FechaIni, DateTime FechaFin)
        {
            try
            {
                FechaIni = FechaIni.Date;
                FechaFin = FechaFin.Date;
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
                            IdAlumno = item.IdAlumno,
                            IdSucursal = item.IdSucursal,
                            IdBodega = item.IdBodega,
                            IdNota = item.IdNota,
                            IdCobro = item.IdCobro,
                            Fecha = item.Fecha,
                            Valor = item.Valor,
                            Observacion = item.Observacion,
                            Estado = item.Estado,
                            IdTipoCbte = item.IdTipoCbte,
                            IdCbteCble = item.IdCbteCble,
                            Referencia = item.Referencia,
                            pe_nombreCompleto = item.pe_nombreCompleto,
                            Codigo = item.Codigo
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

        private decimal GetId(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;

                using (EntitiesCuentasPorCobrar db = new EntitiesCuentasPorCobrar())
                {
                    int Cont = db.cxc_ConciliacionNotaCredito.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (Cont > 0)
                        ID = db.cxc_ConciliacionNotaCredito.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdConciliacion) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public cxc_ConciliacionNotaCredito_Info GetInfo( int IdEmpresa, decimal IdConciliacion)
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
                        IdAplicacion = Entity.IdAplicacion,
                        IdAlumno = Entity.IdAlumno,
                        IdSucursal = Entity.IdSucursal,
                        IdBodega = Entity.IdBodega,
                        IdNota = Entity.IdNota,
                        IdCobro = Entity.IdCobro,
                        Fecha = Entity.Fecha,
                        Valor = Entity.Valor,
                        Observacion = Entity.Observacion,
                        Estado = Entity.Estado,
                        IdTipoCbte = Entity.IdTipoCbte,
                        IdCbteCble = Entity.IdCbteCble,

                        IdString = Entity.IdSucursal.ToString("0000")+Entity.IdBodega.ToString("0000")+Entity.IdNota.ToString("0000000000")
                    };
                }

                return info;
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
                #region Variables
                EntitiesCuentasPorCobrar dbCxc = new EntitiesCuentasPorCobrar();
                EntitiesFacturacion dbFac = new EntitiesFacturacion();
                #endregion


                #region Cabecera
                cxc_ConciliacionNotaCredito Entity = new cxc_ConciliacionNotaCredito
                {
                    IdEmpresa = info.IdEmpresa,
                    IdConciliacion = info.IdConciliacion = GetId(info.IdEmpresa),
                    IdAplicacion = info.IdAplicacion,
                    IdAlumno = info.IdAlumno,
                    IdSucursal = info.IdSucursal,
                    IdBodega = info.IdBodega,
                    IdNota = info.IdNota,
                    Fecha = info.Fecha,
                    Valor = info.Valor,
                    Observacion = info.Observacion,
                    Estado = true,

                    IdUsuarioCreacion = info.IdUsuarioCreacion,
                    FechaCreacion = DateTime.Now
                };
                #endregion

                #region Relacion NC con Facturas o ND
                int Secuencia_nt = 1;
                var lstDetNc = dbFac.fa_notaCreDeb_x_fa_factura_NotaDeb.Where(q => q.IdEmpresa_nt == info.IdEmpresa && q.IdSucursal_nt == info.IdSucursal && q.IdBodega_nt == info.IdBodega && q.IdNota_nt == info.IdNota).ToList();
                if (lstDetNc.Count > 0)
                    Secuencia_nt = lstDetNc.Max(q => q.secuencia) + 1;

                foreach (var item in info.ListaDet)
                {
                    dbFac.fa_notaCreDeb_x_fa_factura_NotaDeb.Add(new fa_notaCreDeb_x_fa_factura_NotaDeb
                    {
                        IdEmpresa_nt = info.IdEmpresa,
                        IdSucursal_nt = info.IdSucursal,
                        IdBodega_nt = info.IdBodega,
                        IdNota_nt = info.IdNota,
                        secuencia = Secuencia_nt,

                        IdEmpresa_fac_nd_doc_mod = item.IdEmpresa,
                        IdSucursal_fac_nd_doc_mod = item.IdSucursal,
                        IdBodega_fac_nd_doc_mod = item.IdBodega,
                        IdCbteVta_fac_nd_doc_mod = item.IdCbteVtaNota,
                        vt_tipoDoc = item.vt_TipoDoc,
                        Valor_Aplicado = item.Valor,
                        fecha_cruce = info.Fecha
                    });
                    item.secuencia_nt = Secuencia_nt++;
                }
                #endregion
                
                #region Detalle
                int Secuencia = 1;
                foreach (var item in info.ListaDet)
                {
                    dbCxc.cxc_ConciliacionNotaCreditoDet.Add(new cxc_ConciliacionNotaCreditoDet
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdConciliacion = Entity.IdConciliacion,
                        Secuencia = Secuencia++,
                        IdSucursal = item.IdSucursal,
                        IdBodega = item.IdBodega,
                        IdCbteVtaNota = item.IdCbteVtaNota,
                        vt_TipoDoc = item.vt_TipoDoc,
                        Valor = item.Valor,
                        ValorProntoPago = item.ValorProntoPago,

                        secuencia_nt = item.secuencia_nt
                    });
                }
                #endregion

                #region Cobro
                var Cobro = ArmarCobro(info);
                if (Cobro != null)
                {
                    if (odataCobro.guardarDB(Cobro))
                    {
                        Entity.IdCobro = Cobro.IdCobro;
                    }
                }
                #endregion

                #region Contabilidad
                var Diario = ArmarDiario(info);
                if (Diario != null)
                {
                    if (odataCt.guardarDB(Diario))
                    {
                        Entity.IdTipoCbte =  info.IdTipoCbte = Diario.IdTipoCbte;
                        Entity.IdCbteCble = info.IdCbteCble = Diario.IdCbteCble;
                    }
                }
                #endregion

                dbCxc.cxc_ConciliacionNotaCredito.Add(Entity);

                dbCxc.SaveChanges();
                dbFac.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool ModificarDB(cxc_ConciliacionNotaCredito_Info info)
        {
            try
            {
                #region Variables
                EntitiesCuentasPorCobrar dbCxc = new EntitiesCuentasPorCobrar();
                EntitiesFacturacion dbFac = new EntitiesFacturacion();
                #endregion

                #region Cabecera
                var Entity = dbCxc.cxc_ConciliacionNotaCredito.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdConciliacion == info.IdConciliacion).FirstOrDefault();
                if (Entity == null)
                    return false;

                Entity.Fecha = info.Fecha;
                Entity.Observacion = info.Observacion;
                Entity.IdUsuarioModificacion = info.IdUsuarioCreacion;
                Entity.FechaModificacion = DateTime.Now;
                #endregion

                #region Relacion NC con Facturas o ND
                var lstDet = dbCxc.cxc_ConciliacionNotaCreditoDet.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdConciliacion == info.IdConciliacion).ToList();
                foreach (var item in lstDet)
                {
                    var DetRel = dbFac.fa_notaCreDeb_x_fa_factura_NotaDeb.Where(q => q.IdEmpresa_nt == info.IdEmpresa && q.IdSucursal_nt == info.IdSucursal && q.IdBodega_nt == info.IdBodega && q.IdNota_nt == info.IdNota && q.secuencia == item.secuencia_nt).FirstOrDefault();
                    if (DetRel != null)
                        dbFac.fa_notaCreDeb_x_fa_factura_NotaDeb.Remove(DetRel);
                }
                int Secuencia_nt = 1;
                var lstDetNc = dbFac.fa_notaCreDeb_x_fa_factura_NotaDeb.Where(q => q.IdEmpresa_nt == info.IdEmpresa && q.IdSucursal_nt == info.IdSucursal && q.IdBodega_nt == info.IdBodega && q.IdNota_nt == info.IdNota).ToList();
                if (lstDetNc.Count > 0)
                    Secuencia_nt = lstDetNc.Max(q => q.secuencia) + 1;

                foreach (var item in info.ListaDet)
                {
                    dbFac.fa_notaCreDeb_x_fa_factura_NotaDeb.Add(new fa_notaCreDeb_x_fa_factura_NotaDeb
                    {
                        IdEmpresa_nt = info.IdEmpresa,
                        IdSucursal_nt = info.IdSucursal,
                        IdBodega_nt = info.IdBodega,
                        IdNota_nt = info.IdNota,
                        secuencia = Secuencia_nt,

                        IdEmpresa_fac_nd_doc_mod = item.IdEmpresa,
                        IdSucursal_fac_nd_doc_mod = item.IdSucursal,
                        IdBodega_fac_nd_doc_mod = item.IdBodega,
                        IdCbteVta_fac_nd_doc_mod = item.IdCbteVtaNota,
                        vt_tipoDoc = item.vt_TipoDoc,
                        Valor_Aplicado = item.Valor,
                        fecha_cruce = info.Fecha
                    });
                    item.secuencia_nt = Secuencia_nt++;
                }
                #endregion

                #region Detalle
                var ListaDetalle = dbCxc.cxc_ConciliacionNotaCreditoDet.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdConciliacion == info.IdConciliacion).ToList();
                dbCxc.cxc_ConciliacionNotaCreditoDet.RemoveRange(ListaDetalle);

                int Secuencia = 1;
                foreach (var item in info.ListaDet)
                {
                    dbCxc.cxc_ConciliacionNotaCreditoDet.Add(new cxc_ConciliacionNotaCreditoDet
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdConciliacion = Entity.IdConciliacion,
                        Secuencia = Secuencia++,
                        IdSucursal = item.IdSucursal,
                        IdBodega = item.IdBodega,
                        IdCbteVtaNota = item.IdCbteVtaNota,
                        vt_TipoDoc = item.vt_TipoDoc,
                        Valor = item.Valor,
                        ValorProntoPago = item.ValorProntoPago,

                        secuencia_nt = item.secuencia_nt
                    });
                }

                #endregion

                #region Cobro
                var Cobro = ArmarCobro(info);
                if (Cobro != null)
                {
                    Cobro.IdCobro = Entity.IdCobro;
                    if (Cobro.IdCobro == 0)
                    {
                        if (odataCobro.guardarDB(Cobro))
                        {
                            Entity.IdCobro = Cobro.IdCobro;
                        }
                    }
                    else
                        odataCobro.modificarDB(Cobro);
                }
                #endregion

                #region Contabilidad
                var Diario = ArmarDiario(info);
                if (Diario != null)
                {
                    Diario.IdTipoCbte = info.IdTipoCbte ?? 0;
                    Diario.IdCbteCble = info.IdCbteCble ?? 0;
                    if (Diario.IdCbteCble == 0)
                    {
                        if (odataCt.guardarDB(Diario))
                        {
                            Entity.IdTipoCbte = info.IdTipoCbte = Diario.IdTipoCbte;
                            Entity.IdCbteCble = info.IdCbteCble = Diario.IdCbteCble;
                        }
                    }else
                    {
                        odataCt.modificarDB(Diario);
                    }
                    
                }
                #endregion

                dbCxc.SaveChanges();
                dbFac.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AnularDB(cxc_ConciliacionNotaCredito_Info info)
        {
            try
            {
                #region Variables
                EntitiesCuentasPorCobrar dbCxc = new EntitiesCuentasPorCobrar();
                EntitiesFacturacion dbFac = new EntitiesFacturacion();
                #endregion

                #region Cabecera
                var Entity = dbCxc.cxc_ConciliacionNotaCredito.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdConciliacion == info.IdConciliacion).FirstOrDefault();
                if (Entity == null)
                    return false;

                Entity.Estado = false;
                Entity.IdUsuarioAnulacion = info.IdUsuarioCreacion;
                Entity.FechaAnulacion = DateTime.Now;
                Entity.MotivoAnulacion = info.MotivoAnulacion;
                #endregion

                #region Relacion NC con Facturas o ND
                var lstDet = dbCxc.cxc_ConciliacionNotaCreditoDet.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdConciliacion == info.IdConciliacion).ToList();
                foreach (var item in lstDet)
                {
                    var DetRel = dbFac.fa_notaCreDeb_x_fa_factura_NotaDeb.Where(q => q.IdEmpresa_nt == info.IdEmpresa && q.IdSucursal_nt == info.IdSucursal && q.IdBodega_nt == info.IdBodega && q.IdNota_nt == info.IdNota && q.secuencia == item.secuencia_nt).FirstOrDefault();
                    if (DetRel != null)
                        dbFac.fa_notaCreDeb_x_fa_factura_NotaDeb.Remove(DetRel);
                }
                #endregion

                if (!odataCobro.anularDB(new cxc_cobro_Info
                {
                    IdEmpresa = Entity.IdEmpresa,
                    IdSucursal = Entity.IdSucursal,
                    IdCobro = Entity.IdCobro,
                    IdUsuario = info.IdUsuarioCreacion,
                    IdUsuarioUltAnu = info.IdUsuarioCreacion,
                    MotiAnula = info.MotivoAnulacion
                }))
                    return false;

                if (Entity.IdCbteCble != null)
                {
                    if (!odataCt.anularDB(new ct_cbtecble_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdTipoCbte = Entity.IdTipoCbte ?? 0,
                        IdCbteCble = Entity.IdCbteCble ?? 0,
                        IdUsuario = info.IdUsuarioCreacion,
                        IdUsuarioAnu = info.IdUsuarioCreacion,
                        cb_MotivoAnu = info.MotivoAnulacion
                    }))
                    {
                        return false;
                    }
                }

                dbCxc.SaveChanges();
                dbFac.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private cxc_cobro_Info ArmarCobro(cxc_ConciliacionNotaCredito_Info info)
        {
            try
            {
                EntitiesFacturacion dbFac = new EntitiesFacturacion();
                var NotaCredito = dbFac.fa_notaCreDeb.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdBodega == info.IdBodega && q.IdNota == info.IdNota).FirstOrDefault();
                if (NotaCredito == null)
                    return null;

                var PuntoVta = dbFac.fa_PuntoVta.Where(q => q.IdEmpresa == NotaCredito.IdEmpresa && q.IdPuntoVta == NotaCredito.IdPuntoVta).FirstOrDefault();
                if (PuntoVta == null)
                    return null;

                cxc_cobro_Info retorno = new cxc_cobro_Info
                {
                    IdEmpresa = info.IdEmpresa,
                    IdSucursal = info.IdSucursal,
                    IdCobro = 0,
                    cr_Codigo = "CONNC"+info.IdConciliacion.ToString(),
                    IdCobro_tipo = "NTCR",
                    IdCliente = NotaCredito.IdCliente,
                    IdAlumno = info.IdAlumno,
                    cr_TotalCobro = info.ListaDet.Sum(q=> q.Valor),
                    cr_fecha = info.Fecha,
                    cr_fechaDocu = info.Fecha,
                    cr_fechaCobro = info.Fecha,
                    //cr_observacion = info.Observacion,
                    cr_ObservacionPantalla = info.Observacion,
                    IdCaja = PuntoVta.IdCaja,
                    IdUsuario= info.IdUsuarioCreacion,
                    IdUsuarioUltMod = info.IdUsuarioCreacion,
                    lst_det = new List<cxc_cobro_det_Info>()
                };

                foreach (var item in info.ListaDet)
                {
                    retorno.lst_det.Add(new cxc_cobro_det_Info
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = item.IdSucursal,
                        dc_TipoDocumento = item.vt_TipoDoc,
                        IdBodega_Cbte = item.IdBodega,
                        IdCbte_vta_nota = item.IdCbteVtaNota,
                        dc_ValorPago = item.Valor,
                        dc_ValorProntoPago = item.ValorProntoPago,
                        estado = "A",
                        IdCobro_tipo_det = "NTCR",

                        IdAnio = item.IdAnio,
                        IdPlantilla = item.IdPlantilla,
                        IdPuntoVta = item.IdPuntoVta,
                        IdCliente = item.IdCliente,
                        IdAlumno = item.IdAlumno,
                    });
                }

                if (info.ListaDet.Count == 0)
                    return null;

                return retorno;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private ct_cbtecble_Info ArmarDiario(cxc_ConciliacionNotaCredito_Info info)
        {
            try
            {
                EntitiesCuentasPorCobrar dbCxc = new EntitiesCuentasPorCobrar();
                EntitiesAcademico dbAca = new EntitiesAcademico();
                EntitiesFacturacion dbFac = new EntitiesFacturacion();
                var ParamCxc = dbCxc.cxc_Parametro.Where(q => q.IdEmpresa == info.IdEmpresa).FirstOrDefault();
                if (ParamCxc == null)
                    return null;

                if (ParamCxc.IdTipoCbte_ConciliacionNC == null)
                    return null;

                var Alumno = dbAca.vwaca_Alumno.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno).FirstOrDefault();
                if (Alumno == null)
                    return null;

                var NotaCredito = dbFac.fa_notaCreDeb.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdBodega == info.IdBodega && q.IdNota == info.IdNota).FirstOrDefault();
                if (NotaCredito == null)
                    return null;

                
                ct_cbtecble_Info retorno = new ct_cbtecble_Info
                {
                    IdEmpresa = info.IdEmpresa,
                    IdTipoCbte = ParamCxc.IdTipoCbte_ConciliacionNC ?? 0,
                    CodCbteCble = "CONNC"+info.IdConciliacion.ToString(),
                    IdSucursal = info.IdSucursal,
                    IdPeriodo = Convert.ToInt32(info.Fecha.ToString("yyyyMM")),
                    cb_Fecha = info.Fecha,
                    cb_Valor = info.ListaDet.Sum(q=> q.Valor),
                    cb_Observacion = "CONNC #"+info.IdConciliacion.ToString()+" ALUMNO: "+Alumno.pe_nombreCompleto+" OBS:"+ info.Observacion,
                    IdUsuario = info.IdUsuarioCreacion,
                    IdUsuarioUltModi = info.IdUsuarioCreacion,
                    lst_ct_cbtecble_det = new List<ct_cbtecble_det_Info>()
                };

                #region Debe
                retorno.lst_ct_cbtecble_det.Add(new ct_cbtecble_det_Info
                {
                    IdCtaCble = NotaCredito.IdCtaCble_TipoNota,
                    dc_Valor = Math.Round(info.ListaDet.Sum(q=> q.Valor),2,MidpointRounding.AwayFromZero)
                });
                #endregion

                #region Haber
                foreach (var item in info.ListaDet)
                {
                    if (item.vt_TipoDoc == "FACT")
                    {
                        var Factura = dbFac.vwfa_factura_ParaContabilizarAcademico.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdSucursal == item.IdSucursal && q.IdBodega == item.IdBodega && q.IdCbteVta == item.IdCbteVtaNota).FirstOrDefault();
                        if (Factura == null)
                            return null;

                        retorno.lst_ct_cbtecble_det.Add(new ct_cbtecble_det_Info
                        {
                            IdCtaCble = Factura.IdCtaCbleDebe,
                            dc_Valor = Math.Round(item.Valor, 2, MidpointRounding.AwayFromZero) *-1
                        });
                    }else
                    {
                        var NotaDebito = dbFac.fa_notaCreDeb.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdSucursal == item.IdSucursal && q.IdBodega == item.IdBodega && q.IdNota == item.IdCbteVtaNota).FirstOrDefault();
                        if (NotaDebito == null)
                            return null;

                        retorno.lst_ct_cbtecble_det.Add(new ct_cbtecble_det_Info
                        {
                            IdCtaCble = NotaDebito.IdCtaCble_TipoNota,
                            dc_Valor = Math.Round(item.Valor, 2, MidpointRounding.AwayFromZero) * -1
                        });
                    }
                }
                #endregion

                if (retorno.lst_ct_cbtecble_det.Count == 0)
                    return null;

                if (Math.Round(retorno.lst_ct_cbtecble_det.Sum(q => q.dc_Valor), 2, MidpointRounding.AwayFromZero) != 0)
                    return null;

                if (retorno.lst_ct_cbtecble_det.Where(q=> string.IsNullOrEmpty(q.IdCtaCble)).Count() > 0)
                    return null;

                return retorno;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool Contabilizar(int IdEmpresa, decimal IdConciliacion, string IdUsuario)
        {
            try
            {
                EntitiesCuentasPorCobrar db = new EntitiesCuentasPorCobrar();
                
                var info = GetInfo(IdEmpresa, IdConciliacion);
                if (info != null)
                {
                    info.IdUsuarioCreacion = IdUsuario;

                    info.ListaDet = odataDet.GetList(IdEmpresa, IdConciliacion);
                    var Diario = ArmarDiario(info);
                    if (Diario != null)
                    {
                        Diario.IdTipoCbte = info.IdTipoCbte ?? Diario.IdTipoCbte;
                        Diario.IdCbteCble = info.IdCbteCble ?? 0;
                        if (Diario.IdCbteCble == 0)
                        {
                            if (odataCt.guardarDB(Diario))
                            {
                                var Entity = db.cxc_ConciliacionNotaCredito.Where(q => q.IdEmpresa == IdEmpresa && q.IdConciliacion == IdConciliacion).FirstOrDefault();
                                Entity.IdTipoCbte = info.IdTipoCbte = Diario.IdTipoCbte;
                                Entity.IdCbteCble = info.IdCbteCble = Diario.IdCbteCble;
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            odataCt.modificarDB(Diario);
                        }

                    }
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ValidarEnConciliacionNC(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdNota, string Tipo)
        {
            try
            {
                using (EntitiesCuentasPorCobrar db = new EntitiesCuentasPorCobrar())
                {
                    int Cont = 0;
                    if (Tipo == "NC")
                    {
                        Cont = db.cxc_ConciliacionNotaCredito.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega && q.IdNota == IdNota && q.Estado == true).Count();
                        if (Cont > 0)
                            return false;
                    }else
                    {
                        Cont = db.cxc_ConciliacionNotaCredito.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdCobro == IdNota && q.Estado == true).Count();
                        if (Cont > 0)
                            return false;
                    }
                    
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
