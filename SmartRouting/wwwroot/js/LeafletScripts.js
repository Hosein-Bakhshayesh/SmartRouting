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

var polygon = null;
var DossierDetail;
const polygonLatlngs = [];
var index = 0;
const DriversID = [];
var polygonGroup = L.featureGroup();
var MarkerPolygonId = [];
var str;
var sumDistance = [];
var sumPolygonKmTrafic = 0;
var sumPolygonTimeTrafic = 0;
var sumPolygonKm = 0;
var sumPolygonTime = 0;



var map = L.map('map');

L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
}).addTo(map);

window.onload = function () {
    $.ajax({
        type: 'Get',
        url: "/Map?handler=Addresses",
        contentType: "application/json",
        dataType: "json",
        success: function (res) {
            DossierDetail = res;
            for (var i = 0; i < res.length; i++) {
                if (i == 0) {
                    map.setView([res[i].dossierDetailLength, res[i].dossierDetailWidth], 10);
                }
                L.marker([res[i].dossierDetailLength, res[i].dossierDetailWidth]).addTo(map)
                    .bindPopup("<b>آدرس : </b>" + res[i].dossierDetailCaddress + "<br/><b>مشتری : </b>" + res[i].dossierDetailCustomer);
                var isExist = DriversID.includes(res[i].dossierDetailDriverId)
                if (isExist == false) {
                    DriversID.push(res[i].dossierDetailDriverId);
                }
            }

            for (var i = 0; i < DriversID.length; i++) {
                for (var j = 0; j < res.length; j++) {
                    if (res[j].dossierDetailDriverId == DriversID[i]) {
                        if (polygon != null) {
                            map.removeLayer(polygon);
                        }
                        polygonLatlngs[index] = [res[j].dossierDetailLength, res[j].dossierDetailWidth];
                        if (index > 0) {
                            DistancePolygon(polygonLatlngs[index - 1], polygonLatlngs[index], polygon._leaflet_id);
                        }
                        polygon = L.polygon(polygonLatlngs, {
                            contextmenu: true,
                            contextmenuInheritItems: false,
                            contextmenuItems: [{
                                text: "اضافه کردن توضیحات",
                            }]
                        }).addTo(map);
                        polygonGroup.eachLayer(function (layer) {
                            layer.addTo(map);
                        });
                        index++;
                    }
                }
                nextPolygon();

            }

        }
    })
};

function nextPolygon() {
    if (index > 1) {
        polygon.addTo(polygonGroup);
        Delete();
    } else {
        $('#error').text("");
        $('#error').text("پالیگان باید حداقل شامل دو آدرس باشد");
    }


}
function Delete() {
    while (polygonLatlngs.length > 0) {
        polygonLatlngs.pop();
    }
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
        type: "POST",
        url: "/Map?handler=FindDistanceTrafic",
        data: js,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("RequestVerificationToken",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (res) {
            $.ajax({
                type: "POST",
                url: "/Map?handler=FindDistance",
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