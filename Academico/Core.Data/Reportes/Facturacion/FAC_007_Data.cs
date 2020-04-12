using Core.Data.Base;
using Core.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Facturacion
{
    public class FAC_007_Data
    {
        public List<FAC_007_Info> GetList(int IdEmpresa, DateTime FechaIni, DateTime FechaFin, int IdEmpresa_rol)
        {
            try
            {
                List<FAC_007_Info> Lista = new List<FAC_007_Info>();
                FechaIni = FechaIni.Date;
                FechaFin = FechaFin.Date;
                using (EntitiesReportes db = new EntitiesReportes())
                {
                    var lst = db.VWFAC_007.Where(q => q.IdEmpresa == IdEmpresa && FechaIni <= q.vt_fecha && q.vt_fecha <= FechaFin).ToList();

                    if(IdEmpresa_rol > 0)
                        lst = lst.Where(q => q.IdEmpresa_rol == IdEmpresa_rol).ToList();

                    foreach (var item in lst)
                    {
                        Lista.Add(new FAC_007_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdSucursal = item.IdSucursal,
                            IdBodega = item.IdBodega,
                            IdCbteVta = item.IdCbteVta,
                            vt_NumFactura = item.vt_NumFactura,
                            vt_fecha = item.vt_fecha,
                            vt_tipo_venta = item.vt_tipo_venta,
                            nom_TerminoPago = item.nom_TerminoPago,
                            Num_Coutas = item.Num_Coutas,
                            Total = item.Total,
                            vt_Observacion = item.vt_Observacion,
                            NomAlumno = item.NomAlumno,
                            CedulaAlumno = item.CedulaAlumno,
                            CodigoAlumno = item.CodigoAlumno,
                            NomEmpleado = item.NomEmpleado,
                            CedulaEmpleado = item.CedulaEmpleado,
                            em_nombre = item.em_nombre,
                            NomCliente = item.NomCliente,
                            pe_cedulaRuc = item.pe_cedulaRuc,
                            IdEmpresa_rol = item.IdEmpresa_rol
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
