//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Core.Data.Base
{
    using System;
    using System.Collections.Generic;
    
    public partial class vwaca_Alumno
    {
        public int IdEmpresa { get; set; }
        public decimal IdAlumno { get; set; }
        public string Codigo { get; set; }
        public decimal IdPersona { get; set; }
        public string pe_Naturaleza { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string pe_apellido { get; set; }
        public string pe_nombre { get; set; }
        public string IdTipoDocumento { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string Direccion { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public string pe_sexo { get; set; }
        public Nullable<System.DateTime> pe_fechaNacimiento { get; set; }
        public string CodCatalogoSangre { get; set; }
        public string CodCatalogoCONADIS { get; set; }
        public Nullable<double> PorcentajeDiscapacidad { get; set; }
        public string NumeroCarnetConadis { get; set; }
        public Nullable<bool> Estado { get; set; }
        public int IdCatalogoESTMAT { get; set; }
        public Nullable<int> IdCurso { get; set; }
        public int IdCatalogoESTALU { get; set; }
        public string pe_telfono_Contacto { get; set; }
        public string NomCatalogoESTMAT { get; set; }
        public string NomCatalogoESTALU { get; set; }
        public System.DateTime FechaIngreso { get; set; }
    }
}
