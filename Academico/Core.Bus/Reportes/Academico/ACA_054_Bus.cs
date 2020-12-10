﻿using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_054_Bus
    {
        ACA_054_Data odata = new ACA_054_Data();

        public List<ACA_054_Info> GetList(int IdEmpresa,int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, bool MostrarRetirados)
        {
            return odata.GetList(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, MostrarRetirados);
        }
    }
}
