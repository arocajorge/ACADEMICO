using Core.Data.Base;
using Core.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.General
{
    public class tb_sucursal_Data
    {
        public List<tb_sucursal_Info> GetList(int IdEmpresa, string IdUsuario, bool MostrarTodos)
        {
            try
            {
                List<tb_sucursal_Info> Lista;
                using (EntitiesSeguridadAcceso Context = new EntitiesSeguridadAcceso())
                {
                    Lista = Context.vwseg_usuario_x_tb_sucursal.Where(q => q.IdEmpresa == IdEmpresa && q.IdUsuario == IdUsuario).Select(q => new tb_sucursal_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdUsuario = q.IdUsuario,
                        IdSucursal = q.IdSucursal,
                        Su_Descripcion = q.Su_Descripcion,
                    }).ToList();
                    if (MostrarTodos)
                    {
                        Lista.Add(new tb_sucursal_Info
                        {
                            IdEmpresa = IdEmpresa,
                            IdSucursal = 0,
                            Su_Descripcion = "TODAS"
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

        public List<tb_sucursal_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<tb_sucursal_Info> Lista;

                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    Lista = Context.tb_sucursal.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == (mostrar_anulados == true ? q.Estado : "A")).Select(q => new tb_sucursal_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdSucursal = q.IdSucursal,
                        Su_Descripcion = q.Su_Descripcion,
                        Su_CodigoEstablecimiento = q.Su_CodigoEstablecimiento,
                        Su_Ruc = q.Su_Ruc,
                        Estado = q.Estado,

                        EstadoBool = q.Estado == "A" ? true : false
                    }).ToList();
                }

                Lista.ForEach(v => { v.IdString = v.IdEmpresa.ToString("000") + v.IdSucursal.ToString("000"); });
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
