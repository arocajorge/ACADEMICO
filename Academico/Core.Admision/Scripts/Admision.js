
//$(".form").keypress(function (e) {
//    if (e.which == 13) {
//        return false;
//    }
//});

function SiguienteAspirante() {
    if ($("#AspiranteValido").val() == "1") {
        $("#DatosAspirante").hide();
        $("#BtnAspirante").attr("class", "w-10 h-10 rounded-full button text-gray-600 bg-gray-200 dark:bg-dark-1");
        $("#DatosPadre").show();
        $("#BtnPadre").attr("class", "w-10 h-10 rounded-full button text-white bg-theme-1");
    }
}
function SiguientePadre() {
    if ($("#PadreValido").val() == "1") {
        $("#DatosPadre").hide();
        $("#BtnPadre").attr("class", "w-10 h-10 rounded-full button text-gray-600 bg-gray-200 dark:bg-dark-1");
        $("#DatosMadre").show();
        $("#BtnMadre").attr("class", "w-10 h-10 rounded-full button text-white bg-theme-1");
    }
}
function AnteriorPadre() {
    $("#DatosPadre").hide();
    $("#BtnPadre").attr("class", "w-10 h-10 rounded-full button text-gray-600 bg-gray-200 dark:bg-dark-1");
    $("#DatosAspirante").show();
    $("#BtnAspirante").attr("class", "w-10 h-10 rounded-full button text-white bg-theme-1");
}
function SiguienteMadre() {
    if ($("#MadreValido").val() == "1")
    {
        $("#DatosMadre").hide();
        $("#BtnMadre").attr("class", "w-10 h-10 rounded-full button text-gray-600 bg-gray-200 dark:bg-dark-1");
        $("#DatosRepresentante").show();
        $("#BtnRepresentante").attr("class", "w-10 h-10 rounded-full button text-white bg-theme-1");
    }
}
function AnteriorMadre() {
    $("#DatosMadre").hide();
    $("#BtnMadre").attr("class", "w-10 h-10 rounded-full button text-gray-600 bg-gray-200 dark:bg-dark-1");
    $("#DatosPadre").show();
    $("#BtnPadre").attr("class", "w-10 h-10 rounded-full button text-white bg-theme-1");
}
function SiguienteRepresentante() {
    if ($("#RepresentanteValido").val() == "1")
    {
        $("#DatosRepresentante").hide();
        $("#BtnRepresentante").attr("class", "w-10 h-10 rounded-full button text-gray-600 bg-gray-200 dark:bg-dark-1");
        $("#DatosSocioEconomico").show();
        $("#BtnSocioEconomico").attr("class", "w-10 h-10 rounded-full button text-white bg-theme-1");
    }
}
function AnteriorRepresentante() {
    $("#DatosRepresentante").hide();
    $("#BtnRepresentante").attr("class", "w-10 h-10 rounded-full button text-gray-600 bg-gray-200 dark:bg-dark-1");
    $("#DatosMadre").show();
    $("#BtnMadre").attr("class", "w-10 h-10 rounded-full button text-white bg-theme-1");
}
function AnteriorSocioEconomico() {
    $("#DatosSocioEconomico").hide();
    $("#BtnSocioEconomico").attr("class", "w-10 h-10 rounded-full button text-gray-600 bg-gray-200 dark:bg-dark-1");
    $("#DatosRepresentante").show();
    $("#BtnRepresentante").attr("class", "w-10 h-10 rounded-full button text-white bg-theme-1");
}


function Validar_cedula_ruc_Aspirante() {
    var datos = {
        naturaleza: $("#Naturaleza_Aspirante").val(),
        tipo_documento: $("#IdTipoDocumento_Aspirante").val(),
        cedula_ruc: $("#CedulaRuc_Aspirante").val(),
    }

    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: '/Admision/Validar_cedula_ruc',
        async: false,
        bDeferRender: true,
        bProcessing: true,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data.isValid == true) {
                $("#error_documento_aspirante").hide();
                $("#Naturaleza_Aspirante").val(data.return_naturaleza);
                $("#AspiranteValido").val("1");
            }
            else {
                $("#error_documento_aspirante").show();
                $("#AspiranteValido").val("0");
            }
        },
        error: function (error) {
        }
    });
};

function Validar_cedula_ruc_Padre() {
    var datos = {
        naturaleza: $("#Naturaleza_Padre").val(),
        tipo_documento: $("#IdTipoDocumento_Padre").val(),
        cedula_ruc: $("#CedulaRuc_Padre").val(),
    }

    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: '/Admision/Validar_cedula_ruc',
        async: false,
        bDeferRender: true,
        bProcessing: true,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data.isValid == true) {
                $("#error_documento_padre").hide();
                $("#Naturaleza_Padre").val(data.return_naturaleza);
                $("#PadreValido").val("1");
            }
            else {
                $("#error_documento_padre").show();
                $("#PadreValido").val("0");
            }
        },
        error: function (error) {
        }
    });
};

function Validar_cedula_ruc_Madre() {
    var datos = {
        naturaleza: $("#Naturaleza_Madre").val(),
        tipo_documento: $("#IdTipoDocumento_Madre").val(),
        cedula_ruc: $("#CedulaRuc_Madre").val(),
    }

    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: '/Admision/Validar_cedula_ruc',
        async: false,
        bDeferRender: true,
        bProcessing: true,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data.isValid == true) {
                $("#error_documento_madre").hide();
                $("#Naturaleza_Madre").val(data.return_naturaleza);
                $("#MadreValido").val("1");
            }
            else {
                $("#error_documento_madre").show();
                $("#PadreValido").val("0");
            }
        },
        error: function (error) {
        }
    });
};

function Validar_cedula_ruc_Representante() {
    var datos = {
        naturaleza: $("#Naturaleza_Representante").val(),
        tipo_documento: $("#IdTipoDocumento_Representante").val(),
        cedula_ruc: $("#CedulaRuc_Representante").val(),
    }

    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: '/Admision/Validar_cedula_ruc',
        async: false,
        bDeferRender: true,
        bProcessing: true,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data.isValid == true) {
                $("#error_documento_representante").hide();
                $("#Naturaleza_Representante").val(data.return_naturaleza);
                $("#RepresentanteValido").val("1");
            }
            else {
                $("#error_documento_representante").show();
                $("#RepresentanteValido").val("0");
            }
        },
        error: function (error) {
        }
    });
};

function CargarJornada() {
    var datos = {
        IdEmpresa: $("#IdEmpresa").val(),
        IdAnio: $("#IdAnio").val(),
        IdSede: $("#IdSede").val(),
    }

    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: '/Admision/CargarJornada',
        async: false,
        bDeferRender: true,
        bProcessing: true,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data != "") {
                $("#IdJornada").empty();
                $.each(data, function (i, row) {
                    $("#IdJornada").append("<option value=" + row.IdJornada + ">" + row.NomJornada + "</option>");
                });
            }
            CargarNivel();
        },
        error: function (error) {
        }
    });
};

function CargarNivel() {
    var datos = {
        IdEmpresa: $("#IdEmpresa").val(),
        IdAnio: $("#IdAnio").val(),
        IdSede: $("#IdSede").val(),
        IdJornada: $("#IdJornada").val(),
    }

    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: '/Admision/CargarNivel',
        async: false,
        bDeferRender: true,
        bProcessing: true,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data != "") {
                $("#IdNivel").empty();
                $.each(data, function (i, row) {
                    $("#IdNivel").append("<option value=" + row.IdNivel + ">" + row.NomNivel + "</option>");
                });
            }
            CargarCurso();
        },
        error: function (error) {
        }
    });
};

function CargarCurso() {
    var datos = {
        IdEmpresa: $("#IdEmpresa").val(),
        IdAnio: $("#IdAnio").val(),
        IdSede: $("#IdSede").val(),
        IdJornada: $("#IdJornada").val(),
        IdNivel: $("#IdNivel").val(),
    }

    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: '/Admision/CargarCurso',
        async: false,
        bDeferRender: true,
        bProcessing: true,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data != "") {
                $("#IdCurso").empty();
                $.each(data, function (i, row) {
                    $("#IdCurso").append("<option value=" + row.IdCurso + ">" + row.NomCurso + "</option>");
                });
            }
        },
        error: function (error) {
        }
    });
};

