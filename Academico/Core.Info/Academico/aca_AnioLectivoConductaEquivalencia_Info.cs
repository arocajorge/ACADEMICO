using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_AnioLectivoConductaEquivalencia_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "El campo año lectivo es obligatorio")]
        public int IdAnio { get; set; }
        public int Secuencia { get; set; }
        [Required(ErrorMessage = "El campo letra es obligatorio")]
        public string Letra { get; set; }
        [Required(ErrorMessage = "El campo calificación es obligatorio")]
        public decimal Calificacion { get; set; }

        #region Campos que no existen en la tabla
        public string Descripcion { get; set; }
        #endregion
    }
}
