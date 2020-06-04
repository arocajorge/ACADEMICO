using Core.Data.Base;
using Core.Info.Contabilidad.Contabilizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Contabilidad.Contabilizacion
{
    public class ct_ContabilizacionConciliacionNC_Data
    {
        public List<ct_ContabilizacionConciliacionNC_Info> GetList(int IdEmpresa, DateTime FechaIni, DateTime FechaFin)
        {
            try
            {
                List<ct_ContabilizacionConciliacionNC_Info> Lista = new List<ct_ContabilizacionConciliacionNC_Info>();

                using (EntitiesCuentasPorCobrar db = new EntitiesCuentasPorCobrar())
                {
                    var lst = db.vwcxc_ConciliacionNotaCredito.Where(q => q.IdEmpresa == IdEmpresa && q.Fecha >= FechaIni && q.Fecha <= FechaFin).ToList();
                    foreach (var item in lst)
                    {
                        Lista.Add(new ct_ContabilizacionConciliacionNC_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdSucursal = item.IdSucursal,
                            IdConciliacion = item.IdConciliacion,
                            IdCobro = item.IdCobro,
                            Fecha = item.Fecha,
                            Estado = item.Estado,
                            IdAlumno = item.IdAlumno,
                            IdBodega = item.IdBodega,
                            IdCbteCble = item.IdCbteCble,
                            Codigo = item.Codigo,
                            pe_nombreCompleto = item.pe_nombreCompleto,
                            IdNota = item.IdNota,
                            IdTipoCbte = item.IdTipoCbte,
                            Observacion = item.Observacion,
                            Referencia=item.Referencia,
                            Valor = item.Valor,
                            IdString = item.IdSucursal.ToString("0000") + item.IdConciliacion.ToString("0000000000")
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
