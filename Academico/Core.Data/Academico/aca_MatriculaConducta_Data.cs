using Core.Data.Base;
using Core.Info.Academico;
using Core.Info.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_MatriculaConducta_Data
    {
        aca_AnioLectivoConductaEquivalencia_Data odata_conducta = new aca_AnioLectivoConductaEquivalencia_Data();
        aca_AnioLectivoConductaEquivalencia_Data odata_conducta_equivalencia = new aca_AnioLectivoConductaEquivalencia_Data();
        aca_Matricula_Data odata_matricula = new aca_Matricula_Data();

        public List<aca_MatriculaConducta_Info> getList(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                List<aca_MatriculaConducta_Info> Lista = new List<aca_MatriculaConducta_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_MatriculaConducta.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaConducta_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            SecuenciaPromedioP1 = q.SecuenciaPromedioP1,
                            PromedioP1 = q.PromedioP1,
                            SecuenciaPromedioFinalP1 = q.SecuenciaPromedioFinalP1,
                            PromedioFinalP1 = q.PromedioFinalP1,
                            SecuenciaPromedioP2 = q.SecuenciaPromedioP2,
                            PromedioP2 = q.PromedioP2,
                            SecuenciaPromedioFinalP2 = q.SecuenciaPromedioFinalP2,
                            PromedioFinalP2 = q.PromedioFinalP2,
                            SecuenciaPromedioP3 = q.SecuenciaPromedioP3,
                            PromedioP3 = q.PromedioP3,
                            SecuenciaPromedioFinalP3 = q.SecuenciaPromedioFinalP3,
                            PromedioFinalP3 = q.PromedioFinalP3,
                            SecuenciaPromedioP4 = q.SecuenciaPromedioP4,
                            PromedioP4 = q.PromedioP4,
                            SecuenciaPromedioFinalP4 = q.SecuenciaPromedioFinalP4,
                            PromedioFinalP4 = q.PromedioFinalP4,
                            SecuenciaPromedioP5 = q.SecuenciaPromedioP5,
                            PromedioP5 = q.PromedioP5,
                            SecuenciaPromedioFinalP5 = q.SecuenciaPromedioFinalP5,
                            PromedioFinalP5 = q.PromedioFinalP5,
                            SecuenciaPromedioP6 = q.SecuenciaPromedioP6,
                            PromedioP6 = q.PromedioP6,
                            SecuenciaPromedioFinalP6 = q.SecuenciaPromedioFinalP6,
                            PromedioFinalP6 = q.PromedioFinalP6,
                            SecuenciaPromedioQ1 = q.SecuenciaPromedioQ1,
                            PromedioQ1 = q.PromedioQ1,
                            SecuenciaPromedioFinalQ1 = q.SecuenciaPromedioFinalQ1,
                            PromedioFinalQ1 = q.PromedioFinalQ1,
                            SecuenciaPromedioQ2 = q.SecuenciaPromedioQ2,
                            PromedioQ2 = q.PromedioQ2,
                            SecuenciaPromedioFinalQ2 = q.SecuenciaPromedioFinalQ2,
                            PromedioFinalQ2 = q.PromedioFinalQ2,
                            SecuenciaPromedioGeneral = q.SecuenciaPromedioGeneral,
                            PromedioGeneral = q.PromedioGeneral,
                            SecuenciaPromedioFinal = q.SecuenciaPromedioFinal,
                            PromedioFinal = q.PromedioFinal
                        });
                    });
                }

                return Lista;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public aca_MatriculaConducta_Info getInfo(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                aca_MatriculaConducta_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_MatriculaConducta.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_MatriculaConducta_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMatricula = Entity.IdMatricula,
                        SecuenciaPromedioP1 = Entity.SecuenciaPromedioP1,
                        PromedioP1 = Entity.PromedioP1,
                        SecuenciaPromedioFinalP1 = Entity.SecuenciaPromedioFinalP1,
                        PromedioFinalP1 = Entity.PromedioFinalP1,
                        SecuenciaPromedioP2 = Entity.SecuenciaPromedioP2,
                        PromedioP2 = Entity.PromedioP2,
                        SecuenciaPromedioFinalP2 = Entity.SecuenciaPromedioFinalP2,
                        PromedioFinalP2 = Entity.PromedioFinalP2,
                        SecuenciaPromedioP3 = Entity.SecuenciaPromedioP3,
                        PromedioP3 = Entity.PromedioP3,
                        SecuenciaPromedioFinalP3 = Entity.SecuenciaPromedioFinalP3,
                        PromedioFinalP3 = Entity.PromedioFinalP3,
                        SecuenciaPromedioP4 = Entity.SecuenciaPromedioP4,
                        PromedioP4 = Entity.PromedioP4,
                        SecuenciaPromedioFinalP4 = Entity.SecuenciaPromedioFinalP4,
                        PromedioFinalP4 = Entity.PromedioFinalP4,
                        SecuenciaPromedioP5 = Entity.SecuenciaPromedioP5,
                        PromedioP5 = Entity.PromedioP5,
                        SecuenciaPromedioFinalP5 = Entity.SecuenciaPromedioFinalP5,
                        PromedioFinalP5 = Entity.PromedioFinalP5,
                        SecuenciaPromedioP6 = Entity.SecuenciaPromedioP6,
                        PromedioP6 = Entity.PromedioP6,
                        SecuenciaPromedioFinalP6 = Entity.SecuenciaPromedioFinalP6,
                        PromedioFinalP6 = Entity.PromedioFinalP6,
                        SecuenciaPromedioQ1 = Entity.SecuenciaPromedioQ1,
                        PromedioQ1 = Entity.PromedioQ1,
                        SecuenciaPromedioFinalQ1 = Entity.SecuenciaPromedioFinalQ1,
                        PromedioFinalQ1 = Entity.PromedioFinalQ1,
                        SecuenciaPromedioQ2 = Entity.SecuenciaPromedioQ2,
                        PromedioQ2 = Entity.PromedioQ2,
                        SecuenciaPromedioFinalQ2 = Entity.SecuenciaPromedioFinalQ2,
                        PromedioFinalQ2 = Entity.PromedioFinalQ2,
                        SecuenciaPromedioGeneral = Entity.SecuenciaPromedioGeneral,
                        PromedioGeneral = Entity.PromedioGeneral,
                        SecuenciaPromedioFinal = Entity.SecuenciaPromedioFinal,
                        PromedioFinal = Entity.PromedioFinal
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_MatriculaConducta_Info> getList_PaseAnio(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, decimal IdAlumno)
        {
            try
            {
                List<aca_MatriculaConducta_Info> Lista = new List<aca_MatriculaConducta_Info>();
                int IdNivelIni = IdNivel;
                int IdNivelFin = IdNivel == 0 ? 9999999 : IdNivel;

                int IdJornadaIni = IdJornada;
                int IdJornadaFin = IdJornada == 0 ? 9999999 : IdJornada;

                int IdCursoIni = IdCurso;
                int IdCursoFin = IdCurso == 0 ? 9999999 : IdCurso;

                int IdParaleloIni = IdParalelo;
                int IdParaleloFin = IdParalelo == 0 ? 9999999 : IdParalelo;

                decimal IdAlumnoIni = IdAlumno;
                decimal IdAlumnoFin = IdAlumno == 0 ? 9999999 : IdAlumno;

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_MatriculaConducta.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede
                    && q.IdNivel >= IdNivelIni && q.IdNivel <= IdNivelFin && q.IdJornada >= IdJornadaIni && q.IdJornada <= IdJornadaFin
                    && q.IdCurso >= IdCursoIni && q.IdCurso <= IdCursoFin && q.IdParalelo >= IdParaleloIni && q.IdParalelo <= IdParaleloFin
                    && q.IdAlumno >= IdAlumnoIni && q.IdAlumno <= IdAlumnoFin).OrderBy(q => q.pe_nombreCompleto).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaConducta_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdAlumno = q.IdAlumno,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel=q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso=q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            SecuenciaPromedioP1 = q.SecuenciaPromedioP1,
                            PromedioP1 = q.PromedioP1,
                            SecuenciaPromedioFinalP1 = q.SecuenciaPromedioFinalP1,
                            PromedioFinalP1 = q.PromedioFinalP1,
                            SecuenciaPromedioP2 = q.SecuenciaPromedioP2,
                            PromedioP2 = q.PromedioP2,
                            SecuenciaPromedioFinalP2 = q.SecuenciaPromedioFinalP2,
                            PromedioFinalP2 = q.PromedioFinalP2,
                            SecuenciaPromedioP3 = q.SecuenciaPromedioP3,
                            PromedioP3 = q.PromedioP3,
                            SecuenciaPromedioFinalP3 = q.SecuenciaPromedioFinalP3,
                            PromedioFinalP3 = q.PromedioFinalP3,
                            SecuenciaPromedioP4 = q.SecuenciaPromedioP4,
                            PromedioP4 = q.PromedioP4,
                            SecuenciaPromedioFinalP4 = q.SecuenciaPromedioFinalP4,
                            PromedioFinalP4 = q.PromedioFinalP4,
                            SecuenciaPromedioP5 = q.SecuenciaPromedioP5,
                            PromedioP5 = q.PromedioP5,
                            SecuenciaPromedioFinalP5 = q.SecuenciaPromedioFinalP5,
                            PromedioFinalP5 = q.PromedioFinalP5,
                            SecuenciaPromedioP6 = q.SecuenciaPromedioP6,
                            PromedioP6 = q.PromedioP6,
                            SecuenciaPromedioFinalP6 = q.SecuenciaPromedioFinalP6,
                            PromedioFinalP6 = q.PromedioFinalP6,
                            SecuenciaPromedioQ1 = q.SecuenciaPromedioQ1,
                            PromedioQ1 = q.PromedioQ1,
                            SecuenciaPromedioFinalQ1 = q.SecuenciaPromedioFinalQ1,
                            PromedioFinalQ1 = q.PromedioFinalQ1,
                            SecuenciaPromedioQ2 = q.SecuenciaPromedioQ2,
                            PromedioQ2 = q.PromedioQ2,
                            SecuenciaPromedioFinalQ2 = q.SecuenciaPromedioFinalQ2,
                            PromedioFinalQ2 = q.PromedioFinalQ2,
                            SecuenciaPromedioGeneral = q.SecuenciaPromedioGeneral,
                            PromedioGeneral = q.PromedioGeneral,
                            SecuenciaPromedioFinal = q.SecuenciaPromedioFinal,
                            PromedioFinal = q.PromedioFinal
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

        public List<aca_MatriculaConducta_Info> getList_Combos(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo)
        {
            try
            {
                int IdNivelIni = IdNivel;
                int IdNivelFin = IdNivel == 0 ? 9999999 : IdNivel;

                int IdJornadaIni = IdJornada;
                int IdJornadaFin = IdJornada == 0 ? 9999999 : IdJornada;

                int IdCursoIni = IdCurso;
                int IdCursoFin = IdCurso == 0 ? 9999999 : IdCurso;

                int IdParaleloIni = IdParalelo;
                int IdParaleloFin = IdParalelo == 0 ? 9999999 : IdParalelo;

                List<aca_MatriculaConducta_Info> Lista = new List<aca_MatriculaConducta_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_AnioLectivo_Paralelo_Conducta.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio==IdAnio
                    && q.IdNivel >= IdNivelIni && q.IdNivel <= IdNivelFin && q.IdJornada >= IdJornadaIni && q.IdJornada <= IdJornadaFin
                    && q.IdCurso >= IdCursoIni && q.IdCurso <= IdCursoFin && q.IdParalelo >= IdParaleloIni && q.IdParalelo <= IdParaleloFin).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaConducta_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            Descripcion = q.Descripcion,
                            NomSede = q.NomSede,
                            NomNivel = q.NomNivel,
                            OrdenNivel = q.OrdenNivel ?? 0,
                            NomJornada = q.NomJornada,
                            OrdenJornada = q.OrdenJornada ?? 0,
                            NomCurso = q.NomCurso,
                            OrdenCurso = q.OrdenCurso ?? 0,
                            NomParalelo = q.NomParalelo,
                            OrdenParalelo = q.OrdenParalelo,
                            CodigoParalelo = q.CodigoParalelo,
                            IdProfesorTutor = q.IdProfesorTutor ?? 0,
                            IdProfesorInspector = q.IdProfesorInspector ?? 0,
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

        public List<aca_MatriculaConducta_Info> getList_Combos_Inspector(int IdEmpresa, decimal IdProfesor, bool EsSuperAdmin)
        {
            try
            {
                List<aca_MatriculaConducta_Info> Lista = new List<aca_MatriculaConducta_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_AnioLectivo_Paralelo_Profesor_Calificaciones.Where(q => q.IdEmpresa == IdEmpresa
                    && q.IdProfesorInspector == (EsSuperAdmin == true ? q.IdProfesorInspector : IdProfesor)).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaConducta_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            Descripcion = q.Descripcion,
                            NomSede = q.NomSede,
                            NomNivel = q.NomNivel,
                            OrdenNivel = q.OrdenNivel ?? 0,
                            NomJornada = q.NomJornada,
                            OrdenJornada = q.OrdenJornada ?? 0,
                            NomCurso = q.NomCurso,
                            OrdenCurso = q.OrdenCurso ?? 0,
                            NomParalelo = q.NomParalelo,
                            OrdenParalelo = q.OrdenParalelo ?? 0,
                            CodigoParalelo = q.CodigoParalelo,
                            IdProfesorTutor = q.IdProfesorTutor ?? 0,
                            IdProfesorInspector = q.IdProfesorInspector ?? 0
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
        public List<aca_MatriculaConducta_Info> getList(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo)
        {
            try
            {
                List<aca_MatriculaConducta_Info> Lista = new List<aca_MatriculaConducta_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_MatriculaConducta.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio == IdAnio && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso && q.IdParalelo == IdParalelo).OrderBy(q=>q.pe_nombreCompleto).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaConducta_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdAlumno = q.IdAlumno,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            SecuenciaPromedioP1 = q.SecuenciaPromedioP1,
                            PromedioP1 = q.PromedioP1,
                            SecuenciaPromedioFinalP1 = q.SecuenciaPromedioFinalP1,
                            PromedioFinalP1 = q.PromedioFinalP1,
                            SecuenciaPromedioP2 = q.SecuenciaPromedioP2,
                            PromedioP2 = q.PromedioP2,
                            SecuenciaPromedioFinalP2 = q.SecuenciaPromedioFinalP2,
                            PromedioFinalP2 = q.PromedioFinalP2,
                            SecuenciaPromedioP3 = q.SecuenciaPromedioP3,
                            PromedioP3 = q.PromedioP3,
                            SecuenciaPromedioFinalP3 = q.SecuenciaPromedioFinalP3,
                            PromedioFinalP3 = q.PromedioFinalP3,
                            SecuenciaPromedioP4 = q.SecuenciaPromedioP4,
                            PromedioP4 = q.PromedioP4,
                            SecuenciaPromedioFinalP4 = q.SecuenciaPromedioFinalP4,
                            PromedioFinalP4 = q.PromedioFinalP4,
                            SecuenciaPromedioP5 = q.SecuenciaPromedioP5,
                            PromedioP5 = q.PromedioP5,
                            SecuenciaPromedioFinalP5 = q.SecuenciaPromedioFinalP5,
                            PromedioFinalP5 = q.PromedioFinalP5,
                            SecuenciaPromedioP6 = q.SecuenciaPromedioP6,
                            PromedioP6 = q.PromedioP6,
                            SecuenciaPromedioFinalP6 = q.SecuenciaPromedioFinalP6,
                            PromedioFinalP6 = q.PromedioFinalP6,
                            SecuenciaPromedioQ1 = q.SecuenciaPromedioQ1,
                            PromedioQ1 = q.PromedioQ1,
                            SecuenciaPromedioFinalQ1 = q.SecuenciaPromedioFinalQ1,
                            PromedioFinalQ1 = q.PromedioFinalQ1,
                            SecuenciaPromedioQ2 = q.SecuenciaPromedioQ2,
                            PromedioQ2 = q.PromedioQ2,
                            SecuenciaPromedioFinalQ2 = q.SecuenciaPromedioFinalQ2,
                            PromedioFinalQ2 = q.PromedioFinalQ2,
                            SecuenciaPromedioGeneral = q.SecuenciaPromedioGeneral,
                            PromedioGeneral = q.PromedioGeneral,
                            SecuenciaPromedioFinal = q.SecuenciaPromedioFinal,
                            PromedioFinal = q.PromedioFinal,
                            MotivoPromedioFinalP1 = q.MotivoPromedioFinalP1,
                            MotivoPromedioFinalP2 = q.MotivoPromedioFinalP2,
                            MotivoPromedioFinalP3 = q.MotivoPromedioFinalP3,
                            MotivoPromedioFinalQ1 = q.MotivoPromedioFinalQ1,
                            MotivoPromedioFinalP4 = q.MotivoPromedioFinalP4,
                            MotivoPromedioFinalP5 = q.MotivoPromedioFinalP5,
                            MotivoPromedioFinalP6 = q.MotivoPromedioFinalP6,
                            MotivoPromedioFinalQ2 = q.MotivoPromedioFinalQ2,
                            MotivoPromedioFinal = q.MotivoPromedioFinal,
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

        public bool generarCalificacion(List<aca_MatriculaConducta_Info> lst_conducta)
        {
            try
            {
                List<aca_MatriculaConducta_Info> Lista = new List<aca_MatriculaConducta_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    foreach (var info in lst_conducta)
                    {
                        var lista_calificacion_conducta = Context.aca_MatriculaConducta.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula).ToList();
                        Context.aca_MatriculaConducta.RemoveRange(lista_calificacion_conducta);

                        aca_MatriculaConducta Entity = new aca_MatriculaConducta
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdMatricula = info.IdMatricula,
                            SecuenciaPromedioP1 = info.SecuenciaPromedioP1,
                            PromedioP1 = info.PromedioP1,
                            SecuenciaPromedioFinalP1 = info.SecuenciaPromedioFinalP1,
                            PromedioFinalP1 = info.PromedioFinalP1,
                            SecuenciaPromedioP2 = info.SecuenciaPromedioP2,
                            PromedioP2 = info.PromedioP2,
                            SecuenciaPromedioFinalP2 = info.SecuenciaPromedioFinalP2,
                            PromedioFinalP2 = info.PromedioFinalP2,
                            SecuenciaPromedioP3 = info.SecuenciaPromedioP3,
                            PromedioP3 = info.PromedioP3,
                            SecuenciaPromedioFinalP3 = info.SecuenciaPromedioFinalP3,
                            PromedioFinalP3 = info.PromedioFinalP3,
                            SecuenciaPromedioP4 = info.SecuenciaPromedioP4,
                            PromedioP4 = info.PromedioP4,
                            SecuenciaPromedioFinalP4 = info.SecuenciaPromedioFinalP4,
                            PromedioFinalP4 = info.PromedioFinalP4,
                            SecuenciaPromedioP5 = info.SecuenciaPromedioP5,
                            PromedioP5 = info.PromedioP5,
                            SecuenciaPromedioFinalP5 = info.SecuenciaPromedioFinalP5,
                            PromedioFinalP5 = info.PromedioFinalP5,
                            SecuenciaPromedioP6 = info.SecuenciaPromedioP6,
                            PromedioP6 = info.PromedioP6,
                            SecuenciaPromedioFinalP6 = info.SecuenciaPromedioFinalP6,
                            PromedioFinalP6 = info.PromedioFinalP6,
                            SecuenciaPromedioQ1 = info.SecuenciaPromedioQ1,
                            PromedioQ1 = info.PromedioQ1,
                            SecuenciaPromedioFinalQ1 = info.SecuenciaPromedioFinalQ1,
                            PromedioFinalQ1 = info.PromedioFinalQ1,
                            SecuenciaPromedioQ2 = info.SecuenciaPromedioQ2,
                            PromedioQ2 = info.PromedioQ2,
                            SecuenciaPromedioFinalQ2 = info.SecuenciaPromedioFinalQ2,
                            PromedioFinalQ2 = info.PromedioFinalQ2,
                            SecuenciaPromedioGeneral = info.SecuenciaPromedioGeneral,
                            PromedioGeneral = info.PromedioGeneral,
                            SecuenciaPromedioFinal = info.SecuenciaPromedioFinal,
                            PromedioFinal = info.PromedioFinal
                        };

                        Context.aca_MatriculaConducta.Add(Entity);
                        Context.SaveChanges();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool modicarPromedioFinal(aca_MatriculaConducta_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MatriculaConducta EntityConducta = Context.aca_MatriculaConducta.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa  && q.IdMatricula == info.IdMatricula);
                    if (EntityConducta == null)
                        return false;

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P1))
                    {
                        EntityConducta.SecuenciaPromedioFinalP1 = info.SecuenciaConductaPromedioParcialFinal;
                        EntityConducta.PromedioFinalP1 = info.ConductaPromedioParcialFinal;
                        EntityConducta.MotivoPromedioFinalP1 = info.MotivoPromedioParcialFinal;
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P2))
                    {
                        EntityConducta.SecuenciaPromedioFinalP2 = info.SecuenciaConductaPromedioParcialFinal;
                        EntityConducta.PromedioFinalP2 = info.ConductaPromedioParcialFinal;
                        EntityConducta.MotivoPromedioFinalP2 = info.MotivoPromedioParcialFinal;
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P3))
                    {
                        EntityConducta.SecuenciaPromedioFinalP3 = info.SecuenciaConductaPromedioParcialFinal;
                        EntityConducta.PromedioFinalP3 = info.ConductaPromedioParcialFinal;
                        EntityConducta.MotivoPromedioFinalP3 = info.MotivoPromedioParcialFinal;
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P4))
                    {
                        EntityConducta.SecuenciaPromedioFinalP4 = info.SecuenciaConductaPromedioParcialFinal;
                        EntityConducta.PromedioFinalP4 = info.ConductaPromedioParcialFinal;
                        EntityConducta.MotivoPromedioFinalP4 = info.MotivoPromedioParcialFinal;
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P5))
                    {
                        EntityConducta.SecuenciaPromedioFinalP5 = info.SecuenciaConductaPromedioParcialFinal;
                        EntityConducta.PromedioFinalP5 = info.ConductaPromedioParcialFinal;
                        EntityConducta.MotivoPromedioFinalP5 = info.MotivoPromedioParcialFinal;
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P6))
                    {
                        EntityConducta.SecuenciaPromedioFinalP6 = info.SecuenciaConductaPromedioParcialFinal;
                        EntityConducta.PromedioFinalP6 = info.ConductaPromedioParcialFinal;
                        EntityConducta.MotivoPromedioFinalP6 = info.MotivoPromedioParcialFinal;
                    }

                    Context.SaveChanges();

                    aca_MatriculaConducta EntityConductaPromedioQuim = Context.aca_MatriculaConducta.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula);
                    if (EntityConductaPromedioQuim == null)
                        return false;

                    var info_matricula = odata_matricula.getInfo(info.IdEmpresa, info.IdMatricula);
                    var info_conducta = odata_conducta_equivalencia.getInfoXPromedioConducta(info.IdEmpresa, info_matricula.IdAnio, Convert.ToDecimal(info.ConductaPromedioParcialFinal));
                    var infoConductaMinima = odata_conducta_equivalencia.getInfo_MinimaConducta(info.IdEmpresa, info_matricula.IdAnio);
                    var SecuenciaConductaMinima = infoConductaMinima.Secuencia;

                    double SumaPromedioQuimestre = 0;
                    double PromedioQuimestre = 0;
                    var SecuenciaConducta = (int?)null;
                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P1) || info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P2) || info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P3))
                    {
                        SumaPromedioQuimestre = Convert.ToDouble((EntityConductaPromedioQuim.PromedioFinalP1>0 ? EntityConductaPromedioQuim.PromedioFinalP1 : EntityConductaPromedioQuim.PromedioP1) + (EntityConductaPromedioQuim.PromedioFinalP2 > 0 ? EntityConductaPromedioQuim.PromedioFinalP2 : EntityConductaPromedioQuim.PromedioP2) + (EntityConductaPromedioQuim.PromedioFinalP3 > 0 ? EntityConductaPromedioQuim.PromedioFinalP3 : EntityConductaPromedioQuim.PromedioP3));
                        PromedioQuimestre = Convert.ToDouble(SumaPromedioQuimestre / 3);
                        var info_conductaQ1 = odata_conducta_equivalencia.getInfoXPromedioConducta(info.IdEmpresa, info.IdAnio, Convert.ToDecimal(PromedioQuimestre));
                        var infoMinimaConductaQ1 = odata_conducta_equivalencia.getInfo_MinimaConducta(info.IdEmpresa, info_matricula.IdAnio);
                        SecuenciaConducta = infoMinimaConductaQ1.Secuencia;

                        EntityConductaPromedioQuim.PromedioQ1 = PromedioQuimestre;
                        EntityConductaPromedioQuim.SecuenciaPromedioQ1 = info_conductaQ1 == null ? SecuenciaConducta : info_conductaQ1.Secuencia;
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P4) || info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P5) || info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P6))
                    {
                        SumaPromedioQuimestre = Convert.ToDouble((EntityConductaPromedioQuim.PromedioFinalP4 > 0 ? EntityConductaPromedioQuim.PromedioFinalP4 : EntityConductaPromedioQuim.PromedioP4) + (EntityConductaPromedioQuim.PromedioFinalP5 > 0 ? EntityConductaPromedioQuim.PromedioFinalP5 : EntityConductaPromedioQuim.PromedioP5) + (EntityConductaPromedioQuim.PromedioFinalP6 > 0 ? EntityConductaPromedioQuim.PromedioFinalP6 : EntityConductaPromedioQuim.PromedioP6));
                        PromedioQuimestre = Convert.ToDouble(SumaPromedioQuimestre / 3);
                        var info_conductaQ2 = odata_conducta_equivalencia.getInfoXPromedioConducta(info.IdEmpresa, info.IdAnio, Convert.ToDecimal(PromedioQuimestre));
                        var infoMinimaConductaQ2 = odata_conducta_equivalencia.getInfo_MinimaConducta(info.IdEmpresa, info_matricula.IdAnio);
                        SecuenciaConducta = infoMinimaConductaQ2.Secuencia;

                        EntityConductaPromedioQuim.PromedioQ2 = PromedioQuimestre;
                        EntityConductaPromedioQuim.SecuenciaPromedioQ2 = info_conductaQ2 == null ? SecuenciaConducta : info_conductaQ2.Secuencia;
                    }

                    double SumaPromedioGeneral = 0;
                    double PromedioGeneral = 0;
                    var SecuenciaConductaGeneral = (int?)null;
                    SumaPromedioGeneral = Convert.ToDouble((EntityConductaPromedioQuim.PromedioFinalQ1 > 0 ? EntityConductaPromedioQuim.PromedioFinalQ1 : EntityConductaPromedioQuim.PromedioQ1) + (EntityConductaPromedioQuim.PromedioFinalQ2 > 0 ? EntityConductaPromedioQuim.PromedioFinalQ2 : EntityConductaPromedioQuim.PromedioQ2));
                    PromedioGeneral = Convert.ToDouble(SumaPromedioGeneral / 2);
                    var info_conducta_general = odata_conducta_equivalencia.getInfoXPromedioConducta(info.IdEmpresa, info.IdAnio, Convert.ToDecimal(PromedioGeneral));
                    var infoMinimaConductaGeneral = odata_conducta_equivalencia.getInfo_MinimaConducta(info.IdEmpresa, info_matricula.IdAnio);
                    SecuenciaConductaGeneral = infoMinimaConductaGeneral.Secuencia;

                    EntityConductaPromedioQuim.PromedioGeneral = PromedioGeneral;
                    EntityConductaPromedioQuim.SecuenciaPromedioGeneral = info_conducta_general == null ? SecuenciaConductaGeneral : info_conducta_general.Secuencia;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool modicarPromedioFinalQuimestre(aca_MatriculaConducta_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MatriculaConducta EntityConducta = Context.aca_MatriculaConducta.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula);
                    if (EntityConducta == null)
                        return false;

                    if (info.IdPromedioFinal == cl_enumeradores.eTipoCatalogoAcademicoConductaFinal.QUIMESTRE_1.ToString())
                    {
                        EntityConducta.SecuenciaPromedioFinalQ1 = info.SecuenciaConductaPromedioParcialFinal;
                        EntityConducta.PromedioFinalQ1 = info.ConductaPromedioParcialFinal;
                        EntityConducta.MotivoPromedioFinalQ1 = info.MotivoPromedioParcialFinal;
                    }

                    if (info.IdPromedioFinal == cl_enumeradores.eTipoCatalogoAcademicoConductaFinal.QUIMESTRE_2.ToString())
                    {
                        EntityConducta.SecuenciaPromedioFinalQ2 = info.SecuenciaConductaPromedioParcialFinal;
                        EntityConducta.PromedioFinalQ2 = info.ConductaPromedioParcialFinal;
                        EntityConducta.MotivoPromedioFinalQ2 = info.MotivoPromedioParcialFinal;
                    }

                    if (info.IdPromedioFinal == cl_enumeradores.eTipoCatalogoAcademicoConductaFinal.PROMEDIOFINAL.ToString())
                    {
                        EntityConducta.SecuenciaPromedioFinal = info.SecuenciaConductaPromedioParcialFinal;
                        EntityConducta.PromedioFinal = info.ConductaPromedioParcialFinal;
                        EntityConducta.MotivoPromedioFinal = info.MotivoPromedioParcialFinal;
                    }
                    Context.SaveChanges();

                    double SumaPromedioGeneral = 0;
                    double PromedioGeneral = 0;
                    var SecuenciaConductaGeneral = (int?)null;
                    SumaPromedioGeneral = Convert.ToDouble((EntityConducta.PromedioFinalQ1 > 0 ? EntityConducta.PromedioFinalQ1 : EntityConducta.PromedioQ1) + (EntityConducta.PromedioFinalQ2 > 0 ? EntityConducta.PromedioFinalQ2 : EntityConducta.PromedioQ2));
                    PromedioGeneral = Convert.ToDouble(SumaPromedioGeneral / 2);
                    var info_matricula = odata_matricula.getInfo(info.IdEmpresa, info.IdMatricula);
                    var info_conducta_general = odata_conducta_equivalencia.getInfoXPromedioConducta(info.IdEmpresa, info.IdAnio, Convert.ToDecimal(PromedioGeneral));
                    var infoMinimaConductaGeneral = odata_conducta_equivalencia.getInfo_MinimaConducta(info.IdEmpresa, info_matricula.IdAnio);
                    SecuenciaConductaGeneral = infoMinimaConductaGeneral.Secuencia;

                    EntityConducta.PromedioGeneral = PromedioGeneral;
                    EntityConducta.SecuenciaPromedioGeneral = info_conducta_general == null ? SecuenciaConductaGeneral : info_conducta_general.Secuencia;

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool modicarPromedioPaseAnio(aca_MatriculaConducta_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MatriculaConducta EntityConducta = Context.aca_MatriculaConducta.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula);
                    if (EntityConducta == null)
                        return false;

                    EntityConducta.SecuenciaPromedioGeneral = info.SecuenciaPromedioGeneral;
                    EntityConducta.PromedioGeneral = info.PromedioGeneral;

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
