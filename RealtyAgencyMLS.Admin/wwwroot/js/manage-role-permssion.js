$(document).ready(function () {

    $(document)
        .off('click', '.btnDelete')
        .on('click', '.btnDelete', function () {
            const id = $(this).attr('data-key');

            swal({
                title: "Are you sure?",
                text: "Once deleted, you will not be able to recover this record!",
                type: "warning",
                showCancelButton: true,
                cancelButtonClass: "btn-primary",
                confirmButtonClass: "btn-danger",
                confirmButtonText: "Delete!",
                closeOnConfirm: false
            },
                function () {
                    debugger;
                    $.ajax({
                        url: `${baseUri}/RolePermission/Delete/${id}`,
                        type: 'POST',
                        cache: false,
                        contentType: false,
                        processData: false,
                        success: function (response) {
                            if (response.isSuccess === true) {
                                swal({
                                    title: "",
                                    text: response.message,
                                    type: "success",
                                    showCancelButton: false,
                                    confirmButtonClass: "btn-primary",
                                    confirmButtonText: "Ok!",
                                    closeOnConfirm: false
                                },
                                    function () {
                                        location.reload();
                                    });

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
});