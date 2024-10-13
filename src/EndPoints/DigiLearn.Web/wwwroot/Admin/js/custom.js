

$("#CategoryId").change(function () {
    var currentId = $(this).val();
    $.ajax({
        url: `/admin/products/Index/LoadChildrenCategories?parentId=${currentId}`,
        type: "get"
    }).done(function (data) {
        $("#SubCategoryId").html(data);

    });
});


$("#SubCategoryId").change(function () {
    var currentId = $(this).val();
    $.ajax({
        url: `/admin/products/Index/LoadChildrenCategories?parentId=${currentId}`,
        type: "get"
    }).done(function (data) {
        $("#FirstSubCategoryId").html(data);

    });
});


function AddRow() {
    var count = $("#rowCount").val();
    for (var i = 0; i < count; i++) {

        $("#table-body").append(
            "<tr>" +
            "<td><input autocomplete='off' type='text' name='Keys' class='form-control' /></td>" +
            "<td><input autocomplete='off' type='text' name='Values'  class='form-control' /></td>" +
            "</tr>"
        );
    }
}

function changePageId(pageId) {
    var url = new URL(window.location.href);
    var search_params = url.searchParams;
    search_params.set('filterParams.pageId', pageId);
    url.search = search_params.toString();
    var new_url = url.toString();
    window.location.replace(new_url);
}