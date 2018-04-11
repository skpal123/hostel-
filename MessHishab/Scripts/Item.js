function getMemberListjs(url) {
    var memberlist = [];
    $.ajax({
        url: url,
        method: 'get',
        contentType: 'application/json',
        success: function (data) {
            memberList = data;
            $(data).each(function(index, value) {
                $('#MemberId').append('<option value="' + value.Id + '">' + value.Name + '</option>');
            });
        },
        error: function (error) {
            alert(error.error);
        }
    })

}
function getBajarListByDateAndMember(memberid,date,url) {
    $.ajax({
        url: url,
        async:false,
        method: 'get',
        contentType: 'application/json',
        data:{date:date,memberId:memberid},
        success: function (data) {
            $(data).each(function(index,value) {
                $('tr').each(function (index, row) {
                    if ($(row).attr('id') == value.ItemId)
                    {
                        $('input', row).each(function (index, collum) {
                            if ($(collum).attr('id') == 'quantity-' + value.ItemId) $('#quantity-' + value.ItemId).val(value.Quantity);
                            if ($(collum).attr('id') == 'price-' + value.ItemId) $('#price-' + value.ItemId).val(value.Price);
                        })
                    }
                })
            })
        },
        error: function (error) {
            alert(error.error);
        }
    })
}