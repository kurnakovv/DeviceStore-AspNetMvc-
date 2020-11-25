$('.device-block').hover(function () {
    $(this).find('.device-item').addClass('flip')
},

function () {
    $(this).find('.device-item').removeClass('flip')
})