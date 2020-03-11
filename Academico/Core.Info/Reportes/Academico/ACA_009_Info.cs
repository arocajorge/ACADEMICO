﻿using System;

namespace Core.Info.Reportes.Academico
{
    public class ACA_009_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public int Secuencia { get; set; }
        public decimal IdAlumno { get; set; }
        public string Codigo { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string pe_cedulaRuc { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string Observacion { get; set; }
        public int IdAnio { get; set; }
        public string Descripcion { get; set; }
        public string IdUsuarioCreacion { get; set; }
    }
}