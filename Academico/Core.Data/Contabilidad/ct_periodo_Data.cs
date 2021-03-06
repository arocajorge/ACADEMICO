﻿using Core.Data.Banco;
using Core.Data.Base;
using Core.Data.CuentasPorCobrar;
using Core.Data.Facturacion;
using Core.Data.Inventario;
using Core.Info.Contabilidad;
using Core.Info.General;
using Core.Info.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Contabilidad
{
    public class ct_periodo_Data
    {
        public List<ct_periodo_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {

            try
            {

                List<ct_periodo_Info> Lista;

                EntitiesGeneral Context_g = new EntitiesGeneral();
                EntitiesContabilidad Context = new EntitiesContabilidad();
                var lst_mes = (from q in Context_g.tb_mes
                               select new tb_mes_Info
                               {
                                   idMes = q.idMes,
                                   smes = q.smes
                               }).ToList();
                Context_g.Dispose();

                var lst_periodo = (from q in Context.vwct_periodo
                                   where q.IdEmpresa == IdEmpresa
                                   select new ct_periodo_Info
                                   {
                                       IdPeriodo = q.IdPeriodo,
                                       IdEmpresa = q.IdEmpresa,
                                       IdanioFiscal = q.IdanioFiscal,
                                       pe_FechaIni = q.pe_FechaIni,
                                       pe_FechaFin = q.pe_FechaFin,
                                       pe_mes = q.pe_mes,
                                       pe_cerrado = q.pe_cerrado,
                                       pe_estado = q.pe_estado,
                                       AnioMes = q.AnioMes,
                                       EstadoBool = q.pe_estado == "A" ? true : false
                                   }).ToList();


                if (mostrar_anulados == true)
                    Lista = (from q in lst_periodo
                             join m in lst_mes
                             on q.pe_mes equals m.idMes
                             where q.IdEmpresa == IdEmpresa
                             select new ct_periodo_Info
                             {
                                 IdPeriodo = q.IdPeriodo,
                                 IdEmpresa = q.IdEmpresa,
                                 IdanioFiscal = q.IdanioFiscal,
                                 pe_FechaIni = q.pe_FechaIni,
                                 pe_FechaFin = q.pe_FechaFin,
                                 pe_mes = q.pe_mes,
                                 pe_cerrado = q.pe_cerrado,
                                 pe_estado = q.pe_estado,
                                 smes = m.smes,
                                 AnioMes = q.AnioMes,

                                 EstadoBool = q.pe_estado == "A" ? true : false
                             }).ToList();
                else
                    Lista = (from q in lst_periodo
                             join m in lst_mes
                              on q.pe_mes equals m.idMes
                             where q.IdEmpresa == IdEmpresa
                              && q.pe_estado == "A"
                             select new ct_periodo_Info
                             {
                                 IdPeriodo = q.IdPeriodo,
                                 IdEmpresa = q.IdEmpresa,
                                 IdanioFiscal = q.IdanioFiscal,
                                 pe_FechaIni = q.pe_FechaIni,
                                 pe_FechaFin = q.pe_FechaFin,
                                 pe_mes = q.pe_mes,
                                 pe_cerrado = q.pe_cerrado,
                                 pe_estado = q.pe_estado,
                                 smes = m.smes,
                                 AnioMes = q.AnioMes,

                                 EstadoBool = q.pe_estado == "A" ? true : false
                             }).ToList();

                Lista.ForEach(q => q.nom_periodo_combo = q.IdanioFiscal + " " + q.smes);

                Context.Dispose();
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ct_periodo_Info get_info(int IdEmpresa, int IdPeriodo)
        {
            try
            {
                ct_periodo_Info info = new ct_periodo_Info();

                using (EntitiesContabilidad Context = new EntitiesContabilidad())
                {
                    ct_periodo Entity = Context.ct_periodo.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdPeriodo == IdPeriodo);
                    if (Entity == null) return null;
                    info = new ct_periodo_Info
                    {
                        IdPeriodo = Entity.IdPeriodo,
                        IdEmpresa = Entity.IdEmpresa,
                        IdanioFiscal = Entity.IdanioFiscal,
                        pe_FechaIni = Entity.pe_FechaIni,
                        pe_FechaFin = Entity.pe_FechaFin,
                        pe_mes = Entity.pe_mes,
                        pe_cerrado_bool = Entity.pe_cerrado == "S" ? true : false,
                        pe_estado = Entity.pe_estado
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ct_periodo_Info info)
        {
            try
            {
                using (EntitiesContabilidad Context = new EntitiesContabilidad())
                {
                    ct_periodo Entity = new ct_periodo
                    {
                        IdPeriodo = info.IdPeriodo,
                        IdEmpresa = info.IdEmpresa,
                        IdanioFiscal = info.IdanioFiscal,
                        pe_FechaIni = info.pe_FechaIni.Date,
                        pe_FechaFin = info.pe_FechaFin.Date,
                        pe_mes = info.pe_mes,
                        pe_cerrado = info.pe_cerrado_bool == true ? "S" : "N",
                        pe_estado = info.pe_estado = "A"
                    };
                    Context.ct_periodo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool modificarDB(ct_periodo_Info info)
        {
            try
            {
                using (EntitiesContabilidad Context = new EntitiesContabilidad())
                {
                    ct_periodo Entity = Context.ct_periodo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdPeriodo == info.IdPeriodo);
                    if (Entity == null)
                        return false;

                    Entity.pe_FechaFin = info.pe_FechaFin.Date;
                    Entity.pe_FechaIni = info.pe_FechaIni.Date;
                    Entity.pe_cerrado = info.pe_cerrado_bool == true ? "S" : "N";
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(ct_periodo_Info info)
        {
            try
            {
                using (EntitiesContabilidad Context = new EntitiesContabilidad())
                {
                    ct_periodo Entity = Context.ct_periodo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdPeriodo == info.IdPeriodo);
                    if (Entity == null)
                        return false;
                    Entity.pe_estado = info.pe_estado = "I";

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int get_id(int IdEmpresa)
        {
            try
            {
                int ID = 1;
                using (EntitiesContabilidad Context = new EntitiesContabilidad())
                {
                    var lst = from q in Context.ct_periodo
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdPeriodo) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ValidarFechaTransaccion(int IdEmpresa, DateTime Fecha, cl_enumeradores.eModulo Modulo, int IdSucursal, ref string mensaje)
        {
            EntitiesContabilidad db_conta = new EntitiesContabilidad();
            EntitiesGeneral db_general = new EntitiesGeneral();

            try
            {
                Fecha = Fecha.Date;
                int Periodo = Convert.ToInt32(Fecha.ToString("yyyyMM"));
                string sModulo = Modulo.ToString();
                ct_CierrePorModuloPorSucursal CierreModulo = new ct_CierrePorModuloPorSucursal();
                var empresa = db_general.tb_empresa.Where(q => q.IdEmpresa == IdEmpresa).FirstOrDefault();
                if (empresa != null)
                {
                    if (empresa.em_fechaInicioActividad > Fecha.Date)
                    {
                        mensaje = "La fecha de la transacción es menor al inicio de actividades de la empresa en el sistema FIXED ERP: " + Fecha.Date.ToString("dd/MM/yyyy");
                        return false;
                    }
                }

                ct_periodo per = db_conta.ct_periodo.Where(q => q.IdEmpresa == IdEmpresa && q.IdPeriodo == Periodo).FirstOrDefault();

                if (per == null)
                {
                    mensaje = "El periodo " + Periodo + " de la transacción no se encuentra registrado.";
                    return false;
                }

                if (per.pe_cerrado == "S")
                {
                    mensaje = "El periodo " + Periodo + " se encuentra cerrado.";
                    return false;
                }

                switch (Modulo)
                {
                    case cl_enumeradores.eModulo.INV:
                        using (EntitiesInventario db = new EntitiesInventario())
                        {
                            var param = db.in_parametro.Where(q => q.IdEmpresa == IdEmpresa).FirstOrDefault();
                            if (param == null)
                            {
                                mensaje = "No existen parámetros para el módulo de inventario";
                                return false;
                            }

                            //var FechaFutura = DateTime.Now.AddDays((param == null ? 0 : param.DiasTransaccionesAFuturo));
                            //var FechaPasada = DateTime.Now.AddDays(-(param == null ? 0 : (param.DiasTransaccionesAPasado == null ? 99999 : Convert.ToInt32(param.DiasTransaccionesAPasado))));

                            //if (!(Fecha >= FechaPasada && Fecha <= FechaFutura))
                            //{
                            //    mensaje = "La fecha de la transacción no está permitida por los parámetros del módulo de inventario";
                            //    return false;
                            //}

                            CierreModulo = db_conta.ct_CierrePorModuloPorSucursal.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.CodModulo == "INV" && q.Cerrado).OrderByDescending(q => q.FechaFin).FirstOrDefault();
                            if (CierreModulo != null)
                            {
                                if (Fecha.Date <= CierreModulo.FechaFin)
                                {
                                    mensaje = "El periodo de la transacción se encuentra cerrado para el módulo de inventario";
                                    return false;
                                }
                            }
                        }
                        break;
                    case cl_enumeradores.eModulo.FAC:
                        using (EntitiesFacturacion db = new EntitiesFacturacion())
                        {
                            var param = db.fa_parametro.Where(q => q.IdEmpresa == IdEmpresa).FirstOrDefault();
                            if (param == null)
                            {
                                mensaje = "No existen parámetros para el módulo de facturación";
                                return false;
                            }

                            //var FechaFutura = DateTime.Now.AddDays((param == null ? 0 : param.DiasTransaccionesAFuturo));
                            //var FechaPasada = DateTime.Now.AddDays(-(param == null ? 0 : (param.DiasTransaccionesAPasado == null ? 99999 : Convert.ToInt32(param.DiasTransaccionesAPasado))));

                            //if (!(Fecha >= FechaPasada && Fecha <= FechaFutura))
                            //{
                            //    mensaje = "La fecha de la transacción no está permitida por los parámetros del módulo de facturación";
                            //    return false;
                            //}


                            if (param.DiasTransaccionesAFuturo > 0 && DateTime.Now.Date.AddDays(param.DiasTransaccionesAFuturo) < Fecha)
                            {
                                mensaje = "La fecha de la transacción es superior a la fecha permitida por los parámetros del módulo de facturación";
                                return false;
                            }

                            CierreModulo = db_conta.ct_CierrePorModuloPorSucursal.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.CodModulo == "FAC" && q.Cerrado).OrderByDescending(q => q.FechaFin).FirstOrDefault();
                            if (CierreModulo != null)
                            {
                                if (Fecha.Date <= CierreModulo.FechaFin)
                                {
                                    mensaje = "El periodo de la transacción se encuentra cerrado para el módulo de facturación";
                                    return false;
                                }
                            }
                        }
                        break;
                    
                    case cl_enumeradores.eModulo.CONTA:
                        using (EntitiesContabilidad db = new EntitiesContabilidad())
                        {
                            var param = db.ct_parametro.Where(q => q.IdEmpresa == IdEmpresa).FirstOrDefault();
                            if (param == null)
                            {
                                mensaje = "No existen parámetros para el módulo de contabilidad";
                                return false;
                            }


                            //var FechaFutura = DateTime.Now.AddDays((param == null ? 0 : param.DiasTransaccionesAFuturo));
                            //var FechaPasada = DateTime.Now.AddDays(-(param == null ? 0 : (param.DiasTransaccionesAPasado == null ? 99999 : Convert.ToInt32(param.DiasTransaccionesAPasado))));

                            //if (!(Fecha >= FechaPasada && Fecha <= FechaFutura))
                            //{
                            //    mensaje = "La fecha de la transacción no está permitida por los parámetros del módulo de contabilidad";
                            //    return false;
                            //}


                            if (param.DiasTransaccionesAFuturo > 0 && DateTime.Now.Date.AddDays(param.DiasTransaccionesAFuturo) < Fecha)
                            {
                                mensaje = "La fecha de la transacción es superior a la fecha permitida por los parámetros del módulo de contabilidad";
                                return false;
                            }

                            CierreModulo = db_conta.ct_CierrePorModuloPorSucursal.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.CodModulo == "CONTA" && q.Cerrado).OrderByDescending(q => q.FechaFin).FirstOrDefault();
                            if (CierreModulo != null)
                            {
                                if (Fecha.Date <= CierreModulo.FechaFin)
                                {
                                    mensaje = "El periodo de la transacción se encuentra cerrado para el módulo de contabilidad";
                                    return false;
                                }
                            }
                        }
                        break;
                    case cl_enumeradores.eModulo.CAJA:
                        using (EntitiesCaja db = new EntitiesCaja())
                        {
                            var param = db.caj_parametro.Where(q => q.IdEmpresa == IdEmpresa).FirstOrDefault();
                            if (param == null)
                            {
                                mensaje = "No existen parámetros para el módulo de caja";
                                return false;
                            }

                            //var FechaFutura = DateTime.Now.AddDays((param == null ? 0 : param.DiasTransaccionesAFuturo));
                            //var FechaPasada = DateTime.Now.AddDays(-(param == null ? 0 : (param.DiasTransaccionesAPasado == null ? 99999 : Convert.ToInt32(param.DiasTransaccionesAPasado))));

                            //if (!(Fecha >= FechaPasada && Fecha <= FechaFutura))
                            //{
                            //    mensaje = "La fecha de la transacción no está permitida por los parámetros del módulo de caja";
                            //    return false;
                            //}

                            if (param.DiasTransaccionesAFuturo > 0 && DateTime.Now.Date.AddDays(param.DiasTransaccionesAFuturo) < Fecha)
                            {
                                mensaje = "La fecha de la transacción es superior a la fecha permitida por los parámetros del módulo de caja";
                                return false;
                            }

                            CierreModulo = db_conta.ct_CierrePorModuloPorSucursal.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.CodModulo == "CAJ" && q.Cerrado).OrderByDescending(q => q.FechaFin).FirstOrDefault();
                            if (CierreModulo != null)
                            {
                                if (Fecha.Date <= CierreModulo.FechaFin)
                                {
                                    mensaje = "El periodo de la transacción se encuentra cerrado para el módulo de caja";
                                    return false;
                                }
                            }
                        }
                        break;
                    case cl_enumeradores.eModulo.BANCO:
                        using (EntitiesBanco db = new EntitiesBanco())
                        {
                            var param = db.ba_parametros.Where(q => q.IdEmpresa == IdEmpresa).FirstOrDefault();
                            if (param == null)
                            {
                                mensaje = "No existen parámetros para el módulo de bancos";
                                return false;
                            }

                            //var FechaFutura = DateTime.Now.AddDays((param == null ? 0 : param.DiasTransaccionesAFuturo));
                            //var FechaPasada = DateTime.Now.AddDays(-(param == null ? 0 : (param.DiasTransaccionesAPasado == null ? 99999 : Convert.ToInt32(param.DiasTransaccionesAPasado))));

                            //if (!(Fecha >= FechaPasada && Fecha <= FechaFutura))
                            //{
                            //    mensaje = "La fecha de la transacción no está permitida por los parámetros del módulo de bancos";
                            //    return false;
                            //}

                            if (param.DiasTransaccionesAFuturo > 0 && DateTime.Now.Date.AddDays(param.DiasTransaccionesAFuturo) < Fecha)
                            {
                                mensaje = "La fecha de la transacción es superior a la fecha permitida por los parámetros del módulo de bancos";
                                return false;
                            }

                            CierreModulo = db_conta.ct_CierrePorModuloPorSucursal.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.CodModulo == "BAN" && q.Cerrado).OrderByDescending(q => q.FechaFin).FirstOrDefault();
                            if (CierreModulo != null)
                            {
                                if (Fecha.Date <= CierreModulo.FechaFin)
                                {
                                    mensaje = "El periodo de la transacción se encuentra cerrado para el módulo de bancos";
                                    return false;
                                }
                            }
                        }
                        break;
                    case cl_enumeradores.eModulo.CXC:
                        using (EntitiesCuentasPorCobrar db = new EntitiesCuentasPorCobrar())
                        {
                            var param = db.cxc_Parametro.Where(q => q.IdEmpresa == IdEmpresa).FirstOrDefault();
                            if (param == null)
                            {
                                mensaje = "No existen parámetros para el módulo de cuentas por cobrar";
                                return false;
                            }

                            //var FechaFutura = DateTime.Now.AddDays((param == null ? 0 : param.DiasTransaccionesAFuturo));
                            //var FechaPasada = DateTime.Now.AddDays(-(param == null ? 0 : (param.DiasTransaccionesAPasado == null ? 99999 : Convert.ToInt32(param.DiasTransaccionesAPasado))));

                            //if (!(Fecha >= FechaPasada && Fecha <= FechaFutura))
                            //{
                            //    mensaje = "La fecha de la transacción no está permitida por los parámetros del módulo cuentas por cobrar";
                            //    return false;
                            //}

                            if (param.DiasTransaccionesAFuturo > 0 && DateTime.Now.Date.AddDays(param.DiasTransaccionesAFuturo) < Fecha)
                            {
                                mensaje = "La fecha de la transacción es superior a la fecha permitida por los parámetros del módulo de cuentas por cobrar";
                                return false;
                            }

                            CierreModulo = db_conta.ct_CierrePorModuloPorSucursal.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.CodModulo == "CXC" && q.Cerrado).OrderByDescending(q => q.FechaFin).FirstOrDefault();
                            if (CierreModulo != null)
                            {
                                if (Fecha.Date <= CierreModulo.FechaFin)
                                {
                                    mensaje = "El periodo de la transacción se encuentra cerrado para el módulo de cuentas por cobrar";
                                    return false;
                                }
                            }
                        }
                        break;
                    case cl_enumeradores.eModulo.CXP:
                        using (EntitiesCuentasPorPagar db = new EntitiesCuentasPorPagar())
                        {
                            var param = db.cp_parametros.Where(q => q.IdEmpresa == IdEmpresa).FirstOrDefault();
                            if (param == null)
                            {
                                mensaje = "No existen parámetros para el módulo de cuentas por pagar";
                                return false;
                            }

                            //var FechaFutura = DateTime.Now.AddDays((param == null ? 0 : param.DiasTransaccionesAFuturo));
                            //var FechaPasada = DateTime.Now.AddDays(-(param == null ? 0 : (param.DiasTransaccionesAPasado == null ? 99999 : Convert.ToInt32(param.DiasTransaccionesAPasado))));

                            //if (!(Fecha >= FechaPasada && Fecha <= FechaFutura))
                            //{
                            //    mensaje = "La fecha de la transacción no está permitida por los parámetros del módulo de cuentas por pagar";
                            //    return false;
                            //}

                            if (param.DiasTransaccionesAFuturo > 0 && DateTime.Now.Date.AddDays(param.DiasTransaccionesAFuturo) < Fecha)
                            {
                                mensaje = "La fecha de la transacción es superior a la fecha permitida por los parámetros del módulo de cuentas por pagar";
                                return false;
                            }

                            CierreModulo = db_conta.ct_CierrePorModuloPorSucursal.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.CodModulo == "CXP" && q.Cerrado).OrderByDescending(q => q.FechaFin).FirstOrDefault();
                            if (CierreModulo != null)
                            {
                                if (Fecha.Date <= CierreModulo.FechaFin)
                                {
                                    mensaje = "El periodo de la transacción se encuentra cerrado para el módulo de cuentas por pagar";
                                    return false;
                                }
                            }
                        }
                        break;
                }

                db_general.Dispose();
                db_conta.Dispose();
                return true;
            }
            catch (Exception)
            {
                db_conta.Dispose();
                db_general.Dispose();
                throw;
            }
        }
    }
}
