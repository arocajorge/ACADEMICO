using Core.Data.Base;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.CuentasPorCobrar
{
    public class CXC_012_Data
    {
        public List<CXC_012_Info> get_list(int IdEmpresa, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                List<CXC_012_Info> Lista = new List<CXC_012_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    var lst = Context.VWCXC_012.Where(q=>q.IdEmpresa==IdEmpresa && q.Fecha>=fecha_ini && q.Fecha<=fecha_fin).ToList();
                    foreach (var q in lst)
                    {
                        Lista.Add(new CXC_012_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAlumno = q.IdAlumno,
                            IdSeguimiento = q.IdSeguimiento,
                            IdMatricula = q.IdMatricula,
                            CorreoEnviado = q.CorreoEnviado,
                            Observacion = q.Observacion,
                            Estado = q.Estado,
                            IdUsuarioCreacion = q.IdUsuarioCreacion,
                            FechaCreacion = q.FechaCreacion,
                            pe_nombreCompleto=q.pe_nombreCompleto,
                            NomSede = q.NomSede,
                            NomCurso = q.NomCurso,
                            NomJornada = q.NomJornada, 
                            NomNivel = q.NomNivel,
                            NomParalelo = q.NomParalelo,
                            OrdenJornada=q.OrdenJornada,
                            OrdenNivel=q.OrdenNivel,
                            OrdenCurso=q.OrdenCurso,
                            OrdenParalelo=q.OrdenParalelo,
                            CodigoParalelo = q.CodigoParalelo,
                            Fecha=q.Fecha
                        });
                    }
                }
                return Lista;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
