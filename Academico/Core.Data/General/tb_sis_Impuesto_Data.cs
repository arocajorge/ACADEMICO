using Core.Data.Base;
using Core.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.General
{
    public class tb_sis_Impuesto_Data
    {
        public List<tb_sis_Impuesto_Info> get_list(string IdTipoImpuesto, bool mostrar_anulados)
        {
            try
            {
                List<tb_sis_Impuesto_Info> Lista;
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    if (mostrar_anulados == true)
                        Lista = (from q in Context.tb_sis_Impuesto
                                 where q.IdTipoImpuesto.Contains(IdTipoImpuesto)
                                 select new tb_sis_Impuesto_Info
                                 {
                                     IdCod_Impuesto = q.IdCod_Impuesto,
                                     nom_impuesto = q.nom_impuesto,
                                     porcentaje = q.porcentaje,
                                     estado = q.estado,
                                     IdTipoImpuesto = q.IdTipoImpuesto
                                 }).ToList();
                    else
                        Lista = (from q in Context.tb_sis_Impuesto
                                 where q.IdTipoImpuesto.Contains(IdTipoImpuesto)
                                 select new tb_sis_Impuesto_Info
                                 {
                                     IdCod_Impuesto = q.IdCod_Impuesto,
                                     nom_impuesto = q.nom_impuesto,
                                     porcentaje = q.porcentaje,
                                     estado = q.estado == true,
                                     IdTipoImpuesto = q.IdTipoImpuesto
                                 }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public tb_sis_Impuesto_Info get_info(string IdCod_Impuesto = "")
        {
            try
            {
                tb_sis_Impuesto_Info info = new tb_sis_Impuesto_Info();
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    tb_sis_Impuesto Entity = Context.tb_sis_Impuesto.FirstOrDefault(q => q.IdCod_Impuesto == IdCod_Impuesto);
                    if (Entity == null) return null;
                    info = new tb_sis_Impuesto_Info
                    {
                        IdCod_Impuesto = Entity.IdCod_Impuesto,
                        IdTipoImpuesto = Entity.IdTipoImpuesto,
                        nom_impuesto = Entity.nom_impuesto,
                        porcentaje = Entity.porcentaje,
                        IdCodigo_SRI = Entity.IdCodigo_SRI,
                        estado = Entity.estado,
                        Usado_en_Compras = Entity.Usado_en_Compras,
                        Usado_en_Ventas = Entity.Usado_en_Ventas
                    };
                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
