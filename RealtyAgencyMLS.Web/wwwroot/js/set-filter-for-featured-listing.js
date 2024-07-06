$(document).ready(function () {

    // Bed and Bath selection
    // Add active class to the current button (highlight it)
    var header = document.getElementById("bedgroup");
    var btns1 = header.getElementsByClassName("bed-select");
    for (var i = 0; i < btns1.length; i++) {
        btns1[i].addEventListener("click", function () {
            var current = document.getElementsByClassName("btn-bedroom bed-select active");
            current[0].className = current[0].className.replace(" active", "");
            this.className += " active";
        });
    }

    // Add active class to the current button (highlight it)
    var header = document.getElementById("bathgroup");
    var btns = header.getElementsByClassName("bath-select");
    for (var i = 0; i < btns.length; i++) {
        btns[i].addEventListener("click", function () {
            var current = document.getElementsByClassName("btn-bedroom bath-select active");
            current[0].className = current[0].className.replace(" active", "");
            this.className += " active";
        });
    }

    // Add active class to the current sale rent (highlight it)
    var header = document.getElementById("salerentgroup");
    var btns = header.getElementsByClassName("salerent-select");
    for (var i = 0; i < btns.length; i++) {
        btns[i].addEventListener("click", function () {
            var current = document.getElementsByClassName("btn-bedroom salerent-select active");
            current[0].className = current[0].className.replace(" active", "");
            this.className += " active";
        });
    }
     
    //Set filter value
    $('.property-tour').on("change", function (e) {
        let propertyView = [];
        $('.property-tour').each(function (i) {
            if (this.checked) {
                propertyView.push(this.value);
            }
        });
        if (propertyView.length > 0) setGetParameter("property_tour", propertyView);
        else removeURLParameter("property_tour");
    });
    $('.property-type').on("change", function (e) {
        let propertyType = [];
        $('.property-type').each(function (i) {
            if (this.checked) {
                propertyType.push("'" + this.value + "'");
            }
        });
        if (propertyType.length > 0) setGetParameter("prop_type", propertyType);
        else removeURLParameter("prop_type");
    });
    $('.property-view').on("change", function (e) {
        let propertyView = [];
        $('.property-view').each(function (i) {
            if (this.checked) {
                propertyView.push(this.value);
            }
        });
        if (propertyView.length > 0) setGetParameter("prop_view", propertyView);
        else removeURLParameter("prop_view");
    });
    $("#hqa_fee").change(function () {
        if ($("#hqa_fee").val() != "-1") setGetParameter("hqa_fee", $("#hqa_fee").val());
        else removeURLParameter("hqa_fee");
    });
    
    $('.other-amenities').on("change", function (e) {
        let propertyType = [];
        $('.other-amenities').each(function (i) {
            if (this.checked) {
                propertyType.push(this.value);
            }
        });
        if (propertyType.length > 0) setGetParameter("other_amenities", propertyType);
        else removeURLParameter("other_amenities");
    });
    $("#readyhomes").on("change", function (e) {
        if (this.checked) {
            setGetParameter("readyhomes", $("#readyhomes").val());
        }
        else removeURLParameter("readyhomes");
    });
    $("#basement").on("change", function (e) {
        if (this.checked) {
            setGetParameter("basement", $("#basement").val());
        }
        else removeURLParameter("basement");
    });
    $("#stories").on("change", function (e) {
        if (this.checked) {
            setGetParameter("stories", $("#stories").val());
        }
        else removeURLParameter("stories");
    });
    $("#search_key").on("input", function (e) {
        if ($("#search_key").val() != null && $("#search_key").val().length > 0)
            setGetParameter("search_key", $("#search_key").val());
        else removeURLParameter("search_key");
    });
    $("#keywords").on("input", function (e) {
        if ($("#keywords").val() != null && $("#keywords").val().length > 0)
            setGetParameter("keywords", $("#keywords").val());
        else removeURLParameter("keywords");
    });
    $("#min_price").on("input", function (e) {
        if ($("#min_price").val() != "-1") setGetParameter("min_price", $("#min_price").val());
        else removeURLParameter("min_price");
    });
    $("#max_price").on("input", function (e) {
        if ($("#max_price").val() != "-1") setGetParameter("max_price", $("#max_price").val());
        else removeURLParameter("max_price");
    });
    $("#min_yearbuilt").on("input", function (e) {
        if ($("#min_yearbuilt").val() != null && $("#min_yearbuilt").val().length > 0)
            setGetParameter("min_yearbuilt", $("#min_yearbuilt").val());
        else removeURLParameter("min_yearbuilt");
    });
    $("#max_yearbuilt").on("input", function (e) {
        if ($("#max_yearbuilt").val() != null && $("#max_yearbuilt").val().length > 0)
            setGetParameter("max_yearbuilt", $("#max_yearbuilt").val());
        else removeURLParameter("max_yearbuilt");
    });
    $("#daysonrealty").change(function () {
        if ($("#daysonrealty").val() != "-1") setGetParameter("days_on_realty", $("#daysonrealty").val());
        else removeURLParameter("days_on_realty");
    });
    $("#garage").change(function () {
        if ($("#garage").val() != "-1") setGetParameter("garage", $("#garage").val());
        else removeURLParameter("garage");
    });
    $("#garagemust").on("change", function (e) {
        if (this.checked) {
            setGetParameter("garagemust", $("#garagemust").val());
        }
        else removeURLParameter("garagemust");
    });
    $("#min_square_feet").change(function () {
        if ($("#min_square_feet").val() != "-1") setGetParameter("min_square_feet", $("#min_square_feet").val());
        else removeURLParameter("min_square_feet");
    });
    $("#max_square_feet").change(function () {
        if ($("#max_square_feet").val() != "-1") setGetParameter("max_square_feet", $("#max_square_feet").val());
        else removeURLParameter("max_square_feet");
    });
    $("#min_lot_size").change(function () {
        if ($("#min_lot_size").val() != "-1") setGetParameter("min_lot_size", $("#min_lot_size").val());
        else removeURLParameter("min_lot_size");
    });
    $("#max_lot_size").change(function () {
        if ($("#max_lot_size").val() != "-1") setGetParameter("max_lot_size", $("#max_lot_size").val());
        else removeURLParameter("max_lot_size");
    });

    //AUTO
    $("#search_key").autocomplete({
        source: function (request, response) {
            if (request.term.length < 2)
                return;
            $.ajax({
                url: `${baseUri}/Home/GetFeaturedSearchResult/`,
                datatype: "json",
                data: {
                    areas: $("#hidSaleRentText").val(),
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
        },
        select: function (event, ui) {
            setGetParameter("search_key", ui.item.label);
            GetPropertyData(false);
        }
    });



    //For MObile
    // Bed and Bath selection
    // Add active class to the current button (highlight it)
    var header = document.getElementById("mob_bedgroup");
    var btns1 = header.getElementsByClassName("mob_bed-select");
    for (var i = 0; i < btns1.length; i++) {
        btns1[i].addEventListener("click", function () {
            var current = document.getElementsByClassName("btn-bedroom mob_bed-select active");
            current[0].className = current[0].className.replace(" active", "");
            this.className += " active";
        });
    }

    // Add active class to the current button (highlight it)
    var header = document.getElementById("mob_bathgroup");
    var btns = header.getElementsByClassName("mob_bath-select");
    for (var i = 0; i < btns.length; i++) {
        btns[i].addEventListener("click", function () {
            var current = document.getElementsByClassName("btn-bedroom mob_bath-select active");
            current[0].className = current[0].className.replace(" active", "");
            this.className += " active";
        });
    }
    $("#mob_search_key").on("input", function (e) {
        if ($("#mob_search_key").val() != null && $("#mob_search_key").val().length > 0)
            setGetParameter("search_key", $("#mob_search_key").val());
        else removeURLParameter("search_key");
    });
    $("#mob_search_key").autocomplete({
        source: function (request, response) {
            if (request.term.length < 2)
                return;
            $.ajax({
                url: `${baseUri}/Home/GetFeaturedSearchResult/`,
                datatype: "json",
                data: {
                    areas: $("#hidSaleRentText").val(),
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
        },
        select: function (event, ui) {
            setGetParameter("search_key", ui.item.label);
            GetPropertyData(false);
        }
    });
    $("#mob_min_price").on("input", function (e) {
        if ($("#mob_min_price").val() != "0") setGetParameter("min_price", $("#mob_min_price").val());
        else removeURLParameter("min_price");
    });
    $("#mob_max_price").on("input", function (e) {
        if ($("#mob_max_price").val() != "0") setGetParameter("max_price", $("#mob_max_price").val());
        else removeURLParameter("max_price");
    });
    $("#mob_hqa_fee").change(function () {
        if ($("#mob_hqa_fee").val() != "-1") setGetParameter("hqa_fee", $("#mob_hqa_fee").val());
        else removeURLParameter("hqa_fee");
    });
    $("#mob_readyhomes").on("change", function (e) {
        if (this.checked) {
            setGetParameter("readyhomes", $("#mob_readyhomes").val());
        }
        else removeURLParameter("readyhomes");
    });
    $("#mob_basement").on("change", function (e) {
        if (this.checked) {
            setGetParameter("basement", $("#mob_basement").val());
        }
        else removeURLParameter("basement");
    });
    $("#mob_stories").on("change", function (e) {
        if (this.checked) {
            setGetParameter("stories", $("#mob_stories").val());
        }
        else removeURLParameter("stories");
    });
    $("#mob_keywords").on("input", function (e) {
        if ($("#keywords").val() != null && $("#mob_keywords").val().length > 0)
            setGetParameter("keywords", $("#mob_keywords").val());
        else removeURLParameter("keywords");
    });
    $("#mob_min_yearbuilt").on("input", function (e) {
        if ($("#mob_min_yearbuilt").val() != null && $("#mob_min_yearbuilt").val().length > 0)
            setGetParameter("min_yearbuilt", $("#mob_min_yearbuilt").val());
        else removeURLParameter("min_yearbuilt");
    });
    $("#mob_max_yearbuilt").on("input", function (e) {
        if ($("#mob_max_yearbuilt").val() != null && $("#mob_max_yearbuilt").val().length > 0)
            setGetParameter("max_yearbuilt", $("#mob_max_yearbuilt").val());
        else removeURLParameter("max_yearbuilt");
    });
    $("#mob_daysonrealty").change(function () {
        if ($("#mob_daysonrealty").val() != "-1") setGetParameter("days_on_realty", $("#mob_daysonrealty").val());
        else removeURLParameter("days_on_realty");
    });
    $("#mob_garage").change(function () {
        if ($("#mob_garage").val() != "-1") setGetParameter("garage", $("#mob_garage").val());
        else removeURLParameter("garage");
    });
    $("#mob_garagemust").on("change", function (e) {
        if (this.checked) {
            setGetParameter("garagemust", $("#mob_garagemust").val());
        }
        else removeURLParameter("garagemust");
    });
    $("#mob_min_square_feet").change(function () {
        if ($("#mob_min_square_feet").val() != "-1") setGetParameter("min_square_feet", $("#mob_min_square_feet").val());
        else removeURLParameter("min_square_feet");
    });
    $("#mob_max_square_feet").change(function () {
        if ($("#mob_max_square_feet").val() != "-1") setGetParameter("max_square_feet", $("#mob_max_square_feet").val());
        else removeURLParameter("max_square_feet");
    });
    $("#mob_min_lot_size").change(function () {
        if ($("#mob_min_lot_size").val() != "-1") setGetParameter("min_lot_size", $("#mob_min_lot_size").val());
        else removeURLParameter("min_lot_size");
    });
    $("#mob_max_lot_size").change(function () {
        if ($("#mob_max_lot_size").val() != "-1") setGetParameter("max_lot_size", $("#mob_max_lot_size").val());
        else removeURLParameter("max_lot_size");
    });
});

//Bed click
function bedClick(paramName, paramValue) {
    if (paramValue != "-1")
        setGetParameter(paramName, paramValue);
    else removeURLParameter(paramName);
}

//Bath Click
function bathClick(paramName, paramValue) {
    if (paramValue != "-1")
        setGetParameter(paramName, paramValue);
    else removeURLParameter(paramName);
}

//SaleRent click
function saleRentClick(paramName, paramValue, viewType) {
    setGetParameter(paramName, paramValue);
        window.location.href = `${baseUri}/Listings/${viewType}/?sale_rent=` + paramValue;
}

function pageClick(paramName, paramValue) {
    setGetParameter(paramName, paramValue);
    GetPropertyData(true);
}

function viewClick(paramName, paramValue) {
    let url = window.location.href;
    let urlparts = url.split('?');
    if (urlparts.length > 1)
        window.location.href = `${baseUri}/${paramName}/${paramValue}/?` + urlparts[1];
    else
        window.location.href = `${baseUri}/${paramName}/${paramValue}`;
}


//set given key in url
function setGetParameter(paramName, paramValue) {
    if (paramName != "page_no") {
        removeURLParameter("page_no");
    }

    let url = window.location.href;
    let hash = location.hash;
    url = url.replace(hash, '');
    if (url.indexOf(paramName + "=") >= 0) {
        let prefix = url.substring(0, url.indexOf(paramName + "="));
        let suffix = url.substring(url.indexOf(paramName + "="));
        suffix = suffix.substring(suffix.indexOf("=") + 1);
        suffix = (suffix.indexOf("&") >= 0) ? suffix.substring(suffix.indexOf("&")) : "";
        url = prefix + paramName + "=" + paramValue + suffix;
    }
    else {
        if (url.indexOf("?") < 0)
            url += "?" + paramName + "=" + paramValue;
        else
            url += "&" + paramName + "=" + paramValue;
    }
    window.history.pushState("1", "Page", url);

}

//Remove given key from url
function removeURLParameter(parameter) {
    if (parameter != "page_no") {
        removeURLParameter("page_no");
    }
    let url = window.location.href;
    let urlparts = url.split('?');
    if (urlparts.length >= 2) {
        let prefix = encodeURIComponent(parameter) + '=';
        let pars = urlparts[1].split(/[&;]/g);
        for (let i = pars.length; i-- > 0;) {
            if (pars[i].lastIndexOf(prefix, 0) !== -1) {
                pars.splice(i, 1);
            }
        }
        window.history.pushState("1", "Page", urlparts[0] + (pars.length > 0 ? '?' + pars.join('&') : ''));
    }
    else {
        window.history.pushState("1", "Page", url);
    }
}



 
 