//#region تعریف آیکون ها
var greenIcon = new L.Icon({
    iconUrl: 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-green.png',
    shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.7/images/marker-shadow.png',
    iconSize: [25, 41],
    iconAnchor: [12, 41],
    popupAnchor: [1, -34],
    shadowSize: [41, 41]
});
var blueIcon = new L.Icon({
    iconUrl: 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-blue.png',
    shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.7/images/marker-shadow.png',
    iconSize: [25, 41],
    iconAnchor: [12, 41],
    popupAnchor: [1, -34],
    shadowSize: [41, 41]
});
var goldIcon = new L.Icon({
    iconUrl: 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-gold.png',
    shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.7/images/marker-shadow.png',
    iconSize: [25, 41],
    iconAnchor: [12, 41],
    popupAnchor: [1, -34],
    shadowSize: [41, 41]
});
var redIcon = new L.Icon({
    iconUrl: 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-red.png',
    shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.7/images/marker-shadow.png',
    iconSize: [25, 41],
    iconAnchor: [12, 41],
    popupAnchor: [1, -34],
    shadowSize: [41, 41]
});
var orangeIcon = new L.Icon({
    iconUrl: 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-orange.png',
    shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.7/images/marker-shadow.png',
    iconSize: [25, 41],
    iconAnchor: [12, 41],
    popupAnchor: [1, -34],
    shadowSize: [41, 41]
});
var yellowIcon = new L.Icon({
    iconUrl: 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-yellow.png',
    shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.7/images/marker-shadow.png',
    iconSize: [25, 41],
    iconAnchor: [12, 41],
    popupAnchor: [1, -34],
    shadowSize: [41, 41]
});
var violetIcon = new L.Icon({
    iconUrl: 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-violet.png',
    shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.7/images/marker-shadow.png',
    iconSize: [25, 41],
    iconAnchor: [12, 41],
    popupAnchor: [1, -34],
    shadowSize: [41, 41]
});
var greyIcon = new L.Icon({
    iconUrl: 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-grey.png',
    shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.7/images/marker-shadow.png',
    iconSize: [25, 41],
    iconAnchor: [12, 41],
    popupAnchor: [1, -34],
    shadowSize: [41, 41]
});
var blackIcon = new L.Icon({
    iconUrl: 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-black.png',
    shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.7/images/marker-shadow.png',
    iconSize: [25, 41],
    iconAnchor: [12, 41],
    popupAnchor: [1, -34],
    shadowSize: [41, 41]
});
//#endregion

var Count = document.getElementById("Count").innerHTML;
var Locations = [];
const Icons = [redIcon, blueIcon, greenIcon];
const PolygonColor = ['red', 'blue', 'green'];
var Draw = 0;
const latlngs = [];
var polygonGroup = L.featureGroup();
var polygonJsonArray = [];
var str;
var sumDistance = [];
var sumPolygonKmTrafic = 0;
var sumPolygonTimeTrafic = 0;
var sumPolygonKm = 0;
var sumPolygonTime = 0;
var PolygonOnEdit;

for (let i = 0; i < Count; i++) {
    var x = document.getElementById('x' + i).value;
    var y = document.getElementById('y' + i).value;
    var s = document.getElementById('s' + i).value;
    var Popup = document.getElementById('Popup' + i).value;
    var id = document.getElementById('id' + i).value;

    Locations[i] = [x, y, s, Popup, id];
}

var x = Locations[0][0];
var y = Locations[0][1];
var map = L.map('map').setView([x, y], 10);


L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', {
    attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
}).addTo(map);

