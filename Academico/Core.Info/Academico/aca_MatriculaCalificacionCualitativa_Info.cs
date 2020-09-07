using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_MatriculaCalificacionCualitativa_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public int IdMateria { get; set; }
        public int IdCatalogoParcial { get; set; }
        public Nullable<decimal> IdProfesor { get; set; }
        public Nullable<int> IdCalificacionCualitativa { get; set; }
        public Nullable<int> Conducta { get; set; }
        public string MotivoConducta { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }


        #region Campos que no existen en la base
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public decimal IdAlumno { get; set; }
        public string Codigo { get; set; }
        public string pe_nombreCompleto { get; set; }
        public Nullable<int> IdCalificacionCualitativaParcial { get; set; }
        public string CodigoCalificacion { get; set; }
        public string DescripcionCorta { get; set; }
        public string Letra { get; set; }
        public int IdCatalogoTipo { get; set; }
        public bool RegistroValidoCalificacion { get; set; }
        public bool RegistroValidoConducta { get; set; }
        public bool RegistroValido { get; set; }
        public bool MostrarRetirados { get; set; }
        #endregion
    }
}
