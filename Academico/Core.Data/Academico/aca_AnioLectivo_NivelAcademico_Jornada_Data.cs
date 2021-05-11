using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AnioLectivo_NivelAcademico_Jornada_Data
    {
        public List<aca_AnioLectivo_NivelAcademico_Jornada_Info> get_list_asignacion(int IdEmpresa, int IdSede, int IdAnio, int IdNivel)
        {
            try
            {
                List<aca_AnioLectivo_NivelAcademico_Jornada_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = (from q in Context.aca_AnioLectivo_NivelAcademico_Jornada
                             join j in Context.aca_Jornada
                             on new { q.IdEmpresa, q.IdJornada } equals new { j.IdEmpresa, j.IdJornada }
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSede == IdSede
                             && q.IdAnio == IdAnio
                             && q.IdNivel == IdNivel
                             && j.Estado == true
                             select new aca_AnioLectivo_NivelAcademico_Jornada_Info
                             {
                                 seleccionado = true,
                                 IdEmpresa = q.IdEmpresa,
                                 IdSede = q.IdSede,
                                 IdAnio = q.IdAnio,
                                 IdNivel = q.IdNivel,
                                 IdJornada = q.IdJornada,
                                 NomJornada = q.NomJornada,
                                 OrdenJornada = q.OrdenJornada
                             }).ToList();

                    Lista.AddRange((from j in Context.aca_Jornada
                                    where !Context.aca_AnioLectivo_NivelAcademico_Jornada.Any(n => n.IdJornada == j.IdJornada && n.IdEmpresa == IdEmpresa && n.IdSede == IdSede && n.IdAnio == IdAnio && n.IdNivel == IdNivel)
                                    && j.Estado == true
                                    select new aca_AnioLectivo_NivelAcademico_Jornada_Info
                                    {
                                        seleccionado = false,
                                        IdEmpresa = IdEmpresa,
                                        IdSede = IdSede,
                                        IdAnio = IdAnio,
                                        IdNivel = IdNivel,
                                        IdJornada = j.IdJornada,
                                        NomJornada = j.NomJornada,
                                        OrdenJornada = j.OrdenJornada
                                    }).ToList());
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_AnioLectivo_NivelAcademico_Jornada_Info> GetList_Update(int IdEmpresa, int IdAnio, int IdJornada)
        {
            try
            {
                List<aca_AnioLectivo_NivelAcademico_Jornada_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.aca_AnioLectivo_NivelAcademico_Jornada.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdJornada == IdJornada).Select(q => new aca_AnioLectivo_NivelAcademico_Jornada_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdAnio = q.IdAnio,
                        IdSede = q.IdSede,
                        IdNivel = q.IdNivel,
                        IdJornada = q.IdJornada,
                        NomJornada = q.NomJornada,
                        OrdenJornada = q.OrdenJornada
                    }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, List<aca_AnioLectivo_NivelAcademico_Jornada_Info> lista)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var info_anio_curso = Context.aca_AnioLectivo.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.EnCurso == true).FirstOrDefault();
                    if (info_anio_curso != null)
                    {
                        var lst_NivelPorJornada = Context.aca_AnioLectivo_NivelAcademico_Jornada.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio == IdAnio && q.IdNivel == IdNivel).ToList();
                        Context.aca_AnioLectivo_NivelAcademico_Jornada.RemoveRange(lst_NivelPorJornada);

                        if (lista.Count > 0)
                        {
                            foreach (var info in lista)
                            {
                                aca_AnioLectivo_NivelAcademico_Jornada Entity = new aca_AnioLectivo_NivelAcademico_Jornada
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdAnio = info.IdAnio,
                                    IdSede = info.IdSede,
                                    IdNivel = info.IdNivel,
                                    IdJornada = info.IdJornada,
                                    NomJornada = info.NomJornada,
                                    OrdenJornada = info.OrdenJornada
                                };
                                Context.aca_AnioLectivo_NivelAcademico_Jornada.Add(Entity);
                            }
                        }
                    }
                    
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(List<aca_AnioLectivo_NivelAcademico_Jornada_Info> lista)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    if (lista.Count > 0)
                    {
                        foreach (var item in lista)
                        {
                            aca_AnioLectivo_NivelAcademico_Jornada Entity = Context.aca_AnioLectivo_NivelAcademico_Jornada.FirstOrDefault(q => q.IdEmpresa == item.IdEmpresa
                            && q.IdSede == item.IdSede && q.IdAnio == item.IdAnio && q.IdNivel == item.IdNivel && q.IdJornada == item.IdJornada);
                            if (Entity == null)
                                return false;

                            Entity.NomJornada = item.NomJornada;
                        }
                        Context.SaveChanges();
                    }
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
