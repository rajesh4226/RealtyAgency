$(document).ready(function () {
    $("#btnContactAgent").on("click", function (e) {
        $("#contact-agent-error").text('');
        $('#btnContactAgent').prop('disabled', true);
        $('#btnContactAgent').html('Send a Question <i class="fa fa-spinner fa-spin"></i>');
        $.ajax({
            url: `${baseUri}/Account/SaveContactQuery`,
            type: "POST",
            data: ({ PropertyID: $("#txtPropertyID").val(), AgentID: $("#txtAgentID").val(), AgentName: $("#txtAgentName").val(), FirstName: $("#txtContactFirstName").val(), LastName: $("#txtContactLastName").val(), Email: $("#txtContactEmail").val(), Phone: $("#txtContactPhone").val(), Query: $("#txtQuery").val(), ContactType: $("#txtContactType").val(), Heading: $("#txtHeading").val(), PropertyURL: $("#txtPropertyUrl").val(), AskingPrice: $("#txtAskingPrice").val(), PropInfo: $("#txtPropInfo").val(), PropAddress: $("#txtPropAddress").val(), PropertyImageAddress: $("#txtPropImageAddress").val() }),
            success: function (response) {
                if (response.isSuccess == true) {
                    $('#btnContactAgent').prop('disabled', false);
                    $("#contactagentsinglemodal").modal('hide');
                    document.querySelector('#frmContactAgent').reset();
                    swal("", response.message, "success")
                }
                else {
                    $("#contact-agent-error").text(response.message);
                    $('#btnContactAgent').prop('disabled', false);
                    $('#btnContactAgent').html('Send a Question');
                }
            },
            error: function () {
                $('#btnContactAgent').prop('disabled', false);
                $('#btnContactAgent').html('Send a Question');
                swal("Error", "An error has occured!", "error")
            }
        });
    });

    $("#btnTitleContactAgent").on("click", function (e) {
        debugger;
        $("#title-contact-agent-error").text('');
        $('#btnTitleContactAgent').prop('disabled', true);
        $('#btnTitleContactAgent').html('Send  <i class="fa fa-spinner fa-spin"></i>');
        $.ajax({
            url: `${baseUri}/Account/SaveTitleContactQuery`,
            type: "POST",
            data: ({ PropertyID: $("#txtPropertyID").val(), AgentID: $("#txtAgentID").val(), AgentName: $("#txtAgentName").val(), FirstName: $("#txtContactFirstName1").val(), LastName: $("#txtContactLastName1").val(), Email: $("#txtContactEmail1").val(), Phone: $("#txtContactPhone1").val(), Query: $("#txtQuery1").val(), ContactType: $("#txtContactType").val(), Heading: $("#txtHeading").val(), PropertyURL: $("#txtPropertyUrl").val(), AskingPrice: $("#txtAskingPrice").val(), TitleRate: $("#TitleInterestRate").val(), TitlePrice: $("#TotalInterestPrice").val(), PropInfo: $("#txtPropInfo").val(), PropAddress: $("#txtPropAddress").val(), PropertyImageAddress: $("#txtPropImageAddress1").val()}),
            success: function (response) {
                if (response.isSuccess == true) {
                    $('#btnTitleContactAgent').prop('disabled', false);
                    $("#titleDetailsmodal").modal('hide');
                    document.querySelector('#frmTitleContactAgent').reset();
                    swal("", response.message, "success")
                }
                else {
                    $("#title-contact-agent-error").text(response.message);
                    $('#btnTitleContactAgent').prop('disabled', false);
                    $('#btnTitleContactAgent').html('Send');
                }
            },
            error: function () {
                $('#btnTitleContactAgent').prop('disabled', false);
                $('#btnTitleContactAgent').html('Send');
                swal("Error", "An error has occured!", "error")
            }
        });
    });

    $("#btnVtourRequest").on("click", function (e) {
        $("#vtour-error").html('');
        $('#btnVtourRequest').prop('disabled', true);
        $('#btnVtourRequest').html('Request Visit <i class="fa fa-spinner fa-spin"></i>');
        $.ajax({
            url: `${baseUri}/Account/SaveVTourRequest`,
            type: "POST",
            data: ({ PropertyID: $("#txtVTPropertyID").val(), AgentID: $("#txtVTAgentID").val(), AgentName: $("#txtVTAgentName").val(), FirstName: $("#txtVTContactFirstName").val(), LastName: $("#txtVTContactLastName").val(), Email: $("#txtVTContactEmail").val(), Phone: $("#txtVTContactPhone").val(), Query: $("#txtVTQuery").val(), ContactType: $("#txtVTContactType").val(), VTourDate: $("#txtVTDate").val(), VTourTime: $("#txtVTTime").val(), PropUrl: $("#txtVTPropertyUrl").val(), TourType: $('input[name="TourType"]:checked').val(), Subject: $("#txtVTSubject").val(), AskingPrice: $("#txtVTAskingPrice").val(), PropInfo: $("#txtVTPropInfo").val(), PropAddress: $("#txtVTPropAddress").val(), PropertyImageAddress: $("#txtVTPropImageAddress").val(), LookingForFinance: $("#txtVTFinfo").is(':checked') }),
            success: function (response) {
                if (response.isSuccess == true) {
                    $("#request-sent-setup").show();
                    $("#contact-info-setup").hide();
                    $("#time-setup").hide();
                }
                else {
                    $('#btnVtourRequest').prop('disabled', false);
                    $('#btnVtourRequest').html('Request Visit');
                    $("#vtour-error").html(response.message);
                }
            },
            error: function () {
                $('#btnVtourRequest').prop('disabled', false);
                $('#btnVtourRequest').html('Request Visit');
                swal("Error", "An error has occured!", "error")
            }
        });
    });

    $("#btnAddAgentReview").on("click", function (e) {
        $("#review-agent-error").text('');
        $('#btnAddAgentReview').prop('disabled', true);
        $('#btnAddAgentReview').html('Submit Review <i class="fa fa-spinner fa-spin"></i>');
        $.ajax({
            type: "POST",
            url: `${baseUri}/Account/GetLoggedInUser`,
            async: false,
            success: function (data) {
                if (data.isLoggedIn == true) {
                    $.ajax({
                        url: `${baseUri}/Agents/AddReview`,
                        type: "POST",
                        data: ({ AgentID: $("#AgentID").val(), StarCount: $("#StarCount").val(), ServiceProvided: $("#ServiceProvided").val(), UserMessage: $("#UserMessage").val() }),
                        success: function (response) {
                            if (response.isSuccess == true) {
                                document.querySelector('#frmAgentReview').reset();
                                //swal("", response.message, "success")
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
                                $('#btnAddAgentReview').prop('disabled', false);
                                $('#btnAddAgentReview').html('Submit Review');
                                $("#review-agent-error").text(response.message);
                            }
                        },
                        error: function () {
                            $('#btnAddAgentReview').prop('disabled', false);
                            $('#btnAddAgentReview').html('Submit Review');
                            swal("Error", "An error has occured!", "error");
                        }
                    });
                }
                else {
                    $('#loginModalRealty').modal('show');
                    $('#btnAddAgentReview').prop('disabled', false);
                    $('#btnAddAgentReview').html('Submit Review');
                }
            },
            error: function () {
                $('#btnAddAgentReview').prop('disabled', false);
                $('#btnAddAgentReview').html('Submit Review');
                swal("Server Error", "Error while adding your review!", "error");
            }
        });  
    });
});