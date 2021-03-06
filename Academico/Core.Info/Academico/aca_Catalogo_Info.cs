﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_Catalogo_Info
    {
        public int IdCatalogo { get; set; }
        public int IdCatalogoTipo { get; set; }
        [StringLength(50, MinimumLength = 0, ErrorMessage = "el campo nombre debe tener máximo 50 caracteres")]
        public string Codigo { get; set; }
        [StringLength(500, MinimumLength = 1, ErrorMessage = "el campo nombre debe tener mínimo 1 caracter y máximo 500")]
        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        public string NomCatalogo { get; set; }
        public int Orden { get; set; }
        public bool Estado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        [Required(ErrorMessage = "El campo motivo de anulación es obligatorio")]
        public string MotivoAnulacion { get; set; }
    }
}
