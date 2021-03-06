﻿using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_007_Bus
    {
        ACA_007_Data odata = new ACA_007_Data();
        public List<ACA_007_Info> GetList(int IdEmpresa, int IdSede, int IdAnio, int IdJornada, int IdNivel, int IdCurso, int IdParalelo, DateTime fecha_ini, DateTime fecha_fin, bool MostrarAlumnosRetirados)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSede, IdAnio, IdJornada, IdNivel, IdCurso, IdParalelo, fecha_ini, fecha_fin, MostrarAlumnosRetirados);
            }
            catch (Exception)
            {

                throw;
            }
        }

       
    }
}
