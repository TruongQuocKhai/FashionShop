var order = {
    init: function () {
        order.loadProvince();
        order.orderEvent();
    },

    orderEvent: function () {
        $('#ddlProvince').off('change').on('change', function () {
            var id = $(this).val(); // id được chọn
            if (id != '') {
                order.loadDistrict(parseInt(id));
            }
            else {    // id Provine null -> District null
                $('#ddlDistrict').html('');
            }
        });
    },

    loadProvince: function () {
        $.ajax({
            url: '/Cart/LoadProvince',
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status === true) {
                    var htmlSelect = '<option value="">--Chọn tỉnh/thành phố--</option>';
                    var data = response.data;
                    $.each(data, function (index, value) {
                        htmlSelect += '<option value="' + value.Id + '">' + value.Name + '</option>'
                    });
                    $('#ddlProvince').html(htmlSelect);
                }
            }
        })
    },
    loadDistrict: function (id) {
        $.ajax({
            url: '/Cart/LoadDistrict',
            type: 'POST',
            data: { provinceId: id }, // tham số truyền vào
            dataType: 'json',
            succes: function (response) {
                if (response.status === true) {
                    var htmlSelect = '<option value="">--Chọn quận/huyện--</option>';
                    var data = response.data;
                    $.each(data, function (index, value) { // vòng lặp foreach của jquery
                        htmlSelect = '<option value="' + value.Id + '">' + value.Name + '</option>'
                    });
                    $('#ddlDistrict').html(htmlSelect);
                }
            }
        });
    },
}
order.init();