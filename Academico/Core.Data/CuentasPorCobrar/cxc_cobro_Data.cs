using Core.Data.Base;
using Core.Data.Caja;
using Core.Data.Contabilidad;
using Core.Data.Facturacion;
using Core.Data.General;
using Core.Info.Caja;
using Core.Info.Contabilidad;
using Core.Info.CuentasPorCobrar;
using Core.Info.Facturacion;
using Core.Info.General;
using Core.Info.Helps;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Core.Data.CuentasPorCobrar
{
    public class cxc_cobro_Data
    {
        caj_Caja_Movimiento_Data DataCajaMovimiento = new caj_Caja_Movimiento_Data();
        ct_cbtecble_Data DataContable = new ct_cbtecble_Data();
        fa_notaCreDeb_Data DataNotaCredito = new fa_notaCreDeb_Data();
        cxc_cobro_det_Data DataDet = new cxc_cobro_det_Data();

        public List<cxc_cobro_Info> get_list(int IdEmpresa, int IdSucursal, DateTime Fecha_ini, DateTime Fecha_fin)
        {
            try
            {
                int IdSucursal_ini = IdSucursal;
                int IdSucursal_fin = IdSucursal == 0 ? 9999 : IdSucursal;
                List<cxc_cobro_Info> Lista;

                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    Lista = (from q in Context.vwcxc_cobro
                             where q.IdEmpresa == IdEmpresa
                             && IdSucursal_ini <= q.IdSucursal && q.IdSucursal <= IdSucursal_fin
                             && Fecha_ini <= q.cr_fecha && q.cr_fecha <= Fecha_fin
                             orderby q.IdCobro descending
                             select new cxc_cobro_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdCobro = q.IdCobro,
                                 IdCliente = q.IdCliente,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 IdCobro_tipo = q.IdCobro_tipo,
                                 cr_fecha = q.cr_fecha,
                                 cr_TotalCobro = q.cr_TotalCobro,
                                 cr_estado = q.cr_estado,
                                 Su_Descripcion = q.Su_Descripcion,
                                 cr_observacion = q.cr_observacion,
                                 nom_Motivo_tipo_cobro = q.nom_Motivo_tipo_cobro,
                                 cr_NumDocumento = q.cr_NumDocumento,
                                 Usuario=q.IdUsuario,
                                 //cr_ObservacionPantalla = q.cr_ObservacionPantalla,
                                 EstadoBool = q.cr_estado == "A" ? true : false
                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<cxc_cobro_Info> get_list_matricula(int IdEmpresa, int IdSucursal, DateTime Fecha_ini, DateTime Fecha_fin)
        {
            try
            {
                int IdSucursal_ini = IdSucursal;
                int IdSucursal_fin = IdSucursal == 0 ? 9999 : IdSucursal;
                List<cxc_cobro_Info> Lista = new List<cxc_cobro_Info>();

                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT dbo.cxc_cobro.IdEmpresa, dbo.cxc_cobro.IdSucursal, dbo.cxc_cobro.IdCobro, dbo.cxc_cobro.IdCliente, dbo.tb_persona.pe_nombreCompleto, dbo.cxc_cobro.IdCobro_tipo, dbo.cxc_cobro.cr_fecha, dbo.cxc_cobro.cr_TotalCobro, "
                    + " dbo.cxc_cobro.cr_estado, dbo.tb_sucursal.Su_Descripcion, dbo.cxc_cobro.cr_observacion, ISNULL(ISNULL(dbo.cxc_cobro.cr_Tarjeta, dbo.cxc_cobro.cr_NumDocumento), ba.ba_descripcion) AS cr_NumDocumento, "
                    + " ISNULL(Tipo.nom_Motivo_tipo_cobro, t2.nom_Motivo_tipo_cobro) AS nom_Motivo_tipo_cobro, dbo.cxc_cobro.IdAlumno, tb_persona_1.pe_nombreCompleto AS NomAlumno, dbo.cxc_cobro.cr_Saldo, dbo.cxc_cobro.IdUsuario "
                    + " FROM dbo.tb_persona AS tb_persona_1 INNER JOIN "
                    + " dbo.aca_Alumno ON tb_persona_1.IdPersona = dbo.aca_Alumno.IdPersona RIGHT OUTER JOIN "
                    + " dbo.cxc_cobro INNER JOIN "
                    + " dbo.fa_cliente ON dbo.cxc_cobro.IdEmpresa = dbo.fa_cliente.IdEmpresa AND dbo.cxc_cobro.IdCliente = dbo.fa_cliente.IdCliente INNER JOIN "
                    + " dbo.tb_persona ON dbo.fa_cliente.IdPersona = dbo.tb_persona.IdPersona INNER JOIN "
                    + " dbo.tb_sucursal ON dbo.cxc_cobro.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.cxc_cobro.IdSucursal = dbo.tb_sucursal.IdSucursal ON dbo.aca_Alumno.IdEmpresa = dbo.cxc_cobro.IdEmpresa AND "
                    + " dbo.aca_Alumno.IdAlumno = dbo.cxc_cobro.IdAlumno LEFT OUTER JOIN "
                      + " (SELECT dbo.cxc_cobro_det.IdEmpresa, dbo.cxc_cobro_det.IdSucursal, dbo.cxc_cobro_det.IdCobro, dbo.cxc_cobro_tipo_motivo.nom_Motivo_tipo_cobro "
                       + " FROM dbo.cxc_cobro_det INNER JOIN "
                       + " dbo.cxc_cobro_tipo ON dbo.cxc_cobro_det.IdCobro_tipo = dbo.cxc_cobro_tipo.IdCobro_tipo INNER JOIN "
                       + " dbo.cxc_cobro_tipo_motivo ON dbo.cxc_cobro_tipo.IdMotivo_tipo_cobro = dbo.cxc_cobro_tipo_motivo.IdMotivo_tipo_cobro "
                       + " GROUP BY dbo.cxc_cobro_det.IdEmpresa, dbo.cxc_cobro_det.IdSucursal, dbo.cxc_cobro_det.IdCobro, dbo.cxc_cobro_tipo_motivo.nom_Motivo_tipo_cobro) AS Tipo ON Tipo.IdEmpresa = dbo.cxc_cobro.IdEmpresa AND "
                    + " Tipo.IdSucursal = dbo.cxc_cobro.IdSucursal AND Tipo.IdCobro = dbo.cxc_cobro.IdCobro LEFT OUTER JOIN "
                    + " dbo.cxc_cobro_tipo AS t1 ON dbo.cxc_cobro.IdCobro_tipo = t1.IdCobro_tipo LEFT OUTER JOIN "
                    + " dbo.cxc_cobro_tipo_motivo AS t2 ON t1.IdMotivo_tipo_cobro = t2.IdMotivo_tipo_cobro LEFT OUTER JOIN "
                    + " dbo.ba_Banco_Cuenta AS ba ON dbo.cxc_cobro.IdEmpresa = ba.IdEmpresa AND dbo.cxc_cobro.IdBanco = ba.IdBanco "
                    + " WHERE dbo.cxc_cobro.IdEmpresa = " + IdEmpresa + " and dbo.cxc_cobro.IdSucursal between " + IdSucursal_ini + " and " + IdSucursal_fin
                    + " and dbo.cxc_cobro.cr_fecha between " + "'" + Fecha_ini.ToString("dd/MM/yyyy") + "'" + " and " + "'" + Fecha_fin.Date.ToString("dd/MM/yyyy") + "'"
                    + " and dbo.cxc_cobro.IdAlumno is not null Order by  dbo.cxc_cobro.IdCobro desc";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new cxc_cobro_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdSucursal = Convert.ToInt32(reader["IdSucursal"]),
                            IdCobro = Convert.ToDecimal(reader["IdCobro"]),
                            IdCliente = Convert.ToDecimal(reader["IdCliente"]),
                            pe_nombreCompleto = reader["pe_nombreCompleto"].ToString(),
                            IdCobro_tipo = reader["IdCobro_tipo"].ToString(),
                            cr_fecha = Convert.ToDateTime(reader["cr_fecha"]),
                            cr_TotalCobro = Convert.ToDouble(reader["cr_TotalCobro"]),
                            cr_estado = reader["cr_estado"].ToString(),
                            Su_Descripcion = reader["Su_Descripcion"].ToString(),
                            cr_observacion = reader["cr_observacion"].ToString(),
                            nom_Motivo_tipo_cobro = reader["nom_Motivo_tipo_cobro"].ToString(),
                            cr_NumDocumento = reader["cr_NumDocumento"].ToString(),
                            NomAlumno = reader["NomAlumno"].ToString(),
                            EstadoBool = (reader["cr_estado"].ToString() == "A" ? true : false),
                            IdUsuario = reader["IdUsuario"].ToString()
                        });
                    }
                    reader.Close();
                }
                /*
                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    Lista = (from q in Context.vwcxc_cobro
                             where q.IdEmpresa == IdEmpresa
                             && IdSucursal_ini <= q.IdSucursal && q.IdSucursal <= IdSucursal_fin
                             && Fecha_ini <= q.cr_fecha && q.cr_fecha <= Fecha_fin
                             && q.IdAlumno != null
                             orderby q.IdCobro descending
                             select new cxc_cobro_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdCobro = q.IdCobro,
                                 IdCliente = q.IdCliente,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 IdCobro_tipo = q.IdCobro_tipo,
                                 cr_fecha = q.cr_fecha,
                                 cr_TotalCobro = q.cr_TotalCobro,
                                 cr_estado = q.cr_estado,
                                 Su_Descripcion = q.Su_Descripcion,
                                 cr_observacion = q.cr_observacion,
                                 nom_Motivo_tipo_cobro = q.nom_Motivo_tipo_cobro,
                                 cr_NumDocumento = q.cr_NumDocumento,
                                 NomAlumno = q.NomAlumno,
                                 EstadoBool = q.cr_estado == "A" ? true : false,
                                 IdUsuario = q.IdUsuario
                             }).ToList();
                }
                */
                return Lista;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<cxc_cobro_Info> get_list_matricula_alumno(int IdEmpresa, int IdSucursal, decimal IdAlumno, DateTime Fecha_ini, DateTime Fecha_fin)
        {
            try
            {
                int IdSucursal_ini = IdSucursal;
                int IdSucursal_fin = IdSucursal == 0 ? 9999 : IdSucursal;
                List<cxc_cobro_Info> Lista = new List<cxc_cobro_Info>();

                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT dbo.cxc_cobro.IdEmpresa, dbo.cxc_cobro.IdSucursal, dbo.cxc_cobro.IdCobro, dbo.cxc_cobro.IdCliente, dbo.tb_persona.pe_nombreCompleto, dbo.cxc_cobro.IdCobro_tipo, dbo.cxc_cobro.cr_fecha, dbo.cxc_cobro.cr_TotalCobro, "
                    + " dbo.cxc_cobro.cr_estado, dbo.tb_sucursal.Su_Descripcion, dbo.cxc_cobro.cr_observacion, ISNULL(ISNULL(dbo.cxc_cobro.cr_Tarjeta, dbo.cxc_cobro.cr_NumDocumento), ba.ba_descripcion) AS cr_NumDocumento, "
                    + " ISNULL(Tipo.nom_Motivo_tipo_cobro, t2.nom_Motivo_tipo_cobro) AS nom_Motivo_tipo_cobro, dbo.cxc_cobro.IdAlumno, tb_persona_1.pe_nombreCompleto AS NomAlumno, dbo.cxc_cobro.cr_Saldo, dbo.cxc_cobro.IdUsuario "
                    + " FROM dbo.tb_persona AS tb_persona_1 INNER JOIN "
                    + " dbo.aca_Alumno ON tb_persona_1.IdPersona = dbo.aca_Alumno.IdPersona RIGHT OUTER JOIN "
                    + " dbo.cxc_cobro INNER JOIN "
                    + " dbo.fa_cliente ON dbo.cxc_cobro.IdEmpresa = dbo.fa_cliente.IdEmpresa AND dbo.cxc_cobro.IdCliente = dbo.fa_cliente.IdCliente INNER JOIN "
                    + " dbo.tb_persona ON dbo.fa_cliente.IdPersona = dbo.tb_persona.IdPersona INNER JOIN "
                    + " dbo.tb_sucursal ON dbo.cxc_cobro.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.cxc_cobro.IdSucursal = dbo.tb_sucursal.IdSucursal ON dbo.aca_Alumno.IdEmpresa = dbo.cxc_cobro.IdEmpresa AND "
                    + " dbo.aca_Alumno.IdAlumno = dbo.cxc_cobro.IdAlumno LEFT OUTER JOIN "
                      + " (SELECT dbo.cxc_cobro_det.IdEmpresa, dbo.cxc_cobro_det.IdSucursal, dbo.cxc_cobro_det.IdCobro, dbo.cxc_cobro_tipo_motivo.nom_Motivo_tipo_cobro "
                       + " FROM dbo.cxc_cobro_det INNER JOIN "
                       + " dbo.cxc_cobro_tipo ON dbo.cxc_cobro_det.IdCobro_tipo = dbo.cxc_cobro_tipo.IdCobro_tipo INNER JOIN "
                       + " dbo.cxc_cobro_tipo_motivo ON dbo.cxc_cobro_tipo.IdMotivo_tipo_cobro = dbo.cxc_cobro_tipo_motivo.IdMotivo_tipo_cobro "
                       + " GROUP BY dbo.cxc_cobro_det.IdEmpresa, dbo.cxc_cobro_det.IdSucursal, dbo.cxc_cobro_det.IdCobro, dbo.cxc_cobro_tipo_motivo.nom_Motivo_tipo_cobro) AS Tipo ON Tipo.IdEmpresa = dbo.cxc_cobro.IdEmpresa AND "
                    + " Tipo.IdSucursal = dbo.cxc_cobro.IdSucursal AND Tipo.IdCobro = dbo.cxc_cobro.IdCobro LEFT OUTER JOIN "
                    + " dbo.cxc_cobro_tipo AS t1 ON dbo.cxc_cobro.IdCobro_tipo = t1.IdCobro_tipo LEFT OUTER JOIN "
                    + " dbo.cxc_cobro_tipo_motivo AS t2 ON t1.IdMotivo_tipo_cobro = t2.IdMotivo_tipo_cobro LEFT OUTER JOIN "
                    + " dbo.ba_Banco_Cuenta AS ba ON dbo.cxc_cobro.IdEmpresa = ba.IdEmpresa AND dbo.cxc_cobro.IdBanco = ba.IdBanco "
                    + " WHERE dbo.cxc_cobro.IdEmpresa = " + IdEmpresa + " and dbo.cxc_cobro.IdSucursal between " + IdSucursal_ini + " and " + IdSucursal_fin
                    + " and dbo.cxc_cobro.cr_fecha between " + "'" + Fecha_ini.ToString("dd/MM/yyyy") + "'" + " and " + "'" + Fecha_fin.Date.ToString("dd/MM/yyyy") + "'"
                    + " and dbo.cxc_cobro.IdAlumno = " + IdAlumno +" Order by  dbo.cxc_cobro.IdCobro desc";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new cxc_cobro_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdSucursal = Convert.ToInt32(reader["IdSucursal"]),
                            IdCobro = Convert.ToDecimal(reader["IdCobro"]),
                            IdCliente = Convert.ToDecimal(reader["IdCliente"]),
                            pe_nombreCompleto = reader["pe_nombreCompleto"].ToString(),
                            IdCobro_tipo = reader["IdCobro_tipo"].ToString(),
                            cr_fecha = Convert.ToDateTime(reader["cr_fecha"]),
                            cr_TotalCobro = Convert.ToDouble(reader["cr_TotalCobro"]),
                            cr_estado = reader["cr_estado"].ToString(),
                            Su_Descripcion = reader["Su_Descripcion"].ToString(),
                            cr_observacion = reader["cr_observacion"].ToString(),
                            nom_Motivo_tipo_cobro = reader["nom_Motivo_tipo_cobro"].ToString(),
                            cr_NumDocumento = reader["cr_NumDocumento"].ToString(),
                            NomAlumno = reader["NomAlumno"].ToString(),
                            EstadoBool = (reader["cr_estado"].ToString() == "A" ? true : false),
                            IdUsuario = reader["IdUsuario"].ToString()
                        });
                    }
                    reader.Close();
                }

                return Lista;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private decimal get_id(int IdEmpesa, int IdSucursal)
        {
            try
            {
                decimal ID = 1;

                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    var lst = from q in Context.cxc_cobro
                              where q.IdEmpresa == IdEmpesa
                              && q.IdSucursal == IdSucursal
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdCobro) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public cxc_cobro_Info get_info(int IdEmpresa, int IdSucursal, decimal IdCobro)
        {
            try
            {
                cxc_cobro_Info info;

                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    var Entity = Context.cxc_cobro.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdCobro == IdCobro).FirstOrDefault();
                    if (Entity == null) return null;
                    info = new cxc_cobro_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdSucursal = Entity.IdSucursal,
                        IdCobro = Entity.IdCobro,
                        cr_Codigo = Entity.cr_Codigo,
                        IdCobro_tipo = Entity.IdCobro_tipo,
                        IdCliente = Entity.IdCliente,
                        IdAlumno = Entity.IdAlumno,
                        cr_TotalCobro = Entity.cr_TotalCobro,
                        cr_fecha = Entity.cr_fecha,
                        cr_fechaDocu = Entity.cr_fechaDocu,
                        cr_fechaCobro = Entity.cr_fechaCobro,
                        cr_observacion = Entity.cr_observacion,
                        cr_Banco = Entity.cr_Banco,
                        cr_cuenta = Entity.cr_cuenta,
                        cr_NumDocumento = Entity.cr_NumDocumento,
                        cr_Tarjeta = Entity.cr_Tarjeta,
                        cr_propietarioCta = Entity.cr_propietarioCta,
                        cr_estado = Entity.cr_estado,
                        cr_recibo = Entity.cr_recibo,
                        cr_es_anticipo = Entity.cr_es_anticipo,
                        IdBanco = Entity.IdBanco,
                        IdCaja = Entity.IdCaja,
                        cr_saldo = Entity.cr_Saldo ?? 0,
                        IdTipoNotaCredito = Entity.IdTipoNotaCredito,
                        cr_ObservacionPantalla = Entity.cr_ObservacionPantalla,
                        IdTarjeta= Entity.IdTarjeta,
                        EstadoBool = (Entity.cr_estado =="A" ? true : false)
                    };
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(cxc_cobro_Info info)
        {
            EntitiesFacturacion dbFac = new EntitiesFacturacion();
            EntitiesCuentasPorCobrar Context_cxc = new EntitiesCuentasPorCobrar();
            try
            {
                #region Variables
                int Secuencia = 1;
                #endregion
       
                #region Cabecera cobro
                cxc_cobro cab = new cxc_cobro
                {
                    IdEmpresa = info.IdEmpresa,
                    IdSucursal = info.IdSucursal,
                    IdCobro = info.IdCobro = get_id(info.IdEmpresa, info.IdSucursal),
                    cr_Codigo = info.cr_Codigo,
                    IdCobro_tipo = info.IdCobro_tipo,
                    IdCliente = info.IdCliente,
                    IdAlumno = info.IdAlumno,
                    cr_TotalCobro = info.cr_TotalCobro,
                    cr_fecha = info.cr_fecha.Date,
                    cr_fechaDocu = info.cr_fechaDocu,
                    cr_fechaCobro = info.cr_fechaCobro,
                    cr_observacion = info.cr_observacion ?? "",

                    cr_Banco = info.cr_Banco,
                    cr_cuenta = info.cr_cuenta,
                    cr_NumDocumento = info.cr_NumDocumento,
                    cr_propietarioCta = info.cr_propietarioCta,
                    cr_estado = "A",
                    cr_es_anticipo = "N",

                    IdBanco = info.IdBanco,
                    IdCaja = info.IdCaja,
                    IdTarjeta = info.IdTarjeta,
                    cr_Tarjeta = info.cr_Tarjeta,
                    cr_Saldo = info.cr_saldo,
                    cr_ObservacionPantalla = info.cr_ObservacionPantalla,
                    Fecha_Transac = DateTime.Now,
                    IdUsuario = info.IdUsuario,
                    IdTipoNotaCredito = info.IdTipoNotaCredito
                };
                Context_cxc.cxc_cobro.Add(cab);
                Context_cxc.SaveChanges();
                #endregion

                #region Detalle cobro
                foreach (var item in info.lst_det)
                {
                    item.IdEmpresa = cab.IdEmpresa;
                    item.IdSucursal = cab.IdSucursal;
                    item.IdCobro = cab.IdCobro;
                    item.IdCliente = cab.IdCliente;
                    item.IdAlumno = cab.IdAlumno;
                    
                    if (item.dc_ValorProntoPago > 0)
                    {
                        var NotaCredito = ArmarNotaCredito(item);
                        if(NotaCredito != null)
                        {
                            if (DataNotaCredito.guardarDB(NotaCredito))
                            {
                                item.IdNotaCredito = NotaCredito.IdNota;
                            }
                        }
                    }

                    cxc_cobro_det det = new cxc_cobro_det
                    {
                        IdEmpresa = item.IdEmpresa,
                        IdSucursal = item.IdSucursal,
                        IdCobro = item.IdCobro,
                        secuencial = item.secuencial = Secuencia++,
                        dc_TipoDocumento = item.dc_TipoDocumento,
                        IdBodega_Cbte = item.IdBodega_Cbte,
                        IdCbte_vta_nota = item.IdCbte_vta_nota,
                        dc_ValorPago = item.dc_ValorPago,
                        IdUsuario = cab.IdUsuario,
                        Fecha_Transac = DateTime.Now,
                        estado = "A",
                        IdCobro_tipo = item.IdCobro_tipo_det,
                        dc_ValorProntoPago = item.dc_ValorProntoPago ?? 0,
                        IdNotaCredito = item.IdNotaCredito
                    };
                    Context_cxc.cxc_cobro_det.Add(det);
                }

                #endregion

                #region Contabilización
                if (info.IdCobro_tipo != "NTCR" && info.IdCobro_tipo != "NTDB" && info.lst_det.Count > 0)
                {
                    if (info.IdCobro_tipo != null)
                    {
                        var TipoCobro = Context_cxc.cxc_cobro_tipo.Where(q => q.IdCobro_tipo == info.IdCobro_tipo).FirstOrDefault();
                        if (TipoCobro != null)
                        {
                            switch (TipoCobro.tc_Tomar_Cta_Cble_De)
                            {
                                case "CAJA":
                                    #region Movimiento de caja
                                    var MovimientoCaja = ArmarMovimientoDeCaja(info);
                                    if (MovimientoCaja != null)
                                    {
                                        if (DataCajaMovimiento.guardarDB(MovimientoCaja))
                                        {
                                            Context_cxc.cxc_cobro_x_ct_cbtecble.Add(new cxc_cobro_x_ct_cbtecble
                                            {
                                                cbr_IdEmpresa = info.IdEmpresa,
                                                cbr_IdSucursal = info.IdSucursal,
                                                cbr_IdCobro = info.IdCobro,
                                                ct_IdEmpresa = MovimientoCaja.IdEmpresa,
                                                ct_IdTipoCbte = MovimientoCaja.IdTipocbte,
                                                ct_IdCbteCble = MovimientoCaja.IdCbteCble,
                                                observacion = ""
                                            });
                                            Context_cxc.SaveChanges();
                                        }
                                    }
                                    #endregion
                                    break;
                                default:
                                    #region Movimiento contable
                                    var MovimientoContable = ArmarMovimientoContable(info);
                                    if (MovimientoContable != null)
                                    {
                                        if (DataContable.guardarDB(MovimientoContable))
                                        {
                                            Context_cxc.cxc_cobro_x_ct_cbtecble.Add(new cxc_cobro_x_ct_cbtecble
                                            {
                                                cbr_IdEmpresa = info.IdEmpresa,
                                                cbr_IdSucursal = info.IdSucursal,
                                                cbr_IdCobro = info.IdCobro,
                                                ct_IdEmpresa = MovimientoContable.IdEmpresa,
                                                ct_IdTipoCbte = MovimientoContable.IdTipoCbte,
                                                ct_IdCbteCble = MovimientoContable.IdCbteCble,
                                                observacion = ""
                                            });
                                            Context_cxc.SaveChanges();
                                        }
                                    }
                                    #endregion
                                    break;
                            }
                        }
                    }
                    else
                    {
                        #region Movimiento contable
                        var MovimientoContable = ArmarMovimientoContable(info);
                        if (MovimientoContable != null)
                        {
                            if (DataContable.guardarDB(MovimientoContable))
                            {
                                Context_cxc.cxc_cobro_x_ct_cbtecble.Add(new cxc_cobro_x_ct_cbtecble
                                {
                                    cbr_IdEmpresa = info.IdEmpresa,
                                    cbr_IdSucursal = info.IdSucursal,
                                    cbr_IdCobro = info.IdCobro,
                                    ct_IdEmpresa = MovimientoContable.IdEmpresa,
                                    ct_IdTipoCbte = MovimientoContable.IdTipoCbte,
                                    ct_IdCbteCble = MovimientoContable.IdCbteCble,
                                    observacion = ""
                                });
                                Context_cxc.SaveChanges();
                            }
                        }
                        #endregion
                    }

                }

                #endregion

                #region Nota de credito por excedente
                if (info.cr_saldo > 0 && info.lst_det.Count == 0)
                {
                    var NotaCredito = ArmarNotaCreditoExcedente(info);
                    if (NotaCredito != null)
                    {
                        if(DataNotaCredito.guardarDB(NotaCredito))
                        {
                            dbFac.fa_notaCreDeb_x_cxc_cobro.Add(new fa_notaCreDeb_x_cxc_cobro
                            {
                                IdEmpresa_cbr= info.IdEmpresa,
                                IdSucursal_cbr = info.IdSucursal,
                                IdCobro_cbr = info.IdCobro,
                                IdEmpresa_nt = info.IdEmpresa,
                                IdSucursal_nt = NotaCredito.IdSucursal,
                                IdBodega_nt = NotaCredito.IdBodega,
                                IdNota_nt = NotaCredito.IdNota
                            });
                            dbFac.SaveChanges();
                        }
                    }
                }
                #endregion

                Context_cxc.SaveChanges();
                Context_cxc.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Context_cxc.Dispose();
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cxc_cobro_Data", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool modificarDB(cxc_cobro_Info info)
        {
            EntitiesCuentasPorCobrar Context_cxc = new EntitiesCuentasPorCobrar();
            EntitiesFacturacion dbFac = new EntitiesFacturacion();
            try
            {
                #region Variables
                int Secuencia = 1;
                #endregion

                #region Cabecera cobro
                var Entity = Context_cxc.cxc_cobro.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdCobro == info.IdCobro).FirstOrDefault();
                if (Entity == null) return false;
                
                Entity.cr_Codigo = info.cr_Codigo;
                Entity.IdCobro_tipo = info.IdCobro_tipo;
                Entity.IdCliente = info.IdCliente;
                Entity.IdAlumno = info.IdAlumno;
                Entity.cr_TotalCobro = info.cr_TotalCobro;
                Entity.cr_fecha = info.cr_fecha.Date;
                Entity.cr_fechaDocu = info.cr_fechaDocu;
                Entity.cr_fechaCobro = info.cr_fechaCobro;
                Entity.cr_observacion = info.cr_observacion;
                Entity.cr_Banco = info.cr_Banco;
                Entity.cr_cuenta = info.cr_cuenta;
                Entity.cr_NumDocumento = info.cr_NumDocumento;
                Entity.cr_Tarjeta = info.cr_Tarjeta;
                Entity.cr_propietarioCta = info.cr_propietarioCta;
                Entity.IdBanco = info.IdBanco;
                Entity.IdCaja = info.IdCaja;
                Entity.cr_Saldo = info.cr_saldo;
                Entity.IdTipoNotaCredito = info.IdTipoNotaCredito;
                Entity.cr_ObservacionPantalla = info.cr_ObservacionPantalla;
                Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                Entity.Fecha_UltMod = DateTime.Now;
                
                #endregion

                #region Detalle cobro
                var cobros_det = Context_cxc.cxc_cobro_det.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdCobro == info.IdCobro).ToList();
                foreach (var item in cobros_det)
                {
                    Context_cxc.cxc_cobro_det.Remove(item);
                }
                foreach (var item in info.lst_det)
                {
                    item.IdEmpresa = info.IdEmpresa;
                    item.IdSucursal = info.IdSucursal;
                    item.IdCobro = info.IdCobro;

                    if (item.dc_ValorProntoPago > 0)
                    {
                        item.IdCliente = info.IdCliente;
                        var NotaCredito = ArmarNotaCredito(item);
                        if (NotaCredito != null)
                        {
                            if (item.IdNotaCredito == null)
                            {
                                if (DataNotaCredito.guardarDB(NotaCredito))
                                {
                                    item.IdNotaCredito = NotaCredito.IdNota;
                                }
                            }else
                            {
                                NotaCredito.IdNota = item.IdNotaCredito ?? 0;
                                if (DataNotaCredito.modificarDB(NotaCredito))
                                {
                                    
                                }
                            }
                            
                        }
                    }
                    cxc_cobro_det det = new cxc_cobro_det
                    {
                        IdEmpresa = item.IdEmpresa = info.IdEmpresa,
                        IdSucursal = item.IdSucursal = info.IdSucursal,
                        IdCobro = item.IdCobro = info.IdCobro,
                        secuencial = item.secuencial = Secuencia++,
                        dc_TipoDocumento = item.dc_TipoDocumento,
                        IdBodega_Cbte = item.IdBodega_Cbte,
                        IdCbte_vta_nota = item.IdCbte_vta_nota,
                        dc_ValorPago = item.dc_ValorPago,
                        IdUsuarioUltMod = info.IdUsuario,
                        Fecha_UltMod = DateTime.Now,
                        estado = "A",
                        IdCobro_tipo = item.IdCobro_tipo_det,
                        dc_ValorProntoPago = item.dc_ValorProntoPago,
                        IdNotaCredito = item.IdNotaCredito
                    };
                    Context_cxc.cxc_cobro_det.Add(det);
                }
                #endregion
                
                if (info.IdCobro_tipo != "NTCR" && info.IdCobro_tipo != "NTDB" && info.lst_det.Count > 0)
                {
                    #region Contabilización
                    if (info.IdCobro_tipo != null)
                    {
                        var TipoCobro = Context_cxc.cxc_cobro_tipo.Where(q => q.IdCobro_tipo == info.IdCobro_tipo).FirstOrDefault();
                        if (TipoCobro != null)
                        {
                            switch (TipoCobro.tc_Tomar_Cta_Cble_De)
                            {
                                case "CAJA":
                                    #region Movimiento de caja
                                    var MovimientoCaja = ArmarMovimientoDeCaja(info);
                                    if (MovimientoCaja != null)
                                    {
                                        var rel = Context_cxc.cxc_cobro_x_ct_cbtecble.Where(q => q.cbr_IdEmpresa == info.IdEmpresa && q.cbr_IdSucursal == info.IdSucursal && q.cbr_IdCobro == info.IdCobro).FirstOrDefault();
                                        if (rel == null)
                                        {
                                            if (DataCajaMovimiento.guardarDB(MovimientoCaja))
                                            {
                                                Context_cxc.cxc_cobro_x_ct_cbtecble.Add(new cxc_cobro_x_ct_cbtecble
                                                {
                                                    cbr_IdEmpresa = info.IdEmpresa,
                                                    cbr_IdSucursal = info.IdSucursal,
                                                    cbr_IdCobro = info.IdCobro,
                                                    ct_IdEmpresa = MovimientoCaja.IdEmpresa,
                                                    ct_IdTipoCbte = MovimientoCaja.IdTipocbte,
                                                    ct_IdCbteCble = MovimientoCaja.IdCbteCble,
                                                    observacion = ""
                                                });
                                                Context_cxc.SaveChanges();
                                            }                                            
                                        }
                                        else
                                        {
                                            MovimientoCaja.IdCbteCble = rel.ct_IdCbteCble;
                                            DataCajaMovimiento.modificarDB(MovimientoCaja);
                                        }
                                    }
                                    #endregion
                                    break;
                                default:
                                    #region Movimiento contable
                                    var MovimientoContable = ArmarMovimientoContable(info);
                                    if (MovimientoContable != null)
                                    {
                                        var rel = Context_cxc.cxc_cobro_x_ct_cbtecble.Where(q => q.cbr_IdEmpresa == info.IdEmpresa && q.cbr_IdSucursal == info.IdSucursal && q.cbr_IdCobro == info.IdCobro).FirstOrDefault();
                                        if (rel == null)
                                        {
                                            if (DataContable.guardarDB(MovimientoContable))
                                            {
                                                Context_cxc.cxc_cobro_x_ct_cbtecble.Add(new cxc_cobro_x_ct_cbtecble
                                                {
                                                    cbr_IdEmpresa = info.IdEmpresa,
                                                    cbr_IdSucursal = info.IdSucursal,
                                                    cbr_IdCobro = info.IdCobro,
                                                    ct_IdEmpresa = MovimientoContable.IdEmpresa,
                                                    ct_IdTipoCbte = MovimientoContable.IdTipoCbte,
                                                    ct_IdCbteCble = MovimientoContable.IdCbteCble,
                                                    observacion = ""
                                                });
                                                Context_cxc.SaveChanges();
                                            }
                                        }
                                        else
                                        {
                                            MovimientoContable.IdCbteCble = rel.ct_IdCbteCble;
                                            DataContable.modificarDB(MovimientoContable);
                                        }
                                    }
                                    #endregion
                                    break;
                            }
                        }
                    }
                    else
                    {
                        #region Movimiento contable
                        var MovimientoContable = ArmarMovimientoContable(info);
                        if (MovimientoContable != null)
                        {
                            var rel = Context_cxc.cxc_cobro_x_ct_cbtecble.Where(q => q.cbr_IdEmpresa == info.IdEmpresa && q.cbr_IdSucursal == info.IdSucursal && q.cbr_IdCobro == info.IdCobro).FirstOrDefault();
                            if (rel == null)
                            {
                                if (DataContable.guardarDB(MovimientoContable))
                                {
                                    Context_cxc.cxc_cobro_x_ct_cbtecble.Add(new cxc_cobro_x_ct_cbtecble
                                    {
                                        cbr_IdEmpresa = info.IdEmpresa,
                                        cbr_IdSucursal = info.IdSucursal,
                                        cbr_IdCobro = info.IdCobro,
                                        ct_IdEmpresa = MovimientoContable.IdEmpresa,
                                        ct_IdTipoCbte = MovimientoContable.IdTipoCbte,
                                        ct_IdCbteCble = MovimientoContable.IdCbteCble,
                                        observacion = ""
                                    });
                                    Context_cxc.SaveChanges();
                                }
                            }
                            else
                            {
                                MovimientoContable.IdCbteCble = rel.ct_IdCbteCble;
                                DataContable.modificarDB(MovimientoContable);
                            }
                        }
                        #endregion
    
                    }

                    #endregion
    
                }

                #region Nota de credito por excedente
                if (info.cr_saldo > 0)
                {
                    var NotaCredito = ArmarNotaCreditoExcedente(info);
                    if (NotaCredito != null)
                    {
                        var relNCCobro = dbFac.fa_notaCreDeb_x_cxc_cobro.Where(q => q.IdEmpresa_cbr == info.IdEmpresa && q.IdSucursal_cbr == info.IdSucursal && q.IdCobro_cbr == info.IdCobro).FirstOrDefault();
                        if (relNCCobro == null)
                        {
                            if (DataNotaCredito.guardarDB(NotaCredito))
                            {
                                dbFac.fa_notaCreDeb_x_cxc_cobro.Add(new fa_notaCreDeb_x_cxc_cobro
                                {
                                    IdEmpresa_cbr = info.IdEmpresa,
                                    IdSucursal_cbr = info.IdSucursal,
                                    IdCobro_cbr = info.IdCobro,
                                    IdEmpresa_nt = info.IdEmpresa,
                                    IdSucursal_nt = NotaCredito.IdSucursal,
                                    IdBodega_nt = NotaCredito.IdBodega,
                                    IdNota_nt = NotaCredito.IdNota
                                });
                                dbFac.SaveChanges();
                            }
                        }else
                        {
                            NotaCredito.IdNota = relNCCobro.IdNota_nt;
                            DataNotaCredito.modificarDB(NotaCredito);
                        }
                        
                    }
                }
                #endregion
                
                Context_cxc.SaveChanges();
                Context_cxc.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                Context_cxc.Dispose();
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cxc_cobro_Data", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool anularDB(cxc_cobro_Info info)
        {
            try
            {
                EntitiesFacturacion dbFac = new EntitiesFacturacion();
                EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar();


                var Entity = Context.cxc_cobro.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdCobro == info.IdCobro).FirstOrDefault();
                if (Entity == null) return false;

                if (Entity.cr_estado == "I") return true;

                Entity.cr_estado = "I";
                var cobros_det = Context.cxc_cobro_det.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdCobro == info.IdCobro).ToList();
                foreach (var item in cobros_det)
                {
                    DataNotaCredito.anularDB(new fa_notaCreDeb_Info
                    {
                        IdEmpresa = item.IdEmpresa,
                        IdSucursal = item.IdSucursal,
                        IdBodega = item.IdBodega_Cbte ?? 0,
                        IdNota = item.IdNotaCredito ?? 0,
                        IdUsuarioUltAnu = info.IdUsuarioUltAnu,
                        MotiAnula = info.MotiAnula
                    });
                    item.estado = "I";
                }

                var relacion = Context.cxc_cobro_x_ct_cbtecble.Where(q => q.cbr_IdEmpresa == info.IdEmpresa && q.cbr_IdSucursal == info.IdSucursal && q.cbr_IdCobro == info.IdCobro).FirstOrDefault();
                if (relacion != null)
                {
                    ct_cbtecble_Data odata_ct = new ct_cbtecble_Data();
                    if (odata_ct.anularDB(new ct_cbtecble_Info
                    {
                        IdEmpresa = relacion.ct_IdEmpresa,
                        IdTipoCbte = relacion.ct_IdTipoCbte,
                        IdCbteCble = relacion.ct_IdCbteCble,
                        IdUsuario = info.IdUsuarioUltAnu,
                        IdUsuarioAnu = info.IdUsuarioUltAnu
                    }))
                    {
                        if (Entity.IdCobro_tipo != null)
                        {
                            var cobro_tipo = Context.cxc_cobro_tipo.Where(q => q.IdCobro_tipo == Entity.IdCobro_tipo).FirstOrDefault();

                            if (cobro_tipo.tc_Tomar_Cta_Cble_De == cl_enumeradores.eTipoCobroTomaCuentaDe.CAJA.ToString())
                            {
                                caj_Caja_Movimiento_Data odata_caj = new caj_Caja_Movimiento_Data();
                                odata_caj.anularDB(new Info.Caja.caj_Caja_Movimiento_Info
                                {
                                    IdEmpresa = relacion.ct_IdEmpresa,
                                    IdTipocbte = relacion.ct_IdTipoCbte,
                                    IdCbteCble = relacion.ct_IdCbteCble,
                                    IdUsuario = info.IdUsuarioUltAnu,
                                    IdUsuario_Anu = info.IdUsuarioUltAnu
                                });
                            }
                        }
                    }
                }


                var NCPorCobro = dbFac.fa_notaCreDeb_x_cxc_cobro.Where(q => q.IdEmpresa_cbr == info.IdEmpresa && q.IdSucursal_cbr == info.IdSucursal && q.IdCobro_cbr == info.IdCobro).FirstOrDefault();
                if (NCPorCobro != null)
                {
                    if (Entity.cr_Saldo > 0)
                    {
                        DataNotaCredito.anularDB(new fa_notaCreDeb_Info
                        {
                            IdEmpresa = NCPorCobro.IdEmpresa_nt,
                            IdSucursal = NCPorCobro.IdSucursal_nt,
                            IdBodega = NCPorCobro.IdBodega_nt,
                            IdNota = NCPorCobro.IdNota_nt,
                            IdUsuarioUltAnu = info.IdUsuarioUltAnu,
                            MotiAnula = info.MotiAnula
                        });
                    }
                }

                Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                Entity.Fecha_UltAnu = DateTime.Now;
                Context.SaveChanges();


                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public caj_Caja_Movimiento_Info ArmarMovimientoDeCaja(cxc_cobro_Info info)
        {
            try
            {
                EntitiesCaja dbCaja = new EntitiesCaja();
                EntitiesCuentasPorCobrar dbCxc = new EntitiesCuentasPorCobrar();
                EntitiesFacturacion dbFac = new EntitiesFacturacion();
                EntitiesAcademico dbAca = new EntitiesAcademico();

                var paramCaja = dbCaja.caj_parametro.Where(q => q.IdEmpresa == info.IdEmpresa).FirstOrDefault();
                if (paramCaja == null)
                    return null;

                var paramCxc = dbCxc.cxc_Parametro.Where(q => q.IdEmpresa == info.IdEmpresa).FirstOrDefault();
                if (paramCxc == null)
                    return null;

                var cliente = dbFac.vwfa_cliente_consulta.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdCliente == info.IdCliente).FirstOrDefault();
                if (cliente == null)
                    return null;

                var alumno = dbAca.vwaca_Alumno.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno).FirstOrDefault();
                if (alumno == null)
                    return null;

                var caja = dbCaja.caj_Caja.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdCaja == info.IdCaja).FirstOrDefault();
                if (caja == null)
                    return null;

                caj_Caja_Movimiento_Info retorno = new caj_Caja_Movimiento_Info
                {
                    IdEmpresa = info.IdEmpresa,
                    IdTipocbte = paramCaja.IdTipoCbteCble_MoviCaja_Ing,
                    IdCbteCble = 0,
                    IdTipoMovi = paramCxc.pa_IdTipoMoviCaja_x_Cobros_x_cliente,
                    cm_fecha = info.cr_fecha,
                    cm_valor = info.cr_TotalCobro,
                    cm_Signo = "+",
                    cm_observacion = "COBRO #" + info.IdCobro + " " + info.cr_observacion + " CLIENTE: " + cliente.pe_nombreCompleto + " ALUMNO: "+alumno.pe_nombreCompleto,
                    Estado = "A",
                    IdPeriodo = Convert.ToInt32(info.cr_fecha.ToString("yyyyMM")),
                    IdCaja = info.IdCaja,
                    IdTipo_Persona = cl_enumeradores.eTipoPersona.ALUMNO.ToString(),
                    IdEntidad = alumno.IdAlumno,
                    IdPersona = alumno.IdPersona,

                    IdUsuario = info.IdUsuario,
                    Fecha_Transac = DateTime.Now,
                    info_caj_Caja_Movimiento_det = new caj_Caja_Movimiento_det_Info
                    {
                        cr_Valor = info.cr_TotalCobro,
                        IdCobro_tipo = info.IdCobro_tipo
                    },
                    lst_ct_cbtecble_det = new List<ct_cbtecble_det_Info>()
                };
                int Secuencia = 1;

                retorno.lst_ct_cbtecble_det.Add(new ct_cbtecble_det_Info
                {
                    secuencia = Secuencia++,
                    IdCtaCble = caja.IdCtaCble,
                    dc_Valor = Math.Round(info.cr_TotalCobro, 2, MidpointRounding.AwayFromZero),
                    dc_Observacion = "Cuenta contable de caja "+caja.ca_Descripcion
                });

                foreach (var item in info.lst_det)
                {
                    if (item.dc_TipoDocumento == "FACT")
                    {
                        var infoFact = dbFac.vwfa_factura_ParaContabilizarAcademico.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdSucursal == item.IdSucursal && q.IdBodega == item.IdBodega_Cbte && q.IdCbteVta == item.IdCbte_vta_nota).FirstOrDefault();
                        if (infoFact != null)
                        {
                            retorno.lst_ct_cbtecble_det.Add(new ct_cbtecble_det_Info
                            {
                                secuencia = Secuencia++,
                                IdCtaCble = infoFact.IdCtaCbleDebe,
                                dc_Valor = Math.Round(item.dc_ValorPago,2,MidpointRounding.AwayFromZero) *-1,
                                dc_Observacion = infoFact.vt_NumFactura
                            });
                        }
                    }else
                    if (item.dc_TipoDocumento == "NTDB")
                    {
                        var infoND = dbFac.spfa_notaCreDeb_ParaContabilizarAcademico(item.IdEmpresa, item.IdSucursal, item.IdBodega_Cbte, item.IdCbte_vta_nota).FirstOrDefault();
                        if (infoND != null)
                        {
                            retorno.lst_ct_cbtecble_det.Add(new ct_cbtecble_det_Info
                            {
                                secuencia = Secuencia++,
                                IdCtaCble = infoND.IdCtaCbleDebe,
                                dc_Valor = Math.Round(item.dc_ValorPago, 2, MidpointRounding.AwayFromZero) * -1,
                                dc_Observacion = infoND.NumNota_Impresa
                            });
                        }
                    }
                }

                if (retorno.lst_ct_cbtecble_det.Count == 0)
                    return null;

                if (Math.Round(retorno.lst_ct_cbtecble_det.Sum(q => q.dc_Valor), 2, MidpointRounding.AwayFromZero) != 0)
                    return null;

                return retorno;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ct_cbtecble_Info ArmarMovimientoContable(cxc_cobro_Info info)
        {
            try
            {
                EntitiesCuentasPorCobrar dbCxc = new EntitiesCuentasPorCobrar();
                EntitiesFacturacion dbFac = new EntitiesFacturacion();
                EntitiesAcademico dbAca = new EntitiesAcademico();

                var paramCxc = dbCxc.cxc_Parametro.Where(q => q.IdEmpresa == info.IdEmpresa).FirstOrDefault();
                if (paramCxc == null)
                    return null;

                var cliente = dbFac.vwfa_cliente_consulta.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdCliente == info.IdCliente).FirstOrDefault();
                if (cliente == null)
                    return null;

                var alumno = dbAca.vwaca_Alumno.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno).FirstOrDefault();
                if (alumno == null)
                    return null;

                ct_cbtecble_Info retorno = new ct_cbtecble_Info
                {
                    IdEmpresa = info.IdEmpresa,
                    IdTipoCbte = paramCxc.pa_IdTipoCbteCble_CxC,
                    cb_Fecha = info.cr_fecha,
                    cb_Valor = Math.Round(info.lst_det.Sum(q=> q.dc_ValorPago),2,MidpointRounding.AwayFromZero),
                    IdPeriodo = Convert.ToInt32(info.cr_fecha.ToString("yyyyMM")),
                    CodCbteCble = "CXC-"+info.IdCobro.ToString(),
                    IdSucursal = info.IdSucursal,
                    cb_Observacion = "COBRO #" + info.IdCobro + " " + info.cr_observacion + " CLIENTE: " + cliente.pe_nombreCompleto + " ALUMNO: " + alumno.pe_nombreCompleto,
                    IdUsuario = info.IdUsuario,
                    IdUsuarioUltModi = info.IdUsuario,
                    lst_ct_cbtecble_det = new List<ct_cbtecble_det_Info>()
                };

                int Secuencia = 1;

                foreach (var item in info.lst_det)
                {
                    if (item.dc_TipoDocumento == "FACT")
                    {
                        var TipoCobroCtaFACT = dbCxc.cxc_cobro_tipo_Param_conta_x_sucursal.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdCobro_tipo == item.IdCobro_tipo_det).FirstOrDefault();
                        if (TipoCobroCtaFACT != null)
                        {
                            retorno.lst_ct_cbtecble_det.Add(new ct_cbtecble_det_Info
                            {
                                secuencia = Secuencia++,
                                IdCtaCble = TipoCobroCtaFACT.IdCtaCble,
                                dc_Valor = Math.Round(item.dc_ValorPago, 2, MidpointRounding.AwayFromZero),
                                dc_Observacion = TipoCobroCtaFACT.IdCobro_tipo
                            });
                        }
                        var infoFact = dbFac.vwfa_factura_ParaContabilizarAcademico.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdSucursal == item.IdSucursal && q.IdBodega == item.IdBodega_Cbte && q.IdCbteVta == item.IdCbte_vta_nota).FirstOrDefault();
                        if (infoFact != null)
                        {
                            retorno.lst_ct_cbtecble_det.Add(new ct_cbtecble_det_Info
                            {
                                secuencia = Secuencia++,
                                IdCtaCble = infoFact.IdCtaCbleDebe,
                                dc_Valor = Math.Round(item.dc_ValorPago, 2, MidpointRounding.AwayFromZero) * -1,
                                dc_Observacion = infoFact.vt_NumFactura
                            });
                        }
                    }
                    else
                    if (item.dc_TipoDocumento == "NTDB")
                    {
                        var TipoCobroCtaNTDB = dbCxc.cxc_cobro_tipo_Param_conta_x_sucursal.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdCobro_tipo == item.IdCobro_tipo_det).FirstOrDefault();
                        if (TipoCobroCtaNTDB != null)
                        {
                            retorno.lst_ct_cbtecble_det.Add(new ct_cbtecble_det_Info
                            {
                                secuencia = Secuencia++,
                                IdCtaCble = TipoCobroCtaNTDB.IdCtaCble,
                                dc_Valor = Math.Round(item.dc_ValorPago, 2, MidpointRounding.AwayFromZero),
                                dc_Observacion = TipoCobroCtaNTDB.IdCobro_tipo
                            });
                        }
                        var infoND = dbFac.spfa_notaCreDeb_ParaContabilizarAcademico(item.IdEmpresa, item.IdSucursal, item.IdBodega_Cbte, item.IdCbte_vta_nota).FirstOrDefault();
                        if (infoND != null)
                        {
                            retorno.lst_ct_cbtecble_det.Add(new ct_cbtecble_det_Info
                            {
                                secuencia = Secuencia++,
                                IdCtaCble = infoND.IdCtaCbleDebe,
                                dc_Valor = Math.Round(item.dc_ValorPago, 2, MidpointRounding.AwayFromZero) * -1,
                                dc_Observacion = infoND.NumNota_Impresa
                            });
                        }
                    }
                }

                if (retorno.lst_ct_cbtecble_det.Count == 0)
                    return null;

                if (Math.Round(retorno.lst_ct_cbtecble_det.Sum(q => q.dc_Valor), 2, MidpointRounding.AwayFromZero) != 0)
                    return null;

                return retorno;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public fa_notaCreDeb_Info ArmarNotaCredito(cxc_cobro_det_Info info)
        {
            try
            {
                EntitiesCuentasPorCobrar dbCxc = new EntitiesCuentasPorCobrar();
                EntitiesFacturacion dbFac = new EntitiesFacturacion();
                EntitiesAcademico dbACA = new EntitiesAcademico();

                var paramCxc = dbCxc.cxc_Parametro.Where(q => q.IdEmpresa == info.IdEmpresa).FirstOrDefault();
                if (paramCxc == null)
                    return null;

                if (info.dc_TipoDocumento == "NTDB" && paramCxc.IdTipoNotaProntoPago == null)
                    return null;
                
                if(info.dc_TipoDocumento == "FACT")
                {
                    var Fac = dbFac.fa_factura_resumen.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdBodega == info.IdBodega_Cbte && q.IdCbteVta == info.IdCbte_vta_nota).FirstOrDefault();
                    if (Fac == null)
                        return null;

                    var Fact = dbFac.fa_factura.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdBodega == info.IdBodega_Cbte && q.IdCbteVta == info.IdCbte_vta_nota).FirstOrDefault();
                    if (Fact == null)
                        return null;

                    info.IdPuntoVta = Fact.IdPuntoVta;

                    var Plantilla = dbACA.aca_Plantilla.Where(q => q.IdEmpresa == Fac.IdEmpresa && q.IdAnio == Fac.IdAnio && q.IdPlantilla == Fac.IdPlantilla).FirstOrDefault();
                    if (Plantilla == null)
                        return null;

                    if (Plantilla.AplicaParaTodo == true)
                    {
                        paramCxc.IdTipoNotaProntoPago = Plantilla.IdTipoNota;
                    }else
                    {
                        var PlantillaDet = dbACA.aca_Plantilla_Rubro.Where(q => q.IdEmpresa == Fac.IdEmpresa && q.IdPlantilla == Fac.IdPlantilla &&  q.IdRubro == Fac.IdRubro).FirstOrDefault();
                        if (PlantillaDet != null)
                        {
                            paramCxc.IdTipoNotaProntoPago = PlantillaDet.IdTipoNota_descuentoDet ?? Plantilla.IdTipoNota;
                        }
                    }

                    
                }

                var TipoNota = dbFac.fa_TipoNota.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoNota == paramCxc.IdTipoNotaProntoPago).FirstOrDefault();
                if (TipoNota == null)
                    return null;

                var PuntoVta = dbFac.vwfa_PuntoVta.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdPuntoVta == info.IdPuntoVta).FirstOrDefault();
                if (PuntoVta == null)
                    return null;

                PuntoVta = dbFac.vwfa_PuntoVta.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.cod_PuntoVta == PuntoVta.cod_PuntoVta && q.codDocumentoTipo == "NTCR").FirstOrDefault();
                if (PuntoVta == null)
                    return null;

                if (TipoNota.IdProducto == null)
                    return null;

                fa_notaCreDeb_Info Retorno = new fa_notaCreDeb_Info
                {
                    IdEmpresa = info.IdEmpresa,
                    IdSucursal = info.IdSucursal,
                    IdBodega = info.IdBodega_Cbte ?? 0,
                    IdPuntoVta = PuntoVta.IdPuntoVta,
                    CodNota = "ProntoPago",
                    CreDeb = "C",
                    CodDocumentoTipo = "NTCR",
                    Serie1 = PuntoVta.Su_CodigoEstablecimiento,
                    Serie2 = PuntoVta.cod_PuntoVta,
                    NumAutorizacion = null,
                    NumNota_Impresa = null,
                    Fecha_Autorizacion = null,
                    IdCliente = info.IdCliente,
                    IdAlumno = info.IdAlumno,
                    no_fecha = DateTime.Now.Date,
                    no_fecha_venc = DateTime.Now.Date,
                    IdTipoNota = paramCxc.IdTipoNotaProntoPago ?? 0,
                    sc_observacion = "NC Por pronto pago de "+info.dc_TipoDocumento+" "+info.Observacion,
                    NaturalezaNota = "SRI",
                    IdCtaCble_TipoNota = TipoNota.IdCtaCble,
                    IdCobro_tipo = "NTCR",
                    IdUsuario = info.IdUsuario,
                    IdUsuarioUltMod = info.IdUsuarioUltMod,
                    info_resumen = new fa_notaCreDeb_resumen_Info(),
                    lst_det = new List<fa_notaCreDeb_det_Info>(),
                    lst_cruce = new List<fa_notaCreDeb_x_fa_factura_NotaDeb_Info>()
                };
                Retorno.lst_det.Add(new fa_notaCreDeb_det_Info
                {
                    IdProducto = TipoNota.IdProducto ?? 0,
                    sc_cantidad = 1,
                    sc_cantidad_factura = null,
                    sc_Precio = info.dc_ValorProntoPago ?? 0,
                    sc_precioFinal = info.dc_ValorProntoPago ?? 0,
                    sc_descUni = 0,
                    sc_PordescUni = 0,
                    vt_por_iva = 0,
                    IdCod_Impuesto_Iva = "IVA0",
                    sc_iva = 0,
                    sc_subtotal = info.dc_ValorProntoPago ?? 0,
                    sc_total = info.dc_ValorProntoPago ?? 0
                });

                Retorno.lst_cruce.Add(new fa_notaCreDeb_x_fa_factura_NotaDeb_Info
                {
                    IdEmpresa_fac_nd_doc_mod = info.IdEmpresa,
                    IdSucursal_fac_nd_doc_mod = info.IdSucursal,
                    IdBodega_fac_nd_doc_mod = info.IdBodega_Cbte ?? 0,
                    IdCbteVta_fac_nd_doc_mod = info.IdCbte_vta_nota,
                    vt_tipoDoc = info.dc_TipoDocumento,
                    Valor_Aplicado = info.dc_ValorProntoPago ?? 0,
                    fecha_cruce = DateTime.Now.Date
                });

                Retorno.info_resumen = new fa_notaCreDeb_resumen_Info
                {
                    SubtotalIVASinDscto = 0,
                    SubtotalSinIVASinDscto = Convert.ToDecimal(info.dc_ValorProntoPago ?? 0),
                    SubtotalSinDscto = Convert.ToDecimal(info.dc_ValorProntoPago ?? 0),
                    Descuento = 0,
                    SubtotalIVAConDscto = 0,
                    SubtotalSinIVAConDscto = Convert.ToDecimal(info.dc_ValorProntoPago ?? 0),
                    SubtotalConDscto = Convert.ToDecimal(info.dc_ValorProntoPago ?? 0),
                    IdCod_Impuesto_IVA = "IVA0",
                    ValorIVA = 0,
                    Total = Convert.ToDecimal(info.dc_ValorProntoPago ?? 0),
                    PorIva = 0
                };

                return Retorno;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public fa_notaCreDeb_Info ArmarNotaCreditoExcedente(cxc_cobro_Info info)
        {
            try
            {
                EntitiesCuentasPorCobrar dbCxc = new EntitiesCuentasPorCobrar();
                EntitiesFacturacion dbFac = new EntitiesFacturacion();
                EntitiesAcademico dbACA = new EntitiesAcademico();

                var paramCxc = dbCxc.cxc_Parametro.Where(q => q.IdEmpresa == info.IdEmpresa).FirstOrDefault();
                if (paramCxc == null)
                    return null;
                
                var TipoNota = dbFac.fa_TipoNota.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoNota == info.IdTipoNotaCredito).FirstOrDefault();
                if (TipoNota == null)
                    return null;

                var PuntoVta = dbFac.vwfa_PuntoVta.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.codDocumentoTipo == "NTCR").FirstOrDefault();
                if (PuntoVta == null)
                    return null;

                PuntoVta = dbFac.vwfa_PuntoVta.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.cod_PuntoVta == PuntoVta.cod_PuntoVta && q.codDocumentoTipo == "NTCR").FirstOrDefault();
                if (PuntoVta == null)
                    return null;

                if (TipoNota.IdProducto == null)
                    return null;

                var Alumno = dbACA.vwaca_Alumno.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno).FirstOrDefault();
                if (Alumno == null)
                    return null;

                fa_notaCreDeb_Info Retorno = new fa_notaCreDeb_Info
                {
                    IdEmpresa = info.IdEmpresa,
                    IdSucursal = info.IdSucursal,
                    IdBodega = PuntoVta.IdBodega,
                    IdPuntoVta = PuntoVta.IdPuntoVta,
                    CodNota = "ProntoPago",
                    CreDeb = "C",
                    CodDocumentoTipo = "NTCR",
                    Serie1 = null,
                    Serie2 = null,
                    NumAutorizacion = null,
                    NumNota_Impresa = null,
                    Fecha_Autorizacion = null,
                    IdCliente = info.IdCliente,
                    IdAlumno = info.IdAlumno,
                    no_fecha = info.cr_fecha,
                    no_fecha_venc = info.cr_fecha,
                    IdTipoNota = info.IdTipoNotaCredito ?? 0,
                    sc_observacion = "NC Por excedente COBRO # " + info.IdCobro.ToString() + " ALUMNO: "+Alumno.pe_nombreCompleto + " "+info.cr_observacion,
                    NaturalezaNota = "INT",
                    IdCtaCble_TipoNota = TipoNota.IdCtaCble,
                    IdCobro_tipo = info.IdCobro_tipo,
                    IdUsuario = info.IdUsuario,
                    IdUsuarioUltMod = info.IdUsuarioUltMod,
                    info_resumen = new fa_notaCreDeb_resumen_Info(),
                    lst_det = new List<fa_notaCreDeb_det_Info>(),
                    lst_cruce = new List<fa_notaCreDeb_x_fa_factura_NotaDeb_Info>()
                };
                Retorno.lst_det.Add(new fa_notaCreDeb_det_Info
                {
                    IdProducto = TipoNota.IdProducto ?? 0,
                    sc_cantidad = 1,
                    sc_cantidad_factura = null,
                    sc_Precio = info.cr_saldo,
                    sc_precioFinal = info.cr_saldo,
                    sc_descUni = 0,
                    sc_PordescUni = 0,
                    vt_por_iva = 0,
                    IdCod_Impuesto_Iva = "IVA0",
                    sc_iva = 0,
                    sc_subtotal = info.cr_saldo,
                    sc_total = info.cr_saldo
                });
                
                Retorno.info_resumen = new fa_notaCreDeb_resumen_Info
                {
                    SubtotalIVASinDscto = 0,
                    SubtotalSinIVASinDscto = Convert.ToDecimal(info.cr_saldo),
                    SubtotalSinDscto = Convert.ToDecimal(info.cr_saldo),
                    Descuento = 0,
                    SubtotalIVAConDscto = 0,
                    SubtotalSinIVAConDscto = Convert.ToDecimal(info.cr_saldo),
                    SubtotalConDscto = Convert.ToDecimal(info.cr_saldo),
                    IdCod_Impuesto_IVA = "IVA0",
                    ValorIVA = 0,
                    Total = Convert.ToDecimal(info.cr_saldo),
                    PorIva = 0
                };

                return Retorno;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<cxc_cobro_Info> get_list_para_retencion(int IdEmpresa, int IdSucursal, DateTime fecha_ini, DateTime fecha_fin, bool TieneRetencion)
        {
            try
            {
                List<cxc_cobro_Info> Lista;
                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    Lista = (from q in Context.vwcxc_cobro_para_retencion
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && fecha_ini <= q.vt_fecha && q.vt_fecha <= fecha_fin
                             && q.TieneRetencion == TieneRetencion
                             select new cxc_cobro_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdCbteVta = q.IdCbteVta,
                                 vt_tipoDoc = q.vt_tipoDoc,
                                 pe_nombreCompleto = q.Nombres,
                                 cr_fecha = q.vt_fecha,
                                 vt_NumFactura = q.vt_NumFactura,
                                 cr_observacion = q.vt_Observacion,
                                 vt_fecha = q.vt_fecha,
                                 vt_fech_venc = q.vt_fech_venc,
                                 vt_Subtotal = q.vt_Subtotal,
                                 vt_Iva = q.vt_iva,
                                 vt_Total = q.vt_total,
                                 Su_Descripcion = q.Su_Descripcion,
                                 
                                 
                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public cxc_cobro_Info get_info_para_retencion(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta, string vt_tipoDoc)
        {
            try
            {
                cxc_cobro_Info info = new cxc_cobro_Info();
                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    vwcxc_cobro_para_retencion Entity = Context.vwcxc_cobro_para_retencion.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega && q.IdCbteVta == IdCbteVta && q.vt_tipoDoc == vt_tipoDoc);
                    if (Entity == null) return null;
                    info = new cxc_cobro_Info
                    {

                        IdEmpresa = Entity.IdEmpresa,
                        IdSucursal = Entity.IdSucursal,
                        IdBodega = Entity.IdBodega,
                        IdCbteVta = Entity.IdCbteVta,
                        vt_Iva = Convert.ToDouble(Entity.vt_iva),
                        vt_Total =  Convert.ToDouble(Entity.vt_total),
                        pe_nombreCompleto = Entity.Nombres,
                        cr_fecha = Entity.vt_fecha,
                        vt_NumFactura = Entity.vt_NumFactura,
                        cr_observacion = Entity.vt_Observacion,
                        vt_Subtotal = Convert.ToDouble(Entity.vt_Subtotal),
                        IdCliente = Entity.IdCliente,
                        IdEntidad = Entity.IdCliente,
                        vt_tipoDoc = Entity.vt_tipoDoc
                                               
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string ValidarSaldoDocumento(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta, string CodDocumentoTipo, double ValorCobrado, double ValorAnterior)
        {
            try
            {
                string Retorno = string.Empty;

                using (EntitiesCuentasPorCobrar db = new EntitiesCuentasPorCobrar())
                {
                    var Documento = db.vwcxc_cartera_x_cobrar.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega && q.IdComprobante == IdCbteVta && q.vt_tipoDoc == CodDocumentoTipo).FirstOrDefault();
                    if (Documento != null)
                    {
                        if (Math.Round((Documento.Saldo ?? 0) + ValorAnterior - ValorCobrado, 2, MidpointRounding.AwayFromZero) < 0)
                        {
                            Retorno += ("Está intentando aplicar " + ValorCobrado.ToString("c2") + " al documento " + Documento.vt_NunDocumento + " cuyo saldo es de " + (Documento.Saldo ?? 0 + ValorAnterior).ToString("c2"));
                        }
                    }
                }

                return Retorno;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<cxc_cobro_Info> get_list_deuda(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                List<cxc_cobro_Info> Lista;
                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    Lista = (from q in Context.vwcxc_cartera_x_cobrar
                             where q.IdEmpresa == IdEmpresa
                             && q.IdAlumno == IdAlumno
                             select new cxc_cobro_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 vt_tipoDoc = q.vt_tipoDoc,
                                 vt_NumFactura = q.vt_NunDocumento,
                                 pe_nombreCompleto = q.NomCliente,
                                 cr_fecha = q.vt_fecha,
                                 vt_fecha = q.vt_fecha,
                                 vt_fech_venc = q.vt_fech_venc,
                                 Su_Descripcion = q.Su_Descripcion,
                                 IdAlumno = q.IdAlumno,
                                 cr_saldo = q.Saldo??0,  

                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<cxc_cobro_Info> get_list_aplicacion_masiva(int IdEmpresa)
        {
            try
            {
                List<cxc_cobro_Info> Lista;
                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    Lista = Context.vwcxc_cartera_x_cobrar.Where(q => q.IdEmpresa == IdEmpresa).GroupBy(q => new { q.IdEmpresa, q.IdAlumno, q.NomCliente }).Select(q => new cxc_cobro_Info
                    {
                        IdEmpresa = q.Key.IdEmpresa,
                        IdAlumno = q.Key.IdAlumno,
                        NomAlumno = q.Key.NomCliente,
                        cr_saldo = q.Sum(h => h.Saldo) ?? 0
                    }).ToList();

                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public double GetSaldoAlumno(int IdEmpresa, decimal IdAlumno, bool ConDescuento)
        {
            try
            {
                EntitiesCuentasPorCobrar dbCxc = new EntitiesCuentasPorCobrar();
                EntitiesFacturacion dbFac = new EntitiesFacturacion();
                double SaldoCxc = 0;
                if (ConDescuento)
                {
                    SaldoCxc = dbCxc.vwcxc_cartera_x_cobrar.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno).Sum(q => q.Saldo - (q.vt_total - q.ValorProntoPago)) ?? 0;
                }else
                    SaldoCxc = dbCxc.vwcxc_cartera_x_cobrar.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno).Sum(q => q.Saldo) ?? 0;

                double Saldo = Convert.ToDouble(SaldoCxc);
                return Saldo;                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ValidarMostrarBotonModificar(int IdEmpresa, int IdSucursal, decimal IdCobro)
        {
            try
            {
                int cont = 0;
                EntitiesCaja db = new EntitiesCaja();
                EntitiesBanco dbb = new EntitiesBanco();
                EntitiesCuentasPorCobrar dbcxc = new EntitiesCuentasPorCobrar();

                var Rel = dbcxc.cxc_cobro_x_ct_cbtecble.Where(q => q.cbr_IdEmpresa == IdEmpresa && q.cbr_IdSucursal == IdSucursal && q.cbr_IdCobro == IdCobro).FirstOrDefault();
                if (Rel != null)
                {
                    cont = db.cp_conciliacion_Caja_det_Ing_Caja.Where(q => q.IdEmpresa_movcaj == IdEmpresa
                    && q.IdTipocbte_movcaj == Rel.ct_IdTipoCbte && q.IdCbteCble_movcaj == Rel.ct_IdCbteCble).Count();

                    if (cont != 0)
                        return false;

                    cont = dbb.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.Where(q => q.mcj_IdEmpresa == IdEmpresa
                    && q.mcj_IdTipocbte == Rel.ct_IdTipoCbte && q.mcj_IdCbteCble == Rel.ct_IdCbteCble).Count();

                    if (cont != 0)
                        return false;
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Contabilizar(int IdEmpresa, int IdSucursal, decimal IdCobro)
        {
            try
            {
                EntitiesCuentasPorCobrar Context_cxc = new EntitiesCuentasPorCobrar();

                var info = get_info(IdEmpresa, IdSucursal, IdCobro);
                if (info == null && info.cr_estado == "I")
                    return false;

                info.lst_det = DataDet.get_list(IdEmpresa, IdSucursal, IdCobro);

                if (info.IdCobro_tipo != "NTCR" && info.IdCobro_tipo != "NTDB" && info.lst_det.Count > 0)
                {
                    #region Contabilización
                    if (info.IdCobro_tipo != null)
                    {
                        var TipoCobro = Context_cxc.cxc_cobro_tipo.Where(q => q.IdCobro_tipo == info.IdCobro_tipo).FirstOrDefault();
                        if (TipoCobro != null)
                        {
                            switch (TipoCobro.tc_Tomar_Cta_Cble_De)
                            {
                                case "CAJA":
                                    #region Movimiento de caja
                                    var MovimientoCaja = ArmarMovimientoDeCaja(info);
                                    if (MovimientoCaja != null)
                                    {
                                        var rel = Context_cxc.cxc_cobro_x_ct_cbtecble.Where(q => q.cbr_IdEmpresa == info.IdEmpresa && q.cbr_IdSucursal == info.IdSucursal && q.cbr_IdCobro == info.IdCobro).FirstOrDefault();
                                        if (rel == null)
                                        {
                                            if (DataCajaMovimiento.guardarDB(MovimientoCaja))
                                            {
                                                Context_cxc.cxc_cobro_x_ct_cbtecble.Add(new cxc_cobro_x_ct_cbtecble
                                                {
                                                    cbr_IdEmpresa = info.IdEmpresa,
                                                    cbr_IdSucursal = info.IdSucursal,
                                                    cbr_IdCobro = info.IdCobro,
                                                    ct_IdEmpresa = MovimientoCaja.IdEmpresa,
                                                    ct_IdTipoCbte = MovimientoCaja.IdTipocbte,
                                                    ct_IdCbteCble = MovimientoCaja.IdCbteCble,
                                                    observacion = ""
                                                });
                                                Context_cxc.SaveChanges();
                                            }
                                        }
                                        else
                                        {
                                            MovimientoCaja.IdCbteCble = rel.ct_IdCbteCble;
                                            DataCajaMovimiento.modificarDB(MovimientoCaja);
                                        }
                                    }
                                    #endregion
                                    break;
                                default:
                                    #region Movimiento contable
                                    var MovimientoContable = ArmarMovimientoContable(info);
                                    if (MovimientoContable != null)
                                    {
                                        var rel = Context_cxc.cxc_cobro_x_ct_cbtecble.Where(q => q.cbr_IdEmpresa == info.IdEmpresa && q.cbr_IdSucursal == info.IdSucursal && q.cbr_IdCobro == info.IdCobro).FirstOrDefault();
                                        if (rel == null)
                                        {
                                            if (DataContable.guardarDB(MovimientoContable))
                                            {
                                                Context_cxc.cxc_cobro_x_ct_cbtecble.Add(new cxc_cobro_x_ct_cbtecble
                                                {
                                                    cbr_IdEmpresa = info.IdEmpresa,
                                                    cbr_IdSucursal = info.IdSucursal,
                                                    cbr_IdCobro = info.IdCobro,
                                                    ct_IdEmpresa = MovimientoContable.IdEmpresa,
                                                    ct_IdTipoCbte = MovimientoContable.IdTipoCbte,
                                                    ct_IdCbteCble = MovimientoContable.IdCbteCble,
                                                    observacion = ""
                                                });
                                                Context_cxc.SaveChanges();
                                            }
                                        }
                                        else
                                        {
                                            MovimientoContable.IdCbteCble = rel.ct_IdCbteCble;
                                            DataContable.modificarDB(MovimientoContable);
                                        }
                                    }
                                    #endregion
                                    break;
                            }
                        }
                    }
                    else
                    {
                        #region Movimiento contable
                        var MovimientoContable = ArmarMovimientoContable(info);
                        if (MovimientoContable != null)
                        {
                            var rel = Context_cxc.cxc_cobro_x_ct_cbtecble.Where(q => q.cbr_IdEmpresa == info.IdEmpresa && q.cbr_IdSucursal == info.IdSucursal && q.cbr_IdCobro == info.IdCobro).FirstOrDefault();
                            if (rel == null)
                            {
                                if (DataContable.guardarDB(MovimientoContable))
                                {
                                    Context_cxc.cxc_cobro_x_ct_cbtecble.Add(new cxc_cobro_x_ct_cbtecble
                                    {
                                        cbr_IdEmpresa = info.IdEmpresa,
                                        cbr_IdSucursal = info.IdSucursal,
                                        cbr_IdCobro = info.IdCobro,
                                        ct_IdEmpresa = MovimientoContable.IdEmpresa,
                                        ct_IdTipoCbte = MovimientoContable.IdTipoCbte,
                                        ct_IdCbteCble = MovimientoContable.IdCbteCble,
                                        observacion = ""
                                    });
                                    Context_cxc.SaveChanges();
                                }
                            }
                            else
                            {
                                MovimientoContable.IdCbteCble = rel.ct_IdCbteCble;
                                DataContable.modificarDB(MovimientoContable);
                            }
                        }
                        #endregion

                    }

                    #endregion

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<cxc_cobro_Info> GetSaldoAlumno(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                cxc_cobro_Info info = new cxc_cobro_Info();
                List<cxc_cobro_Info> Lista = new List<cxc_cobro_Info>();

                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT cabfac.IdEmpresa, cabfac.IdSucursal, cabfac.IdBodega, cabfac.vt_tipoDoc, cabfac.vt_tipoDoc + '-' + CAST(CAST(cabfac.vt_NumFactura AS int) AS varchar(20)) AS vt_NunDocumento, cabfac.vt_Observacion AS Referencia, "
                  + " cabfac.IdCbteVta AS IdComprobante, cabfac.CodCbteVta AS CodComprobante, Sucu.Su_Descripcion, cabfac.IdCliente, cabfac.IdAlumno, cabfac.vt_fecha, CAST(detfac.Total AS FLOAT) AS vt_total, "
                  + " ROUND(detfac.Total - ROUND(ISNULL(vwcxc_total_cobros_x_Docu.dc_ValorPago, 0), 2), 2) AS Saldo, ISNULL(vwcxc_total_cobros_x_Docu.dc_ValorPago, 0) AS TotalxCobrado, Bod.bo_Descripcion AS Bodega, "
                  + " CAST(detfac.SubtotalConDscto AS FLOAT) AS vt_Subtotal, CAST(detfac.ValorIVA AS FLOAT) AS vt_iva, cabfac.vt_fech_venc, ROUND(0, 2) AS dc_ValorRetFu, ROUND(0, 2) AS dc_ValorRetIva, Cli.Codigo AS CodCliente, "
                  + " tb_persona.pe_nombreCompleto AS NomCliente, tb_empresa.em_nombre, cabfac.Estado, CASE WHEN aca_AnioLectivo_Rubro.AplicaProntoPago = 1 AND aca_AnioLectivo_Periodo.FechaProntoPago >= CAST(GETDATE() AS DATE) "
                  + " THEN detfac.ValorProntoPago ELSE detfac.Total END AS ValorProntoPago, aca_AnioLectivo_Periodo.FechaProntoPago AS FechaProntoPago, detfac.IdAnio, detfac.IdPlantilla, cabfac.IdPuntoVta, "
                  + " CASE WHEN aca_AnioLectivo_Rubro.AplicaProntoPago = 1 AND aca_AnioLectivo_Periodo.FechaProntoPago >= CAST(GETDATE() AS DATE) THEN detfac.ValorProntoPago - round(isnull(vwcxc_total_cobros_x_Docu.dc_ValorPago, 0), 2) "
                  + " ELSE ROUND(detfac.Total - ROUND(ISNULL(vwcxc_total_cobros_x_Docu.dc_ValorPago, 0), 2), 2) END AS SaldoProntoPago "
                  + " FROM fa_factura_resumen AS detfac INNER JOIN "
                  + " fa_factura AS cabfac ON detfac.IdBodega = cabfac.IdBodega AND detfac.IdSucursal = cabfac.IdSucursal AND detfac.IdEmpresa = cabfac.IdEmpresa AND detfac.IdCbteVta = cabfac.IdCbteVta INNER JOIN "
                  + " tb_sucursal AS Sucu ON cabfac.IdEmpresa = Sucu.IdEmpresa AND cabfac.IdSucursal = Sucu.IdSucursal INNER JOIN "
                  + " tb_bodega AS Bod ON cabfac.IdEmpresa = Bod.IdEmpresa AND cabfac.IdSucursal = Bod.IdSucursal AND cabfac.IdBodega = Bod.IdBodega AND Sucu.IdEmpresa = Bod.IdEmpresa AND Sucu.IdSucursal = Bod.IdSucursal INNER JOIN "
                  + " fa_cliente AS Cli ON cabfac.IdEmpresa = Cli.IdEmpresa AND cabfac.IdCliente = Cli.IdCliente INNER JOIN "
                  + " tb_persona ON Cli.IdPersona = tb_persona.IdPersona INNER JOIN "
                  + " tb_empresa ON cabfac.IdEmpresa = tb_empresa.IdEmpresa LEFT OUTER JOIN "
                  + " aca_AnioLectivo_Rubro ON detfac.IdEmpresa = aca_AnioLectivo_Rubro.IdEmpresa AND detfac.IdAnio = aca_AnioLectivo_Rubro.IdAnio AND detfac.IdRubro = aca_AnioLectivo_Rubro.IdRubro LEFT OUTER JOIN "
                  + " aca_AnioLectivo_Periodo ON detfac.IdPeriodo = aca_AnioLectivo_Periodo.IdPeriodo AND detfac.IdEmpresa = aca_AnioLectivo_Periodo.IdEmpresa LEFT OUTER JOIN "
                  + " vwcxc_total_cobros_x_Docu ON cabfac.IdEmpresa = vwcxc_total_cobros_x_Docu.IdEmpresa AND cabfac.IdSucursal = vwcxc_total_cobros_x_Docu.IdSucursal AND cabfac.IdBodega = vwcxc_total_cobros_x_Docu.IdBodega_Cbte AND "
                  + " cabfac.vt_tipoDoc = vwcxc_total_cobros_x_Docu.dc_TipoDocumento AND cabfac.IdCbteVta = vwcxc_total_cobros_x_Docu.IdCbte_vta_nota "
                + " WHERE(cabfac.Estado = 'A') AND ROUND(detfac.Total - ROUND(ISNULL(vwcxc_total_cobros_x_Docu.dc_ValorPago, 0), 2), 2) > 0 "
                + " and cabfac.IdEmpresa= " + IdEmpresa + " and cabfac.IdAlumno= "+ IdAlumno
                + " UNION "
                + " SELECT A.IdEmpresa, A.IdSucursal, A.IdBodega, 'NTDB' AS CreDeb, CASE WHEN A.NumNota_Impresa IS NULL THEN 'N/D#' + CAST(A.IdNota AS varchar(20)) ELSE 'N/D#' + A.Serie1 + '-' + A.Serie2 + '' + A.NumNota_Impresa END AS Documento,  "
                  + " A.sc_observacion, A.IdNota, A.CodNota, su.Su_Descripcion, A.IdCliente, A.IdAlumno, A.no_fecha, ROUND(SUM(B.sc_total), 2) AS sc_total, ROUND(SUM(B.sc_total) - ISNULL(SUM(CB.dc_ValorPago), 0), 2) AS Saldo, "
                  + " ISNULL(SUM(CB.dc_ValorPago), 0) AS totalCobrado, Bo.bo_Descripcion, ROUND(SUM(B.sc_subtotal), 2) AS sc_subtotal, ROUND(SUM(B.sc_iva), 2) AS sc_iva, A.no_fecha_venc, cast(0 AS float) AS RtFT, cast(0 AS float) AS RtIVA, "
                  + " Cli.Codigo AS CodCliente, tb_persona.pe_nombreCompleto, tb_empresa.em_nombre, A.Estado, ROUND(SUM(B.sc_total), 2), NULL, NULL IdAnio, NULL IdPlantilla, A.IdPuntoVta, ROUND(SUM(B.sc_total) - ISNULL(SUM(CB.dc_ValorPago), 0), "
                  + " 2) SaldoProntoPago "
                + " FROM fa_notaCreDeb AS A INNER JOIN "
                  + " fa_notaCreDeb_det AS B ON A.IdEmpresa = B.IdEmpresa AND A.IdSucursal = B.IdSucursal AND A.IdBodega = B.IdBodega AND A.IdNota = B.IdNota INNER JOIN "
                  + " tb_bodega AS Bo ON A.IdEmpresa = Bo.IdEmpresa AND A.IdSucursal = Bo.IdSucursal AND A.IdBodega = Bo.IdBodega INNER JOIN "
                  + " tb_sucursal AS su ON Bo.IdEmpresa = su.IdEmpresa AND Bo.IdSucursal = su.IdSucursal INNER JOIN "
                  + " fa_cliente AS Cli ON A.IdEmpresa = Cli.IdEmpresa AND A.IdCliente = Cli.IdCliente INNER JOIN "
                  + " tb_persona ON Cli.IdPersona = tb_persona.IdPersona INNER JOIN "
                  + " tb_empresa ON A.IdEmpresa = tb_empresa.IdEmpresa LEFT OUTER JOIN "
                  + " vwcxc_total_cobros_x_Docu AS CB ON A.IdEmpresa = CB.IdEmpresa AND A.IdSucursal = CB.IdSucursal AND A.IdBodega = CB.IdBodega_Cbte AND A.IdNota = CB.IdCbte_vta_nota AND "
                  + " A.CodDocumentoTipo = CB.dc_TipoDocumento "
                + " WHERE A.Estado = 'A' and A.IdEmpresa= " + IdEmpresa +" and A.IdAlumno=" + IdAlumno
                + "AND NOT EXISTS "
                     + " (SELECT * "
                      + " FROM      fa_notaCreDeb_x_fa_factura_NotaDeb Cruce "
                      + " WHERE   Cruce.IdEmpresa_nt = A.IdEmpresa AND Cruce.IdSucursal_nt = A.IdSucursal AND Cruce.IdBodega_nt = A.IdBodega AND Cruce.IdNota_nt = A.IdNota AND Cruce.Valor_Aplicado <> 0) "
                + " GROUP BY A.IdEmpresa, A.IdSucursal, A.IdBodega, A.no_fecha, A.CreDeb, A.IdNota, A.Serie1, A.Serie2, A.NumNota_Impresa, A.sc_observacion, A.CodNota, su.Su_Descripcion, A.IdCliente, A.IdAlumno, Bo.bo_Descripcion, A.no_fecha_venc, "
                  + " Cli.Codigo, tb_persona.pe_nombreCompleto, tb_empresa.em_nombre, A.Estado, A.IdPuntoVta "
                + " HAVING(A.CreDeb = 'D') AND ROUND(SUM(B.sc_total) - ISNULL(SUM(CB.dc_ValorPago), 0), 2) > 0 ";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new cxc_cobro_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdCliente = Convert.ToDecimal(reader["IdCliente"]),
                            cr_saldo = Convert.ToDouble(reader["Saldo"]),
                            vt_Total = Convert.ToDouble(reader["vt_Total"])
                        });
                    }
                    reader.Close();
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
