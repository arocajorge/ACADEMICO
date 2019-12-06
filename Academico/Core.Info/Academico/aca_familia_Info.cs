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
        public int IdCatalogoFichaInst { get; set; }
        [StringLength(200, MinimumLength = 0, ErrorMessage = "el campo empresa debe tener máximo 200")]
        public string EmpresaTrabajo { get; set; }
        [StringLength(500, MinimumLength = 0, ErrorMessage = "el campo dirección debe tener máximo 500")]
        public string DireccionTrabajo { get; set; }
        [StringLength(200, MinimumLength = 0, ErrorMessage = "el campo telefono debe tener máximo 200")]
        public string TelefonoTrabajo { get; set; }
        [StringLength(200, MinimumLength = 0, ErrorMessage = "el campo cargo debe tener máximo 200")]
        public string CargoTrabajo { get; set; }
        public Nullable<int> AniosServicio { get; set; }
        public Nullable<double> IngresoMensual { get; set; }
        public bool VehiculoPropio { get; set; }
        [StringLength(50, MinimumLength = 0, ErrorMessage = "el campo marca debe tener máximo 50")]
        public string Marca { get; set; }
        [StringLength(50, MinimumLength = 0, ErrorMessage = "el campo modelo debe tener máximo 50")]
        public string Modelo { get; set; }
        public bool CasaPropia { get; set; }
        public bool SeFactura { get; set; }
        public bool EsRepresentante { get; set; }
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
        [Required(ErrorMessage = "El campo tipo de documento es obligatorio")]
        public string IdTipoDocumento { get; set; }
        [Required(ErrorMessage = "El campo naturaleza es obligatorio")]
        public string pe_Naturaleza { get; set; }
        [Required(ErrorMessage = "El campo nombre completo es obligatorio")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "el campo nombre completo debe tener mínimo 2 caracter y máximo 200 caracteres")]
        public string pe_nombreCompleto { get; set; }
        [StringLength(100, MinimumLength = 0, ErrorMessage = "el campo apellidos debe tener máximo 100 caracteres")]
        public string pe_apellido { get; set; }
        [StringLength(100, MinimumLength = 0, ErrorMessage = "el campo nombres debe tener máximo 100 caracteres")]
        public string pe_nombre { get; set; }
        [Required(ErrorMessage = "El campo numero de documento es obligatorio")]
        public string pe_cedulaRuc { get; set; }
        public string pe_telfono_Contacto { get; set; }
        [StringLength(150, MinimumLength = 0, ErrorMessage = "el campo razón social debe tener máximo 150 caracteres")]
        public string pe_razonSocial { get; set; }
        public string pe_sexo { get; set; }
        public string IdEstadoCivil { get; set; }
        public Nullable<System.DateTime> pe_fechaNacimiento { get; set; }
        public string CodCatalogoSangre { get; set; }
        public string CodCatalogoCONADIS { get; set; }
        public Nullable<double> PorcentajeDiscapacidad { get; set; }
        [StringLength(200, MinimumLength = 0, ErrorMessage = "el campo numero de carnet de conadis debe tener máximo 200 caracteres")]
        public string NumeroCarnetConadis { get; set; }
        public string anio { get; set; }
        public string mes { get; set; }
        public string dia { get; set; }
        public string NomCatalogo { get; set; }
        public int IdProfesion { get; set; }
        public tb_persona_Info info_persona { get; set; }
        #endregion
    }
}
