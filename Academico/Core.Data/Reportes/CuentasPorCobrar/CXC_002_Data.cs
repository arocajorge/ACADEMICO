using Core.Data.Base;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.CuentasPorCobrar
{
    public class CXC_002_Data
    {
        public List<CXC_002_Info> get_list(int IdEmpresa, int IdSucursal, decimal IdCobro)
        {
            try
            {
                List<CXC_002_Info> Lista = new List<CXC_002_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    var lst = Context.VWCXC_002.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdCobro == IdCobro).ToList();
                    foreach (var item in lst)
                    {
                        Lista.Add(new CXC_002_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdSucursal = item.IdSucursal,
                            IdCobro = item.IdCobro,
                            IdAlumno = item.IdAlumno,
                            pe_nombreCompleto = item.pe_nombreCompleto,
                            cr_estado = item.cr_estado,
                            cr_fecha = item.cr_fecha,
                            tc_descripcion = item.tc_descripcion,
                            CodigoAlumno = item.CodigoAlumno,
                            cr_observacion = item.cr_observacion,
                            NomSede = item.NomSede,
                            NomNivel = item.NomNivel,
                            NomJornada = item.NomJornada,
                            NomCurso = item.NomCurso,
                            NomParalelo = item.NomParalelo,
                            CodigoParalelo = item.CodigoParalelo,
                            NomPlantilla = item.NomPlantilla,
                            cr_TotalCobro = item.cr_TotalCobro,
                            cr_Saldo = item.cr_Saldo,
                            NomCliente = item.NomCliente,
                            CedulaCliente = item.CedulaCliente,
                            cr_Banco = item.cr_Banco,
                            cr_NumDocumento = item.cr_NumDocumento
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
    }
}
