using Core.Data.Base;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Academico
{
  public class ACA_009_Data
    {
        public List< ACA_009_Info> GetList(int IdEmpresa, int IdAnio, decimal IdAlumno, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                decimal IdAlumnoini = IdAlumno;
                decimal IdALumnofin = IdAlumno == 0 ? 99999999 : IdAlumno;
                fecha_ini = fecha_ini.Date;
                fecha_fin = fecha_fin.Date;

                List<ACA_009_Info> Lista = new List<ACA_009_Info>();
                using (EntitiesReportes db = new EntitiesReportes())
                {
                    Lista = db.VWACA_009.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio &&
                    q.IdAlumno == IdAlumno && fecha_ini <= q.FechaCreacion && q.FechaCreacion <= fecha_fin).Select(q => new ACA_009_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdMatricula = q.IdMatricula,
                        Secuencia = q.Secuencia,
                        IdAlumno = q.IdAlumno,
                        Codigo = q.Codigo,
                        pe_nombreCompleto = q.pe_nombreCompleto,
                        pe_cedulaRuc = q.pe_cedulaRuc,
                        FechaCreacion = q.FechaCreacion,
                        Observacion = q.Observacion,
                        IdAnio = q.IdAnio,
                        Descripcion = q.Descripcion,
                        IdUsuarioCreacion = q.IdUsuarioCreacion
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
