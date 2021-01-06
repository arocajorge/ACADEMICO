function SiguienteAspirante() {
    $("#DatosAspirante").hide();
    $("#BtnAspirante").attr("class", "w-10 h-10 rounded-full button text-gray-600 bg-gray-200 dark:bg-dark-1");
    $("#DatosPadre").show();
    $("#BtnPadre").attr("class", "w-10 h-10 rounded-full button text-white bg-theme-1");
}
function SiguientePadre() {
    $("#DatosPadre").hide();
    $("#BtnPadre").attr("class", "w-10 h-10 rounded-full button text-gray-600 bg-gray-200 dark:bg-dark-1");
    $("#DatosMadre").show();
    $("#BtnMadre").attr("class", "w-10 h-10 rounded-full button text-white bg-theme-1");
}
function AnteriorPadre() {
    $("#DatosPadre").hide();
    $("#BtnPadre").attr("class", "w-10 h-10 rounded-full button text-gray-600 bg-gray-200 dark:bg-dark-1");
    $("#DatosAspirante").show();
    $("#BtnAspirante").attr("class", "w-10 h-10 rounded-full button text-white bg-theme-1");
}
function SiguienteMadre() {
    $("#DatosMadre").hide();
    $("#BtnMadre").attr("class", "w-10 h-10 rounded-full button text-gray-600 bg-gray-200 dark:bg-dark-1");
    $("#DatosRepresentante").show();
    $("#BtnRepresentante").attr("class", "w-10 h-10 rounded-full button text-white bg-theme-1");
}
function AnteriorMadre() {
    $("#DatosMadre").hide();
    $("#BtnMadre").attr("class", "w-10 h-10 rounded-full button text-gray-600 bg-gray-200 dark:bg-dark-1");
    $("#DatosPadre").show();
    $("#BtnPadre").attr("class", "w-10 h-10 rounded-full button text-white bg-theme-1");
}
function SiguienteRepresentante() {
    $("#DatosRepresentante").hide();
    $("#BtnRepresentante").attr("class", "w-10 h-10 rounded-full button text-gray-600 bg-gray-200 dark:bg-dark-1");
    $("#DatosSocioEconomico").show();
    $("#BtnSocioEconomico").attr("class", "w-10 h-10 rounded-full button text-white bg-theme-1");
}
function AnteriorRepresentante() {
    $("#DatosRepresentante").hide();
    $("#BtnRepresentante").attr("class", "w-10 h-10 rounded-full button text-gray-600 bg-gray-200 dark:bg-dark-1");
    $("#DatosMadre").show();
    $("#BtnMadre").attr("class", "w-10 h-10 rounded-full button text-white bg-theme-1");
}
function AnteriorSocioenonomico() {
    $("#DatosSocioEconomico").hide();
    $("#BtnSocioEconomico").attr("class", "w-10 h-10 rounded-full button text-gray-600 bg-gray-200 dark:bg-dark-1");
    $("#DatosRepresentante").show();
    $("#BtnRepresentante").attr("class", "w-10 h-10 rounded-full button text-white bg-theme-1");
}

$("#CedulaRuc_Aspirante").blur(function () {
    Validar_cedula_ruc_Aspirante();
});

$("#CedulaRuc_Padre").blur(function () {
    Validar_cedula_ruc_Padre();
});

$("#CedulaRuc_Madre").blur(function () {
    Validar_cedula_ruc_Madre();
});

function Validar_cedula_ruc_Aspirante() {
    var datos = {
        naturaleza: $("#Naturaleza_Aspirante").val(),
        tipo_documento: $("#IdTipoDocumento_Aspirante").val(),
        cedula_ruc: $("#cedulaRuc_Aspirante").val(),
    }

    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: '@Url.Action("Validar_cedula_ruc", "Admision", new { Area = "Admision" })',
        async: false,
        bDeferRender: true,
        bProcessing: true,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data.isValid == true) {
                $("#error_documento_padre").hide();
                $("#pe_Naturaleza_padre").val(data.return_naturaleza);
            }
            else {
                $("#error_documento_padre").show();
            }
        },
        error: function (error) {
        }
    });
};