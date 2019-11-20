using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_Menu_x_seg_usuario_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSede { get; set; }
        public int IdMenu { get; set; }
        public string IdUsuario { get; set; }
        public bool Nuevo { get; set; }
        public bool Modificar { get; set; }
        public bool Anular { get; set; }


        //Campos que no existen en la tabla
        public string DescripcionMenu { get; set; }
        public aca_Menu_Info info_menu { get; set; }
        public bool seleccionado { get; set; }
        public int modificado { get; set; }
        public int? IdMenuPadre { get; set; }
    }
}
