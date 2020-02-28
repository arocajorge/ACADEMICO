using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Web.Helps
{
    public interface ISessionValueProvider
    {
        string IdEmpresa { get; set; }
        string IdUsuario { get; set; }
        string NomEmpresa { get; set; }
        string IdSucursal { get; set; }
        string IdSede { get; set; }
        string IdAnio { get; set; }
        string IdNivel { get; set; }
        string IdTransaccionSession { get; set; }
        string IdTransaccionSessionActual { get; set; }
        string NombreImagenAlumno { get; set; }
        string NombreImagenProfesor { get; set; }
        string IdNivelDescuento { get; set; }
        string IdEntidad { get; set; }
        string EsSuperAdmin { get; set; }
        string IdProducto_padre_dist { get; set; }
        string Idtipo_cliente { get; set; }
        string EsContador { get; set; }
        string IdCaja { get; set; }
        string TipoPersona { get; set; }
        string IdAlumno { get; set; }
        string Ruc { get; set; }

        #region Combos bajo demanda de curso - paralelo - plantilla
        string IdAnioBajoDemanda { get; set; }
        string IdSedeBajoDemanda { get; set; }
        string IdNivelBajoDemanda { get; set; }
        string IdJornadaBajoDemanda { get; set; }
        string IdCursoBajoDemanda { get; set; }
        #endregion
    }
    public static class SessionFixed
    {
        private static ISessionValueProvider _sessionValueProvider;
        public static void SetSessionValueProvider(ISessionValueProvider provider)
        {
            _sessionValueProvider = provider;
        }
        public static string IdEmpresa
        {
            get { return _sessionValueProvider.IdEmpresa; }
            set { _sessionValueProvider.IdEmpresa = value; }
        }
        public static string NomEmpresa
        {
            get { return _sessionValueProvider.NomEmpresa; }
            set { _sessionValueProvider.NomEmpresa = value; }
        }
        public static string IdUsuario
        {
            get { return _sessionValueProvider.IdUsuario; }
            set { _sessionValueProvider.IdUsuario = value; }
        }
        public static string IdTransaccionSession
        {
            get { return _sessionValueProvider.IdTransaccionSession; }
            set { _sessionValueProvider.IdTransaccionSession = value; }
        }
        public static string IdTransaccionSessionActual
        {
            get { return _sessionValueProvider.IdTransaccionSessionActual; }
            set { _sessionValueProvider.IdTransaccionSessionActual = value; }
        }
        public static string IdNivelDescuento
        {
            get { return _sessionValueProvider.IdNivelDescuento; }
            set { _sessionValueProvider.IdNivelDescuento = value; }

        }
        public static string IdSucursal
        {
            get { return _sessionValueProvider.IdSucursal; }
            set { _sessionValueProvider.IdSucursal = value; }
        }
        public static string IdEntidad
        {
            get { return _sessionValueProvider.IdEntidad; }
            set { _sessionValueProvider.IdEntidad = value; }
        }

        public static string IdSede
        {
            get { return _sessionValueProvider.IdSede; }
            set { _sessionValueProvider.IdSede = value; }
        }

        public static string IdAnio
        {
            get { return _sessionValueProvider.IdAnio; }
            set { _sessionValueProvider.IdAnio = value; }
        }

        public static string IdNivel
        {
            get { return _sessionValueProvider.IdNivel; }
            set { _sessionValueProvider.IdNivel = value; }
        }
        
        public static string NombreImagenAlumno
        {
            get { return _sessionValueProvider.NombreImagenAlumno; }
            set { _sessionValueProvider.NombreImagenAlumno = value; }
        }

        public static string NombreImagenProfesor
        {
            get { return _sessionValueProvider.NombreImagenProfesor; }
            set { _sessionValueProvider.NombreImagenProfesor = value; }
        }
        public static string EsSuperAdmin
        {
            get { return _sessionValueProvider.EsSuperAdmin; }
            set { _sessionValueProvider.EsSuperAdmin = value; }
        }

        public static string IdProducto_padre_dist
        {
            get { return _sessionValueProvider.IdProducto_padre_dist; }
            set { _sessionValueProvider.IdProducto_padre_dist = value; }

        }

        public static string Idtipo_cliente
        {
            get { return _sessionValueProvider.Idtipo_cliente; }
            set { _sessionValueProvider.Idtipo_cliente = value; }
        }

        public static string EsContador
        {
            get { return _sessionValueProvider.EsContador; }
            set { _sessionValueProvider.EsContador = value; }
        }

        public static string IdCaja
        {
            get { return _sessionValueProvider.IdCaja; }
            set { _sessionValueProvider.IdCaja = value; }
        }

        public static string TipoPersona
        {
            get { return _sessionValueProvider.TipoPersona; }
            set { _sessionValueProvider.TipoPersona = value; }
        }

        public static string Ruc
        {
            get { return _sessionValueProvider.Ruc; }
            set { _sessionValueProvider.Ruc = value; }
        }

        public static string IdAlumno
        {
            get { return _sessionValueProvider.IdAlumno; }
            set { _sessionValueProvider.IdAlumno = value; }
        }

        public static string IdAnioBajoDemanda
        {
            get { return _sessionValueProvider.IdAnioBajoDemanda; }
            set { _sessionValueProvider.IdAnioBajoDemanda = value; }
        }
        public static string IdSedeBajoDemanda
        {
            get { return _sessionValueProvider.IdSedeBajoDemanda; }
            set { _sessionValueProvider.IdSedeBajoDemanda = value; }
        }
        public static string IdNivelBajoDemanda
        {
            get { return _sessionValueProvider.IdNivelBajoDemanda; }
            set { _sessionValueProvider.IdNivelBajoDemanda = value; }
        }
        public static string IdJornadaBajoDemanda
        {
            get { return _sessionValueProvider.IdJornadaBajoDemanda; }
            set { _sessionValueProvider.IdJornadaBajoDemanda = value; }
        }

        public static string IdCursoBajoDemanda
        {
            get { return _sessionValueProvider.IdCursoBajoDemanda; }
            set { _sessionValueProvider.IdCursoBajoDemanda = value; }
        }
    }
    public class WebSessionValueProvider : ISessionValueProvider
    {
        private const string _IdUsuario = "FxAca_IdUsuario";
        private const string _IdEmpresa = "FxAca_IdEmpresa";
        private const string _NomEmpresa = "FxAca_FIXED";
        private const string _IdSucursal = "FxAca_IdSucursal";
        private const string _IdNivel = "FxAca_IdNivel";
        private const string _IdSede = "FxAca_IdSede";
        private const string _IdAnio = "FxAca_IdAnio";
        private const string _IdTransaccionSession = "FxAca_IdTransaccionSesssion";
        private const string _IdTransaccionSessionActual = "FxAca_IdTransaccionSessionActual";
        private const string _NombreImagenAlumno = "FxAca_NombreImagenAlumno";
        private const string _NombreImagenProfesor = "FxAca_NombreImagenProfesor";
        private const string _IdEntidad = "Fx_IdEntidadParam";
        private const string _IdNivelDescuento = "Fx_IdNivelDescuento";
        private const string _EsSuperAdmin = "Fx_EsSuperAdmin";
        private const string _IdProducto_padre_dist = "Fx_IdProducto_padre_dist";
        private const string _Idtipo_cliente = "Fx_Idtipo_cliente";
        private const string _EsContador = "Fx_EsContador";
        private const string _IdCaja = "Fx_IdCaja";
        private const string _IdTipoPersona = "Fx_PERSONA";
        private const string _Ruc = "Fx_Ruc";
        private const string _IdAlumno = "Fx_IdAlumno";

        private const string _IdAnioBajoDemanda = "Fx_IdAnioBajoDemanda";
        private const string _IdSedeBajoDemanda = "Fx_IdSedeBajoDemanda";
        private const string _IdNivelBajoDemanda = "Fx_IdNivelBajoDemanda";
        private const string _IdJornadaBajoDemanda = "Fx_IdJornadaBajoDemanda";
        private const string _IdCursoBajoDemanda = "Fx_IdCursoBajoDemanda";

        public string IdEmpresa
        {
            get { return (string)HttpContext.Current.Session[_IdEmpresa]; }
            set { HttpContext.Current.Session[_IdEmpresa] = value; }
        }
        public string IdUsuario
        {
            get { return (string)HttpContext.Current.Session[_IdUsuario]; }
            set { HttpContext.Current.Session[_IdUsuario] = value; }
        }
        public string NomEmpresa
        {
            get { return (string)HttpContext.Current.Session[_NomEmpresa]; }
            set { HttpContext.Current.Session[_NomEmpresa] = value; }
        }
        public string IdSucursal
        {
            get { return (string)HttpContext.Current.Session[_IdSucursal]; }
            set { HttpContext.Current.Session[_IdSucursal] = value; }
        }
        public string IdTransaccionSession
        {
            get { return (string)HttpContext.Current.Session[_IdTransaccionSession]; }
            set { HttpContext.Current.Session[_IdTransaccionSession] = value; }
        }
        public string IdTransaccionSessionActual
        {
            get { return (string)HttpContext.Current.Session[_IdTransaccionSessionActual]; }
            set { HttpContext.Current.Session[_IdTransaccionSessionActual] = value; }
        }
        public string IdSede
        {
            get { return (string)HttpContext.Current.Session[_IdSede]; }
            set { HttpContext.Current.Session[_IdSede] = value; }
        }
        public string IdNivel
        {
            get { return (string)HttpContext.Current.Session[_IdNivel]; }
            set { HttpContext.Current.Session[_IdNivel] = value; }
        }

        public string IdAnio
        {
            get { return (string)HttpContext.Current.Session[_IdAnio]; }
            set { HttpContext.Current.Session[_IdAnio] = value; }
        }
        public string NombreImagenAlumno
        {
            get { return (string)HttpContext.Current.Session[_NombreImagenAlumno]; }
            set { HttpContext.Current.Session[_NombreImagenAlumno] = value; }
        }
        public string NombreImagenProfesor
        {
            get { return (string)HttpContext.Current.Session[_NombreImagenProfesor]; }
            set { HttpContext.Current.Session[_NombreImagenProfesor] = value; }
        }
        public string IdEntidad
        {
            get { return (string)HttpContext.Current.Session[_IdEntidad]; }
            set { HttpContext.Current.Session[_IdEntidad] = value; }
        }
        public string IdNivelDescuento
        {
            get { return (string)HttpContext.Current.Session[_IdNivelDescuento]; }
            set { HttpContext.Current.Session[_IdNivelDescuento] = value; }
        }
        public string EsSuperAdmin
        {
            get { return (string)HttpContext.Current.Session[_EsSuperAdmin]; }
            set { HttpContext.Current.Session[_EsSuperAdmin] = value; }
        }

        public string IdProducto_padre_dist
        {
            get { return (string)HttpContext.Current.Session[_IdProducto_padre_dist]; }
            set { HttpContext.Current.Session[_IdProducto_padre_dist] = value; }
        }

        public string Idtipo_cliente
        {
            get { return (string)HttpContext.Current.Session[_Idtipo_cliente]; }
            set { HttpContext.Current.Session[_Idtipo_cliente] = value; }
        }

        public string EsContador
        {
            get { return (string)HttpContext.Current.Session[_EsContador]; }
            set { HttpContext.Current.Session[_EsContador] = value; }
        }
        public string IdCaja
        {
            get { return (string)HttpContext.Current.Session[_IdCaja]; }
            set { HttpContext.Current.Session[_IdCaja] = value; }
        }

        public string TipoPersona
        {
            get { return (string)HttpContext.Current.Session[_IdTipoPersona]; }
            set { HttpContext.Current.Session[_IdTipoPersona] = value; }
        }

        public string Ruc
        {
            get { return (string)HttpContext.Current.Session[_Ruc]; }
            set { HttpContext.Current.Session[_Ruc] = value; }
        }

        public string IdAlumno
        {
            get { return (string)HttpContext.Current.Session[_IdAlumno]; }
            set { HttpContext.Current.Session[_IdAlumno] = value; }
        }
        public string IdAnioBajoDemanda
        {
            get { return (string)HttpContext.Current.Session[_IdAnioBajoDemanda]; }
            set { HttpContext.Current.Session[_IdAnioBajoDemanda] = value; }
        }
        public string IdSedeBajoDemanda
        {
            get { return (string)HttpContext.Current.Session[_IdSedeBajoDemanda]; }
            set { HttpContext.Current.Session[_IdSedeBajoDemanda] = value; }
        }
        public string IdNivelBajoDemanda
        {
            get { return (string)HttpContext.Current.Session[_IdNivelBajoDemanda]; }
            set { HttpContext.Current.Session[_IdNivelBajoDemanda] = value; }
        }

        public string IdJornadaBajoDemanda
        {
            get { return (string)HttpContext.Current.Session[_IdJornadaBajoDemanda]; }
            set { HttpContext.Current.Session[_IdJornadaBajoDemanda] = value; }
        }
        public string IdCursoBajoDemanda
        {
            get { return (string)HttpContext.Current.Session[_IdCursoBajoDemanda]; }
            set { HttpContext.Current.Session[_IdCursoBajoDemanda] = value; }
        }
    }
}