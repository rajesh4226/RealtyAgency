$(document).ready(function () {
    $('#affiliatePartner').hide();

    $("#btnContactbroker").on("click", function (e) {
        $("#contact-agent-error").text('');
        $('#btnContactbroker').prop('disabled', true);
        $('#btnContactbroker').html('Send a Question <i class="fa fa-spinner fa-spin"></i>');
        $.ajax({
            url: `${baseUri}/Account/SaveContactBrokerQuery`,
            type: "POST",
            data: ({ PropertyID: $("#txtPropertyID").val(), AgentID: $("#txtAgentID").val(), AgentName: $("#txtAgentName").val(), FirstName: $("#txtContactFirstName").val(), LastName: $("#txtContactLastName").val(), Email: $("#txtContactEmail").val(), Phone: $("#txtContactPhone").val(), Query: $("#txtQuery").val(), ContactType: $("#txtContactType").val(), Heading: $("#txtHeading").val(), PropertyURL: $("#txtPropertyUrl").val(), AskingPrice: $("#txtAskingPrice").val(), PropInfo: $("#txtPropInfo").val(), PropAddress: $("#txtPropAddress").val(), PropertyImageAddress: $("#txtPropImageAddress").val() }),
            success: function (response) {
                if (response.isSuccess == true) {
                    $('#btnContactbroker').prop('disabled', false);
                    $("#contactbrokersinglemodal").modal('hide');
                    document.querySelector('#frmContactAgent').reset();
                    swal("", response.message, "success")
                }
                else {
                    $("#contact-agent-error").text(response.message);
                    $('#btnContactbroker').prop('disabled', false);
                    $('#btnContactbroker').html('Send a Question');
                }
            },
            error: function () {
                $('#btnContactbroker').prop('disabled', false);
                $('#btnContactbroker').html('Send a Question');
                swal("Error", "An error has occured!", "error")
            }
        });
    });
});