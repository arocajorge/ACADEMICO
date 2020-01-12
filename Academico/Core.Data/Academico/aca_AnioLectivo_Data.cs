using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AnioLectivo_Data
    {
        aca_AnioLectivo_Periodo_Data odata_periodo = new aca_AnioLectivo_Periodo_Data();
        aca_AnioLectivo_Sede_NivelAcademico_Data odata_sede_nivel = new aca_AnioLectivo_Sede_NivelAcademico_Data();
        aca_AnioLectivo_NivelAcademico_Jornada_Data odata_nivel_jornada = new aca_AnioLectivo_NivelAcademico_Jornada_Data();
        aca_AnioLectivo_Jornada_Curso_Data odata_jornada_curso = new aca_AnioLectivo_Jornada_Curso_Data();
        aca_AnioLectivo_Curso_Paralelo_Data odata_curso_paralelo = new aca_AnioLectivo_Curso_Paralelo_Data();
        aca_AnioLectivo_Curso_Documento_Data odata_curso_documento = new aca_AnioLectivo_Curso_Documento_Data();
        aca_AnioLectivo_Curso_Materia_Data odata_curso_materia = new aca_AnioLectivo_Curso_Materia_Data();
        aca_Sede_Data odata_sede = new aca_Sede_Data();
        aca_NivelAcademico_Data odata_nivel = new aca_NivelAcademico_Data();
        aca_Jornada_Data odata_jornada = new aca_Jornada_Data();
        aca_Curso_Data odata_curso = new aca_Curso_Data();
        aca_Paralelo_Data odata_paralelo = new aca_Paralelo_Data();
        aca_Materia_Data odata_materia = new aca_Materia_Data();
        aca_Documento_Data odata_documento = new aca_Documento_Data();
        

        public List<aca_AnioLectivo_Info> getList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<aca_AnioLectivo_Info> Lista = new List<aca_AnioLectivo_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_AnioLectivo.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == (MostrarAnulados ? q.Estado : true)).OrderBy(q=>q.Descripcion).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_AnioLectivo_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAnio = q.IdAnio,
                            Descripcion = q.Descripcion,
                            FechaDesde = q.FechaDesde,
                            FechaHasta = q.FechaHasta,
                            EnCurso = q.EnCurso,
                            BloquearMatricula = q.BloquearMatricula,
                            Estado = q.Estado
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

        public List<aca_AnioLectivo_Info> getList_update(int IdEmpresa)
        {
            try
            {
                List<aca_AnioLectivo_Info> Lista = new List<aca_AnioLectivo_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.aca_AnioLectivo.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true && q.BloquearMatricula == false).Select(q => new aca_AnioLectivo_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdAnio = q.IdAnio,
                        Descripcion = q.Descripcion,
                        FechaDesde = q.FechaDesde,
                        FechaHasta = q.FechaHasta,
                        EnCurso = q.EnCurso,
                        BloquearMatricula = q.BloquearMatricula,
                        Estado = q.Estado
                    }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_AnioLectivo_Info getInfo(int IdEmpresa, int IdAnio)
        {
            try
            {
                aca_AnioLectivo_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_AnioLectivo.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_AnioLectivo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAnio = Entity.IdAnio,
                        Descripcion = Entity.Descripcion,
                        FechaDesde = Entity.FechaDesde,
                        FechaHasta = Entity.FechaHasta,
                        EnCurso = Entity.EnCurso,
                        BloquearMatricula = Entity.BloquearMatricula,
                        Estado = Entity.Estado
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_AnioLectivo_Info getInfo_AnioAnterior(int IdEmpresa, int Anio)
        {
            try
            {
                aca_AnioLectivo_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_AnioLectivo.Where(q => q.IdEmpresa == IdEmpresa && q.FechaDesde.Year == Anio).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_AnioLectivo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAnio = Entity.IdAnio,
                        Descripcion = Entity.Descripcion,
                        FechaDesde = Entity.FechaDesde,
                        FechaHasta = Entity.FechaHasta,
                        EnCurso = Entity.EnCurso,
                        BloquearMatricula = Entity.BloquearMatricula,
                        Estado = Entity.Estado
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_AnioLectivo_Info getInfo_AnioEnCurso(int IdEmpresa, int IdAnio)
        {
            try
            {
                aca_AnioLectivo_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_AnioLectivo.Where(q => q.IdEmpresa == IdEmpresa && q.Estado==true && q.EnCurso==true && (IdAnio==0 ? q.IdAnio == q.IdAnio : q.IdAnio!= IdAnio)).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_AnioLectivo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAnio = Entity.IdAnio,
                        Descripcion = Entity.Descripcion,
                        FechaDesde = Entity.FechaDesde,
                        FechaHasta = Entity.FechaHasta,
                        EnCurso = Entity.EnCurso,
                        Estado = Entity.Estado
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int getId(int IdEmpresa)
        {
            try
            {
                int ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_AnioLectivo.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_AnioLectivo.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdAnio) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_AnioLectivo_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var IdPeriodo = 0;
                    aca_AnioLectivo Entity = new aca_AnioLectivo
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdAnio = info.IdAnio = getId(info.IdEmpresa),
                        Descripcion = info.Descripcion,
                        FechaDesde = info.FechaDesde,
                        FechaHasta = info.FechaHasta,
                        EnCurso = info.EnCurso,
                        BloquearMatricula = info.BloquearMatricula,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_AnioLectivo.Add(Entity);

                    if (info.lst_periodos.Count >0)
                    {
                        IdPeriodo = odata_periodo.getId(info.IdEmpresa);
                        foreach (var item in info.lst_periodos)
                        {
                            aca_AnioLectivo_Periodo Entity_Periodo = new aca_AnioLectivo_Periodo
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdPeriodo = IdPeriodo++,
                                IdAnio = info.IdAnio,
                                IdMes = item.IdMes,
                                FechaDesde = item.FechaDesde,
                                FechaHasta = item.FechaHasta,
                                FechaProntoPago = null,
                                Estado = true,
                                IdUsuarioCreacion = info.IdUsuarioCreacion,
                                FechaCreacion = info.FechaCreacion = DateTime.Now

                            };
                                Context.aca_AnioLectivo_Periodo.Add(Entity_Periodo);
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

        public bool modificarDB(aca_AnioLectivo_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AnioLectivo Entity = Context.aca_AnioLectivo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio);
                    if (Entity == null)
                        return false;

                    Entity.Descripcion = info.Descripcion;
                    Entity.FechaDesde = info.FechaDesde;
                    Entity.FechaHasta = info.FechaHasta;
                    Entity.EnCurso = info.EnCurso;
                    Entity.BloquearMatricula = info.BloquearMatricula;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = info.FechaModificacion = DateTime.Now;

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(aca_AnioLectivo_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AnioLectivo Entity = Context.aca_AnioLectivo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio);
                    if (Entity == null)
                        return false;
                    Entity.Estado = info.Estado = false;
                    Entity.MotivoAnulacion = info.MotivoAnulacion;
                    Entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                    Entity.FechaAnulacion = info.FechaAnulacion = DateTime.Now;
                    Entity.EnCurso = false;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarAperturaDB(aca_AnioLectivo_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst_asignacion_sede_nivel = odata_sede_nivel.get_list_asignacion(info.IdEmpresa, info.IdSede, info.IdAnio).Where(q=> q.seleccionado==true).ToList();

                    if (lst_asignacion_sede_nivel.Count > 0)
                    {
                        foreach (var item_sn in lst_asignacion_sede_nivel)
                        {
                            var info_sede = odata_sede.GetInfo(info.IdEmpresa, item_sn.IdSede);
                            var info_nivel = odata_nivel.getInfo(info.IdEmpresa, item_sn.IdNivel);
                            aca_AnioLectivo_Sede_NivelAcademico Entity_SedeNivel = new aca_AnioLectivo_Sede_NivelAcademico
                            {
                                IdEmpresa = item_sn.IdEmpresa,
                                IdAnio = info.IdAnioApertura,
                                IdSede = item_sn.IdSede,
                                IdNivel = item_sn.IdNivel,
                                NomNivel = info_nivel.NomNivel,
                                NomSede = info_sede.NomSede
                            };
                            Context.aca_AnioLectivo_Sede_NivelAcademico.Add(Entity_SedeNivel);
                        }

                        var lst_nivel = lst_asignacion_sede_nivel.GroupBy(q=>q.IdNivel).ToList();
                        if (lst_nivel.Count >0)
                        {
                            foreach (var item_niv in lst_nivel)
                            {
                                var lst_asignacion_nivel_jornada = odata_nivel_jornada.get_list_asignacion(info.IdEmpresa, info.IdSede, info.IdAnio, Convert.ToInt32(item_niv.Key) ).Where(q => q.seleccionado == true).ToList();

                                foreach (var item_nj in lst_asignacion_nivel_jornada)
                                {
                                    var info_jornada = odata_jornada.getInfo(info.IdEmpresa, item_nj.IdJornada);
                                    aca_AnioLectivo_NivelAcademico_Jornada Entity_NivelJornada = new aca_AnioLectivo_NivelAcademico_Jornada
                                    {
                                        IdEmpresa = item_nj.IdEmpresa,
                                        IdAnio = info.IdAnioApertura,
                                        IdSede = item_nj.IdSede,
                                        IdNivel = item_nj.IdNivel,
                                        IdJornada = item_nj.IdJornada,
                                        NomJornada = info_jornada.NomJornada,
                                        OrdenJornada = info_jornada.OrdenJornada
                                    };
                                    Context.aca_AnioLectivo_NivelAcademico_Jornada.Add(Entity_NivelJornada);
                                }

                                var lst_jornada = lst_asignacion_nivel_jornada.GroupBy(q => q.IdJornada).ToList();
                                if (lst_jornada.Count > 0)
                                {
                                    foreach (var item_jor in lst_jornada)
                                    {
                                        var lst_asignacion_jornada_curso = odata_jornada_curso.get_list_asignacion(info.IdEmpresa, info.IdSede, info.IdAnio, Convert.ToInt32(item_niv.Key), Convert.ToInt32(item_jor.Key)).Where(q => q.seleccionado == true).ToList();

                                        foreach (var item_jc in lst_asignacion_jornada_curso)
                                        {
                                            var info_curso = odata_curso.getInfo(info.IdEmpresa, item_jc.IdCurso);
                                            aca_AnioLectivo_Jornada_Curso Entity_JornadaCurso = new aca_AnioLectivo_Jornada_Curso
                                            {
                                                IdEmpresa = item_jc.IdEmpresa,
                                                IdAnio = info.IdAnioApertura,
                                                IdSede = item_jc.IdSede,
                                                IdNivel = item_jc.IdNivel,
                                                IdJornada = item_jc.IdJornada,
                                                IdCurso = item_jc.IdCurso,
                                                NomCurso = info_curso.NomCurso,
                                                OrdenCurso = info_curso.OrdenCurso
                                            };
                                            Context.aca_AnioLectivo_Jornada_Curso.Add(Entity_JornadaCurso);
                                        }

                                        var lst_curso = lst_asignacion_jornada_curso.GroupBy(q => q.IdCurso).ToList();
                                        if (lst_curso.Count > 0)
                                        {
                                            foreach (var item_cur in lst_curso)
                                            {
                                                var lst_asignacion_curso_paralelo = odata_curso_paralelo.get_list_asignacion(info.IdEmpresa, info.IdSede, info.IdAnio, Convert.ToInt32(item_niv.Key), Convert.ToInt32(item_jor.Key), Convert.ToInt32(item_cur.Key)).Where(q => q.seleccionado == true).ToList();

                                                foreach (var item_cp in lst_asignacion_curso_paralelo)
                                                {
                                                    var inf_paralelo = odata_paralelo.getInfo(info.IdEmpresa, item_cp.IdParalelo);
                                                    aca_AnioLectivo_Curso_Paralelo Entity_CursoParalelo = new aca_AnioLectivo_Curso_Paralelo
                                                    {
                                                        IdEmpresa = item_cp.IdEmpresa,
                                                        IdAnio = info.IdAnioApertura,
                                                        IdSede = item_cp.IdSede,
                                                        IdNivel = item_cp.IdNivel,
                                                        IdJornada = item_cp.IdJornada,
                                                        IdCurso = item_cp.IdCurso,
                                                        IdParalelo = item_cp.IdParalelo,
                                                        CodigoParalelo = inf_paralelo.CodigoParalelo,
                                                        NomParalelo = inf_paralelo.NomParalelo,
                                                        OrdenParalelo = inf_paralelo.OrdenParalelo
                                                    };
                                                    Context.aca_AnioLectivo_Curso_Paralelo.Add(Entity_CursoParalelo);
                                                }

                                                var lst_asignacion_curso_materia = odata_curso_materia.get_list_asignacion(info.IdEmpresa, info.IdSede, info.IdAnio, Convert.ToInt32(item_niv.Key), Convert.ToInt32(item_jor.Key), Convert.ToInt32(item_cur.Key)).Where(q => q.seleccionado == true).ToList();

                                                foreach (var item_cm in lst_asignacion_curso_materia)
                                                {
                                                    var info_materia = odata_materia.getInfo(info.IdEmpresa, item_cm.IdMateria);
                                                    aca_AnioLectivo_Curso_Materia Entity_CursoMateria = new aca_AnioLectivo_Curso_Materia
                                                    {
                                                        IdEmpresa = item_cm.IdEmpresa,
                                                        IdAnio = info.IdAnioApertura,
                                                        IdSede = item_cm.IdSede,
                                                        IdNivel = item_cm.IdNivel,
                                                        IdJornada = item_cm.IdJornada,
                                                        IdCurso = item_cm.IdCurso,
                                                        IdMateria = item_cm.IdMateria,
                                                        NomMateria = info_materia.NomMateria,
                                                        NomMateriaGrupo = info_materia.NomMateriaGrupo,
                                                        EsObligatorio = info_materia.EsObligatorio,
                                                        OrdenMateria = info_materia.OrdenMateria
                                                    };
                                                    Context.aca_AnioLectivo_Curso_Materia.Add(Entity_CursoMateria);
                                                }

                                                var lst_asignacion_curso_documento = odata_curso_documento.get_list_asignacion(info.IdEmpresa, info.IdSede, info.IdAnio, Convert.ToInt32(item_niv.Key), Convert.ToInt32(item_jor.Key), Convert.ToInt32(item_cur.Key)).Where(q => q.seleccionado == true).ToList();

                                                foreach (var item_cd in lst_asignacion_curso_documento)
                                                {
                                                    var inf_documento = odata_documento.getInfo(info.IdEmpresa, item_cd.IdDocumento);
                                                    aca_AnioLectivo_Curso_Documento Entity_CursoDocumento = new aca_AnioLectivo_Curso_Documento
                                                    {
                                                        IdEmpresa = item_cd.IdEmpresa,
                                                        IdAnio = info.IdAnioApertura,
                                                        IdSede = item_cd.IdSede,
                                                        IdNivel = item_cd.IdNivel,
                                                        IdJornada = item_cd.IdJornada,
                                                        IdCurso = item_cd.IdCurso,
                                                        IdDocumento = item_cd.IdDocumento,
                                                        NomDocumento = inf_documento.NomDocumento,
                                                        OrdenDocumento = inf_documento.OrdenDocumento
                                                    };
                                                    Context.aca_AnioLectivo_Curso_Documento.Add(Entity_CursoDocumento);
                                                }
                                            }


                                        }
                                    }
                                }
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
    }
}
