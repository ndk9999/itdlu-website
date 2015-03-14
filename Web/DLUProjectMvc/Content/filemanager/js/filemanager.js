jQuery(document).ready(function () {
   // $("[rel='tooltip']").tooltip();
    $('body').layout({
        // enable showOverflow on west-pane so popups will overlap north pane
        west__showOverflowOnHover: true,
        west__size: 220,
        north__size: 40,
        south__size: 60,

        east__showOverflowOnHover: true,
        east: {
            initClosed: true,
        }
    });

    $.ColorLife.sessionId = $.ColorLife.getQueryString('sessionid');
    $.ColorLife.onPageLoad();
   
    $("#myModalNewFolder").draggable({
        handle: ".modal-header"
    });
    $("#myModalUpload").draggable({
        handle: ".modal-header"
    });
    $("#myModalHelp").draggable({
        handle: ".modal-header"
    });
     
    $("#btnCreateFolder").click(function () {
        var ok = $.ColorLife.createNewFolder($("#txtFolderName").val());
        $('#myModalNewFolder').modal('hide');
    });

    $("#btnDelete").click(function () {
        $.ColorLife.deleteSelectedItem();
        $('#myModalDelete').modal('hide');
    });
     
    // $("#_fileContainer").contextMenu(menu1, { theme: 'vista' });

   
    
});

 