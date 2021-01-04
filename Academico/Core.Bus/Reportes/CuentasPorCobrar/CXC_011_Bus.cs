﻿using Core.Data.Reportes.CuentasPorCobrar;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.CuentasPorCobrar
{
    public class CXC_011_Bus
    {
        CXC_011_Data odata = new CXC_011_Data();
        public List<CXC_011_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdJornada, int IdNivel, int IdCurso, int IdParalelo, decimal IdAlumno)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdAnio, IdSede, IdJornada, IdNivel, IdCurso, IdParalelo, IdAlumno);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
