using Core.Data.Base;
using Core.Info.Academico;
using Core.Info.SeguridadAcceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_Menu_x_seg_usuario_Data
    {
        public List<aca_Menu_x_seg_usuario_Info> get_list(int IdEmpresa, int IdSede, string IdUsuario, bool MostrarTodo)
        {
            try
            {
                List<aca_Menu_x_seg_usuario_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = (from m in Context.aca_Menu
                             join me in Context.aca_Menu_x_aca_Sede
                             on m.IdMenu equals me.IdMenu
                             join meu in Context.aca_Menu_x_seg_usuario
                             on new { me.IdEmpresa, me.IdSede, me.IdMenu } equals new { meu.IdEmpresa, meu.IdSede, meu.IdMenu }
                             where m.Estado == true && meu.IdEmpresa == IdEmpresa
                             && meu.IdUsuario == IdUsuario
                             && meu.IdSede == IdSede
                             orderby m.PosicionMenu
                             select new aca_Menu_x_seg_usuario_Info
                             {
                                 seleccionado = true,
                                 IdEmpresa = meu.IdEmpresa,
                                 IdSede = meu.IdSede,
                                 IdUsuario = meu.IdUsuario,
                                 IdMenu = meu.IdMenu,
                                 IdMenuPadre = m.IdMenuPadre,
                                 DescripcionMenu = m.DescripcionMenu,
                                 Nuevo = meu.Nuevo,
                                 Modificar = meu.Modificar,
                                 Anular = meu.Anular,
                                 info_menu = new aca_Menu_Info
                                 {
                                     IdMenu = m.IdMenu,
                                     DescripcionMenu = m.DescripcionMenu,
                                     IdMenuPadre = m.IdMenuPadre,
                                     PosicionMenu = m.PosicionMenu,
                                     web_nom_Action = m.web_nom_Action,
                                     web_nom_Area = m.web_nom_Area,
                                     web_nom_Controller = m.web_nom_Controller
                                 }
                             }).ToList();

                    if (MostrarTodo)
                        Lista.AddRange((from q in Context.aca_Menu
                                        join me in Context.aca_Menu_x_aca_Sede
                                        on q.IdMenu equals me.IdMenu
                                        where q.Estado == true && me.IdEmpresa == IdEmpresa && me.IdSede == IdSede
                                        && !Context.aca_Menu_x_seg_usuario.Any(meu => meu.IdMenu == q.IdMenu && meu.IdEmpresa == IdEmpresa && me.IdSede == IdSede && meu.IdUsuario == IdUsuario)
                                        select new aca_Menu_x_seg_usuario_Info
                                        {
                                            seleccionado = false,
                                            IdEmpresa = IdEmpresa,
                                            IdSede = IdSede,
                                            IdUsuario = IdUsuario,
                                            IdMenu = q.IdMenu,
                                            IdMenuPadre = q.IdMenuPadre,
                                            DescripcionMenu = q.DescripcionMenu,
                                            Nuevo = true,
                                            Modificar = true,
                                            Anular = true,
                                            info_menu = new aca_Menu_Info
                                            {
                                                IdMenu = q.IdMenu,
                                                DescripcionMenu = q.DescripcionMenu,
                                                IdMenuPadre = q.IdMenuPadre,
                                                PosicionMenu = q.PosicionMenu
                                            }

                                        }).ToList());
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_Menu_x_seg_usuario_Info> get_list(int IdEmpresa, int IdSede, string IdUsuario, int IdMenuPadre)
        {
            try
            {
                List<aca_Menu_x_seg_usuario_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = (from m in Context.aca_Menu
                             join me in Context.aca_Menu_x_aca_Sede
                             on m.IdMenu equals me.IdMenu
                             join meu in Context.aca_Menu_x_seg_usuario
                             on new { me.IdEmpresa, me.IdSede, me.IdMenu } equals new { meu.IdEmpresa, meu.IdSede, meu.IdMenu }
                             where m.Estado == true && meu.IdEmpresa == IdEmpresa && meu.IdSede== IdSede
                             && meu.IdUsuario == IdUsuario && m.IdMenuPadre == IdMenuPadre
                             orderby m.PosicionMenu
                             select new aca_Menu_x_seg_usuario_Info
                             {
                                 seleccionado = true,
                                 IdEmpresa = meu.IdEmpresa,
                                 IdUsuario = meu.IdUsuario,
                                 IdMenu = meu.IdMenu,
                                 IdSede = meu.IdSede,
                                 Nuevo = meu.Nuevo,
                                 Modificar = meu.Modificar,
                                 Anular = meu.Anular,
                                 info_menu = new aca_Menu_Info
                                 {
                                     IdMenu = m.IdMenu,
                                     DescripcionMenu = m.DescripcionMenu,
                                     IdMenuPadre = m.IdMenuPadre,
                                     PosicionMenu = m.PosicionMenu,
                                     web_nom_Action = m.web_nom_Action,
                                     web_nom_Area = m.web_nom_Area,
                                     web_nom_Controller = m.web_nom_Controller
                                 }
                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool eliminarDB(int IdEmpresa, int IdSede, string IdUsuario)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Context.Database.ExecuteSqlCommand("DELETE aca_Menu_x_seg_usuario WHERE IdEmpresa = " + IdEmpresa + " AND IdSede = " + IdSede + " AND IdUsuario = '" + IdUsuario + "'");
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(List<aca_Menu_x_seg_usuario_Info> Lista, int IdEmpresa, int IdSede, string IdUsuario)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    foreach (var item in Lista)
                    {
                        aca_Menu_x_seg_usuario Entity = new aca_Menu_x_seg_usuario
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdSede = item.IdSede,
                            IdUsuario = item.IdUsuario,
                            IdMenu = item.IdMenu,
                            Nuevo = item.Nuevo,
                            Modificar = item.Modificar,
                            Anular = item.Anular,
                        };
                        Context.aca_Menu_x_seg_usuario.Add(Entity);
                    }

                    Context.SaveChanges();
                    string sql = "exec spaca_corregir_menu '" + IdEmpresa + "','" + IdSede.ToString() +"','" + IdUsuario + "'";
                    Context.Database.ExecuteSqlCommand(sql);

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
