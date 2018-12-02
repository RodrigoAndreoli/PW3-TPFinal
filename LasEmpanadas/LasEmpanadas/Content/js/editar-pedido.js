$(document).ready(function () {
    if (!$('#selectGusto').hasClass("select2-hidden-accessible")) {
        initializeSelectGusto();
    } else {
        $('#selectGusto').select2('destroy');
        initializeSelectGusto();
    }

    $('#UsuariosNuevosString').select2({
        tags: true,
        tokenSeparators: [',', ' ']
    });

    $('#buttonSubmit').on('click', function () {
        $('#form').submit();

    });
});

function initializeSelectGusto() {
    $('#selectGusto').select2();
}