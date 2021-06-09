var popup, dataTable;

$(document).ready(function () {
    dataTable = $('#gridMarca').DataTable({
        "ajax": {
            "url": "/api/marca",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "marcaDesc" },
            {
                "data": "marcaId",
                "render": function (data) {
                    return "<a class='btn btn-default btn-sm' onclick=ShowPopup('/Marca/AddEditMarca/" + data + "')><i class='fa fa-pencil'></i> Edit</a><a class='btn btn-danger btn-sm' style='margin-left:5px' onclick=Delete(" + data + ")><i class='fa fa-trash'></i> Delete</a>";
                }
            }
        ],
        "language": {
            "emptyTable": "no data found."
        }
    });
});

$(document).ready(function () {
     $('#gridMarcaUserOff').DataTable({
        "ajax": {
            "url": "/api/marca",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "marcaDesc" }
        ],
        "language": {
            "emptyTable": "no data found."
        }
    });
});

function SubmitAddEditMarca(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        var data = $(form).serializeJSON();
        data = JSON.stringify(data);
        $.ajax({
            type: 'POST',
            url: '/api/marca',
            data: data,
            contentType: 'application/json',
            success: function (data) {
                if (data.success) {
                    popup.dialog('close');
                    ShowMessage(data.message);
                    dataTable.ajax.reload();
                }
            }
        });

    }
    return false;
}

function ShowPopup(url) {

    var formDiv = $('<div/>');
    $.get(url)
        .done(function (response) {
            formDiv.html(response);
            popup = formDiv.dialog({
                autoOpen: true,
                resizeable: false,
                width: 600,
                height: 400,
                title: 'Adicionar ou Editar',
                close: function () {
                    popup.dialog('destroy').remove();
                }
            });
        });
}
function ShowMessage(msg) {
    toastr.success(msg);
}

