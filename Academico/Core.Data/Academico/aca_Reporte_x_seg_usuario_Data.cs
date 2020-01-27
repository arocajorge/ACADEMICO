using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_Reporte_x_seg_usuario_Data
    {
        public List<aca_Reporte_x_seg_usuario_Info> get_list(int IdEmpresa, string IdUsuario, bool MostrarNoAsignados)
        {
            try
            {
                List<aca_Reporte_x_seg_usuario_Info> Lista;
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = (from q in Context.aca_Reporte_x_seg_usuario
                             join r in Context.aca_Reporte
                             on q.CodReporte equals r.CodReporte
                             where q.IdEmpresa == IdEmpresa
                             && q.IdUsuario == IdUsuario
                             && r.se_muestra_administrador_reportes == true
                             select new aca_Reporte_x_seg_usuario_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdUsuario = q.IdUsuario,
                                 CodReporte = q.CodReporte,
                                 nom_reporte = r.nom_reporte,
                                 observacion = r.observacion,
                                 mvc_area = r.mvc_area,
                                 mvc_controlador = r.mvc_controlador,
                                 mvc_accion = r.mvc_accion,
                                 seleccionado = true
                             }).ToList();

                    if (MostrarNoAsignados)
                    {
                        Lista.AddRange((from q in Context.aca_Reporte
                                        where !Context.aca_Reporte_x_seg_usuario.Any(meu => meu.CodReporte == q.CodReporte && meu.IdEmpresa == IdEmpresa && meu.IdUsuario == IdUsuario)
                                        && q.se_muestra_administrador_reportes == true
                                        select new aca_Reporte_x_seg_usuario_Info
                                        {
                                            IdEmpresa = IdEmpresa,
                                            IdUsuario = IdUsuario,
                                            CodReporte = q.CodReporte,
                                            nom_reporte = q.nom_reporte,
                                            observacion = q.observacion,
                                            seleccionado = false
                                        }).ToList());
                    }
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool eliminarDB(int IdEmpresa, string IdUsuario)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Context.Database.ExecuteSqlCommand("DELETE aca_Reporte_x_seg_usuario WHERE IdEmpresa = " + IdEmpresa + " AND IdUsuario = '" + IdUsuario + "'");
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(List<aca_Reporte_x_seg_usuario_Info> Lista, int IdEmpresa, string IdUsuario)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    foreach (var item in Lista)
                    {
                        Context.aca_Reporte_x_seg_usuario.Add(new aca_Reporte_x_seg_usuario
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdUsuario = item.IdUsuario,
                            CodReporte = item.CodReporte
                        });
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
