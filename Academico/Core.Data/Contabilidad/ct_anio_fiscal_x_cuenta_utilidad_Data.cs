using Core.Data.Base;
using Core.Info.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Contabilidad
{
    public class ct_anio_fiscal_x_cuenta_utilidad_Data
    {
        public ct_anio_fiscal_x_cuenta_utilidad_Info get_info(int IdEmpresa, int IdanioFiscal)
        {
            try
            {
                ct_anio_fiscal_x_cuenta_utilidad_Info info = new ct_anio_fiscal_x_cuenta_utilidad_Info();
                using (EntitiesContabilidad Context = new EntitiesContabilidad())
                {
                    ct_anio_fiscal_x_cuenta_utilidad Entity = Context.ct_anio_fiscal_x_cuenta_utilidad.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdanioFiscal == IdanioFiscal);
                    if (Entity == null) return null;
                    info = new ct_anio_fiscal_x_cuenta_utilidad_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdanioFiscal = Entity.IdanioFiscal,
                        IdCtaCble = Entity.IdCtaCble,
                        IdCtaCbleCierre = Entity.IdCtaCbleCierre
                    };
                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool guardarDB(ct_anio_fiscal_x_cuenta_utilidad_Info info)
        {
            try
            {
                using (EntitiesContabilidad Context = new EntitiesContabilidad())
                {
                    ct_anio_fiscal_x_cuenta_utilidad Entity = new ct_anio_fiscal_x_cuenta_utilidad
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdanioFiscal = info.IdanioFiscal,
                        IdCtaCble = info.IdCtaCble,
                        IdCtaCbleCierre = info.IdCtaCbleCierre
                    };
                    Context.ct_anio_fiscal_x_cuenta_utilidad.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool eliminarDB(int IdEmpresa, int IdanioFiscal)
        {
            try
            {
                using (EntitiesContabilidad Context = new EntitiesContabilidad())
                {
                    Context.Database.ExecuteSqlCommand("delete ct_anio_fiscal_x_cuenta_utilidad where IdEmpresa_cta = '" + IdEmpresa + "' and IdCtaCble = " + IdanioFiscal);

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
