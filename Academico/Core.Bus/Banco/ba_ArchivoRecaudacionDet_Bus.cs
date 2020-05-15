using Core.Data.Banco;
using Core.Info.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Banco
{
    public class ba_ArchivoRecaudacionDet_Bus
    {
        ba_ArchivoRecaudacionDet_Data odata = new ba_ArchivoRecaudacionDet_Data();
        public List<ba_ArchivoRecaudacionDet_Info> GetList(int IdEmpresa, decimal IdArchivo)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdArchivo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
