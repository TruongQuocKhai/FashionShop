var order = {
    init: function () {
        order.loadProvince();
        order.orderEvent();
    },
    orderEvent: function () {
        $('#ddlProvince').off('change').on('change', function () {
            var nameProvince = $(this).val();
            if (nameProvince != '') {
                order.loadDistrict(nameProvince);
            }
            else {
                $('#ddlDistrict').html('');
            }
        });
        $('#ddlDistrict').off('change').on('change', function () {
            var nameDistrict = $(this).val();
            if (nameDistrict != '') {
                order.loadWard(nameDistrict);
            }
            else {
                $('#ddlWard').html('');
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
                    var htmlSelect = '<option value="">--Chọn tỉnh thành--</option>';
                    var data = response.data;
                    $.each(data, function (index, value) {
                        htmlSelect += '<option value="' + value.Name + '">' + value.Name + '</option>'
                    });
                    $('#ddlProvince').html(htmlSelect);
                }
            }
        })
    },
    loadDistrict: function (name) {
        $.ajax({
            url: '/Cart/LoadDistrict',
            type: 'POST',
            data: { provinceName: name },
            dataType: 'json',
            success: function (response) {
                if (response.status === true) {
                    var htmlSelect = '<option value="">--Chọn quận/huyện--</option>';
                    var data = response.data;
                    $.each(data, function (index, value) {
                        htmlSelect += '<option value="' + value.Name + '">' + value.Name + '</option>'
                    });
                    $('#ddlDistrict').html(htmlSelect);
                }
            }
        })
    },
    loadWard: function (name) {
        $.ajax({
            url: '/Cart/LoadWard',
            type: 'POST',
            data: { districtName: name },
            dataType: 'json',
            success: function (response) {
                if (response === true) {
                    var htmlSelect = '<option value"">--Chọn phường/xã--</option>';
                    var data = response.data;
                    $.each(data, function (index, value) {
                        htmlSelect += '<option value="' + value.Name + '">' + value.Name + '</option>'
                    });
                    $('#ddlWard').html(htmlSelect);
                }
            }
        });
    }
}
order.init();