using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_Plantilla_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "El campo año lectivo es obligatorio")]
        public int IdAnio { get; set; }
        public int IdPlantilla { get; set; }
        [StringLength(500, MinimumLength = 1, ErrorMessage = "el campo nombre debe tener mínimo 1 caracter y máximo 500")]
        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        public string NomPlantilla { get; set; }
        [Required(ErrorMessage = "El campo valor es obligatorio")]
        public decimal Valor { get; set; }
        public bool Estado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        [Required(ErrorMessage = "El campo motivo de anulación es obligatorio")]
        public string MotivoAnulacion { get; set; }

        #region Campos que no existen
        public List<aca_Plantilla_Rubro_Info> lst_Plantilla_Rubro { get; set; }
        
        #endregion
    }
}
