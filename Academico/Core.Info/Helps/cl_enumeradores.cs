using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Helps
{
    public class cl_enumeradores
    {
        public enum eEstadoContratoRRHH
        {
            ECT_ACT,
            ECT_LIQ,
            ECT_PLQ
        }
        public enum eEstadoEmpleadoRRHH
        {
            EST_ACT = 1,
            EST_DES = 2,
            EST_INC = 3,
            EST_LIQ = 4,
            EST_PER = 5,
            EST_PLQ = 6,
            EST_SUB = 7,
            EST_VAC = 8,
            EST_VB = 9,

        }
        public enum eTipoServicioCXP
        {
            BIEN,
            SERVI,
            AMBAS
        }
        public enum eTipoCbteBancario
        {
            CHEQ,
            DEPO,
            NCBA,
            NDBA
        }
        public enum eEstadoCierreCaja
        {
            EST_CIE_ABI,
            EST_CIE_CER
        }
        public enum eEstadoAprobacionOrdenPago
        {
            APRO,
            PENDI,
            REPRO
        }
        public enum eFormaPagoOrdenPago
        {
            CHEQUE,
            EFEC,
            NTDEB_BAN,
            TARJE_CRE
        }
        public enum eTipoNotaCXP
        {
            T_TIP_NOTA_INT,
            T_TIP_NOTA_SRI
        }
        public enum eTipoOrdenPago
        {
            ANTI_EMPLE,
            ANTI_PROVEE,
            FACT_PROVEE,
            LIQ_HAB,
            OTROS_CONC,
            PRESTAMOS,
            VACACIONES
        }
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
            MAMA = 11,
            REPRESENTANTE_ECONOMICO=12,
            HERMANO_A = 13,
            ABUELO_A = 14,
            TIO_TIA = 15,
            PADRINO_MADRINA = 16,
            PADRASTRO = 17,
            MADRASTRA = 18,
            OTROS = 22,
            PRIMO_A = 23,
            HERMANASTRO_A = 24,
            CUÑADO_A = 25
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
            INSPECTOR,
            PROFESOR,
            ALUMNO_MATRICULA,
            ALUMNO_MATRICULADOS
        }

        public enum eTipoCatalogoAcademico
        {
            ESTMAT = 1,
            ESTALU = 2,
            PAREN = 3,
            PERNEG = 4,
            CONDIC = 5,
            QUIM1 = 6,
            QUIM2 = 7,
            EXSUP = 8,
            TIPOCAL = 9,
            ESTCONV = 10
        }

        public enum eCatalogoTipoCalificacion
        {
            CUANTI = 40,
            CUALI = 41
        }
        public enum eTipoCatalogoAcademicoExamen
        {
            EXQUI1 = 34,
            EXQUI2 = 35,
            EXMEJ = 36,
            EXSUP = 37,
            EXREM = 38,
            EXGRA = 39
        }

        public enum eTipoCatalogoAcademicoParcial
        {
            P1 = 28,
            P2 = 29,
            P3 = 30,
            P4 = 31,
            P5 = 32,
            P6 = 33
        }

        public enum eTipoCatalogoAcademicoConductaFinal
        {
            QUIMESTRE_1,
            QUIMESTRE_2,
            PROMEDIOFINAL
        }
        public enum eCatalogoAcademicoMatricula
        {
            REGISTRADO = 1,
            MATRICULADO = 2,
            APROBADO = 42,
            REPROBADO = 43
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

        public enum eCatalogoAcademicoConvenio
        {
            PENDIENTE = 44,
            CANCELADO = 45,
            ANULADO = 45
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
            INSTRUCCION = 7,
            VIVECON = 8
        }

        public enum eCatalogoSocioEconomico_OtrodMotivos
        {
            OTROS_MOTIVOING = 15,
            OTROS_INSTITUCION = 19,
            OTROS_MEDIOS = 22
        }

        public enum eCatalogoMatriculaCondicional
        {
            APROVECHAMIENTO = 26,
            CONDUCTA = 27
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

        public enum eTipoCobroTomaCuentaDe
        {
            TIP_COBRO,
            CAJA
        }

        public enum eTipoIngEgr
        {
            ING,
            EGR
        }
        public enum eTipoProcesoBancario
        {
            NCR,
            ROL_ELECTRONICO,
            NCR_TRASN,
            RECBG,
            RECPB
        }
        public enum eTipoProcesoBancarioCobrosAcademico
        {
            RECBG = 4,
            RECPB = 5,
            RECBB = 6
        }
    }
}
