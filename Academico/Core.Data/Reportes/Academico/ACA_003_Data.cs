using Core.Data.Base;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Academico
{
    public class ACA_003_Data
    {
        public List<ACA_003_Info> get_list(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                List<ACA_003_Info> Lista;
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Lista = Context.VWACA_003.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno).Select(q => new ACA_003_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdMatricula = q.IdMatricula,
                        CedulaAlumno = q.CedulaAlumno,
                        CedulaRepresentante = q.CedulaRepresentante,
                        CedulaSeFactura = q.CedulaSeFactura,
                        DescripcionActual = q.DescripcionActual,
                        DescripcionAnterior = q.DescripcionAnterior,
                        DescripcionPensiones = q.DescripcionPensiones,
                        Direccion= q.Direccion,
                        IdAlumno = q.IdAlumno,
                        NomAlumno = q.NomAlumno,
                        NomCurso = q.NomCurso,
                        NomRepresentante = q.NomRepresentante,
                        NomSeFactura =q.NomSeFactura

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
