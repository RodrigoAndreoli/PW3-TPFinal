$(document).ready(function () {
    $("#success").hide();
    $("#fail").hide();
    calcularSubtotal();
    calcularTotal();
    $(".cantidad").change(calcularSubtotal);
    $(".cantidad").change(calcularTotal);
    $("#grabar").click(saveGustos);
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

function saveGustos() {
    var idUser = parseInt($("#IdUsuario").val());
    $.ajax({
        url: "http://localhost:52521/api/Pedido/ConfirmarGustos/",
        type: 'POST',
        headers: {
            Accept: "application/json",
            contentType: "application/json"
        },
        dataType: 'json',
        data: {
            "IdUsuario": idUser,
            "Token": $("#token").val(),
            "GustosEmpanadasCantidad": getGustosCantidad()
        },        
        success: function (data) {
            debugger;
            sendSuccessAlert();
        },
        error: function (data) {
            debugger;
        }
    });
}
function getGustosCantidad() {
    var cantidades = $(".cantidad");
    var arr = [];
    $(".idGustoEmpanada").each(function (i) {
        if (cantidades.get(i).value > 0) {
            arr.push({ "IdGustoEmpanada": $(this).val(), "Cantidad": cantidades.get(i).value });
        }
    });
    return arr;
}

function sendSuccessAlert() {
    $("#success").show();
    setTimeout("$('#success').hide();", 4000);
}

function sendFailAlert() {
    $("#fail").show();
    setTimeout("$('#fail').hide();", 4000);
}