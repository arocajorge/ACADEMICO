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
    
    public partial class EntitiesFacturacion : DbContext
    {
        public EntitiesFacturacion()
            : base("name=EntitiesFacturacion")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<fa_TerminoPago> fa_TerminoPago { get; set; }
        public virtual DbSet<fa_cliente> fa_cliente { get; set; }
        public virtual DbSet<fa_catalogo> fa_catalogo { get; set; }
        public virtual DbSet<fa_catalogo_tipo> fa_catalogo_tipo { get; set; }
        public virtual DbSet<fa_cliente_contactos> fa_cliente_contactos { get; set; }
        public virtual DbSet<fa_cliente_tipo> fa_cliente_tipo { get; set; }
        public virtual DbSet<fa_cliente_x_fa_Vendedor_x_sucursal> fa_cliente_x_fa_Vendedor_x_sucursal { get; set; }
        public virtual DbSet<fa_formaPago> fa_formaPago { get; set; }
        public virtual DbSet<fa_NivelDescuento> fa_NivelDescuento { get; set; }
        public virtual DbSet<fa_parametro> fa_parametro { get; set; }
        public virtual DbSet<fa_PuntoVta> fa_PuntoVta { get; set; }
        public virtual DbSet<fa_factura_x_ct_cbtecble> fa_factura_x_ct_cbtecble { get; set; }
        public virtual DbSet<fa_notaCreDeb> fa_notaCreDeb { get; set; }
        public virtual DbSet<fa_notaCreDeb_det> fa_notaCreDeb_det { get; set; }
        public virtual DbSet<fa_notaCreDeb_x_ct_cbtecble> fa_notaCreDeb_x_ct_cbtecble { get; set; }
        public virtual DbSet<fa_notaCreDeb_x_cxc_cobro> fa_notaCreDeb_x_cxc_cobro { get; set; }
        public virtual DbSet<fa_notaCreDeb_x_fa_factura_NotaDeb> fa_notaCreDeb_x_fa_factura_NotaDeb { get; set; }
        public virtual DbSet<fa_TipoNota> fa_TipoNota { get; set; }
        public virtual DbSet<vwfa_PuntoVta> vwfa_PuntoVta { get; set; }
        public virtual DbSet<fa_Vendedor> fa_Vendedor { get; set; }
        public virtual DbSet<vwfa_cliente_consulta> vwfa_cliente_consulta { get; set; }
        public virtual DbSet<vwfa_cliente_contactos> vwfa_cliente_contactos { get; set; }
        public virtual DbSet<fa_TerminoPago_Distribucion> fa_TerminoPago_Distribucion { get; set; }
        public virtual DbSet<vwfa_factura_sin_automatico> vwfa_factura_sin_automatico { get; set; }
        public virtual DbSet<vwfa_factura> vwfa_factura { get; set; }
        public virtual DbSet<fa_TipoNota_x_Empresa_x_Sucursal> fa_TipoNota_x_Empresa_x_Sucursal { get; set; }
        public virtual DbSet<vwfa_notaCreDeb_x_fa_factura_NotaDeb> vwfa_notaCreDeb_x_fa_factura_NotaDeb { get; set; }
        public virtual DbSet<vwfa_notaCreDeb_det> vwfa_notaCreDeb_det { get; set; }
        public virtual DbSet<vwfa_notaCreDeb> vwfa_notaCreDeb { get; set; }
        public virtual DbSet<fa_factura_det> fa_factura_det { get; set; }
        public virtual DbSet<fa_factura_resumen> fa_factura_resumen { get; set; }
        public virtual DbSet<vwfa_factura_det> vwfa_factura_det { get; set; }
        public virtual DbSet<fa_factura> fa_factura { get; set; }
        public virtual DbSet<fa_notaCreDeb_resumen> fa_notaCreDeb_resumen { get; set; }
    }
}
