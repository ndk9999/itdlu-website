function print_preview(id) {
    var myWindow = window.open("/Article/Print/" + id, "_blank", "toolbar=no, scrollbars=yes, resizable=yes, top=0, left=50, width=768, height=650");
}
/*jQuery time*/
$(document).ready(function () {
    startclock_vi_VN();

    $("#accordian h3").click(function () {
        //slide up all the link lists
        $("#accordian ul ul").slideUp();
        //slide down the link list below the h3 clicked - only if its closed
        if (!$(this).next().is(":visible")) {
            $(this).next().slideDown();
        }
    })
})

$(document).ready(function () {
    jQuery('.SITE_NAME').each(function () {
        $.ColorLife.jsonGetSettingValue('SITE_NAME', '.SITE_NAME');
    });
    jQuery('.SITE_PHONE').each(function () {
        $.ColorLife.jsonGetSettingValue('SITE_PHONE', '.SITE_PHONE');
    });
    jQuery('.SITE_EMAIL').each(function () {
        $.ColorLife.jsonGetSettingValue('SITE_EMAIL', '.SITE_EMAIL');
    });
    jQuery('.SITE_ADDRESS').each(function () {
        $.ColorLife.jsonGetSettingValue('SITE_ADDRESS', '.SITE_ADDRESS');
    });
    jQuery('.SITE_URL').each(function () {
        $.ColorLife.jsonGetSettingValue('SITE_URL', '.SITE_URL');
    });
    jQuery('.SITE_FAX').each(function () {
        $.ColorLife.jsonGetSettingValue('SITE_FAX', '.SITE_FAX');
    });
    $.ColorLife.jsonGetDataObject('SITE_LOGO', 'Setting', function (handler) {
        $(".SITE_LOGO").attr('src', handler.Value);
    });
    if ($(".topmenu").length) {
        $.ColorLife.jsonGetDataList('Top', 'GetAllMenuByFlag', function (handler) {
            var url = '<ul>';
            $.each(handler, function (i, item) {
                url += '<li><a href="' + item.Url + '" rel="tooltip" title="' + item.Name + '">' + item.Name + '</a></li>';
            });
            url += '</ul>';
            $(".topmenu").html(url);
        });
    }
    if ($("#accordion").length) {
        $.ColorLife.jsonGetDataObject('0', 'GetMainDLUDeptMenu', function (handler) {
            $("#accordion").html(handler.Data);
           
        });
    }
    
    if ($(".officemenu").length) {
        $.ColorLife.jsonGetDataObject('0', 'GetDepartmentOnHomePage', function (handler) {
            var html = '<div class="title">';
            html += 'CÁC ĐƠN VỊ THÀNH VIÊN VÀ TRỰC THUỘC';
            html += '</div>';
            html += handler.Data;


            $(".officemenu").html(html);
            var script = document.createElement("script");
            script.type = "text/javascript";
            script.text = handler.Message;               // use this for inline script       
            script.text += 'jQuery(document).ready(function($){jkmegamenu.render($) })';
            $(".officemenu").append(script);
        });
    }
    if ($(".dept-menu").length) {
        $.ColorLife.jsonGetDataObject('0', 'GetDepartmentOnHomePage2', function (handler) {
            var html = '';
            html += handler.Data;


            $(".dept-menu").html(html);
           
        });
    }
    if ($(".mytabs").length) {
        $.ColorLife.jsonGetDataObject('0', 'GetDepartmentOnHomePageTabs', function (handler) {
            var html = '';
            html += handler.Data;


            $(".mytabs").html(html);
            $(".mytabs ul.tabs li:first").addClass("current");
            $(".mytabs .tab-content:first").addClass("current");
            $('.mytabs ul.tabs li').click(function () {
                var tab_id = $(this).attr('data-tab');

                $('.mytabs ul.tabs li').removeClass('current');
                $('.mytabs .tab-content').removeClass('current');

                $(this).addClass('current');
                $("#" + tab_id).addClass('current');

                
            })

            $('.tab-content').niceScroll({
                autohidemode: "true",
                cursorborderradius: "0px",
                background: "#E5E9E7",
                cursorwidth: "5px", cursorcolor: "#999999"
            });
        });

       
    }
    $.ColorLife.jsonGetDataList('Footer1', 'GetAllMenuByFlag', function (handler) {
        var url = '<ul class="tblmenubottom">';
        $.each(handler, function (i, item) {
            url += '<li><a href="' + item.Url + '" rel="tooltip" title="' + item.Name + '">' + item.Name + '</a></li>';
        });
        url += '</ul>';
        $("#Footer1").html(url);
    });

    // 
    if ($("#myslider").length) {
        $.ColorLife.jsonGetDataList('SlideShow', 'GetSlider', function (handler) {
            var div = ' <div id="carousel-example-generic" class="carousel slide" data-ride="carousel">';
            var ol = '<ol class="carousel-indicators">';
            var div2 = ' <div class="carousel-inner" role="listbox">';
            $.each(handler, function (i, item) {
                ol += '<li data-target="#carousel-example-generic" data-slide-to="' + i + '" class=""></li>';
            });
            ol += '</ol>';
            ol += div2;

            $.each(handler, function (i, item) {
                ol += ' <div class="item ">';
                ol += ' <img src="' + item.ImageUrl + '" class="img-responsive"  />';
                ol += '<div class="carousel-caption">';
                ol += item.Name;
                ol += ' </div>';
                ol += ' </div>';
            });
            ol += '</div>';
            div += ol + '</div>';

            $("#myslider").html(div);
            jQuery(".carousel-indicators > li:first").addClass("active");
            jQuery(".carousel-inner > .item:first").addClass("active");
        });
    }
    if ($(".bannerlink").length) {
        $.ColorLife.jsonGetDataList('Left_Banner', 'GetImageBanner', function (handler) {
            $(".bannerlink").html('Loading...');
            var html = '';
            $.each(handler, function (i, item) {
                html += '<a href="' + item.Link + '" target="_blank">';
                html += '<img src="' + item.Image + '" class="img-responsive" width="100%" />';
                html += '</a>';
            });
            $(".bannerlink").html(html);
        });
    }
    if ($("#logodoitac").length) {
        $.ColorLife.jsonGetDataList('Bottom_Banner', 'GetImageBanner', function (handler) {
            $("#logodoitac").html('Loading...');
            var html = '';
            html += ' <ul class="bxslider">';
            $.each(handler, function (i, item) {
                html += ' <li>';
                html += ' <div class="item_slider">';
                html += ' <div class="thumb_image">';
                html += '<a href="' + item.Link + '" target="_blank">';
                html += '<img src="' + item.Image + '" class="img-responsive"/>';
                html += '</a>';
                html += ' </div>';
                html += ' </div>';
                html += ' </li>';
            });
            html += '</ul>';
            $("#logodoitac").html(html);
            $('.bxslider').bxSlider({
                nextSelector: '#slider-next',
                prevSelector: '#slider-prev',
                nextText: '<i class="fa fa-chevron-right"></i>',
                prevText: '<i class="fa fa-chevron-left"></i>',

                minSlides: 10,
                maxSlides: 10,
                slideWidth: 245,
                slideMargin: 0,
                auto: true,
                autoControls: true,
                pager: false
            });
        });
    }
    if ($("#newshome").length) {
        $.ColorLife.jsonGetDataList(25, 'GetNewsFrontPage', function (handler) {
            $("#newshome").html('Loading...');
            var html = '';
            var url = ' <ul id="demo">';
            $.each(handler, function (i, item) {
                url += '<li> <div class="media">';
                url += '<a class="media-left " href="/tin-tuc/'+item.Alias+'/' + item.ContentID + '"">';
                url += '<img src="' + item.Image + '" alt="' + item.Name + '" width="120" class="thumbnail">';
                url += '</a>';
                url += ' <div class="media-body">';
                url += '   <h5 class="media-heading">';
                url += '<a class="home-title-a" href="/tin-tuc/'+item.Alias+'/' + item.ContentID + '">' + item.Name + '</a>';
                url += '</h5>';
                url += item.Description;

                url += '</div>';
                url += '</div>';
                url += '</li>';
            });
            url += '</ul>';
            html += url;
            html += '<div class="holder"></div>';

            $("#newshome").html(html);
            $("#newshome").addClass('main-center_children');
            $("div.holder").jPages({
                containerID: "demo",
                perPage: 5,
                previous: '',
                next: '',

            });
        });
    }
    if ($(".divthongbao").length) {
        $.ColorLife.jsonGetDataList(50, 'GetTop10NewNotice', function (handler) {
            $(".divthongbao").html('Loading...');
            var html = ''; //'  <div class="main-title"> <a href="/thong-bao">THÔNG BÁO MỚI</a></div>';
            html += '<div class="top10thongbao">';
            html += ' <ul>';
            $.each(handler, function (i, item) {
                html += '<li> <div class="media">';
                html += '<a href="/thong-bao/'+item.Alias+'/'+item.NoticeID+'">';
                html += item.Name;
                html += '</a>';
                if (item.IsNew) {
                    html += '<img border="0" width="31px" height="15px" src="/content/images/icon_new1.gif">';
                }
                html += '</li>';
            });
            html += '</ul>';
            html += '</div>';
            $(".divthongbao").html(html);
            $(".top10thongbao").niceScroll( );
        });
    }
    if ($(".divthongbaonoibat").length) {
        $.ColorLife.jsonGetDataList(20, 'GetTopNoticePined', function (handler) {
            $(".divthongbaonoibat").html('Loading...');
            var html = ''; //'  <div class="main-title"> <a href="/thong-bao">THÔNG BÁO MỚI</a></div>';

            html += ' <ol>';
            $.each(handler, function (i, item) {
                html += '<li> <div class="media">';
                html += '<a href="/thong-bao/' + item.Alias + '/' + item.NoticeID + '">';
                html += item.Name;
                html += '</a>';
                if (item.IsNew) {
                    html += '<img border="0" width="31px" height="15px" src="/content/images/icon_new1.gif">';
                }
                html += '</br>Đơn vị: ' + item.CreatedByID;
                html += ' <small><i class="fa fa-calendar"></i> Ngày: ' + item.GetDateTime + '</small>';
                html += '</li>';
            });
            html += '</ol>';

            $(".divthongbaonoibat").html(html);

        });
    }
    if ($(".divvanbanmoinhat").length) {
        $.ColorLife.jsonGetDataList(20, 'GetLastestDocument', function (handler) {
            $(".divvanbanmoinhat").html('Loading...');
            var html = ''; //'  <div class="main-title"> <a href="/thong-bao">THÔNG BÁO MỚI</a></div>';

            html += ' <ol>';
            $.each(handler, function (i, item) {
                html += '<li> <div class="media">';
                html += '<b>' + item.DocType.Name + '</b> ';
                html += '<a href="/van-ban/' + item.Alias + '/' + item.DocumentID + '">';
                html += item.Name;
                html += '</a>';
                if (item.IsNew) {
                    html += '<img border="0" width="31px" height="15px" src="/content/images/icon_new1.gif">';
                }


                html += '</li>';
            });
            html += '</ol>';

            $(".divvanbanmoinhat").html(html);

        });
    }
    if ($("#gocbaochi").length) {
        var id = $("#gocbaochi").attr('data-id');

        $.ColorLife.jsonGetDataListContentByCategoryID(id, 30, function (handler) {
            $("#gocbaochi").html('Loading...');
            var id = $("#gocbaochi").attr('data-id');
            var html = '';
            html += '  <ul class="goc-bao-chi">';
            //    html += '<h4>';
            //  html += ' <a href="/danh-muc/thong-tin-bao-chi/' + id + '">Thông tin từ báo chí</a>';
            // html += '</h4><div class="stripe-line"></div>';
            //   html += '</div>';
            //  html += ' <div class="widget-container">';
            //  html += ' <ul>';
            $.each(handler, function (i, item) {
                html += '<li>';
                html += '<b>' + item.Source + '</b>';
                html += '<a class="media-left " href="/tin-tuc/' + item.Alias + '/' + item.ContentID + '"">';
                html += item.Name;
                html += '</a>';

                html += '</li>';
            });
            html += '</ul>';
            $("#gocbaochi").html('Loading...');
            $("#gocbaochi").html(html);
            $('.gocbaochi').niceScroll({
                autohidemode: "true",
                cursorborderradius: "0px",
                background: "#E5E9E7",
                cursorwidth: "5px", cursorcolor: "#999999"
            });
        });
    }
    if ($("#list-myimage").length) {
        $.ColorLife.jsonGetDataList(12, 'GetPhotoHomePage', function (handler) {
            $("#list-myimage").html('Loading...');
            var html = '';
          
            html += ' <ul>';
            $.each(handler, function (i, item) {
                html += '<li>';
                html += '<a href="' + item.Image + '"  class="group4 ">';
                html += '<img src="'+item.Image+'" / alt='+item.Caption+'>';
                html += '</a>';
               
                html += '</li>';
            });
            html += '</ul>';
            
            $("#list-myimage").html(html);
            $(".group4").colorbox({ rel: 'group4', slideshow: true });
        });
    }
    if ($("#list-hinhanhhoatdong").length) {
        $.ColorLife.jsonGetDataList(9, 'GetPhotoHomePage', function (handler) {
            $("#list-myimage").html('Loading...');
            var html = '';
            html += ' <h3 class="page-header"> HÌNH ẢNH HOẠT ĐỘNG</h3>';
            html += '   <div class="flexslider">';
            html += '  <ul class="slides">';
            $.each(handler, function (i, item) {
                html += '<li>';
                html += '<a href="' + item.Image + '"  class="group4 img-responsive">';
                html += '<img src="' + item.Image + '" / alt=' + item.Caption + '>';
                html += '</a>';

                html += '</li>';
            });
            html += '</ul>';
            html += '</div>';
            $("#list-hinhanhhoatdong").html(html);
            $('.flexslider').flexslider({
                animation: "slide",
                prevText: '<i class="fa fa-angle-left icon-large"></i>',
                nextText: '<i class="fa fa-angle-right icon-large"></i>',
            });
            $(".group4").colorbox({ rel: 'group4', slideshow: true });
        });
    }
    if ($("#list-doitacdhdl").length) {
        $.ColorLife.jsonGetDataList('Bottom_Banner', 'GetImageBanner', function (handler) {
            $("#list-doitacdhdl").html('Loading...');

            var html = '';
            html += ' <h4 class="title-video">ĐỐI TÁC ĐHĐL</h4>';
            html += ' <ul class="list-doitacdhdl">';
            $.each(handler, function (i, item) {
                html += ' <li>';
                html += ' <div class="item_slider">';
                html += ' <div class="thumb_image">';
                html += '<a href="' + item.Link + '" target="_blank">';
                html += '<img src="' + item.Image + '" class="img-responsive"/>';
                html += '</a>';
                html += ' </div>';
                html += ' </div>';
                html += ' </li>';
            });
            html += '</ul>';
            $("#list-doitacdhdl").html(html);
        });
    }
 
    
    if ($('.list-myvideo-home').length > 0) {


        $.ColorLife.jsonGetDataList(10, 'GetVideoHomePage', function (handler) {
            $(".list-myvideo-home").html('Loading...');
            $('#video-item').html('<iframe class="embed-responsive-item" src="//www.youtube.com/embed/' + handler[0].VideoUrl + '?rel=0&amp;showinfo=0" frameborder="0" allowfullscreen></iframe>');
            var html = '';

            html += ' <ul>';
            $.each(handler, function (i, item) {
                html += '<li>';
                html += "<a href='javascript:void(0);' id='" + item.VideoUrl + "' data-videourl='" + item.VideoUrl + "' class='videourl'>";
                html += item.Name;
                html += '</a>';

                html += '</li>';
            });
            html += '</ul>';

            $(".list-myvideo-home").html(html);
            
            $('.list-myvideo-home').niceScroll({
                autohidemode: "true",
                cursorborderradius: "0px",
                background: "#E5E9E7",
                cursorwidth: "5px", cursorcolor: "#999999"
            });

            $(".videourl").on("click", function (e) {
                id = e.target.id;
               // alert(id);
                var html = '<iframe class="embed-responsive-item" src="//www.youtube.com/embed/' + id + '?rel=0&amp;showinfo=0&autoplay=1" frameborder="0" allowfullscreen></iframe>';
                $('#video-item').html(html);
            });

        });
         
    }

    
    //$(".top10thongbao").niceScroll({
    //    autohidemode: "true",
    //    cursorborderradius: "0px",
    //    background: "#E5E9E7",
    //    cursorwidth: "5px", cursorcolor: "#999999"
    //});
    // listImage();

    jsonGetDataListNoticeDLU(8);
    if ($("time.timeago").length > 0) {
        jQuery("time.timeago").timeago();
    }
    $(".youtube").colorbox({ iframe: true, innerWidth: 640, innerHeight: 390 });

    $.ColorLife.goToTop();
    $.ColorLife.backToTop();
});
// 
function listImage() {
    var html = '';
    html += '<ul>';
    for (i = 0; i < 9; i++) {
        html += ' <li class="thumbnail1"> <a href="/Uploads/1.jpg" class="group4 "><img src="/Uploads/1.jpg">                </a>            </li>';
    }
    html += '</ul>';
    $("#list-myimage").html(html);
    $(".group4").colorbox({ rel: 'group4', slideshow: true });
}
function playVideo() {
    id = $('.videourl').attr('data-videourl');
    var html = '<iframe class="embed-responsive-item" src="//www.youtube.com/embed/'+id+'?rel=0&amp;showinfo=0&autoplay=1" frameborder="0" allowfullscreen></iframe>';
    $('#video-item').html(html);
}
function jsonGetDataListNoticeDLU(id) {
    $.ajax({
        type: "GET",
        url: "http://mqc1.ddns.net/api/Application/getlatestnotice/" + id,
        // data: JSON.stringify(dto),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            // $(obj).html($.ColorLife.IMGLoading);
        },
        success: function Success(result, status) {
            // handler(result.ListData);
            
            var html = '';
            $.each(result.ListData, function (i, item) {
                html += '  <a href="http://mqc1.ddns.net/thong-bao/' + item.Alias + '/' + item.NoticeID + '" class="list-group-item " target="_blank">';
                html += ' <h5 class="list-group-item-heading">' + item.Name + '</h5>';
                html+=' <p class="list-group-item-text">';
                html += 'Đơn vị: ' + item.CreatedByID;
                html += '<i class="fa fa-calendar"></i> Ngày: ' + item.GetDateTime;
                html += '</p>';
                html += '</a>';
            });
            $("#thongbaonhatruong").html(html);
            $("#thongbaonhatruong a:first").addClass('active');
        },
        error: function Error(request, status, error) {
            //  $(obj).html(request.statusText);
            return null;
        }
    });
}

