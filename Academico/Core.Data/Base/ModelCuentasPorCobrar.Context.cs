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
    
    public partial class EntitiesCuentasPorCobrar : DbContext
    {
        public EntitiesCuentasPorCobrar()
            : base("name=EntitiesCuentasPorCobrar")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<cxc_Catalogo> cxc_Catalogo { get; set; }
        public virtual DbSet<cxc_CatalogoTipo> cxc_CatalogoTipo { get; set; }
        public virtual DbSet<cxc_cobro_det_x_ct_cbtecble_det> cxc_cobro_det_x_ct_cbtecble_det { get; set; }
        public virtual DbSet<cxc_cobro_tipo> cxc_cobro_tipo { get; set; }
        public virtual DbSet<cxc_cobro_tipo_motivo> cxc_cobro_tipo_motivo { get; set; }
        public virtual DbSet<cxc_cobro_tipo_Param_conta_x_sucursal> cxc_cobro_tipo_Param_conta_x_sucursal { get; set; }
        public virtual DbSet<cxc_cobro_x_caj_Caja_Movimiento> cxc_cobro_x_caj_Caja_Movimiento { get; set; }
        public virtual DbSet<cxc_cobro_x_ct_cbtecble> cxc_cobro_x_ct_cbtecble { get; set; }
        public virtual DbSet<vwcxc_cobro_det_retencion> vwcxc_cobro_det_retencion { get; set; }
        public virtual DbSet<vwcxc_cobro_det_valor_retenciones> vwcxc_cobro_det_valor_retenciones { get; set; }
        public virtual DbSet<vwcxc_cobro_para_retencion> vwcxc_cobro_para_retencion { get; set; }
        public virtual DbSet<cxc_cobro_det> cxc_cobro_det { get; set; }
        public virtual DbSet<vwcxc_cartera_x_cobrar> vwcxc_cartera_x_cobrar { get; set; }
        public virtual DbSet<vwcxc_cobro_det> vwcxc_cobro_det { get; set; }
        public virtual DbSet<cxc_cobro> cxc_cobro { get; set; }
        public virtual DbSet<cxc_ConciliacionNotaCreditoDet> cxc_ConciliacionNotaCreditoDet { get; set; }
        public virtual DbSet<vwcxc_ConciliacionNotaCredito> vwcxc_ConciliacionNotaCredito { get; set; }
        public virtual DbSet<vwcxc_cobro> vwcxc_cobro { get; set; }
        public virtual DbSet<vwcxc_ConciliacionNotaCreditoDet> vwcxc_ConciliacionNotaCreditoDet { get; set; }
        public virtual DbSet<cxc_LiquidacionTarjeta> cxc_LiquidacionTarjeta { get; set; }
        public virtual DbSet<cxc_LiquidacionTarjeta_x_ba_TipoFlujo> cxc_LiquidacionTarjeta_x_ba_TipoFlujo { get; set; }
        public virtual DbSet<cxc_LiquidacionTarjeta_x_cxc_cobro> cxc_LiquidacionTarjeta_x_cxc_cobro { get; set; }
        public virtual DbSet<cxc_LiquidacionTarjetaDet> cxc_LiquidacionTarjetaDet { get; set; }
        public virtual DbSet<cxc_MotivoLiquidacionTarjeta> cxc_MotivoLiquidacionTarjeta { get; set; }
        public virtual DbSet<cxc_MotivoLiquidacionTarjeta_x_tb_sucursal> cxc_MotivoLiquidacionTarjeta_x_tb_sucursal { get; set; }
        public virtual DbSet<vwcxc_LiquidacionTarjeta> vwcxc_LiquidacionTarjeta { get; set; }
        public virtual DbSet<vwcxc_MotivoLiquidacionTarjeta_x_tb_sucursal> vwcxc_MotivoLiquidacionTarjeta_x_tb_sucursal { get; set; }
        public virtual DbSet<vwcxc_LiquidacionTarjeta_x_ba_TipoFlujo> vwcxc_LiquidacionTarjeta_x_ba_TipoFlujo { get; set; }
        public virtual DbSet<cxc_ConciliacionNotaCredito> cxc_ConciliacionNotaCredito { get; set; }
        public virtual DbSet<vwcxc_cartera_cobrada_saldo0> vwcxc_cartera_cobrada_saldo0 { get; set; }
        public virtual DbSet<cxc_CobroMasivoDet> cxc_CobroMasivoDet { get; set; }
        public virtual DbSet<vwcxc_CobroMasivoDet> vwcxc_CobroMasivoDet { get; set; }
        public virtual DbSet<cxc_CobroMasivo> cxc_CobroMasivo { get; set; }
        public virtual DbSet<cxc_SeguimientoCartera> cxc_SeguimientoCartera { get; set; }
        public virtual DbSet<vwcxc_SeguimientoCartera> vwcxc_SeguimientoCartera { get; set; }
        public virtual DbSet<vwcxc_LiquidacionTarjeta_x_cxc_cobro> vwcxc_LiquidacionTarjeta_x_cxc_cobro { get; set; }
        public virtual DbSet<cxc_Parametro> cxc_Parametro { get; set; }
    }
}
