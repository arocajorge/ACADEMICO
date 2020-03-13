using Core.Data.Base;
using Core.Info.Contabilidad.Contabilizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Contabilidad.Contabilizacion
{
    public class ct_ContabilizacionCobros_Data
    {
        public List<ct_ContabilizacionCobros_Info> GetList(int IdEmpresa, DateTime FechaIni, DateTime FechaFin)
        {
            try
            {
                List<ct_ContabilizacionCobros_Info> Lista = new List<ct_ContabilizacionCobros_Info>();

                using (EntitiesContabilidad db = new EntitiesContabilidad())
                {
                    var lst = db.SPACA_ContabilizacionCobros(IdEmpresa, FechaIni, FechaFin);
                    foreach (var item in lst)
                    {
                        Lista.Add(new ct_ContabilizacionCobros_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdSucursal = item.IdSucursal,
                            IdCobro = item.IdCobro,
                            pe_nombreCompleto = item.pe_nombreCompleto,
                            IdCobro_tipo = item.IdCobro_tipo,
                            ct_IdEmpresa = item.ct_IdEmpresa,
                            ct_IdTipoCbte = item.ct_IdTipoCbte,
                            ct_IdCbteCble = item.ct_IdCbteCble,
                            TotalModulo = item.TotalModulo,
                            TotalContabilidad = item.TotalContabilidad,
                            Saldo = item.Saldo,
                            cr_fecha = item.cr_fecha,
                            cr_ObservacionPantalla = item.cr_ObservacionPantalla
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
