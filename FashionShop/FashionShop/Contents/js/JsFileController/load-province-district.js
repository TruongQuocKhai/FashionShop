var order = {
    init: function () {
        order.loadProvince();
    },
    loadProvince: function () {
        var html = '';
        $.ajax({
            url: '/Cart/LoadProvince',
            type: "POST",
            dataType: "json",
            success: function (response) {
                if (response.status === true) {
                    //var html = '<option value="">--Chọn tỉnh thành--</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.Id + '">' + item.Name + '</option>'
                    });
                    $('#ddlProvince').html(html);
                }
            }
        })
    },
}
order.init();