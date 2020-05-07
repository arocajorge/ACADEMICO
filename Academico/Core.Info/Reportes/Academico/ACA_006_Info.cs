using System;

namespace Core.Info.Reportes.Academico
{
   public class ACA_006_Info
    {
        public string NomSede { get; set; }
        public string NomNivel { get; set; }
        public int OrdenNivel { get; set; }
        public string NomJornada { get; set; }
        public int OrdenJornada { get; set; }
        public int OrdenCurso { get; set; }
        public string NomCurso { get; set; }
        public string CodigoParalelo { get; set; }
        public string NomParalelo { get; set; }
        public int OrdenParalelo { get; set; }
        public string pe_sexo { get; set; }
        public string NomSexo { get; set; }
        public int Cantidad { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public Nullable<bool> EsRetirado { get; set; }
        public string EsRetiradoString { get; set; }
    }
}
