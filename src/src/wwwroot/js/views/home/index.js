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
                    return "<a class='btn btn-default btn-sm' onclick=ShowPopup('/Home/AddEditMarca/"+data+"')><i class='fa fa-pencil'></i> Edit</a><a class='btn btn-danger btn-sm' style='margin-left:5px' onclick=Delete(" + data + ")><i class='fa fa-trash'></i> Delete</a>";
                }
            }
        ],
        "language": {
            "emptyTable": "no data found."
        }
    });
});

$(document).ready(function () {
    dataTable = $('#gridModelo').DataTable({
        "ajax": {
            "url": "/api/modelo",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "modeloDesc" },
            { "data": "marca.marcaDesc" },
            {
                "data": "modeloId",
                "render": function (data) {
                    return "<a class='btn btn-default btn-sm' onclick=ShowPopup('/Modelo/AddEditModelo/" + data + "')><i class='fa fa-pencil'></i> Edit</a><a class='btn btn-danger btn-sm' style='margin-left:5px' onclick=Delete(" + data + ")><i class='fa fa-trash'></i> Delete</a>";
                }
            }
        ],
        "language": {
            "emptyTable": "no data found."
        }
    });
});

$(document).ready(function () {
    dataTable = $('#gridAnuncio').DataTable({
        "ajax": {
            "url": "/api/anuncio",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "anuncioDesc" },
            { "data": "modelo.marca.marcaDesc" },
            { "data": "modelo.modeloDesc" },
            { "data": "ano" },
            { "data": "valorCompra" },
            { "data": "valorVenda" },
            { "data": "cor" },
            { "data": "tipoCombustivel" },
            { "data": "dataVenda" },
            {
                "data": "anuncioId",
                "render": function (data) {
                    return "<a class='btn btn-default btn-sm' onclick=ShowPopup('/Anuncio/AddEditAnuncio/" + data + "')><i class='fa fa-pencil'></i> Edit</a><a class='btn btn-danger btn-sm' style='margin-left:5px' onclick=Delete(" + data + ")><i class='fa fa-trash'></i> Delete</a>";
                }
            }
        ],
        "language": {
            "emptyTable": "no data found."
        }
    });
});

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


function FillSelectValues() {
        data = JSON.stringify(3);
        $.ajax({
            type: 'POST',
            url: '/api/Modelo',
            data: data,
            contentType: 'application/json',
            success: function (data) {
                if (data.success) {
                    $.each(data, function (key, value) {
                        $('#dropDownMarca')
                            .append($("<option></option>")
                                .attr("value", key)
                                .text(value));
                    });
                }
            }
        });
}


function Delete(id) {
    swal({
        title: "Tem certeza que deseja excluir?",
        text: "Não será possivel restaurar esse registro!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Sim, exclua!",
        closeOnConfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: '/api/modelo/' + id,
            success: function (data) {
                if (data.success) {
                    ShowMessage(data.message);
                    dataTable.ajax.reload();
                }
            }
        });
    });


}


function SubmitAddEdit(form) {
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

function Delete(id) {
    swal({
        title: "Tem certeza que deseja excluir?",
        text: "Não será possivel restaurar esse registro!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Sim, exclua!",
        closeOnConfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: '/api/marca/' + id,
            success: function (data) {
                if (data.success) {
                    ShowMessage(data.message);
                    dataTable.ajax.reload();
                }
            }
        });
    });


}


function ShowMessage(msg) {
    toastr.success(msg);
}

