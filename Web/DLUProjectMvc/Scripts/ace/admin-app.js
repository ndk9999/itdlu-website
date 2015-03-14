 
 
jQuery(document).ready(function () {

    jQuery('.SITE_NAME').each(function () {
        $.ColorLife.jsonGetSettingValue('SITE_NAME', '.SITE_NAME');
    });
    jQuery('.COPYRIGHT').each(function () {
        $.ColorLife.jsonGetSettingValue('COPYRIGHT', '.COPYRIGHT');
    });
    $("#menu-toggle").click(function (e) {
        e.preventDefault();
        $("#wrapper").toggleClass("active");
    });
    jQuery("#Title").blur(function () {
        var name = $(this).val();
        jQuery.ColorLife.getAliasURL(name);
    });
    jQuery("#Name").blur(function () {
        var name = $(this).val();
        jQuery.ColorLife.getAliasURL(name);
    });
    
        FormValidate();
        helper();
        mutilDelete();
    

    // Active Menu
    $("li.dropdown-submenu.active").parent().parent().addClass("active");
     
});
function TableSorter() {
    if ($("table").hasClass("tablesorter")) {

        $("table").tablesorter();
    }
}

function helper() {

    $('#TuKhoa').keydown(function (e) {         
        if (e.keyCode == 13) {
            $('#btnTimKiem').click();
        }
    })

    $('#metanav-favbar-toggle').click(function () {
        if ($(this).hasClass('active')) {
            //configuser["favbar_pinned"] = 0;
            $("#metanav-favbar").animate({ "bottom": "-80px" }, 500, function ()
            { $('#metanav-favbar-toggle').removeClass('active'); });
        }
        else {
            //configuser["favbar_pinned"] = 1;
            $("#metanav-favbar").animate({ "bottom": "0" }, 500, function ()
            { $('#metanav-favbar-toggle').addClass('active'); });
        }
    });
    $.fn.datepicker.defaults.format = "mm/dd/yyyy";
    if ($('.datepicker').length > 0) {
        $('.datepicker').datepicker({
            format: 'mm/dd/yyyy',            
        });
    }

    if ($('#NgaySinh').length > 0) {
        $('#NgaySinh').datepicker({ 'startDate': '', 'format': 'dd/mm/yyyy' });
    }
    $('[rel=tooltip]').tooltip();

    if ($('.chooseImage').length > 0) {
        $(".chooseImage").colorbox({ iframe: true, width: "85%", height: "100%" });
    }
    
    $('.price').blur(function () {
        var value = $(this).val();
        var newPrice = $.ColorLife.stringCurrency(value);
        $(this).val(newPrice);
    });

    
    
    if ($("#Image").length > 0) {
        var a = $("#Image");
        $(a).blur(function () {
            $("#imgthumb").attr('src', a.val());
        });
    }
    //if ($("#anhDaiDienSV").length > 0) {
    //    var thumb = jQuery("img#imgthumb");
    //    var txtPicture = jQuery("[id$=AnhDaiDien]");
    //    var act = jQuery("#anhDaiDienSV");

    //    var data = '/Uploads/AnhDaiDien/';
    //    jQuery.ColorLife.doUpload(act, thumb, data, txtPicture);

    //    //  doUploadAnhDaiDien();
    //}
    //if ($("#choosefile").length > 0) {
    //    var thumb = jQuery("img#imgthumb");
    //    var txtPicture = jQuery("[id$=Image]");
    //    var act = jQuery("#choosefile");

    //    var data = '/Uploads/images/';
    //   jQuery.ColorLife.doUpload(act, thumb, data, txtPicture);

    //    //  doUploadAnhDaiDien();
    //}
   
    //var btnRemove = jQuery("#removefile");
    //btnRemove.click(function () {
    //    jQuery.ColorLife.removeUpload(txtPicture.val());
    //    // Xoa file moi up len
    //    txtPicture.val('/Content/Images/noimage.png');
    //    thumb.attr('src', '/Content/Images/noimage.png');
    //});

   
}

function insertIcon(path) {
    var txtPicture = jQuery("#Image");
    var thumb = jQuery("img#imgthumb");
    txtPicture.val(path);
    thumb.attr('src', path);
    $('#modal-icon').modal('hide');
}

function FormValidate(sender, args) {
    // Validation
    if (jQuery('form').length > 0) {
        jQuery('form').each(function () {
            var id = $(this).attr('id');
            $("#" + id).validate({
                errorElement: 'div',
                errorClass: 'help-block has-error',
                errorPlacement: function (error, element) {
                    element.parents('.form-group div').append(error);
                },
                highlight: function (label) {
                    $(label).closest('.form-group').removeClass('has-error has-success').addClass('has-error');
                },
                success: function (label) {
                    label.addClass('valid').closest('.form-group').removeClass('has-error has-success').addClass('has-success');
                }
            });
        });
    }
   
}
function currentTimeVN() {
    var e = $(".stats .icon-calendar").parent(),
        t = new Date,
        n = ["Tháng một", "Tháng hai", "Tháng ba", "Tháng tư", "Tháng năm", "Tháng sáu", "Tháng bảy", "Tháng tám", "Tháng chín", "Tháng mười", "Tháng mười một", "Tháng mười hai"],
        r = ["Chủ nhật", "Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy"];
    e.find(".details .big").html(t.getDate() + " " + n[t.getMonth()] + ", " + t.getFullYear());
    e.find(".details span").last().html(r[t.getDay()] + ", " + t.getHours() + ":" + ("0" + t.getMinutes()).slice(-2));
    setTimeout(function () { currentTimeVN() }, 1e4)
}
function currentTime() {
    var e = $(".stats .icon-calendar").parent(),
        t = new Date,
        n = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
        r = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
    e.find(".details .big").html(n[t.getMonth()] + " " + t.getDate() + ", " + t.getFullYear());
    e.find(".details span").last().html(r[t.getDay()] + ", " + t.getHours() + ":" + ("0" + t.getMinutes()).slice(-2));
    setTimeout(function () { currentTime() }, 1e4)
}

