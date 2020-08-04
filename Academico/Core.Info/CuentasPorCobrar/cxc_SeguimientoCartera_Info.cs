using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.CuentasPorCobrar
{
    public class cxc_SeguimientoCartera_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSeguimiento { get; set; }
        public Nullable<decimal> IdMatricula { get; set; }
        public bool CorreoEnviado { get; set; }
        [Required(ErrorMessage = "El campo estudiante es obligatorio")]
        public decimal IdAlumno { get; set; }
        [Required(ErrorMessage = "El campo fecha es obligatorio")]
        public System.DateTime Fecha { get; set; }
        [Required(ErrorMessage = "El campo observación es obligatorio")]
        public string Observacion { get; set; }
        public bool Estado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        [Required(ErrorMessage = "El campo motivo de anulación es obligatorio")]
        public string MotivoAnulacion { get; set; }

        #region Campos que no existe en la tabla
        public string Codigo { get; set; }
        public string NombreAlumno { get; set; }
        public string DatosAcademicos { get; set; }
        public string RepLegal { get; set; }
        public string TelefonoLegal { get; set; }
        public string CorreoLegal { get; set; }
        public string RepEconomico { get; set; }
        public string TelefonoEconomico { get; set; }
        public string CorreoEconomico { get; set; }
        public string Saldo { get; set; }
        public string SaldoProntoPago { get; set; }
        public byte[] seguimiento_foto { get; set; }
        public string NomPlantillaTipo { get; set; }
        public string CelularEmiteFactura { get; set; }
        public string CelularRepresentante { get; set; }
        public List<cxc_SeguimientoCartera_Info> lst_det { get; set; }
        #endregion
    }
}
