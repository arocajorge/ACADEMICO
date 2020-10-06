using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_MatriculaCalificacionCualitativaPromedio_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public int IdMateria { get; set; }
        public Nullable<decimal> IdProfesor { get; set; }
        public Nullable<int> IdCalificacionCualitativaQ1 { get; set; }
        public Nullable<decimal> PromedioQ1 { get; set; }
        public Nullable<int> IdCalificacionCualitativaQ2 { get; set; }
        public Nullable<decimal> PromedioQ2 { get; set; }
        public Nullable<int> IdCalificacionCualitativaFinal { get; set; }
        public Nullable<decimal> PromedioFinal { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
    }
}
