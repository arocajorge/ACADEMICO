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
    
    public partial class EntitiesBanco : DbContext
    {
        public EntitiesBanco()
            : base("name=EntitiesBanco")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<cp_conciliacion_Caja_det_Ing_Caja> cp_conciliacion_Caja_det_Ing_Caja { get; set; }
        public virtual DbSet<ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito> ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito { get; set; }
        public virtual DbSet<ba_Banco_Cuenta> ba_Banco_Cuenta { get; set; }
        public virtual DbSet<ba_Banco_Cuenta_x_tb_sucursal> ba_Banco_Cuenta_x_tb_sucursal { get; set; }
        public virtual DbSet<ba_Catalogo> ba_Catalogo { get; set; }
        public virtual DbSet<ba_CatalogoTipo> ba_CatalogoTipo { get; set; }
        public virtual DbSet<ba_parametros> ba_parametros { get; set; }
        public virtual DbSet<ba_Cbte_Ban> ba_Cbte_Ban { get; set; }
        public virtual DbSet<ba_Cbte_Ban_tipo> ba_Cbte_Ban_tipo { get; set; }
        public virtual DbSet<ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo> ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo { get; set; }
        public virtual DbSet<ba_Cbte_Ban_x_ba_TipoFlujo> ba_Cbte_Ban_x_ba_TipoFlujo { get; set; }
        public virtual DbSet<ba_Conciliacion> ba_Conciliacion { get; set; }
        public virtual DbSet<ba_Conciliacion_det> ba_Conciliacion_det { get; set; }
        public virtual DbSet<ba_Conciliacion_det_IngEgr> ba_Conciliacion_det_IngEgr { get; set; }
        public virtual DbSet<ba_Talonario_cheques_x_banco> ba_Talonario_cheques_x_banco { get; set; }
        public virtual DbSet<ba_tipo_nota> ba_tipo_nota { get; set; }
        public virtual DbSet<ba_TipoFlujo> ba_TipoFlujo { get; set; }
        public virtual DbSet<ba_TipoFlujo_Movimiento> ba_TipoFlujo_Movimiento { get; set; }
        public virtual DbSet<ba_TipoFlujo_Plantilla> ba_TipoFlujo_Plantilla { get; set; }
        public virtual DbSet<ba_TipoFlujo_PlantillaDet> ba_TipoFlujo_PlantillaDet { get; set; }
        public virtual DbSet<vwba_Cbte_Ban_x_ba_TipoFlujo> vwba_Cbte_Ban_x_ba_TipoFlujo { get; set; }
        public virtual DbSet<vwba_TipoFlujo_Movimiento> vwba_TipoFlujo_Movimiento { get; set; }
        public virtual DbSet<vwba_TipoFlujoPlantillaDet> vwba_TipoFlujoPlantillaDet { get; set; }
        public virtual DbSet<vwba_Talonario_cheques_x_banco_ID> vwba_Talonario_cheques_x_banco_ID { get; set; }
        public virtual DbSet<vwba_Cbte_Ban> vwba_Cbte_Ban { get; set; }
        public virtual DbSet<vwba_Caja_Movimiento_x_Cbte_Ban_x_Deposito> vwba_Caja_Movimiento_x_Cbte_Ban_x_Deposito { get; set; }
        public virtual DbSet<vwba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_x_depositar> vwba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_x_depositar { get; set; }
        public virtual DbSet<ba_Archivo_Transferencia> ba_Archivo_Transferencia { get; set; }
        public virtual DbSet<ba_Archivo_Transferencia_Det> ba_Archivo_Transferencia_Det { get; set; }
        public virtual DbSet<ba_archivo_transferencia_x_ba_tipo_flujo> ba_archivo_transferencia_x_ba_tipo_flujo { get; set; }
        public virtual DbSet<vwba_Archivo_Transferencia_Det> vwba_Archivo_Transferencia_Det { get; set; }
    }
}
