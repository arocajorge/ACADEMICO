using Core.Bus.General;
using Core.Data.CuentasPorCobrar;
using Core.Info.CuentasPorCobrar;
using Core.Info.General;
using System;
using System.Collections.Generic;

namespace Core.Bus.CuentasPorCobrar
{
    public class cxc_cobro_Bus
    {
        cxc_cobro_Data odata = new cxc_cobro_Data();
        public List<cxc_cobro_Info> get_list(int IdEmpresa, int IdSucursal, DateTime Fecha_ini, DateTime Fecha_fin)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, Fecha_ini, Fecha_fin);
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
                return odata.get_list_matricula(IdEmpresa, IdSucursal, Fecha_ini, Fecha_fin);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public List<cxc_cobro_Info> get_list_matricula_alumno(int IdEmpresa, int IdSucursal, decimal IdAlumno, DateTime Fecha_ini, DateTime Fecha_fin)
        {
            try
            {
                return odata.get_list_matricula_alumno(IdEmpresa, IdSucursal, IdAlumno, Fecha_ini, Fecha_fin);
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
                return odata.get_info(IdEmpresa, IdSucursal, IdCobro);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(cxc_cobro_Info info)
        {
            try
            {
                return odata.guardarDB(info);
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cxc_cobro_Bus", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool modificarDB(cxc_cobro_Info info)
        {
            try
            {
                return odata.modificarDB(info);
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cxc_cobro_Bus", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool anularDB(cxc_cobro_Info info)
        {
            try
            {
                return odata.anularDB(info);
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
                return odata.get_list_para_retencion(IdEmpresa, IdSucursal, fecha_ini, fecha_fin, TieneRetencion);
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
                return odata.get_info_para_retencion(IdEmpresa, IdSucursal, IdBodega, IdCbteVta, vt_tipoDoc);
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
                return odata.ValidarSaldoDocumento(IdEmpresa, IdSucursal, IdBodega, IdCbteVta, CodDocumentoTipo, ValorCobrado, ValorAnterior);
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
                return odata.get_list_deuda(IdEmpresa, IdAlumno);
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
                return odata.get_list_aplicacion_masiva(IdEmpresa);
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
                return odata.GetSaldoAlumno(IdEmpresa, IdAlumno, ConDescuento);
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
                return odata.ValidarMostrarBotonModificar(IdEmpresa, IdSucursal, IdCobro);
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
                return odata.Contabilizar(IdEmpresa, IdSucursal, IdCobro);
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
                return odata.GetSaldoAlumno(IdEmpresa, IdAlumno);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ReProcesarDet(cxc_cobro_det_Info item)
        {
            try
            {
                return odata.ReProcesarDet(item);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
