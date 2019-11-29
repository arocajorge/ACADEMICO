using Core.Info.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_Alumno_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdAlumno { get; set; }
        [StringLength(50, MinimumLength = 0, ErrorMessage = "el campo código debe tener máximo 50")]
        public string Codigo { get; set; }
        public decimal IdPersona { get; set; }
        public string Direccion { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public Nullable<bool> Estado { get; set; }
        public int IdCatalogoESTMAT { get; set; }
        public Nullable<int> IdCurso { get; set; }
        public int IdCatalogoESTALU { get; set; }
        public string MotivoNoMatricula { get; set; }
        public System.DateTime FechaIngreso { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        [Required(ErrorMessage = "El campo motivo de anulación es obligatorio")]
        public string MotivoAnulacion { get; set; }

        #region Campos que no existen en la tabla
        public byte[] alu_foto { get; set; }
        public string NomCatalogoESTALU { get; set; }
        public string NomCatalogoESTMAT { get; set; }

        #region Alumno
        [Required(ErrorMessage = "El campo tipo de documento es obligatorio")]
        public string IdTipoDocumento { get; set; }
        [Required(ErrorMessage = "El campo naturaleza es obligatorio")]
        public string pe_Naturaleza { get; set; }
        [StringLength(200, MinimumLength = 1, ErrorMessage = "el campo nombre completo debe tener mínimo 1 caracter y máximo 200 caracteres")]
        public string pe_nombreCompleto { get; set; }
        [StringLength(100, MinimumLength = 0, ErrorMessage = "el campo apellidos debe tener máximo 100 caracteres")]
        public string pe_apellido { get; set; }
        [StringLength(100, MinimumLength = 0, ErrorMessage = "el campo nombres debe tener máximo 100 caracteres")]
        public string pe_nombre { get; set; }
        [Required(ErrorMessage = "El campo numero de documento es obligatorio")]
        public string pe_cedulaRuc { get; set; }
        public string pe_telfono_Contacto { get; set; }
        public string pe_sexo { get; set; }
        public Nullable<System.DateTime> pe_fechaNacimiento { get; set; }
        public string CodCatalogoSangre { get; set; }
        public string CodCatalogoCONADIS { get; set; }
        public Nullable<double> PorcentajeDiscapacidad { get; set; }
        public string NumeroCarnetConadis { get; set; }
        public List<aca_AlumnoDocumento_Info> lst_alumno_documentos { get; set; }
        #endregion

        #region Datos padre
        public int IdCatalogo_padre { get; set; }
        public decimal IdPersona_padre { get; set; }
        public string IdTipoDocumento_padre { get; set; }
        public string pe_Naturaleza_padre { get; set; }
        [StringLength(200, MinimumLength = 1, ErrorMessage = "el campo nombre completo debe tener mínimo 1 caracter y máximo 200 caracteres")]
        public string pe_nombreCompleto_padre { get; set; }
        [StringLength(100, MinimumLength = 0, ErrorMessage = "el campo apellidos debe tener máximo 100 caracteres")]
        public string pe_apellido_padre { get; set; }
        [StringLength(100, MinimumLength = 0, ErrorMessage = "el campo nombres debe tener máximo 100 caracteres")]
        public string pe_nombre_padre { get; set; }
        public string pe_cedulaRuc_padre { get; set; }
        [StringLength(150, MinimumLength = 0, ErrorMessage = "el campo razón social debe tener máximo 150 caracteres")]
        public string pe_razonSocial_padre { get; set; }
        public string Direccion_padre { get; set; }
        public string pe_telfono_Contacto_padre { get; set; }
        public string Celular_padre { get; set; }
        public string Correo_padre { get; set; }
        public string pe_sexo_padre { get; set; }
        public string IdEstadoCivil_padre { get; set; }
        public Nullable<System.DateTime> pe_fechaNacimiento_padre { get; set; }
        public string CodCatalogoSangre_padre { get; set; }
        public string CodCatalogoCONADIS_padre { get; set; }
        public Nullable<double> PorcentajeDiscapacidad_padre { get; set; }
        public string NumeroCarnetConadis_padre { get; set; }
        public bool SeFactura_padre { get; set; }
        public bool EsRepresentante_padre { get; set; }
        public bool info_valido_padre { get; set; }
        #endregion

        #region Datos madre
        public int IdCatalogo_madre { get; set; }
        public decimal IdPersona_madre { get; set; }
        public string IdTipoDocumento_madre { get; set; }
        public string pe_Naturaleza_madre { get; set; }
        [StringLength(200, MinimumLength = 1, ErrorMessage = "el campo nombre completo debe tener mínimo 1 caracter y máximo 200 caracteres")]
        public string pe_nombreCompleto_madre { get; set; }
        [StringLength(150, MinimumLength = 0, ErrorMessage = "el campo razón social debe tener máximo 150 caracteres")]
        public string pe_razonSocial_madre { get; set; }
        [StringLength(100, MinimumLength = 0, ErrorMessage = "el campo apellidos debe tener máximo 100 caracteres")]
        public string pe_apellido_madre { get; set; }
        [StringLength(100, MinimumLength = 0, ErrorMessage = "el campo nombres debe tener máximo 100 caracteres")]
        public string pe_nombre_madre { get; set; }
        public string pe_cedulaRuc_madre { get; set; }
        public string Direccion_madre { get; set; }
        public string pe_telfono_Contacto_madre { get; set; }
        public string Celular_madre { get; set; }
        public string Correo_madre { get; set; }
        public string pe_sexo_madre { get; set; }
        public string IdEstadoCivil_madre { get; set; }
        public Nullable<System.DateTime> pe_fechaNacimiento_madre { get; set; }
        public string CodCatalogoSangre_madre { get; set; }
        public string CodCatalogoCONADIS_madre { get; set; }
        public Nullable<double> PorcentajeDiscapacidad_madre { get; set; }
        public string NumeroCarnetConadis_madre { get; set; }
        public bool SeFactura_madre { get; set; }
        public bool EsRepresentante_madre { get; set; }
        public bool info_valido_madre { get; set; }
        #endregion

        public tb_persona_Info info_persona_alumno { get; set; }
        public tb_persona_Info info_persona_padre { get; set; }
        public tb_persona_Info info_persona_madre { get; set; }
        public string anio { get; set; }
        public string mes { get; set; }
        public string dia { get; set; }
        #endregion
    }
}
