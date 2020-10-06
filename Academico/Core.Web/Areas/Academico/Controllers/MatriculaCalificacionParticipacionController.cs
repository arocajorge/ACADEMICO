using Core.Info.Academico;
using Core.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class MatriculaCalificacionParticipacionController : Controller
    {
        
    }

    public class aca_MatriculaCalificacionParticipacion_Ingreso_List
    {
        string Variable = "aca_MatriculaCalificacionParticipacion_Ingreso_Info";
        public List<aca_MatriculaCalificacionParticipacion_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_MatriculaCalificacionParticipacion_Info> list = new List<aca_MatriculaCalificacionParticipacion_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_MatriculaCalificacionParticipacion_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_MatriculaCalificacionParticipacion_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void UpdateRow(aca_MatriculaCalificacionParticipacion_Info info_det, decimal IdTransaccionSession)
        {
            //int IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);
            //string IdUsuario = SessionFixed.IdUsuario;
            //bool EsSuperAdmin = Convert.ToBoolean(SessionFixed.EsSuperAdmin);
            //var info_profesor = bus_profesor.GetInfo_x_Usuario(IdEmpresa, IdUsuario);
            //var IdProfesor = (info_profesor == null ? 0 : info_profesor.IdProfesor);

            //aca_MatriculaCalificacionParticipacion_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdEmpresa == IdEmpresa && m.IdMatricula == info_det.IdMatricula).FirstOrDefault();

            //if (edited_info.IdProfesor > 0)
            //{
            //    edited_info.CalificacionP1 = info_det.CalificacionP1;
            //    var info_equivalencia = bus_equivalecia.getInfo(edited_info.IdEmpresa, edited_info.IdAnio, Convert.ToInt32(edited_info.IdCalificacionCualitativa));
            //    edited_info.Calificacion = info_equivalencia == null ? null : info_equivalencia.Calificacion;
            //    edited_info.Conducta = info_det.Conducta;
            //    edited_info.MotivoConducta = info_det.MotivoConducta;

            //    bus_calificacion_parcial.modificarDB(edited_info);
            //}
        }
    }
}