L.TileLayer.wmsHeader(
    "https://map.ir/shiveh",
    {
        attribution: '© map.ir © openstreetmap',
        layers: "Shiveh:Shiveh",
        format: "image/png",
        minZoom: 1,
        maxZoom: 20,
        tileSize: 128
    },
    [
        {
            header: "x-api-key",
            value: "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImp0aSI6IjdjOWJjYjk0NTE3MDVjMzE2M2FjZDJiYWI1YWZiZDhmMjdiNzM5ODZmNTFlMTA3Y2FiNTBmOTQxNDU5NDAyYzUzYjkxMDExNzI3NGMwNGZiIn0.eyJhdWQiOiIyMzE2MiIsImp0aSI6IjdjOWJjYjk0NTE3MDVjMzE2M2FjZDJiYWI1YWZiZDhmMjdiNzM5ODZmNTFlMTA3Y2FiNTBmOTQxNDU5NDAyYzUzYjkxMDExNzI3NGMwNGZiIiwiaWF0IjoxNjg5MjM1NTQzLCJuYmYiOjE2ODkyMzU1NDMsImV4cCI6MTY5MTgyNzU0Mywic3ViIjoiIiwic2NvcGVzIjpbImJhc2ljIl19.dJjZKagKXsrmhmbyPZZ4ciSn0Ctn5-sfUyLPu-tpXUJec7Wc1pPGcybFeVdzf3b_9WiQVlonz_BiBJI2uvZ21CfUfjkn0dH5NP_HnffSMdWqe_eMIFBWWU8RsFIO1CA5Q85bvjlG8Jylsp7yj2a_7uBaBgGdQBGkWFtxtMgu4CoQsQtRDastlpcn6VIi9AvGF-NGj9aM0o1Wn-sshO-rgMepSO3xB_FaWj0YACB71nyCiGQT-oaG3r5xZtZ6gtNHdGCIaTfh6fh31C-iuqTjTakqxAtS270tJ7ELtSBIEkrwmm7GJZNUS4vPPv7qdwYy3kGLsD83dTYt11IAV4Ds7g"
        }
    ]
).addTo(map);

var index = 0;
var polygon = null;
var MarkerPolygonId = [];
var PolygonWithMarkers = [];

for (var i = 0; i < Locations.length; i++) {
    marker = new L.marker([Locations[i][0], Locations[i][1]], {
        icon: Icons[(Locations[i][2]) - 1],
        contextmenu: true,
        contextmenuInheritItems: true,
        contextmenuItems: [{
            text: 'اضافه کرده پالیگان',
            callback: function (marker) {
                if (Draw == 1) {
                    var DontDraw = false;
                    for (var j = 0; j < PolygonWithMarkers.length; j++) {
                        for (var z = 0; z < PolygonWithMarkers[j][1].length; z++) {
                            if (PolygonWithMarkers[j][1][z] == marker.relatedTarget._icon.id)
                                DontDraw = true;
                        }
                    }
                    var t;
                    var t2;
                    var s = -1;
                    var s2 = -1;
                    if (index > 0) {
                        t = marker.relatedTarget._icon.id;
                        t2 = MarkerPolygonId[index - 1];
                        for (var j = 0; j < Locations.length; j++) {

                            if (Locations[j][4] == t) {
                                s = Locations[j][2];
                            }
                            if (Locations[j][4] == t2) {
                                s2 = Locations[j][2];
                            }
                            if (s != -1 && s2 != -1)
                                break;
                        }
                    }
                    if (s2 == s && DontDraw == false) {
                        $('#error').text("");
                        if (polygon != null) {
                            map.removeLayer(polygon);
                        }
                        latlngs[index] = [marker.latlng.lat, marker.latlng.lng]
                        if (index > 0) {
                            DistancePolygon(latlngs[index - 1], latlngs[index]);
                        }
                        polygon = new L.polygon(latlngs, {
                            color: 'red',
                            contextmenu: true,
                            contextmenuInheritItems: false,
                            contextmenuItems: [{
                                text: "اضافه کردن توضیحات",
                                callback: function (polygon) {
                                    PolygonOnEdit = polygon.relatedTarget._leaflet_id;
                                    $('#myModal').modal('show');
                                    $('#popupText span').remove();
                                    str = polygon.relatedTarget._popup._content;
                                    $('#popupText').append(str);
                                }
                            }, {
                                text: "ویرایش پالیگان",
                                callback: function (polygon) {
                                    if (index > 0) {
                                        nextPolygon();
                                    }
                                    var k = polygon.relatedTarget._leaflet_id;
                                    polygon = polygonGroup._layers[k];
                                    delete polygonGroup._layers[k];
                                    for (var i = 0; i < polygon._latlngs[0].length; i++) {
                                        latlngs[i] = [polygon._latlngs[0][i].lat, polygon._latlngs[0][i].lng];
                                        index = i + 1;
                                    }
                                    for (var i = 0; i < PolygonWithMarkers.length; i++) {
                                        if (PolygonWithMarkers[i][0] == k) {
                                            for (var j = 0; j < PolygonWithMarkers[i][1].length; j++) {
                                                MarkerPolygonId[j] = PolygonWithMarkers[i][1][j];
                                            }
                                        }
                                    }
                                }
                            }]
                        }).addTo(map).bindPopup(sumPolygonKmTrafic);
                        polygonGroup.eachLayer(function (layer) {
                            layer.addTo(map);
                        });
                        MarkerPolygonId[index] = marker.relatedTarget._icon.id;
                        index++;
                    } else {
                        if (DontDraw == true) {
                            $('#error').text("");
                            $('#error').text("پین قبلا در یک پالیگان وجود دارد.");
                        } else {
                            $('#error').text("");
                            $('#error').text("پین ها باید همرنگ باشند.");
                        }

                    }
                }
            }
        }, {
            separator: true,
            index: 1
        }]
    })
        .addTo(map).bindPopup(Locations[i][3]);
    marker._icon.id = Locations[i][4];
}

