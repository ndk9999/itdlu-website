$(document).ready(function () {

    if (pluginlist.indexOf("Flash") != -1) {
        $("#ajaxUploader").hide();
        $("#file_upload").uploadify({
            'swf': '/Scripts/Portal/uploadify.swf',
            'uploader': '/FileUpload.axd',

            'buttonText': 'SELECT FILE (S)',
            'fileTypeExts': '*.jpg;*.gif;*.png;*.bmp',
            'fileTypeDesc': 'Image Files',
            'multi': true,
            'auto': true,
            'formData': {
                'folder': '/Uploads/AnhDaiDien',
                'tocken': 'tuanitpro'
            },
            'onInit': function (instance) {
                //  alert('The queue ID is ' + instance.settings.uploader + $.ColorLife.currentFolder);
            },
            'onQueueComplete': function (queueData) {
                $.ColorLife.notifyInfo('Upload file thành công');
                $('.result').html(queueData.uploadsSuccessful + ' files tải lên thành công. ' + queueData.uploadsErrored + ' files lỗi.');
                $.ColorLife.refresh();
            }
        });
    }
    else {
        alert('Trình duyệt của bạn chưa cài Flash. Bạn cần cài Adobe Flash vào để upload tập tin.');
        $("#uploadify").hide();
        
        new AjaxUpload("dragandrophandler", {
            action: '/FileUpload.axd',
            name: 'myFile',
            data: { 'folder': '/Uploads/AnhDaiDien' },
            autoSubmit: true,
            onSubmit: function (file, extension) {
                if (!(extension && /^(jpg|png|jpeg|gif|bmp|JPG|PNG|JPEG|BMP|GIF)$/.test(extension))) {

                    alert('Only JPG, PNG or GIF files are allowed');

                    return false;
                }

            },
            onComplete: function (file, response) {
                var resp = response;
                resp = jQuery(resp).html();
                $(".statusbar").html('Upload file ' + resp + ' thành công');
                $.ColorLife.notifyInfo('Upload file thành công');
                $('.result').html('<img src=' + resp + ' width="150" height="200"/>');

                return true;
            }
        });
    }
});