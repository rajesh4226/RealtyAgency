$(document).ready(function () {
    $("#btnPreQualify").on("click", function (e) {
        $("#contact-agent-error").text('');
        $('#btnPreQualify').prop('disabled', true);
        $('#btnPreQualify').html('Submit <i class="fa fa-spinner fa-spin"></i>');
        $.ajax({
            url: `${baseUri}/PreQualify/PostPreQualify`,
            type: "POST",
            data: ({ FirstTypeBuyer: $('input[name="FirstTypeBuyer"]:checked').val(), HaveAgent: $('input[name="HaveAgent"]:checked').val(), LookingFor: $('input[name="LookingFor"]:checked').val(), HomeType: $('input[name="HomeType"]:checked').val(), PurchasePrice: $("#PurchasePrice").val(), Downpayment: $("#Downpayment").val(), CreditScore: $('input[name="CreditScore"]:checked').val(), Name: $("#Name").val(), Address: $("#Address").val(), Email: $("#Email").val(), Phone: $("#Phone").val(), Query: $("#Query").val() }),
            success: function (response) {
                if (response.isSuccess == true) {
                    $('#btnPreQualify').prop('disabled', false);
                    document.querySelector('#PreQualifyform').reset();
                    //swal("", response.message, "success");
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
                            window.location.href = "/PreQualify/?landing=true";
                        });
                }
                else {
                    debugger;
                    $('#btnPreQualify').prop('disabled', false);
                    $('#btnPreQualify').html('Submit');
                    swal("", response.message, "error");
                }
            },
            error: function () {
                $('#btnPreQualify').prop('disabled', false);
                $('#btnPreQualify').html('Submit');
                swal("Error", "An error has occured!", "error");
            }
        });
    });
});