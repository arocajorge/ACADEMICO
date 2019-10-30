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
    public class aca_Menu_Data
    {
        public List<aca_Menu_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                List<aca_Menu_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                        Lista = Context.aca_Menu.Where(q=> q.Estado == (mostrar_anulados == true ? q.Estado : true)).Select(q => new aca_Menu_Info
                        {
                            IdMenu = q.IdMenu,
                            IdMenuPadre = q.IdMenuPadre,
                            DescripcionMenu = q.DescripcionMenu,
                            nivel = q.nivel,
                            PosicionMenu = q.PosicionMenu,
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

        public List<aca_Menu_Info> get_list_combo(bool mostrar_anulados)
        {
            try
            {
                List<aca_Menu_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.aca_Menu
                                 join m in Context.aca_Menu
                                 on q.IdMenuPadre equals m.IdMenu into temp_padre
                                 from m in temp_padre.DefaultIfEmpty()
                                 join padre in Context.aca_Menu
                                 on m.IdMenuPadre equals padre.IdMenu into temp_padre_padre
                                 from padre in temp_padre_padre.DefaultIfEmpty()
                                 select new aca_Menu_Info
                                 {
                                     IdMenu = q.IdMenu,
                                     IdMenuPadre = q.IdMenuPadre,
                                     DescripcionMenu = q.DescripcionMenu,
                                     nivel = q.nivel,
                                     PosicionMenu = q.PosicionMenu,
                                     Estado = q.Estado,
                                     DescripcionMenu_combo = (padre == null ? "" : (padre.IdMenuPadre + " " + padre.DescripcionMenu + " - ")) + (m == null ? "" : (m.IdMenuPadre + " " + m.DescripcionMenu + " - ")) + q.IdMenuPadre + " " + q.DescripcionMenu

                                 }).ToList();
                    else
                        Lista = (from q in Context.aca_Menu
                                 join m in Context.aca_Menu
                                 on q.IdMenuPadre equals m.IdMenu into temp_padre
                                 from m in temp_padre.DefaultIfEmpty()
                                 join padre in Context.aca_Menu
                                 on m.IdMenuPadre equals padre.IdMenu into temp_padre_padre
                                 from padre in temp_padre_padre.DefaultIfEmpty()
                                 where q.Estado == true
                                 select new aca_Menu_Info
                                 {
                                     IdMenu = q.IdMenu,
                                     IdMenuPadre = q.IdMenuPadre,
                                     DescripcionMenu = q.DescripcionMenu,
                                     nivel = q.nivel,
                                     PosicionMenu = q.PosicionMenu,
                                     Estado = q.Estado,
                                     DescripcionMenu_combo = (padre == null ? "" : (padre.IdMenuPadre + " " + padre.DescripcionMenu + " - ")) + (m == null ? "" : (m.IdMenuPadre + " " + m.DescripcionMenu + " - ")) + q.IdMenuPadre + " " + q.DescripcionMenu
                                 }).ToList();
                }

                return Lista.OrderBy(q => q.DescripcionMenu_combo).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_Menu_Info get_info(int IdMenu)
        {
            try
            {
                aca_Menu_Info info = new aca_Menu_Info();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Menu Entity = Context.aca_Menu.FirstOrDefault(q => q.IdMenu == IdMenu);
                    if (Entity == null)
                        return null;
                    info = new aca_Menu_Info
                    {
                        IdMenu = Entity.IdMenu,
                        IdMenuPadre = Entity.IdMenuPadre,
                        DescripcionMenu = Entity.DescripcionMenu,
                        PosicionMenu = Entity.PosicionMenu,
                        web_nom_Area = Entity.web_nom_Area,
                        web_nom_Controller = Entity.web_nom_Controller,
                        web_nom_Action = Entity.web_nom_Action,
                        nivel = Entity.nivel,
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

        private int get_id()
        {
            try
            {
                int ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_Menu.Count();
                    if (cont > 0)
                        ID = Context.aca_Menu.Max(q => q.IdMenu) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_Menu_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Menu Entity = new aca_Menu
                    {
                        IdMenu = get_id(),
                        IdMenuPadre = info.IdMenuPadre,
                        DescripcionMenu = info.DescripcionMenu,
                        PosicionMenu = info.PosicionMenu,
                        Estado = info.Estado = true,
                        nivel = 1,
                        web_nom_Area = info.web_nom_Area,
                        web_nom_Controller = info.web_nom_Controller == null ? "" : info.web_nom_Controller,
                        web_nom_Action = info.web_nom_Action
                    };
                    Context.aca_Menu.Add(Entity);
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_Menu_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Menu Entity = Context.aca_Menu.FirstOrDefault(q => q.IdMenu == info.IdMenu);
                    if (Entity == null) return false;
                    Entity.IdMenuPadre = info.IdMenuPadre;
                    Entity.DescripcionMenu = info.DescripcionMenu;
                    Entity.PosicionMenu = info.PosicionMenu;
                    Entity.web_nom_Controller = info.web_nom_Controller == null ? "" : info.web_nom_Controller;
                    Entity.web_nom_Area = info.web_nom_Area;
                    Entity.web_nom_Action = info.web_nom_Action;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(aca_Menu_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Menu Entity = Context.aca_Menu.FirstOrDefault(q => q.IdMenu == info.IdMenu);
                    if (Entity == null) return false;
                    Entity.Estado = info.Estado = false;
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
