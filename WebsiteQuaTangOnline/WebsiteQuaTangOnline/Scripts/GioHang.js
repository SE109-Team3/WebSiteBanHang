function update()
{

    $('.giohang').each(function () {
        // kiểm tra số lượng có bằng giá trị hay không
        if ($(this).attr('data-number') != $(this).val()) {
            $.get("/Home/UpdateProduct/",
                { id: $(this).attr("name"), soluong: $(this).val() },
                function () {
                    window.location='/Home/Cart/'
                });
        }
    });
}