function CargarRegion_Aspirante() {
    $("#Cod_Region_Aspirante").empty();
    var datos = {
        IdPais: $("#IdPais_Aspirante").val(),
    }
    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: '/Admision/CargarRegion',
        async: false,
        bDeferRender: true,
        bProcessing: true,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data != "") {
                $.each(data, function (i, row) {
                    $("#Cod_Region_Aspirante").append("<option value=" + row.Cod_Region + ">" + row.Nom_region + "</option>");
                });
            }
        },
        error: function (error) {
        }
    });
};

function CargarProvincia_Aspirante() {
    $("#IdProvincia_Aspirante").empty();
    var datos = {
        IdPais: $("#IdPais_Aspirante").val(),
    }
    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: '/Admision/CargarProvincia',
        async: false,
        bDeferRender: true,
        bProcessing: true,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data != "") {
                $.each(data, function (i, row) {
                    $("#IdProvincia_Aspirante").append("<option value=" + row.IdProvincia + ">" + row.Descripcion_Prov + "</option>");
                });
            }
            CargaCiudad_Aspirante();
        },
        error: function (error) {
        }
    });
};

function CargaCiudad_Aspirante() {
    $("#IdCiudad_Aspirante").empty();
    var datos = {
        IdProvincia: $("#IdProvincia_Aspirante").val(),
    }
    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: '/Admision/CargarCiudad',
        async: false,
        bDeferRender: true,
        bProcessing: true,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data != "") {
                $.each(data, function (i, row) {
                    $("#IdCiudad_Aspirante").append("<option value=" + row.IdCiudad + ">" + row.Descripcion_Ciudad + "</option>");
                });
            }
            CargarParroquia_Aspirante()
        },
        error: function (error) {
        }
    });
};

function CargarParroquia_Aspirante() {
    $("#IdParroquia_Aspirante").empty();
    var datos = {
        IdCiudad: $("#IdCiudad_Aspirante").val(),
    }
    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: '/Admision/CargarParroquia',
        async: false,
        bDeferRender: true,
        bProcessing: true,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data != "") {
                $.each(data, function (i, row) {
                    $("#IdParroquia_Aspirante").append("<option value=" + row.IdParroquia + ">" + row.nom_parroquia + "</option>");
                });
            }
        },
        error: function (error) {
        }
    });
};

function CargarRegion_Padre() {
    $("#Cod_Region_Padre").empty();
    var datos = {
        IdPais: $("#IdPais_Padre").val(),
    }
    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: '/Admision/CargarRegion',
        async: false,
        bDeferRender: true,
        bProcessing: true,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data != "") {
                $.each(data, function (i, row) {
                    $("#Cod_Region_Padre").append("<option value=" + row.Cod_Region + ">" + row.Nom_region + "</option>");
                });
            }
        },
        error: function (error) {
        }
    });
};

function CargarProvincia_Padre() {
    $("#IdProvincia_Padre").empty();
    var datos = {
        IdPais: $("#IdPais_Padre").val()
    }
    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: '/Admision/CargarProvincia',
        async: false,
        bDeferRender: true,
        bProcessing: true,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data != "") {
                $.each(data, function (i, row) {
                    $("#IdProvincia_Padre").append("<option value=" + row.IdProvincia + ">" + row.Descripcion_Prov + "</option>");
                });
            }
            CargaCiudad_Padre();
        },
        error: function (error) {
        }
    });
};

function CargaCiudad_Padre() {
    $("#IdCiudad_Padre").empty();
    var datos = {
        IdProvincia: $("#IdProvincia_Padre").val(),
    }
    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: '/Admision/CargarCiudad',
        async: false,
        bDeferRender: true,
        bProcessing: true,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data != "") {
                $.each(data, function (i, row) {
                    $("#IdCiudad_Padre").append("<option value=" + row.IdCiudad + ">" + row.Descripcion_Ciudad + "</option>");
                });
            }
            CargarParroquia_Padre()
        },
        error: function (error) {
        }
    });
};

function CargarParroquia_Padre() {
    $("#IdParroquia_Padre").empty();
    var datos = {
        IdCiudad: $("#IdCiudad_Padre").val(),
    }
    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: '/Admision/CargarParroquia',
        async: false,
        bDeferRender: true,
        bProcessing: true,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data != "") {
                $.each(data, function (i, row) {
                    $("#IdParroquia_Padre").append("<option value=" + row.IdParroquia + ">" + row.nom_parroquia + "</option>");
                });
            }
        },
        error: function (error) {
        }
    });
};

function CargarRegion_Madre() {
    $("#Cod_Region_Madre").empty();
    var datos = {
        IdPais: $("#IdPais_Madre").val(),
    }
    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: '/Admision/CargarRegion',
        async: false,
        bDeferRender: true,
        bProcessing: true,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data != "") {
                $.each(data, function (i, row) {
                    $("#Cod_Region_Madre").append("<option value=" + row.Cod_Region + ">" + row.Nom_region + "</option>");
                });
            }
        },
        error: function (error) {
        }
    });
};

function CargarProvincia_Madre() {
    $("#IdProvincia_Madre").empty();
    var datos = {
        IdPais: $("#IdPais_Madre").val()
    }
    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: '/Admision/CargarProvincia',
        async: false,
        bDeferRender: true,
        bProcessing: true,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data != "") {
                $.each(data, function (i, row) {
                    $("#IdProvincia_Madre").append("<option value=" + row.IdProvincia + ">" + row.Descripcion_Prov + "</option>");
                });
            }
            CargaCiudad_Madre();
        },
        error: function (error) {
        }
    });
};

function CargaCiudad_Madre() {
    $("#IdCiudad_Madre").empty();
    var datos = {
        IdProvincia: $("#IdProvincia_Madre").val(),
    }
    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: '/Admision/CargarCiudad',
        async: false,
        bDeferRender: true,
        bProcessing: true,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data != "") {
                $.each(data, function (i, row) {
                    $("#IdCiudad_Madre").append("<option value=" + row.IdCiudad + ">" + row.Descripcion_Ciudad + "</option>");
                });
            }
            CargarParroquia_Madre()
        },
        error: function (error) {
        }
    });
};

function CargarParroquia_Madre() {
    $("#IdParroquia_Madre").empty();
    var datos = {
        IdCiudad: $("#IdCiudad_Madre").val(),
    }
    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: '/Admision/CargarParroquia',
        async: false,
        bDeferRender: true,
        bProcessing: true,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data != "") {
                $.each(data, function (i, row) {
                    $("#IdParroquia_Madre").append("<option value=" + row.IdParroquia + ">" + row.nom_parroquia + "</option>");
                });
            }
        },
        error: function (error) {
        }
    });
};

function CargarRegion_Representante() {
    $("#Cod_Region_Representante").empty();
    var datos = {
        IdPais: $("#IdPais_Representante").val(),
    }
    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: '/Admision/CargarRegion',
        async: false,
        bDeferRender: true,
        bProcessing: true,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data != "") {
                $.each(data, function (i, row) {
                    $("#Cod_Region_Representante").append("<option value=" + row.Cod_Region + ">" + row.Nom_region + "</option>");
                });
            }
        },
        error: function (error) {
        }
    });
};

function CargarProvincia_Representante() {
    $("#IdProvincia_Representante").empty();
    var datos = {
        IdPais: $("#IdPais_Representante").val()
    }
    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: '/Admision/CargarProvincia',
        async: false,
        bDeferRender: true,
        bProcessing: true,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data != "") {
                $.each(data, function (i, row) {
                    $("#IdProvincia_Representante").append("<option value=" + row.IdProvincia + ">" + row.Descripcion_Prov + "</option>");
                });
            }
            CargaCiudad_Representante();
        },
        error: function (error) {
        }
    });
};

function CargaCiudad_Representante() {
    $("#IdCiudad_Representante").empty();
    var datos = {
        IdProvincia: $("#IdProvincia_Representante").val(),
    }
    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: '/Admision/CargarCiudad',
        async: false,
        bDeferRender: true,
        bProcessing: true,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data != "") {
                $.each(data, function (i, row) {
                    $("#IdCiudad_Representante").append("<option value=" + row.IdCiudad + ">" + row.Descripcion_Ciudad + "</option>");
                });
            }
            CargarParroquia_Representante()
        },
        error: function (error) {
        }
    });
};