$('select').on('change', function () {
    if (Draw == 1)
        Draw = 0;
    else
        Draw = 1;
});

$('#nextPolygon').on('click', nextPolygon)


$('#savePolygon').on('click', function () {
    if (index > 0) {
        nextPolygon();
    }

    var check = [];
    for (var i = 0; i < Locations.length; i++) {
        var temp = Locations[i][4];
        for (var j = 0; j < PolygonWithMarkers.length; j++) {
            for (var z = 0; z < PolygonWithMarkers[j][1].length; z++) {
                if (temp == PolygonWithMarkers[j][1][z]) {
                    check[i] = temp;
                }
            }
        }
    }
    if (check.length != Locations.length) {
        $('#error').text("لطفا تمام آدرس‌ها را در پالیگان قرار دهید");
        LastPolygon();
    }
    else {
        $('#error').text("");
        if (PolygonWithMarkers.length > 0) {
            for (var i = 0; i < PolygonWithMarkers.length; i++) {
                var polygonJson = {};
                polygonJson.PolygonID = PolygonWithMarkers[i][0];
                polygonJson.Address = [];
                for (var j = 0; j < PolygonWithMarkers[i][1].length; j++) {
                    polygonJson.Address.push(PolygonWithMarkers[i][1][j])
                }
                polygonJson.Popup = PolygonWithMarkers[i][2];
                polygonJsonArray.push(polygonJson);
                delete polygonJson;
            }
            var js = JSON.stringify(polygonJsonArray);
            $.ajax({
                type: "Post",
                url: '/Polygon/SavePolygon',
                data: js,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    alert(res);
                },
                error: function () {
                    alert("error");
                }
            });
        }
    }
})
$('#LoadBtn').on('click', function () {
    $.ajax({
        type: "Get",
        url: '/GetPolygon/GetPolygon',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (res) {

            for (var i = 0; i < res.length; i++) {
                for (var j = 0; j < res[i].latLngs.length; j++) {
                    $('#error').text("");
                    var stemp;
                    for (var z = 0; z < Locations.length; z++) {
                        if (Locations[z][0] == res[i].latLngs[j].lat && Locations[z][1] == res[i].latLngs[j].lng) {
                            stemp = Locations[z][2];
                            MarkerPolygonId.push(Locations[z][4]);

                        }
                    }
                    if (polygon != null) {
                        map.removeLayer(polygon);
                    }
                    latlngs[index] = [res[i].latLngs[j].lat, res[i].latLngs[j].lng]
                    polygon = new L.polygon(latlngs, {
                        color: PolygonColor[res[i].style - 1],
                        contextmenu: true,
                        contextmenuInheritItems: false,
                        contextmenuItems: [{
                            text: "اضافه کردن توضیحات",
                            callback: function (polygon) {
                                PolygonOnEdit = polygon.relatedTarget._leaflet_id;
                                $('#myModal').modal('show');
                                $('#popupText span').remove();
                                str = polygon.relatedTarget._popup._content;
                                $('#popupText').append(str);
                            }
                        }, {
                            text: "ویرایش پالیگان",
                            callback: function (polygon) {
                                if (index > 0) {
                                    nextPolygon();
                                }
                                var k = polygon.relatedTarget._leaflet_id;
                                polygon = polygonGroup._layers[k];
                                delete polygonGroup._layers[k];
                                for (var i = 0; i < polygon._latlngs[0].length; i++) {
                                    latlngs[i] = [polygon._latlngs[0][i].lat, polygon._latlngs[0][i].lng];
                                    index = i + 1;
                                }
                                for (var i = 0; i < PolygonWithMarkers.length; i++) {
                                    if (PolygonWithMarkers[i][0] == k) {
                                        for (var j = 0; j < PolygonWithMarkers[i][1].length; j++) {
                                            MarkerPolygonId[j] = PolygonWithMarkers[i][1][j];
                                        }
                                    }
                                }
                            }
                        }]
                    }).addTo(map).bindPopup(res[i].popup);
                    polygonGroup.eachLayer(function (layer) {
                        layer.addTo(map);
                    });

                    index++;
                }
                nextPolygon();
            }
        },
        error: function () {
            alert("error");
        }
    });
})

