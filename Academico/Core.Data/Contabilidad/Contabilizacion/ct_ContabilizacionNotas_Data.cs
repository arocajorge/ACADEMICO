using Core.Data.Base;
using Core.Info.Contabilidad.Contabilizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Contabilidad.Contabilizacion
{
    public class ct_ContabilizacionNotas_Data
    {
        public List<ct_ContabilizacionNotas_Info> GetList(int IdEmpresa, DateTime FechaIni, DateTime FechaFin)
        {
            try
            {
                List<ct_ContabilizacionNotas_Info> Lista = new List<ct_ContabilizacionNotas_Info>();

                using (EntitiesContabilidad db = new EntitiesContabilidad())
                {
                    var lst = db.SPACA_ContabilizacionNotas(IdEmpresa, FechaIni, FechaFin).ToList();
                    foreach (var item in lst)
                    {
                        Lista.Add(new ct_ContabilizacionNotas_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdSucursal = item.IdSucursal,
                            IdBodega = item.IdBodega,
                            IdNota = item.IdNota,
                            no_fecha = item.no_fecha,
                            sc_observacion = item.sc_observacion,
                            pe_nombreCompleto = item.pe_nombreCompleto,
                            No_Descripcion = item.No_Descripcion,
                            ct_IdEmpresa = item.ct_IdEmpresa,
                            ct_IdTipoCbte = item.ct_IdTipoCbte,
                            ct_IdCbteCble = item.ct_IdCbteCble,
                            TotalModulo = item.TotalModulo,
                            TotalContabilidad = item.TotalContabilidad,
                            Saldo = item.Saldo,
                            IdString = item.IdSucursal.ToString("0000") + item.IdBodega.ToString("0000") + item.IdNota.ToString("0000000000")
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
