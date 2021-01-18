var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#btnContinue').on('click', function () {
            window.location.href = "/";
        });
        $('#btnOrder').off('click').on('click', function () {
            window.location.href = '/dat-hang';
        });
        $('#btnUpdate').on('click', function () {
            var listProduct = $('.txtQuantity');
            var listItemsInCart = [];
            $.each(listProduct, function (i, item) {
                listItemsInCart.push({
                    Quantity: $(item).val(),
                    Product: {
                        product_id: $(item).data('id')
                    }
                });
            });
            $.ajax({
                url: '/Cart/Update',
                data: { cartModel: JSON.stringify(listItemsInCart) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status === true) {
                        window.location.href = '/gio-hang';
                    }
                }
            });
        });
        $('.btn-delete').on('click', function () {
            $.ajax({
                url: 'Cart/Delete',
                data: { product_id: $(this).data('id') },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status === true) {
                        window.location.href = '/gio-hang';
                    }
                }
            });
        });
    }
};
cart.init();
