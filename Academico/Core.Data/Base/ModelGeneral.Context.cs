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
    
    public partial class EntitiesGeneral : DbContext
    {
        public EntitiesGeneral()
            : base("name=EntitiesGeneral")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tb_empresa> tb_empresa { get; set; }
        public virtual DbSet<tb_sucursal> tb_sucursal { get; set; }
        public virtual DbSet<tb_ciudad> tb_ciudad { get; set; }
        public virtual DbSet<tb_parroquia> tb_parroquia { get; set; }
        public virtual DbSet<tb_Catalogo> tb_Catalogo { get; set; }
        public virtual DbSet<tb_sis_Impuesto> tb_sis_Impuesto { get; set; }
        public virtual DbSet<tb_sis_Impuesto_Tipo> tb_sis_Impuesto_Tipo { get; set; }
        public virtual DbSet<tb_profesion> tb_profesion { get; set; }
        public virtual DbSet<tb_pais> tb_pais { get; set; }
        public virtual DbSet<tb_provincia> tb_provincia { get; set; }
        public virtual DbSet<tb_region> tb_region { get; set; }
        public virtual DbSet<vwtb_pais> vwtb_pais { get; set; }
        public virtual DbSet<vwtb_parroquia> vwtb_parroquia { get; set; }
        public virtual DbSet<vwtb_provincia> vwtb_provincia { get; set; }
        public virtual DbSet<vwtb_Ciudad> vwtb_Ciudad { get; set; }
        public virtual DbSet<vwtb_ciudad_id> vwtb_ciudad_id { get; set; }
        public virtual DbSet<tb_LogError> tb_LogError { get; set; }
        public virtual DbSet<tb_sis_Documento_Tipo> tb_sis_Documento_Tipo { get; set; }
        public virtual DbSet<tb_sis_Documento_Tipo_Talonario> tb_sis_Documento_Tipo_Talonario { get; set; }
        public virtual DbSet<tb_sis_Documento_Tipo_x_Empresa> tb_sis_Documento_Tipo_x_Empresa { get; set; }
        public virtual DbSet<tb_sis_Impuesto_x_ctacble> tb_sis_Impuesto_x_ctacble { get; set; }
        public virtual DbSet<tb_sucursal_FormaPago_x_fa_NivelDescuento> tb_sucursal_FormaPago_x_fa_NivelDescuento { get; set; }
        public virtual DbSet<tbl_TransaccionesAutorizadas> tbl_TransaccionesAutorizadas { get; set; }
        public virtual DbSet<tb_ColaImpresionDirecta> tb_ColaImpresionDirecta { get; set; }
        public virtual DbSet<tb_mes> tb_mes { get; set; }
        public virtual DbSet<tb_bodega> tb_bodega { get; set; }
        public virtual DbSet<tb_CatalogoTipo> tb_CatalogoTipo { get; set; }
        public virtual DbSet<tb_dia> tb_dia { get; set; }
        public virtual DbSet<vwtb_bodega_x_tb_sucursal> vwtb_bodega_x_tb_sucursal { get; set; }
        public virtual DbSet<vwtb_bodega_x_sucursal> vwtb_bodega_x_sucursal { get; set; }
        public virtual DbSet<tb_banco> tb_banco { get; set; }
        public virtual DbSet<tb_banco_procesos_bancarios_x_empresa> tb_banco_procesos_bancarios_x_empresa { get; set; }
        public virtual DbSet<tb_parametro> tb_parametro { get; set; }
        public virtual DbSet<tb_modulo> tb_modulo { get; set; }
        public virtual DbSet<vwtb_banco_procesos_bancarios_x_empresa> vwtb_banco_procesos_bancarios_x_empresa { get; set; }
        public virtual DbSet<tb_sis_reporte_x_tb_empresa> tb_sis_reporte_x_tb_empresa { get; set; }
        public virtual DbSet<tb_Religion> tb_Religion { get; set; }
        public virtual DbSet<tb_GrupoEtnico> tb_GrupoEtnico { get; set; }
        public virtual DbSet<tb_persona> tb_persona { get; set; }
        public virtual DbSet<tb_TarjetaCredito> tb_TarjetaCredito { get; set; }
        public virtual DbSet<tb_TarjetaCredito_x_cp_proveedor> tb_TarjetaCredito_x_cp_proveedor { get; set; }
    }
}
