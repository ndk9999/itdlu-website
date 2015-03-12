using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Web.UI;

namespace ColorLife.Core.Web
{
    [DefaultValue("ColorLifeEditor")]
    [ToolboxData("<{0}:ColorLifeEditor runat=server></{0}:ColorLifeEditor>")]
    public class ColorLifeEditor : TextBox
    {
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            base.Render(writer);
        }
        protected override void OnPreRender(EventArgs e)
        {
            string tinyMceIncludeKey = "TinyMCEInclude";
            string tinyMceIncludeScript = "<script type=\"text/javascript\" src=\"/editors/tiny_mce/tiny_mce.js\"></script>";

            if (!Page.ClientScript.IsStartupScriptRegistered(tinyMceIncludeKey))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), tinyMceIncludeKey, tinyMceIncludeScript);
            }

            if (!Page.ClientScript.IsStartupScriptRegistered(GetInitKey()))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), GetInitKey(), GetInitScript());
            }

            if (!CssClass.Contains(GetEditorClass())) //probably this is not the best way how to add the css class but I do not know any beter way
            {
                if (CssClass.Length > 0)
                {
                    CssClass += " ";
                }
                CssClass += GetEditorClass();
            }
            base.OnPreRender(e);
        }

        private string GetInitKey()
        {
            string simpleKey = "TinyMCESimple";
            string fullKey = "TinyMCEFull";

            switch (Mode)
            {
                case TextEditorMode.Simple:
                    return simpleKey;
                case TextEditorMode.Full:
                    return fullKey;
                case TextEditorMode.Basic:
                    return fullKey;
                default:
                    goto case TextEditorMode.Full;
            }
        }

        private string GetEditorClass()
        {
            return GetEditorClass(Mode);
        }

        private string GetEditorClass(TextEditorMode mode)
        {
            string simpleClass = "SimpleTextEditor";
            string fullClass = "FullTextEditor";

            switch (mode)
            {
                case TextEditorMode.Simple:
                    return simpleClass;
                case TextEditorMode.Full:

                    return fullClass;
                case TextEditorMode.Basic:
                    return fullClass;
                default:
                    goto case TextEditorMode.Full;
            }
        }

        private string GetInitScript()
        {
            string simpleScript =
                "<script language=\"javascript\" type=\"text/javascript\">tinyMCE.init({{mode : \"textareas\",theme : \"simple\",editor_selector : \"{0}\""+
                 

                "}});</script>";
            string basicScript =
               "<script language=\"javascript\" type=\"text/javascript\">tinyMCE.init({{mode : \"textareas\",theme : \"advanced\",editor_selector : \"{0}\"" +
               ",font_size_style_values : \"xx-large\"" +
               ",theme_advanced_toolbar_location: \"top\"" +
               ",theme_advanced_toolbar_align: \"left\"" +
               ",theme_advanced_statusbar_location: \"bottom\"" +
               ",entity_encoding: \"raw\"" +

               ",convert_urls: false" +
               ",plugins: \"" + Plugins + "\"" +
               ",skin: \"o2k7\"" +
               ",skin_variant: \"silver\"" +
               ",theme_advanced_buttons1: \"fullscreen,code,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,bullist,numlist,|,undo,redo,|,styleselect,formatselect,fontselect,fontsizeselect,|,forecolor,backcolor\"" +
               ",theme_advanced_buttons2: \"link,unlink,anchor,emotions,image,media,code,insertfile,insertimage,insertcode,imgmanager,|,charmap,iespell,advhr,|,insertdate,inserttime,preview,|,tablecontrols,|,hr,removeformat,visualaid,|,cut,copy,paste,pastetext,pasteword,|,search,replace,|,link,unlink,anchor,emotions,image,media,cleanup,help\"" +
               ",theme_advanced_buttons3: \"\"" +
                ",theme_advanced_buttons4: \"\"" +
               ",theme_advanced_resizing: true" +
               ",theme_advanced_resize_horizontal: true" +
               ",tab_focus: \":prev,:next\"" +
               ",gecko_spellcheck: true" +
               ",theme_advanced_path: false" +

               //",setup: function (ed) {   ed.onKeyUp.add(function (ed, e) {var strip = (tinyMCE.activeEditor.getContent()).replace(/(<([^>]+)>)/ig, \"\");     var text = strip.split(\" \").length + \" Words, \" + strip.length + \" Characters\"                        tinymce.DOM.setHTML(tinymce.DOM.get(tinyMCE.activeEditor.id + \"_path_row\"), text);                    });            }"+
               ",file_browser_callback : \"filebrowser\""+
               "}});</script>";

           
            string fullScript =
                "<script language=\"javascript\" type=\"text/javascript\">tinyMCE.init({{mode : \"textareas\",theme : \"advanced\",editor_selector : \"{0}\""+
                 ",font_size_style_values : \"xx-large\"" +
                ",theme_advanced_toolbar_location: \"top\""+
                ",theme_advanced_toolbar_align: \"left\""+
                ",theme_advanced_statusbar_location: \"bottom\""+
                ",entity_encoding: \"raw\""+

                ",convert_urls: false"+
                ",plugins: \"" + Plugins + "\"" +
                ",skin: \"o2k7\"" +
                ",skin_variant: \"silver\"" +
                ",theme_advanced_buttons1: \"" + Theme_advanced_buttons1 + "\"" +
                ",theme_advanced_buttons2: \""+Theme_advanced_buttons2+"\""+
                ",theme_advanced_buttons3: \"" + Theme_advanced_buttons3 + "\"" +
                ",theme_advanced_buttons4: \"" + Theme_advanced_buttons4 + "\"" +
                ",theme_advanced_resizing: true"+
                ",theme_advanced_resize_horizontal: true" +
                ",tab_focus: \":prev,:next\""+
                ",gecko_spellcheck: true"+
                ",theme_advanced_path: false" +
                
                //",setup: function (ed) {   ed.onKeyUp.add(function (ed, e) {var strip = (tinyMCE.activeEditor.getContent()).replace(/(<([^>]+)>)/ig, \"\");     var text = strip.split(\" \").length + \" Words, \" + strip.length + \" Characters\"                        tinymce.DOM.setHTML(tinymce.DOM.get(tinyMCE.activeEditor.id + \"_path_row\"), text);                    });            }"+
                ",file_browser_callback : \"filebrowser\"" +
                "}});</script>";

            switch (Mode)
            {
                case TextEditorMode.Simple:
                    return string.Format(simpleScript, GetEditorClass(TextEditorMode.Simple));
                case TextEditorMode.Full:
                    return string.Format(fullScript, GetEditorClass(TextEditorMode.Full));
                case TextEditorMode.Basic:
                    return string.Format(basicScript, GetEditorClass(TextEditorMode.Full));
                default:
                    goto case TextEditorMode.Full;
            }
        }

        public override TextBoxMode TextMode
        {
            get
            {
                return TextBoxMode.MultiLine;
            }
        }
        [Bindable(true), Category("Settings"), DefaultValue("")]
        public string Plugins
        {
            get
            {
                string str = (string)this.ViewState["Plugins"];
                if (str == null)
                {
                    str = "autolink,lists,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,wordcount,advlist,autosave,insertcode,imgmanager";
                }
                return str;
            }
            set
            {
                ViewState["Plugins"] = value;
            }
        }
        [Bindable(true), Category("Settings"), DefaultValue(""), Description("Theme Advanced Button 1")]
        public string Theme_advanced_buttons1
        {
            get
            {
                string str = (string)this.ViewState["Theme_advanced_buttons1"];
                if (str == null)
                {
                    return "fullscreen,code,|,save,newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,styleselect,formatselect,fontselect,fontsizeselect";
                }
                return str;
            }
            set
            {
                ViewState["Theme_advanced_buttons1"] = value;
            }
        }
        [Bindable(true), Category("Settings"), DefaultValue(""), Description("Theme Advanced Button 2")]
        public string Theme_advanced_buttons2
        {
            get
            {
                string str = (string)this.ViewState["Theme_advanced_buttons2"];
                if (str == null)
                {
                    return "cut,copy,paste,pastetext,pasteword,|,search,replace,|,link,unlink,anchor,emotions,image,media,cleanup,help,code,|,charmap,iespell,advhr,|,insertdate,inserttime,preview,|,forecolor,backcolor,|,tablecontrols,|,hr,removeformat,visualaid";
                }
                return str;
            }
            set
            {
                ViewState["Theme_advanced_buttons2"] = value;
            }
        }
        [Bindable(true), Category("Settings"), DefaultValue(""), Description("Theme Advanced Button 3")]
        public string Theme_advanced_buttons3
        {
            get
            {
                string str = (string)this.ViewState["Theme_advanced_buttons3"];
                if (str == null)
                {
                    return "sub,sup,|,print,|,ltr,rtl,|,insertlayer,moveforward,movebackward,absolute,|,styleprops,spellchecker,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,template,blockquote,pagebreak,|,insertfile,insertimage,insertcode,imgmanager";
                }
                return str;
            }
            set
            {
                ViewState["Theme_advanced_buttons3"] = value;
            }
        }
        [Bindable(true), Category("Settings"), DefaultValue(""), Description("Theme Advanced Button 4")]
        public string Theme_advanced_buttons4
        {
            get
            {
                string str = (string)this.ViewState["Theme_advanced_buttons4"];
                if (str == null)
                {
                    return "";
                }
                return str;
            }
            set
            {
                ViewState["Theme_advanced_buttons4"] = value;
            }
        }
        [Bindable(true), Category("Settings"), DefaultValue(""), Description("Mode Simple/Basic/Full")]
        public TextEditorMode Mode
        {
            get
            {
                Object obj = ViewState["Mode"];
                if (obj == null)
                {
                    return TextEditorMode.Simple;
                }
                return (TextEditorMode)obj;
            }
            set
            {
                ViewState["Mode"] = value;
            }
        }
        [Bindable(true), Category("Settings"), DefaultValue(""), Description("Mode")]
        public enum TextEditorMode
        {
            Basic,
            Simple,            
            Full
        }
    }
}