function CargarParroquia_Representante() {
    $("#IdParroquia_Representante").empty();
    var datos = {
        IdCiudad: $("#IdCiudad_Representante").val(),
    }
    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: '/Admision/CargarParroquia',
        async: false,
        bDeferRender: true,
        bProcessing: true,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data != "") {
                $.each(data, function (i, row) {
                    $("#IdParroquia_Representante").append("<option value=" + row.IdParroquia + ">" + row.nom_parroquia + "</option>");
                });
            }
        },
        error: function (error) {
        }
    });
};

function PadreRepresentante() {
    var Naturaleza = $("#Naturaleza_Padre").val();
    var IdTipoDocumento = $("#IdTipoDocumento_Padre").val();
    var CedulaRuc = $("#CedulaRuc_Padre").val();
    var EstaFallecido = $("#EstaFallecido_Padre").prop('checked');
    var Nombres = $("#Nombres_Padre").val();
    var Apellidos = $("#Apellidos_Padre").val();
    var NombreCompleto = $("#NombreCompleto_Padre").val();
    var RazonSocial = $("#RazonSocial_Padre").val();
    var FechaNacimiento = $("#FechaNacimiento_Padre").val();
    var IdPais = $("#IdPais_Padre").val();
    var Cod_Region = $("#Cod_Region_Padre").val();
    var IdProvincia = $("#IdProvincia_Padre").val();
    var IdCiudad = $("#IdCiudad_Padre").val();
    var IdParroquia = $("#IdParroquia_Padre").val();
    var Sexo = $("#Sexo_Padre").val();
    var IdEstadoCivil = $("#IdEstadoCivil_Padre").val();
    var IdGrupoEtnico = $("#IdGrupoEtnico_Padre").val();
    var Telefono = $("#Telefono_Padre").val();
    var Celular = $("#Celular_Padre").val();
    var Correo = $("#Correo_Padre").val();
    var IdReligion = $("#IdReligion_Padre").val();
    var AsisteCentroCristiano = $("#AsisteCentroCristiano_Padre").prop('checked');
    var CasaPropia = $("#CasaPropia_Padre").prop('checked');
    var VehiculoPropio = $("#VehiculoPropio_Padre").prop('checked');
    var Marca = $("#Marca_Padre").val();
    var Modelo = $("#Modelo_Padre").val();
    var Direccion = $("#Direccion_Padre").val();
    var Sector = $("#Sector_Padre").val();
    var EmpresaTrabajo = $("#EmpresaTrabajo_Padre").val();
    var IdCatalogoFichaInst = $("#IdCatalogoFichaInst_Padre").val();
    var IdProfesion = $("#IdProfesion_Padre").val();
    var CargoTrabajo = $("#CargoTrabajo_Padre").val();
    var TelefonoTrabajo = $("#TelefonoTrabajo_Padre").val();
    var AniosServicio = $("#AniosServicio_Padre").val();
    var IngresoMensual = $("#IngresoMensual_Padre").val();
    var DireccionTrabajo = $("#DireccionTrabajo_Padre").val();
    var CodCatalogoCONADIS = $("#CodCatalogoCONADIS_Padre").val();
    var PorcentajeDiscapacidad = $("#PorcentajeDiscapacidad_Padre").val();
    var NumeroCarnetConadis = $("#NumeroCarnetConadis_Padre").val();
    var Parentezco = $("#IdCatalogoPAREN_Padre").val();

    $("#Naturaleza_Representante").val(Naturaleza);
    $("#IdTipoDocumento_Representante").val(IdTipoDocumento);
    $("#CedulaRuc_Representante").val(CedulaRuc);
    $("#EstaFallecido_Representante").prop('checked', EstaFallecido);
    $("#Nombres_Representante").val(Nombres);
    $("#Apellidos_Representante").val(Apellidos);
    $("#NombreCompleto_Representante").val(NombreCompleto);
    $("#RazonSocial_Representante").val(RazonSocial);
    $("#FechaNacimiento_Representante").val(FechaNacimiento);
    $("#IdPais_Representante").val(IdPais);
    CargarRegion_Representante();
    $("#Cod_Region_Representante").val(Cod_Region);
    CargarProvincia_Representante();
    $("#IdProvincia_Representante").val(IdProvincia);
    CargaCiudad_Representante();
    $("#IdCiudad_Representante").val(IdCiudad);
    CargarParroquia_Representante();
    $("#IdParroquia_Representante").val(IdParroquia);
    $("#Sexo_Representante").val(Sexo);
    $("#IdEstadoCivil_Representante").val(IdEstadoCivil);
    $("#IdGrupoEtnico_Representante").val(IdGrupoEtnico);
    $("#Telefono_Representante").val(Telefono);
    $("#Celular_Representante").val(Celular);
    $("#Correo_Representante").val(Correo);
    $("#AsisteCentroCristiano_Representante").prop('checked', VehiculoPropio);
    $("#CasaPropia_Representante").prop('checked', CasaPropia);
    $("#VehiculoPropio_Representante").prop('checked', VehiculoPropio);
    mostrar_VehiculoRepresentante();
    $("#Marca_Representante").val(Marca);
    $("#Modelo_Representante").val(Modelo);
    $("#IdReligion_Representante").val(IdReligion);
    $("#Direccion_Representante").val(Direccion);
    $("#Sector_Representante").val(Sector);
    $("#EmpresaTrabajo_Representante").val(EmpresaTrabajo);
    $("#IdCatalogoFichaInst_Representante").val(IdCatalogoFichaInst);
    $("#IdProfesion_Representante").val(IdProfesion);
    $("#CargoTrabajo_Representante").val(CargoTrabajo);
    $("#TelefonoTrabajo_Representante").val(TelefonoTrabajo);
    $("#AniosServicio_Representante").val(AniosServicio);
    $("#IngresoMensual_Representante").val(IngresoMensual);
    $("#DireccionTrabajo_Representante").val(DireccionTrabajo);
    $("#CodCatalogoCONADIS_Representante").val(CodCatalogoCONADIS);
    $("#PorcentajeDiscapacidad_Representante").val(PorcentajeDiscapacidad);
    $("#NumeroCarnetConadis_Representante").val(NumeroCarnetConadis);
    $("#IdCatalogoPAREN_Representante").val(Parentezco);

    $("#Naturaleza_Representante").prop('readonly', true);
    $("#IdTipoDocumento_Representante").prop('readonly', true);
    $("#CedulaRuc_Representante").prop('readonly', true);
    $("#EstaFallecido_Representante").prop('readonly', true);
    $("#Nombres_Representante").prop('readonly', true);
    $("#Apellidos_Representante").prop('readonly', true);
    $("#NombreCompleto_Representante").prop('readonly', true);
    $("#RazonSocial_Representante").prop('readonly', true);
    $("#FechaNacimiento_Representante").prop('readonly', true);
    $("#IdPais_Representante").prop('readonly', true);
    $("#Cod_Region_Representante").prop('readonly', true);
    $("#IdProvincia_Representante").prop('readonly', true);
    $("#IdCiudad_Representante").prop('readonly', true);
    $("#IdParroquia_Representante").prop('readonly', true);
    $("#Sexo_Representante").prop('readonly', true);
    $("#IdEstadoCivil_Representante").prop('readonly', true);
    $("#IdGrupoEtnico_Representante").prop('readonly', true);
    $("#Telefono_Representante").prop('readonly', true);
    $("#Celular_Representante").prop('readonly', true);
    $("#Correo_Representante").prop('readonly', true);
    $("#IdReligion_Representante").prop('readonly', true);
    $("#AsisteCentroCristiano_Representante").prop('readonly', true);
    $("#CasaPropia_Representante").prop('readonly', true);
    $("#VehiculoPropio_Representante").prop('readonly', true);
    $("#Marca_Representante").prop('readonly', true);
    $("#Modelo_Representante").prop('readonly', true);
    $("#Direccion_Representante").prop('readonly', true);
    $("#Sector_Representante").prop('readonly', true);
    $("#EmpresaTrabajo_Representante").prop('readonly', true);
    $("#IdCatalogoFichaInst_Representante").prop('readonly', true);
    $("#IdProfesion_Representante").prop('readonly', true);
    $("#CargoTrabajo_Representante").prop('readonly', true);
    $("#TelefonoTrabajo_Representante").prop('readonly', true);
    $("#AniosServicio_Representante").prop('readonly', true);
    $("#IngresoMensual_Representante").prop('readonly', true);
    $("#DireccionTrabajo_Representante").prop('readonly', true);
    $("#CodCatalogoCONADIS_Representante").prop('readonly', true);
    $("#PorcentajeDiscapacidad_Representante").prop('readonly', true);
    $("#NumeroCarnetConadis_Representante").prop('readonly', true);
    $("#IdCatalogoPAREN_Representante").prop('readonly', true);

    Validar_cedula_ruc_Representante();
}

