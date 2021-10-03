$('.rating').ready(function () {
    var rating = $('.rating');
    for (var i = 0; i < rating.length; i++) {
        var averagePoint = parseInt(rating[i].getAttribute('data-average-point'));
        var productId = rating[i].getAttribute('data-product-id');
        for (var j = 0; j < averagePoint; j++) {
            $('.average-star-rate-' + productId + "-" + j).addClass("checked");
        }
    }
});