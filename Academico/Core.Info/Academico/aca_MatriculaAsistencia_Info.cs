using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_MatriculaAsistencia_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public Nullable<int> FInjustificadaP1 { get; set; }
        public Nullable<int> FJustificadaP1 { get; set; }
        public Nullable<int> AtrasosP1 { get; set; }
        public Nullable<int> FInjustificadaP2 { get; set; }
        public Nullable<int> FJustificadaP2 { get; set; }
        public Nullable<int> AtrasosP2 { get; set; }
        public Nullable<int> FInjustificadaP3 { get; set; }
        public Nullable<int> FJustificadaP3 { get; set; }
        public Nullable<int> AtrasosP3 { get; set; }
        public Nullable<int> FInjustificadaP4 { get; set; }
        public Nullable<int> FJustificadaP4 { get; set; }
        public Nullable<int> AtrasosP4 { get; set; }
        public Nullable<int> FInjustificadaP5 { get; set; }
        public Nullable<int> FJustificadaP5 { get; set; }
        public Nullable<int> AtrasosP5 { get; set; }
        public Nullable<int> FInjustificadaP6 { get; set; }
        public Nullable<int> FJustificadaP6 { get; set; }
        public Nullable<int> AtrasosP6 { get; set; }
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
        public Nullable<int> FInjustificada { get; set; }
        public Nullable<int> FJustificada { get; set; }
        public Nullable<int> Atrasos { get; set; }
        public int IdCatalogoParcial { get; set; }
        public int IdCatalogoTipo { get; set; }
        public int IdMateria { get; set; }
        #endregion
    }
}