function MadreRepresentante() {
    var Naturaleza = $("#Naturaleza_Madre").val();
    var IdTipoDocumento = $("#IdTipoDocumento_Madre").val();
    var CedulaRuc = $("#CedulaRuc_Madre").val();
    var EstaFallecido = $("#EstaFallecido_Madre").prop('checked');
    var Nombres = $("#Nombres_Madre").val();
    var Apellidos = $("#Apellidos_Madre").val();
    var NombreCompleto = $("#NombreCompleto_Madre").val();
    var RazonSocial = $("#RazonSocial_Madre").val();
    var FechaNacimiento = $("#FechaNacimiento_Madre").val();
    var IdPais = $("#IdPais_Madre").val();
    CargarRegion_Representante();
    var Cod_Region = $("#Cod_Region_Madre").val();
    CargarProvincia_Representante();
    var IdProvincia = $("#IdProvincia_Madre").val();
    CargaCiudad_Representante();
    var IdCiudad = $("#IdCiudad_Madre").val();
    CargarParroquia_Representante();
    var IdParroquia = $("#IdParroquia_Madre").val();
    var Sexo = $("#Sexo_Madre").val();
    var IdEstadoCivil = $("#IdEstadoCivil_Madre").val();
    var IdGrupoEtnico = $("#IdGrupoEtnico_Madre").val();
    var Telefono = $("#Telefono_Madre").val();
    var Celular = $("#Celular_Madre").val();
    var Correo = $("#Correo_Madre").val();
    var IdReligion = $("#IdReligion_Madre").val();
    var AsisteCentroCristiano = $("#AsisteCentroCristiano_Madre").prop('checked');
    var CasaPropia = $("#CasaPropia_Madre").prop('checked');
    var VehiculoPropio = $("#VehiculoPropio_Madre").prop('checked');
    var Marca = $("#Marca_Madre").val();
    var Modelo = $("#Modelo_Madre").val();
    var Direccion = $("#Direccion_Madre").val();
    var Sector = $("#Sector_Madre").val();
    var EmpresaTrabajo = $("#EmpresaTrabajo_Madre").val();
    var IdCatalogoFichaInst = $("#IdCatalogoFichaInst_Madre").val();
    var IdProfesion = $("#IdProfesion_Madre").val();
    var CargoTrabajo = $("#CargoTrabajo_Madre").val();
    var TelefonoTrabajo = $("#TelefonoTrabajo_Madre").val();
    var AniosServicio = $("#AniosServicio_Madre").val();
    var IngresoMensual = $("#IngresoMensual_Madre").val();
    var DireccionTrabajo = $("#DireccionTrabajo_Madre").val();
    var CodCatalogoCONADIS = $("#CodCatalogoCONADIS_Madre").val();
    var PorcentajeDiscapacidad = $("#PorcentajeDiscapacidad_Madre").val();
    var NumeroCarnetConadis = $("#NumeroCarnetConadis_Madre").val();
    var Parentezco = $("#IdCatalogoPAREN_Madre").val();

    $("#Naturaleza_Representante").val(Naturaleza);
    $("#IdTipoDocumento_Representante").val(IdTipoDocumento);
    $("#CedulaRuc_Representante").val(CedulaRuc);
    $("#EstaFallecido_Representante").prop('checked', EstaFallecido);
    $("#Nombres_Representante").val(Nombres);
    $("#Apellidos_Representante").val(Apellidos);
    $("#NombreCompleto_Representante").val(NombreCompleto);
    $("#RazonSocial_Representante").val(RazonSocial);
    $("#FechaNacimiento_Representante").val(FechaNacimiento);
    $("#IdPais_Representante").val(IdPais);
    $("#Cod_Region_Representante").val(Cod_Region);
    $("#IdProvincia_Representante").val(IdProvincia);
    $("#IdCiudad_Representante").val(IdCiudad);
    $("#IdParroquia_Representante").val(IdParroquia);
    $("#Sexo_Representante").val(Sexo);
    $("#IdEstadoCivil_Representante").val(IdEstadoCivil);
    $("#IdGrupoEtnico_Representante").val(IdGrupoEtnico);
    $("#Telefono_Representante").val(Telefono);
    $("#Celular_Representante").val(Celular);
    $("#Correo_Representante").val(Correo);
    $("#AsisteCentroCristiano_Representante").prop('checked', AsisteCentroCristiano);
    $("#CasaPropia_Representante").prop('checked', CasaPropia);
    $("#VehiculoPropio_Representante").prop('checked', VehiculoPropio)
    mostrar_VehiculoRepresentante();
    $("#Marca_Representante").val(Marca);
    $("#Modelo_Representante").val(Modelo);
    $("#IdReligion_Representante").val(IdReligion);
    $("#Direccion_Representante").val(Direccion);
    $("#Sector_Representante").val(Sector);
    $("#EmpresaTrabajo_Representante").val(EmpresaTrabajo);
    $("#IdCatalogoFichaInst_Representante").val(IdCatalogoFichaInst);
    $("#IdProfesion_Representante").val(IdProfesion);
    $("#CargoTrabajo_Representante").val(CargoTrabajo);
    $("#TelefonoTrabajo_Representante").val(TelefonoTrabajo);
    $("#AniosServicio_Representante").val(AniosServicio);
    $("#IngresoMensual_Representante").val(IngresoMensual);
    $("#DireccionTrabajo_Representante").val(DireccionTrabajo);
    $("#CodCatalogoCONADIS_Representante").val(CodCatalogoCONADIS);
    $("#PorcentajeDiscapacidad_Representante").val(PorcentajeDiscapacidad);
    $("#NumeroCarnetConadis_Representante").val(NumeroCarnetConadis);
    $("#IdCatalogoPAREN_Representante").val(Parentezco);

    $("#Naturaleza_Representante").prop('readonly', true);
    $("#IdTipoDocumento_Representante").prop('readonly', true);
    $("#CedulaRuc_Representante").prop('readonly', true);
    $("#EstaFallecido_Representante").prop('readonly', true);
    $("#Nombres_Representante").prop('readonly', true);
    $("#Apellidos_Representante").prop('readonly', true);
    $("#NombreCompleto_Representante").prop('readonly', true);
    $("#RazonSocial_Representante").prop('readonly', true);
    $("#FechaNacimiento_Representante").prop('readonly', true);
    $("#IdPais_Representante").prop('readonly', true);
    $("#Cod_Region_Representante").prop('readonly', true);
    $("#IdProvincia_Representante").prop('readonly', true);
    $("#IdCiudad_Representante").prop('readonly', true);
    $("#IdParroquia_Representante").prop('readonly', true);
    $("#Sexo_Representante").prop('readonly', true);
    $("#IdEstadoCivil_Representante").prop('readonly', true);
    $("#IdGrupoEtnico_Representante").prop('readonly', true);
    $("#Telefono_Representante").prop('readonly', true);
    $("#Celular_Representante").prop('readonly', true);
    $("#Correo_Representante").prop('readonly', true);
    $("#IdReligion_Representante").prop('readonly', true);
    $("#AsisteCentroCristiano_Representante").prop('readonly', true);
    $("#CasaPropia_Representante").prop('readonly', true);
    $("#VehiculoPropio_Representante").prop('readonly', true);
    $("#Marca_Representante").prop('readonly', true);
    $("#Modelo_Representante").prop('readonly', true);
    $("#Direccion_Representante").prop('readonly', true);
    $("#Sector_Representante").prop('readonly', true);
    $("#EmpresaTrabajo_Representante").prop('readonly', true);
    $("#IdCatalogoFichaInst_Representante").prop('readonly', true);
    $("#IdProfesion_Representante").prop('readonly', true);
    $("#CargoTrabajo_Representante").prop('readonly', true);
    $("#TelefonoTrabajo_Representante").prop('readonly', true);
    $("#AniosServicio_Representante").prop('readonly', true);
    $("#IngresoMensual_Representante").prop('readonly', true);
    $("#DireccionTrabajo_Representante").prop('readonly', true);
    $("#CodCatalogoCONADIS_Representante").prop('readonly', true);
    $("#PorcentajeDiscapacidad_Representante").prop('readonly', true);
    $("#NumeroCarnetConadis_Representante").prop('readonly', true);
    $("#IdCatalogoPAREN_Representante").prop('readonly', true);

    Validar_cedula_ruc_Representante();
}

