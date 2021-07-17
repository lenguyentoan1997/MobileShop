var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#btnContinue').off('click').on('click', function () {
            window.location.href = "/";
        });
        $('#btnUpdate').off('click').on('click', function () {
            var listProduct = $('.txtQuantity');
            var cartList = [];
            $.each(listProduct, function (i, item) {
                /*The object here must be the same as the object in Cart to be mapping*/
                cartList.push({
                    Quantity: $(item).val(),
                    SanPham: {
                        MaSanPham: $(item).data('id')
                    }
                });
            });
            $.ajax({
                url: '/Cart/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    //return results from CartController
                    if (res.status == true) {
                        window.location.href = "/Cart";
                    }
                }
            })
        });
    }
}
cart.init();