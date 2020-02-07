﻿using Core.Data.Base;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.CuentasPorCobrar
{
    public class CXC_002_Data
    {
        public List<CXC_002_Info> get_list(int IdEmpresa, int IdSucursal, decimal IdCobro)
        {
            try
            {
                List<CXC_002_Info> Lista;
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Lista = (from q in Context.VWCXC_002
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.IdCobro == IdCobro
                             select new CXC_002_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdCobro = q.IdCobro,
                                 secuencial = q.secuencial,
                                 IdBodega_Cbte = q.IdBodega_Cbte,
                                 IdCbte_vta_nota = q.IdCbte_vta_nota,
                                 dc_TipoDocumento = q.dc_TipoDocumento,
                                 dc_ValorPago = q.dc_ValorPago,
                                 tc_descripcion = q.tc_descripcion,
                                 IdPersona = q.IdPersona,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 vt_fecha = q.vt_fecha,
                                 vt_NumFactura = q.vt_NumFactura,
                                 ObservacionCobro = q.ObservacionCobro,
                                 ObservacionFact = q.ObservacionFact,
                                 cr_estado = q.cr_estado,
                                 cr_fecha = q.cr_fecha,
                                 cr_NumDocumento = q.cr_NumDocumento,
                                 cr_TotalCobro = q.cr_TotalCobro,
                                 Su_Descripcion = q.Su_Descripcion,
                                 ba_descripcion = q.ba_descripcion,
                                 Correo = q.Correo,
                                 Direccion = q.Direccion,
                                 NombreContacto = q.NombreContacto
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