function OtroRepresentante() {
    $("#Naturaleza_Representante").val("NATU");
    $("#IdTipoDocumento_Representante").val("CED");
    $("#CedulaRuc_Representante").val("");
    $("#EstaFallecido_Representante").prop('checked', false);
    $("#Nombres_Representante").val("");
    $("#Apellidos_Representante").val("");
    $("#NombreCompleto_Representante").val("");
    $("#RazonSocial_Representante").val("");
    $("#FechaNacimiento_Representante").val("");
    $("#IdPais_Representante").val("1");
    CargarRegion_Representante();
    $("#Cod_Region_Representante").val("00001");
    CargarProvincia_Representante();
    $("#IdProvincia_Representante").val("");
    CargaCiudad_Representante();
    $("#IdCiudad_Representante").val("");
    CargarParroquia_Representante();
    $("#IdParroquia_Representante").val("");
    $("#Sexo_Representante").val("");
    $("#IdEstadoCivil_Representante").val("");
    $("#IdGrupoEtnico_Representante").val("");
    $("#Telefono_Representante").val("");
    $("#Celular_Representante").val("");
    $("#Correo_Representante").val("");
    $("#AsisteCentroCristiano_Representante").prop('checked', false);
    $("#CasaPropia_Representante").prop('checked', false);
    $("#VehiculoPropio_Representante").prop('checked', false);
    mostrar_VehiculoRepresentante();
    $("#Marca_Representante").val("");
    $("#Modelo_Representante").val("");
    $("#IdReligion_Representante").val("");
    $("#Direccion_Representante").val("");
    $("#Sector_Representante").val("");
    $("#EmpresaTrabajo_Representante").val("");
    $("#IdCatalogoFichaInst_Representante").val("");
    $("#IdProfesion_Representante").val("");
    $("#CargoTrabajo_Representante").val("");
    $("#TelefonoTrabajo_Representante").val("");
    $("#AniosServicio_Representante").val("");
    $("#IngresoMensual_Representante").val("");
    $("#DireccionTrabajo_Representante").val("");
    $("#CodCatalogoCONADIS_Representante").val("");
    $("#PorcentajeDiscapacidad_Representante").val("");
    $("#NumeroCarnetConadis_Representante").val("");
    /*
    $("#Naturaleza_Representante").removeAttr('readonly');
    $("#IdTipoDocumento_Representante").removeAttr('readonly');
    $("#CedulaRuc_Representante").removeAttr('readonly');
    $("#EstaFallecido_Representante").removeAttr('readonly');
    $("#Nombres_Representante").removeAttr('readonly');
    $("#Apellidos_Representante").removeAttr('readonly');
    $("#NombreCompleto_Representante").removeAttr('readonly');
    $("#RazonSocial_Representante").removeAttr('readonly');
    $("#FechaNacimiento_Representante").removeAttr('readonly');
    $("#IdPais_Representante").removeAttr('readonly');
    $("#Cod_Region_Representante").removeAttr('readonly');
    $("#IdProvincia_Representante").removeAttr('readonly');
    $("#IdCiudad_Representante").removeAttr('readonly');
    $("#IdParroquia_Representante").removeAttr('readonly');
    $("#Sexo_Representante").removeAttr('readonly');
    $("#IdEstadoCivil_Representante").removeAttr('readonly');
    $("#IdGrupoEtnico_Representante").removeAttr('readonly');
    $("#Telefono_Representante").removeAttr('readonly');
    $("#Celular_Representante").removeAttr('readonly');
    $("#Correo_Representante").removeAttr('readonly');
    $("#IdReligion_Representante").removeAttr('readonly');
    $("#AsisteCentroCristiano_Representante").removeAttr('readonly');
    $("#CasaPropia_Representante").removeAttr('readonly');
    $("#VehiculoPropio_Representante").removeAttr('readonly');
    $("#Marca_Representante").removeAttr('readonly');
    $("#Modelo_Representante").removeAttr('readonly');
    $("#Direccion_Representante").removeAttr('readonly');
    $("#Sector_Representante").removeAttr('readonly');
    $("#EmpresaTrabajo_Representante").removeAttr('readonly');
    $("#IdCatalogoFichaInst_Representante").removeAttr('readonly');
    $("#IdProfesion_Representante").removeAttr('readonly');
    $("#CargoTrabajo_Representante").removeAttr('readonly');
    $("#TelefonoTrabajo_Representante").removeAttr('readonly');
    $("#AniosServicio_Representante").removeAttr('readonly');
    $("#IngresoMensual_Representante").removeAttr('readonly');
    $("#DireccionTrabajo_Representante").removeAttr('readonly');
    $("#CodCatalogoCONADIS_Representante").removeAttr('readonly');
    $("#PorcentajeDiscapacidad_Representante").removeAttr('readonly');
    $("#NumeroCarnetConadis_Representante").removeAttr('readonly');
    $("#IdCatalogoPAREN_Representante").prop('readonly');
    */

    $("#Naturaleza_Representante").prop('readonly', false);
    $("#IdTipoDocumento_Representante").prop('readonly', false);
    $("#CedulaRuc_Representante").prop('readonly', false);
    $("#EstaFallecido_Representante").prop('readonly', false);
    $("#Nombres_Representante").prop('readonly', false);
    $("#Apellidos_Representante").prop('readonly', false);
    $("#NombreCompleto_Representante").prop('readonly', false);
    $("#RazonSocial_Representante").prop('readonly', false);
    $("#FechaNacimiento_Representante").prop('readonly', false);
    $("#IdPais_Representante").prop('readonly', false);
    $("#Cod_Region_Representante").prop('readonly', false);
    $("#IdProvincia_Representante").prop('readonly', false);
    $("#IdCiudad_Representante").prop('readonly', false);
    $("#IdParroquia_Representante").prop('readonly', false);
    $("#Sexo_Representante").prop('readonly', false);
    $("#IdEstadoCivil_Representante").prop('readonly', false);
    $("#IdGrupoEtnico_Representante").prop('readonly', false);
    $("#Telefono_Representante").prop('readonly', false);
    $("#Celular_Representante").prop('readonly', false);
    $("#Correo_Representante").prop('readonly', false);
    $("#IdReligion_Representante").prop('readonly', false);
    $("#AsisteCentroCristiano_Representante").prop('readonly', false);
    $("#CasaPropia_Representante").prop('readonly', false);
    $("#VehiculoPropio_Representante").prop('readonly', false);
    $("#Marca_Representante").prop('readonly', false);
    $("#Modelo_Representante").prop('readonly', false);
    $("#Direccion_Representante").prop('readonly', false);
    $("#Sector_Representante").prop('readonly', false);
    $("#EmpresaTrabajo_Representante").prop('readonly', false);
    $("#IdCatalogoFichaInst_Representante").prop('readonly', false);
    $("#IdProfesion_Representante").prop('readonly', false);
    $("#CargoTrabajo_Representante").prop('readonly', false);
    $("#TelefonoTrabajo_Representante").prop('readonly', false);
    $("#AniosServicio_Representante").prop('readonly', false);
    $("#IngresoMensual_Representante").prop('readonly', false);
    $("#DireccionTrabajo_Representante").prop('readonly', false);
    $("#CodCatalogoCONADIS_Representante").prop('readonly', false);
    $("#PorcentajeDiscapacidad_Representante").prop('readonly', false);
    $("#NumeroCarnetConadis_Representante").prop('readonly', false);
    $("#IdCatalogoPAREN_Representante").prop('readonly', false);
    $("#RepresentanteValido").val("0");
    //$("#DivDatosRepresentante").show();
}

