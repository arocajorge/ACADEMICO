﻿using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_042_Pagos_Bus
    {
        ACA_042_Pagos_Data odata = new ACA_042_Pagos_Data();
        public List<ACA_042_Pagos_Info> GetList(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdAlumno);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
