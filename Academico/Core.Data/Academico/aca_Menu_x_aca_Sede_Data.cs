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
    public class aca_Menu_x_aca_Sede_Data
    {
        public List<aca_Menu_x_aca_Sede_Info> get_list(int IdEmpresa, int IdSede)
        {
            try
            {
                List<aca_Menu_x_aca_Sede_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = (from q in Context.aca_Menu_x_aca_Sede
                             join m in Context.aca_Menu
                             on q.IdMenu equals m.IdMenu
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSede == IdSede
                             && m.Estado == true
                             select new aca_Menu_x_aca_Sede_Info
                             {
                                 seleccionado = true,
                                 IdEmpresa = q.IdEmpresa,
                                 IdMenu = q.IdMenu,
                                 info_menu = new aca_Menu_Info
                                 {
                                     IdMenu = m.IdMenu,
                                     DescripcionMenu = m.DescripcionMenu,
                                     IdMenuPadre = m.IdMenuPadre,
                                     PosicionMenu = m.PosicionMenu
                                 }
                             }).ToList();

                    Lista.AddRange((from q in Context.aca_Menu
                                    where !Context.aca_Menu_x_aca_Sede.Any(me => me.IdMenu == q.IdMenu && me.IdEmpresa == IdEmpresa && me.IdSede == IdSede)
                                    && q.Estado == true
                                    select new aca_Menu_x_aca_Sede_Info
                                    {
                                        seleccionado = false,
                                        IdEmpresa = IdEmpresa,
                                        IdMenu = q.IdMenu,

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

        public bool eliminarDB(int IdEmpresa, int IdSede)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Context.Database.ExecuteSqlCommand("DELETE aca_Menu_x_aca_Sede WHERE IdEmpresa = " + IdEmpresa+" and IdSede = "+ IdSede);
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(List<aca_Menu_x_aca_Sede_Info> Lista)
        {
            try
            {
                int IdEmpresa = 0;
                int IdSede = 0;
                if (Lista.Count() > 0)
                {
                    IdEmpresa = Lista.FirstOrDefault().IdEmpresa;
                    IdSede = Lista.FirstOrDefault().IdSede;
                }

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    foreach (var item in Lista)
                    {
                        aca_Menu_x_aca_Sede Entity = new aca_Menu_x_aca_Sede
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdSede = item.IdSede,
                            IdMenu = item.IdMenu
                        };
                        Context.aca_Menu_x_aca_Sede.Add(Entity);
                    }
                    Context.SaveChanges();

                    //string sql = "exec spaca_corregir_menu '" + IdEmpresa + "','" + IdSede.ToString() + "','" + "" + "'";
                    //Context.Database.ExecuteSqlCommand(sql);
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