function mostrar_otro_motivo() {
    var MotivoIngreso = $("#IdCatalogoFichaMotivo_Aspirante").val();
    if (MotivoIngreso == 15) {
        $("#otro_motivo_ingreso").show();
    }
    else {
        $("#otro_motivo_ingreso").hide();
    }
};

function mostrar_otro_informacion_inst() {
    var MotivoIngreso = $("#IdCatalogoFichaInst_Aspirante").val();
    if (MotivoIngreso == 19) {
        $("#otro_informacion_inst").show();
    }
    else {
        $("#otro_informacion_inst").hide();
    }
};

function mostrar_otro_financiamiento() {
    var Informacion = $("#IdCatalogoFichaFinanc_Aspirante").val();
    if (Informacion == 22) {
        $("#otro_forma_financ").show();
    }
    else {
        $("#otro_forma_financ").hide();
    }
};

function mostrar_CantidadHermanos() {
    if ($("#TieneHermanos_Aspirante").prop('checked') == true) {
        $("#cant_hermanos").show();
    }
    else {
        $("#cant_hermanos").hide();
    }
}

function mostrar_VehiculoPadre() {
    if ($("#VehiculoPropio_Padre").prop('checked') == true) {
        $("#MarcaVehiculoPadre").show();
        $("#ModeloVehiculoPadre").show();
    }
    else {
        $("#MarcaVehiculoPadre").hide();
        $("#ModeloVehiculoPadre").hide();
    }
}

function mostrar_VehiculoMadre() {
    if ($("#VehiculoPropio_Madre").prop('checked') == true) {
        $("#MarcaVehiculoMadre").show();
        $("#ModeloVehiculoMadre").show();
    }
    else {
        $("#MarcaVehiculoMadre").hide();
        $("#ModeloVehiculoMadre").hide();
    }
}

function mostrar_VehiculoRepresentante() {
    if ($("#VehiculoPropio_Representante").prop('checked') == true) {
        $("#MarcaVehiculoRepresentante").show();
        $("#ModeloVehiculoRepresentante").show();
    }
    else {
        $("#MarcaVehiculoRepresentante").hide();
        $("#ModeloVehiculoRepresentante").hide();
    }
}

function sumar_ingresos() {
    var TotalIng = parseFloat($("#SueldoPadre").val()) + parseFloat($("#SueldoMadre").val()) + parseFloat($("#OtroIngresoPadre").val()) + parseFloat($("#OtroIngresoMadre").val());
    $("#TotalIngresos").val(TotalIng);
    saldo();
};

function sumar_egresos() {
    var TotalGasto = parseFloat($("#GastoAlimentacion").val()) + parseFloat($("#GastoEducacion").val()) + parseFloat($("#GastoServicioBasico").val()) + parseFloat($("#GastoSalud").val()) + parseFloat($("#GastoArriendo").val()) + parseFloat($("#GastoPrestamo").val()) + parseFloat($("#OtroGasto").val());
    $("#TotalEgresos").val(TotalGasto);
    saldo();
};

function saldo() {
    var Total = parseFloat($("#TotalIngresos").val()) - parseFloat($("#TotalEgresos").val());
    $("#Saldo").val(Total);
};

function actualizar_nombre_completo_aspirante() {
    var apellido = $("#Apellidos_Aspirante").val();
    var nombre = $("#Nombres_Aspirante").val();

    var nombre_completo = apellido + ' ' + nombre;
    $("#NombreCompleto_Aspirante").val(nombre_completo)
}

function actualizar_nombre_completo_padre() {
    var tipo_doc = $("#IdTipoDocumento_Padre").val();
    if (tipo_doc == "CED") {
        var apellido = $("#Apellidos_Padre").val();
        var nombre = $("#Nombres_Padre").val();

        var nombre_completo = apellido + ' ' + nombre;
        $("#NombreCompleto_Padre").val(nombre_completo)
    }
    else {
        var razon_social = $("#RazonSocial_Padre").val();
        $("#NombreCompleto_Padre").val(razon_social)
    }
}

function actualizar_nombre_completo_madre() {
    var tipo_doc = $("#IdTipoDocumento_Madre").val();
    if (tipo_doc == "CED") {
        var apellido = $("#Apellidos_Madre").val();
        var nombre = $("#Nombres_Madre").val();

        var nombre_completo = apellido + ' ' + nombre;
        $("#NombreCompleto_Madre").val(nombre_completo)
    }
    else {
        var razon_social = $("#RazonSocial_Madre").val();
        $("#NombreCompleto_Madre").val(razon_social)
    }
}

function actualizar_nombre_completo_representante() {
    var tipo_doc = $("#IdTipoDocumento_Representante").val();
    if (tipo_doc == "CED") {
        var apellido = $("#Apellidos_Representante").val();
        var nombre = $("#Nombres_Representante").val();

        var nombre_completo = apellido + ' ' + nombre;
        $("#NombreCompleto_Representante").val(nombre_completo)
    }
    else {
        var razon_social = $("#RazonSocial_Representante").val();
        $("#NombreCompleto_Representante").val(razon_social)
    }
}

function get_info_x_num_cedula_aspirante() {
    var cedula = $("#CedulaRuc_Aspirante").val();
    var tipo_doc = $("#IdTipoDocumento_Aspirante").val();

    if (cedula == null || cedula == "") {
        vaciar_campos_aspirante();
        return;
    }

    if (tipo_doc == "RUC") {
        if (cedula.length != 13) {
            alert("El documento de tipo RUC, debe tener una longitud de 13 caracteres");
            $("#CedulaRuc_Aspirante").val("");
            vaciar_campos_aspirante();
            return;
        }
    } else
        if (tipo_doc == "CED") {
            if (cedula.length != 10) {
                alert("El documento de tipo cédula, debe tener una longitud de 10 caracteres");
                $("#CedulaRuc_Aspirante").val("");
                vaciar_campos_aspirante();
                return;
            }
        }

    var datos = {
        IdEmpresa: $("#IdEmpresa").val(),
        pe_cedulaRuc: cedula
    }
    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: '/Admision/get_info_x_num_cedula',
        async: false,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data == "") {
                return;
            }
            if (data.IdAlumno != 0) {
                alert("El aspirante ya es alumno de nuestra Institución ID: " + data.Codigo);
                window.location.href = '/Admision/Index'
            }
            else if (data.IdAdmision != 0) {
                alert("El aspirante ya fue registrado");
                window.location.href = '/Admision/Index'
            }
            else {
                if (data.IdPersona != 0 && data.IdAlumno == 0) {
                    $("#Naturaleza_Aspirante").val(data.pe_Naturaleza);
                    $("#IdTipoDocumento_Aspirante").val(data.IdTipoDocumento);
                    $("#Nombres_Aspirante").val(data.pe_nombre);
                    $("#Apellidos_Aspirante").val(data.pe_apellido);
                    $("#NombreCompleto_Aspirante").val(data.pe_nombreCompleto);
                    $("#Sexo_Aspirante").val(data.pe_sexo);
                    //pe_fechaNacimiento.SetDate(new Date(data.anio, data.mes, data.dia));
                    $("#Direccion_Aspirante").val(data.Direccion);
                    $("#Telefono_Aspirante").val(data.pe_telfono_Contacto);
                    $("#Celular_Aspirante").val(data.Celular);
                    $("#Correo_Aspirante").val(data.Correo);
                    $("#FechaNacimiento_Aspirante").val(new Date(data.anio, data.mes, data.dia));
                    $("#CodCatalogoSangre_Aspirante").val(data.CodCatalogoSangre);
                    $("#CodCatalogoCONADIS_Aspirante").val(data.CodCatalogoCONADIS);
                    $("#PorcentajeDiscapacidad_Aspirante").val(data.PorcentajeDiscapacidad);
                    $("#NumeroCarnetConadis_Aspirante").val(data.NumeroCarnetConadis);
                    $("#IdGrupoEtnico_Aspirante").val(data.IdGrupoEtnico);
                    $("#IdReligion_Aspirante").val(data.IdReligion);
                    $("#AsisteCentroCristiano_Aspirante").prop("checked", data.AsisteCentroCristiano);

                    $("#LugarNacimiento_Aspirante").val(data.LugarNacimiento);
                    $("#IdPais_Aspirante").val(data.IdPais);
                    $("#Cod_Region_Aspirante").val(data.Cod_Region);
                    $("#IdProvincia_Aspirante").val(data.IdProvincia);
                    $("#IdCiudad_Aspirante").val(data.IdCiudad);
                    $("#IdParroquia_Aspirante").val(data.IdParroquia);
                    $("#Sector_Aspirante").val(data.NumeroCarnetConadis);
                    $("#IdCiudad_Aspirante").val(data.NumeroCarnetConadis);
                }
                else {
                    vaciar_campos_aspirante();
                }
            }
        },
        error: function (error) {
            alert(error);
        }
    });
}

