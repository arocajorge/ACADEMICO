function ValidarEnter() {
    $(document).ready(function () {

        $('form').keypress(function (e) {
            if (e == 13) {
                return false;
            }
        });

        $('input').keypress(function (e) {
            if (e.which == 13) {
                return false;
            }
        });

    });
}

function Buscar() {
    var url_sistema = GetPathServer();
    var datos = {
        IdEmpresa: $("#IdEmpresa").val(),
        IdAnio: $("#IdAnio").val(),
        CedulaRuc_Aspirante: $("#CedulaRuc_Aspirante").val(),
    }

    $.ajax({
        type: 'POST',
        data: JSON.stringify(datos),
        url: url_sistema + '/Consulta/ConsultaAdmision',
        async: false,
        bDeferRender: true,
        bProcessing: true,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data != null) {
                $("#IdAdmision").html(data.IdAdmision);
                $("#NombreCompleto_Aspirante").html(data.NombreCompleto_Aspirante);
                $("#FechaIngreso_Aspirante").html(data.FechaString);
                $("#EstadoAdmision").html(data.CodigoEstadoAdmision);
                $("#SiguientePaso").html(data.EstadoAdmision);
            }
        },
        error: function (error) {
        }
    });
};
