using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_048_Rendimiento_Info
    {
        public int IdEmpresa { get; set; }
        public int IdAnio { get; set; }
        public int IdCalificacionCualitativa { get; set; }
        public string Codigo { get; set; }
        public string DescripcionCorta { get; set; }
        public string DescripcionLarga { get; set; }
        public decimal Calificacion { get; set; }
        public bool Estado { get; set; }
        public Nullable<int> ContadorTotal { get; set; }
        public  Nullable<int> AlumnosConCalificacion { get; set; }
        public Nullable<decimal>Porcentaje { get; set; }

    }
}
