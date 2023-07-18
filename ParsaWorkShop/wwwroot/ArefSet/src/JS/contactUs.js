
// Creating map options
// var mapOptions = {
//    center: [36.55652, 53.03909],
//    zoom: 20
// }

// // var marker = L.marker([51.5, -0.09]).addTo(map);

// // Creating a map object
// var map = new L.map('map', mapOptions);

// // Creating a Layer object
// var layer = new L.TileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png');

// // Adding layer to the map
// map.addLayer(layer);
// ---------------------------------------------------------------
var map = L.map("map").setView([36.55652, 53.03909], 17);
L.tileLayer("https://tile.openstreetmap.org/{z}/{x}/{y}.png", {
  maxZoom: 19,
  attribution:
    '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>',
}).addTo(map);
var marker = L.marker([36.55652, 53.03909]).addTo(map);
function loadedPage() {
    // alert("Page is loaded");
    document.querySelector(".leaflet-control-attribution").remove();
}
// document.querySelector(".leaflet-control-attribution").remove();