function nextPolygon() {
    if (index > 1) {
        $('#error').text("");
        PolygonWithMarkers.push([polygon._leaflet_id, MarkerPolygonId, polygon._popup._content]);
        polygon.addTo(polygonGroup);
        Delete();
    } else {
        $('#error').text("");
        $('#error').text("پالیگان باید حداقل شامل دو آدرس باشد");
    }


}
function Delete() {
    while (latlngs.length > 0) {
        latlngs.pop();
    }
    MarkerPolygonId = [];
    sumPolygonKm = 0;
    sumPolygonKmTrafic = 0;
    sumPolygonTime = 0;
    sumPolygonTimeTrafic = 0;
    index = 0;
}


function DistancePolygon(originLatLng, distanceLatLng, id) {
    var Distance = {};
    Distance.OriginLatLng = originLatLng[0] + "," + originLatLng[1];
    Distance.DistanceLatLng = distanceLatLng[0] + "," + distanceLatLng[1];
    var js = JSON.stringify(Distance)
    var have = false;
    for (var i = 0; i < sumDistance.length; i++) {
        if (sumDistance[i].id == id)
            have = true;
    }
    if (have == false) {
        sumDistance.push({ id: id, sumPolygonKmTrafic: 0, sumPolygonTimeTrafic: 0, sumPolygonKm: 0, sumPolygonTime: 0, pString: "" });
    }
    $.ajax({
        type: "Post",
        url: "/Distance/FindDistanceTrafic",
        data: js,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (res) {
            $.ajax({
                type: "Post",
                url: "/Distance/FindDistance",
                data: js,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res1) {

                    for (var i = 0; i < sumDistance.length; i++) {
                        if (sumDistance[i].id == id) {
                            sumDistance[i].sumPolygonKmTrafic += (res.rows[0].elements[0].distance.value);
                            sumDistance[i].sumPolygonTimeTrafic += (res.rows[0].elements[0].duration.value);
                            sumDistance[i].sumPolygonKm += (res1.rows[0].elements[0].distance.value);
                            sumDistance[i].sumPolygonTime += (res1.rows[0].elements[0].duration.value)

                        }

                    }
                    for (var i = 0; i < sumDistance.length; i++) {
                        temp = Math.round(sumDistance[i].sumPolygonTimeTrafic / 60);
                        temp2 = Math.round(sumDistance[i].sumPolygonTime / 60);
                        sumDistance[i].pString = "<span><b>مسافت : </b> " + (sumDistance[i].sumPolygonKmTrafic / 1000).toFixed(1) + " کیلومتر" + "<br>" + "<b>محاسبه زمان با ترافیک : </b> " + temp + " دقیقه" + "<br><b>محاسبه زمان بدون ترافیک : </b> " + temp2 + " دقیقه</span>";

                        var tempId = sumDistance[i].id;
                        polygon = polygonGroup._layers[tempId];
                        polygon._popup._content = sumDistance[i].pString;

                        for (var j = 0; j < PolygonWithMarkers.length; j++) {
                            if (PolygonWithMarkers[j][0] == sumDistance[i].id) {
                                PolygonWithMarkers[j][2] = sumDistance[i].pString;
                            }
                        }

                    }


                },
                error: function () {
                    alert("Error");
                }
            })

        },
        error: function () {
            alert("Error");
        }

    });
}


