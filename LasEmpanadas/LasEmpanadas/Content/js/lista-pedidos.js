$(document).ready(function () {
    $('#tablaPedidos').DataTable({
        "lengthMenu": [[5, 10, -1], [5, 10, "Todos"]],
        "orderFixed": [[0, "desc"]],
        "columns": [
            {
                "name": "Fecha creacion",
                "orderable": true,
                "searchable": true
            },
            {
                "name": "Nombre negocio",
                "orderable": false,
                "searchable": true
            },
            {
                "name": "Id estado",
                "orderable": false,
                "searchable": true
            },
            {
                "name": "Rol",
                "orderable": false,
                "searchable": true
            },
            {
                "name": "Editar",
                "orderable": false,
                "searchable": false
            },
            {
                "name": "Elegir gustos",
                "orderable": false,
                "searchable": false
            },
            {
                "name": "Eliminar",
                "orderable": false,
                "searchable": false
            }
        ]
    });
});