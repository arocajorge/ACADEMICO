using Core.Data.Base;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Academico
{
    public class ACA_001_Data
    {
        public List<ACA_001_Info> get_list(int IdEmpresa, decimal IdAlumno, int IdAnio)
        {
            try
            {
                List<ACA_001_Info> Lista = new List<ACA_001_Info>(); ;
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    //Lista = Context.VWACA_001.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno && q.IdAnio == IdAnio).Select(q => new ACA_001_Info
                    //{
                    //    IdEmpresa = q.IdEmpresa,
                    //    IdMatricula = q.IdMatricula,
                    //    FechaMatricula = q.FechaMatricula,
                    //    CodMatricula = q.CodMatricula,
                    //    CodAlumno = q.CodAlumno,
                    //    IdAlumno = q.IdAlumno,
                    //    ApellidoAlumno = q.ApellidoAlumno,
                    //    NombreAlumno = q.NombreAlumno,
                    //    Anio = q.Anio,
                    //    NomSede = q.NomSede,
                    //    NomNivel = q.NomNivel,
                    //    NomJornada = q.NomJornada,
                    //    NomCurso = q.NomCurso,
                    //    NomParalelo = q.NomParalelo,
                    //    Sexo = q.Sexo,
                    //    LugarNacimiento = q.LugarNacimiento,
                    //    pe_fechaNacimiento = q.pe_fechaNacimiento,
                    //    DireccionAlumno = q.DireccionAlumno,
                    //    TelefonoAlumno = q.TelefonoAlumno,
                    //    NacionalidadAlumno = q.NacionalidadAlumno,
                    //    TipoDiscapacidadAlumno = q.TipoDiscapacidadAlumno,
                    //    CelularAlumno=q.CelularAlumno,
                    //    TieneElectricidad = q.TieneElectricidad,
                    //    Agua = q.Agua,
                    //    TieneHermanos=q.TieneHermanos,
                    //    NombreHermanos=q.NombreHermanos,
                    //    TipoVivienda = q.TipoVivienda,
                    //    TenenciaVivienda = q.TenenciaVivienda,
                    //    CodCatalogoCONADIS = q.CodCatalogoCONADIS,
                    //    PorcentajeDiscapacidad=q.PorcentajeDiscapacidad,
                    //    NumeroCarnetConadis=q.NumeroCarnetConadis

                    //}).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
