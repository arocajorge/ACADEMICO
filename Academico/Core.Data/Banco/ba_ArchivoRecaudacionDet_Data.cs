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
                    Lista = Context.ba_ArchivoRecaudacionDet.Where(q => q.IdEmpresa == IdEmpresa && q.IdArchivo == IdArchivo).Select(q => new ba_ArchivoRecaudacionDet_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdArchivo = q.IdArchivo,
                        Secuencia = q.Secuencia,
                        IdMatricula = q.IdMatricula,
                        IdAlumno =q.IdAlumno,
                        Valor =q.Valor,
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

    }
}