$('#CloseModal').on('click', function () {
    $('#myModal').modal('hide');
})

$('#AppendBtn').on('click', function () {
    var text = $('#addPopup').val();
    str += "<br/>" + text;
    polygonGroup._layers[PolygonOnEdit]._popup._content = str;
    $('#myModal').modal('hide');
})

$('#lastPolygon').on('click', LastPolygon)

function LastPolygon() {
    var k = Object.keys(polygonGroup._layers).pop();
    polygon = polygonGroup._layers[k];
    delete polygonGroup._layers[k];
    for (var i = 0; i < polygon._latlngs[0].length; i++) {
        latlngs[i] = [polygon._latlngs[0][i].lat, polygon._latlngs[0][i].lng];
        index++;
    }
    for (var i = 0; i < PolygonWithMarkers.length; i++) {
        if (PolygonWithMarkers[i][0] == k) {
            for (var j = 0; j < PolygonWithMarkers[i][1].length; j++) {
                MarkerPolygonId[j] = PolygonWithMarkers[i][1][j];
            }
        }
    }
}

$('#autoPolygon').on('click', autoPolygon);

function autoPolygon() {

    var styleArray = [];
    for (var i = 0; i < Locations.length; i++) {
        var have = false;
        for (var j = 0; j < styleArray.length; j++) {
            if (styleArray[j] == Locations[i][2])
                have = true;
        }
        if (have == false)
            styleArray.push(Locations[i][2]);
    }
    Delete();
    for (var i = 0; i < styleArray.length; i++) {
        for (var j = 0; j < Locations.length; j++) {
            if (styleArray[i] == Locations[j][2]) {
                $('#error').text("");
                if (polygon != null) {
                    map.removeLayer(polygon);
                }
                latlngs[index] = [Locations[j][0], Locations[j][1]]
                polygon = L.polygon(latlngs, {
                    color: PolygonColor[(Locations[j][2] - 1)],
                    contextmenu: true,
                    contextmenuInheritItems: false,
                    contextmenuItems: [{
                        text: "اضافه کردن توضیحات",
                        callback: function (polygon) {
                            PolygonOnEdit = polygon.relatedTarget._leaflet_id;
                            $('#myModal').modal('show');
                            $('#popupText span').remove();
                            $('#popupText').append(str);
                        }
                    }, {
                        text: "ویرایش پالیگان",
                        callback: function (polygon) {
                            nextPolygon();
                            var k = polygon.relatedTarget._leaflet_id;
                            polygon = polygonGroup._layers[k];
                            delete polygonGroup._layers[k];
                            for (var i = 0; i < polygon._latlngs[0].length; i++) {
                                latlngs[i] = [polygon._latlngs[0][i].lat, polygon._latlngs[0][i].lng];
                                index = i + 1;
                            }
                            for (var i = 0; i < PolygonWithMarkers.length; i++) {
                                if (PolygonWithMarkers[i][0] == k) {
                                    for (var j = 0; j < PolygonWithMarkers[i][1].length; j++) {
                                        MarkerPolygonId[j] = PolygonWithMarkers[i][1][j];
                                    }
                                }
                            }
                        }
                    }]
                }).addTo(map).bindPopup(sumPolygonKmTrafic);
                polygonGroup.eachLayer(function (layer) {
                    layer.addTo(map);
                });
                MarkerPolygonId[index] = Locations[j][4];
                index++;
            }

        }
        for (var j = 0; j < latlngs.length - 1; j++) {
            DistancePolygon(latlngs[j], latlngs[j + 1], polygon._leaflet_id);
        }
        nextPolygon();
    }

}