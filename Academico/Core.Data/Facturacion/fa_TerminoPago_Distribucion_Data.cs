using Core.Data.Base;
using Core.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Facturacion
{
   public class fa_TerminoPago_Distribucion_Data
    {
        public List<fa_TerminoPago_Distribucion_Info> get_list(string IdTerminoPago)
        {
            try
            {
                List<fa_TerminoPago_Distribucion_Info> Lista;
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    Lista = (from q in Context.fa_TerminoPago_Distribucion
                             where q.IdTerminoPago == IdTerminoPago
                             select new fa_TerminoPago_Distribucion_Info
                             {
                                 IdTerminoPago = q.IdTerminoPago,
                                 Num_Dias_Vcto = q.Num_Dias_Vcto,
                                 Por_distribucion = q.Por_distribucion,
                                 Secuencia = q.Secuencia
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
