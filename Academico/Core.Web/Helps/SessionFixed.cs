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
    }
}