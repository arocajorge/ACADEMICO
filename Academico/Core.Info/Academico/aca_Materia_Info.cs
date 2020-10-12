using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_Materia_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public int IdMateria { get; set; }
        public Nullable<int> IdMateriaGrupo { get; set; }
        public Nullable<int> IdMateriaArea { get; set; }
        [StringLength(500, MinimumLength = 1, ErrorMessage = "el campo materia debe tener mínimo 1 caracter y máximo 500")]
        [Required(ErrorMessage = "El campo materia es obligatorio")]
        public string NomMateria { get; set; }
        public bool EsObligatorio { get; set; }
        [Required(ErrorMessage = "El campo orden es obligatorio")]
        public int OrdenMateria { get; set; }
        [Required(ErrorMessage = "El campo tipo de calificación es obligatorio")]
        public Nullable<int> IdCatalogoTipoCalificacion { get; set; }
        public bool Estado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        [Required(ErrorMessage = "El campo motivo de anulación es obligatorio")]
        public string MotivoAnulacion { get; set; }

        #region Campos que no existen en la tabla
        public string NomMateriaGrupo { get; set; }
        public string NomMateriaArea { get; set; }
        public int OrdenMateriaGrupo { get; set; }
        public int OrdenMateriaArea { get; set; }
        public bool PromediarGrupo { get; set; }
        #endregion
    }
}
