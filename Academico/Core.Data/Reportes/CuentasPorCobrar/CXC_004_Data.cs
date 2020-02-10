using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.CuentasPorCobrar
{
    public class CXC_004_Data
    {
        public List<CXC_004_Info> Getlist(int IdEmpresa, string IdUsuario, DateTime FechaCorte)
        {
            try
            {
                 

                List < CXC_004_Info > Lista = new List<CXC_004_Info>();




                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
       
    }
}
