$(document).ready(function () {
    if (!$('#selectGusto').hasClass("select2-hidden-accessible")) {
        initializeSelectGusto();
    } else {
        $('#selectGusto').select2('destroy');
        initializeSelectGusto();
    }

    $('#selectEmail').select2({
        tags: true,
        tokenSeparators: [',', ' ']
    });

    $('#buttonSubmit').on('click', function () {
        $('#form').submit();
        
    });


    function initializeSelectGusto(){
        $('#selectGusto').select2({
            ajax: {
                url: 'http://localhost:52521/GustoEmpanada/All',
                dataType: 'json',
                processResults: function (data) {
                    var a = [];
                    if (data.length) {
                        data.forEach(function (gusto) {
                            a.push({ "id": gusto.Id, "text": gusto.Gusto })
                        });
                    } else {
                        debugger;
                        a.push({ "id": data.Id, "text": data.Gusto })
                    }
                    return {
                        results: a
                    };
                }
            }
        });
    }

});