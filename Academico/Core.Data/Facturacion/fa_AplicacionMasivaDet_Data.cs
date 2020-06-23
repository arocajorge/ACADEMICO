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
                List<fa_AplicacionMasivaDet_Info> Lista;

                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    Lista = (from q in Context.vwfa_AplicacionMasivaDet
                             where q.IdEmpresa == IdEmpresa
                             && q.IdAplicacion == IdAplicacion
                             select new fa_AplicacionMasivaDet_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdAplicacion= q.IdAplicacion,
                                 IdAlumno = q.IdAlumno,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 Saldo = q.Saldo
                                 
                             }).ToList();
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
