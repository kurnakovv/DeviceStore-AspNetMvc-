$(function () {
    $.ajaxSetup({ cache: false });
    $(".deviceEdit").click(function (e) {

        e.preventDefault();
        $.get(this.href, function (data) {
            $('#dialogContent').html(data);
            $('#modelDialog').modal('show');
        });
    });
})

$(function () {
    $.ajaxSetup({ cache: false });
    $(".deviceDelete").click(function (d) {

        d.preventDefault();
        $.get(this.href, function (data) {
            $('#dialogContent').html(data);
            $('#modelDialog').modal('show');
        });
    });
})