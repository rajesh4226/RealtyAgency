﻿$(document).ready(function () {

    $('#btnAddNewVehiclePopup').click(function () {
        $("#error-msg-create").text('');
        $('#createModal').modal('show');
    });
    var table = $('#tblVehicle')
        .DataTable({
            "sAjaxSource": `${baseUri}/Vehicle/GetAllVehicle`,
            "bServerSide": true,
            "bProcessing": true,
            "responsive": true,
            "bSearchable": true,
            "ordering": false,
            "searching": true,
            "lengthChange": true,
            "order": [[0, 'asc']],
            "language": {
                "emptyTable": "No record found.",
                "processing":
                    '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> '
            },
            "columns": [
                {
                    "data": "pid",
                    "autoWidth": true,
                    "searchable": false
                },
                {
                    "data": "vehicleName",
                    "autoWidth": true,
                    "searchable": false
                },
               
                {
                    "data": "price",
                    "autoWidth": true,
                    "searchable": false
                },
                {
                    "data": "description",
                    "autoWidth": true,
                    "searchable": false
                },
                {
                    "orderable": false,
                    "autoWidth": true,
                    render: function (data, type, row) {
                        return `<div>
                                    <button type="button" class="btn btn-sm btn-info mr-2 btnEdit" data-key="${row.pid}">Edit</button>
                                    <button type="button" class="btn btn-sm btn-danger btnDelete" data-key="${row.pid}">Delete</button>
                                </div>`;
                    }

                },
            ],

            //"columnDefs": [
            //    { "width": "50%", "targets": [1] }
            //],
        });

    $("#Refresh").click(function (e) {
        $('#tblVehicle').DataTable().ajax.reload(null, false);
    });

    $(document)
        .off('click', '.btnEdit')
        .on('click', '.btnEdit', function () {

            const id = $(this).attr('data-key');
            $.ajax({
                url: `${baseUri}/Vehicle/Edit/${id}`,
                type: 'GET',
                cache: false,
                contentType: false,
                processData: false,
                success: function (response) {
                    $('#editPartial').html(response);
                    $('#editModal').modal('show');
                },
                error: function (response) {
                    swal("Error", "An error has occured!", "error")
                }
            });
        });

    $(document)
        .off('click', '#btnCreate')
        .on('click', '#btnCreate', function (e) {
            debugger;
            alert();
            e.preventDefault();
            var _this = $(this);
            var _form = _this.closest("form");
            var isvalid = _form.valid();
            if (isvalid) {
                var formData = new FormData();
                formData.append("VehicleName", $("#frmCreate #VehicleName").val());
                formData.append("Price", $("#frmCreate #Price").val());
                formData.append("ImagePath", $("#frmCreate #ImagePath").val());
                formData.append("Description", $("#frmCreate #Description").val());
                formData.append("CompanyID", $("#frmCreate #CompanyID").val());
                formData.append("VehicleTypeID", $("#frmCreate #VehicleTypeID").val());
                formData.append("ImageFile", $("#frmCreate #ImageFile").get(0).files[0]);
                formData.append("ImageFile", $("#frmCreate #ImageFile").get(0).files[0]);
                $.ajax({
                    url: `${baseUri}/Vehicle/Create`,
                    type: 'POST',
                    cache: false,
                    contentType: false,
                    processData: false,
                    data: formData,
                    success: function (response) {
                        if (response.isSuccess === true) {
                            document.querySelector('#frmCreate').reset();
                            $('#tblVehicle').DataTable().ajax.reload(null, false);
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
            }
        });

    $(document)
        .off('click', '#btnUpdate')
        .on('click', '#btnUpdate', function () {
            debugger;
            var formData = new FormData();
            formData.append("PID", $("#frmEdit #PID").val());
            formData.append("VehicleName", $("#frmEdit #VehicleName").val());
            formData.append("Price", $("#frmEdit #Price").val());
            formData.append("ImagePath", $("#frmEdit #ImagePath").val());
            formData.append("Description", $("#frmEdit #Description").val());
            formData.append("CompanyID", $("#frmEdit #CompanyID").val());
            formData.append("VehicleTypeID", $("#frmEdit #VehicleTypeID").val());
            formData.append("ImageFile", $("#frmEdit #ImageFile").get(0).files[0]);
            $.ajax({
                url: `${baseUri}/Vehicle/Edit`,
                type: 'POST',
                cache: false,
                contentType: false,
                processData: false,
                data: formData,
                success: function (response) {
                    if (response.isSuccess === true) {
                        $('#tblVehicle').DataTable().ajax.reload(null, false);
                        $('#editModal').modal('hide');
                        $('#editPartial').html('');
                        swal("", response.message, "success")
                    }
                    else {
                        $("#error-msg-edit").text(response.message);
                    }
                },
                error: function (response) {
                    swal("Error", "An error has occured!", "error")
                }
            });
        });

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
                    $.ajax({
                        url: `${baseUri}/Vehicle/Delete/${id}`,
                        type: 'POST',
                        cache: false,
                        contentType: false,
                        processData: false,
                        success: function (response) {
                            if (response.isSuccess === true) {
                                $('#tblVehicle').DataTable().ajax.reload(null, false);
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
});