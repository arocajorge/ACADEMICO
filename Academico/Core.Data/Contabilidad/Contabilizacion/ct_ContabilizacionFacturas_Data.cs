using Core.Data.Base;
using Core.Info.Contabilidad.Contabilizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Contabilidad.Contabilizacion
{
    public class ct_ContabilizacionFacturas_Data
    {
        public List<ct_ContabilizacionFacturas_Info> GetList(int IdEmpresa, DateTime FechaIni, DateTime FechaFin)
        {
            try
            {
                List<ct_ContabilizacionFacturas_Info> Lista = new List<ct_ContabilizacionFacturas_Info>();

                using (EntitiesContabilidad db = new EntitiesContabilidad())
                {
                    var lst = db.SPACA_ContabilizacionFacturas(IdEmpresa, FechaIni, FechaFin).ToList();
                    foreach (var item in lst)
                    {
                        Lista.Add(new ct_ContabilizacionFacturas_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdSucursal = item.IdSucursal,
                            IdBodega = item.IdBodega,
                            IdCbteVta = item.IdCbteVta,
                            vt_NumFactura = item.vt_NumFactura,
                            Alumno = item.Alumno,
                            vt_fecha = item.vt_fecha,
                            vt_Observacion = item.vt_Observacion,
                            IdCtaCbleDebe = item.IdCtaCbleDebe,
                            IdCtaCbleHaber = item.IdCtaCbleHaber,
                            ct_IdEmpresa = item.ct_IdEmpresa,
                            ct_IdTipoCbte = item.ct_IdTipoCbte,
                            ct_IdCbteCble = item.ct_IdCbteCble,
                            IdCtaCble = item.IdCtaCble,
                            TotalModulo = item.TotalModulo,
                            TotalContable = item.TotalContable,
                            Diferencia = item.Diferencia
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
