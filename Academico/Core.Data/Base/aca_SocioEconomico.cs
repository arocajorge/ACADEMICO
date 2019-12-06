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
    
    public partial class aca_SocioEconomico
    {
        public int IdEmpresa { get; set; }
        public int IdSocioEconomico { get; set; }
        public decimal IdAlumno { get; set; }
        public int IdCatalogoFichaVi { get; set; }
        public int IdCatalogoFichaTVi { get; set; }
        public int IdCatalogoFichaAg { get; set; }
        public bool TieneElectricidad { get; set; }
        public bool TieneHermanos { get; set; }
        public string NombreHermanos { get; set; }
        public double SueldoPadre { get; set; }
        public double SueldoMadre { get; set; }
        public double OtroIngreso { get; set; }
        public double GastoAlimentacion { get; set; }
        public double GastoEducacion { get; set; }
        public double GastoServicioBasico { get; set; }
        public double GastoSalud { get; set; }
        public double GastoArriendo { get; set; }
        public double GastoPrestamo { get; set; }
        public double OtroGasto { get; set; }
        public int IdCatalogoFichaMot { get; set; }
        public int IdCatalogoFichaIns { get; set; }
        public int IdCatalogoFichaFin { get; set; }
        public string OtroMotivoIngreso { get; set; }
        public string OtroInformacionInst { get; set; }
        public string OtroFinanciamiento { get; set; }
        public bool Estado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
    
        public virtual aca_Alumno aca_Alumno { get; set; }
        public virtual aca_CatalogoFicha aca_CatalogoFicha { get; set; }
        public virtual aca_CatalogoFicha aca_CatalogoFicha1 { get; set; }
        public virtual aca_CatalogoFicha aca_CatalogoFicha2 { get; set; }
        public virtual aca_CatalogoFicha aca_CatalogoFicha3 { get; set; }
        public virtual aca_CatalogoFicha aca_CatalogoFicha4 { get; set; }
        public virtual aca_CatalogoFicha aca_CatalogoFicha5 { get; set; }
    }
}
