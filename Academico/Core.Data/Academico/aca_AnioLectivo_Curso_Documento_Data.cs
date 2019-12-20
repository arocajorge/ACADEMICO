using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AnioLectivo_Curso_Documento_Data
    {
        public List<aca_AnioLectivo_Curso_Documento_Info> get_list_asignacion(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso)
        {
            try
            {
                List<aca_AnioLectivo_Curso_Documento_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = (from q in Context.aca_AnioLectivo_Curso_Documento
                             join c in Context.aca_Documento
                             on new { q.IdEmpresa, q.IdDocumento } equals new { c.IdEmpresa, c.IdDocumento }
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSede == IdSede
                             && q.IdAnio == IdAnio
                             && q.IdNivel == IdNivel
                             && q.IdJornada == IdJornada
                             && q.IdCurso == IdCurso
                             && c.Estado == true
                             select new aca_AnioLectivo_Curso_Documento_Info
                             {
                                 seleccionado = true,
                                 IdEmpresa = q.IdEmpresa,
                                 IdSede = q.IdSede,
                                 IdAnio = q.IdAnio,
                                 IdNivel = q.IdNivel,
                                 IdJornada = q.IdJornada,
                                 IdCurso = q.IdCurso,
                                 IdDocumento = q.IdDocumento,
                                 NomDocumento = q.NomDocumento,
                                 OrdenDocumento = q.OrdenDocumento
                             }).ToList();

                    Lista.AddRange((from j in Context.aca_Documento
                                    where !Context.aca_AnioLectivo_Curso_Documento.Any(n => n.IdDocumento == j.IdDocumento && n.IdEmpresa == IdEmpresa && n.IdSede == IdSede && n.IdAnio == IdAnio && n.IdNivel == IdNivel && n.IdJornada == IdJornada && n.IdCurso == IdCurso)
                                    && j.Estado == true
                                    select new aca_AnioLectivo_Curso_Documento_Info
                                    {
                                        seleccionado = false,
                                        IdEmpresa = IdEmpresa,
                                        IdSede = IdSede,
                                        IdAnio = IdAnio,
                                        IdNivel = IdNivel,
                                        IdJornada = IdJornada,
                                        IdCurso = IdCurso,
                                        IdDocumento = j.IdDocumento,
                                        NomDocumento = j.NomDocumento,
                                        OrdenDocumento = j.OrdenDocumento
                                    }).ToList());
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_AnioLectivo_Curso_Documento_Info> get_list_matricula(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso)
        {
            try
            {
                List<aca_AnioLectivo_Curso_Documento_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = (from q in Context.aca_AnioLectivo_Curso_Documento
                             join c in Context.aca_Documento
                             on new { q.IdEmpresa, q.IdDocumento } equals new { c.IdEmpresa, c.IdDocumento }
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSede == IdSede
                             && q.IdAnio == IdAnio
                             && q.IdNivel == IdNivel
                             && q.IdJornada == IdJornada
                             && q.IdCurso == IdCurso
                             && c.Estado == true
                             select new aca_AnioLectivo_Curso_Documento_Info
                             {
                                 seleccionado = true,
                                 IdEmpresa = q.IdEmpresa,
                                 IdSede = q.IdSede,
                                 IdAnio = q.IdAnio,
                                 IdNivel = q.IdNivel,
                                 IdJornada = q.IdJornada,
                                 IdCurso = q.IdCurso,
                                 IdDocumento = q.IdDocumento,
                                 NomDocumento = q.NomDocumento,
                                 OrdenDocumento = q.OrdenDocumento
                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool guardarDB(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, List<aca_AnioLectivo_Curso_Documento_Info> lista)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst_DocumentoPorCurso = Context.aca_AnioLectivo_Curso_Documento.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio == IdAnio && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso).ToList();
                    Context.aca_AnioLectivo_Curso_Documento.RemoveRange(lst_DocumentoPorCurso);

                    if (lista.Count > 0)
                    {
                        foreach (var info in lista)
                        {
                            aca_AnioLectivo_Curso_Documento Entity = new aca_AnioLectivo_Curso_Documento
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdAnio = info.IdAnio,
                                IdSede = info.IdSede,
                                IdNivel = info.IdNivel,
                                IdJornada = info.IdJornada,
                                IdCurso = info.IdCurso,
                                IdDocumento = info.IdDocumento,
                                NomDocumento = info.NomDocumento,
                                OrdenDocumento = info.OrdenDocumento
                            };
                            Context.aca_AnioLectivo_Curso_Documento.Add(Entity);
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

        public List<aca_AnioLectivo_Curso_Documento_Info> getList_Update(int IdEmpresa, int IdAnio, int IdDocumento)
        {
            try
            {
                List<aca_AnioLectivo_Curso_Documento_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.aca_AnioLectivo_Curso_Documento.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdDocumento == IdDocumento).Select(q => new aca_AnioLectivo_Curso_Documento_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdSede = q.IdSede,
                        IdAnio = q.IdAnio,
                        IdNivel = q.IdNivel,
                        IdJornada = q.IdJornada,
                        IdCurso = q.IdCurso,
                        IdDocumento = q.IdDocumento,
                        NomDocumento = q.NomDocumento,
                        OrdenDocumento = q.OrdenDocumento
                    }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(List<aca_AnioLectivo_Curso_Documento_Info> lista)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    if (lista.Count > 0)
                    {
                        foreach (var item in lista)
                        {
                            aca_AnioLectivo_Curso_Documento Entity = Context.aca_AnioLectivo_Curso_Documento.FirstOrDefault(q => q.IdEmpresa == item.IdEmpresa
                            && q.IdSede == item.IdSede && q.IdAnio == item.IdAnio && q.IdNivel == item.IdNivel && q.IdJornada == item.IdJornada && q.IdCurso == item.IdCurso && q.IdDocumento == item.IdDocumento);
                            if (Entity == null)
                                return false;

                            Entity.NomDocumento = item.NomDocumento;
                            Entity.OrdenDocumento = item.OrdenDocumento;
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
