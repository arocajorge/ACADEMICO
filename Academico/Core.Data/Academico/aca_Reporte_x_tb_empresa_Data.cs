using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_Reporte_x_tb_empresa_Data
    {
        public aca_Reporte_x_tb_empresa_Info GetInfo(int IdEmpresa, string CodReporte)
        {
            try
            {
                aca_Reporte_x_tb_empresa_Info info;
                EntitiesGeneral dbg = new EntitiesGeneral();
                EntitiesAcademico dba = new EntitiesAcademico();

                var Entity = dba.aca_Reporte_x_tb_empresa.Where(q => q.IdEmpresa == IdEmpresa && q.CodReporte == CodReporte).FirstOrDefault();
                if (Entity == null) return null;
                else
                {
                    info = new aca_Reporte_x_tb_empresa_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        CodReporte = Entity.CodReporte,
                        ReporteDisenio = Entity.ReporteDisenio,
                        Reporte = Entity.aca_Reporte.nom_reporte
                    };
                    var Modulo = dbg.tb_modulo.Where(q => q.CodModulo == Entity.aca_Reporte.CodModulo).FirstOrDefault();
                    if (Modulo != null)
                    {
                        info.Nom_Carpeta = Modulo.Nom_Carpeta;
                    }
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(aca_Reporte_x_tb_empresa_Info info)
        {
            try
            {
                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_Reporte_x_tb_empresa.Where(q => q.IdEmpresa == info.IdEmpresa && q.CodReporte == info.CodReporte).FirstOrDefault();
                    if (Entity == null)
                        db.aca_Reporte_x_tb_empresa.Add(new aca_Reporte_x_tb_empresa
                        {
                            IdEmpresa = info.IdEmpresa,
                            CodReporte = info.CodReporte,
                            ReporteDisenio = info.ReporteDisenio
                        });
                    else
                        Entity.ReporteDisenio = info.ReporteDisenio;
                    db.SaveChanges();
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
