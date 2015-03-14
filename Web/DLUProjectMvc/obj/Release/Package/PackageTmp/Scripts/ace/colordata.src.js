/**
FileName: ColorData.src.js
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
        ColorLifeData: {
            IMGLoading: '<img src=\"/Content/Images/ajax-loader.gif\" border=\"0\" />',
            templateUrl: '/Content/templates/',
            imagesFolder: '/Content/images/',
            siteUrl: 'http://localhost',
            serviceUrl: '/api/',
            loader: function (isFade) {
                if (isFade) {
                 
                    $('#Loader').fadeIn();
                } else {
                 
                    $('#Loader').fadeOut();
                }
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

             deleteConfirm: function (message, callback) {
                new BootstrapDialog({
                    title: 'Confirmation',
                    // type: BootstrapDialog.TYPE_WARNING,
                    message: message,
                    closable: false,
                    draggable: true,
                    data: {
                        'callback': callback
                    },
                    buttons: [{
                        label: 'Cancel',
                        action: function (dialog) {
                            typeof dialog.getData('callback') === 'function' && dialog.getData('callback')(false);
                            dialog.close();
                        }
                    }, {
                        label: 'OK',
                        cssClass: 'btn-primary',
                        action: function (dialog) {
                            typeof dialog.getData('callback') === 'function' && dialog.getData('callback')(true);
                            dialog.close();
                        }
                    }]
                }).open();
            },
            deleteDataCallback: function (obj) {
                var table = $(obj).closest("table").attr("id");
                var row = $(obj).closest("tr");
                var id = row.attr("id");
                // var myService = "JsonDelete_" + table;
                //$.ColorLifeData.serviceUrl +
                var myService = $.ColorLifeData.serviceUrl + table + "/" + id;
                var dto = { "id": id };
                $.ajax({
                    url: myService,
                    data: { "id": id }, //JSON.stringify(dto),
                    type: "DELETE",//type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            row.remove();
                            $.ColorLifeData.notifySuccess(result.Message);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
            },
            jsonDeleteData: function (obj) {

                $.ColorLifeData.deleteConfirm("Bạn chắc chắn muốn xóa mục này?", function (result) {
                    if (result) {
                        $.ColorLifeData.deleteDataCallback(obj);
                    }
                });
                return false;
            },
            jsonDeleteSelectedData: function (obj) {
                var table = $(obj);
                var n = $(".check:checked").length;
                var selectedIds = $(".check:checked");
                if (n == 0) {
                    $.ColorLifeData.notifyError('Vui lòng chọn dòng cần xóa!');
                    return false;
                }
                else {
                    //dele
                    $.ColorLifeData.deleteConfirm("Bạn chắc chắn muốn xóa mục đã chọn?", function (result) {
                        if (result) {
                            selectedIds.each(function () {
                                var id = $(this).val()
                                var dto = { "id": id };
                                var row = $("#" + obj + " > tbody > tr[id=" + JSON.stringify(id) + "]");
                                var myService = obj + "/" + id;
                                $.ajax({
                                    url: $.ColorLifeData.serviceUrl + myService,
                                    data: JSON.stringify(dto),
                                    type: "DELETE",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    async: true,
                                    //beforeSend: $.ColorLifeData.loader(true),
                                    //complete: $.ColorLifeData.loader(false),
                                    success: function (result) {
                                        if (result.Success) {
                                            row.remove();
                                            // $(".dataTables_info").html(result.Message);
                                            // $.ColorLifeData.notify(true, 'Thành công', result.Message);
                                        }
                                        else {
                                            // $.ColorLifeData.notify(false, 'Lỗi', result.Message);
                                            //  $.ColorLifeData.showStatus("warning", result.Message);
                                        }
                                    },
                                    error: function (errormessage) {
                                        //Hiển thị lỗi nếu xảy ra
                                        // $.ColorLifeData.notify(false, 'Lỗi', errormessage.responseText);
                                    }
                                });
                            });

                        }
                    });

                    return false;
                }
            },
            jsonDeleteAllData: function (obj) {

                $.ColorLife.deleteConfirm("Cảnh báo! Chọn chức năng này sẽ xóa toàn bộ dữ liệu. Bạn chắc chắn muốn xóa tất cả?", function (result) {
                    if (result) {
                        var table = $(obj);
                        $.ajax({
                            // url: $.ColorLife.serviceUrl + myService,
                            url: $.ColorLife.serviceUrl + obj + "/DeleteAll/?id=1&x=1",
                            //data: JSON.stringify(dto),
                            type: "DELETE",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: true,
                            beforeSend: $.ColorLife.loader(true),
                            complete: $.ColorLife.loader(false),
                            success: function (result) {
                                if (result.Success) {
                                    var row = $("#" + obj + " > tbody");
                                    row.empty();
                                    $("#PagingControl1").html('');
                                    $.ColorLife.notifySuccess(result.Message);
                                    console.log();
                                }
                                else {
                                    $.ColorLife.notifyError(result.Message);
                                    console.log(result.Message);
                                }
                            },
                            error: function (errormessage) {
                                //Hiển thị lỗi nếu xảy ra
                                $.ColorLife.notifyError(errormessage.responseText);
                                console.log(errormessage.responseText);
                            }
                        });
                    };
                });
                return false;
            },
            jsonIsPublishedData: function (obj) {
                var table = $(obj).closest("table").attr("id");
                var row = $(obj).closest("tr");
                var id = row.attr("id");
                var state = $(obj).closest("span");
                var classCurr = state.attr("class");
                var published = row.attr("data-ispublish");

                var x = (published == "True") ? "False" : "True";
                var dto = { "id": id };

                var myService = "Json" + table + "_IsPublished";
                $.ajax({
                    // url: $.ColorLifeData.serviceUrl + myService,
                    url: $.ColorLifeData.serviceUrl + table + "/PutIsPublished/" + id,
                    data: JSON.stringify(dto),
                    type: "PUT",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            state.removeClass(classCurr);
                            state.addClass("published-" + x);
                            row.attr("data-ispublish", x);

                            $.ColorLifeData.notifySuccess(result.Message);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                            //  $.ColorLifeData.showStatus("warning", result.Message);
                        }
                    },
                    error: function (errormessage) {
                        //Hiển thị lỗi nếu xảy ra
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                }); return false;
            },
            jsonSortOrderData: function (obj, number) {
                var table = $(obj).closest("table").attr("id");
                var row = $(obj).closest("tr");
                var id = row.attr("id");
                //   var myService = "Json" + table + "_SortOrder";
                var dto = { "id": id, "number": number };
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + table + "/PutSortOrder?id=" + id + "&number=" + number,
                    //data: JSON.stringify(dto),
                    type: "PUT",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {

                            $.ColorLifeData.notifySuccess(result.Message);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                }); return false;

            },
            jsonSettingUpdate: function (form) {
                var obj = $(form);
                var Setting = {
                    SettingID: $(obj).attr("id"),
                    Name: $(obj).attr("id"),
                    Value: $(obj).val(),
                };
                
                if (Setting.Value.length > 0) {
                    $.ajax({
                        url: $.ColorLifeData.serviceUrl + "Setting",
                        data: JSON.stringify(Setting),
                        type: "PUT",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: $.ColorLifeData.loader(true),
                        complete: $.ColorLifeData.loader(false),
                        success: function (result) {
                            if (result.Success) {
                                $.ColorLifeData.notifySuccess(result.Message);
                            }
                            else {
                                $.ColorLifeData.notifyError(result.Message);
                            }
                        },
                        error: function (errormessage) {
                            $.ColorLifeData.notifyError(errormessage.responseText);
                        }
                    });
                }
                else {                   
                    $.ColorLifeData.notifyError('Bạn cần nhập dữ liệu đầy đủ.');
                }
                return false;
            },
            jsonEditableSetting: function () {
                $('#Setting a.editable').editable({
                    type: 'text',
                    name: 'Name',

                    title: 'Nhập vào giá trị',
                    // mode: 'inline', // popup or inline
                    ajaxOptions: {
                        type: 'put',
                        dataType: 'json'
                    },
                    validate: function (value) {
                        if ($.trim(value) == '') {
                            return 'This field is required';
                        }
                    },
                    url: function (params) {

                        var Setting = {
                            Name: params.pk,
                            Value: params.value,

                        };
                        return $.ajax({
                            type: 'PUT',
                            url: $.ColorLifeData.serviceUrl + 'Setting',
                            data: JSON.stringify(Setting),
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            async: true,
                            cache: false,
                            timeout: 10000,
                            beforeSend: $.ColorLifeData.loader(true),
                            complete: $.ColorLifeData.loader(false),
                            success: function (result) {
                                if (result.Success) {
                                    $.ColorLifeData.notifySuccess(result.Message);
                                }
                                else {
                                    $.ColorLifeData.notifyError(result.Message);
                                }
                            },
                            error: function (errormessage) {
                                $.ColorLifeData.notifyError(errormessage.responseText);
                            }
                        });
                    }
                });
            },

            // 26/11/2014
            // HangHoa           
            jsonInsertProductStore: function (form) {
                // var dto = { "id": id, "roleId": rId };
                var obj = $(form);
                var ProductStored = {
                    ProductID: jQuery("#ProductID").val(),
                    StoreID: obj.find("#StoreID").val(),
                    SalerID: obj.find("#SalerID").val(),
                    CustomerID: obj.find("#CustomerID").val(),
                    DateAdded: obj.find("#DateAdded").val(),
                    Quantity: obj.find("#Quantity").val(),
                    DateEnded: obj.find("#DateEnded").val(),
                    Status: obj.find("#StoreStatus").val(),
                };


                if (ProductStored.ProductID.length && ProductStored.StoreID.length) {
                    $.ajax({
                        url: $.ColorLifeData.serviceUrl + "ProductStored",
                        data: JSON.stringify(ProductStored),
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: $.ColorLifeData.loader(true),
                        complete: $.ColorLifeData.loader(false),
                        success: function (result) {
                            if (result.Success) {
                                $.ColorLifeData.notifySuccess(result.Message);
                                 
                                // obj.modal('hide');
                            }
                            else {
                                $.ColorLifeData.notifyError(result.Message);
                            }
                        },
                        error: function (errormessage) {
                            $.ColorLifeData.notifyError(errormessage.responseText);
                        }
                    });
                }
                else {
                    $(obj).find('.form-group').removeClass('has-error has-success').addClass('has-error');
                    $.ColorLifeData.notifyError('Bạn cần nhập dữ liệu đầy đủ.');
                }
                return false;
            },
            // 22/11/2014
            jsonStaffCheckEmailValidate: function (id, handler) {
                if (id.length) {
                    $.ajax({
                        url: $.ColorLife.serviceUrl + "Staff/CheckEmailAvailable/" + id,

                        type: "GET",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",

                        success: function (result) {
                            handler(result);

                        },
                        error: function (errormessage) {

                            alert(errormessage.responseText);

                        }
                    });
                    return false;
                }
                return false;
            },

            // 17/11/2014
            jsonGetDataObject: function (id, url, handler) {
                $.ajax({
                    type: "GET",
                    url: $.ColorLife.serviceUrl + url + "/" + id,
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
                    url: $.ColorLife.serviceUrl + url + "/" + id,
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
            jsonGetProductImageById: function (id) {
                $.ColorLifeData.jsonGetDataList(id, 'ProductImage/GetByProduct', function (handler) {
                    var html = '<ul class="ace-thumbnails clearfix">';
                    $.each(handler, function (i, item) {
                        html += ' <li>';
                        html += ' <a data-rel="colorbox" href="' + item.ImageUrl + '" class="cboxElement">';
                        html += '<img width="150" height="150" src="' + item.ImageUrl + '" alt="150x150">';
                        html += '<div class="text"><div class="inner">Click để xem</div></div>';
                        html += '</a>';
                        html += ' <div class="tools tools-bottom">';
                        html += '<a href="#" onclick="return $.ColorLifeData.jsonDeleteProductImage(' + item.ImageID + ',' + item.ProductID + ')">';
                        html += '   <i class="ace-icon fa fa-times red"></i>';
                        html += '</a>';
                        html += '   </div>';
                        html += ' </li>';
                    });
                    html += '</ul>';
                    $("#ProductImage").html(html);
                });
            },
            jsonInsertProductImage: function (productId, imageUrl) {
                // var dto = { "id": id, "roleId": rId };
                var ProductImage = {
                    ImageID: -1,
                    ProductID: productId,
                    ImageUrl: imageUrl
                };
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "ProductImage",
                    data: JSON.stringify(ProductImage),
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            $.ColorLifeData.notifySuccess(result.Message);
                            $.ColorLifeData.jsonGetProductImageById(productId);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },
            jsonDeleteProductImage: function (id, productId) {
                 
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "ProductImage/"+id,                    
                    type: "DELETE",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            $.ColorLifeData.notifySuccess(result.Message);
                            $.ColorLifeData.jsonGetProductImageById(productId);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },
            // 03/08/2014
            jsonInsertDienNuoc: function () {
                // var dto = { "id": id, "roleId": rId };
                var DienNuoc = {
                    IDDienNuoc: -1,
                    IDPhong: $("#IDPhong").val(),
                    ChiSoMoi: $("#ChiSoCuoi").val(),
                    Thang: $("#Thang").val(),
                    LoaiSuDung: $("#formThemDienNuoc input[type='radio']:checked").val()

                };

                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "DienNuoc",
                    data: JSON.stringify(DienNuoc),
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            $.ColorLifeData.notifySuccess(result.Message);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });


                return false;
            },
            jsonGetAllFileIconMenu: function () {

                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "FileIconMenu/LoadAllMenuIcon?path=/content/icons/",
                    //data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            $("#datafileicon").html(result.Data);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },
            // THONG KE
            //1.	Thống kê tổng số sinh viên hiện ở Khu Nội trú;
            jsonThongKeTongSoSinhVienKhuNoiTru: function (handleData) {

                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "ThongKe/ThongKeTongSoSinhVienKhuNoiTru/1",
                    //data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            handleData(result.Data);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },
            // 4.	Thống kê tổng số sinh viên là người dân tộc (nam + nữ) ở Khu Nội trú;
            jsonThongKeSVTheoDanToc: function (handleData) {

                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "ThongKe/ThongKeTongSoSinhVienDanToc/1",
                    //data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            handleData(result);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },

            // 5. Thong ke sinh vien theo khoa  / khóa học
            jsonThongKeSVTheoKhoa: function (handleData) {

                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "ThongKe/ThongKeTongSoSinhVienKhoa/1",
                    //data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            handleData(result);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },
            // 5. Thong ke sinh vien theo khoa  / khóa học
            jsonThongKeSVTheoKhoaHoc: function (handleData) {

                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "ThongKe/ThongKeTongSoSinhVienKhoaHoc/1",
                    //data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            handleData(result);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },
            // 6.	Thống kê tổng số sinh viên theo tỉnh ở Khu Nội trú;
            jsonThongKeSVTheoTinhThanh: function (handleData) {

                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "ThongKe/ThongKeTongSoSinhVienTinhThanh/1",
                    //data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            handleData(result);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },

            // 7. Thong ke sinh vien doi tuong uu tien
            jsonThongKeSVTheoDTUT: function (handleData) {

                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "ThongKe/ThongKeTongSoSinhVienDoiTuongUuTien/1",
                    //data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            handleData(result);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },
            // 10.	Thống kê số lượng thành viên từng hộ gia đình ở Khu Nội trú;
            jsonThongKeTongSoThanNhan: function (handleData) {

                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "ThongKe/ThongKeTongSoThanNhanHoGiaDinh/1",
                    //data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            handleData(result);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },
            
            jsonThongKeHoSoChiTiet: function (handleData) {

                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "ThongKe",
                    //data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            handleData(result.Data);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },
            jsonThongKeSVTheoKhuNha: function (handleData) {

                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "ThongKeSVTheoKhuNha",
                    //data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            handleData(result);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },
          
         
            jsonThongKeSVTheoQuocGia: function (handleData) {

                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "ThongKe/ThongKeTongSoSinhVienNuocNgoai/1",
                    //data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            handleData(result);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },
            jsonThongKeGVTheoQuocGia: function (handleData) {

                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "ThongKe/ThongKeTongSoGiaoVienNuocNgoai/1",
                    //data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            handleData(result);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },
            jsonThongKePhongChiTiet: function (handleData) {

                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "ThongKe/ThongKePhong/1",
                    //data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            handleData(result.Data);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },
            jsonThongKeThietBiChiTiet: function (handleData) {

                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "ThongKe/ThongKeThietBi/1",
                    //data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            handleData(result.Data);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },
            jsonThongKeHoaDonChiTiet: function (handleData, year) {

                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "ThongKe/ThongKeHoaDon/?id=1&year=" + year,
                    //data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            handleData(result.Data);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },
            jsonThongKeTongThuHDSinhVienTheoNam: function (handleData, year) {
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "ThongKeHoaDonSinhVien?year=" + year,
                    //data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            handleData(result.Data);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },
            jsonThongKeTongThuHDSinhVienTheoThang: function (handleData, fDate,tDate) {
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "ThongKeHoaDonSinhVien/GetByMonth?fromDate=" + fDate + "&toDate="+tDate,
                    //data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            handleData(result.Data);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },

            jsonThongKeTongThuHDTienDienTheoNam: function (handleData, year) {
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "ThongKeHoaDonTienDien?year=" + year,
                    //data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            handleData(result.Data);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },
            jsonThongKeTongThuHDTienDienTheoThang: function (handleData, fDate, tDate) {
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "ThongKeHoaDonTienDien/GetByMonth?fromDate=" + fDate + "&toDate=" + tDate,
                    //data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            handleData(result.Data);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },

            jsonThongKeTongThuHDTienNuocTheoNam: function (handleData, year) {
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "ThongKeHoaDonTienNuoc?year=" + year,
                    //data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            handleData(result.Data);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },
            jsonThongKeTongThuHDTienNuocTheoThang: function (handleData, fDate, tDate) {
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "ThongKeHoaDonTienNuoc/GetByMonth?fromDate=" + fDate + "&toDate=" + tDate,
                    //data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            handleData(result.Data);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },

            jsonThongKeHoaDonTongThuKNTTheoNam: function (handleData, year) {
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "ThongKeHoaDonTongThuKNT?year=" + year,
                    //data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            handleData(result.Data);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },
            jsonThongKeHoaDonTongThuKNTTheoThang: function (handleData, fDate, tDate) {
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "ThongKeHoaDonTongThuKNT/GetByMonth?fromDate=" + fDate + "&toDate=" + tDate,
                    //data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            handleData(result.Data);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },

        

            jsonBaoHongThietBi: function (obj) {
                var table = $(obj).closest("table").attr("id");
                var row = $(obj).closest("tr");
                var id = row.attr("id");

                $.ajax({
                    // url: $.ColorLifeData.serviceUrl + myService,
                    url: $.ColorLifeData.serviceUrl + table + "/BaoHongThietBi?id=" + id,
                    // data: JSON.stringify(dto),
                    type: "PUT",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {

                            $.ColorLifeData.notifySuccess(result.Message);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                            //  $.ColorLifeData.showStatus("warning", result.Message);
                        }
                    },
                    error: function (errormessage) {
                        //Hiển thị lỗi nếu xảy ra
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                }); return false;
            },
            jsonBaoMatThietBi: function (obj) {
                var table = $(obj).closest("table").attr("id");
                var row = $(obj).closest("tr");
                var id = row.attr("id");

                $.ajax({
                    // url: $.ColorLifeData.serviceUrl + myService,
                    url: $.ColorLifeData.serviceUrl + table + "/BaoMatThietBi?id=" + id + "&abc=1",
                    // data: JSON.stringify(dto),
                    type: "PUT",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {

                            $.ColorLifeData.notifySuccess(result.Message);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                            //  $.ColorLifeData.showStatus("warning", result.Message);
                        }
                    },
                    error: function (errormessage) {
                        //Hiển thị lỗi nếu xảy ra
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                }); return false;
            },
            jsonTraThietBi: function (obj) {
                var table = $(obj).closest("table").attr("id");
                var row = $(obj).closest("tr");
                var id = row.attr("data-id2");

                $.ajax({
                    // url: $.ColorLifeData.serviceUrl + myService,
                    url: $.ColorLifeData.serviceUrl + table + "/TraThietBi?id=" + id + "&abc=1",
                    // data: JSON.stringify(dto),
                    type: "DELETE",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {

                            $.ColorLifeData.notifySuccess(result.Message);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                            //  $.ColorLifeData.showStatus("warning", result.Message);
                        }
                    },
                    error: function (errormessage) {
                        //Hiển thị lỗi nếu xảy ra
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                }); return false;
            },
            jsonPhongChiTietModal_openModal: function (id) {
                var obj = $('#myPhongChiTietModal');
                
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "Phong/"+id,
                    // data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            obj.find('.modal-title').html('Phòng ' + result.Data.Phong.Ten);
                            obj.find('.tenphong').html(result.Data.Phong.Ten);
                            obj.find('.tenkhunha').html(result.Data.Phong.KhuNha.Ten);
                            obj.find('.loaiphong').html(result.Data.Phong.GetLoaiPhong + ' - ' + result.Data.Phong.GetLoaiPhongSuDung);
                            obj.find('.tinhtrangphong').html(result.Data.Phong.GetTrangThaiPhong);
                            obj.find('.sogiuong').html(result.Data.Phong.SoGiuong);
                            obj.find('.ghichu').html(result.Data.Phong.GhiChu);

                            obj.find('.badge').html(result.Data.HoSos.length);
                            var table = '<table class="table table-hover table-nomargin dataTable table-bordered">'
                            table += "<tr>";
                            table += "<th>MSSV</th>";
                            table += "<th>Họ tên / Giới tính</th>";
                            table += "<th>Ngày sinh</th>";
                            table += "<th>Email/Số ĐT</th>";
                            table += "<th>Ngào vào / ra</th>";
                            table += "<th>Lớp</th>";

                            table += "<th></th>";
                            table += "</tr>";
                            var list = '';
                            $.each(result.Data.HoSos, function (i, value) {
                                list += "<tr>"
                                list += '<td>' + value.MaSoCaNhan + '</td>';
                                list += '<td><a href="../QLSinhVien/Default?controller=CTSinhVien&ID=' + value.MaSoCaNhan + '">' + value.HoTenDayDu + '</a> (' + value.GioiTinh + ')</td>';
                                list += '<td>' + value.NgaySinh + '</br>CMND: ' + value.SoCMND + '</td>';
                                list += '<td>' + value.Email + '</br>' + value.SoDienThoai + '</td>';
                                list += '<td>' + value.NgayBatDau + '</br>' + value.NgayKetThuc + '</td>';
                                list += '<td>' + value.Lop + '</td>';

                                list += '<td><a href="../Phong/Default?controller=DoiPhong&MSCN=' + value.MaSoCaNhan + '" title="Đổi phòng" rel="tooltip"><i class="fa fa-refresh"></i></a><a href="../Phong/Default?controller=TraPhong&MSCN=' + value.MaSoCaNhan + '" title="Trả phòng" rel="tooltip"><i class="fa fa-times"></i></a></td>';
                                list += "</tr>"
                            });
                            table += list;
                            table += '</table>';
                            obj.find('#danhSach').html(table);

                            var dienNuoc = '<table class="table table-hover table-nomargin dataTable table-bordered">'
                            dienNuoc += "<tr>";
                            dienNuoc += "<th>Kỳ SD</th>";
                            dienNuoc += "<th>Loại HD</th>";
                           
                            dienNuoc += "<th>CS cũ</th>";
                            dienNuoc += "<th>Cs mới</th>";
                            dienNuoc += "<th>Tổng mức</th>";
                            dienNuoc += "<th>Đơn giá</th>";
                            dienNuoc += "<th>Thành tiền</th>";
                            dienNuoc += "<th>Trạng thái</th>";

                            dienNuoc += "<th></th>";
                            dienNuoc += "</tr>";
                            var list1 = '';
                            $.each(result.Data.DSHoaDonDienNuocs, function (i, value) {
                                list1 += "<tr>";
                                list1 += '<td>' + value.Thang + '/' + value.Nam + '</td>';
                                list1 += '<td>' + value.GetLoaiHoaDon + '</td>';
                               
                                list1 += '<td>' + value.GetChiSoCu + '</td>';
                                list1 += '<td>' + value.GetChiSoMoi + '</td>';
                                list1 += '<td>' + value.GetTongMucTieuThu + '</td>';
                                list1 += '<td>' + value.DonGia + '</td>';
                                list1 += '<td>' + value.ThanhTien +'</td>';
                                list1 += '<td>' + value.GetTrangThai + '</td>';

                                list1 += '<td><a href="../HoaDon/Default?controller=CTHDTienDienNuoc&id=' + value.IDHoaDon + '" title="Chi tiết" rel="tooltip"><i class="fa fa-edit"></i></a></td>';
                                list1 += "</tr>"
                            });
                            dienNuoc += list1;
                            dienNuoc += '</table>';
                           
                            dienNuoc += '<a href="Default.aspx?controller=ThemHoaDon&IDPhong='+result.Data.Phong.IDPhong+'" class="btn btn-primary pull-right">Thêm điện nước</a>';
                            dienNuoc += '</br>';
                            obj.find('#dienNuoc').html(dienNuoc);

                            obj.find('.linkchitiet').attr('href', 'Default.aspx?controller=CTPhong&IDPhong=' + result.Data.Phong.IDPhong);
                            $(obj).modal('show');
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
               
            },
            jsonUpdateHitsData: function (obj) {
                var table = $(obj).attr("data-table");
                var id = $(obj).attr("data-id");
                var myService = "Json" + table + "_UpdateHits";
                var dto = { "id": id };
                $.ajax({
                    url: $.ColorLife.serviceUrl + myService,
                    data: JSON.stringify(dto),
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,

                    success: function (result) {
                        if (result.Success) {
                            // alert(result.Message);
                            //$.ColorLife.notify(true, 'Thành công', result.Message);
                        }
                        else {
                            // $.ColorLife.notify(false, 'Lỗi', result.Message);
                            //  $.ColorLife.showStatus("warning", result.Message);
                            // alert(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        //Hiển thị lỗi nếu xảy ra
                        // $.ColorLife.notify(false, 'Lỗi', errormessage.responseText);
                        // alert(errormessage.responseText);
                    }
                });
                return false;
            },
            // 21/7/2014 - CRUD
            jsonGetCaiDat:function(id){
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "CaiDat/" + id,
                    // data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            alert(result.Data.KeyValue);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
            },
            jsonGetCaiDatThongTinCongTy: function (handleData) {
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "ThongTinCongTy",
                    // data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            handleData(result.Data);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
            },
            jsonUpdateThongTinCongTy: function () {
                var obj = $('#myModalThongTin');
                var ThongTinCongTyModel = {
                    TenCongTy: obj.find("#CTY_TEN").val(),
                    DiaChi: obj.find("#CTY_DIACHI").val(),
                    SoDienThoai: obj.find("#CTY_SODT").val(),
                    Fax:obj.find("#CTY_FAX").val(),
                    Website:obj.find("#CTY_WEBSITE").val(),
                    Email:obj.find("#CTY_EMAIL").val(),
                    LinhVuc:obj.find("#CTY_LINHVUC").val(),
                    MST:obj.find("#CTY_MST").val(),
                    Logo:obj.find("#CTY_LOGO").val(),
                    GPKD: obj.find("#CTY_GPKD").val(),
                };
                if (ThongTinCongTyModel.TenCongTy.length && ThongTinCongTyModel.DiaChi.length > 3) {
                    $.ajax({
                        url: $.ColorLifeData.serviceUrl + "ThongTinCongTy",
                        data: JSON.stringify(ThongTinCongTyModel),
                        type: "PUT",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                        },
                        success: function (result) {
                            if (result.Success) {
                                $.ColorLifeData.notifySuccess(result.Message);
                              //  $('#myModalThongTin').modal('hide');
                            }
                            else {
                                $.ColorLifeData.notifyError(result.Message);
                            }
                        },
                        error: function (errormessage) {
                            $.ColorLifeData.notifyError(errormessage.responseText);
                        }
                    });
                }
                else {
                    $(obj).find('.form-group').removeClass('has-error has-success').addClass('has-error');
                    $.ColorLifeData.notifyError('Bạn cần nhập dữ liệu đầy đủ.');
                }
                return false;
            },
            jsonGetCaiDatMaTuDong: function (handleData) {
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "MaTuDong",
                    // data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            handleData(result.Data);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
            },
            jsonUpdateMaTuDong: function () {
                var obj = $('#myModalMaTuDong');
                var MaTuDongModel = {
                    HangHoa: obj.find("#MA_HH").val(),
                    KhachHang: obj.find("#MA_KH").val(),
                    NhaCungCap: obj.find("#MA_NCC").val(),
                    PhieuNhap: obj.find("#MA_PN").val(),
                    PhieuXuat: obj.find("#MA_PX").val(),
                    PhieuChuyenKho: obj.find("#MA_PCH").val(),
                    PhieuNhapTra: obj.find("#MA_PNT").val(),
                    PhieuXuatTra: obj.find("#MA_PXT").val(),
                    PhieuThu: obj.find("#MA_PT").val(),
                    PhieuChi: obj.find("#MA_PC").val(),
                    PhieuThuKhac: obj.find("#MA_PTK").val(),
                    PhieuChiKhac: obj.find("#MA_PCK").val(),
                };
                if (MaTuDongModel.HangHoa.length && MaTuDongModel.KhachHang.length) {
                    $.ajax({
                        url: $.ColorLifeData.serviceUrl + "MaTuDong",
                        data: JSON.stringify(MaTuDongModel),
                        type: "PUT",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                        },
                        success: function (result) {
                            if (result.Success) {
                                $.ColorLifeData.notifySuccess(result.Message);
                                //  $('#myModalThongTin').modal('hide');
                            }
                            else {
                                $.ColorLifeData.notifyError(result.Message);
                            }
                        },
                        error: function (errormessage) {
                            $.ColorLifeData.notifyError(errormessage.responseText);
                        }
                    });
                }
                else {
                    $(obj).find('.form-group').removeClass('has-error has-success').addClass('has-error');
                    $.ColorLifeData.notifyError('Bạn cần nhập dữ liệu đầy đủ.');
                }
                return false;
            },
            jsonEditableCaiDat: function () {
                $('#CaiDat a.editable').editable({
                    type: 'text',
                    name: 'KeyName',

                    title: 'Nhập vào giá trị',
                    // mode: 'inline', // popup or inline
                    ajaxOptions: {
                        type: 'put',
                        dataType: 'json'
                    },
                    validate: function (value) {
                        if ($.trim(value) == '') {
                            return 'This field is required';
                        }
                    },
                    url: function (params) {

                        var CaiDat = {
                            KeyName: params.pk,
                            KeyValue: params.value,

                        };
                        return $.ajax({
                            type: 'PUT',
                            url: $.ColorLifeData.serviceUrl + 'CaiDat',
                            data: JSON.stringify(CaiDat),
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            async: true,
                            cache: false,
                            timeout: 10000,
                            success: function (result) {
                                if (result.Success) {
                                    $.ColorLifeData.notifySuccess(result.Message);
                                }
                                else {
                                    $.ColorLifeData.notifyError(result.Message);
                                }
                            },
                            error: function (errormessage) {
                                $.ColorLifeData.notifyError(errormessage.responseText);
                            }
                        });
                    }
                });
            },

            jsonGetHoSo: function (id) {
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "HoSo/" + id,
                    // data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            alert(result.Data.KeyValue);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
            },
            jsonValidateMaSoCaNhan: function (id) {
                if (id === '')
                    $.ColorLifeData.notifyError('Vui lòng nhập Mã số cá nhân.');
                else {
                    $.ajax({
                        url: $.ColorLifeData.serviceUrl + "HoSo/GetValidate/" + id,
                        // data: JSON.stringify(dto),
                        type: "GET",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: $.ColorLifeData.loader(true),
                        complete: $.ColorLifeData.loader(false),
                        success: function (result) {
                            if (result.Success) {
                                $('.masocanhan').val('');
                                $.ColorLifeData.notifyError(result.Message);
                            }
                            else {
                                $.ColorLifeData.notifyInfo(result.Message);
                            }
                        },
                        error: function (errormessage) {
                            $.ColorLifeData.notifyError(errormessage.responseText);
                        }
                    });
                }
            },
            jsonValidateMaSoSinhVien: function (id) {
                if (id === '')
                    $.ColorLifeData.notifyError('Vui lòng nhập Mã số Sinh viên.');
                else {
                    $.ajax({
                        url: $.ColorLifeData.serviceUrl + "Student/GetValidate/" + id,
                        // data: JSON.stringify(dto),
                        type: "GET",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: $.ColorLifeData.loader(true),
                        complete: $.ColorLifeData.loader(false),
                        success: function (result) {
                            if (result.Success) {
                                $('.masocanhan').val('');
                                $.ColorLifeData.notifyError(result.Message);
                            }
                            else {
                                $.ColorLifeData.notifyInfo(result.Message);
                            }
                        },
                        error: function (errormessage) {
                            $.ColorLifeData.notifyError(errormessage.responseText);
                        }
                    });
                }
            },
            jsonValidateMaSoCaNhanHoaDon: function (id) {
                if (id === '')
                    $.ColorLifeData.notifyError('Vui lòng nhập Mã số cá nhân.');
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "HoSo/?id=" + id+"&x=1",
                    // data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            
                            // $.ColorLifeData.notifyError(result.Message);
                        }
                        else {
                            $('.masocanhan').val('');
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
            },
            jsonInsertNhomHangHoa: function (form) {
                // var dto = { "id": id, "roleId": rId };
                var obj = $(form);
                var NhomHangHoa = {
                    IDNhomHangHoa: obj.find("#IDNhomHangHoa").val(),
                    TenNhomHangHoa: obj.find("#TenNhomHangHoa").val(),
                    GhiChu: obj.find("#GhiChu").val(),
                };

                // alert(NhomHangHoa.TenNhomHangHoa);
                if (NhomHangHoa.IDNhomHangHoa.length && NhomHangHoa.TenNhomHangHoa.length > 3) {
                    $.ajax({
                        url: $.ColorLifeData.serviceUrl + "NhomHangHoa",
                        data: JSON.stringify(NhomHangHoa),
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: $.ColorLifeData.loader(true),
                        complete: $.ColorLifeData.loader(false),
                        success: function (result) {
                            if (result.Success) {
                                $.ColorLifeData.notifySuccess(result.Message);
                                obj.find("#IDNhomHangHoa").val(result.Data);
                                obj.find("#TenNhomHangHoa").val('');
                                obj.find("#GhiChu").val('');
                                // obj.modal('hide');
                            }
                            else {
                                $.ColorLifeData.notifyError(result.Message);
                            }
                        },
                        error: function (errormessage) {
                            $.ColorLifeData.notifyError(errormessage.responseText);
                        }
                    });
                }
                else {
                    $(obj).find('.form-group').removeClass('has-error has-success').addClass('has-error');
                    $.ColorLifeData.notifyError('Bạn cần nhập dữ liệu đầy đủ.');
                }
                return false;
            },
            jsonInsertNhomHangHoa2: function (form) {
                // var dto = { "id": id, "roleId": rId };
                var obj = $(form);
                var NhomHangHoa = {
                    IDNhomHangHoa: obj.find("#IDNhomHangHoa").val(),
                    TenNhomHangHoa: obj.find("#TenNhomHangHoa").val(),
                    GhiChu: obj.find("#GhiChu").val(),
                };

                // alert(NhomHangHoa.TenNhomHangHoa);
                if (NhomHangHoa.IDNhomHangHoa.length && NhomHangHoa.TenNhomHangHoa.length > 3) {
                    $.ajax({
                        url: $.ColorLifeData.serviceUrl + "NhomHangHoa",
                        data: JSON.stringify(NhomHangHoa),
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: $.ColorLifeData.loader(true),
                        complete: $.ColorLifeData.loader(false),
                        success: function (result) {
                            if (result.Success) {
                                $.ColorLifeData.notifySuccess(result.Message);
                                $('.idnhomhanghoa').each(function () {
                                    $(this).append('<option value=' + NhomHangHoa.IDNhomHangHoa + ' selected="selected">' + NhomHangHoa.TenNhomHangHoa + '</option>');
                                });
                                
                                obj.find("#IDNhomHangHoa").val(result.Data);
                                obj.find("#TenNhomHangHoa").val('');
                                obj.find("#GhiChu").val('');
                                obj.modal('hide');
                               
                                
                            }
                            else {
                                $.ColorLifeData.notifyError(result.Message);
                            }
                        },
                        error: function (errormessage) {
                            $.ColorLifeData.notifyError(errormessage.responseText);
                        }
                    });
                }
                else {
                    $(obj).find('.form-group').removeClass('has-error has-success').addClass('has-error');
                    $.ColorLifeData.notifyError('Bạn cần nhập dữ liệu đầy đủ.');
                }
                return false;
            },

            jsonGetNhomHangHoa: function (id) {

                
                var obj = $("#editModal");

                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "NhomHangHoa/" + id,
                    // data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            obj.find("#IDNhomHangHoa").val(result.Data.IDNhomHangHoa);
                            obj.find("#TenNhomHangHoa").val(result.Data.TenNhomHangHoa);
                            obj.find("#GhiChu").val(result.Data.GhiChu);
                            obj.modal('show');
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });

                return false;
            },
            jsonUpdateNhomHangHoa: function (form) {
                var obj = $(form);
                var NhomHangHoa = {
                    IDNhomHangHoa: obj.find("#IDNhomHangHoa").val(),
                    TenNhomHangHoa: obj.find("#TenNhomHangHoa").val(),
                    GhiChu: obj.find("#GhiChu").val(),
                };
                if (NhomHangHoa.IDNhomHangHoa.length && NhomHangHoa.TenNhomHangHoa.length > 3) {
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "NhomHangHoa",
                    data: JSON.stringify(NhomHangHoa),
                    type: "PUT",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                    },
                    success: function (result) {
                        if (result.Success) {
                            $.ColorLifeData.notifySuccess(result.Message);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                }
                else {
                    $(obj).find('.form-group').removeClass('has-error has-success').addClass('has-error');
                    $.ColorLifeData.notifyError('Bạn cần nhập dữ liệu đầy đủ.');
                }
                return false;
            },
            jsonEditableNhomHangHoa: function () {
                $('#NhomHangHoa a.editable').editable({
                    type: 'text',
                    name: 'TenNhomHangHoa',

                    title: 'Nhập vào tên Nhóm Hàng Hóa',

                    ajaxOptions: {
                        type: 'put',
                        dataType: 'json'
                    },
                    validate: function (value) {
                        if ($.trim(value) == '') {
                            return 'This field is required';
                        }
                    },
                    url: function (params) {

                        var NhomHangHoa = {
                            IDNhomHangHoa: params.pk,
                            TenNhomHangHoa: params.value,

                        };
                        return $.ajax({
                            type: 'PUT',
                            url: $.ColorLifeData.serviceUrl + 'NhomHangHoa',
                            data: JSON.stringify(NhomHangHoa),
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            async: true,
                            cache: false,
                            timeout: 10000,
                            success: function (result) {
                                if (result.Success) {
                                    $.ColorLifeData.notifySuccess(result.Message);
                                }
                                else {
                                    $.ColorLifeData.notifyError(result.Message);
                                }
                            },
                            error: function (errormessage) {
                                $.ColorLifeData.notifyError(errormessage.responseText);
                            }
                        });
                    }
                });
            },
            // Kho
            jsonInsertKho: function (form) {
                // var dto = { "id": id, "roleId": rId };
                var obj = $(form);
                var Kho = {
                    IDKho: obj.find("#IDKho").val(),
                    TenKho: obj.find("#TenKho").val(),
                    GhiChu: obj.find("#GhiChu").val(),
                };

                // alert(NhomHangHoa.TenNhomHangHoa);
                if (Kho.IDKho.length && Kho.TenKho.length > 3) {
                    $.ajax({
                        url: $.ColorLifeData.serviceUrl + "Kho",
                        data: JSON.stringify(Kho),
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: $.ColorLifeData.loader(true),
                        complete: $.ColorLifeData.loader(false),
                        success: function (result) {
                            if (result.Success) {
                                $.ColorLifeData.notifySuccess(result.Message);
                                obj.find("#IDKho").val(result.Data);
                                obj.find("#TenKho").val('');
                                obj.find("#GhiChu").val('');
                                // obj.modal('hide');
                            }
                            else {
                                $.ColorLifeData.notifyError(result.Message);
                            }
                        },
                        error: function (errormessage) {
                            $.ColorLifeData.notifyError(errormessage.responseText);
                        }
                    });
                }
                else {
                    $(obj).find('.form-group').removeClass('has-error has-success').addClass('has-error');
                    $.ColorLifeData.notifyError('Bạn cần nhập dữ liệu đầy đủ.');
                }
                return false;
            },
            jsonGetKho: function (id) { 
                var obj = $("#editModal");

                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "Kho/" + id,
                    // data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {


                            obj.find("#IDKho").val(result.Data.IDKho);
                            obj.find("#TenKho").val(result.Data.TenKho);
                            obj.find("#GhiChu").val(result.Data.GhiChu);
                            obj.modal('show');
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });

                return false;
            },
            jsonUpdateKho: function (form) {
                var obj = $(form);
                var Kho = {
                    IDKho: obj.find("#IDKho").val(),
                    TenKho: obj.find("#TenKho").val(),
                    GhiChu: obj.find("#GhiChu").val(),
                };
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "Kho",
                    data: JSON.stringify(Kho),
                    type: "PUT",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                    },
                    success: function (result) {
                        if (result.Success) {
                            $.ColorLifeData.notifySuccess(result.Message);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },
            jsonEditableKho:function(){
                $('#Kho a.editable').editable({
                    type: 'text',
                    name: 'TenKho',

                    title: 'Nhập vào tên kho',

                    ajaxOptions: {
                        type: 'put',
                        dataType: 'json'
                    },
                    validate: function (value) {
                        if ($.trim(value) == '') {
                            return 'This field is required';
                        }
                    },
                    url: function (params) {

                        var Kho = {
                            IDKho: params.pk,
                            TenKho: params.value,
                        };
                        return $.ajax({
                            type: 'PUT',
                            url: $.ColorLifeData.serviceUrl + 'Kho',
                            data: JSON.stringify(Kho),
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            async: true,
                            cache: false,
                            timeout: 10000,
                            success: function (result) {
                                if (result.Success) {
                                    $.ColorLifeData.notifySuccess(result.Message);
                                }
                                else {
                                    $.ColorLifeData.notifyError(result.Message);
                                }
                            },
                            error: function (errormessage) {
                                $.ColorLifeData.notifyError(errormessage.responseText);
                            }
                        });
                    }
                });
            },
            // DonViTinh
            jsonInsertDonViTinh: function (form) {
                // var dto = { "id": id, "roleId": rId };
                var obj = $(form);
                var DonViTinh = {
                    IDDVT: obj.find("#IDDVT").val(),
                    TenDVT: obj.find("#TenDVT").val(),
                };

                // alert(NhomHangHoa.TenNhomHangHoa);
                if (DonViTinh.IDDVT.length && DonViTinh.TenDVT.length > 3) {
                    $.ajax({
                        url: $.ColorLifeData.serviceUrl + "DonViTinh",
                        data: JSON.stringify(DonViTinh),
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: $.ColorLifeData.loader(true),
                        complete: $.ColorLifeData.loader(false),
                        success: function (result) {
                            if (result.Success) {
                                $.ColorLifeData.notifySuccess(result.Message);

                                obj.find("#IDDVT").val('');
                                obj.find("#TenDVT").val('');
                                var tr;
                                tr = $('<tr id=' + DonViTinh.IDDVT + '/>');
                                tr.append("<td class='with-checkbox'> <input type=\"checkbox\" name=\"check\" class=\"check\" value=" + DonViTinh.IDDVT + "></td>");
                                tr.append("<td class='table-item-title'>" + DonViTinh.IDDVT + "</td>");
                                tr.append("<td class='table-item-title'>" + DonViTinh.TenDVT + "</td>");
                                tr.append("<td class='table-item-title'></td>");
                                tr.append("<td class='td-actions'></td>");
                                
                                $('table[id^=DonViTinh] tbody').append(tr);
                                
                                // obj.modal('hide');
                            }
                            else {
                                $.ColorLifeData.notifyError(result.Message);
                            }
                        },
                        error: function (errormessage) {
                            $.ColorLifeData.notifyError(errormessage.responseText);
                        }
                    });
                }
                else {
                    $(obj).find('.form-group').removeClass('has-error has-success').addClass('has-error');
                    $.ColorLifeData.notifyError('Bạn cần nhập dữ liệu đầy đủ.');
                }
                return false;
            },
            jsonInsertDonViTinh2: function (form) {
                // var dto = { "id": id, "roleId": rId };
                var obj = $(form);
                var DonViTinh = {
                    IDDVT: obj.find("#IDDVT").val(),
                    TenDVT: obj.find("#TenDVT").val(),

                };

                // alert(NhomHangHoa.TenNhomHangHoa);
                if (DonViTinh.IDDVT.length && DonViTinh.TenDVT.length > 3) {
                    $.ajax({
                        url: $.ColorLifeData.serviceUrl + "DonViTinh",
                        data: JSON.stringify(DonViTinh),
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: $.ColorLifeData.loader(true),
                        complete: $.ColorLifeData.loader(false),
                        success: function (result) {
                            if (result.Success) {
                                $.ColorLifeData.notifySuccess(result.Message);
                                $('.donvitinh').each(function () {
                                    $(this).append('<option value=' + DonViTinh.IDDVT + ' selected="selected">' + DonViTinh.TenDVT + '</option>');
                                });
                                
                                obj.find("#IDDVT").val('');
                                obj.find("#TenDVT").val('');
                                obj.modal('hide');
                            }
                            else {
                                $.ColorLifeData.notifyError(result.Message);
                            }
                        },
                        error: function (errormessage) {
                            $.ColorLifeData.notifyError(errormessage.responseText);
                        }
                    });
                }
                else {
                    $(obj).find('.form-group').removeClass('has-error has-success').addClass('has-error');
                    $.ColorLifeData.notifyError('Bạn cần nhập dữ liệu đầy đủ.');
                }
                return false;
            },
            jsonGetDonViTinh: function (id) {
          
                var obj = $("#editModal");

                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "DonViTinh/" + id,
                    // data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                     beforeSend: $.ColorLifeData.loader(true),
                      complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {


                            obj.find("#IDDVT").val(result.Data.IDDVT);
                            obj.find("#TenDVT").val(result.Data.TenDVT);

                            obj.modal('show');
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });

                return false;
            },
            jsonUpdateDonViTinh: function (form) {
                var obj = $(form);
                var DonViTinh = {
                    IDDVT: obj.find("#IDDVT").val(),
                    TenDVT: obj.find("#TenDVT").val(),

                }; if (DonViTinh.IDDVT.length && DonViTinh.TenDVT.length > 3) {
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "DonViTinh",
                    data: JSON.stringify(DonViTinh),
                    type: "PUT",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                    },
                    success: function (result) {
                        if (result.Success) {
                            $.ColorLifeData.notifySuccess(result.Message);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                }
                else {
                    $(obj).find('.form-group').removeClass('has-error has-success').addClass('has-error');
                    $.ColorLifeData.notifyError('Bạn cần nhập dữ liệu đầy đủ.');
                }
                return false;
            },
            jsonEditableDonViTinh: function () {
                $('#DonViTinh a.editable').editable({
                    type: 'text',
                    name: 'TenDVT',

                    title: 'Nhập vào tên Đơn vị tính',
                    // mode: 'inline', // popup or inline
                    ajaxOptions: {
                        type: 'put',
                        dataType: 'json'
                    },
                    validate: function (value) {
                        if ($.trim(value) == '') {
                            return 'This field is required';
                        }
                    },
                    url: function (params) {

                        var DonViTinh = {
                            IDDVT: params.pk,
                            TenDVT: params.value,

                        };
                        return $.ajax({
                            type: 'PUT',
                            url: $.ColorLifeData.serviceUrl + 'DonViTinh',
                            data: JSON.stringify(DonViTinh),
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            async: true,
                            cache: false,
                            timeout: 10000,
                            success: function (result) {
                                if (result.Success) {
                                    $.ColorLifeData.notifySuccess(result.Message);
                                }
                                else {
                                    $.ColorLifeData.notifyError(result.Message);
                                }
                            },
                            error: function (errormessage) {
                                $.ColorLifeData.notifyError(errormessage.responseText);
                            }
                        });
                    }
                });
            },
            // TaiKhoan           
            jsonInsertTaiKhoan: function () {
                // var dto = { "id": id, "roleId": rId };
                var obj = $("#addNewModal");
                var TaiKhoan = {
                    TenTK: obj.find("#TenTK").val(),
                    SoTaiKhoan: obj.find("#SoTaiKhoan").val(),
                    SoTien: obj.find("#SoTien").val(),
                    GhiChu: obj.find("#GhiChu").val(),
                };

                // alert(NhomHangHoa.TenNhomHangHoa);
                if (TaiKhoan.TenTK.length && TaiKhoan.SoTaiKhoan.length > 3) {
                    $.ajax({
                        url: $.ColorLifeData.serviceUrl + "TaiKhoan",
                        data: JSON.stringify(TaiKhoan),
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: $.ColorLifeData.loader(true),
                        complete: $.ColorLifeData.loader(false),
                        success: function (result) {
                            if (result.Success) {
                                $.ColorLifeData.notifySuccess(result.Message);

                                obj.find("#TenTK").val('');
                                obj.find("#SoTaiKhoan").val('');
                                obj.find("#SoTien").val('');
                                obj.find("#GhiChu").val('');
                                // obj.modal('hide');
                            }
                            else {
                                $.ColorLifeData.notifyError(result.Message);
                            }
                        },
                        error: function (errormessage) {
                            $.ColorLifeData.notifyError(errormessage.responseText);
                        }
                    });
                }
                else {
                    $(obj).find('.form-group').removeClass('has-error has-success').addClass('has-error');
                    $.ColorLifeData.notifyError('Bạn cần nhập dữ liệu đầy đủ.');
                }
                return false;
            },
            jsonGetTaiKhoan: function (id) {

                var obj = $("#editModal");

                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "TaiKhoan/" + id,
                    // data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            obj.find("#IDTaiKhoan").val(result.Data.IDTaiKhoan);
                            obj.find("#TenTK").val(result.Data.TenTK);
                            obj.find("#SoTaiKhoan").val(result.Data.SoTaiKhoan);
                            obj.find("#SoTien").val(result.Data.SoTien);
                            obj.find("#GhiChu").val(result.Data.GhiChu);
                            obj.modal('show');
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });

                return false;
            },
            jsonUpdateTaiKhoan: function () {
                var obj = $("#editModal");
                var TaiKhoan = {
                    IDTaiKhoan: obj.find("#IDTaiKhoan").val(),
                    TenTK: obj.find("#TenTK").val(),
                    SoTaiKhoan: obj.find("#SoTaiKhoan").val(),
                    SoTien: obj.find("#SoTien").val(),
                    GhiChu: obj.find("#GhiChu").val(),
                };
                if (TaiKhoan.TenTK.length && TaiKhoan.SoTaiKhoan.length > 3) {
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "TaiKhoan",
                    data: JSON.stringify(TaiKhoan),
                    type: "PUT",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                    },
                    success: function (result) {
                        if (result.Success) {
                            $.ColorLifeData.notifySuccess(result.Message);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                }
                else {
                    $(obj).find('.form-group').removeClass('has-error has-success').addClass('has-error');
                    $.ColorLifeData.notifyError('Bạn cần nhập dữ liệu đầy đủ.');
                }
                return false;
            },

           

            // KhachHang
            jsonInsertKhachHang: function (form) {
                // var dto = { "id": id, "roleId": rId };
                var obj = $(form);
                var KhachHang = {
                    IDKhachHang: obj.find("#IDKhachHang").val(),
                    HoTenKhachHang: obj.find("#HoTenKhachHang").val(),
                    DiaChi: obj.find("#DiaChi").val(),
                    SoDienThoai: obj.find("#SoDienThoai").val(),
                    Email: obj.find("#Email").val(),
                    MaSoThue: obj.find("#MaSoThue").val(),
                    TaiKhoan: obj.find("#TaiKhoan").val(),
                    NguoiLienHe: obj.find("#NguoiLienHe").val(),
                    NoDau: obj.find("#NoDau").val(),
                    Ngung: obj.find("#Ngung").val(),
                    Kho: obj.find("#Kho").val(),
                    GhiChu: obj.find("#GhiChu").val(),
                };

                // alert(NhomHangHoa.TenNhomHangHoa);
                if (KhachHang.IDKhachHang.length && KhachHang.HoTenKhachHang.length > 3) {
                    $.ajax({
                        url: $.ColorLifeData.serviceUrl + "KhachHang",
                        data: JSON.stringify(KhachHang),
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: $.ColorLifeData.loader(true),
                        complete: $.ColorLifeData.loader(false),
                        success: function (result) {
                            if (result.Success) {
                                $.ColorLifeData.notifySuccess(result.Message);
                                obj.find("#IDKhachHang").val(result.Data);

                                obj.find("#HoTenKhachHang").val();
                                obj.find("#DiaChi").val();
                                obj.find("#SoDienThoai").val();
                                obj.find("#Email").val();
                                obj.find("#MaSoThue").val();
                                obj.find("#TaiKhoan").val();
                                obj.find("#NguoiLienHe").val();
                                obj.find("#NoDau").val();
                                obj.find("#Ngung").val();
                                obj.find("#Kho").val();
                                obj.find("#GhiChu").val();
                                // obj.modal('hide');
                            }
                            else {
                                $.ColorLifeData.notifyError(result.Message);
                            }
                        },
                        error: function (errormessage) {
                            $.ColorLifeData.notifyError(errormessage.responseText);
                        }
                    });
                }
                else {
                    $(obj).find('.form-group').removeClass('has-error has-success').addClass('has-error');
                    $.ColorLifeData.notifyError('Bạn cần nhập dữ liệu đầy đủ.');
                }
                return false;
            },
            jsonGetKhachHang: function (id) {
                var obj = $("#editModal");

                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "KhachHang/" + id,
                    // data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {


                            obj.find("#IDKhachHang").val(result.Data.IDKhachHang);

                            obj.find("#HoTenKhachHang").val(result.Data.HoTenKhachHang);
                            obj.find("#DiaChi").val(result.Data.DiaChi);
                            obj.find("#SoDienThoai").val(result.Data.SoDienThoai);
                            obj.find("#Email").val(result.Data.Email);
                            obj.find("#MaSoThue").val(result.Data.MaSoThue);
                            obj.find("#TaiKhoan").val(result.Data.TaiKhoan);
                            obj.find("#NguoiLienHe").val(result.Data.NguoiLienHe);
                            obj.find("#NoDau").val(result.Data.NoDau);
                            obj.find("#Ngung").val(result.Data.Ngung);
                            obj.find("#Kho").val(result.Data.Kho);
                            obj.find("#GhiChu").val(result.Data.GhiChu);
                            obj.modal('show');
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });

                return false;
            },
            jsonUpdateKhachHang: function (form) {
                var obj = $(form);
                var KhachHang = {
                    IDKhachHang: obj.find("#IDKhachHang").val(),
                    HoTenKhachHang: obj.find("#HoTenKhachHang").val(),
                    DiaChi: obj.find("#DiaChi").val(),
                    SoDienThoai: obj.find("#SoDienThoai").val(),
                    Email: obj.find("#Email").val(),
                    MaSoThue: obj.find("#MaSoThue").val(),
                    TaiKhoan: obj.find("#TaiKhoan").val(),
                    NguoiLienHe: obj.find("#NguoiLienHe").val(),
                    NoDau: obj.find("#NoDau").val(),
                    Ngung: obj.find("#Ngung").val(),
                    Kho: obj.find("#Kho").val(),
                    GhiChu: obj.find("#GhiChu").val(),
                };
                if (KhachHang.IDKhachHang.length && KhachHang.HoTenKhachHang.length > 3) {
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "KhachHang",
                    data: JSON.stringify(KhachHang),
                    type: "PUT",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                    },
                    success: function (result) {
                        if (result.Success) {
                            $.ColorLifeData.notifySuccess(result.Message);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                }
                else {
                    $(obj).find('.form-group').removeClass('has-error has-success').addClass('has-error');
                    $.ColorLifeData.notifyError('Bạn cần nhập dữ liệu đầy đủ.');
                }
                return false;
            },

            
            
            // AccountGroup
            jsonInsertAccountGroup: function (form) {
                // var dto = { "id": id, "roleId": rId };
                var obj = $(form);
                var AccountGroup = {
                    
                    Name: obj.find("#Name").val(),

                };

                // alert(NhomHangHoa.TenNhomHangHoa);
                if (AccountGroup.Name.length > 3) {
                    $.ajax({
                        url: $.ColorLifeData.serviceUrl + "AccountGroup",
                        data: JSON.stringify(AccountGroup),
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: $.ColorLifeData.loader(true),
                        complete: $.ColorLifeData.loader(false),
                        success: function (result) {
                            if (result.Success) {
                                $.ColorLifeData.notifySuccess(result.Message);

                                 
                                obj.find("#Name").val('');
                                // obj.modal('hide');
                            }
                            else {
                                $.ColorLifeData.notifyError(result.Message);
                            }
                        },
                        error: function (errormessage) {
                            $.ColorLifeData.notifyError(errormessage.responseText);
                        }
                    });
                }
                else {
                    $(obj).find('.form-group').removeClass('has-error has-success').addClass('has-error');
                    $.ColorLifeData.notifyError('Bạn cần nhập dữ liệu đầy đủ.');
                }
                return false;
            },
            jsonInsertAccountGroup2: function (form) {
                // var dto = { "id": id, "roleId": rId };
                var obj = $(form);
                var AccountGroup = {
                    
                    Name: obj.find("#Name").val(),

                };

                // alert(NhomHangHoa.TenNhomHangHoa);
                if (AccountGroup.Name.length > 3) {
                    $.ajax({
                        url: $.ColorLifeData.serviceUrl + "AccountGroup",
                        data: JSON.stringify(AccountGroup),
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: $.ColorLifeData.loader(true),
                        complete: $.ColorLifeData.loader(false),
                        success: function (result) {
                            if (result.Success) {
                                $.ColorLifeData.notifySuccess(result.Message);
                                $('.donvitinh').each(function () {
                                    $(this).append('<option value=' + DonViTinh.IDDVT + ' selected="selected">' + DonViTinh.TenDVT + '</option>');
                                });

                                obj.find("#IDDVT").val('');
                                obj.find("#TenDVT").val('');
                                obj.modal('hide');
                            }
                            else {
                                $.ColorLifeData.notifyError(result.Message);
                            }
                        },
                        error: function (errormessage) {
                            $.ColorLifeData.notifyError(errormessage.responseText);
                        }
                    });
                }
                else {
                    $(obj).find('.form-group').removeClass('has-error has-success').addClass('has-error');
                    $.ColorLifeData.notifyError('Bạn cần nhập dữ liệu đầy đủ.');
                }
                return false;
            },
            jsonGetAccountGroup: function (id) {

                var obj = $("#editModal");

                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "AccountGroup/" + id,
                    // data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {


                            obj.find("#GroupID").val(result.Data.GroupID);
                            obj.find("#Name").val(result.Data.Name);

                            obj.modal('show');
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });

                return false;
            },
            jsonUpdateAccountGroup: function (form) {
                var obj = $(form);
                var AccountGroup = {
                    GroupID: obj.find("#GroupID").val(),
                    Name: obj.find("#Name").val(),

                }; if (AccountGroup.GroupID.length && AccountGroup.Name.length > 3) {
                    $.ajax({
                        url: $.ColorLifeData.serviceUrl + "AccountGroup",
                        data: JSON.stringify(AccountGroup),
                        type: "PUT",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                        },
                        success: function (result) {
                            if (result.Success) {
                                $.ColorLifeData.notifySuccess(result.Message);
                            }
                            else {
                                $.ColorLifeData.notifyError(result.Message);
                            }
                        },
                        error: function (errormessage) {
                            $.ColorLifeData.notifyError(errormessage.responseText);
                        }
                    });
                }
                else {
                    $(obj).find('.form-group').removeClass('has-error has-success').addClass('has-error');
                    $.ColorLifeData.notifyError('Bạn cần nhập dữ liệu đầy đủ.');
                }
                return false;
            },
            jsonEditableAccountGroup: function () {
                $('#AccountGroup a.editable').editable({
                    type: 'text',
                    name: 'Name',

                    title: 'Nhập vào tên nhóm',
                    // mode: 'inline', // popup or inline
                    ajaxOptions: {
                        type: 'put',
                        dataType: 'json'
                    },
                    validate: function (value) {
                        if ($.trim(value) == '') {
                            return 'This field is required';
                        }
                    },
                    url: function (params) {

                        var AccountGroup = {
                            GroupID: params.pk,
                            Name: params.value,

                        };
                        return $.ajax({
                            type: 'PUT',
                            url: $.ColorLifeData.serviceUrl + 'AccountGroup',
                            data: JSON.stringify(AccountGroup),
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            async: true,
                            cache: false,
                            timeout: 10000,
                            success: function (result) {
                                if (result.Success) {
                                    $.ColorLifeData.notifySuccess(result.Message);
                                }
                                else {
                                    $.ColorLifeData.notifyError(result.Message);
                                }
                            },
                            error: function (errormessage) {
                                $.ColorLifeData.notifyError(errormessage.responseText);
                            }
                        });
                    }
                });
            },
            jsonAddAccountToGroup: function (id, gId) {
                var AccountInGroup = {
                    AccountID: id,
                    GroupID: gId,
                };
                // alert(AccountGroupFunction.GroupID +'__'+ AccountGroupFunction.FunctionID);
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "Account/AddAccountToGroup/" + id,
                    data: JSON.stringify(AccountInGroup),
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {

                            $.ColorLifeData.notifySuccess(result.Message);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                }); return false;
            },
            // Permission
            jsonAddFunctionToGroup: function (id, fId) {
                var dto = { "id": id, "fId": fId };
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "AccountGroup/AddFunctionToGroup?id=" + id + "&fId=" + fId,
                    //data: JSON.stringify(dto),
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {

                            $.ColorLifeData.notifySuccess(result.Message);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                }); return false;
            },
            jsonRemoveFunctionFromGroup: function (id, fId) {
                var dto = { "id": id, "fId": fId };
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "AccountGroup/RemoveFunctionFromGroup?id=" + id + "&fId=" + fId,
                    //data: JSON.stringify(dto),
                    type: "DELETE",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {

                            $.ColorLifeData.notifySuccess(result.Message);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                }); return false;
            },
            jsonGrantPermisstion: function (id, fId) {
                var AccountGroupFunction = {
                    GroupID: id,
                    FunctionID: fId,                    
                };
               // alert(AccountGroupFunction.GroupID +'__'+ AccountGroupFunction.FunctionID);
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "AccountGroup/GrantPermission/1",
                    data: JSON.stringify(AccountGroupFunction),
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {

                            $.ColorLifeData.notifySuccess(result.Message);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                }); return false;
            },
            jsonGrantAllPermisstion: function (id) {
                var PermissionModel = {
                    GroupID: 1,
                    FunctionID: 1,
                    GroupFunctionID:id
                };
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "Permission/GrantAllPermisstion/1",
                    data: JSON.stringify(PermissionModel),
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            $.ColorLifeData.notifySuccess(result.Message);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                
                return false;
            },
            
            // , tiep tuc ham khac            
        }// end ColorLife
    }); // end extend
})(jQuery);