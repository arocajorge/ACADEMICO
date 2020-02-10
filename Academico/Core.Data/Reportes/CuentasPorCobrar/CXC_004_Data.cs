using Core.Data.Base;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Data.Reportes.CuentasPorCobrar
{
    public class CXC_004_Data
    {
        public List<CXC_004_Info> Getlist(int IdEmpresa, string IdUsuario, DateTime FechaCorte)
        {
            try
            {
                FechaCorte = FechaCorte.Date;

                List <CXC_004_Info> Lista = new List<CXC_004_Info>();

                using (EntitiesReportes db = new EntitiesReportes())
                {
                    var lst = db.SPCXC_004(IdEmpresa, IdUsuario, FechaCorte).ToList();

                    foreach (var item in lst)
                    {
                        Lista.Add(new CXC_004_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdAlumno = item.IdAlumno,
                            IdAnio = item.IdAnio,
                            IdUsuario = item.IdUsuario,
                            NomAnio = item.NomAnio,
                            CodigoAlumno = item.CodigoAlumno,
                            NombreAlumno = item.NombreAlumno,
                            IdJornada = item.IdJornada,
                            NombreJornada = item.NombreJornada,
                            SaldoDeudor = item.SaldoDeudor,
                            SaldoAcreedor = item.SaldoAcreedor,
                            SaldoFinal = item.SaldoFinal,

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
