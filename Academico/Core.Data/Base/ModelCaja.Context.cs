﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EntitiesCaja : DbContext
    {
        public EntitiesCaja()
            : base("name=EntitiesCaja")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<caj_Caja_Movimiento> caj_Caja_Movimiento { get; set; }
        public virtual DbSet<caj_Caja_Movimiento_det> caj_Caja_Movimiento_det { get; set; }
        public virtual DbSet<caj_Caja> caj_Caja { get; set; }
        public virtual DbSet<caj_catalogo> caj_catalogo { get; set; }
        public virtual DbSet<caj_catalogo_tipo> caj_catalogo_tipo { get; set; }
        public virtual DbSet<caj_parametro> caj_parametro { get; set; }
        public virtual DbSet<caj_Caja_x_seg_usuario> caj_Caja_x_seg_usuario { get; set; }
        public virtual DbSet<caj_Caja_Movimiento_Tipo> caj_Caja_Movimiento_Tipo { get; set; }
        public virtual DbSet<cp_conciliacion_Caja_det_x_ValeCaja> cp_conciliacion_Caja_det_x_ValeCaja { get; set; }
        public virtual DbSet<cp_conciliacion_Caja_det_Ing_Caja> cp_conciliacion_Caja_det_Ing_Caja { get; set; }
        public virtual DbSet<vwcaj_Caja_Movimiento> vwcaj_Caja_Movimiento { get; set; }
    }
}
