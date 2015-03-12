tinymce.init({
    selector: "textarea#FullText",
    theme: "modern",
    entity_encoding: "raw",
    height: 300,
    fontsize_formats: "8pt 10pt 12pt 14pt 18pt 24pt 36pt",
    relative_urls: false,
    browser_spellcheck: true,
    remove_script_host: false,
    convert_urls: false,
    plugins: [
         "advlist autolink link image lists charmap print preview hr anchor pagebreak spellchecker",
         "searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking",
         "save table contextmenu directionality emoticons template paste textcolor colorlifefilemanager"
    ],
    content_css: "",
    toolbar: "insertfile undo redo | styleselect fontselect  fontsizeselect | bold italic underline | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link colorlifefilemanager | print preview media fullpage | forecolor backcolor emoticons",
    style_formats: [
         { title: 'Bold text', inline: 'b' },
         { title: 'Red text', inline: 'span', styles: { color: '#ff0000' } },
         { title: 'Red header', block: 'h1', styles: { color: '#ff0000' } },
         { title: 'Example 1', inline: 'span', classes: 'example1' },
         { title: 'Example 2', inline: 'span', classes: 'example2' },
         { title: 'Table styles' },
         { title: 'Table row 1', selector: 'tr', classes: 'tablerow1' }
    ]
    , file_browser_callback: "filebrowser"
  , external_filemanager_path: "/"
  , filemanager_title: "ColorLife File Manager [Lê Thanh Tuấn - 097 6060 432]",

  
  
   // external_plugins: { "filemanager": "/filemanager/plugin.min.js" }
});
function myFileBrowser(field_name, url, type, win) {

    var cmsURL = window.location.pathname;      // script URL
    var searchString = window.location.search;  // possible parameters
    if (searchString.length < 1) {
        // add "?" to the URL to include parameters (in other words: create a search string because there wasn't one before)
        searchString = "?";
    }

    // block multiple file browser windows
    if (!tinyMCE.selectedInstance.fileBrowserAlreadyOpen) {
        // no file browser window open
        tinyMCE.selectedInstance.fileBrowserAlreadyOpen = true; // but now it will be

        tinyMCE.openWindow({
            file: cmsURL + searchString + "&type=" + type,
            title: "File Browser",
            width: 420,
            height: 400,
            close_previous: "no"
        }, {
            window: win,
            input: field_name,
            resizable: "yes",
            inline: "yes",
            editor_id: tinyMCE.selectedInstance.editorId
        });
    }

    return false;
}
function filebrowser(field_name, url, type, win) {

    fileBrowserURL = "/Admin/FileManager?sessionid=preview&type=" + type;
    tinyMCE.activeEditor.windowManager.open({
        title: "File Manage [Lê Thanh Tuấn - 097 6060 432]",
        url: fileBrowserURL,
        file: fileBrowserURL,
        width: 780,
        height: 600,
        inline: 1,
        resizable: 0,
        maximizable: 1,
        close_previous: 0,
        popup_css: true
    }, {
        window: win,
        input: field_name,
        sessionid: 'preview'
    });
}