function pageSizeOnChange(obj) {
    if ($("#pagesize").length > 0) {
        $(obj).submit();
    };
}
function doSubmit(obj) {
    $(obj).submit();
}
$(function () {
    var colorbox_params = {
        reposition: true,
        scalePhotos: true,
        scrolling: false,
        previous: '<i class="icon-arrow-left"></i>',
        next: '<i class="icon-arrow-right"></i>',
        close: '&times;',
        current: '{current} of {total}',
        maxWidth: '100%',
        maxHeight: '100%',
        onOpen: function () {
            document.body.style.overflow = 'hidden';
        },
        onClosed: function () {
            document.body.style.overflow = 'auto';
        },
        onComplete: function () {
            $.colorbox.resize();
        }
    };

    $('.ace-thumbnails [data-rel="colorbox"]').colorbox(colorbox_params);
    $("#cboxLoadingGraphic").append("<i class='icon-spinner orange'></i>");//let's add a custom loading icon

    /**$(window).on('resize.colorbox', function() {
		try {
			//this function has been changed in recent versions of colorbox, so it won't work
			$.fn.colorbox.load();//to redraw the current frame
		} catch(e){}
	});*/
});
function mutilDelete() {
    $("#check_all").click(function () {
        var checked_status = this.checked;
        $(".check").each(function () {
            this.checked = checked_status;
        });
        var n = $(".check:checked").length;
        if (n > 0) {
            $('ace check').each(function () {
                $(this).css('backgroundColor', '#ffb44b ');
            });
            $('#deleteSelected').removeClass('delete-selected');
        }
        else {
            $('tbody tr').each(function () {
                $(this).css('backgroundColor', '');
            });
          $('#deleteSelected').addClass('delete-selected');
        }
    });


    $(".check").click(function () {

        var n = $(".check:checked").length;
        if (n > 0) {
            $(this).parent().parent().css('background', '#ffb44b');
            $('#deleteSelected').removeClass('delete-selected');
        }
        else {
            $(this).parent().parent().css('background', '');
          $('#deleteSelected').addClass('delete-selected');
        }
    });

    $(".displayflags").click(function () {
        // var n = $(".displayflags:checked").length;
        var selectedIds = $(".displayflags:checked");
        var total = 0;
        selectedIds.each(function () {
            // var id = $(this).val();
            total += parseInt($(this).val());
        });
        $("#DisplayFlags").val(total);
    });
    $(".productFeatured").click(function () {
        // var n = $(".displayflags:checked").length;
        var selectedIds = $(".productFeatured:checked");
        var total = 0;
        selectedIds.each(function () {
            // var id = $(this).val();
            total += parseInt($(this).val());
        });
        $("#Featured").val(total);
    });
    $("a.multidelete").click(function () {
        var n = $(".check:checked").length;
        var selectedIds = $(".check:checked");
        if (n == 0) {
            alert('Vui lòng chọn dòng cần xóa!');
            return false;
        }
        else {
            var ok = confirm('WARNING: The seleted items will be removed permanently. Are you sure?');
            if (ok) {
                var arr = [];
                selectedIds.each(function () {
                    alert($(this).val());
                })
            }
        }
    });
}

(function ($) {
    $.validator.addMethod('accept', function () { return true; });
    $.extend($.validator.messages, {
        required: "Dữ liệu bắt buộc nhập.",
        remote: "Hãy sửa cho đúng.",
        email: "Hãy nhập email.",
        url: "Hãy nhập URL.",
        date: "Hãy nhập ngày.",
        dateISO: "Hãy nhập ngày (ISO).",
        number: "Hãy nhập số.",
        digits: "Hãy nhập chữ số.",
        creditcard: "Hãy nhập số thẻ tín dụng.",
        equalTo: "Hãy nhập thêm lần nữa.",
        extension: "Phần mở rộng không đúng.",
        maxlength: $.validator.format("Hãy nhập từ {0} kí tự trở xuống."),
        minlength: $.validator.format("Hãy nhập từ {0} kí tự trở lên."),
        rangelength: $.validator.format("Hãy nhập từ {0} đến {1} kí tự."),
        range: $.validator.format("Hãy nhập từ {0} đến {1}."),
        max: $.validator.format("Hãy nhập từ {0} trở xuống."),
        min: $.validator.format("Hãy nhập từ {1} trở lên.")
    });
}(jQuery));



function GetMoneyText(money) {
    money = Math.round(money * 10) / 10;
    var retval = '';
    var sodu = 0;
    if (money >= 1000000000) {
        sodu = Math.floor(money / 1000000000);
        retval += sodu + ' tỷ ';
        money = money - (sodu * 1000000000);
    }
    if (money >= 1000000) {
        sodu = Math.floor(money / 1000000);
        retval += sodu + ' triệu ';
        money = money - (sodu * 1000000);
    }
    if (money >= 1000) {
        sodu = Math.floor(money / 1000);
        retval += sodu + ' nghìn ';
        money = money - (sodu * 1000);
    }
    if (money > 0) {
        retval += money + ' đồng';
    }
    return retval;
}

function GetMoneyText2(money) {
    money = Math.round(money * 10) / 10;

    var retval = '';
    var sodu = 0;
    if (money >= 1000000000) {
        sodu = money / 1000000000;
        return sodu + ' tỷ ';
    }
    if (money >= 1000000) {
        sodu = money / 1000000;
        return sodu + ' triệu ';
    }
    return GetMoneyText(money);
}