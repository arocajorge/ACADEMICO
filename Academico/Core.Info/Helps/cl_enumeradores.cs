using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Helps
{
    public class cl_enumeradores
    {
        public enum eTipoDocumento
        {
            COTIZ,
            FACT,
            GUIA,
            NTCR,
            NTDB,
            NTPEDI,
            NTVTA,
            ORDESP,
            PEDI,
            RETEN
        }
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

        public enum eTipoCatalogoBanco
        {
            CALTAMOR = 1,
            EST_CB_BA = 2,
            EST_CH = 3,
            EST_CONCI = 4,
            EST_PAG = 5
        }

        public enum eTipoCatalogoFact
        {
            FormaDePago = 15
        }

        public enum eCatalogoFact
        {
            EFEC,
            TARCRE,
            CRED,
            TARDEB
        }
        public enum eTipoParentezco
        {
            PAPA = 10,
            MAMA = 11
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
            ALUMNO,
            TUTOR,
            INSPECTOR
        }

        public enum eTipoCatalogoAcademico
        {
            ESTMAT = 1,
            ESTALU = 2,
            PAREN = 3,
        }

        public enum eCatalogoAcademicoMatricula
        {
            REGISTRADO = 1,
            MATRICULADO = 2
        }

        public enum eCatalogoAcademicoAlumno
        {
            CURSANDO = 3,
            REPROBADO_NOTAS = 4,
            REPROBADO_DEUDA = 5,
            REPROBADO_OTROS = 6,
            SUPLENCIA = 7,
            PROMOVIDO = 8,
            NO_PROMOVIDO = 9,
            RETIRADO = 21
        }

        public enum eTipoRepresentante
        {
            LEGAL,
            ECON
        }

        public enum eCatalogoPermisoMatricula
        {
            PERMITIR = 19,
            NEGAR = 20
        }

        public enum eTipoCatalogoSocioEconomico
        {
            VIVIENDA = 1,
            TIPOVIVIENDA = 2,
            AGUA = 3,
            MOTIVOING = 4,
            INSTITUCION = 5,
            ESTUDIOS = 6,
            INSTRUCCION = 7
        }

        public enum eTipoBusquedaProducto
        {
            SOLOPADRES,
            SOLOHIJOS,
            PORMODULO,
            TODOS,
            TODOS_MENOS_PADRES,
            PORSUCURSAL,
            SOLOSERVICIOS
        }

        public enum eTipoCatalogoInventario
        {
            EST_APROB = 1,
            FECH_CONTA = 4,
            TIPO_CONTA_CTA = 6,
            ING_EGR = 8
        }
    }
}
