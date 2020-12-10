using System;

namespace Core.Info.Reportes.Academico
{
    public class ACA_056_Info
    {
        public int Num { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdProfesor { get; set; }
        public int IdPersona { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string pe_nombreCompleto { get; set; }
        public Nullable<DateTime> pe_fechaNacimiento { get; set; }
        public string pe_sexo { get; set; }
        public string Sexo { get; set; }
        public Nullable<int> IdProfesion { get; set; }
        public string Profesion { get; set; }
        public string Direccion { get; set; }
        public string Telefonos { get; set; }
        public string Correo { get; set; }
        public string pe_celular { get; set; }
        public bool EsProfesor { get; set; }
        public bool EsInspector { get; set; }
    }
}