function get_info_x_num_cedula_padre() {
    var cedula = $("#CedulaRuc_Padre").val();
    var tipo_doc = $("#IdTipoDocumento_Padre").val();

    if (cedula == null || cedula=="") {
        vaciar_campos_padre();
        return;
    }

    if (tipo_doc == "RUC") {
        if (cedula.length != 13) {
            alert("El documento de tipo RUC, debe tener una longitud de 13 caracteres");
            $("#CedulaRuc_Padre").val("");
            vaciar_campos_padre();
            return;
        }
    } else
        if (tipo_doc == "CED") {
            if (cedula.length != 10) {
                alert("El documento de tipo cédula, debe tener una longitud de 10 caracteres");
                $("#CedulaRuc_Padre").val("");
                vaciar_campos_padre();
                return;
            }
        }

    var datos = {
        pe_cedulaRuc: cedula
    }
    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: '/Admision/get_info_x_num_cedula_persona',
        async: false,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data.IdPersona == 0) {
                return;
            }
            else{
                $("#Naturaleza_Padre").val(data.pe_Naturaleza);
                $("#IdTipoDocumento_Padre").val(data.IdTipoDocumento);
                $("#Nombres_Padre").val(data.pe_nombre);
                $("#Apellidos_Padre").val(data.pe_apellido);
                $("#NombreCompleto_Padre").val(data.pe_nombreCompleto);
                $("#RazonSocial_Padre").val(data.pe_razonSocial); 
                $("#Sexo_Padre").val(data.pe_sexo);
                //pe_fechaNacimiento.SetDate(new Date(data.anio, data.mes, data.dia));
                $("#Direccion_Padre").val(data.pe_direccion);
                $("#Telefono_Padre").val(data.pe_telfono_Contacto);
                $("#Celular_Padre").val(data.pe_celular);
                $("#Correo_Padre").val(data.pe_correo);
                $("#IdEstadoCivil_Padre").val(data.IdEstadoCivil);
                $("#FechaNacimiento_Padre").val(new Date(data.anio, data.mes, data.dia));
                $("#CodCatalogoCONADIS_Padre").val(data.CodCatalogoCONADIS);
                $("#PorcentajeDiscapacidad_Padre").val(data.PorcentajeDiscapacidad);
                $("#NumeroCarnetConadis_Padre").val(data.NumeroCarnetConadis);
                $("#IdGrupoEtnico_Padre").val(data.IdGrupoEtnico);
                $("#IdReligion_Padre").val(data.IdReligion);
                $("#IdProfesion_Padre").val(data.IdProfesion);
                $("#AsisteCentroCristiano_Padre").prop("checked", data.AsisteCentroCristiano);               
            }
        },
        error: function (error) {
            alert(error);
        }
    });
}

function get_info_x_num_cedula_madre() {
    var cedula = $("#CedulaRuc_Madre").val();
    var tipo_doc = $("#IdTipoDocumento_Madre").val();

    if (cedula == null || cedula == "") {
        vaciar_campos_madre();
        return;
    }

    if (tipo_doc == "RUC") {
        if (cedula.length != 13) {
            alert("El documento de tipo RUC, debe tener una longitud de 13 caracteres");
            $("#CedulaRuc_Madre").val("");
            vaciar_campos_madre();
            return;
        }
    } else
        if (tipo_doc == "CED") {
            if (cedula.length != 10) {
                alert("El documento de tipo cédula, debe tener una longitud de 10 caracteres");
                $("#CedulaRuc_Madre").val("");
                vaciar_campos_madre();
                return;
            }
        }

    var datos = {
        pe_cedulaRuc: cedula
    }
    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: '/Admision/get_info_x_num_cedula_persona',
        async: false,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data.IdPersona == 0) {
                return;
            }
            else {
                $("#Naturaleza_Madre").val(data.pe_Naturaleza);
                $("#IdTipoDocumento_Madre").val(data.IdTipoDocumento);
                $("#Nombres_Madre").val(data.pe_nombre);
                $("#Apellidos_Madre").val(data.pe_apellido);
                $("#NombreCompleto_Madre").val(data.pe_nombreCompleto);
                $("#RazonSocial_Madre").val(data.pe_razonSocial);
                $("#Sexo_Madre").val(data.pe_sexo);
                //pe_fechaNacimiento.SetDate(new Date(data.anio, data.mes, data.dia));
                $("#Direccion_Madre").val(data.pe_direccion);
                $("#Telefono_Madre").val(data.pe_telfono_Contacto);
                $("#Celular_Madre").val(data.pe_celular);
                $("#Correo_Madre").val(data.pe_correo);
                $("#IdEstadoCivil_Madre").val(data.IdEstadoCivil);
                $("#FechaNacimiento_Madre").val(new Date(data.anio, data.mes, data.dia));
                $("#CodCatalogoCONADIS_Madre").val(data.CodCatalogoCONADIS);
                $("#PorcentajeDiscapacidad_Madre").val(data.PorcentajeDiscapacidad);
                $("#NumeroCarnetConadis_Madre").val(data.NumeroCarnetConadis);
                $("#IdGrupoEtnico_Madre").val(data.IdGrupoEtnico);
                $("#IdReligion_Madre").val(data.IdReligion);
                $("#IdProfesion_Madre").val(data.IdProfesion);
                $("#AsisteCentroCristiano_Madre").prop("checked", data.AsisteCentroCristiano);
            }
        },
        error: function (error) {
            alert(error);
        }
    });
}

function get_info_x_num_cedula_representante() {
    var cedula = $("#CedulaRuc_Representante").val();
    var tipo_doc = $("#IdTipoDocumento_Representante").val();

    if (cedula == null || cedula == "") {
        vaciar_campos_representante();
        return;
    }

    if (tipo_doc == "RUC") {
        if (cedula.length != 13) {
            alert("El documento de tipo RUC, debe tener una longitud de 13 caracteres");
            $("#CedulaRuc_Representante").val("");
            vaciar_campos_representante();
            return;
        }
    } else
        if (tipo_doc == "CED") {
            if (cedula.length != 10) {
                alert("El documento de tipo cédula, debe tener una longitud de 10 caracteres");
                $("#CedulaRuc_Representante").val("");
                vaciar_campos_representante();
                return;
            }
        }

    var datos = {
        pe_cedulaRuc: cedula
    }
    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: '/Admision/get_info_x_num_cedula_persona',
        async: false,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data.IdPersona == 0) {
                return;
            }
            else {
                $("#Naturaleza_Representante").val(data.pe_Naturaleza);
                $("#IdTipoDocumento_Representante").val(data.IdTipoDocumento);
                $("#Nombres_Representante").val(data.pe_nombre);
                $("#Apellidos_Representante").val(data.pe_apellido);
                $("#NombreCompleto_Representante").val(data.pe_nombreCompleto);
                $("#RazonSocial_Representante").val(data.pe_razonSocial);
                $("#Sexo_Representante").val(data.pe_sexo);
                //pe_fechaNacimiento.SetDate(new Date(data.anio, data.mes, data.dia));
                $("#Direccion_Representante").val(data.pe_direccion);
                $("#Telefono_Representante").val(data.pe_telfono_Contacto);
                $("#Celular_Representante").val(data.pe_celular);
                $("#Correo_Representante").val(data.pe_correo);
                $("#IdEstadoCivil_Representante").val(data.IdEstadoCivil);
                $("#FechaNacimiento_Representante").val(new Date(data.anio, data.mes, data.dia));
                $("#CodCatalogoCONADIS_Representante").val(data.CodCatalogoCONADIS);
                $("#PorcentajeDiscapacidad_Representante").val(data.PorcentajeDiscapacidad);
                $("#NumeroCarnetConadis_Representante").val(data.NumeroCarnetConadis);
                $("#IdGrupoEtnico_Representante").val(data.IdGrupoEtnico);
                $("#IdReligion_Representante").val(data.IdReligion);
                $("#IdProfesion_Representante").val(data.IdProfesion);
                $("#AsisteCentroCristiano_Representante").prop("checked", data.AsisteCentroCristiano);
            }
        },
        error: function (error) {
            alert(error);
        }
    });
}

