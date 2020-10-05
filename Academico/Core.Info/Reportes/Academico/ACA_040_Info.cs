using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_040_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdAlumno { get; set; }
        public decimal IdMatricula { get; set; }
        public string NombreAlumno { get; set; }
        public string pe_cedulaRuc { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public string Descripcion { get; set; }
        public string NomSede { get; set; }
        public string NomNivel { get; set; }
        public string NomJornada { get; set; }
        public string NomCurso { get; set; }
        public string NomParalelo { get; set; }
        public Nullable<int> OrdenJornada { get; set; }
        public Nullable<int> OrdenNivel { get; set; }
        public Nullable<int> OrdenCurso { get; set; }
        public Nullable<int> OrdenParalelo { get; set; }
        public string Letra { get; set; }
        public string Equivalencia { get; set; }
        public string NomCatalogoTipo { get; set; }
        public string NomCatalogo { get; set; }

        #region Campos que no existen
        public string FechaActual { get; set; }
        public string DescripcionParcial { get; set; }
        #endregion
    }
}