function startclock_vi_VN() {
    var curTime = new Date();
    var nhours = curTime.getHours();
    var nmins = curTime.getMinutes();
    var nsecn = curTime.getSeconds();
    var nday = curTime.getDay();
    var nmonth = curTime.getMonth();
    var ntoday = curTime.getDate();
    var nyear = curTime.getYear();
    var AMorPM = " ";

    if (nhours >= 12)
        AMorPM = "PM";
    else
        AMorPM = "AM";

    if (nhours >= 13)
        nhours -= 12;

    if (nhours == 0)
        nhours = 12;

    if (nsecn < 10)
        nsecn = "0" + nsecn;

    if (nmins < 10)
        nmins = "0" + nmins;

    if (nday == 0)
        nday = "Chủ nhật";
    if (nday == 1)
        nday = "Thứ 2";
    if (nday == 2)
        nday = "Thứ 3";
    if (nday == 3)
        nday = "Thứ 4";
    if (nday == 4)
        nday = "Thứ 5";
    if (nday == 5)
        nday = "Thứ 6";
    if (nday == 6)
        nday = "Thứ 7";


    nmonth += 1;

    if (nyear <= 99)
        nyear = "19" + nyear;

    if ((nyear > 99) && (nyear < 2000))
        nyear += 1900;
    var d;
    d = document.getElementById("theClock");

    d.innerHTML = 'Hôm nay:  <i class="fa fa-calendar"></i> ' + nday + ", Ngày " + ntoday + "/" + nmonth + "/" + nyear + " | <i class='fa fa-clock-o'></i> " + nhours + ":" + nmins + ":" + nsecn + " " + AMorPM;

    setTimeout('startclock_vi_VN()', 1000);

}