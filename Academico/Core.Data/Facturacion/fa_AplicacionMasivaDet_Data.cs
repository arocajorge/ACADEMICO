using Core.Data.Base;
using Core.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Facturacion
{
    public class fa_AplicacionMasivaDet_Data
    {
        public List<fa_AplicacionMasivaDet_Info> get_list(int IdEmpresa, decimal IdAplicacion)
        {
            try
            {
                List<fa_AplicacionMasivaDet_Info> Lista= new List<fa_AplicacionMasivaDet_Info>();

                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    var lst = Context.vwfa_AplicacionMasivaDet.Where(q => q.IdEmpresa == IdEmpresa && q.IdAplicacion == IdAplicacion).ToList();

                    foreach (var q in lst)
                    {
                        var info = new fa_AplicacionMasivaDet_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAplicacion = q.IdAplicacion,
                            IdAlumno = q.IdAlumno,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            Saldo = q.Saldo
                        };
                        Lista.Add(info);
                    }
                }
                return Lista;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
