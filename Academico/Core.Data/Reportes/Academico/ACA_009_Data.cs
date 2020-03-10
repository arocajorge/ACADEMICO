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
                    var lst = db.VWACA_009.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio &&
                    q.IdAlumno == IdAlumno && fecha_ini <= q.FechaCreacion && q.FechaCreacion <= fecha_fin).ToList();
                    foreach (var item in lst)
                    {
                        Lista.Add(new ACA_009_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdMatricula = item.IdMatricula,
                            Secuencia = item.Secuencia,
                            IdAlumno = item.IdAlumno,
                            Codigo = item.Codigo,
                            pe_nombreCompleto = item.pe_nombreCompleto,
                            pe_cedulaRuc = item.pe_cedulaRuc,
                            FechaCreacion = item.FechaCreacion,
                            Observacion = item.Observacion,
                            IdAnio = item.IdAnio,
                            Descripcion = item.Descripcion,
                            IdUsuarioCreacion = item.IdUsuarioCreacion
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
