﻿using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_011_Bus
    {
        ACA_011_Data odata = new ACA_011_Data();
        public List<ACA_011_Info> GetList(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
