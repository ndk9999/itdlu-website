

tinymce.PluginManager.add('colorlifefilemanager', function(editor) {
    
    function openmanager() {
	editor.focus(true);
        var title="COLOR LIFE FileManager";
        if (typeof editor.settings.filemanager_title !== "undefined" && editor.settings.filemanager_title) {
            title=editor.settings.filemanager_title;
        }
	var sort_by="";
	var descending="false";
	if (typeof editor.settings.filemanager_sort_by !== "undefined" && editor.settings.filemanager_sort_by) 
	    sort_by=editor.settings.filemanager_sort_by;
	if (typeof editor.settings.filemanager_descending !== "undefined" && editor.settings.filemanager_descending) 
	    descending=editor.settings.filemanager_descending;
        win = editor.windowManager.open({
            title: title,
            file: editor.settings.external_filemanager_path+'FileManager?type=4&descending='+descending+'&sort_by='+sort_by+'&lang='+editor.settings.language,
            width: 880,
            height: 570,
            inline: 1,
	    resizable: true,
	    maximizable: true
        });
    }
    
    editor.addButton('colorlifefilemanager', {
		icon: 'image',
		tooltip: 'Insert Image',
		shortcut: 'Ctrl+E',
                onclick:openmanager
	});
        
	editor.addShortcut('Ctrl+E', '', openmanager);

	editor.addMenuItem('colorlifefilemanager', {
	    icon: 'image',
		text: 'Insert Image',
		shortcut: 'Ctrl+E',
		onclick: openmanager,
		context: 'insert'
	});
	
});