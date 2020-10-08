using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_048_SinPromedios_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public decimal IdAlumno { get; set; }
        public string Codigo { get; set; }
        public string pe_nombreCompleto { get; set; }
        public int IdMateria { get; set; }
        public string NomMateria { get; set; }
        public Nullable<int> OrdenMateriaGrupo { get; set; }
        public Nullable<int> OrdenMateria { get; set; }
        public Nullable<int> IdCalificacionCualitativaQ1 { get; set; }
        public Nullable<int> IdCalificacionCualitativaQ2 { get; set; }
        public Nullable<decimal> PromedioQ1 { get; set; }
        public Nullable<decimal> PromedioQ2 { get; set; }

        #region Campos que no existen
        public Nullable<decimal> PromedioQuimestral { get; set; }
        #endregion


    }
}
