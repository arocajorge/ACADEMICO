using Core.Data.Base;
using Core.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.CuentasPorCobrar
{
    public class cxc_CobroMasivoDet_Data
    {
        public List<cxc_CobroMasivoDet_Info> get_list(int IdEmpresa, decimal IdCobroMasivo)
        {
            try
            {
                List<cxc_CobroMasivoDet_Info> Lista = new List<cxc_CobroMasivoDet_Info>(); ;

                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    var lst = Context.vwcxc_CobroMasivoDet.Where(q => q.IdEmpresa == IdEmpresa && q.IdCobroMasivo == IdCobroMasivo).ToList();

                    foreach (var q in lst)
                    {
                        var info = new cxc_CobroMasivoDet_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdSucursal = q.IdSucursal,
                            IdAlumno = q.IdAlumno,
                            NombreAlumno = q.pe_nombreCompleto,
                            CodigoAlumno = q.Codigo,
                            Fecha = q.Fecha,
                            Valor = q.Valor,
                            IdCobro = q.IdCobro,
                            IdCobroMasivo = q.IdCobroMasivo,
                            Secuencia = q.Secuencia,
                            Repetido=false,
                            ExisteAlumno =true,
                            ValorIgual = true
                        };
                        Lista.Add(info);
                    }
                    
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool modificarDB(cxc_CobroMasivoDet_Info info)
        {
            try
            {
                using (EntitiesCuentasPorCobrar db_f = new EntitiesCuentasPorCobrar())
                {
                    #region Detalle
                    var entity = db_f.cxc_CobroMasivoDet.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdCobroMasivo == info.IdCobroMasivo && q.Secuencia == info.Secuencia).FirstOrDefault();
                    if (entity == null) return false;

                    entity.IdSucursal = info.IdSucursal;
                    entity.IdCobro = info.IdCobro;

                    #endregion

                    db_f.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
