using Core.Data.Base;
using Core.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.General
{
    public class tb_Catalogo_Data
    {
        public List<tb_Catalogo_Info> get_list(int IdTipoCatalogo, bool mostrar_anulados)
        {
            try
            {
                List<tb_Catalogo_Info> Lista;
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    if (mostrar_anulados == true)
                        Lista = (from q in Context.tb_Catalogo
                                 where q.IdTipoCatalogo == IdTipoCatalogo
                                 select new tb_Catalogo_Info
                                 {
                                     IdCatalogo = q.IdCatalogo,
                                     IdTipoCatalogo = q.IdTipoCatalogo,
                                     ca_descripcion = q.ca_descripcion,
                                     ca_estado = q.ca_estado,
                                     ca_orden = q.ca_orden,
                                     CodCatalogo = q.CodCatalogo
                                 }).ToList();
                    else
                        Lista = (from q in Context.tb_Catalogo
                                 where q.ca_estado == "A"
                                 && q.IdTipoCatalogo == IdTipoCatalogo
                                 select new tb_Catalogo_Info
                                 {
                                     IdCatalogo = q.IdCatalogo,
                                     IdTipoCatalogo = q.IdTipoCatalogo,
                                     ca_descripcion = q.ca_descripcion,
                                     ca_estado = q.ca_estado,
                                     ca_orden = q.ca_orden,
                                     CodCatalogo = q.CodCatalogo
                                 }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public tb_Catalogo_Info get_info(string CodCatalogo)
        {
            try
            {
                tb_Catalogo_Info info = new tb_Catalogo_Info();
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    tb_Catalogo Entity = Context.tb_Catalogo.FirstOrDefault(q => q.CodCatalogo == CodCatalogo);
                    if (Entity == null) return null;
                    info = new tb_Catalogo_Info
                    {
                        IdTipoCatalogo = Entity.IdTipoCatalogo,
                        CodCatalogo = Entity.CodCatalogo,
                        ca_descripcion = Entity.ca_descripcion,
                        ca_orden = Entity.ca_orden
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
