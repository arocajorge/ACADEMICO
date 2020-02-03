using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_AnioLectivoCalificacionHistorico_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "El campo año lectivo es obligatorio")]
        public int IdAnio { get; set; }
        [Required(ErrorMessage = "El campo alumno es obligatorio")]
        public decimal IdAlumno { get; set; }
        [Required(ErrorMessage = "El campo curso es obligatorio")]
        public int IdCurso { get; set; }
        [Required(ErrorMessage = "El campo nivel es obligatorio")]
        public int IdNivel { get; set; }
        [StringLength(500, MinimumLength = 1, ErrorMessage = "el campo antigua institución debe tener mínimo 1 caracter y máximo 500")]
        [Required(ErrorMessage = "El campo antigua institución es obligatorio")]
        public string AntiguaInstitucion { get; set; }
        [Required(ErrorMessage = "El campo promedio es obligatorio")]
        public decimal Promedio { get; set; }
        [Required(ErrorMessage = "El campo conducta es obligatorio")]
        public decimal Conducta { get; set; }

        #region Campos que no existen en la tabla
        public string Descripcion { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string NomNivel { get; set; }
        public string NomCurso { get; set; }
        public string IdString { get; set; }
        public string Letra { get; set; }
        #endregion
    }
}
