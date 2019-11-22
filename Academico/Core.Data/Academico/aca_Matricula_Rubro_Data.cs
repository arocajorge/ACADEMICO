using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_Matricula_Rubro_Data
    {
        public List<aca_Matricula_Rubro_Info> getListMatricula(int IdEmpresa, int IdAnio, int IdPlantilla)
        {
            try
            {
                List<aca_Matricula_Rubro_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.vwaca_Plantilla_Rubro_Matricula.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdPlantilla == IdPlantilla).Select(q => new aca_Matricula_Rubro_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdAnio = q.IdAnio,
                        IdPlantilla = q.IdPlantilla,
                        IdPeriodo = q.IdPeriodo,
                        IdRubro = q.IdRubro,
                        IdProducto = q.IdProducto,
                        Subtotal = q.Subtotal,
                        IdCod_Impuesto_Iva = q.IdCod_Impuesto_Iva,
                        ValorIVA = q.ValorIVA,
                        Porcentaje = q.Porcentaje,
                        Total = q.Total,
                        NomRubro = q.NomRubro,
                        FechaDesde = q.FechaDesde,
                        pr_descripcion = q.pr_descripcion
                    }).ToList();
                }
                Lista.ForEach(v => { v.Periodo = v.FechaDesde.Year.ToString("0000") + v.FechaDesde.Month.ToString("00"); });
                Lista.ForEach(q => q.IdString = q.IdPlantilla.ToString("000000") + q.IdPeriodo.ToString("0000") + q.IdRubro.ToString("000000"));
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_Matricula_Rubro_Info> getList(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                List<aca_Matricula_Rubro_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.vwaca_Matricula_Rubro.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula).Select(q => new aca_Matricula_Rubro_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdMatricula = q.IdMatricula,
                        IdPeriodo = q.IdPeriodo,
                        IdRubro = q.IdRubro,
                        IdProducto = q.IdProducto,
                        Subtotal = q.Subtotal,
                        IdCod_Impuesto_Iva = q.IdCod_Impuesto_Iva,
                        ValorIVA = q.ValorIVA,
                        Porcentaje = q.Porcentaje,
                        Total = q.Total,
                        NomRubro = q.NomRubro,
                        FechaDesde = q.FechaDesde,
                        FechaFacturacion = q.FechaFacturacion,
                        pr_descripcion = q.pr_descripcion
                    }).ToList();
                }
                Lista.ForEach(v => { v.Periodo = v.FechaDesde.Year.ToString("0000") + v.FechaDesde.Month.ToString("00"); });
                Lista.ForEach(q => q.IdString = q.IdMatricula.ToString("000000") + q.IdPeriodo.ToString("0000") + q.IdRubro.ToString("000000"));
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
