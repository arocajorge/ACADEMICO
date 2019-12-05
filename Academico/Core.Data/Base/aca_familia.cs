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
    
    public partial class aca_Familia
    {
        public int IdEmpresa { get; set; }
        public decimal IdAlumno { get; set; }
        public int Secuencia { get; set; }
        public int IdCatalogoPAREN { get; set; }
        public decimal IdPersona { get; set; }
        public string Direccion { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public int IdCatalogoFichaInst { get; set; }
        public string EmpresaTrabajo { get; set; }
        public string DireccionTrabajo { get; set; }
        public string TelefonoTrabajo { get; set; }
        public string CargoTrabajo { get; set; }
        public Nullable<int> AniosServicio { get; set; }
        public Nullable<double> IngresoMensual { get; set; }
        public bool VehiculoPropio { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public bool CasaPropia { get; set; }
        public bool SeFactura { get; set; }
        public bool EsRepresentante { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public string MotivoAnulacion { get; set; }
    
        public virtual aca_CatalogoFicha aca_CatalogoFicha { get; set; }
        public virtual aca_Alumno aca_Alumno { get; set; }
    }
}
