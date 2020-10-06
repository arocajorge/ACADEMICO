using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_AnioLectivo_Tematica_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public int IdAnio { get; set; }
        public int IdTematica { get; set; }
        public int IdCampoAccion { get; set; }
        public string NombreCampoAccion { get; set; }
        public string NombreTematica { get; set; }
        public Nullable<int> OrdenCampoAccion { get; set; }
        public Nullable<int> OrdenTematica { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }

        #region Campos que no existen en la tabla
        public bool seleccionado { get; set; }
        #endregion
    }
}
