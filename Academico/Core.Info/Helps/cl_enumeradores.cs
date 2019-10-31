using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Helps
{
    public class cl_enumeradores
    {
        public enum eTipoCatalogoGeneral
        {
            SEXO = 1,
            ESTCIVIL = 2,
            TIPODOC = 3,
            TIPONATPER = 5,
            TIP_CTA_AC = 27,
            TIPOSANGRE = 6,
            TIPODISCAP = 7
        }

        public enum eTipoSexoGeneral
        {
            SEXO_FEM,
            SEXO_MAS
        }

        public enum Parentezco
        {
            PAPA = 5,
            MAMA = 6
        }
    }
}
