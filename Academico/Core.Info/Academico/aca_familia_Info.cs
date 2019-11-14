using Core.Info.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_Familia_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "El campo alumno es obligatorio")]
        public decimal IdAlumno { get; set; }
        public int Secuencia { get; set; }
        [Required(ErrorMessage = "El campo parentezco es obligatorio")]
        public int IdCatalogoPAREN { get; set; }
        public decimal IdPersona { get; set; }
        public string Direccion { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public bool SeFactura { get; set; }
        public bool EsRepresentante { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        [Required(ErrorMessage = "El campo motivo de anulación es obligatorio")]
        public string MotivoAnulacion { get; set; }


        #region Campos que no existen en la tabla
        [Required(ErrorMessage = "El campo tipo de documento es obligatorio")]
        public string IdTipoDocumento { get; set; }
        [Required(ErrorMessage = "El campo naturaleza es obligatorio")]
        public string pe_Naturaleza { get; set; }
        public string pe_nombreCompleto { get; set; }
        [Required(ErrorMessage = "El campo apellido es obligatorio")]
        public string pe_apellido { get; set; }
        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        public string pe_nombre { get; set; }
        [Required(ErrorMessage = "El campo numero de documento es obligatorio")]
        public string pe_cedulaRuc { get; set; }
        public string pe_telfono_Contacto { get; set; }
        public string pe_sexo { get; set; }
        public string IdEstadoCivil { get; set; }
        public Nullable<System.DateTime> pe_fechaNacimiento { get; set; }
        public string CodCatalogoSangre { get; set; }
        public string CodCatalogoCONADIS { get; set; }
        public Nullable<double> PorcentajeDiscapacidad { get; set; }
        public string NumeroCarnetConadis { get; set; }
        public string anio { get; set; }
        public string mes { get; set; }
        public string dia { get; set; }
        public string NomCatalogo { get; set; }

        public tb_persona_Info info_persona { get; set; }
        #endregion
    }
}
