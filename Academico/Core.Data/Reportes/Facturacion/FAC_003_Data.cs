﻿using Core.Data.Base;
using Core.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Facturacion
{
   public class FAC_003_Data
    {
        public List<FAC_003_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdNota)
        {
            try
            {
                List<FAC_003_Info> Lista = new List<FAC_003_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Lista = (from q in Context.VWFAC_003
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.IdBodega == IdBodega
                             && q.IdNota == IdNota
                             select new FAC_003_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdNota = q.IdNota,
                                 Secuencia = q.Secuencia,
                                 IdProducto = q.IdProducto,
                                 pr_descripcion = q.pr_descripcion,
                                 nom_presentacion = q.nom_presentacion,
                                 lote_fecha_vcto = q.lote_fecha_vcto,
                                 lote_num_lote = q.lote_num_lote,
                                 sc_cantidad = q.sc_cantidad,
                                 sc_descUni = q.sc_descUni,
                                 sc_total = q.sc_total,
                                 sc_iva = q.sc_iva,
                                 sc_PordescUni = q.sc_PordescUni,
                                 sc_Precio = q.sc_Precio,
                                 sc_precioFinal = q.sc_precioFinal,
                                 sc_subtotal = q.sc_subtotal,
                                 sc_subtotal0 = q.sc_subtotal0,
                                 sc_subtotalIVA = q.sc_subtotalIVA,
                                 Su_Descripcion = q.Su_Descripcion,
                                 CreDeb = q.CreDeb,
                                 DescTotal = q.DescTotal,
                                 Nombres = q.Nombres,
                                 no_fecha = q.no_fecha,
                                 no_fecha_venc = q.no_fecha_venc,
                                 NumNota_Impresa = q.NumNota_Impresa,
                                 vt_por_iva = q.vt_por_iva,
                                 No_Descripcion = q.No_Descripcion,
                                 sc_observacion = q.sc_observacion,
                                 CodigoAlumno = q.CodigoAlumno,
                                 NomSede = q.NomSede,
                                 NomNivel = q.NomNivel,
                                 NomJornada = q.NomJornada,
                                 NomCurso = q.NomCurso,
                                 NomParalelo = q.NomParalelo,
                                 CodigoParalelo = q.CodigoParalelo,
                                 NomPlantilla = q.NomPlantilla,
                                 IdUsuario = q.IdUsuario,
                                 Fecha_UltMod = q.Fecha_UltMod,
                                 FechaTransaccion = q.FechaTransaccion,

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
