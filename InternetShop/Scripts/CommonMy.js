
var percentOff = 0.0;
function cartAdd(id) {
    var data = "id=" + id;
    $.post(addToCartUrl, data, function (response) {
        $("#cart-total").text('('+response+')');
    });
}



function CartClick() {
    window.location.href = GoToCartUrl;
}

function IncDec(id, count) {
    var oldCount = $("#GoodCount" + id).text();
    var newCount = Number(oldCount) + Number(count);
    if (newCount < 0)
        DeleteFromCart(id)
    else {
        var data = "GoodId=" + id + "&newCount=" + newCount;
        $.post(SetCountUrl, data, function (response) {
            if (response === "success") {
                $("#GoodCount" + id).text(newCount);
                cartAdd(0);
                reCalc();
            }
        });
    }
}

function reCalc() {
    var arr = document.getElementsByClassName("price");
    var sum = 0;
    for (var i = 0; i < arr.length; i++) {
        var id = $(arr[i]).attr("data-idGood");
        sum += Number($(arr[i]).text()) * Number($("#GoodCount" + id).text());
    }
    $("#totalCost").text(sum);
    $("#totalCostWithPromo").text(sum*(100-percentOff)/100);

}

function DeleteFromCart(id) {
    var data = "GoodId=" + id + "&newCount=" + -1;
    $.post(SetCountUrl, data, function (response) {
        if (response === "success") {
            $("#tr" + id).remove();
            cartAdd(0);
            reCalc();
        }
    });
}

function LegalCheck1() {
    var data;
    if ($("#LegalCheck").is(':checked')) {
       data = "arg=entity"
    }
    else data = "arg = individual";
    $.post(EntityOrIndividualUrl, data, function (response) {

        $("#changeForm").html(response);
        
    });
}

function getCookie(name) {
    var matches = document.cookie.match(new RegExp(
        "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}

function PromoClick() {
    var data = "keyword="+$("#PromoInput").val();
    $.post(PromoUrl, data, function (response) {
        var arr = response.split(';');
        if (arr[0] == 'success') {
            percentOff = arr[1];
            reCalc();
        }
        else {
            $('#ErrorMessage').text('Промокод не действителен');
        }

    });
}