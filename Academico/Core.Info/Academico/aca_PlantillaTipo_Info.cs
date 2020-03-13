using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_PlantillaTipo_Info
    {
        public decimal IdTransaccionSession;

        public int IdEmpresa { get; set; }
        public int IdTipoPlantilla { get; set; }
        [Required(ErrorMessage ="El campo nombre de tipo de plantilla es obligatorio")]
        [StringLength(500,ErrorMessage ="La longitud máxima para el campo nombre de tipo de plantilla es de 500 caracteres")]
        public string NomPlantillaTipo { get; set; }
        public bool Estado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        [Required(ErrorMessage = "El campo motivo anulación es obligatorio")]
        public string MotivoAnulacion { get; set; }
    }
}
