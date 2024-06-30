window.onload = function () {
    bindDatatable("Agent");
};

$(document).ready(function () {

    $("#ddlUserType").change(function () {
        bindDatatable($("#ddlUserType").val());
    });

    $('#btnAddNewUser').click(function () {
        $("#error-msg-create").text('');
        $('#createModal').modal('show');
    });

    $("#Id").change(function () {
        var result = $('#Id').find('option:selected').val();

        $.ajax({
            url: `${baseUri}/UserManage/GetRole`,
            type: 'GET',
            dataType: 'json',
            data: { 'id': result },
            success: function (data) {
                if (data != null) {
                    if (data.name.toLowerCase() == "agent") {
                        //$('#Name').val(data.name);
                        $('#agentTextFields').show();
                    }
                    else {
                        //$('#Name').val(data.name);
                        $('#agentTextFields').hide();
                    }
                }
                else {
                }
            }
        });

    });
     
    $("#Refresh").click(function (e) {
        $('#tblMyUsers').DataTable().ajax.reload(null, false);
    });


$(document)
    .off('click', '#btnCreate')
    .on('click', '#btnCreate', function (e) {
        e.preventDefault();
        var _this = $(this);
        var _form = _this.closest("form");
        var isvalid = _form.valid();
        if (isvalid) {
            var formData = new FormData();
            formData.append("Id", $("#frmCreate #Id").val());
            formData.append("FirstName", $("#frmCreate #FirstName").val());
            formData.append("LastName", $("#frmCreate #LastName").val());
            formData.append("Email", $("#frmCreate #Email").val());
            formData.append("PhoneNumber", $("#frmCreate #PhoneNumber").val());
            formData.append("AddressStreet", $("#frmCreate #AddressStreet").val());
            formData.append("City", $("#frmCreate #City").val());
            formData.append("State", $("#frmCreate #State").val());
            formData.append("CCountry", $("#frmCreate #CCountry").val());
            formData.append("Zip", $("#frmCreate #Zip").val());
            formData.append("OrganizationName", $("#frmCreate #OrganizationName").val());
            formData.append("OrgAddressStreet", $("#frmCreate #OrgAddressStreet").val());
            formData.append("OrgCity", $("#frmCreate #OrgCity").val());
            formData.append("OrgState", $("#frmCreate #OrgState").val());
            formData.append("OrgZip", $("#frmCreate #OrgZip").val());
            formData.append("OrgPhoneNumber", $("#frmCreate #OrgPhoneNumber").val());
            formData.append("ImageFile", $("#frmCreate #ImageFile").get(0).files[0]);
            formData.append("LicenseID", $("#frmCreate #LicenseID").val());
            formData.append("Facebook", $("#frmCreate #Facebook").val());
            formData.append("Twitter", $("#frmCreate #Twitter").val());
            formData.append("Instagram", $("#frmCreate #Instagram").val());
            formData.append("LinkedIN", $("#frmCreate #LinkedIN").val());
            formData.append("AboutUs", $("#frmCreate #AboutUs").val());
            $.ajax({
                url: `${baseUri}/UserManage/CreateUser`,
                type: 'POST',
                cache: false,
                contentType: false,
                processData: false,
                data: formData,
                success: function (response) {
                    if (response.isSuccess === true) {
                        document.querySelector('#frmCreate').reset();
                        $("#frame").attr("src", "/images/user-icon.png");
                        $('#tblMyUsers').DataTable().ajax.reload(null, false);
                        $('#createModal').modal('hide');
                        swal("", response.message, "success")
                        //URL.createObjectURL(event.target.files[0]).reset();
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
        .off('click', '.btnEdit')
        .on('click', '.btnEdit', function () {
            const id = $(this).attr('data-key');
            $.ajax({
                url: `${baseUri}/UserManage/EditUser/${id}`,
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
        .off('click', '#btnAgentUpdate')
        .on('click', '#btnAgentUpdate', function () {
                var formData = new FormData();
                formData.append("UserId", $("#frmEdit #UserId").val());
                formData.append("Id", $("#frmEdit #Id").val());
                formData.append("Name", $('#frmEdit #Name').val());
                formData.append("FirstName", $("#frmEdit #FirstName").val());
                formData.append("LastName", $("#frmEdit #LastName").val());
                formData.append("Email", $("#frmEdit #Email").val());
                formData.append("PhoneNumber", $("#frmEdit #PhoneNumber").val());
                formData.append("AddressStreet", $("#frmEdit #AddressStreet").val());
                formData.append("City", $("#frmEdit #City").val());
                formData.append("State", $("#frmEdit #State").val());
                formData.append("CCountry", $("#frmEdit #CCountry").val());
                formData.append("Zip", $("#frmEdit #Zip").val());
                formData.append("OrganizationName", $("#frmEdit #OrganizationName").val());
                formData.append("OrgAddressStreet", $("#frmEdit #OrgAddressStreet").val());
                formData.append("OrgCity", $("#frmEdit #OrgCity").val());
                formData.append("OrgState", $("#frmEdit #OrgState").val());
                formData.append("OrgZip", $("#frmEdit #OrgZip").val());
                formData.append("OrgPhoneNumber", $("#frmEdit #OrgPhoneNumber").val());
                formData.append("ImageFile", $("#frmEdit #ImageFile").get(0).files[0]);
                formData.append("LicenseID", $("#frmEdit #LicenseID").val());
                formData.append("Facebook", $("#frmEdit #Facebook").val());
                formData.append("Twitter", $("#frmEdit #Twitter").val());
                formData.append("Instagram", $("#frmEdit #Instagram").val());
                formData.append("LinkedIN", $("#frmEdit #LinkedIN").val());
                formData.append("AboutUs", $("#frmEdit #AboutUs").val());

                $.ajax({
                    url: `${baseUri}/UserManage/EditUser`,
                    type: 'POST',
                    cache: false,
                    contentType: false,
                    processData: false,
                    data: formData,
                    success: function (response) {
                        if (response.isSuccess === true) {
                            $('#tblMyUsers').DataTable().ajax.reload(null, false);
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
        .off('click', '#btnAdminUpdate')
        .on('click', '#btnAdminUpdate', function () {
                var formData = new FormData();
                formData.append("UserId", $("#frmEdit #UserId").val());
                formData.append("Id", $("#frmEdit #Id").val());
                formData.append("Name", $('#frmEdit #Name').val());
                formData.append("FirstName", $("#frmEdit #FirstName").val());
                formData.append("LastName", $("#frmEdit #LastName").val());
                formData.append("Email", $("#frmEdit #Email").val());
                formData.append("PhoneNumber", $("#frmEdit #PhoneNumber").val());
                formData.append("AddressStreet", $("#frmEdit #AddressStreet").val());
                formData.append("City", $("#frmEdit #City").val());
                formData.append("State", $("#frmEdit #State").val());
                formData.append("CCountry", $("#frmEdit #CCountry").val());
                formData.append("Zip", $("#frmEdit #Zip").val());
                formData.append("ImageFile", $("#frmEdit #ImageFile").get(0).files[0]);
                $.ajax({
                    url: `${baseUri}/UserManage/EditUser`,
                    type: 'POST',
                    cache: false,
                    contentType: false,
                    processData: false,
                    data: formData,
                    success: function (response) {
                        if (response.isSuccess === true) {
                            $('#tblMyUsers').DataTable().ajax.reload(null, false);
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
                        url: `${baseUri}/UserManage/DeleteUser/${id}`,
                        type: 'POST',
                        cache: false,
                        contentType: false,
                        processData: false,
                        success: function (response) {
                            if (response.isSuccess === true) {
                                $('#tblMyUsers').DataTable().ajax.reload(null, false);
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


function bindDatatable(val) {
    $("#tblMyUsers").DataTable().destroy();
    datatable = $('#tblMyUsers')
        .DataTable({
            "sAjaxSource": `${baseUri}/UserManage/GetAllUsers/`,
            "bServerSide": true,
            "bProcessing": true,
            "responsive": true,
            "bSearchable": true,
            "fnServerData": function (sSource, aoData, fnCallback) {
                aoData.push({ "name": "UserType", "value": val });
                $.getJSON(sSource, aoData, function (json) {
                    fnCallback(json)
                });
            },
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
                    "data": "firstName",
                    "autoWidth": true,
                    "searchable": false
                },
                {
                    "data": "lastName",
                    "autoWidth": true,
                    "searchable": false
                },
                {
                    "data": "email",
                    "autoWidth": true,
                    "searchable": false
                },
                {
                    "data": "phoneNumber", 
                    "autoWidth": true,
                    "searchable": false
                },
                {
                    "data": "addressStreet",
                    "autoWidth": true,
                    "searchable": false
                },
                {
                    "data": "city",
                    "autoWidth": true,
                    "searchable": false
                },

                {
                    "data": "roleName",
                    "autoWidth": true,
                    "searchable": false
                },
                {
                    "orderable": false,
                    "autoWidth": true,
                    render: function (data, type, row) {
                        return `<div>
                                    <button type="button" class="btn btn-default btn-info customhref btnEdit" data-key="${row.id}">Edit</button>
                                    <button type="button" class="btn btn-sm btn-danger btnDelete" data-key="${row.id}">Delete</button>
                                </div>`;
                    }

                },
            ],

            "columnDefs": [
                { "width": "50%", "targets": [1] }
            ],
        });
}