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

}
function AnteriorMadre() {
    $("#DatosMadre").hide();
    $("#BtnMadre").attr("class", "w-10 h-10 rounded-full button text-gray-600 bg-gray-200 dark:bg-dark-1");
    $("#DatosPadre").show();
    $("#BtnPadre").attr("class", "w-10 h-10 rounded-full button text-white bg-theme-1");
}