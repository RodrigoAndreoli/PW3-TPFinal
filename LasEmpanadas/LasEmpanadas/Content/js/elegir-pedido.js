$(document).ready(function () {
    calcularSubtotal();
    calcularTotal();
    $(".cantidad").change(calcularSubtotal);
    $(".cantidad").change(calcularTotal);
});

function calcularSubtotal() {
    var unidad = parseInt($("#precio-unidad").html());
    var docena = parseInt($("#precio-docena").html());
    var subtotales = $(".precio-subtotal");
    $(".cantidad").each(function (i) {
        subtotalDocena = Math.floor($(this).val() / 12) * docena; // Calculo cantidad de docenas y las multiplico por el precio de la docena
        subtotalUnidad = ($(this).val() % 12) * unidad; // Calculo cantidad de unidades restantes a docenas y las multiplico por el precio de la unidad
        subtotales[i].innerText = subtotalDocena + subtotalUnidad;
    });
}
function calcularTotal() {
    var total = 0;
    $(".precio-subtotal").each(function () {
        total += parseInt( $(this).html() );
    });
    $("#precio-total").html(total);
}