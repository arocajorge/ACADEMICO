using Core.Data.Base;
using Core.Info.Reportes.Caja;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Core.Data.Reportes.Caja
{
    public class CAJ_002_Data
    {
        public List<CAJ_002_Info> GetList(int IdEmpresa, DateTime FechaIni, DateTime FechaFin)
        {

            try 
	{
                FechaIni = FechaIni.Date;
                FechaFin = FechaFin.Date;

		    List <CAJ_002_Info > Lista = new List<CAJ_002_Info>();
                using (EntitiesReportes db = new EntitiesReportes())
                {
                    Lista = db.VWCAJ_002.Where(q => q.IdEmpresa == IdEmpresa && FechaIni <= q.Fecha_ini && q.Fecha_fin <= FechaFin).Select(q => new CAJ_002_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdRow = q.IdRow,
                        IdConciliacion_Caja = q.IdConciliacion_Caja,
                        Secuencia = q.Secuencia,
                        IdEmpresa_OGiro = q.IdEmpresa_OGiro,
                        IdTipoCbte_Ogiro = q.IdTipoCbte_Ogiro,
                        IdCbteCble_Ogiro = q.IdCbteCble_Ogiro,
                        co_factura = q.co_factura,
                        pe_nombreCompleto = q.pe_nombreCompleto,
                        co_FechaFactura = q.co_FechaFactura,
                        co_total = q.co_total,
                        valor_retencion = q.valor_retencion,
                        valor_a_pagar = q.valor_a_pagar,
                        Valor_a_aplicar = q.Valor_a_aplicar,
                        co_observacion = q.co_observacion,
                        Saldo_cont_al_periodo = q.Saldo_cont_al_periodo,
                        Ingresos = q.Ingresos,
                        Total_fact_vale = q.Total_fact_vale,
                        TIPO = q.TIPO,
                        Fecha_ini = q.Fecha_ini,
                        Dif_x_pagar_o_cobrar = q.Dif_x_pagar_o_cobrar,
                        Fecha_fin = q.Fecha_fin,
                        valor_a_reponer = q.valor_a_reponer,
                        NombreCaja = q.NombreCaja,
                        tm_descripcion = q.tm_descripcion,
                        IdUsuarioCreacion = q.IdUsuarioCreacion,
                        NombreUsuario = q.NombreUsuario,
                        Su_Descripcion = q.Su_Descripcion,
                        SecuenciaCaja = q.SecuenciaCaja,
                        SecuenciaVale = q.SecuenciaVale
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
