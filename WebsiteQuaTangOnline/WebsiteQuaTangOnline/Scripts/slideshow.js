$(document).ready(function () {
    $('#SlideShow img:gt(0)').hide();
    var ArrImage = new Array('img1', 'img2', 'img3');
    var cur = 0;
    setInterval(function () {
        $('#' + ArrImage[cur]).fadeOut();
        if ((cur++) >= ArrImage.length - 1) {
            cur = 0;
        }
        $('#' + ArrImage[cur]).fadeIn();
    },
      3000);
    $('#Back').click(function () {
        $('#' + ArrImage[cur]).fadeOut();
        cur--;
        if (cur < 0) {
            cur = ArrImage.length - 1;
        }
        $('#' + ArrImage[cur]).fadeIn();
    });
    $('#Next').click(function () {
        $('#' + ArrImage[cur]).fadeOut();
        if ((cur++) >= ArrImage.length - 1) {
            cur = 0;
        }
        $('#' + ArrImage[cur]).fadeIn();
    });

});