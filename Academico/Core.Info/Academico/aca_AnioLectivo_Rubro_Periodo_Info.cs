using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_AnioLectivo_Rubro_Periodo_Info
    {
        public int IdEmpresa { get; set; }
        public int IdAnio { get; set; }
        public int IdRubro { get; set; }
        public int IdPeriodo { get; set; }
        public Nullable<System.DateTime> FechaFacturacion { get; set; }
        public Nullable<System.DateTime> FechaPago { get; set; }

        #region Campos que no existen en la tabla
        public string IdString { get; set; }
        public bool seleccionado { get; set; }
        public string NomPeriodo { get; set; }
        public System.DateTime FechaDesde { get; set; }
        public System.DateTime FechaHasta { get; set; }
        #endregion
    }
}
