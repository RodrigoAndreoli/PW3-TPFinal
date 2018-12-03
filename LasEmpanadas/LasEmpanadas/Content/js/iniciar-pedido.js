$(document).ready(function () {
    if (!$('#selectGusto').hasClass("select2-hidden-accessible")) {
        initializeSelectGusto();
    } else {
        $('#selectGusto').select2('destroy');
        initializeSelectGusto();
    }
    if (!$('#selectEmail').hasClass("select2-hidden-accessible")) {
        initializeSelectEmail();
    } else {
        $('#selectEmail').select2('destroy');
        initializeSelectEmail();
    }

    $('#selectEmail').select2({
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
function initializeSelectEmail() {
    $('#selectEmail').select2();
}