/**
FileName: colorlife.js
Project Name: ColorLife
Date Created: 29/6/2014 9:45:51 PM
Description: Auto-generated
Version: 1.0.0.0
Author:	Lê Thanh Tuấn - Khoa CNTT
Author Email: tuanitpro@gmail.com
Author Mobile: 0976060432
Author URI: http://tuanitpro.com
License: 
*/

 
(function ($) {
    $.extend({
        ColorLife: {
            IMGLoading: '<img src=\"/Content/Images/ajax-loader.gif\" border=\"0\" />',
            templateUrl: '/Content/templates/',
            imagesFolder: '/Content/images/',
            siteUrl: 'http://localhost',
            serviceUrl: '/api/',
            userId: 0,
            userEmail: '',
            yourIP: '',
            params: {},
            VietNamChar: [
           "aAeEoOuUiIdDyY",
           "áàạảãâấầậẩẫăắằặẳẵ",
           "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
           "éèẹẻẽêếềệểễ",
           "ÉÈẸẺẼÊẾỀỆỂỄ",
           "óòọỏõôốồộổỗơớờợởỡ",
           "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
           "úùụủũưứừựửữ",
           "ÚÙỤỦŨƯỨỪỰỬỮ",
           "íìịỉĩ",
           "ÍÌỊỈĨ",
           "đ",
           "Đ",
           "ýỳỵỷỹ",
           "ÝỲỴỶỸ"
            ],
            // HELPER //
            loader: function (isFade) {
                if (isFade) {

                    $('#Loader').fadeIn();
                } else {

                    $('#Loader').fadeOut();
                }
            },
            replaceUnicode: function (strInput) {
                for (i = 1; i < $.ColorLife.VietNamChar.length; i++) {
                    for (j = 0; j < $.ColorLife.VietNamChar[i].length; j++) {
                        strInput = strInput.replace($.ColorLife.VietNamChar[i][j], $.ColorLife.VietNamChar[0][i - 1]);
                    }
                }
                return strInput;
            },
               removeDiacriticsURL2: function (s) {
                var str = $.ColorLife.replaceUnicode(s).toLowerCase();
                str = str.replace(/\-/g, '');
                str = str.replace(/ /g, '-');
                str = str.replace(/\?/g, '');
                str = str.replace(/@/g, '-');
                str = str.replace(/\//g, '-');
                str = str.replace(/\&/g, '-');
                str = str.replace(/\(/g, '-');
                str = str.replace(/\=/g, '-');
                str = str.replace(/\)/g, '-');
                str = str.replace(/\./g, '-');
                str = str.replace(/\;/g, '-');
                str = str.replace(/\:/g, '');
                str = str.replace(/\,/g, '-');
                str = str.replace(/\"/g, '');
                str = str.replace(/\'/g, '');

                //  var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
                // return reg.test(email);
                return str.trim();
            },
            remove_vietnamese_accent: function (str) {
                str = str.replace(/[\u0300\u0301\u0303\u0309\u0323]/g, "");
                return str;
            },
            remove_special_characters: function (str) {
                str = str.replace(/[\u0021-\u002D\u002F\u003A-\u0040\u005B-\u0060\u007B-\u007E\u00A1-\u00BF]/g, " ");
                return str;
            },
            replace_vietnamese_characters: function (str) {
                str = str.replace(/[\u00C0-\u00C3\u00E0-\u00E3\u0102\u0103\u1EA0-\u1EB7]/g, "a");
                str = str.replace(/[\u00C8-\u00CA\u00E8-\u00EA\u1EB8-\u1EC7]/g, "e");
                str = str.replace(/[\u00CC\u00CD\u00EC\u00ED\u0128\u0129\u1EC8-\u1ECB]/g, "i");
                str = str.replace(/[\u00D2-\u00D5\u00F2-\u00F5\u01A0\u01A1\u1ECC-\u1EE3]/g, "o");
                str = str.replace(/[\u00D9-\u00DA\u00F9-\u00FA\u0168\u0169\u01AF\u01B0\u1EE4-\u1EF1]/g, "u");
                str = str.replace(/[\u00DD\u00FD\u1EF2-\u1EF9]/g, "y");
                str = str.replace(/[\u0110\u0111]/g, "d");

                return str;
            },
            removeDiacriticsURL: function (str) {
                str = $.ColorLife.remove_vietnamese_accent(str);
                str = $.ColorLife.remove_special_characters(str);
                str = $.ColorLife.replace_vietnamese_characters(str);

                // Replace spaces with hyphen minus (-)
                str = str.replace(/\s+/g, "-");

                // Ensure all characters are alphanumeric or hyphen minus (-)
                str = str.replace(/[^A-Za-z0-9\-]/g, "");

                // Replace two or more hyphen minus (--) with one (-)
                str = str.replace(/\-+/g, "-");

                // Remove the hyphen minus (-) at the begining or the end
                str = str.replace(/^\-|\-$/g, "");

                

                // Convert to Lowercase
                str = str.toLowerCase();

                
                return str.trim();
            },
            getQueryString: function (key) {
                key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
                var regex = new RegExp("[\\?&]" + key + "=([^&#]*)", 'ig');
                var qs = regex.exec(window.location.href);
                if (qs == null)
                    return '';
                else
                    return qs[1];
            },
            getParentWin: function () {
                var w = opener || parent;
                return w != window ? w : null;
            },
            isEnter: function (event) {
                return ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13));
            },

            insertFile: function (url) {
                var returnOpenerId = $.ColorLife.getQueryString('returnId');
                // alert(returnOpenerId);

                var win = this.getParentWin();

                if (returnOpenerId != null && returnOpenerId != '') {
                    win.document.getElementById(returnOpenerId).value = url;
                    self.close();
                    parent.$.fn.colorbox.close();
                }

                else if (navigator.appName.indexOf('Microsoft') != -1) {
                    window.returnValue = url;
                    self.close();
                    parent.$.fn.colorbox.close();
                }


            },
            getAliasURL: function (s) {
                var alias = $('#Alias');
                alias.val(jQuery.ColorLife.removeDiacriticsURL(s));
            },
            demoFunction: function () {
                alert($.ColorLife.userEmail);
            },

            notifyInfo: function (msg) {
                toastr.info(msg, 'Thông báo', options = { "closeButton": true, "positionClass": "toast-bottom-left" });
            },
            notifySuccess: function (msg) {
                toastr.success(msg, 'Thành công', options = { "closeButton": true, "positionClass": "toast-bottom-left" });
            },
            notifyError: function (msg) {
                toastr.warning(msg, 'Lỗi', options = { "closeButton": true, "positionClass": "toast-bottom-left" });
            },


            // JSON Data Fomater
            formatJSONDate: function (jsonDate) {
                var d = new Date(parseInt(jsonDate.substr(6), 10));
                var nullDate = new Date(1001, 0, 1);
                if (d.getTime() <= nullDate.getTime()) {
                    return "";
                }
                else {
                    var m = d.getMonth() + 1;
                    var s = d.getDate() + "/" + m + "/" + d.getFullYear() + ' ' + d.getHours() + ':' + d.getMinutes() + ':' + d.getSeconds();
                    return s;
                }
            },
            stringCurrency: function (num) {
                num = num.toString().replace(/\$|\,/g, '');
                if (isNaN(num)) num = "0";
                sign = (num == (num = Math.abs(num)));
                num = Math.floor(num * 100 + 0.50000000001);
                cents = num % 100;
                num = Math.floor(num / 100).toString();
                if (cents < 10) cents = "0" + cents;
                for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3) ; i++)
                    num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
                return num;
            },
            // Validate Email
            validateEmail: function (email) {
                var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
                return reg.test(email);
            },
            validatePassword: function (password) {
                var reg = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&*]).{6,20}.*$/;
                return reg.test(password);
                // return true;
            },
            validatePasswordConfirm: function (password1, password2) {
                if (password2 != password1)
                    return false;
                return true;
            },

            doUpload: function (action, preview, data, output) {
                new AjaxUpload(action, {
                    action: '/Admin/Image/SimpleUpload',
                    name: 'myFile',
                    data: { 'folder': data },
                    autoSubmit: true,
                    onSubmit: function (file, extension) {
                        if (!(extension && /^(jpg|png|jpeg|gif|bmp|JPG|PNG|JPEG|BMP|GIF)$/.test(extension))) {

                            alert('Only JPG, PNG or GIF files are allowed');

                            return false;
                        }

                        preview.attr('src', '/Content/images/bigloading.gif');

                        output.val('Loading...');
                        output.attr('disabled', true);
                        this.disable();

                    },
                    onComplete: function (file, response) {
                        this.enable();
                        preview.load(function () {
                            preview.unbind();
                        });
                        output.attr('disabled', false);
                        output.val('');
                        var resp = response;                        
                        resp = jQuery(resp).html();
                        preview.attr('src', resp);
                        output.val(resp);
                         
                        if ($("#removefile").length > 0) {
                            $("#removefile").attr('data-file', resp);
                            $("#removefile").show();
                        }
                        // alert(resp);
                        return false;
                    }
                });
            },
            removeUpload: function (file) {
                
                $.ajax({
                    type: "POST",
                    url: "/Admin/Image/DeleteFile?file=" + file,
                     
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                    },
                    success: function Success(result) {
                        $.ColorLife.notifySuccess(result.data);
                    },
                    error: function Error(request, status, error) {
                        $.ColorLife.notifyError(request.statusText);
                    }
                });
                return false;
            },

            json_loadjTemplate: function (divId) {
                var dto = {}; var myService = "JsonStats";
                $.ColorLife.loader(true);
                $.ajax({
                    type: "POST",
                    url: $.ColorLife.serviceUrl + myService,
                    data: JSON.stringify(dto),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: $.ColorLife.loader(true),
                    success: function Success(data, status) {
                        $(divId).setTemplateURL($.ColorLife.templateUrl + 'jtemplate.html');
                        $(divId).processTemplate(data.d);
                        $.ColorLife.loader(false);

                    },
                    error: function Error(request, status, error) {
                        $(divId).html(request.statusText);
                    }
                });
            }
            ,
             // API Helper 18/11/2014 //
            jsonGetDataObject: function (id, url, handler) {
                $.ajax({
                    type: "GET",
                    url: $.ColorLife.serviceUrl + "Application/" + url + "/" + id,
                    // data: JSON.stringify(dto),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        // $(obj).html($.ColorLife.IMGLoading);
                    },
                    success: function Success(result, status) {
                        handler(result.Data);
                    },
                    error: function Error(request, status, error) {
                        //  $(obj).html(request.statusText);
                        return null;
                    }
                });
            },
            jsonGetDataList: function (id, url, handler) {
                $.ajax({
                    type: "GET",
                    url: $.ColorLife.serviceUrl + "Application/" + url + "/" + id,
                    // data: JSON.stringify(dto),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        // $(obj).html($.ColorLife.IMGLoading);
                    },
                    success: function Success(result, status) {
                        handler(result.ListData);
                    },
                    error: function Error(request, status, error) {
                        //  $(obj).html(request.statusText);
                        return null;
                    }
                });
            },
            // SETTING            
            jsonGetSettingValue: function (id, obj) {
                this.jsonGetDataObject(id, 'Setting', function (handler) {
                    $(obj).html(handler.Value);
                });
            },             

            // , tiep tuc ham khac            
        }// end ColorLife
    }); // end extend
})(jQuery);