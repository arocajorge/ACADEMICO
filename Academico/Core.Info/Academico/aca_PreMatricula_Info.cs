﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_PreMatricula_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdPreMatricula { get; set; }
        public decimal IdAdmision { get; set; }
        public string Codigo { get; set; }
        public decimal IdAlumno { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public decimal IdPersonaF { get; set; }
        public decimal IdPersonaR { get; set; }
        public int IdPlantilla { get; set; }
        public decimal IdMecanismo { get; set; }
        public Nullable<int> IdCatalogoESTPREMAT { get; set; }
        public System.DateTime Fecha { get; set; }
        public int IdPuntoVta { get; set; }
        public int IdSucursal { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorProntoPago { get; set; }
        public string Observacion { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public string MotivoAnulacion { get; set; }
        public Nullable<int> IdEmpresa_rol { get; set; }
        public Nullable<decimal> IdEmpleado { get; set; }
        public Nullable<bool> EsPatrocinado { get; set; }

        #region Campos que no existen en la tabla
        public bool ExisteAlumno { get; set; }
        public string IdComboCurso { get; set; }
        public aca_Admision_Info info_admision { get; set; }
        public aca_Alumno_Info info_alumno { get; set; }
        public List<aca_Matricula_Info> lst_matricula_curso { get; set; }
        public List<aca_PreMatricula_Rubro_Info> lst_PreMatriculaRubro { get; set; }
        public List<aca_AlumnoDocumento_Info> lst_Documentos { get; set; }
        public List<aca_AnioLectivo_Curso_Documento_Info> lst_alumnoDocumentos { get; set; }
        public aca_SocioEconomico_Info info_socioeconomico { get; set; }
        public bool info_valido_padre { get; set; }
        public bool info_valido_madre { get; set; }
        public bool info_valido_representante { get; set; }
        public bool OtraPersonaFamiliar { get; set; }
        public int IdCatalogoPAREN_OtroFamiliar { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal ValorPago { get; set; }
        public string CodigoAlumno { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string pe_nombreCompleto { get; set; }
        public bool ValidaImportacionPreMatricula { get; set; }
        public string IdString { get; set; }
        public decimal IdMatricula { get; set; }

        public string NomSede { get; set; }
        public string NomNivel { get; set; }
        public string NomJornada { get; set; }
        public string NomParalelo { get; set; }
        public string Descripcion { get; set; }
        public string NomCurso { get; set; }
        public int OrdenJornada { get; set; }
        public int OrdenNivel { get; set; }
        public int OrdenCurso { get; set; }
        public int OrdenParalelo { get; set; }
        public string NomPlantilla { get; set; }
        public string NomPlantillaTipo { get; set; }
        #endregion
    }
}
