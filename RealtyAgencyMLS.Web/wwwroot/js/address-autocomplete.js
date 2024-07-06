
$(document).ready(function () {
    //For Buy
    $("#searchSellProperty").autocomplete({
        source: function (request, response) {
            if (request.term.length < 2)
                return;
            $.ajax({
                url: `${baseUri}/Home/GetSearchResult/`,
                datatype: "json",
                data: {
                    areas: 'For Sale',
                    term: request.term
                },
                success: function (data) {
                    response($.map(data, function (val, item) {
                        return {
                            label: val.address,
                            value: val.address,
                        }
                    }))
                }
            })
        }
    });
    //For Rent
    $("#searchRentProperty").autocomplete({
        source: function (request, response) {
            if (request.term.length < 2)
                return;
            $.ajax({
                url: `${baseUri}/Home/GetSearchResult/`,
                datatype: "json",
                data: {
                    areas: 'For Rent',
                    term: request.term
                },
                success: function (data) {
                    response($.map(data, function (val, item) {
                        return {
                            label: val.address,
                            value: val.address,
                        }
                    }))
                }
            })
        }
    });


    //Address  Seardh for single property valuation
    $("#searchaddress").autocomplete({
        source: function (request, response) {
            if (request.term.length < 2)
                return;
            $.ajax({
                url: `${baseUri}/Home/GetSearchAddressForSell/`,
                datatype: "json",
                data: {
                    areas: 'For Sale',
                    term: request.term
                },
                success: function (data) {
                    response($.map(data, function (val, item) {
                        return {
                            label: val.address,
                            value: val.address,
                        }
                    }))
                }
            })
        }
    });

    function bindAnotherTextbox() {
        alert();
        //ajax call to bind textbox
    }

    //Autocomplete Google map
    let autocomplete;
    autocomplete = new google.maps.places.Autocomplete(
        document.getElementById("autocomplete"),
        { types: ["geocode"] }
    );
    autocomplete.setFields(["address_component"]);

    //Button click for Buy
    $("#searchBuyClick").click(function () {
        window.location.href = `${baseUri}/ForSell/MapView/?search_key=` + $("#searchSellProperty").val();
    });

    //Button click for Rent
    $("#searchRentClick").click(function () {
        window.location.href = `${baseUri}/ForRent/MapView/?search_key=` + $("#searchRentProperty").val();
    });

    //Button click for sell
    $("#searchSellClick").click(function () {
        debugger;
        window.location.href = `${baseUri}/PropertyValuation/GetPropertyDetail/?search_key=` + $("#searchaddress").val();
    });

    //Button click for sell
    $(".searchSellClick").click(function () {
        debugger;
        window.location.href = `${baseUri}/PropertyValuation/GetPropertyDetail/?search_key=` + $("#searchaddress").val();
    });

});
