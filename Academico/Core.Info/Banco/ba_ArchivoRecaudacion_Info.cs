using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Banco
{
    public class ba_ArchivoRecaudacion_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdArchivo { get; set; }
        [Required(ErrorMessage = "El campo banco es obligatorio")]
        public int IdBanco { get; set; }
        [Required(ErrorMessage = "El campo proceso bancario es obligatorio")]
        public int IdProceso_bancario { get; set; }
        public string Nom_Archivo { get; set; }
        public int SecuencialDescarga { get; set; }
        [Required(ErrorMessage = "El campo fecha es obligatorio")]
        public System.DateTime Fecha { get; set; }
        public bool Estado { get; set; }
        public string Observacion { get; set; }
        public double Valor { get; set; }
        public double ValorProntoPago { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        [Required(ErrorMessage = "El campo motivo de anulación es obligatorio")]
        public string MotivoAnulacion { get; set; }
        public Nullable<System.DateTime> FechaProceso { get; set; }
        public string IdUsuarioProceso { get; set; }

        #region Campos que no existen en la tabla
        public List<ba_ArchivoRecaudacionDet_Info> Lst_det { get; set; }
        public int IdSucursal { get; set; }
        #endregion
    }
}
