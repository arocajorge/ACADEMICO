﻿using Core.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data.Base;

namespace Core.Data.Reportes.Facturacion
{
    public class FAC_006_Data
    {
        public List<FAC_006_Info> get_list(int IdEmpresa, int IdSucursal, int IdCliente, DateTime fecha_ini, DateTime fecha_fin, bool MostrarAnulados)
        {
            try
            {
                int IdSucursalIni = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 999999 : IdSucursal;
                int IdClienteIni = IdCliente;
                int IdClienteFin = IdCliente == 0 ? 999999999 : IdCliente;
                fecha_ini = fecha_ini.Date;
                fecha_fin = fecha_fin.Date;
                List<FAC_006_Info> Lista;
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Lista = (from q in Context.SPFAC_006(IdEmpresa, IdSucursalIni, IdSucursalFin, IdClienteIni, IdClienteFin, fecha_ini, fecha_fin, MostrarAnulados)
                             select new FAC_006_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdCbteVta = q.IdCbteVta,
                                 Estado = q.Estado,
                                 vt_NumFactura = q.vt_NumFactura,
                                 IdAlumno = q.IdAlumno,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 NombreFormaPago = q.NombreFormaPago,
                                 IdCatalogo_FormaPago = q.IdCatalogo_FormaPago,
                                 vt_fecha = q.vt_fecha,
                                 Ve_Vendedor = q.Ve_Vendedor,
                                 IdVendedor = q.IdVendedor,
                                 Su_Descripcion = q.Su_Descripcion,
                                 Su_Telefonos = q.Su_Telefonos,
                                 Su_Direccion = q.Su_Direccion,
                                 Su_Ruc = q.Su_Ruc,
                                 SubtotalIVAConDscto = q.SubtotalIVAConDscto,
                                 SubtotalSinIVAConDscto = q.SubtotalSinIVAConDscto,
                                 ValorIVA = q.ValorIVA,
                                 Total = q.Total,
                                 FacturasAnuladas = q.FacturasAnuladas,
                                 nom_FormaPago = q.nom_FormaPago,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 Tarifa = q.Tarifa,
                                 vt_Observacion = q.vt_Observacion
                                 
                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
