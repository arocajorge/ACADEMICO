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
                List<aca_Matricula_Rubro_Info> Lista = new List<aca_Matricula_Rubro_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst = Context.vwaca_Plantilla_Rubro_Matricula.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdPlantilla == IdPlantilla).OrderBy(q => q.IdPeriodo).ToList();
                    foreach (var q in lst)
                    {
                        Lista.Add(new aca_Matricula_Rubro_Info
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
                            pr_descripcion = q.pr_descripcion,
                            AplicaProntoPago = q.AplicaProntoPago,
                            ValorProntoPago = Convert.ToDecimal(q.ValorProntoPago ?? 0),
                            FechaProntoPago = q.FechaProntoPago ?? DateTime.Now.Date
                        });
                    }
                    
                }
                Lista.ForEach(v => { v.Periodo = v.FechaDesde.Year.ToString("0000") + v.FechaDesde.Month.ToString("00"); });
                Lista.ForEach(q => q.IdString = q.IdEmpresa.ToString("0000000") + q.IdPlantilla.ToString("0000000") + q.IdPeriodo.ToString("0000000") + q.IdRubro.ToString("0000000"));
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
                    Lista = Context.vwaca_Matricula_Rubro.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula).OrderBy(q=>q.IdPeriodo).Select(q => new aca_Matricula_Rubro_Info
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
                        pr_descripcion = q.pr_descripcion,
                        IdMecanismo = q.IdMecanismo,
                        EnMatricula = q.EnMatricula,
                        IdAnio = q.IdAnio,
                        IdPlantilla = q.IdPlantilla,
                        IdCbteVta = q.IdCbteVta,
                        IdBodega = q.IdBodega,
                        IdSucursal = q.IdSucursal,
                        IdSede = q.IdSede,
                        IdJornada = q.IdJornada,
                        IdNivel = q.IdNivel,
                        IdCurso=q.IdCurso,
                        IdParalelo = q.IdParalelo
                    }).ToList();
                }
                Lista.ForEach(v => { v.Periodo = v.FechaDesde.Year.ToString("0000") + v.FechaDesde.Month.ToString("00"); });
                Lista.ForEach(q => q.IdString = q.IdEmpresa.ToString("0000")+ q.IdMatricula.ToString("000000") + q.IdPeriodo.ToString("0000") + q.IdRubro.ToString("000000"));
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_Matricula_Rubro_Info getInfo(int IdEmpresa, decimal IdMatricula, int IdPeriodo, int IdRubro)
        {
            try
            {
                aca_Matricula_Rubro_Info info = new aca_Matricula_Rubro_Info();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var Entity = Context.vwaca_Matricula_Rubro.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula && q.IdPeriodo == IdPeriodo && q.IdRubro == IdRubro).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_Matricula_Rubro_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMatricula = Entity.IdMatricula,
                        IdPeriodo = Entity.IdPeriodo,
                        IdRubro = Entity.IdRubro,
                        IdProducto = Entity.IdProducto,
                        Subtotal = Entity.Subtotal,
                        IdCod_Impuesto_Iva = Entity.IdCod_Impuesto_Iva,
                        ValorIVA = Entity.ValorIVA,
                        Porcentaje = Entity.Porcentaje,
                        Total = Entity.Total,
                        NomRubro = Entity.NomRubro,
                        FechaDesde = Entity.FechaDesde,
                        FechaFacturacion = Entity.FechaFacturacion,
                        pr_descripcion = Entity.pr_descripcion,
                        IdMecanismo = Entity.IdMecanismo,
                        EnMatricula = Entity.EnMatricula,
                        IdAnio = Entity.IdAnio,
                        IdPlantilla = Entity.IdPlantilla,
                        IdSede = Entity.IdSede,
                        IdJornada = Entity.IdJornada,
                        IdNivel = Entity.IdNivel,
                        IdCurso = Entity.IdCurso,
                        IdParalelo = Entity.IdParalelo
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_Matricula_Rubro_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Matricula_Rubro Entity = Context.aca_Matricula_Rubro.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula && q.IdPeriodo==info.IdPeriodo && q.IdRubro==info.IdRubro);
                    if (Entity == null)
                        return false;

                    Entity.IdSucursal = info.IdSucursal;
                    Entity.IdBodega = info.IdBodega;
                    Entity.IdCbteVta = info.IdCbteVta;
                    Entity.FechaFacturacion = info.FechaFacturacion;

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_Matricula_Rubro_Info> getList_FactMasiva(int IdEmpresa, int IdAnio, int IdPeriodo)
        {
            try
            {
                List<aca_Matricula_Rubro_Info> Lista = new List<aca_Matricula_Rubro_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst = Context.vwaca_Matricula_Rubro_PorFacturarMasiva.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdPeriodo == IdPeriodo).ToList();
                    foreach (var q in lst)
                    {
                        var info = new aca_Matricula_Rubro_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdAnio = q.IdAnio,
                            IdPeriodo = q.IdPeriodo,
                            IdPlantilla = q.IdPlantilla,
                            IdRubro = q.IdRubro,
                            IdProducto = q.IdProducto,
                            Subtotal = q.Subtotal,
                            IdCod_Impuesto_Iva = q.IdCod_Impuesto_Iva,
                            ValorIVA = q.ValorIVA,
                            Porcentaje = q.Porcentaje,
                            Total = q.Total,
                            ValorProntoPago = Convert.ToDecimal(q.ValorProntoPago),
                            vt_Observacion = q.Observacion,
                            IdAlumno = q.IdAlumno,
                            FechaDesde = Convert.ToDateTime(q.FechaDesde),
                            FechaProntoPago = Convert.ToDateTime(q.FechaProntoPago),
                            IdTerminoPago = q.IdTerminoPago,
                            IdCliente = q.IdCliente ?? 0,
                            Codigo = q.Codigo,
                            pe_nombreCompleto = q.Alumno,
                            Procesado = false,
                            IdEmpresa_rol = q.IdEmpresa_rol,
                            IdEmpleado = q.IdEmpleado,
                        };
                        Lista.Add(info);
                    }

                    var lstProcesado = Context.vwaca_Matricula_Rubro_FacturaMasiva.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdPeriodo == IdPeriodo).ToList();
                    foreach (var item in lstProcesado)
                    {
                        Lista.Add(new aca_Matricula_Rubro_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdAnio = item.IdAnio,
                            IdPeriodo = item.IdPeriodo,
                            IdAlumno = item.IdAlumno,
                            pe_nombreCompleto = item.pe_nombreCompleto,
                            Total = item.Total,
                            IdSucursal = item.IdSucursal,
                            IdBodega = item.IdBodega,
                            IdCbteVta = item.IdCbteVta,
                            Correo = item.Correo,
                            vt_autorizacion = item.vt_autorizacion,
                            Procesado = true
                        });
                    }
                }
                
                Lista.ForEach(q => q.IdString = q.IdEmpresa.ToString("0000") + q.IdMatricula.ToString("000000") + q.IdPeriodo.ToString("0000") + q.IdRubro.ToString("000000"));
                return Lista;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
