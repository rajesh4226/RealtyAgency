$(document).on("click", ".save-home", function () {
    let propId = $(this).attr('property-id');
    let viewType = $(this).attr('viewtype');
    let propUrl = $(this).attr('property-url');
    let propdetails = $(this).attr('property-details');
    let propType = $(this).attr('property-type');
    var pcchangeid = $(this);
    $.ajax({
        type: "POST",
        url: `${baseUri}/Account/GetLoggedInUser`,
        async: false,
        success: function (data) {
            if (data.isLoggedIn == true) {
                $.ajax({
                    type: "POST",
                    url: `${baseUri}/Account/SaveListing`,
                    data: ({ PropID: propId, PropType: propType, propUrl: `${baseUri}/${propType}/Details/${propId}/${propUrl}/${viewType}/` }),
                    success: function (data) {
                        if (data.isSuccess == true) {
                            $(pcchangeid).removeClass("save-home");
                            $(pcchangeid).addClass("remove-home");
                            if (propdetails)
                                $(pcchangeid).html('<i class="fa fa-heart" aria-hidden="true"></i> Remove');
                            else
                                $(pcchangeid).html('<i class="fa fa-heart" aria-hidden="true"></i>');
                        }
                    },
                    error: function () {
                        swal("Server Error", "An error has occured while saving property!", "error");

                    }
                });
            }
            else {
                $('#loginModalRealty').modal('show');
            }
        },
        error: function () {
            swal("Server Error", "Error while getting user info!", "error");
        }
    });
});
$(document).on("click", ".remove-home", function () {
    let propId = $(this).attr('property-id');
    let viewType = $(this).attr('viewtype');
    let propUrl = $(this).attr('property-url');
    let propdetails = $(this).attr('property-details');
    let propType = $(this).attr('property-type');
    var pcchangeid = $(this);
    $.ajax({
        type: "POST",
        url: `${baseUri}/Account/GetLoggedInUser`,
        async: false,
        success: function (data) {
            if (data.isLoggedIn == true) {
                $.ajax({
                    type: "POST",
                    url: `${baseUri}/Account/RemoveListing`,
                    data: ({ PropID: propId, PropType: propType, propUrl: `${baseUri}/${propType}/Details/${propId}/${propUrl}/${viewType}/` }),
                    success: function (data) {
                        if (data.isSuccess == true) {
                            $(pcchangeid).removeClass("remove-home");
                            $(pcchangeid).addClass("save-home");
                            if (propdetails)
                                $(pcchangeid).html('<i class="fa fa-heart-o" aria-hidden="true"></i> Save');
                            else
                                $(pcchangeid).html('<i class="fa fa-heart-o" aria-hidden="true"></i>');
                        }

                    },
                    error: function () {
                        swal("Server Error", "An error has occured while removing property!", "error");
                    }
                });
            }
            else {
                $('#loginModalRealty').modal('show');
            }
        },
        error: function () {
            swal("Server Error", "Error while getting user info!", "error");
        }
    });
});