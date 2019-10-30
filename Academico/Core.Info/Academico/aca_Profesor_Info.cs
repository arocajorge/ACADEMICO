using Core.Info.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_Profesor_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdProfesor { get; set; }
        public decimal IdPersona { get; set; }
        [StringLength(50, MinimumLength = 0, ErrorMessage = "el campo código debe tener máximo 50")]
        public string Codigo { get; set; }
        public bool Estado { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string Telefonos { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public string MotivoAnulacion { get; set; }

        #region Campos que no existen en la tablas
        public tb_persona_Info info_persona { get; set; }
        public string IdCiudad { get; set; }
        public string IdParroquia { get; set; }
        #endregion
    }
}
