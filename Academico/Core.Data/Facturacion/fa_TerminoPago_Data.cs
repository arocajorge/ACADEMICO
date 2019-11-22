using Core.Data.Base;
using Core.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Facturacion
{
    public class fa_TerminoPago_Data
    {
        public List<fa_TerminoPago_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                List<fa_TerminoPago_Info> Lista;
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.fa_TerminoPago
                                 select new fa_TerminoPago_Info
                                 {
                                     IdTerminoPago = q.IdTerminoPago,
                                     Dias_Vct = q.Dias_Vct,
                                     nom_TerminoPago = q.nom_TerminoPago,
                                     Num_Coutas = q.Num_Coutas,
                                     estado = q.estado
                                 }).ToList();
                    else
                        Lista = (from q in Context.fa_TerminoPago
                                 where q.estado == true
                                 select new fa_TerminoPago_Info
                                 {
                                     IdTerminoPago = q.IdTerminoPago,
                                     Dias_Vct = q.Dias_Vct,
                                     nom_TerminoPago = q.nom_TerminoPago,
                                     Num_Coutas = q.Num_Coutas,
                                     estado = q.estado
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
