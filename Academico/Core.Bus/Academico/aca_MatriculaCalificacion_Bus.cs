using Core.Data.Academico;
using Core.Info.Academico;
using Core.Info.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_MatriculaCalificacion_Bus
    {
        aca_MatriculaCalificacion_Data odata = new aca_MatriculaCalificacion_Data();
        aca_AnioLectivoParcial_Data odata_parcial = new aca_AnioLectivoParcial_Data();
        aca_Alumno_Bus odata_alumno = new aca_Alumno_Bus();
        aca_MatriculaCalificacion_Data odata_calificacion = new aca_MatriculaCalificacion_Data();
        aca_AnioLectivo_Data odata_anio = new aca_AnioLectivo_Data();
        aca_Matricula_Data odata_matricula = new aca_Matricula_Data();

        public List<aca_MatriculaCalificacion_Info> GetList(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria)
        {
            try
            {
                return odata.getList(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo, IdMateria);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_MatriculaCalificacion_Info> GetList_PaseAnio(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, decimal IdAlumno)
        {
            try
            {
                return odata.getList_PaseAnio(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo, IdAlumno);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<aca_MatriculaCalificacion_Info> GetList_x_Profesor(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria, decimal IdProfesor)
        {
            try
            {
                return odata.getList_x_Profesor(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo, IdMateria, IdProfesor);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_MatriculaCalificacion_Info> GetList(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                return odata.getList(IdEmpresa, IdMatricula);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria,  List<aca_MatriculaCalificacion_Info> lista)
        {
            try
            {
                return odata.guardarDB(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo, IdMateria, lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GenerarCalificacion(List<aca_MatriculaCalificacion_Info> lst_calificacion)
        {
            try
            {
                return odata.generarCalificacion(lst_calificacion);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_MatriculaCalificacion_Info> GetList_Combos(int IdEmpresa, decimal IdProfesor, bool EsSuperAdmin)
        {
            try
            {
                return odata.getList_Combos(IdEmpresa, IdProfesor, EsSuperAdmin);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public aca_MatriculaCalificacion_Info GetInfo(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria, decimal IdAlumno)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo, IdMateria, IdAlumno);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ModicarDB(aca_MatriculaCalificacion_Info info)
        {
            try
            {
                if (odata.modificarDB(info))
                {
                    var info_matricula = odata_matricula.getInfo(info.IdEmpresa, info.IdMatricula);
                    var info_parcial = odata_parcial.getInfo(info.IdEmpresa, info_matricula.IdSede, info_matricula.IdAnio, info.IdCatalogoParcial);

                    if (info_parcial!=null)
                    {
                        if (info_parcial.ValidaEstadoAlumno == true)
                        {
                            var info_alumno = odata_alumno.GetInfo(info_matricula.IdEmpresa, info_matricula.IdAlumno);
                            var lista_calificacion = GetList_PaseAnio(info_matricula.IdEmpresa, info_matricula.IdAnio, info_matricula.IdSede, info_matricula.IdNivel, info_matricula.IdJornada, info_matricula.IdCurso, info_matricula.IdParalelo, info.IdAlumno);
                            var info_anio = odata_anio.getInfo(info.IdEmpresa, info_matricula.IdAnio);
                            var lst_agrupada = lista_calificacion.GroupBy(q => new { q.IdEmpresa, q.IdMatricula, q.IdMateria }).ToList();
                            decimal PromedioGeneral = 0;
                            decimal PromedioFinal = 0;
                            decimal PromedioFinalTemp = 0;
                            decimal PromedioMinimoPromocion = Math.Round(Convert.ToDecimal(info_anio.PromedioMinimoPromocion), 2, MidpointRounding.AwayFromZero);

                            foreach (var item_x_matricula in lista_calificacion)
                            {
                                decimal PromedioFinalQ1 = Convert.ToDecimal(item_x_matricula.PromedioFinalQ1);
                                decimal PromedioFinalQ2 = Convert.ToDecimal(item_x_matricula.PromedioFinalQ2);
                                decimal ExamenMejoramiento = Convert.ToDecimal(item_x_matricula.ExamenMejoramiento);
                                decimal ExamenSupletorio = Convert.ToDecimal(item_x_matricula.ExamenSupletorio);
                                decimal ExamenRemedial = Convert.ToDecimal(item_x_matricula.ExamenRemedial);
                                decimal ExamenGracia = Convert.ToDecimal(item_x_matricula.ExamenGracia);
                                PromedioFinalTemp = Math.Round(Convert.ToDecimal((PromedioFinalQ1 + PromedioFinalQ2) / 2), 2, MidpointRounding.AwayFromZero);

                                if (PromedioFinalTemp < PromedioMinimoPromocion)
                                {
                                    if (ExamenSupletorio >= PromedioMinimoPromocion)
                                    {
                                        PromedioFinal = PromedioMinimoPromocion;
                                    }
                                    else if (ExamenRemedial >= PromedioMinimoPromocion)
                                    {
                                        PromedioFinal = PromedioMinimoPromocion;
                                    }
                                    else if (ExamenGracia >= PromedioMinimoPromocion)
                                    {
                                        PromedioFinal = PromedioMinimoPromocion;
                                    }
                                    else
                                    {
                                        PromedioFinal = PromedioFinalTemp;
                                    }

                                }
                                else
                                {
                                    if (ExamenMejoramiento > 0)
                                    {
                                        if (PromedioFinalQ1 < PromedioFinalQ2)
                                        {
                                            PromedioFinalQ1 = ExamenMejoramiento;
                                        }
                                        else if (PromedioFinalQ2 < PromedioFinalQ1)
                                        {
                                            PromedioFinalQ2 = ExamenMejoramiento;
                                        }

                                        PromedioFinal = Math.Round(Convert.ToDecimal((PromedioFinalQ1 + PromedioFinalQ2) / 2), 2, MidpointRounding.AwayFromZero);
                                    }
                                    else
                                    {
                                        PromedioFinal = PromedioFinalTemp;
                                    }
                                }
                                info.PromedioFinal = PromedioFinal;
                                odata_calificacion.modicarPaseAnioDB(info);

                                if (PromedioFinal < PromedioMinimoPromocion)
                                {
                                    info_alumno.IdCatalogoESTMAT = Convert.ToInt32(cl_enumeradores.eCatalogoAcademicoAlumno.SUPLENCIA);
                                    odata_alumno.PaseAnioDB(info_alumno);
                                }
                                //PromedioGeneral = PromedioGeneral + PromedioFinal;
                            }

                            //PromedioGeneral = Math.Round((PromedioGeneral / lst_agrupada.Count()), 2, MidpointRounding.AwayFromZero);
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

        public bool ModicarPaseAnioDB(aca_MatriculaCalificacion_Info info)
        {
            try
            {
                return odata.modicarPaseAnioDB(info);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
