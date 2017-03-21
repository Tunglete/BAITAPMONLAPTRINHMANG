
function Edit(id, page) {
    $.ajax({
        type: 'get',
        data: {
            id: id,
            page: page
        },
        url: actionUrl.urlEdit,
        success: function (data) {
            $("#div_edit").html(data); 
        }
    });
}

function dong() {
    $('#div_edit').html("");
}
