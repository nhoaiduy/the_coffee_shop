$('.format_currency').each(function () {
    var item = $(this).text();
    var num = Number(item).toLocaleString('it-IT', { style: 'currency', currency: 'VND' });

    $(this).text(num);
});