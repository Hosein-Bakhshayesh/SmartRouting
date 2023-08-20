$('#AddBtn').on("click", function () {
    $('#AddModal').modal('show');
})

$('#Cancel').on("click", function () {
    $.ajax({
        type: "POST",
        url: '/Admin/NavyInfo?handler=Cancel',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    })
});