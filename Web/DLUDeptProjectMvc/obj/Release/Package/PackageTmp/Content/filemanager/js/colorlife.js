(function ($) {
    $.extend({
        ColorLife: {
            returnOpenerId: '', // insert file to opener element Id
            returnFullUrl: false, // true: return full url same as: http://your-domain.com/upload/file.jpg            
            tinymce:'',            
            serviceUrl: '/FileManager/',
            iconsPath: '/content/filemanager/icons/',
            imageThumbnail: '/ThumbnailHandler.axd?img=',

            txtPath: '#txtPath',
            rootFolder: '/Uploads',
            currentFolder: '',
            currentFile: '',
            prevFolder: '',            
            Copy: null,
            Cut: null,
            containerId: '#_fileContainer',
            loadingId: '#_loading',
            loaderID: '#loader',            
            itemInfoId: '#_itemInfo',
            itemPreview: '#_preview',
            itemClass: 'xfm-item',
            itemSelectedClass: 'xfm-item-selected',
            itemSelectedNoFocusClass: 'xfm-item-selected-nofocus',
            itemHoverClass: 'xfm-item-over',
            itemNameClass: 'xfm-item-name',
            itemCutClass: 'xfm-item-cut',
            imagePattern:/\.(jpg|JPG|jpeg|JPEG|gif|GIF|png|PNG)$/,
            rememberLastPath: true, // save last path
            pageIndex: 1, // current page index          
            filter: '0', //* all files and folders , *.* only files
            filterExt: 0, // All=0, Image=1,File=2,Archive=3,Music=4,Video=5
            sortOrder: '', // sort "Name DESC","Size","Date", LastWriteDate asc/desc
            searchTxt: '',
            multiSelect: false,
            clipboardData: '',
            clipboardType: 'cut',  // cut || copy          
            items: {},            
            viewType: 'list', //list,detail,icons
            messages: {
                mustSelectItem: 'Bạn phải chọn file hoặc thư mục nào đó',
                deleteItemsConfirm: 'Are you sure you want to delete the selected "{0}" files?',
                folderEmpty: 'This folder is empty.',
                overwriteExists: 'Bạn có muốn ghi đè file / thư mục đã có?',
                folderInfo: '{0} folders, {1} files',
                //fileCount:'Showing page {0} through {1} out of {2} in this folder'                
                searchFile: 'Search files',
                uploadFileFromServerSuccessful: 'Upload file successful',
                uploadFileFromServerFaild: 'Upload file faild',
                uploadFileFromServerValidate: 'Please enter file address',
                pleaseSelectedFolder:'Please select folder',
            },
            // Cookie
            setCookie: function (name, value, days) {
                var expires;

                if (days) {
                    var date = new Date();
                    date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
                    expires = "; expires=" + date.toGMTString();
                } else {
                    expires = "";
                }
                document.cookie = escape(name) + "=" + escape(value) + expires + "; path=/";
            },
            getCookie: function (name) {
                var nameEQ = escape(name) + "=";
                var ca = document.cookie.split(';');
                for (var i = 0; i < ca.length; i++) {
                    var c = ca[i];
                    while (c.charAt(0) === ' ') c = c.substring(1, c.length);
                    if (c.indexOf(nameEQ) === 0) return unescape(c.substring(nameEQ.length, c.length));
                }
                return null;
            },

            removeCookie: function (name) {
                $.ColorLife.setCookie(name, "", -1);
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

            String: {
                trim: function () {
                    return this.replace(/^\s+|\s+$/g, '');
                },
                 
                isNullOrEmpty: function (value) {
                    if (value) {
                        if (typeof (value) == 'string') {
                            if (value.length > 0)
                                return false;
                        }
                        if (value != null)
                            return false;
                    }
                    return true;

                },

                replace: function (source, oldVal, newVal) {
                    var result = source;
                    //result = result.replace(oldVal, newVal);
                    result = result.replace(new RegExp(oldVal, 'ig'), newVal);
                    //result = result.replace(new RegExp('\\' + oldVal + '\\', 'g'), newVal);

                    return result;
                },

                //alert( $.StringEx.format("Hello {0}.","Nasa8x") );
                format: function (format, args) {
                    var result = format;
                    for (var i = 1; i < arguments.length; i++) {
                        result = result.replace(new RegExp('\\{' + (i - 1) + '\\}', 'g'), arguments[i]);
                    }
                    return result;
                },
                startsWith: function (prefix, ignoreCase) {
                    if (!prefix) return false;
                    if (prefix.length > this.length) return false;
                    if (ignoreCase) {
                        if (ignoreCase == true) {
                            return (this.substr(0, prefix.length).toUpperCase() == prefix.toUpperCase());
                        }
                    }
                    return (this.substr(0, prefix.length) === prefix);
                },
                endsWith: function (suffix, ignoreCase) {
                    if (!suffix) return false;
                    if (suffix.length > this.length) return false;
                    if (ignoreCase) {
                        if (ignoreCase == true) {
                            return (this.substr(this.length - suffix.length).toUpperCase() == suffix.toUpperCase());
                        }
                    }
                    return (this.substr(this.length - suffix.length) === suffix);
                }
            },
            // HELPER //
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
            _undefined: function () {
                for (var i = 0; i < arguments.length; i++)
                    if (typeof arguments[i] != 'undefined') return false;
                return true;
            },
            _viewport: function () {
                var d = document.documentElement, b = document.body, w = window;
                return jQuery.extend(
                    // jQuery.browser.msie ?
                    /msie/.test(navigator.userAgent.toLowerCase()) ?
                        { left: b.scrollLeft || d.scrollLeft, top: b.scrollTop || d.scrollTop} :
                        { left: w.pageXOffset, top: w.pageYOffset },
                    !this._undefined(w.innerWidth) ?
                        { width: w.innerWidth, height: w.innerHeight} :
                        (!this._undefined(d) && !this._undefined(d.clientWidth) && d.clientWidth != 0 ?
                            { width: d.clientWidth, height: d.clientHeight} :
                            { width: b.clientWidth, height: b.clientHeight }));
            },
            _overlay: function (status, callback) {
                switch (status) {
                    case 'show':
                        $("body").append('<div id="_overlay"></div>');
                        $("#_overlay").css({
                            position: 'absolute',
                            zIndex: 9000,
                            top: '0px',
                            left: '0px',
                            width: '100%',
                            background: '#333333',
                            opacity: 0.2,
                            height: $(document).height()

                        }).fadeIn(500, function () {
                            if ($.isFunction(callback))
                                callback.apply();
                        });
                        break;
                    case 'hide':
                        $('#_overlay').fadeOut('fast', function () {
                            $("#_overlay").remove();
                            if ($.isFunction(callback))
                                callback.apply();

                        });

                        break;
                }
            },
            elementsCenterScreen: function () {
                var _v = this._viewport();
                for (var i = 0; i < arguments.length; i++) {
                    var _left = (_v.width - $('#' + arguments[i]).outerWidth()) / 2;
                    var _top = _v.top + (_v.height + $('#' + arguments[i]).outerHeight()) / 2;
                    $('#' + arguments[i]).css({ top: _top, left: _left });

                }

            },
            showLoading: function (sender, args) {
                this._overlay('show');
                var _v = this._viewport();
                var _left = (_v.width - $(this.loadingId).outerWidth()) / 2;
                var _top = _v.top + (_v.height - $(this.loadingId).outerHeight()) / 2;
                $(this.loadingId).css({ top: _top, left: _left }).show();

                //alert('Loading');

            },
            hideLoading: function (sender, args) {
                $(this.loadingId).fadeOut();
                this._overlay('hide');
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

            insertFile: function () {
                var url = this.currentFile;
                var win = this.getParentWin();                
                if (this.returnOpenerId != null && this.returnOpenerId != '') {
                    win.document.getElementById(this.returnOpenerId).value = url;
                    self.close();
                }

                else if (this.tinymce) { // Plugin TinyMce 
                    if (this.tinymce == 'preview') {
                        $.ColorLife.tyinyMCE_insertFile(_item, _item);  // Xem anh truoc khi chen
                    }
                    else {
                        alert('Chen anh truc tiep, khong can xem truoc');
                        
                        tinyMCE.activeEditor.selection.setContent("HAAAAA");

                         ImageDialog.insert(_item, _item);                       
                    }
                }

                else if (navigator.appName.indexOf('Microsoft') != -1) {
                    window.returnValue = url;
                    self.close();
                }
            },
            tyinyMCE_insertFile: function (fileName, title) {
                var win = tinyMCEPopup.getWindowArg("window");
                win.document.getElementById(tinyMCEPopup.getWindowArg("input")).value = fileName;
                if (typeof (win.ImageDialog) != "undefined") {

                    if (win.ImageDialog.getImageData)
                        win.ImageDialog.getImageData();

                    if (win.ImageDialog.showPreviewImage)
                        win.ImageDialog.showPreviewImage(fileName);
                }
                tinyMCEPopup.close();
            },
            isImage: function (_fileExt) {                             
                var reg = $.ColorLife.imagePattern; // /\.(jpg|JPG|gif|doc|pdf)$/;
                return reg.test(_fileExt);
            },
            checkExtension: function (text) {
                var reg = /\.(jpg|gif|doc|pdf)$/
                return reg.test(text);
            },
            checkExt: function (filename) {//use in a form event or ina input
                var ext = /^.+\.([^.]+)$/.exec(filename);
                return ext == null ? "" : ext[1];
            },
            saveFileToServer: function () {
                var html = '';
                var fileUrl = $("#txtSaveFileToServer").val();
                if (fileUrl.length > 0 && $.ColorLife.isImage(fileUrl)) {
                    var params = "{'folderPath':'" + $.ColorLife.currentFolder + "','fileUrl':'" + fileUrl + "'}";
                    $.ColorLife.ajaxCallback('SaveFileToServer', params, $.ColorLife.refresh);
                    
                    html+='<div class="alert alert-info">';
                    html += '<b><i class="fa fa-info"></i> Thông báo:</b> ' + $.ColorLife.messages.uploadFileFromServerSuccessful;
                    html += '  </div>';
                    $("#uploadMsg").html(html);
                    $("#uploadMsg").fadeIn().delay(3000).fadeOut();
                }
                else {

                    html += '<div class="alert alert-danger">';
                    html += '<b><i class="fa fa-exclamation"></i> Thông báo:</b> ' + $.ColorLife.messages.uploadFileFromServerValidate;
                    html += '  </div>';
                    $("#uploadMsg").html(html);
                    $("#uploadMsg").fadeIn().delay(3000).fadeOut();
                }
            },
            

            ajaxCallback: function (cmd, params, callback) {
                this.showLoading();
                $(this.itemInfoId).html('');
                $(this.itemPreview).html('');
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: (params),
                    url: this.serviceUrl + cmd,
                    beforeSend: function () {
                        $(this.loaderID).show();
                    },
                    success: function (response) {                        
                        var result = (typeof response) == 'string' ? eval('(' + response + ')') : response;
                        if ($.isFunction(callback)) {
                            callback(result);
                        }
                        $(this.loaderID).hide();
                        $.ColorLife.hideLoading();
                    },
                    error: function (xhr, status) {
                        alert(status);
                    }
                });
            },
            selectItemInfo: function (i) {
                // clear class item no selected
                //   $('.' + $.xFM.itemSelectedClass).removeClass($.xFM.itemSelectedNoFocusClass);
                var html1 = "";
                var _item = $.ColorLife.items[i];
               
                var _icon = this.iconsPath + 'none.gif';
                if (_item.IsFolder) {
                    _icon = this.iconsPath + 'folder.gif';
                }
                else if (this.isImage(_item.Name)) {
                    _icon = this.imageThumbnail + _item.FullName;
                    html1 = "<img src=" + _item.FullName.replace(/' '/g, "%20") + " width='100%'/>";
                }
                else if (_item.IsExistsIcon) {
                    var ext = this.checkExt(_item.FullName);
                    switch (ext.toLowerCase()) {

                        case "swf":
                            html1 += '<embed type="application/x-shockwave-flash" quality="high" wmode="transparent" src="' + _item.FullName + '">';
                            _icon = this.iconsPath + _item.Extension.replace('.', '') + '.gif';
                            html = "<img src='" + _icon + "' width='48px'/>";
                            break;
                        default:
                            _icon = this.iconsPath + _item.Extension.replace('.', '') + '.gif';
                            html1 = "<img src='" + _icon + "' width='48px'/>";
                            break;
                        // bg = "ThumbnailHandler.axd?img=" + _item + "";                           
                    }
                }
                this.currentFile = _item.FullName;

                var _html = '<div class="item-info item-info-icon" style="background: url(\'' + _icon + '\') no-repeat center center;">';
                //_html += '<img class="" src="' + _icon + '" />';
                _html += '</div>';
                _html += '<div class="item-info item-info-content">';
                _html += '<div class="line-1 item-info-name">' + _item.Name + '</div>';
                if (_item.IsFolder) {
                    _html += '<div class="line-2">' + _item.SubFolderCount + ' folders, ' + _item.FileCount + ' files</div>';
                }
                else {
                    _html += '<div class="line-2"><span class="item-info-label">Size: </span>' + _item.Size + '</div>';
                }
                _html += '</div>';

                $(this.itemInfoId).html(_html);

                $(this.itemPreview).html(html1);

                $(this.txtPath).val(_item.FullName);
                // TODELETE
                $("#deleteConfirm").html(' <i class="fa fa-question fa-2x"></i> ' + this.messages.deleteItemsConfirm.replace('{0}', _item.FullName));
                $(".menumanage").css("display", "inline-block");
                $("#previewFileName").html(_item.Name);
                $("#previewImage").attr('src', _item.FullName);
            },
            getSelectedItems: function () {
                
                var _items = new Array();
                // $("input[@class='item-selected']:checked").each(function() {
                $('.' + this.itemSelectedClass).each(function () {
                    _items.push($(this).attr('rel'));
                });
                return _items;
            },
            dbSelectItem: function (_type, _item) { // dblclick
                if (_type == 'folder') {
                    this.loadFileFolder(_item);
                }
                else {
                    var win = this.getParentWin();
                    if (this.returnOpenerId != null && this.returnOpenerId != '') {
                        win.document.getElementById(this.returnOpenerId).value = _item;
                        win.document.getElementById("imgthumb").src = _item;
                        self.close();
                        parent.$.colorbox.close();
                    } else {
                        if (this.tinymce == 'preview') {
                            this.tyinyMCE_insertFile(_item, _item);  // Xem anh truoc khi chen
                        }
                        else {
                            var img = "<img src='" + _item + "'/>";
                            tinyMCE.activeEditor.selection.setContent(img);

                            tinyMCEPopup.close();
                            // ImageDialog.insert(_item, _item);               // Chen anh truc tiep, khong can xem truoc                       
                        }
                    }
                }
            },
            selectAllItems: function () {
                $('.' + this.itemClass).addClass(this.itemSelectedClass);
            },
            buildTypeIcons: function () {
                var _html = '<ul class="xfm-' + this.viewType + '">';
                var _iconPath = this.iconsPath;

                $.each($.ColorLife.items, function (i, item) {

                    if (item.IsFolder) {
                        $.ColorLife.folderInfo.subFolders += 1;
                    }
                    else {
                        $.ColorLife.folderInfo.files += 1;
                    }
                    var _icon = _iconPath + 'none.gif';
                    var _type = 'file';
                    if (item.IsFolder) {
                        _icon = _iconPath + 'folder.gif';
                        _type = 'folder';
                    }
                    else if ($.ColorLife.isImage(item.FullName)) {
                        _icon = $.ColorLife.imageThumbnail + item.FullName;
                    }
                    else if (item.IsExistsIcon) {
                        _icon = _iconPath + item.Extension.replace('.', '') + '.gif';
                    };
                    _html += '<li id="xfm-item-' + i + '" title="' + item.Name + '" class="' + $.ColorLife.itemClass + ' ' + _type + '" stt="' + i + '" type="' + _type + '" rel="' + item.FullName + '" data-toggle="context" data-target="#context-menu">';
                    _html += '<div class="xfm-item-thumb align-center" ondblclick=\"$.ColorLife.dbSelectItem(\'' + _type + '\',\'' + item.FullName + '\')\" style="background: url(\'' + _icon + '\') no-repeat center center;">';

                    _html += '</div>';
                    _html += '<div id="xfm_item_' + i + '" class="align-center ' + $.ColorLife.itemNameClass + '">' + item.Name + '</div>';
                    _html += '</li>';
                });

                _html += '</ul>';


                $(this.containerId).html(_html);

                $('.' + this.itemClass).mousedown(
                function (e) {
                    if (e.shiftKey || e.ctrlKey) {
                        $(this).toggleClass($.ColorLife.itemSelectedClass);
                        //$.ColorLife.multiSelect = true;
                    };
                    $(this).addClass($.ColorLife.itemSelectedClass);
                })
                .click(function (e) {
                    // var obj = (e.target || e.srcElement); if (obj.href || obj.type) return true;
                    if (!e.shiftKey && !e.ctrlKey) {
                        $($.ColorLife.containerId + ' li').removeClass($.ColorLife.itemSelectedClass);
                        $(this).addClass($.ColorLife.itemSelectedClass);

                    };

                    $.ColorLife.selectItemInfo($(this).attr('stt'));

                    $(this).removeClass($.ColorLife.itemSelectedNoFocusClass); //.addClass($.xFM.itemSelectedClass);

                })

                .hover(function (e) {
                    $(this).addClass($.ColorLife.itemHoverClass);
                },
                function () {
                    $(this).removeClass($.ColorLife.itemHoverClass);
                });
            },
            buildTypeList: function () {
                var _html = '<ul class="xfm-' + $.ColorLife.viewType + '">';
               
                var type='';

                var _iconPath = $.ColorLife.iconsPath;

                $.each($.ColorLife.items, function (i, item) {

                    if (item.IsFolder) {
                        $.ColorLife.folderInfo.subFolders += 1;
                        type = 'Folder';
                    }
                    else {
                        $.ColorLife.folderInfo.files += 1;
                        type='File'
                    }
                    var _icon = _iconPath + 'none.gif';
                    var _type = 'file';
                    if (item.IsFolder) {
                        _icon = _iconPath + 'folder.gif';
                        _type = 'folder';
                    }
                    else if ($.ColorLife.isImage(item.FullName)) {
                        _icon = $.ColorLife.imageThumbnail+ item.FullName;
                    }
                    else if (item.IsExistsIcon) {
                        _icon = _iconPath + item.Extension.replace('.', '') + '.gif';
                    };

                    _html += '<li id="xfm-item-' + i + '" title="' + item.Name + '" class="xfm-item-list" stt="' + i + '" type="' + _type + '" rel="' + item.FullName + '"  data-toggle="context" data-target="#context-menu">';

                    
                    _html += '<div class="xfm-list-thumb align-center" onmousedown=\"$.ColorLife.dbSelectItem(\'' + _type + '\',\'' + item.FullName + '\')\" style="background: url(\'' + _icon + '\') no-repeat center center;"></div>';

                    
                    _html += '<div id="xfm_item_' + i + '" class="' + $.ColorLife.itemNameClass + '">' + item.Name + '</div>';
                    _html += '</li>'
                    
                                        
                });
                 
                _html += '</ul>';

                $($.ColorLife.containerId).html(_html);

                $('.xfm-item-list').mousedown(
              function (e) {
                  if (e.shiftKey || e.ctrlKey) {
                      $(this).toggleClass($.ColorLife.itemSelectedClass);
                      //$.ColorLife.multiSelect = true;
                  };
                  $(this).addClass($.ColorLife.itemSelectedClass);
              })
              .click(function (e) {
                  // var obj = (e.target || e.srcElement); if (obj.href || obj.type) return true;
                  if (!e.shiftKey && !e.ctrlKey) {
                      $($.ColorLife.containerId + ' li').removeClass($.ColorLife.itemSelectedClass);
                      $(this).addClass($.ColorLife.itemSelectedClass);

                  };

                  $.ColorLife.selectItemInfo($(this).attr('stt'));

                  $(this).removeClass($.ColorLife.itemSelectedNoFocusClass); //.addClass($.xFM.itemSelectedClass);

              })

              .hover(function (e) {
                  $(this).addClass($.ColorLife.itemHoverClass);
              },
              function () {
                  $(this).removeClass($.ColorLife.itemHoverClass);
              });
            },
            buildTypeDetail: function () {
                var _html = '<table class="xfm-' + $.ColorLife.viewType + '">';
                _html += '<thead>';
                _html += '<tr><th></th><th>File Name</th><th>Type</th><th>Size</th><th>Date</th></tr>';
                _html += '</thead>';
                _html += '<tbody>';
                var type = '';

                var _iconPath = $.ColorLife.iconsPath;

                $.each($.ColorLife.items, function (i, item) {

                    if (item.IsFolder) {
                        $.ColorLife.folderInfo.subFolders += 1;
                        type = 'Folder';
                    }
                    else {
                        $.ColorLife.folderInfo.files += 1;
                        type = 'File'
                    }
                    var _icon = _iconPath + 'none.gif';
                    var _type = 'file';
                    if (item.IsFolder) {
                        _icon = _iconPath + 'folder.gif';
                        _type = 'folder';
                    }
                    else if ($.ColorLife.isImage(item.FullName)) {
                        _icon = $.ColorLife.imageThumbnail + item.FullName;
                    }
                    else if (item.IsExistsIcon) {
                        _icon = _iconPath + item.Extension.replace('.', '') + '.gif';
                    };

                    _html += '<tr id="xfm-item-' + i + '" title="' + item.Name + '" class="xfm-item-detail" stt="' + i + '" type="' + _type + '" rel="' + item.FullName + '"  data-toggle="context" data-target="#context-menu">';

                    _html += '<td>';
                    _html += '<div class="xfm-detail-thumb align-center" onmousedown=\"$.ColorLife.dbSelectItem(\'' + _type + '\',\'' + item.FullName + '\')\" style="background: url(\'' + _icon + '\') no-repeat center center;"></div>';


                    _html += '</td>';
                    _html += '<th>'
                    _html += '<div id="xfm_item_' + i + '" class="' + $.ColorLife.itemNameClass + '">' + item.Name + '</div>';
                    _html += '</td>';
                    _html += '<td>' + type + '</td>';
                    _html += '<td>' + item.Size + '</td>';
                    _html += '<td>' + $.ColorLife.formatJSONDate(item.LastAccessDate) + '</td>';

                    _html += '</tr>';
                     
                });
                _html += '</tbody>';
                _html += '</table>';

                $($.ColorLife.containerId).html(_html);

                $('.xfm-item-detail').mousedown(
              function (e) {
                  if (e.shiftKey || e.ctrlKey) {
                      $(this).toggleClass($.ColorLife.itemSelectedClass);
                      //$.ColorLife.multiSelect = true;
                  };
                  $(this).addClass($.ColorLife.itemSelectedClass);
              })
              .click(function (e) {
                  // var obj = (e.target || e.srcElement); if (obj.href || obj.type) return true;
                  if (!e.shiftKey && !e.ctrlKey) {
                      $($.ColorLife.containerId + ' li').removeClass($.ColorLife.itemSelectedClass);
                      $(this).addClass($.ColorLife.itemSelectedClass);

                  };

                  $.ColorLife.selectItemInfo($(this).attr('stt'));

                  $(this).removeClass($.ColorLife.itemSelectedNoFocusClass); //.addClass($.xFM.itemSelectedClass);

              })

              .hover(function (e) {
                  $(this).addClass($.ColorLife.itemHoverClass);
              },
              function () {
                  $(this).removeClass($.ColorLife.itemHoverClass);
              });
            },
            buildData: function (result) {
                if (result.length > 0) {
                    $.ColorLife.items = result;
                    if ($.ColorLife.viewType === 'icons') {
                        $.ColorLife.buildTypeIcons();
                    }
                    if ($.ColorLife.viewType === 'list') {
                        $.ColorLife.buildTypeList();
                    }
                    if ($.ColorLife.viewType === 'detail') {
                        $.ColorLife.buildTypeDetail();
                    }
                    $.ColorLife.clickToReName();
                }
                else {
                    $($.ColorLife.containerId).html('<div class="empty">' + $.ColorLife.messages.folderEmpty + '</div>');
                };
            },

            // LOAD DATA //
            loadFileFolder: function (path) {
                $(this.txtPath).val(path);
                this.currentFolder = path;
                $("#deleteConfirm").html(this.messages.deleteItemsConfirm.replace('{0}', path));
                if (path != this.rootFolder) {
                    $('#_back').show();
                }
                else {
                    $('#_back').hide();
                }
                var params = "{'path':'" + path + "','searchTxt':'" + this.searchTxt + "','sortOrder':'" + this.sortOrder + "','filter':" + this.filter + ",'filterExt':" + this.filterExt + "}";
                this.ajaxCallback('LoadAllFileFolder', params, this.buildData);
                //$.ajax({
                //    type: "POST",
                //    contentType: "application/json; charset=utf-8",
                //    dataType: "json",
                //    data: (params),
                //    url: $.ColorLife.serviceUrl + 'LoadAllFileFolder',
                //    beforeSend: function () {
                //        $.ColorLife.showLoading();
                //       // $($.ColorLife.loaderID).show();
                //    },
                //    success: function (response) {

                //        var result = (typeof response) == 'string' ? eval('(' + response + ')') : response;
                //        $.ColorLife.buildData(result);
                //        $($.ColorLife.loaderID).hide();
                //        $.ColorLife.hideLoading();
                //    },
                //    error: function (xhr, status) {
                //        alert(status);
                //    }
                //});
            },
            
            prevFolder: function () {
                this.loadFileFolder(this.getPrevFolder(this.currentFolder));
            },
            getPrevFolder: function (_currentFolder) {
                if (_currentFolder != this.rootFolder) {
                    return _currentFolder.substr(0, _currentFolder.lastIndexOf('/'));
                }
                else {
                    return this.rootFolder;
                }
            },         
            folderInfo: function (event) {
                // alert('Oaaa');
                var _callInfo = true;

                if (event) {
                    //alert($(event.target).parent().attr('class'));

                    if ($(event.target).hasClass(this.itemClass) || $(event.target).parent().hasClass(this.itemClass)) {

                        return;
                    }

                    $('.' + this.itemSelectedClass).addClass(this.itemSelectedNoFocusClass);

                }

                if ($.ColorLife.folderInfo.subFolders != undefined) {

                    var _icon = $.ColorLife.iconsPath + 'folder.gif';
                    var _html = '<div class="item-info item-info-icon" style="background: url(\'' + _icon + '\') no-repeat center center;">';
                    //_html += '<img class="" src="' + _icon + '" />';
                    _html += '</div>';
                    _html += '<div class="item-info item-info-content">';
                    _html += '<div class="line-1 item-info-name">' + $.ColorLife.String.format($.ColorLife.messages.folderInfo, $.ColorLife.folderInfo.subFolders, $.ColorLife.folderInfo.files) + '</div>';
                    _html += '</div>';

                    $($.ColorLife.itemInfoId).html(_html);
                }
            },
           

            refresh: function () {
               // $('#tree').jstree(true).refresh();
                $.ColorLife.loadFileFolder($.ColorLife.currentFolder);
            },
           
            zip: function () {
                alert('Comming soon!');
            },
            previewCommand: function () {
                var fileName = $(this.txtPath).val();                
                if ($.ColorLife.checkExt(fileName) === '') {
                    $.ColorLife.loadFileFolder(fileName);
                }
                else {
                    $("#myModalPreviewFile").modal('show');
                }
            },
            clipboardItems: function (_type) {
                this.clipboardType = _type;
                var _items = this.getSelectedItems();
                if (_items.length > 0) {
                    $('.' + this.itemClass).removeClass(this.itemCutClass); // clear cut item
                    this.clipboardData = _items.join('|');
                };
                if (_type == 'cut') {
                    $('.' + this.itemSelectedClass).addClass(this.itemCutClass);
                    $.ColorLife.notifyInfo('Cut!');
                }
                else $.ColorLife.notifyInfo('Copied!');
                 
            },
          
            pasteItems: function () {
                if (this.String.isNullOrEmpty(this.clipboardData)) return;
                 var _overwrite = confirm(this.messages.overwriteExists);
                 if (_overwrite) {
                     if (this.clipboardType == 'cut') {
                         var _params = '{"source":"' + this.clipboardData + '","target":"' + this.currentFolder + '"}';
                         $.ColorLife.ajaxCallback('MoveFileFolder', _params, $.ColorLife.refresh);
                     }
                     else {
                         var _params = '{"source":"' + this.clipboardData + '","target":"' + this.currentFolder + '"}';                         
                         $.ColorLife.ajaxCallback('CopyFileFolder', _params, $.ColorLife.refresh);
                     };
                     this.clipboardData = '';
                     $.ColorLife.notifySuccess('Past');
                 }
            },
           
            createNewFolder: function (name) {
                if (name.length > 0) {
                    var params = "{'path':'" + $.ColorLife.currentFolder + "','text':'" + name + "'}";
                    $.ColorLife.ajaxCallback('CreateNewFolder', params, $.ColorLife.refresh);
                    return true;
                }
                else {
                   // alert("Bạn cần nhập tên thư mục");
                    return false;
                }
            },
            moveFileFolder: function (source, target) {
                if (source.length > 0 && target.length > 0) {
                    var params = "{'source':'" + source + "','target':'" + target + "'}";
                    $.ColorLife.ajaxCallback('MoveFileFolder', params, $.ColorLife.refresh);
                    return true;
                }
                else {                    
                    return false;
                }
            },
            renameFileFolder: function (oldName, newName) {
                if (oldName.length > 0 && newName.length > 0) {
                    
                    var _params = '{"oldName":"' + oldName + '","newName":"' + newName + '"}';

                    $.ColorLife.ajaxCallback('RenameFileFolder', _params, $.ColorLife.refresh);
                    return true;
                }
                else {
                     
                    return false;
                }
              
            },
            uploadCommand: function () {
                $('#myModalUpload').modal('show');

                var manualuploader = $('#thumbnail-fine-uploader').fineUploader({
                    template: "qq-simple-thumbnails-template",
                    request: {
                        endpoint:  $.ColorLife.serviceUrl + 'AsyncUpload2?folder=' + $.ColorLife.currentFolder
                    },
                    validation: {
                        allowedExtensions: ['jpeg', 'jpg', 'gif', 'png', 'pdf', 'doc', 'docx', 'xls', 'xlsx', 'ppt', 'pptx']
                    },

                    debug: true,
                    autoUpload: false,
                    filenameParam: 'myFile',
                }).on("complete", function (event, id, name, responseJSON, xhr) {
                    if (responseJSON.success) {
                        if (responseJSON.data.IsImage) {
                            $("#pictureImageOutput").html("<img src='" + responseJSON.data.FullName + "'/>");
                        }

                        $.ColorLife.refresh();
                    }
                });
                $("#btnUpload").click(function () {
                    // alert('/FileManager/AsyncUpload2?folder=' + $.ColorLife.currentFolder);
                    manualuploader.fineUploader('uploadStoredFiles');
                    // $('#myModalUpload').modal('hide');
                });
               // $.ColorLife.doUpload();
            },

            deleteCommand: function () {
                $('#myModalDelete').modal('show');
            },

            deleteSelectedItem: function () {
                var fileName = $(this.txtPath).val();              
                if (fileName.length > 0) {
                    $.ColorLife.ajaxCallback('DeleteFileFolder', '{"path":"' + fileName + '"}', $.ColorLife.refresh);
                }
                else {
                    alert($.ColorLife.messages.mustSelectItem);
                }               
            },

            clickToReName: function () {
                $("." + $.ColorLife.itemNameClass).editable({
                    editBy: 'dblclick',
                    editClass: 'width80',
                    onSubmit: function (content) {
                        if (content.current != content.previous) {
                            var _oldName = $.ColorLife.currentFolder + '/' + content.previous;
                            var _newName = $.ColorLife.currentFolder + '/' + content.current;
                           
                            $.ColorLife.renameFileFolder(_oldName, _newName);
                           
                        }
                        
                    }
                });
            },
            dragToSelect: function () {
                               // Init Select
                if ($.fn.selectable) {
                    $($.ColorLife.containerId).selectable({
                        selected: function (e, ui) {
                            $('li.ui-selected').addClass($.xFM.itemSelectedClass);
                        },
                        unselected: function (e, ui) {
                            $('li.ui-selected').removeClass($.xFM.itemSelectedClass);
                        }
                    });
                }
                                // Init Drag
                if ($.fn.draggable && $.fn.droppable) {


                    $("." + $.ColorLife.itemClass).draggable({
                        revert: true,
                        containment: $.ColorLife.containerId,
                        opacity: 0.5,
                        // helper: 'clone',
                        zIndex: 9999,
                        scroll: true,
                        scrollSpeed: 100
                    });

                    $("." + $.ColorLife.itemClass).droppable({

                        accept: '.xfm-item',
                        hoverClass: 'xfm-item-highlight',
                        drop: function (event, ui) {


                            var _target = $('#' + event.target.id);
                            if (_target.attr('type') == 'folder') {

                                $(this).addClass('xfm-item-highlight');

                                var _dest = _target.attr('rel');

                                var _items = $.ColorLife.getSelectedItems();

                                if (_items.length > 0) {

                                    var _overwrite = confirm($.ColorLife.messages.overwriteExists);

                                    var _params = '{"_filesOrFolders":"' + _items.join('|') + '","_destination":"' + _dest + '","_overwrite":' + _overwrite + '}';

                                    //$("." + $.xFM.itemClass).draggable('destroy');
                                    //$(ui.draggable).remove();

                                    $('.' + $.ColorLife.itemSelectedClass).remove();

                                    _target.removeClass('xfm-item-highlight');


                                    $.ColorLife.ajaxCallback('Move', _params);
                                }

                            }

                        }


                    });

                }

                                /*

                var selected = $([]), offset = { top: 0, left: 0 };
                //$("#xfmContainer").selectable();

                $(".xfm-item").draggable({

                start: function (ev, ui) {

                selected = $(".ui-selected").each(function () {
                var el = $(this);
                el.data("offset", el.position({ of: $.xFM.containerId })); //el.offset());

                });

                offset = $(this).position({ of: $.xFM.containerId }); //$(this).offset();

                },

                drag: function (ev, ui) {

                var dt = ui.position.top - offset.top, dl = ui.position.left - offset.left;

                selected.not(this).each(function () {
                var el = $(this), off = el.data("offset");
                el.css({ top: off.top + dt, left: off.left + dl });
                });

                }

                });

                */

            },
            viewTypeCommand: function (viewType) {
                $('[class*=btn-viewtype]').removeClass('active');
                $('.btn-viewtype-' + viewType).addClass('active');
                $.ColorLife.setCookie('CLViewType', viewType, 365);
                $.ColorLife.viewType = viewType;
                $.ColorLife.loadFileFolder($.ColorLife.currentFolder);
            },
            viewSortCommand: function (sortType) {
                $('[class*=sortby-]').removeClass('sort-active');
                $('.sortby-' + sortType).addClass('sort-active');
                 
                $.ColorLife.setCookie('CLSortBy', sortType, 365);
                $.ColorLife.sortOrder = sortType + ' desc';
                $.ColorLife.loadFileFolder($.ColorLife.currentFolder);
            },
            viewFilterCommand:function(filterType){
                $('[class*=filterby-]').removeClass('filter-active');
                $('.filterby-' + filterType).addClass('filter-active');
                $.ColorLife.filterExt = filterType;
                $.ColorLife.loadFileFolder($.ColorLife.currentFolder);
            },
            searchCommand: function () {
                var keyword = $("#txtSearch").val();
               this.searchTxt = keyword;

                $.ColorLife.loadFileFolder($.ColorLife.currentFolder);
            },
            loadTreeFunction: function () {
                $('#tree').jstree({
                 'core': {
                     'data': {
                         'url': $.ColorLife.serviceUrl + 'GetTreeData', 
                         'type': "POST",
                         'dataType': "json",
                         'contentType': "application/json; charset=utf8",
                         'data': JSON.stringify({ 'id': $.ColorLife.rootFolder }),
                         //'data':  function (node) {
                         //    return { 'id': node.id };
                         //}
                     },
                     'check_callback': function (o, n, p, i, m) {
                         if (m && m.dnd && m.pos !== 'i') { return false; }
                         if (o === "move_node" || o === "copy_node") {
                             if (this.get_node(n).parent === this.get_node(p).id) { return false; }
                         }
                         return true;
                     },
                     'themes': {
                         'responsive': false,
                         'variant': 'small',
                         'stripes': true
                     }
                 },
                 'sort': function (a, b) {
                     return this.get_type(a) === this.get_type(b) ? (this.get_text(a) > this.get_text(b) ? 1 : -1) : (this.get_type(a) >= this.get_type(b) ? 1 : -1);
                 },
                 'contextmenu': {
                     'items': function (node) {
                         var tmp = $.jstree.defaults.contextmenu.items();
                         delete tmp.create.action;
                         tmp.create.label = "New";
                         tmp.create.submenu = {
                             "create_folder": {
                                 "separator_after": true,
                                 "label": "Folder",
                                 "action": function (data) {
                                     var inst = $.jstree.reference(data.reference),
                                         obj = inst.get_node(data.reference);
                                     inst.create_node(obj, { type: "default" }, "last", function (new_node) {
                                         setTimeout(function () { inst.edit(new_node); }, 0);
                                     });
                                 }
                             },
                             "create_file": {
                                 "label": "File",
                                 "action": function (data) {
                                     var inst = $.jstree.reference(data.reference),
                                         obj = inst.get_node(data.reference);
                                     inst.create_node(obj, { type: "file" }, "last", function (new_node) {
                                         setTimeout(function () { inst.edit(new_node); }, 0);
                                     });
                                 }
                             }
                         };
                         if (this.get_type(node) === "file") {
                             delete tmp.create;
                         }
                         return tmp;
                     }
                 },
                 //'types': {
                 //    'default': { 'icon': 'folder' },
                 //    'file': { 'valid_children': [], 'icon': 'file' }
                 //},
                 'unique': {
                     'duplicate': function (name, counter) {
                         return name + ' ' + counter;
                     }
                 },
                 'plugins': ['state', 'dnd', 'sort', 'types', 'contextmenu', 'unique', "wholerow"]
             }).on('dblclick.jstree', function (e) {
                 var href = e.node.a_attr.href;
              
                 if (href) {
                     window.location = href;
                 } 
             })
                .on('select_node.jstree', function (e, data) {
                 
                   
                })
             .on('delete_node.jstree', function (e, data) {

                 var params = "{'path':'" + data.node.id + "'}";
                 $.ColorLife.ajaxCallback('DeleteFileFolder', params, $.ColorLife.refresh);
                
             })
             .on('create_node.jstree', function (e, data) {
                 $.ColorLife.createNewFolder(data.node.text);
                
             })
             .on('rename_node.jstree', function (e, data) {
                 var parent = data.node.id;
                 var x = parent.substr(0, parent.lastIndexOf('/'));
                    
                 var _oldName = data.node.id;
                 var _newName = x+"/"+ data.node.text;
                 var _params = '{"oldName":"' + _oldName + '","newName":"' + _newName + '"}';
 
                 $.ColorLife.renameFileFolder(_oldName, _newName);

                // data.instance.refresh();
                
             })
             .on('move_node.jstree', function (e, data) {
                 $.ColorLife.moveFileFolder(data.node.id, data.parent);
               
             })
             .on('copy_node.jstree', function (e, data) {
                 $.get('?operation=copy_node', { 'id': data.original.id, 'parent': data.parent })
                     .done(function (d) {
                         //data.instance.load_node(data.parent);
                         data.instance.refresh();
                     })
                     .fail(function () {
                         data.instance.refresh();
                     });
             })
             .on('changed.jstree', function (e, data) {
                 if (data && data.selected && data.selected.length) {
                     //  alert(data.selected.join(':'));
                     var path = data.selected.join(':');
                     $.ColorLife.loadFileFolder(path);
                 }
                 else {
                  //   $.ColorLife.loadFileFolder($.ColorLife.rootFolder);
                     //$($.ColorLife.containerId).html('<div class="empty">' + $.ColorLife.messages.pleaseSelectedFolder + '</div>');
                 }
             });

            },
           
            help: function () {
                $("#myModalHelp").modal("show");
            },
           
            // Page Load
            onPageLoad: function () {
                var viewType = $.ColorLife.getCookie("CLViewType");
                if (viewType == undefined || viewType == null) {
                    viewType = 'icons';
                }
                var sortType = $.ColorLife.getCookie("CLSortBy");
                if (sortType == undefined || sortType == null) {
                    sortType = 'name';
                }

                $.ColorLife.sortOrder = sortType+' asc';
                $.ColorLife.viewType = viewType;
                $('.sortby-' + sortType).addClass('sort-active');
                $('.btn-viewtype-' + viewType).addClass('active');
                $.ColorLife.loadTreeFunction();

                $.ColorLife.returnOpenerId = $.ColorLife.getQueryString('returnId');
                $.ColorLife.tinymce = $.ColorLife.getQueryString('tinymce');

             //   if ($.ColorLife.getQueryString('filterby') === null) $.ColorLife.filterExt = 0;
              //  else $.ColorLife.filterExt = $.ColorLife.getQueryString('filterby');
                ///////////
                // Keyboard Shortcut
                $(document).bind('keydown', 'h', function (evt) { $.ColorLife.help(); return false; });
                $(document).bind('keydown', 'f5', function (evt) { $.ColorLife.refresh(); return false; });
                $(document).bind('keydown', 'backspace', function (evt) { $.ColorLife.prevFolder(); return false; });
                $(document).bind('keydown', 'del', function (evt) { $.ColorLife.deleteCommand(); return false; });
                $(document).bind('keydown', 'shift+del', function (evt) { $.ColorLife.deleteSelectedItem(); return false; });
                         $(document).bind('keydown', 'ctrl+a', function (evt) { $.ColorLife.selectAllItems(); return false; });
                $(document).bind('keydown', 'ctrl+x', function (evt) { $.ColorLife.clipboardItems('cut'); return false; });
                $(document).bind('keydown', 'ctrl+c', function (evt) { $.ColorLife.clipboardItems('copy'); return false; });
                $(document).bind('keydown', 'ctrl+v', function (evt) { $.ColorLife.pasteItems(); return false; });
                $(document).bind('keydown', 'ctrl+u', function (evt) { $.ColorLife.uploadCommand(); return false; });


                // $(document).bind('click', function (evt) { $.ColorLife.folderInfo(evt); return false; });
                // $(document).bind('mousedown', function (evt) { $.ColorLife.folderInfo(evt); return false; });

                //$($.xFM.containerId).bind('mousedown', function (evt) { $.xFM.folderInfo(evt); return false; });

                $(document).bind('keydown', 'esc', function (evt) { $.ColorLife.hideLoading(); });
                /////////
            }                       
        }
        // end ColorLife
    });   // end extend
})(jQuery);

