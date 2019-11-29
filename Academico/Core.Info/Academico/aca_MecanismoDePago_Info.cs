﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_MecanismoDePago_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdMecanismo { get; set; }
        [StringLength(50, MinimumLength = 1, ErrorMessage = "el campo mecanismo debe tener mínimo 1 caracter y máximo 500")]
        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        public string NombreMecanismo { get; set; }
        [Required(ErrorMessage = "El campo termino de pago es obligatorio")]
        public string IdTerminoPago { get; set; }
        public Nullable<bool> Estado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        [Required(ErrorMessage = "El campo motivo de anulación es obligatorio")]
        public string MotivoAnulacion { get; set; }

        #region Campos que no existen en la tabla
        public string nom_TerminoPago { get; set; }
        #endregion
    }
}
