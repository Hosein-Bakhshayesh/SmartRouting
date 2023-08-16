function CloseNav() {
    document.getElementById("navbar").style.width = "0px";
    document.getElementById("main").style.marginRight = "3%";
    document.getElementById("openIcon").style.visibility = "visible";
}
function OpenNav() {
    document.getElementById("navbar").style.width = "18%";
    document.getElementById("main").style.marginRight = "20%";
    document.getElementById("openIcon").style.visibility = "hidden";
}

$('#AddNavyTypeBtn').on("click", function () {
    $('#AddNavyTypeModal').modal('show');
})


