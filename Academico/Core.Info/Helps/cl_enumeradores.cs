﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Helps
{
    public class cl_enumeradores
    {
        public enum eTipoCatalogoGeneral
        {
            SEXO = 1,
            ESTCIVIL = 2,
            TIPODOC = 3,
            TIPONATPER = 5,
            TIP_CTA_AC = 27,
            TIPOSANGRE = 6,
            TIPODISCAP = 7
        }

        public enum eTipoSexoGeneral
        {
            SEXO_FEM,
            SEXO_MAS
        }

        public enum eTipoParentezco
        {
            PAPA = 10,
            MAMA = 11
        }

        public enum eTipoBusquedaProducto
        {
            PORMODULO,
            TODOS,
            PORSUCURSAL,
            SOLOSERVICIOS,
            PORBODEGA
        }

        public enum eModulo
        {
            INV,
            FAC,
            COM,
            ACF,
            RRHH,
            IMP,
            CONTA,
            CAJA,
            BANCO,
            CXC,
            CXP,
            ACA
        }

        public enum eTipoPersona
        {
            CLIENTE,
            EMPLEA,
            PERSONA,
            PROVEE,
            ALUMNO
        }
    }
}
