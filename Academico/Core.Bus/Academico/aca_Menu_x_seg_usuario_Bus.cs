using Core.Data.Academico;
using Core.Data.SeguridadAcceso;
using Core.Info.Academico;
using Core.Info.SeguridadAcceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_Menu_x_seg_usuario_Bus
    {
        aca_Menu_x_seg_usuario_Data odata = new aca_Menu_x_seg_usuario_Data();
        public List<aca_Menu_x_seg_usuario_Info> get_list(int IdEmpresa, int IdSede, string IdUsuario, bool MostrarTodo)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSede, IdUsuario, MostrarTodo);
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
                return odata.get_list(IdEmpresa, IdSede, IdUsuario, IdMenuPadre);
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
                return odata.eliminarDB(IdEmpresa, IdSede, IdUsuario);
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
                return odata.guardarDB(Lista, IdEmpresa, IdSede, IdUsuario);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_Menu_x_seg_usuario_Info getInfo(int IdEmpresa, int IdSede, string IdUsuario, int IdMenu)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdSede, IdUsuario, IdMenu);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(aca_Menu_x_seg_usuario_Info info)
        {
            try
            {
                return odata.guardarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(aca_Menu_x_seg_usuario_Info info)
        {
            try
            {
                return odata.modificarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_Menu_x_seg_usuario_Info get_list_menu_accion(int IdEmpresa, int IdSede, string IdUsuario, string Area, string NomControlador, string Accion)
        {
            try
            {
                return odata.get_list_menu_accion(IdEmpresa, IdSede, IdUsuario, Area, NomControlador, Accion);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
