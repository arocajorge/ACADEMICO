using System;

namespace Core.Info.Reportes.Academico
{
    public class ACA_059_Info
    {
        public int Num { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public string Tutor { get; set; }
        public string Inspector { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public int IdMateria { get; set; }
        public string Descripcion { get; set; }
        public string NomSede { get; set; }
        public string NomNivel { get; set; }
        public Nullable<int> OrdenNivel { get; set; }
        public string NomJornada { get; set; }
        public Nullable<int> OrdenJornada { get; set; }
        public Nullable<int> OrdenCurso { get; set; }
        public string NomCurso { get; set; }
        public string NomParalelo { get; set; }
        public Nullable<int> OrdenParalelo { get; set; }
        public bool EsRetirado { get; set; }
        public string pe_nombreCompleto { get; set; }
    }
}
