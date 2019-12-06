using Core.Data.General;
using Core.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.General
{
   public class tbl_TransaccionesAutorizadas_Bus
    {
        tbl_TransaccionesAutorizadas_Data odata = new tbl_TransaccionesAutorizadas_Data();
        public bool guardarDB(tbl_TransaccionesAutorizadas_info info)
        {
            try
            {
                return odata.guardarDB(info);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
    }
