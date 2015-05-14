(function ($, win) {
    var timer;

    $(document).ready(function () {
        $('#origin').autocomplete({
            source: function () {
                $("#address-map-area").gmap3({
                    getaddress: {
                        address: $(this).val(),
                        callback: function (results) {
                            if (!results) return;
                            $("#origin").autocomplete("display", results, false);
                        }
                    }
                });
            },
            cb: {
                cast: function (item) {
                    return item.formatted_address;
                },
                select: function (item) {
                    search_onclick();
                }
            }
        });
    });

    this.search_onclick = function () {
        $('#map-area').height(($(window).height() / 3) + 'px');
        $("#map-area").gmap3({
            action: 'clear',
            name: 'directionRenderer'
        });
        $("#map-area").gmap3({
            getroute: {
                options: {
                    origin: $('#origin').val(),//"New Jersey State Fair 37 Plains Road Augusta, NJ 07822", //"48 Pirrama Road, Pyrmont NSW",
                    destination: $('#destination').val(),//"New Jersey State Fair P.O. Box 2456 Branchville, NJ 07826",//"Bondi Beach, NSW",
                    travelMode: google.maps.DirectionsTravelMode.DRIVING
                },
                callback: function (results) {
                    if (!results) return;
                    if ($('#direction-area').length > 0) {
                        $("#direction-area").empty();
                    }
                    $(this).gmap3({
                        map: {
                            options: {
                                zoom: 50,
                                center: [-33.879, 151.235]
                            }
                        },
                        directionsrenderer: {
                            container: $('#direction-area'),
                            options: {
                                directions: results
                            }
                        }
                    });

                    timer = window.setTimeout(function () {
                        makeScrollableDirection();
                    }, 100);
                }
            }
        });
    };

    this.makeScrollableDirection = function () {
        window.clearTimeout(timer);
        $('#map-area').height(($(window).height() / 3) + 'px');
        var directionHeight = ($(window).height() - ($('#map-area').height() + ($('.adp-placemark').height() * 5)));
        $('.adp-directions').parent('div[jstcache]')
            .css({ 'height': directionHeight + 'px', 'overflow-y': 'auto', 'overflow-x': 'hidden' });
    };

    $(window).resize(function () {
        makeScrollableDirection();
    });
}(jQuery, window));