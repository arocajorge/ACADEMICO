using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_028_Promedios_Info
    {
        public int IdEmpresa { get; set; }
        public int IdMateria { get; set; }
        public Nullable<decimal> IdProfesor { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public string Descripcion { get; set; }
        public string NomSede { get; set; }
        public string NomNivel { get; set; }
        public Nullable<int> OrdenNivel { get; set; }
        public string NomJornada { get; set; }
        public Nullable<int> OrdenJornada { get; set; }
        public string NomCurso { get; set; }
        public Nullable<int> OrdenCurso { get; set; }
        public string CodigoParalelo { get; set; }
        public string NomParalelo { get; set; }
        public Nullable<int> OrdenParalelo { get; set; }
        public string NombreProfesor { get; set; }
        public string NomMateria { get; set; }
        public Nullable<int> OrdenMateriaGrupo { get; set; }
        public Nullable<int> OrdenMateria { get; set; }
        public Nullable<int> IdCatalogoTipoCalificacion { get; set; }
        public string Quim1 { get; set; }
        public string PorcentajeQuim1 { get; set; }
        public string EXQuim1 { get; set; }
        public string Quim2 { get; set; }
        public string PorcentajeQuim2 { get; set; }
        public string EXQuim2 { get; set; }

        #region Campos que no existen
        public string PromedioQuimestre { get; set; }
        public string PromedioExamen { get; set; }
        public string PorcentajePromedio { get; set; }
        #endregion
    }
}
