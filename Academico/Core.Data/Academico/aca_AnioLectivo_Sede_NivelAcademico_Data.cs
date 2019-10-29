using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AnioLectivo_Sede_NivelAcademico_Data
    {
        public List<aca_AnioLectivo_Sede_NivelAcademico_Info> getList(int IdEmpresa, int IdSede, int IdAnio)
        {
            try
            {
                List<aca_AnioLectivo_Sede_NivelAcademico_Info> Lista = new List<aca_AnioLectivo_Sede_NivelAcademico_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_AnioLectivo_Sede_NivelAcademico.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio== IdAnio).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_AnioLectivo_Sede_NivelAcademico_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            NomSede = q.NomSede,
                            NomNivel = q.NomNivel
                        });
                    });
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_AnioLectivo_Sede_NivelAcademico_Info> get_list_asignacion(int IdEmpresa, int IdSede, int IdAnio)
        {
            try
            {
                List<aca_AnioLectivo_Sede_NivelAcademico_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = (from q in Context.aca_AnioLectivo_Sede_NivelAcademico
                             join n in Context.aca_NivelAcademico
                             on new {q.IdEmpresa, q.IdNivel } equals new { n.IdEmpresa, n.IdNivel }
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSede == IdSede
                             && q.IdAnio == IdAnio
                             && n.Estado == true
                             select new aca_AnioLectivo_Sede_NivelAcademico_Info
                             {
                                 seleccionado = true,
                                 IdEmpresa = q.IdEmpresa,
                                 IdSede = q.IdSede,
                                 IdAnio = q.IdAnio,
                                 IdNivel = q.IdNivel,
                                 NomNivel = q.NomNivel
                             }).ToList();

                    Lista.AddRange((from q in Context.aca_NivelAcademico
                                    where !Context.aca_AnioLectivo_Sede_NivelAcademico.Any(n => n.IdNivel == q.IdNivel && n.IdEmpresa == IdEmpresa && n.IdSede == IdSede && n.IdAnio==IdAnio)
                                    && q.Estado == true
                                    select new aca_AnioLectivo_Sede_NivelAcademico_Info
                                    {
                                        seleccionado = false,
                                        IdEmpresa = IdEmpresa,
                                        IdSede = IdSede,
                                        IdAnio = IdAnio,
                                        IdNivel = q.IdNivel,
                                        NomNivel = q.NomNivel
                                    }).ToList());
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_AnioLectivo_Sede_NivelAcademico_Info getInfo(int IdEmpresa, int IdSede, int IdAnio)
        {
            try
            {
                aca_AnioLectivo_Sede_NivelAcademico_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_AnioLectivo_Sede_NivelAcademico.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_AnioLectivo_Sede_NivelAcademico_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAnio = Entity.IdAnio,
                        IdSede = Entity.IdSede,
                        IdNivel = Entity.IdNivel,
                        NomSede = Entity.NomSede,
                        NomNivel = Entity.NomNivel
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(int IdEmpresa, int IdSede, int IdAnio, List<aca_AnioLectivo_Sede_NivelAcademico_Info> lista)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst_SedePorNivel = Context.aca_AnioLectivo_Sede_NivelAcademico.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio == IdAnio).ToList();
                    Context.aca_AnioLectivo_Sede_NivelAcademico.RemoveRange(lst_SedePorNivel);

                    if (lista.Count > 0)
                    {
                        foreach (var info in lista)
                        {
                            aca_AnioLectivo_Sede_NivelAcademico Entity = new aca_AnioLectivo_Sede_NivelAcademico
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdAnio = info.IdAnio,
                                IdSede = info.IdSede,
                                IdNivel = info.IdNivel,
                                NomSede = info.NomSede,
                                NomNivel = info.NomNivel
                            };
                            Context.aca_AnioLectivo_Sede_NivelAcademico.Add(Entity);

                            Context.SaveChanges();
                        }
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
