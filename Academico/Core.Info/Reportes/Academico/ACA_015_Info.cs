using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_015_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public decimal IdAlumno { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public int IdPlantilla { get; set; }
        public string NomPlantilla { get; set; }
        public string CodigoAlumno { get; set; }
        public string NombreAlumno { get; set; }
        public string IdentificacionAlumno { get; set; }
        public Nullable<System.DateTime> FechaNacAlumno { get; set; }
        public string NomSede { get; set; }
        public string NomAnioLectivo { get; set; }
        public string NomNivel { get; set; }
        public Nullable<int> OrdenNivel { get; set; }
        public string NomJornada { get; set; }
        public Nullable<int> OrdenJornada { get; set; }
        public string NomCurso { get; set; }
        public Nullable<int> OrdenCurso { get; set; }
        public string NomParalelo { get; set; }
        public Nullable<int> OrdenParalelo { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string Direccion { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public string pe_telfono_Contacto { get; set; }
        public string Descripcion { get; set; }



        public int Num { get; set; }
    }
}