function vaciar_campos_aspirante() {
    $("#Naturaleza_Aspirante").val("NATU");
    $("#IdTipoDocumento_Aspirante").val("CED");
    $("#Nombres_Aspirante").val("");
    $("#Apellidos_Aspirante").val("");
    $("#NombreCompleto_Aspirante").val("");
    $("#Sexo_Aspirante").val("");
    $("#Direccion_Aspirante").val("");
    $("#Telefono_Aspirante").val("");
    $("#Celular_Aspirante").val("");
    $("#Correo_Aspirante").val("");
    $("#FechaNacimiento_Aspirante").val(new Date().getDate() + "/" + (new Date().getMonth() +1) + "/" + new Date().getFullYear());
    $("#CodCatalogoSangre_Aspirante").val("");
    $("#CodCatalogoCONADIS_Aspirante").val("");
    $("#PorcentajeDiscapacidad_Aspirante").val("");
    $("#NumeroCarnetConadis_Aspirante").val("");
    $("#IdGrupoEtnico_Aspirante").val("0");
    $("#IdReligion_Aspirante").val("0");
    $("#AsisteCentroCristiano_Aspirante").prop("checked", false);

    $("#LugarNacimiento_Aspirante").val("");
    $("#IdPais_Aspirante").val("");
    $("#Cod_Region_Aspirante").val("");
    $("#IdProvincia_Aspirante").val("");
    $("#IdCiudad_Aspirante").val("");
    $("#IdParroquia_Aspirante").val("");
    $("#Sector_Aspirante").val("");
    $("#IdCiudad_Aspirante").val("");
}

function vaciar_campos_padre() {
    $("#Naturaleza_Padre").val("NATU");
    $("#IdTipoDocumento_Padre").val("CED");
    $("#CedulaRuc_Padre").val("");
    $("#EstaFallecido_Padre").prop('checked', false);
    $("#Nombres_Padre").val("");
    $("#Apellidos_Padre").val("");
    $("#NombreCompleto_Padre").val("");
    $("#RazonSocial_Padre").val("");
    $("#FechaNacimiento_Padre").val("");
    $("#IdPais_Padre").val("1");
    $("#Cod_Region_Padre").val("00001");
    $("#IdProvincia_Padre").val("");
    $("#IdCiudad_Padre").val("");
    $("#IdParroquia_Padre").val("");
    $("#Sexo_Padre").val("");
    $("#IdEstadoCivil_Padre").val("");
    $("#IdGrupoEtnico_Padre").val("");
    $("#Telefono_Padre").val("");
    $("#Celular_Padre").val("");
    $("#Correo_Padre").val("");
    $("#AsisteCentroCristiano_Padre").prop('checked', false);
    $("#CasaPropia_Padre").prop('checked', false);
    $("#VehiculoPropio_Padre").prop('checked', false);
    $("#Marca_Padre").val("");
    $("#Modelo_Padre").val("");
    $("#IdReligion_Padre").val("");
    $("#Direccion_Padre").val("");
    $("#Sector_Padre").val("");
    $("#EmpresaTrabajo_Padre").val("");
    $("#IdCatalogoFichaInst_Padre").val("");
    $("#IdProfesion_Padre").val("");
    $("#CargoTrabajo_Padre").val("");
    $("#TelefonoTrabajo_Padre").val("");
    $("#AniosServicio_Padre").val("");
    $("#IngresoMensual_Padre").val("");
    $("#DireccionTrabajo_Padre").val("");
    $("#CodCatalogoCONADIS_Padre").val("");
    $("#PorcentajeDiscapacidad_Padre").val("");
    $("#NumeroCarnetConadis_Padre").val("");
}

function vaciar_campos_madre() {
    $("#Naturaleza_Madre").val("NATU");
    $("#IdTipoDocumento_Madre").val("CED");
    $("#CedulaRuc_Madre").val("");
    $("#EstaFallecido_Madre").prop('checked', false);
    $("#Nombres_Madre").val("");
    $("#Apellidos_Madre").val("");
    $("#NombreCompleto_Madre").val("");
    $("#RazonSocial_Madre").val("");
    $("#FechaNacimiento_Madre").val("");
    $("#IdPais_Madre").val("1");
    $("#Cod_Region_Madre").val("00001");
    $("#IdProvincia_Madre").val("");
    $("#IdCiudad_Madre").val("");
    $("#IdParroquia_Madre").val("");
    $("#Sexo_Madre").val("");
    $("#IdEstadoCivil_Madre").val("");
    $("#IdGrupoEtnico_Madre").val("");
    $("#Telefono_Madre").val("");
    $("#Celular_Madre").val("");
    $("#Correo_Madre").val("");
    $("#AsisteCentroCristiano_Madre").prop('checked', false);
    $("#CasaPropia_Madre").prop('checked', false);
    $("#VehiculoPropio_Madre").prop('checked', false);
    $("#Marca_Madre").val("");
    $("#Modelo_Madre").val("");
    $("#IdReligion_Madre").val("");
    $("#Direccion_Madre").val("");
    $("#Sector_Madre").val("");
    $("#EmpresaTrabajo_Madre").val("");
    $("#IdCatalogoFichaInst_Madre").val("");
    $("#IdProfesion_Madre").val("");
    $("#CargoTrabajo_Madre").val("");
    $("#TelefonoTrabajo_Madre").val("");
    $("#AniosServicio_Madre").val("");
    $("#IngresoMensual_Madre").val("");
    $("#DireccionTrabajo_Madre").val("");
    $("#CodCatalogoCONADIS_Madre").val("");
    $("#PorcentajeDiscapacidad_Madre").val("");
    $("#NumeroCarnetConadis_Madre").val("");
}

function vaciar_campos_representante() {
    $("#Naturaleza_Representante").val("NATU");
    $("#IdTipoDocumento_Representante").val("CED");
    $("#CedulaRuc_Representante").val("");
    $("#EstaFallecido_Representante").prop('checked', false);
    $("#Nombres_Representante").val("");
    $("#Apellidos_Representante").val("");
    $("#NombreCompleto_Representante").val("");
    $("#RazonSocial_Representante").val("");
    $("#FechaNacimiento_Representante").val("");
    $("#IdPais_Representante").val("1");
    $("#Cod_Region_Representante").val("00001");
    $("#IdProvincia_Representante").val("");
    $("#IdCiudad_Representante").val("");
    $("#IdParroquia_Representante").val("");
    $("#Sexo_Representante").val("");
    $("#IdEstadoCivil_Representante").val("");
    $("#IdGrupoEtnico_Representante").val("");
    $("#Telefono_Representante").val("");
    $("#Celular_Representante").val("");
    $("#Correo_Representante").val("");
    $("#AsisteCentroCristiano_Representante").prop('checked',false);
    $("#CasaPropia_Representante").prop('checked', false);
    $("#VehiculoPropio_Representante").prop('checked', false);
    $("#Marca_Representante").val("");
    $("#Modelo_Representante").val("");
    $("#IdReligion_Representante").val("");
    $("#Direccion_Representante").val("");
    $("#Sector_Representante").val("");
    $("#EmpresaTrabajo_Representante").val("");
    $("#IdCatalogoFichaInst_Representante").val("");
    $("#IdProfesion_Representante").val("");
    $("#CargoTrabajo_Representante").val("");
    $("#TelefonoTrabajo_Representante").val("");
    $("#AniosServicio_Representante").val("");
    $("#IngresoMensual_Representante").val("");
    $("#DireccionTrabajo_Representante").val("");
    $("#CodCatalogoCONADIS_Representante").val("");
    $("#PorcentajeDiscapacidad_Representante").val("");
    $("#NumeroCarnetConadis_Representante").val("");
}