using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_028_PromedioXSexo_Info
    {
        public int IdEmpresa { get; set; }
        public string Sexo { get; set; }
        public Nullable<int> CantQ1 { get; set; }
        public Nullable<int> CantQ2 { get; set; }
        public Nullable<decimal> PromedioFinalQ1 { get; set; }
        public Nullable<decimal> PromedioFinalQ2 { get; set; }

        #region Campos que no existen
        public Nullable<int> Cantidad { get; set; }
        public Nullable<decimal> PromedioFinal { get; set; }
        #endregion
    }
}
