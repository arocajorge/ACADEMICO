﻿using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_014_Conducta_Bus
    {
        ACA_014_Conducta_Data odata = new ACA_014_Conducta_Data();
        public List<ACA_014_Conducta_Info> GetList(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdMatricula);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
