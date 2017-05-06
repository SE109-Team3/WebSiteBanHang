$(document).ready(function() {
	
	// hàm chạy
	var run=function chay()
	{
		// lấy ra thành phần đầu tiên
	    var first = $('.list').find('.item-product:first');
		// lấy ra độ dài của thành phần
		var first_long = first.css('height');	
		var long_f = first_long.substr(0, first_long.indexOf("px"));

		// lấy ra margin hiện tại
		var first_top = first.css('margin-top');
		var top_f = first_top.substr(0, first_top.indexOf("px"));
	   
		// kiểm tra height có lớn hơn margin hay không
		if(long_f>Math.abs(top_f)-10)
		{
			first.css('margin-top',(top_f-1)+"px");
			//alert(var first_margin=first.css('margin-top'));
		}
		else
		{
			
			// lấy ra thành phần cha
		    var parent = $('.list');
		    parent.find('.item-product:first').remove();
		    // tạo phần tử mới ở cuối dãy
		    var str = "<div class='item-product'>" + first.html() + "</div>";
		    parent.append(str);

		}
	}
	var parent = $('.list');
	var f_run=setInterval(run,1000/60);
	parent.mouseenter(function(){
		clearInterval(f_run);
		}
	);
	parent.mouseleave(function(){
		f_run=setInterval(run,1000/60);
		}
	);
	
});