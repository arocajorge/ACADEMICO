using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Info.Academico
{
    public class aca_Menu_Info
    {
        [Key]
        public int IdMenu { get; set; }
        public Nullable<int> IdMenuPadre { get; set; }
        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "El campo descripción debe tener mínimo 1 caracter y máximo 500")]
        public string DescripcionMenu { get; set; }
        [Required(ErrorMessage = "El campo posición es obligatorio")]
        public int PosicionMenu { get; set; }
        public bool Estado { get; set; }
        public Nullable<int> nivel { get; set; }
        [StringLength(200, MinimumLength = 0, ErrorMessage = "El campo área debe tener mínimo 0 caracter y máximo 200")]
        public string web_nom_Area { get; set; }
        [StringLength(200, MinimumLength = 0, ErrorMessage = "El campo controlador debe tener mínimo 0 caracter y máximo 200")]
        public string web_nom_Controller { get; set; }
        [StringLength(300, MinimumLength = 0, ErrorMessage = "El campo acción debe tener mínimo 0 caracter y máximo 300")]
        public string web_nom_Action { get; set; }

        //Campos que no existen en la base
        public string DescripcionMenu_combo { get; set; }
    }
}
