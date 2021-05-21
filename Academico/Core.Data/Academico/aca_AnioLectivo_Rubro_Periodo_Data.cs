using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AnioLectivo_Rubro_Periodo_Data
    {
        public List<aca_AnioLectivo_Rubro_Periodo_Info> getList(int IdEmpresa, int IdAnio, bool MostrarAnulados)
        {
            try
            {
                List<aca_AnioLectivo_Rubro_Periodo_Info> Lista = new List<aca_AnioLectivo_Rubro_Periodo_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.aca_AnioLectivo_Rubro_Periodo.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio).Select(q => new aca_AnioLectivo_Rubro_Periodo_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdAnio = q.IdAnio,
                        IdRubro = q.IdRubro,
                        IdPeriodo = q.IdPeriodo
                    }).ToList();
                }
                Lista.ForEach(v => { v.IdString = v.IdEmpresa.ToString("000") + v.IdAnio.ToString("0000") + v.IdRubro.ToString("0000") + v.IdPeriodo.ToString("0000"); });
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_AnioLectivo_Rubro_Periodo_Info> get_list_asignacion(int IdEmpresa, int IdAnio, int IdRubro)
        {
            try
            {
                List<aca_AnioLectivo_Rubro_Periodo_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = (from q in Context.aca_AnioLectivo_Rubro_Periodo
                             join j in Context.aca_AnioLectivo_Periodo on new { q.IdEmpresa, q.IdAnio, q.IdPeriodo } equals new { j.IdEmpresa, j.IdAnio, j.IdPeriodo }
                             where q.IdEmpresa == IdEmpresa
                             && q.IdAnio == IdAnio
                             && q.IdRubro == IdRubro
                             && j.Estado == true
                             select new aca_AnioLectivo_Rubro_Periodo_Info
                             {
                                 seleccionado = true,
                                 IdEmpresa = q.IdEmpresa,
                                 IdAnio = q.IdAnio,
                                 IdRubro = q.IdRubro,
                                 IdPeriodo = q.IdPeriodo,
                                 FechaDesde = j.FechaDesde,
                                 FechaHasta = j.FechaHasta,
                             }).ToList();

                    Lista.AddRange((from j in Context.aca_AnioLectivo_Periodo
                                    where !Context.aca_AnioLectivo_Rubro_Periodo.Any(n => n.IdPeriodo == j.IdPeriodo && n.IdEmpresa == IdEmpresa && n.IdAnio == IdAnio && n.IdRubro == IdRubro)
                                    && j.Estado == true && j.IdEmpresa==IdEmpresa && j.IdAnio == IdAnio
                                    select new aca_AnioLectivo_Rubro_Periodo_Info
                                    {
                                        seleccionado = false,
                                        IdEmpresa = IdEmpresa,
                                        IdAnio = IdAnio,
                                        IdRubro = IdRubro,
                                        IdPeriodo = j.IdPeriodo,
                                        FechaDesde = j.FechaDesde,
                                        FechaHasta = j.FechaHasta
                                    }).ToList());

                    Lista.ForEach(v => { v.NomPeriodo = v.FechaDesde.Year.ToString("0000")+ v.FechaDesde.Month.ToString("00"); });
                    Lista.ForEach(v => { v.IdString = IdEmpresa.ToString("000") + IdAnio.ToString("0000") + IdRubro.ToString("0000") + v.IdPeriodo.ToString("0000"); });
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_AnioLectivo_Rubro_Periodo_Info getInfo(int IdEmpresa, int IdAnio, int IdRubro, int IdPeriodo)
        {
            try
            {
                aca_AnioLectivo_Rubro_Periodo_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_AnioLectivo_Rubro_Periodo.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdRubro == IdRubro && q.IdPeriodo == IdPeriodo).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_AnioLectivo_Rubro_Periodo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdRubro = Entity.IdRubro,
                        IdAnio = Entity.IdAnio,
                        IdPeriodo = Entity.IdPeriodo,
                        Secuencia = Entity.Secuencia
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
