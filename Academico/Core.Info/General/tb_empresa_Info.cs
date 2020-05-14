using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.General
{
    public class tb_empresa_Info
    {
        public int IdEmpresa { get; set; }
        public string codigo { get; set; }
        public string em_nombre { get; set; }
        public string RazonSocial { get; set; }
        public string NombreComercial { get; set; }
        public string ContribuyenteEspecial { get; set; }
        public string em_ruc { get; set; }
        public string em_gerente { get; set; }
        public string em_contador { get; set; }
        public string em_rucContador { get; set; }
        public string em_telefonos { get; set; }
        public string em_direccion { get; set; }
        public byte[] em_logo { get; set; }
        public System.DateTime em_fechaInicioContable { get; set; }
        public string Estado { get; set; }
        public System.DateTime em_fechaInicioActividad { get; set; }
        public string cod_entidad_dinardap { get; set; }
        public string em_Email { get; set; }
        public Nullable<bool> TieneAcademico { get; set; }
    }
}
