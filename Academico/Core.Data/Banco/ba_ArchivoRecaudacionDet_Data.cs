using Core.Data.Base;
using Core.Info.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Banco
{
    public class ba_ArchivoRecaudacionDet_Data
    {
        public List<ba_ArchivoRecaudacionDet_Info> GetList(int IdEmpresa, decimal IdArchivo)
        {
            try
            {
                List<ba_ArchivoRecaudacionDet_Info> Lista;
                using (EntitiesBanco Context = new EntitiesBanco())
                {
                    Lista = Context.vwba_ArchivoRecaudacionDet.Where(q => q.IdEmpresa == IdEmpresa && q.IdArchivo == IdArchivo).Select(q => new ba_ArchivoRecaudacionDet_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdArchivo = q.IdArchivo,
                        Secuencia = q.Secuencia,
                        IdMatricula = q.IdMatricula,
                        IdAlumno =q.IdAlumno,
                        CodigoAlumno = q.Codigo,
                        pe_nombreCompleto = q.pe_nombreCompleto,
                        Valor =q.Valor,
                        ValorProntoPago =q.ValorProntoPago,
                        FechaProceso = q.FechaProceso
                    }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ba_ArchivoRecaudacionDet_Info> getList_ConSaldo(int IdEmpresa)
        {
            try
            {
                List<ba_ArchivoRecaudacionDet_Info> Lista;
                using (EntitiesBanco Context = new EntitiesBanco())
                {
                    Lista = Context.vwba_ArchivoRecaudacionDet_Saldos.Where(q => q.IdEmpresa == IdEmpresa).Select(q => new ba_ArchivoRecaudacionDet_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdMatricula = q.IdMatricula,
                        CodigoAlumno = q.CodigoAlumno,
                        IdAlumno = q.IdAlumno??0,
                        pe_nombreCompleto = q.pe_nombreCompleto,
                        Saldo = q.Saldo??0,
                        SaldoProntoPago = q.SaldoProntoPago??0,
                    }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ba_ArchivoRecaudacionDet_Info> getList_Archivo(int IdEmpresa, decimal IdArchivo)
        {
            try
            {
                List<ba_ArchivoRecaudacionDet_Info> Lista;
                using (EntitiesBanco Context = new EntitiesBanco())
                {
                    Lista = Context.vwba_ArchivoRecaudacion_Archivo.Where(q => q.IdEmpresa == IdEmpresa && q.IdArchivo == IdArchivo).Select(q => new ba_ArchivoRecaudacionDet_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdArchivo = q.IdArchivo,
                        Secuencia = q.Secuencia,
                        IdMatricula = q.IdMatricula,
                        IdAlumno = q.IdAlumno,
                        CodigoAlumno = q.CodigoAlumno,
                        pe_nombreCompleto = q.NomAlumno,
                        pe_cedulaRuc = q.pe_cedulaRuc,
                        Nom_Archivo = q.Nom_Archivo,
                        Valor = q.Valor,
                        ValorProntoPago = q.ValorProntoPago,
                        Observacion = q.Observacion,
                        ba_Num_Cuenta = q.ba_Num_Cuenta,
                        CodigoLegal=q.CodigoLegal,
                        IdTipoDocumento=q.IdTipoDocumento,
                        Fecha = q.Fecha
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
