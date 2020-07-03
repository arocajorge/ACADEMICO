using Core.Data.Base;
using Core.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.General
{
    public class tb_ColaCorreoCodigo_Data
    {
        public List<tb_ColaCorreoCodigo_Info> GetList(int IdEmpresa)
        {
            try
            {
                List<tb_ColaCorreoCodigo_Info> Lista = new List<tb_ColaCorreoCodigo_Info>();

                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    var lst = db.tb_ColaCorreoCodigo.Where(q => q.IdEmpresa == IdEmpresa).ToList();
                    foreach (var item in lst)
                    {
                        Lista.Add(new tb_ColaCorreoCodigo_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            Codigo = item.Codigo,
                            Asunto = item.Asunto,
                            Cuerpo = item.Cuerpo
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

        public tb_ColaCorreoCodigo_Info GetInfo(int IdEmpresa, string Codigo)
        {
            try
            {
                tb_ColaCorreoCodigo_Info info = new tb_ColaCorreoCodigo_Info();

                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    var Entity = db.tb_ColaCorreoCodigo.Where(q => q.IdEmpresa == IdEmpresa && q.Codigo == Codigo).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new tb_ColaCorreoCodigo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        Codigo = Entity.Codigo,
                        Asunto = Entity.Asunto,
                        Cuerpo = Entity.Cuerpo
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
