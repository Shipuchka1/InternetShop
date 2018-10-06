
function MenuItemClick( elem) {

    var clickable = $(elem).attr('data-itemcl');
    if (clickable === 'false') {
        var url = $("#UlCategory").attr('data-url');
        var data = "id=" + $(elem).attr('data-id');
        $(elem).attr('data-itemcl', 'true');
        $.post(url, data, function (response) {
            $(elem).after(response);
        });
        $("#goodsList").empty();
        var url2 = $("#goodsList").attr('data-url');
        $.post(url2, data, function (response) {
            $("#goodsList").append(response);

        });

                }
    else {
        $(elem).attr('data-itemcl', 'false');
        $('.res' + $(elem).attr('data-id')).hide(1000);
        $('.res' + $(elem).attr('data-id')).remove();
    }
}