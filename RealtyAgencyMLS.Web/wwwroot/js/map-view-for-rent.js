﻿function GetPropertyData(isPagination) {
    if (!isPagination)
        GetPropertyMapData(false);

    let url = window.location.href;
    let urlparts = url.split("?");
    $.ajax({
        url: `${baseUri}/ForRent/GetPropertyListAjaxMapView/?IsPagination=` + isPagination + "&" + urlparts[1],
        type: "get",
        success: function (response) {
            $("#dvPropertyList").html('');
            $("#dvPropertyList").html(response);
            if (isPagination) {
                $("html, body").animate({ scrollTop: 0 }, 1000);
            }
        },
        error: function () {
            swal("Server Error", "An error has occured!", "error");
        },
    });
}

function GetPropertyMapData(isPagination) {
    let url = window.location.href;
    let urlparts = url.split("?");
    $.ajax({
        url: `${baseUri}/ForRent/GetPropertyListAjaxMapData/?IsPagination=` + isPagination + "&" + urlparts[1],
        type: "get",
        success: function (response) {
            RefereshMapData(response);
        },
        error: function () {
            swal("Server Error", "An error has occured!", "danger");
        },
    });
}

var markers = [];
var map;
var markerClusterer;
function initMap() {
    DeleteMarkers();
    var mapOptions = {
        center: new google.maps.LatLng(32.82, -114.33),
        zoom: 8,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
    };
    map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);

    //Drawing LIB
    //const drawingManager = new google.maps.drawing.DrawingManager({
    //    drawingMode: google.maps.drawing.OverlayType.MARKER,
    //    drawingControl: true,
    //    drawingControlOptions: {
    //        position: google.maps.ControlPosition.TOP_CENTER,
    //        drawingModes: [
    //            google.maps.drawing.OverlayType.CIRCLE,
    //            google.maps.drawing.OverlayType.RECTANGLE,
    //        ],
    //    },
    //    circleOptions: {
    //        fillColor: "#4caf507a",
    //        fillOpacity: 1,
    //        strokeWeight: 2,
    //        clickable: false,
    //        editable: true,
    //        zIndex: 1,
    //    },
    //    rectangleOptions: {
    //        fillColor: "#4caf507a",
    //        fillOpacity: 1,
    //        strokeWeight: 2,
    //        clickable: false,
    //        editable: true,
    //        zIndex: 1,
    //    },
    //});
    //drawingManager.setMap(map);
    //google.maps.event.addListener(drawingManager, 'circlecomplete', function (circle) {
    //    console.log(circle.getRadius());
    //});
    //google.maps.event.addListener(drawingManager, 'rectanglecomplete', function (rectangle) {
    //    console.log(rectangle.bounds);
    //});
    //END
}
function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}
function RefereshMapData(data) {
    DeleteMarkers();
    $.each(data, function (i, item) {
        var marker = new google.maps.Marker({
            position: new google.maps.LatLng(item.latitude, item.longitude),
            map: map,
            title: item.Address,
        });
        marker.setIcon(`${baseUri}/map-ico/marker-red.ico`);
        let contentstring = '';
        if (item.listOffice.includes("The Realty Agency")) {
            contentstring = `<a target="_blank" href="${baseUri}/FeaturedListings/Details/${item.listingID}/${item.propUrl}"><div class="row"> <div class="col-md-4"><img class="card-img-top" src="${baseUri}/${item.sourceUrl ? item.sourceUrl : "/Resources/no-image.jpg"}" alt="Card image" style="height: 50px; width: 60px;" /></div> <div class="col-md-8"> <i><strong>$${numberWithCommas(item.askingPrice)}</strong></i> <br /><br /> <span>${item.squareFeet ? item.squareFeet : '--'} SqFt | ${item.totalBeds ? item.totalBeds : '--'} Bed | ${item.totalBaths ? item.totalBaths : '--'} Bed</span> </div> </div></a>`
        } else {
            contentstring = `<a onclick="GetPropertyDetails(${item.listingID}, '${item.propUrl}', 'MapView')"><div class="row"> <div class="col-md-4"><img class="card-img-top" src="${baseUri}/${item.sourceUrl ? item.sourceUrl : "/Resources/no-image.jpg"}" alt="Card image" style="height: 50px; width: 60px;" /></div> <div class="col-md-8"> <i><strong>$${numberWithCommas(item.askingPrice)}</strong></i> <br /><br /> <span>${item.squareFeet ? item.squareFeet : '--'} SqFt | ${item.totalBeds ? item.totalBeds : '--'} Bed | ${item.totalBaths ? item.totalBaths : '--'} Bed</span> </div> </div></a>`
        }
        var infowindow = new google.maps.InfoWindow({
            content: contentstring,
        });
        google.maps.event.addListener(marker, "click", function () {
            if (item.listOffice.includes("The Realty Agency")) {
                window.open(
                    `${baseUri}/FeaturedListings/Details/${item.listingID}/${item.propUrl}`,
                    '_blank' // <- This is what makes it open in a new window.
                );
            } else {
                GetPropertyDetails(item.listingID, item.propUrl, 'MapView');
            }
        });
        google.maps.event.addListener(marker, "mouseover", function () {
            marker.setIcon(`${baseUri}/map-ico/marker-green.ico`);
            infowindow.open(map, marker);
        });
        google.maps.event.addListener(marker, "mouseout", function () {
            marker.setIcon(`${baseUri}/map-ico/marker-red.ico`);
            infowindow.close(map, marker);
        });

        markers.push(marker);
    });

    // clusterer
    const clusterOptions = {
        imagePath: "https://developers.google.com/maps/documentation/javascript/examples/markerclusterer/m",
        gridSize: 30,
        zoomOnClick: true,
        maxZoom: 18,
    };
    markerClusterer = new MarkerClusterer(map, markers, clusterOptions);
    const styles = markerClusterer.getStyles();
    for (let i = 0; i < styles.length; i++) {
        styles[i].textColor = "white";
        styles[i].textSize = 10;
    }
}
function DeleteMarkers() {
    for (var i = 0; i < markers.length; i++) {
        markerClusterer.removeMarker(markers[i]);
    }
    markers = [];
}