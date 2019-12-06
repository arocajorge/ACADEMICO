using Core.Data.Base;
using Core.Info.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes
{
    public class ACA_001_Data
    {
        public List<ACA_001_Info> get_list(int IdEmpresa, int IdMatricula)
        {
            try
            {
                List<ACA_001_Info> Lista;
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Lista = Context.VWACA_001.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula).Select(q => new ACA_001_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdMatricula = q.IdMatricula,
                        FechaMatricula = q.FechaMatricula,
                        CodMatricula = q.CodMatricula,
                        CodAlumno = q.CodAlumno,
                        IdAlumno = q.IdAlumno,
                        IdPersona = q.IdPersona,
                        pe_apellido = q.pe_apellido,
                        pe_nombre = q.pe_nombre,
                        Anio = q.Anio,
                        NomSede = q.NomSede,
                        NomNivel = q.NomNivel,
                        NomJornada = q.NomJornada,
                        NomCurso = q.NomCurso,
                        NomParalelo = q.NomParalelo,
                        Sexo = q.Sexo,
                        LugarNacimiento = q.LugarNacimiento,
                        pe_fechaNacimiento = q.pe_fechaNacimiento,
                        Direccion = q.Direccion,
                        Celular=q.Celular,
                        TieneElectricidad = q.TieneElectricidad,
                        TieneHermanos=q.TieneHermanos,
                        NombreHermanos=q.NombreHermanos,
                        TipoVivienda = q.TipoVivienda,
                        TenenciaVivienda = q.TenenciaVivienda,
                        CodCatalogoCONADIS = q.CodCatalogoCONADIS,
                        PorcentajeDiscapacidad=q.PorcentajeDiscapacidad,
                        NumeroCarnetConadis=q.NumeroCarnetConadis

                    }).ToList();
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
