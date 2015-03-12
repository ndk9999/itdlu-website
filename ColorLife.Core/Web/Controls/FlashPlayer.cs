using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;

namespace ColorLife.Core.Web
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:FlashPlayer runat=server></{0}:FlashPlayer>")]
    public class FlashPlayer : WebControl
    {
        public enum PlayerMode
        {
            Single,
            PlayList
        }
        [Bindable(true), Category("Settings"), DefaultValue("swfobject.js")]
        public string Swfobject_jsLocation
        {

            get
            {
                string str = (string)this.ViewState["Swfobject_jsLocation"];
                if (str == null)
                {
                    return "swfobject.js";
                }
                return str;
            }
            set
            {
                this.ViewState["Swfobject_jsLocation"] = value;
            }
        }
        [Bindable(true), Category("Settings"), DefaultValue("jwplayer.js")]
        public string Player_jsLocation
        {

            get
            {
                string str = (string)this.ViewState["Player_jsLocation"];
                if (str == null)
                {
                    return "jwplayer.js";
                }
                return str;
            }
            set
            {
                this.ViewState["Player_jsLocation"] = value;
            }
        }

        [Bindable(true), Category("Settings"), DefaultValue("player.swf")]
        public string FlashPlayerLocation
        {

            get
            {
                string str = (string)this.ViewState["FlashPlayerLocation"];
                if (str == null)
                {
                    return "player.swf";
                }
                return str;
            }
            set
            {
                this.ViewState["FlashPlayerLocation"] = value;
            }
        }
        [Bindable(true), Category("Settings"), DefaultValue("preview.jpg")]
        public string Image
        {
            get
            {
                string str = (string)this.ViewState["ImagePreview"];
                if (str == null)
                {
                    return "preview.jpg";
                }
                return str;
            }
            set
            {
                ViewState["ImagePreview"] = value;
            }
        }
        [Bindable(true), Category("Settings"), DefaultValue(true)]
        public bool AllowFullScreen
        {
            get
            {
                bool str = (bool)this.ViewState["ImagePreview"];
                if (str == null)
                {
                    return true;
                }
                return str;
            }
            set
            {

                ViewState["AllowFullScreen"] = value;
            }
        }

        [Bindable(true), Category("Settings")]
        public string File
        {
            get
            {
                string str = (string)this.ViewState["File"];
                if (str == null)
                {
                    return "preview.jpg";
                }
                return str;

            }
            set
            {

                ViewState["File"] = value;
            }
        }

        [Bindable(true), Category("Settings")]
        public string FileList
        {
            get
            {
                string str = (string)this.ViewState["FileList"];
                if (str == null)
                {
                    return "";
                }
                return str;

            }
            set
            {

                ViewState["FileList"] = value;
            }
        }

        [Bindable(true), Category("Settings")]
        public string Skin
        {
            get
            {
                string str = (string)this.ViewState["Skin"];
                if (str == null)
                {
                    return "preview.jpg";
                }
                return str;
            }
            set
            {

                ViewState["Skin"] = value;
            }
        }
        [Bindable(true), Category("Settings")]
        public bool AutoStart
        {
            get
            {
                bool str = (bool)this.ViewState["AutoStart"];
                if (str == null)
                {
                    return false;
                }
                return str;
            }
            set
            {

                ViewState["AutoStart"] = value;

            }
        }

        [Bindable(true), Category("Settings")]
        public int BufferLength
        {
            get
            {
                int str = (int)this.ViewState["BufferLength"];
                if (str == null)
                {
                    return 960;
                }
                return str;
            }
            set
            {

                ViewState["BufferLength"] = value;

            }
        }


        [Bindable(true), Category("Settings")]
        public bool FullScreen
        {
            get
            {
                bool str = (bool)this.ViewState["FullScreen"];
                if (str == null)
                {
                    return true;
                }
                return str;
            }
            set
            {

                ViewState["FullScreen"] = value;

            }
        }

        [Bindable(true), Category("Settings")]
        public bool Mute
        {
            get
            {
                bool str = (bool)this.ViewState["Mute"];
                if (str == null)
                {
                    return true;
                }
                return str;
            }
            set
            {

                ViewState["Mute"] = value;

            }
        }

        [Bindable(true), Category("Settings")]
        public bool Quality
        {
            get
            {
                bool str = (bool)this.ViewState["Quality"];
                if (str == false)
                {
                    return true;
                }
                return str;
            }
            set
            {

                ViewState["Quality"] = value;

            }
        }

        [Bindable(true), Category("Settings")]
        public int Volume
        {
            get
            {
                int str = (int)this.ViewState["Volume"];
                if (str == 0)
                {
                    return 100;
                }
                return str;
            }
            set
            {

                ViewState["Volume"] = value;
            }
        }
        [Bindable(true), Category("Settings"), DefaultValue(360)]
        public int DefaultWidth
        {
            get
            {
                return (int)ViewState["DefaultWidth"];
            }
            set
            {

                ViewState["DefaultWidth"] = value;

            }
        }
        [Bindable(true), Category("Settings"), DefaultValue(320)]
        public int DefaultHeight
        {
            get
            {
                return (int)ViewState["DefaultHeight"];
            }
            set
            {

                ViewState["DefaultHeight"] = value;

            }
        }
        private PlayerMode _PlayerMode = PlayerMode.Single;
        [Bindable(true), Category("Settings"), DefaultValue(320)]
        public PlayerMode PlayerModeVideo
        {
            get
            {
                return _PlayerMode;
            }
            set
            {
                if (Enum.IsDefined(typeof(FlashPlayer.PlayerMode), value))
                {
                    _PlayerMode = value;
                }
            }
        }
        public string PlayListVideo()
        {
            string html = "";
              html += " <script src=\"" + Player_jsLocation + "\" type=\"text/javascript\"></script>";

            html += "<div id=\"flash_" + this.ClientID + "\"></div>";
            html += "<script type=\"text/javascript\">";
            html += " jwplayer(\"flash_" + this.ClientID + "\").setup({";
            html += "flashplayer: \"" + FlashPlayerLocation + "\",";
            html+="stretching: \"fill\",";

            html += "skin: \"" + Skin + "\",";
            html += "file: \"" + File + "\",";
            html += "playlistfile: \"" + FileList + "\",";
            html += "'playlist': 'bottom',";
            html += "'playlist.position': 'bottom',";
            html += "image: \"" + Image + "\",";
            html += "stretching: \"fill\",";

            html += "width: " + DefaultWidth + ",";
            html += "height: " + DefaultHeight + "";

            html += "});";
            html += "</script>";
            return html;
            
        }
        public string PlaySingleVideo()
        {
            string html = "";
           
            html += " <script src=\"" + Player_jsLocation + "\" type=\"text/javascript\"></script>";

            html += "<div id=\"flash_" + this.ClientID + "\"></div>";
            html += "<script type=\"text/javascript\">";
            html += " jwplayer(\"flash_" + this.ClientID + "\").setup({";
            html += "flashplayer: \"" + FlashPlayerLocation + "\",";
            html += "skin: \"" + Skin + "\",";
            html += "stretching: \"fill\",";
            html += "file: \"" + File + "\",";
            html += "image: \"" + Image + "\",";
            html += "stretching: \"fill\",";

            html += "width: " + DefaultWidth + ",";
            html += "height: " + DefaultHeight + "";

            html += "});";
            html += "</script>";
            return html;

        }
        protected override void Render(HtmlTextWriter output)
        {
            if (PlayerModeVideo == PlayerMode.Single)
                output.Write(PlaySingleVideo());
            if (PlayerModeVideo == PlayerMode.PlayList)
                output.Write(PlayListVideo());
            else output.Write(PlaySingleVideo());
           // base.Render(output);
        }
    }
}
