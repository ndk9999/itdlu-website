using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Text.RegularExpressions;
namespace ColorLife.Paging.Web
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:PagingControl runat=server></{0}:PagingControl>")]
    public class PagingControl : WebControl
    {

        #region Enumration
        public enum PagingModeEnum
        {
            Default,
            Numeric,
            DefaultAjax,
            NumericAjax,
            UrlFriendly

        }

        #endregion
        #region Private Properties
        private string _FirstPageText = "First";
        [Bindable(true)]
        [Category("PagerSettings")]
        public string FirstPageText
        {
            get { return _FirstPageText; }
            set { _FirstPageText = value; }
        }
        private string _PreviousPageText = "« Prev";
        [Bindable(true)]
        [Category("PagerSettings")]
        public string PreviousPageText
        {
            get { return _PreviousPageText; }
            set { _PreviousPageText = value; }
        }
        private string _NextPageText = "Next »";
        [Bindable(true)]
        [Category("PagerSettings")]
        public string NextPageText
        {
            get { return _NextPageText; }
            set { _NextPageText = value; }
        }
        private string _LastPageText = "Last";
        [Bindable(true)]
        [Category("PagerSettings")]
        public string LastPageText
        {
            get { return _LastPageText; }
            set { _LastPageText = value; }
        }
        private string _QueryStringKey = "Page";
        [Bindable(true)]
        [Category("PagerSettings")]
        public string QueryStringKey
        {
            get
            {
                return _QueryStringKey;
            }

            set
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(value, "[a-zA-z]"))
                {
                    _QueryStringKey = value;
                }
            }
        }

        private PagingModeEnum _PagingMode = PagingModeEnum.Default;
        [Bindable(true)]
        [Category("PagerSettings")]
        public PagingModeEnum PagingMode
        {
            get
            {
                return _PagingMode;
            }
            set
            {
                if (Enum.IsDefined(typeof(PagingControl.PagingModeEnum), value))
                {
                    _PagingMode = value;
                }
            }
        }
        [Bindable(true)]
        [DefaultValue(0)]
        [Description("Total Page")]
        [Category("PagerControls")]
        public int PageSize
        {
            get
            {
                object objPage = ViewState["_PageSize"];
                int _PageSize = 10;
                if (objPage == null)
                {
                    _PageSize = 10;
                }
                else
                {
                    _PageSize = (int)objPage;
                }
                return _PageSize;
            }
            set { ViewState["_PageSize"] = value; }
        }
        [Bindable(true)]
        [DefaultValue(0)]
        [Description("Total Page")]
        [Category("PagerControls")]
        public int PageNum
        {
            get
            {
                object objPage = ViewState["_PageNum"];
                int _PageNum = 1;
                if (objPage == null)
                {
                    _PageNum = 1;
                }
                else
                {
                    _PageNum = (int)objPage;
                }
                return _PageNum;
            }
            set { ViewState["_PageNum"] = value; }
        }

        [Bindable(true)]
        [DefaultValue(0)]
        [Description("Total Record")]
        [Category("PagerControls")]
        public int TotalRecord
        {
            get
            {
                object objPage = ViewState["_TotalRecord"];
                int _TotalRecord = 0;
                if (objPage == null)
                {
                    _TotalRecord = 0;
                }
                else
                {
                    _TotalRecord = (int)objPage;
                }
                return _TotalRecord;
            }
            set { ViewState["_TotalRecord"] = value; }
        }

        [Bindable(true)]
        [DefaultValue(0)]
        [Description("Current Page")]
        [Category("PagerControls")]
        public int CurrentPage
        {
            get
            {
                object objPage = ViewState["_CurrentPage"];
                int _CurrentPage = 0;
                if (objPage == null)
                {
                    _CurrentPage = 0;
                }
                else
                {
                    _CurrentPage = (int)objPage;
                }
                return _CurrentPage;
            }
            set { ViewState["_CurrentPage"] = value; }
        }
        [Bindable(true)]
        [DefaultValue(0)]
        [Description("First Index")]
        [Category("PagerControls")]
        public int FistIndex
        {
            get
            {

                int _FirstIndex = 1;
                if (ViewState["_FirstIndex"] == null)
                {
                    _FirstIndex = 1;
                }
                else
                {
                    _FirstIndex = Convert.ToInt32(ViewState["_FirstIndex"]);
                }
                return _FirstIndex;
            }
            set { ViewState["_FirstIndex"] = value; }
        }
        [Bindable(true)]
        [DefaultValue(0)]
        [Description("Last Index")]
        [Category("PagerControls")]
        public int LastIndex
        {
            get
            {

                int _LastIndex = 0;
                if (ViewState["_LastIndex"] == null)
                {
                    _LastIndex = 0;
                }
                else
                {
                    _LastIndex = Convert.ToInt32(ViewState["_LastIndex"]);
                }
                return _LastIndex;
            }
            set { ViewState["_LastIndex"] = value; }
        }
        [Bindable(true)]
        [DefaultValue(0)]
        [Description("Onclick")]
        [Category("PagerControls")]
        public string OnClick { get; set; }
        [Bindable(true)]
        [DefaultValue(0)]
        [Description("Onclick")]
        [Category("PagerControls")]
        public string AjaxParameter { get; set; }
        [Bindable(true)]
        [DefaultValue(0)]
        [Description("Page Link Title")]
        [Category("PagerControls")]
        public string PageLinkTitle { get; set; }


        private string LabelResult
        {
            get
            {
                int StartRecordIndex = (CurrentPage - 1) * PageSize + 1;
                int EndRecordIndex = TotalRecord > CurrentPage * PageSize ? CurrentPage * PageSize : TotalRecord;
                return string.Format(ResultsFormat, StartRecordIndex, EndRecordIndex, TotalRecord);
            }
        }

        [Bindable(true)]
        [DefaultValue(0)]
        [Description("ResultsFormat")]
        [Category("PagerControls")]
        public string ResultsFormat
        {
            get
            {
                object objPage = ViewState["_ResultsFormat"];
                string _ResultsFormat = "Hiển thị {0} đến {1} của {2} bản tin";
                if (objPage == null)
                {
                    //int StartRecordIndex = (CurrentPage - 1) * PageSize + 1;
                    //int EndRecordIndex = TotalRecord > CurrentPage * PageSize ? CurrentPage * PageSize : TotalRecord;                  
                    _ResultsFormat = "Hiển thị {0} đến {1} của {2} bản tin"; //string.Format("Hiển thị {0} đến {1} của {2} bản tin", StartRecordIndex, EndRecordIndex, TotalRecord);
                }
                else
                {
                    _ResultsFormat = (string)objPage;
                }
                return _ResultsFormat;
            }
            set { ViewState["_ResultsFormat"] = value; }
        }

        #endregion

        int UpdatePagerControl()
        {
            int TotalPage = (TotalRecord % PageSize == 0) ? TotalRecord / PageSize : TotalRecord / PageSize + 1;

            FistIndex = CurrentPage - 5;
            LastIndex = 0;
            if (CurrentPage > 5)
            {
                LastIndex = CurrentPage + 5;
            }
            else
            {
                LastIndex = 10;
            }
            if (LastIndex > TotalPage)
            {
                LastIndex = TotalPage;
                FistIndex = LastIndex - 10;
            }
            if (FistIndex < 1)
            {
                FistIndex = 1;
            }
            return TotalPage;
        }
        #region PagingDefault
        /*
        Using bootstrap
        * 
        <ul class="pagination">
          <li><a href="#">&laquo;</a></li>
          <li><a href="#">1</a></li>
          <li><a href="#">2</a></li>
          <li><a href="#">3</a></li>
          <li><a href="#">4</a></li>
          <li><a href="#">5</a></li>
          <li><a href="#">&raquo;</a></li>
        </ul>

        */
        private string PagingDefault()
        {

            StringBuilder link = new StringBuilder();
            link.AppendLine(" <div style=\"display:none\">Dịch vụ thiết kế website chuyên nghiệp - Lê Thanh Tuấn | http://tuanitpro.com </div>");
            link.AppendLine(" <div class=\"pull-right hidden-xs pagination\">");
            if (TotalRecord > 0)
                link.Append(LabelResult);
            link.AppendLine("</div>");

            int TotalPage = UpdatePagerControl();
            link.AppendLine("<div class=\"pull-left\">");
            if (TotalPage >= 2)
            {

                link.AppendLine("<ul class=\"pagination\">");

                if (CurrentPage != 1)
                {
                    link.Append("<li><a href=" + UpdateQueryStringItem(Context.Request, QueryStringKey, "1") + "><span class=\"firstLink\">" + FirstPageText + "</span></a></li>");
                    link.Append("<li><a href=" + UpdateQueryStringItem(Context.Request, QueryStringKey, (CurrentPage - 1).ToString()) + "><span class=\"prevLink\">" + PreviousPageText + "</span></a></li>");
                }
                for (int i = FistIndex; i < LastIndex + 1; i++)
                {
                    if (i != CurrentPage)
                    {
                        link.Append(" <li><a href=" + UpdateQueryStringItem(Context.Request, QueryStringKey, i.ToString()) + ">" + i.ToString() + "</a> </li>");
                    }
                    else
                        link.Append("<li class=\"active\"><a href='#'>" + i.ToString() + "</a></li>");
                }
                if (CurrentPage != TotalPage)
                {
                    link.Append("<li> <a href=" + UpdateQueryStringItem(Context.Request, QueryStringKey, (CurrentPage + 1).ToString()) + " class=\"nextLink\">" + NextPageText + " </a></li>");
                    link.Append("<li> <a href=" + UpdateQueryStringItem(Context.Request, QueryStringKey, TotalPage.ToString()) + " class=\"lastLink\">" + LastPageText + " </a></li>");
                }
            }
            link.AppendLine("</ul>");
            link.AppendLine("<ul class=\"pagination\"><li>");
            link.AppendLine("<a href=" + Context.Request.Url.AbsoluteUri + "><i class='fa fa-refresh'></i> Tải lại</a>");
            link.AppendLine("</li></ul>");
            link.AppendLine("</div>");
            return link.ToString();
        }
      

        private string PagingNumeric()
        {

                      StringBuilder link = new StringBuilder();


            link.AppendLine(" <div class=\"pull-right hidden-xs pagination\">");
            if (TotalRecord > 0)
                link.Append(LabelResult);
            link.AppendLine("</div>");

            int TotalPage = UpdatePagerControl();
            link.AppendLine("<div class=\"pull-left\">");


            link.AppendLine("<ul class=\"pagination\">");
            if (TotalPage >= 2)
            {
                if (CurrentPage != 1)
                {
                    link.Append("<li><a href=" + UpdateQueryStringItem(Context.Request, QueryStringKey, "1") + ">" + FirstPageText + "</a></li>");
                    link.Append("<li><a href=" + UpdateQueryStringItem(Context.Request, QueryStringKey, (CurrentPage - 1).ToString()) + ">" + PreviousPageText + "</a></li>");
                }
                for (int i = FistIndex; i < LastIndex + 1; i++)
                {
                    if (i != CurrentPage)
                    {
                        link.Append("<li> <a href=" + UpdateQueryStringItem(Context.Request, QueryStringKey, i.ToString()) + ">" + i.ToString() + "</a></li>");
                    }
                    else
                        link.Append("<li class=\"active\"><a href'#'>" + i.ToString() + "</a></li>");
                }
                if (CurrentPage != TotalPage)
                {
                    link.Append("<li> <a href=" + UpdateQueryStringItem(Context.Request, QueryStringKey, (CurrentPage + 1).ToString()) + ">" + NextPageText + "</a></li>");
                    link.Append("<li> <a href=" + UpdateQueryStringItem(Context.Request, QueryStringKey, TotalPage.ToString()) + ">" + LastPageText + "</a></li>");
                }

            }
            link.AppendLine("</ul>");
            link.AppendLine("</div>");
            return link.ToString();
        }


        private string UrlFriendly()
        {
            int TotalPage = UpdatePagerControl();

            StringBuilder link = new StringBuilder();

            if (TotalPage >= 2)
            {
                link.Append(LabelResult);
                if (CurrentPage != 1)
                {
                    //  link.Append("<a href=" + UpdateQueryStringItem(Context.Request, QueryStringKey, "1") + "><span class=\"firstLink\">" + FirstPageText + "</span></a>");
                    // link.Append("<a href=" + UpdateQueryStringItem(Context.Request, QueryStringKey, (CurrentPage - 1).ToString()) + "><span class=\"previous_page\">" + PreviousPageText + "</span></a>");

                    link.Append("<a class=\"previous_page\" rel=\"prev\" href=?" + QueryStringKey + "=" + (CurrentPage - 1).ToString() + ">&#8592; " + PreviousPageText + "</a> ");
                }
                for (int i = FistIndex; i < LastIndex + 1; i++)
                {
                    if (i != CurrentPage)
                    {
                        link.Append("<a href=\"?" + QueryStringKey + "=" + i + "\" class=\"\">" + i + "</a> ");
                    }
                    else
                        link.Append("<em class=\"current\">" + i + " </em> ");
                }
                if (CurrentPage != TotalPage)
                {
                    link.Append(" <a href=?" + QueryStringKey + "=" + (CurrentPage + 1) + " class=\"next_page\" rel=\"next\" >" + NextPageText + "&#8594; </a>");
                    // link.Append(" <a href=" + UpdateQueryStringItem(Context.Request, QueryStringKey, TotalPage.ToString()) + " class=\"lastLink\">" + LastPageText + " </a>");
                }
            }
            return link.ToString();
        }


        #endregion
        #region PagingAjax
        private string PagingDefaultAjax()
        {

            int TotalPage = UpdatePagerControl();

            StringBuilder link = new StringBuilder();

            if (TotalPage >= 2)
            {
                if (CurrentPage != 1)
                {
                    link.Append("<a href='#First' onclick=" + OnClick + "(" + AjaxParameter + "," + 1 + ")><b>" + FirstPageText + "</b></a>");
                    link.Append(" <a href='#Prev' onclick=" + OnClick + "(" + AjaxParameter + "," + (CurrentPage - 1).ToString() + ")><b>" + PreviousPageText + "</b></a>");
                }
                for (int i = FistIndex; i < LastIndex; i++)
                {
                    if (i != CurrentPage)
                    {
                        link.Append(" <a href='#Page" + i + "' onclick=" + OnClick + "(" + AjaxParameter + "," + i + ")>[<b>" + i.ToString() + "</b>]</a>");
                    }
                    else
                        link.Append(" [<b>" + i.ToString() + "</b>]");
                }
                if (CurrentPage != TotalPage)
                {
                    link.Append(" <a href='#Next' onclick=" + OnClick + "(" + AjaxParameter + "," + (CurrentPage + 1).ToString() + ")><b>" + NextPageText + "</b></a>");
                    link.Append(" <a href='#Last' onclick=" + OnClick + "(" + AjaxParameter + "," + TotalPage + ") ><b>" + LastPageText + "</b></a>");
                }
            }
            return link.ToString();
        }
        private string PagingNumericAjax()
        {

            int TotalPage = UpdatePagerControl();

            StringBuilder link = new StringBuilder();
            if (TotalPage >= 2)
            {
                for (int i = FistIndex; i < LastIndex + 1; i++)
                {
                    if (i != CurrentPage)
                    {
                        link.Append(" <a href='#Page" + i + "' onclick=" + OnClick + "(" + AjaxParameter + "," + i + ")><b>" + i.ToString() + "</b></a>");
                    }
                    else
                        link.Append(" <b>" + i.ToString() + "</b>");
                }
            }
            return link.ToString();
        }

        #endregion

        #region Utility: UpdateQueryStringItem and IsNumeric

        //This method takes in a HttpRequest, QueryString Key, and new QueryStringValue.
        //What it does is replaces the value of a specific QueryString key if it exists... if it
        //does not exist then it adds it. It also keeps preserves of the other Querystring values.
        static public string UpdateQueryStringItem(System.Web.HttpRequest httpRequest, string queryStringKey, string newQueryStringValue)
        {
            // string NewURL = httpRequest.RawUrl;
            string NewURL = httpRequest.RawUrl;
            //QueryString CONTAINS the Key...
            if (httpRequest.QueryString[queryStringKey] != null)
            {
                string OrignalSet = String.Format("{0}={1}", queryStringKey, httpRequest.QueryString[queryStringKey]);
                string NewSet = String.Format("{0}={1}", queryStringKey, newQueryStringValue);

                //REMOVE the key/value completely since the NewValue is blank
                if (newQueryStringValue.Trim() == "")
                {
                    //Replace Key/Value down the line...
                    NewURL = Regex.Replace(NewURL, "&" + OrignalSet, "", RegexOptions.IgnoreCase);// Thay doi neu ton tai query string

                    //Replace Key/Value at the beginning When there is other
                    //key/values after...
                    NewURL = Regex.Replace(NewURL, "?" + OrignalSet + "&", "?", RegexOptions.IgnoreCase); // Thay doi

                    //Replace Key/Value at the beginning when there 
                    //are NO other Key/Values.
                    NewURL = Regex.Replace(NewURL, "?" + OrignalSet, "", RegexOptions.IgnoreCase);
                }
                //REPLACE old value with new value.
                else
                {
                    NewURL = Regex.Replace(NewURL, OrignalSet, NewSet, RegexOptions.IgnoreCase);
                }
            }
            //Only add the key/value IF the new value is not blank.
            else if (newQueryStringValue.Trim() != "")
            {
                //QueryString DOES NOT CONTAIN the Key... and DOES NOT HAVE other key/value pairs.
                if (httpRequest.QueryString.Count == 0)
                {
                    NewURL += string.Format("?{0}={1}", queryStringKey, newQueryStringValue);
                }
                //QueryString DOES NOT CONTAIN the Key... and HAS other key/value pairs.
                else
                {
                    NewURL += string.Format("&{0}={1}", queryStringKey, newQueryStringValue); // Thay doi
                }
            }
            return NewURL;
        }
        #endregion
        string RenderPaging()
        {

            PagingModeEnum pagingMode = PagingMode;
            if (pagingMode == PagingModeEnum.Default)
                return PagingDefault();
            if (pagingMode == PagingModeEnum.DefaultAjax)
                return PagingDefaultAjax();
            if (pagingMode == PagingModeEnum.Numeric)
                return PagingNumeric();
            if (pagingMode == PagingModeEnum.UrlFriendly)
                return UrlFriendly();

            else
                return PagingDefault();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            // base.Render(writer);
            RenderBeginTag(writer);

            writer.Write(RenderPaging());
            RenderEndTag(writer);
        }
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            base.RenderBeginTag(writer);
        }
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            base.RenderEndTag(writer);
        }
    }

}
