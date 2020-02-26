using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_MatriculaCondicional_Det_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdMatriculaCondicional { get; set; }
        public int Secuencia { get; set; }
        public int IdParrafo { get; set; }

        #region Campos que no existen
        public string Nombre { get; set; }
        public string NomCatalogo { get; set; }
        #endregion
    }
}
