using System;

namespace Core.Info.Reportes.Academico
{
    public class ACA_060_Info
    {
        public int Num { get; set; }
        public int IdEmpresa { get; set; }
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
        public Nullable<int> OrdenCurso { get; set; }
        public string NomCurso { get; set; }
        public string NomParalelo { get; set; }
        public Nullable<int> OrdenParalelo { get; set; }
        public decimal IdProfesor { get; set; }
        public string NombreProfesor { get; set; }
        public int IdTematica { get; set; }
        public int CampoAccion { get; set; }
        public string NombreCampoAccion { get; set; }
        public string NombreTematica { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string Correo { get; set; }
    }
}
