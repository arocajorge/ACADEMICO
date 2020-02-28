using Core.Data.Base;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Data.Reportes.CuentasPorCobrar
{
    public class CXC_001_Data
    {
        public List<CXC_001_Info> GetList(int IdEmpresa, int IdSucursal, decimal IdAlumno, DateTime FechaCorte, bool MostrarSaldo0)
        {
            try
            {
                List<CXC_001_Info> Lista = new List<CXC_001_Info>();
                int IdSucursalFin = IdSucursal == 0 ? 999999 : IdSucursal;
                decimal IdAlumnoFin = IdAlumno == 0 ? 9999999999 : IdAlumno;
                using (EntitiesReportes db = new EntitiesReportes())
                {
                    var lst = db.SPCXC_001(IdEmpresa, IdSucursal, IdSucursalFin, IdAlumno, IdAlumnoFin, FechaCorte, MostrarSaldo0).ToList();
                    foreach (var item in lst)
                    {
                        Lista.Add(new CXC_001_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdSucursal = item.IdSucursal,
                            IdBodega = item.IdBodega,
                            IdCbteVta = item.IdCbteVta,
                            vt_tipoDoc = item.vt_tipoDoc,
                            vt_NumFactura = item.vt_NumFactura,
                            IdAlumno = item.IdAlumno,
                            NomCliente = item.NomCliente,
                            vt_fecha = item.vt_fecha,
                            vt_fech_venc = item.vt_fech_venc,
                            Subtotal = item.Subtotal,
                            IVA = item.IVA,
                            Total = item.Total,
                            Cobrado = item.Cobrado,
                            NotaCredito = item.NotaCredito,
                            Saldo = item.Saldo,
                            Su_Descripcion=item.Su_Descripcion
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
