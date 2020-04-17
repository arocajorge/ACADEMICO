using Core.Data.Base;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Academico
{
    public class ACA_012_Data
    {
        public List<ACA_012_Info> GetList(int IdEmpresa, int IdAnio, DateTime FechaIni, DateTime FechaFin, decimal IdRubro)
        {
            try
            {
                FechaIni = FechaIni.Date;
                FechaFin = FechaFin.Date;
                List<ACA_012_Info> Lista = new List<ACA_012_Info>();
                using (EntitiesReportes db = new EntitiesReportes())
                {
                    var lst = db.VWACA_012.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && FechaIni <= (q.cr_fecha ?? FechaIni) && (q.cr_fecha ?? FechaIni) <= FechaFin).ToList();
                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_012_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdPeriodo = q.IdPeriodo,
                            IdRubro = q.IdRubro,
                            IdSucursal = q.IdSucursal,
                            IdBodega = q.IdBodega,
                            IdCbteVta = q.IdCbteVta,
                            IdAlumno = q.IdAlumno,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            vt_NumFactura = q.vt_NumFactura,
                            vt_fecha = q.vt_fecha,
                            vt_Observacion = q.vt_Observacion,
                            cr_fecha = q.cr_fecha,
                            dc_ValorPago = q.dc_ValorPago,
                            NomSede = q.NomSede,
                            OrdenNivel = q.OrdenNivel,
                            NomNivel = q.NomNivel,
                            OrdenJornada = q.OrdenJornada,
                            NomJornada = q.NomJornada,
                            OrdenCurso = q.OrdenCurso,
                            NomCurso = q.NomCurso,
                            OrdenParalelo = q.OrdenParalelo,
                            NomParalelo = q.NomParalelo,
                            CodigoAlumno = q.CodigoAlumno,
                            pe_cedulaRuc = q.pe_cedulaRuc,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            pe_fechaNacimiento = q.pe_fechaNacimiento,
                            Nacionalidad = q.Nacionalidad,
                            NomRubro = q.NomRubro,
                            DescripcionAnio = q.DescripcionAnio,
                            EstadoPago = q.EstadoPago
                        });
                    }
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
