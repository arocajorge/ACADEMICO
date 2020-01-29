﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.General
{
    public class tb_GrupoEtnico_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdGrupoEtnico { get; set; }
        [StringLength(500, MinimumLength = 1, ErrorMessage = "el campo grupo etnico debe tener mínimo 1 caracter y máximo 500")]
        [Required(ErrorMessage = "El campo religión es obligatorio")]
        public string NomGrupoEtnico { get; set; }
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