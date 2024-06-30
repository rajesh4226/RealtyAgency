$(document).ready(function () {

    $(document)
        .off('click', '#btnCreate')
        .on('click', '#btnCreate', function () {

            var formData = new FormData();
            formData.append("ModuleName", $("#frmCreate #ModuleName").val());
            formData.append("Description", $("#frmCreate #Description").val());

            $.ajax({
                url: `${baseUri}/Role/CreateModuleasync`,
                type: 'POST',
                cache: false,
                contentType: false,
                processData: false,
                data: formData,
                success: function (response) {
                    if (response.isSuccess === true) {
                        document.querySelector('#frmCreate').reset();
                        $('#tblmyModules').DataTable().ajax.reload(null, false);
                        $('#createModal').modal('hide');
                        swal("", response.message, "success")
                    }
                    else {
                        $("#error-msg-create").text(response.message);
                    }
                },
                error: function (response) {
                    swal("Error", "An error has occured!", "error")
                }
            });


        });
});