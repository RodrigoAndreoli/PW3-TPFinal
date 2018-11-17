$(document).ready(function () {
            
    $('#selectGusto').select2({
        ajax: {
            url: 'http://localhost:52521/api/GustoEmpanadasApi',
            dataType: 'json',
            processResults: function (data) {
                var a = [];
                data.forEach(function (gusto) {
                    a.push({ "id": gusto.IdGustoEmpanada, "text": gusto.Nombre })
                });
                return {
                    results: a
                };
            }
        }
    });

    $('#selectEmail').select2({
        tags: true,
        tokenSeparators: [',', ' ']
    });

    $('#buttonSubmit').on('click', function () {
        $('#form').submit();
        
    });

});