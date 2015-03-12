using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data;

namespace ColorLife.Core.Web
{
	#region Enums (Public)

	/// <summary>
	///	Allows you to switch between paging modes.
	/// </summary>
	public enum PagingModeType
	{
		PostBack, QueryString
	}

	/// <summary>
	/// Allows you to specify where you'd like the Back & Next Buttons/Links to appear
	/// </summary>
	public enum BackNextLocationType
	{
		Right, Left, Split, None
	}

	/// <summary>
	/// Allows you to switch between Buttons and Text links.
	/// </summary>
	public enum BackNextDisplayType
	{	
		Buttons, HyperLinks
	}

	/// <summary>
	/// Allows you to specify where you want the results to display.
	/// </summary>
	public enum PageNumbersDisplayType
	{	
		Numbers, Results
	}

	/// <summary>
	/// Allows you to specify where you want the results to display.
	/// </summary>
	public enum ResultsLocationType
	{	
		Top, Bottom, None
	}

	#endregion

	/// <summary>
	/// CollectionPager. Allows you to easily add paging capabilities to such
	/// things as the Repeater control. Can also be used with any Collection.
	/// Originally developed by Kevin Nord
	/// </summary>
	
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:CollectionPager runat=server></{0}:CollectionPager>")]
	public class CollectionPager : System.Web.UI.Control, IPostBackEventHandler
	{
		// Properties (Private)

		#region Data Related
		
		protected PagedDataSource _PagedDataSet;
		protected int _MaxPages = 10;
		protected int _CurrentPage = 1;
		protected string _RenderedHtml = "";
		
		#endregion

		#region Behavior Related
		
		protected PagingModeType _PagingMode = PagingModeType.QueryString;
		protected string _QueryStringKey	= "Page";
		protected bool _IgnoreQueryString = false;

		#endregion

		#region Control

		protected string _ControlStyle = "";
		protected string _ControlCssClass = "";
		protected int	_SectionPadding	= 10;
		protected bool _HideOnSinglePage = true;
		protected string _BindToControl = "";
		
		#endregion

		#region Results Info
		
		protected string _ResultsStyle = "PADDING-BOTTOM:4px;PADDING-TOP:4px;FONT-WEIGHT: bold;";
		protected string _ResultsFormat = "Displaying results {0}-{1} (of {2})";
		protected ResultsLocationType _ResultsLocation = ResultsLocationType.Top;

		#endregion

		#region Label
		
		protected string _LabelStyle = "FONT-WEIGHT: bold;";
		protected bool _ShowLabel	= true;
		protected string _LabelText = "Page:";

		#endregion

		#region Page Numbers

		protected string _PageNumbersStyle = "";
		protected string _PageNumberStyle = "";
		protected PageNumbersDisplayType _PageNumbersDisplay = PageNumbersDisplayType.Numbers; //TODO: Update ViewState
		protected string _PageNumbersSeparator	= "-";
		protected bool _ShowPageNumbers = true;
		protected bool _UseSlider = true;
		protected int _SliderSize = 10;

		#endregion

		#region Back/Next Section

		protected string _BackNextStyle = "";
		protected BackNextDisplayType _BackNextDisplay = BackNextDisplayType.HyperLinks;
		protected BackNextLocationType _BackNextLocation = BackNextLocationType.Right;
		protected bool _ShowFirstLast = false;

		protected string _BackText	= "« Back";
		protected string _NextText	= "Next »";
		protected string _FirstText	= "First";
		protected string _LastText	= "Last";

		//Buttons Version
		protected string _BackNextButtonStyle = "";

		//Link version
		protected string _BackNextLinkSeparator	= "·";

		#endregion

		//Contructor

		#region Contructor

		public CollectionPager()
		{
			_PagedDataSet = new PagedDataSource();
			_PagedDataSet.PageSize = 20;
			_PagedDataSet.CurrentPageIndex = 0;
			_PagedDataSet.AllowPaging = true;
		}

		#endregion

		// Accessors

		#region * Runtime properties *

		public int PageCount
		{
			get
			{
				int PageCount = 0;
				if(Context==null)
				{
					PageCount = MaxPages;
				}
				else
				{
				
					PageCount = _PagedDataSet.PageCount;
					if(PageCount > MaxPages)
					{
						PageCount = MaxPages;
					}
				}
				return PageCount;
			}
		}

		public int CurrentPage
		{
			get
			{
				if(_CurrentPage>MaxPages)
				{
					return MaxPages;
				}
				else
				{
					return _CurrentPage;
				}
			}
		}

		public int RecordStart
		{
			get
			{
				if(Context==null)
				{
					return 0;
				}
				else
				{
					return ((CurrentPage-1)*_PagedDataSet.PageSize)+1;
				}
			}
		}

		public int RecordEnd
		{
			get
			{
				if(Context==null)
				{
					return PageSize;
				}
				else
				{
					return RecordStart - 1 + _PagedDataSet.Count;
				}
			}
		}

		public int TotalRecords
		{
			get
			{
				if(Context==null)
				{
					return MaxPages * PageSize;
				}
				else if (_PagedDataSet.DataSourceCount > MaxPages*PageSize)
				{
					return MaxPages * PageSize;
				}				
				else
				{
					return _PagedDataSet.DataSourceCount;
				}
			}
		}		
	
		public string RenderedHtml
		{
			get
			{
				return _RenderedHtml;
			}
		}

		#endregion

		#region Data Related

		[Bindable(false)]
		[Category("Data")]
		public System.Collections.IEnumerable DataSource 
		{
			set
			{
				this._PagedDataSet.DataSource = value;
			}
			get
			{
				return this._PagedDataSet.DataSource;
			}
		}

		[Bindable(false)]
		[Category("Data")] 
		public PagedDataSource DataSourcePaged
		{
			get
			{
				return this._PagedDataSet;
			}
		}

		[Bindable(false)]
		[Category("Data")] 
		public int PageSize 
		{
			get
			{
				return _PagedDataSet.PageSize;
			}

			set
			{
				_PagedDataSet.PageSize = value;
			}
		}

		[Bindable(false)]
		[Category("Data")] 
		public int MaxPages 
		{
			get
			{
				return _MaxPages;
			}

			set
			{
				_MaxPages = value;
			}
		}

		#endregion

		#region Behavior

		[Bindable(false)]
		[Category("Behavior")] 
		public string QueryStringKey 
		{
			get
			{
				return _QueryStringKey;
			}

			set
			{
				if(Regex.IsMatch(value, "[a-zA-z]"))
				{
					_QueryStringKey = value;
				}
			}
		}

		[Bindable(false)]
		[Category("Behavior")] 
		public PagingModeType PagingMode 
		{
			get
			{
				return _PagingMode;
			}

			set
			{
				if(Enum.IsDefined(typeof(PagingModeType), value))
				{
					_PagingMode = value;
				}
			}
		}

		[Bindable(false)]
		[Category("Control")] 
		public bool IgnoreQueryString
		{
			get{return _IgnoreQueryString;}
			set{_IgnoreQueryString = value;}
		}
		
		#endregion		
		
		#region Control

		[Bindable(false)]
		[Category("Control")] 
		public string ControlStyle
		{
			get
			{
				return _ControlStyle;
			}
			set
			{
				_ControlStyle = value;
			}
		}

		[Bindable(false)]
		[Category("Control")] 
		public string ControlCssClass
		{
			get
			{
				return _ControlCssClass;
			}
			set
			{
				_ControlCssClass = value;
			}
		}

		[Bindable(false)]
		[Category("Control")] 
		public int SectionPadding 
		{
			get
			{
				return _SectionPadding;
			}

			set
			{
				_SectionPadding = value;
			}
		}

		[Bindable(false)]
		[Category("Control")] 
		public bool HideOnSinglePage 
		{
			get
			{
				return _HideOnSinglePage;
			}

			set
			{
				_HideOnSinglePage = value;
			}
		}	

		[Bindable(false)]
		[Category("Control")] 
		public Control BindToControl
		{
			set
			{
				_BindToControl = value.UniqueID;
			}
		}

		#endregion

		#region Results Info

		[Bindable(false)]
		[Category("Results Info")] 
		public string ResultsStyle
		{
			get
			{
				return _ResultsStyle;
			}
			set
			{
				_ResultsStyle = value;
			}

		}

		[Bindable(true)]
		[Category("Results Info")] 
		public ResultsLocationType ResultsLocation
		{
			get
			{
				return _ResultsLocation;
			}
			set
			{
				if(Enum.IsDefined(typeof(ResultsLocationType), value))
				{
					_ResultsLocation = value;
				}
			}
		}

		[Bindable(true)]
		[Category("Results Info")] 
		public string ResultsFormat
		{
			get
			{
				return _ResultsFormat;
			}
			set
			{
				_ResultsFormat = value.Trim();
			}
		}
		
		#endregion

		#region Label

		[Bindable(false)]
		[Category("Label")] 
		public string LabelStyle
		{
			get
			{
				return _LabelStyle;
			}
			set
			{
				_LabelStyle = value;
			}

		}

		[Bindable(false)]
		[Category("Label")] 
		public bool ShowLabel 
		{
			get
			{
				return _ShowLabel;
			}

			set
			{
				_ShowLabel = value;
			}
		}

		[Bindable(false)]
		[Category("Label")] 
		public string LabelText 
		{
			get
			{
				return _LabelText;
			}

			set
			{
				_LabelText = value.Trim();
			}
		}

		#endregion

		#region Page Numbers

		[Bindable(false)]
		[Category("Page Numbers")] 
		public PageNumbersDisplayType PageNumbersDisplay
		{
			get
			{
				return _PageNumbersDisplay;
			}

			set
			{
				if(Enum.IsDefined(typeof(PageNumbersDisplayType), value))
				{
					_PageNumbersDisplay = value;
				}
			}
		}

		[Bindable(false)]
		[Category("Page Numbers")] 
		public bool ShowPageNumbers
		{
			get
			{
				return _ShowPageNumbers;
			}
			set
			{
				_ShowPageNumbers = value;
			}
		}
		
		[Bindable(false)]
		[Category("Page Numbers")] 
		public string PageNumbersSeparator
		{
			get
			{
				return _PageNumbersSeparator;
			}

			set
			{
				if (value.Trim() == "")
				{
					_PageNumbersSeparator = "&nbsp;";
				}
				else
				{
					_PageNumbersSeparator = value.Trim();
				}
			}
		}

		[Bindable(false)]
		[Category("Page Numbers")] 
		public string PageNumbersStyle
		{
			get
			{
				return _PageNumbersStyle;
			}
			set
			{
				_PageNumbersStyle = value;
			}
		}

		[Bindable(false)]
		[Category("Page Numbers")] 
		public string PageNumberStyle
		{
			get
			{
				return _PageNumberStyle;
			}
			set
			{
				_PageNumberStyle = value;
			}
		}

		[Bindable(false)]
		[Category("Page Numbers")] 
		public bool UseSlider 
		{
			get
			{
				return _UseSlider;
			}

			set
			{
				_UseSlider = value;
			}
		}		
		
		[Bindable(false)]
		[Category("Page Numbers")] 
		public int SliderSize 
		{
			get
			{
				return _SliderSize;
			}

			set
			{
				_SliderSize = value;
			}
		}	

		#endregion

		#region Back/Next Section

		[Bindable(false)]
		[Category("Back/Next")] 
		public string BackNextStyle
		{
			get
			{
				return _BackNextStyle;
			}
			set
			{
				_BackNextStyle = value;
			}
		}

		[Bindable(false)]
		[Category("Back/Next")] 
		public string BackNextButtonStyle
		{
			get
			{
				return _BackNextButtonStyle;
			}
			set
			{
				_BackNextButtonStyle = value;
			}
		}

		[Bindable(false), Category("Back/Next")] 
		public BackNextDisplayType BackNextDisplay
		{
			get
			{
				return _BackNextDisplay;
			}

			set
			{
				if(Enum.IsDefined(typeof(BackNextDisplayType), value))
				{
					_BackNextDisplay = value;
				}
			}
		}

		[Bindable(false), Category("Back/Next")] 
		public BackNextLocationType BackNextLocation
		{
			get
			{
				return _BackNextLocation;
			}

			set
			{
				if(Enum.IsDefined(typeof(BackNextLocationType), value))
				{
					_BackNextLocation = value;
				}
			}
		}

		[Bindable(false), Category("Back/Next")] 
		public string BackNextLinkSeparator 
		{
			get
			{
				return _BackNextLinkSeparator;
			}

			set
			{
				_BackNextLinkSeparator = value.Trim();
			}
		}

		[Bindable(false), Category("Back/Next")] 
		public bool ShowFirstLast
		{
			get
			{
				return _ShowFirstLast;
			}

			set
			{
				_ShowFirstLast = value;
			}
		}

		//BackNext Button/Hyperlink Display

		[Bindable(false), Category("Back/Next")] 
		public string BackText 
		{
			get
			{
				return _BackText;
			}

			set
			{
				_BackText = value.Trim();
			}
		}

		[Bindable(false), Category("Back/Next")] 
		public string NextText 
		{
			get
			{
				return _NextText;
			}

			set
			{
				_NextText = value.Trim();
			}
		}

		[Bindable(false), Category("Back/Next")] 
		public string FirstText 
		{
			get
			{
				return _FirstText;
			}

			set
			{
				_FirstText = value.Trim();
			}
		}

		[Bindable(false), Category("Back/Next")] 
		public string LastText 
		{
			get
			{
				return _LastText;
			}

			set
			{
				_LastText = value.Trim();
			}
		}

		#endregion

		//Control Lifecycle

		#region Save Viewstate

		protected override object SaveViewState()
		{  
			// Save State as a cumulative array of objects.
			object baseState = base.SaveViewState();
			object[] allStates = new object[35];
			allStates[0] = baseState;

			//Data Related
			allStates[1] = _MaxPages;
			allStates[2] = _CurrentPage ;

			//Behavior Related
			allStates[3] = _PagingMode;
			allStates[4] = _QueryStringKey;

			//Control
			allStates[5] = _ControlStyle;
			allStates[6] = _ControlCssClass;
			allStates[7] = _SectionPadding;
			allStates[8] = _HideOnSinglePage;
			allStates[9] = _BindToControl;

			//Results Info
			allStates[10] = _ResultsStyle;
			allStates[11] = _ResultsFormat;
			allStates[12] = _ResultsLocation;

			//Label
			allStates[13] = _LabelStyle;
			allStates[14] = _ShowLabel;
			allStates[15] = _LabelText;

			//Page Numbers
			allStates[16] = _PageNumbersStyle;
			allStates[17] = _PageNumbersDisplay;
			allStates[18] = _PageNumbersSeparator;
			allStates[19] = _ShowPageNumbers;

			//Back/Next Section
			allStates[20] = _BackNextStyle;
			allStates[21] = _BackNextDisplay;
			allStates[22] = _BackNextLocation;
			allStates[23] = _ShowFirstLast;

			allStates[24] = _BackText;
			allStates[25] = _NextText;
			allStates[26] = _FirstText;
			allStates[27] = _LastText;

			//Buttons Version
			allStates[28] = _BackNextButtonStyle;

			//Link version
			allStates[29] = _BackNextLinkSeparator;

			//Additonal Items
			allStates[30] = _UseSlider;
			allStates[31] = _SliderSize;

			allStates[32] = _IgnoreQueryString;
			allStates[33] = _RenderedHtml;
			allStates[34] = _PageNumberStyle;

			return allStates;
		}

		#endregion

		#region Load Viewstate

		protected override void LoadViewState(object savedState) 
		{
			if (savedState != null)
			{
				// Load State from the array of objects that was saved at ;
				// SavedViewState.
				
				object[] allStates = (object[])savedState;
				
				if (allStates[0] != null)
				{
					base.LoadViewState(allStates[0]);
				}

				//Data Related
				if (allStates[1] != null)	_MaxPages = (int)allStates[1];
				if (allStates[2] != null)	_CurrentPage = (int)allStates[2];

				//Behavior Related
				if (allStates[3] != null)	_PagingMode = (PagingModeType)allStates[3];
				if (allStates[4] != null)	_QueryStringKey = (string)allStates[4];

				//Control
				if (allStates[5] != null)	_ControlStyle = (string)allStates[5];
				if (allStates[6] != null)	_ControlCssClass = (string)allStates[6];
				if (allStates[7] != null)	_SectionPadding = (int)allStates[7];
				if (allStates[8] != null)	_HideOnSinglePage = (bool)allStates[8];
				if (allStates[9] != null)	_BindToControl = (string)allStates[9];

				//Results Info
				if (allStates[10] != null)	_ResultsStyle = (string)allStates[10];
				if (allStates[11] != null)	_ResultsFormat = (string)allStates[11];
				if (allStates[12] != null)	_ResultsLocation = (ResultsLocationType)allStates[12];

				//Label
				if (allStates[13] != null)	_LabelStyle = (string)allStates[13];
				if (allStates[14] != null)	_ShowLabel = (bool)allStates[14];
				if (allStates[15] != null)	_LabelText = (string)allStates[15];

				//Page Numbers
				if (allStates[16] != null)	_PageNumbersStyle = (string)allStates[16];
				if (allStates[17] != null)	_PageNumbersDisplay = (PageNumbersDisplayType)allStates[17];
				if (allStates[18] != null)	_PageNumbersSeparator = (string)allStates[18];
				if (allStates[19] != null)	_ShowPageNumbers = (bool)allStates[19];

				//Back/Next Section
				if (allStates[20] != null)	_BackNextStyle = (string)allStates[20];
				if (allStates[21] != null)	_BackNextDisplay = (BackNextDisplayType)allStates[21];
				if (allStates[22] != null)	_BackNextLocation = (BackNextLocationType)allStates[22];
				if (allStates[23] != null)	_ShowFirstLast = (bool)allStates[23];

				if (allStates[24] != null)	_BackText = (string)allStates[24];
				if (allStates[25] != null)	_NextText = (string)allStates[25];
				if (allStates[26] != null)	_FirstText = (string)allStates[26];
				if (allStates[27] != null)	_LastText = (string)allStates[27];

				//Buttons Version
				if (allStates[28] != null)	_BackNextButtonStyle = (string)allStates[28];

				//Link version
				if (allStates[29] != null)	_BackNextLinkSeparator = (string)allStates[29];

				//Additional 
				if (allStates[30] != null)	_UseSlider = (bool)allStates[30];
				if (allStates[31] != null)	_SliderSize = (int)allStates[31];
				if (allStates[32] != null)	_IgnoreQueryString = (bool)allStates[32];
				if (allStates[33] != null)	_RenderedHtml = (string)allStates[33];
				if (allStates[34] != null)	_PageNumberStyle = (string)allStates[34];			}
		}

		#endregion

		#region *** OnPreRender ***

		protected override void OnPreRender(EventArgs e)
		{
			//Figure Out CurrentState Info

			if(PagingMode==PagingModeType.QueryString)
			{
				BindToQueryString();
			}			
			
			_PagedDataSet.CurrentPageIndex = _CurrentPage - 1;

			//If only 1 page, then hide and exit.
			if(PageCount<=1 && Context!=null && HideOnSinglePage)
			{
				this.Visible = false;
			}
			else
			{
				BuildControl();
			}

			if(_BindToControl.Length>0)
			{
				Control BoundControl = this.Page.FindControl(this._BindToControl);
				if(BoundControl!=null)
				{
					BoundControl.DataBind();
				}
			}		

			base.OnPreRender (e);
		}
		
		#endregion

		#region Render

		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void Render(HtmlTextWriter output)
		{
			if(Context==null)
			{
				BuildControl();
			}
			output.Write(_RenderedHtml);
			base.Render(output);
		}

		#endregion

		//Build Methods (builds _RenderedHtml string)

		#region Build Control

		protected virtual void BuildControl()
		{
			StringBuilder Html = new StringBuilder();

			string CurrentPages = "";
			string Results = "";
			string BackNextNavBack = "";
			string BackNextNavSeparator = "";
			string BackNextNavNext = "";

			Results = BuildResults(ResultsFormat);

			if(PageCount==0 || Context==null)
			{
				if(ResultsLocation != ResultsLocationType.None)
				{
					Results = string.Format(ResultsFormat, "1", PageSize.ToString(), (MaxPages*PageSize).ToString());
				}
			}
			else
			{
				//Make Results (if needed)
				if(ResultsLocation != ResultsLocationType.None)
				{
					Results = string.Format(ResultsFormat
						, RecordStart
						, RecordEnd
						, TotalRecords);
				}
			}

			//Make Back & Next section (if needed)
			if(BackNextLocation != BackNextLocationType.None)
			{
				if(BackNextDisplay==BackNextDisplayType.Buttons)
				{
					BackNextNavBack = BuildButtonNavBack(CurrentPage);
					BackNextNavSeparator += "&nbsp;&nbsp;";
					BackNextNavNext += BuildButtonNavNext(CurrentPage, PageCount);
				}
				else
				{
					BackNextNavBack = BuildLinkNavBack(CurrentPage);
					BackNextNavSeparator += string.Format(" {0} ", BackNextLinkSeparator);
					BackNextNavNext += BuildLinkNavNext(CurrentPage, PageCount);
				}
			}
		
			//========================================================
			// NEW RENDER

			//-------------------------------
			// START CONTROL
			//-------------------------------
			bool IsSpaceNeeded = false;

			Html.Append("<div");
			if(_ControlStyle.Length>0)
			{
				Html.AppendFormat(" style='{0}'", _ControlStyle);
			}
			if(_ControlCssClass.Length>0)
			{
				Html.AppendFormat(" class='{0}'", _ControlCssClass);
			}

			Html.Append(">");

			// TOP Results
			if(ResultsLocation == ResultsLocationType.Top)
			{
				Html.Append(BuildTop(Results));
			}

			//-------------------------------
			// MIDDLE
			//-------------------------------
			Html.Append("<div>");

			// If Back/Next == Left or Split
			if(BackNextLocation == BackNextLocationType.Left || BackNextLocation == BackNextLocationType.Split)
			{
				if(_BackNextStyle.Length>0)
				{
					Html.AppendFormat("<span style='{0}'>", _BackNextStyle); 
				}
				else
				{
					Html.Append("<span>");
				}
				Html.Append(BackNextNavBack);

				//If Back/Next == LEFT
				if(BackNextLocation == BackNextLocationType.Left)
				{
					Html.Append(BackNextNavSeparator);
					Html.Append(BackNextNavNext);
				}
				Html.Append("</span>");
				IsSpaceNeeded = true;
			}
			
			if(ShowLabel && LabelText.Length>0)
			{
				if(IsSpaceNeeded)
				{
					Html.AppendFormat("<span style='padding-left:{0}'></span>", SectionPadding);
				}

				if(_LabelStyle.Length>0)
				{
					Html.AppendFormat("<span style='{0}'>", _LabelStyle); 
				}
				else
				{
					Html.Append("<span>");
				}
				Html.Append(LabelText);
				Html.Append("</span>");
				IsSpaceNeeded = true;
			}

			//Make Page numbers (if needed)
			if(ShowPageNumbers)
			{
				if(ShowLabel)
				{
					Html.Append("<span style='padding-left:5'></span>");
				}
				else if(IsSpaceNeeded)
				{
					Html.AppendFormat("<span style='padding-left:{0}'></span>", SectionPadding);
				}

				CurrentPages = BuildPageNumbers(CurrentPage, PageCount);
				if(_PageNumbersStyle.Length>0)
				{
					Html.AppendFormat("<span style='{0}'>", _PageNumbersStyle); 
				}
				else
				{
					Html.Append("<span>");
				}
				Html.Append(CurrentPages);
				Html.Append("</span>");
				IsSpaceNeeded = true;
			}
			
			// If Back/Next == Left or Split
			if(BackNextLocation == BackNextLocationType.Right 
				|| BackNextLocation == BackNextLocationType.Split)
			{
				if(IsSpaceNeeded)
				{
					Html.AppendFormat("<span style='padding-left:{0}'></span>", SectionPadding);
				}

				if(_BackNextStyle.Length>0)
				{
					Html.AppendFormat("<span style='{0}'>", _BackNextStyle); 
				}
				else
				{
					Html.Append("<span>");
				}

				//If Back/Next == LEFT
				if(BackNextLocation == BackNextLocationType.Right)
				{
					Html.Append(BackNextNavBack);
					Html.Append(BackNextNavSeparator);
				}

				Html.Append(BackNextNavNext);
				Html.Append("</span>");
			}

			Html.Append("</div>");

			//-------------------------------

			// BOTTOM
			if(ResultsLocation == ResultsLocationType.Bottom)
			{
				Html.Append(BuildBottom(Results));
			}

			//-------------------------------
			// END CONTROL
			//-------------------------------

			Html.Append("</div>");
			_RenderedHtml = Html.ToString();
		}

		#endregion

		#region Build Top

		protected virtual string BuildTop(string resultsHTML)
		{
			StringBuilder sb = new StringBuilder();

			if(_ResultsStyle.Length>0)
			{
				sb.AppendFormat("<div style='{0}'>", _ResultsStyle);
			}
			else
			{
				sb.Append("<div>");
			}
			sb.Append(resultsHTML);
			sb.Append("</div>");

			return sb.ToString();
		}

		#endregion

		#region Build Middle

		protected virtual string BuildMiddle(HtmlTextWriter output)
		{
			StringBuilder sb = new StringBuilder();

			return sb.ToString();
		}

		#endregion

		#region Build Bottom

		protected virtual string BuildBottom(string resultsHTML)
		{
			StringBuilder sb = new StringBuilder();

			if(_ResultsStyle.Length>0)
			{
				sb.AppendFormat("<div style='{0}'>", _ResultsStyle); 
			}
			else
			{
				sb.Append("<div>");
			}
			sb.Append(resultsHTML);
			sb.Append("</div>");

			return sb.ToString();
		}

		#endregion

		//Methods (Private)

		#region Bind current page to QueryString
	
		protected virtual void BindToQueryString()
		{
			if(Context!=null && Context.Request.QueryString[QueryStringKey]!=null && !IgnoreQueryString)
			{
				if(IsNumeric(Context.Request.QueryString[QueryStringKey].ToString()))
				{
					int newPage = int.Parse(Context.Request.QueryString.Get(QueryStringKey));

					ChangePage(newPage);
				}
			}
		}

		#endregion

		#region Change Page

		protected virtual void ChangePage(int newPage)
		{
			if (newPage<=1)
			{
				newPage = 1;
			}
			
			if (newPage > _PagedDataSet.PageCount)
			{
				newPage = _PagedDataSet.PageCount;
			}
			
			if(newPage > MaxPages)
			{
				newPage = MaxPages;
			}

			_CurrentPage = newPage;
		}

		#endregion

		#region Build Page Number Links
		
		//Build Page Number Links
		
		protected virtual string BuildPageNumbers(int currentPage, int pageCount)
		{
			StringBuilder Pages = new StringBuilder();
			string PageNumberStyleTag = "";

			if(_PageNumberStyle.Trim().Length>0)
			{
				PageNumberStyleTag = string.Format(" style='{0}'", _PageNumberStyle);
			}

			int StartPage=1;

			//End Page
			int EndPage=0;
			if(pageCount>MaxPages)
			{
				EndPage = MaxPages;
			}
			else
			{
				EndPage = pageCount;
			}

			//Adjust for slider
			if(UseSlider)
			{
				int Half = (int)Math.Floor((double)(SliderSize-1)/2);
				//8: Half = 3
				//7: Half = 3

				int NumAbove = currentPage + Half + ((SliderSize-1)%2);
				int NumBelow = currentPage - Half;

				if(NumBelow<1)
				{
					NumAbove += (1 - NumBelow);
					NumBelow = 1;
				}

				if(NumAbove>EndPage)
				{
					NumBelow -= (NumAbove - EndPage);
					if(NumBelow<1)
					{
						NumBelow = 1;
					}
					NumAbove = EndPage;
				}

				StartPage = NumBelow;
				EndPage = NumAbove;
			}

			for(int i=StartPage;i<=EndPage;i++)
			{
				int RecordStart;
				int RecordEnd;

				//For Display
				if(Context==null)
				{
					RecordStart = ((i-1)*_PagedDataSet.PageSize)+1;
					RecordEnd = i * _PagedDataSet.PageSize;
					
					if(i>StartPage)
					{
						if(PageNumbersSeparator.ToLower()=="&nbsp;")
						{
							Pages.AppendFormat("{0}", PageNumbersSeparator);
						}
						else
						{
							Pages.AppendFormat(" {0} ", PageNumbersSeparator);
						}
					}
					
					if(i==StartPage)
					{
						if(PageNumbersDisplay==PageNumbersDisplayType.Numbers)
						{
							Pages.AppendFormat("<B>{0}</B>", i.ToString());
						}
						else
						{
							Pages.AppendFormat("[<B>{0}-{1}</B>]", RecordStart, RecordEnd);
						}
					}
					else
					{
						if(PageNumbersDisplay==PageNumbersDisplayType.Numbers)
						{
							Pages.AppendFormat("<a href='#'>{0}</a>", i.ToString());
						}
						else
						{
							Pages.AppendFormat("[<a href='#'>{0}-{1}</a>]", RecordStart, RecordEnd);
						}
					}
				}
				else
				{
					//For Runtime.

					//Figure out RecordStart
					RecordStart = ((i-1)*_PagedDataSet.PageSize) + 1;
				
					//Figure out RecordEnd
				
					if(i*_PagedDataSet.PageSize>_PagedDataSet.DataSourceCount)
					{
						RecordEnd = _PagedDataSet.DataSourceCount;
					}
					else if(_PagedDataSet.Count ==_PagedDataSet.PageSize)
					{
						RecordEnd = (RecordStart - 1) + _PagedDataSet.PageSize;
					}
					else
					{
						RecordEnd = (RecordStart - 1) + _PagedDataSet.Count;
					}

					if(i>StartPage)
					{
						if(PageNumbersSeparator.ToLower()=="&nbsp;")
						{
							Pages.AppendFormat("{0}", PageNumbersSeparator);
						}
						else
						{
							Pages.AppendFormat(" {0} ", PageNumbersSeparator);
						}
					}
					
					if(i==currentPage) 
					{
						if(PageNumbersDisplay==PageNumbersDisplayType.Numbers)
						{
							Pages.AppendFormat("<B>{0}</B>", i.ToString());
						}
						else
						{
							Pages.AppendFormat("[<B>{0}-{1}</B>]", RecordStart, RecordEnd);
						}
					}
					else
					{
						//Set up PostBack link.
						if(PagingMode==PagingModeType.PostBack)
						{
							if(PageNumbersDisplay==PageNumbersDisplayType.Numbers)
							{
								Pages.AppendFormat("<a id=\"{0}\" href=\"javascript:{1}\"{2}>{3}</a>"
									, this.UniqueID
									, Page.GetPostBackEventReference(this,i.ToString())
									, PageNumberStyleTag
									, i);
							}
							else
							{
								Pages.AppendFormat("[<a id=\"{0}\" href=\"'javascript:{1}\"{2}>{3}-{4}</a>]"
									, this.UniqueID
									, Page.GetPostBackEventReference(this,i.ToString())
									, PageNumberStyleTag
									, RecordStart, RecordEnd);
							}
						}
							//Set up querystring link.
						else
						{
							if(PageNumbersDisplay==PageNumbersDisplayType.Numbers)
							{
								Pages.AppendFormat("<a href='{0}'{1}>{2}</a>"
									, UpdateQueryStringItem(Context.Request, QueryStringKey, i.ToString())
									, PageNumberStyleTag
									, i);
							}
							else
							{
								Pages.AppendFormat("[<a href='{0}'{1}>{2}-{3}</a>]"
									, UpdateQueryStringItem(Context.Request, QueryStringKey, i.ToString())
									, PageNumberStyleTag
									, RecordStart
									, RecordEnd);
							}
						}
					}
				}
			}
			return Pages.ToString();
		}
		#endregion

		#region Build Back/Next link Nav

		protected virtual string BuildLinkNavBack(int currentPage)
		{
			StringBuilder LinkNav = new StringBuilder();

			//Next Links
			if(_PagedDataSet.IsFirstPage || _PagedDataSet.DataSourceCount==0)
			{
				if(ShowFirstLast)
				{
					LinkNav.Append(FirstText + "&nbsp;");
				}
				LinkNav.Append(BackText);
			}
			else
			{
				//Set up PostBack link.
				if(PagingMode==PagingModeType.PostBack)
				{
					if(ShowFirstLast)
					{
						LinkNav.AppendFormat("<a id=\"{0}\" href=\"javascript:{1}\">{2}</a>&nbsp;"
							, this.UniqueID
							, Page.GetPostBackEventReference(this,Convert.ToString(1))
							, FirstText);
					}

					LinkNav.AppendFormat("<a id=\"{0}\" href=\"javascript:{1}\">{2}</a>"
						, this.UniqueID
						, Page.GetPostBackEventReference(this,Convert.ToString(currentPage-1))
						, BackText);
				}
				else
				{
					if(ShowFirstLast)
					{
						LinkNav.AppendFormat("<a href='{0}'>{1}</A>&nbsp;"
							,UpdateQueryStringItem(Context.Request, QueryStringKey, Convert.ToString(1))
							, FirstText);
					}
					LinkNav.AppendFormat("<a href='{0}'>{1}</A>"
						,UpdateQueryStringItem(Context.Request, QueryStringKey, Convert.ToString(currentPage-1))
						, BackText);
				}
			}

			return LinkNav.ToString();
		}
		
		protected virtual string BuildLinkNavNext(int currentPage, int pageCount)
		{
			StringBuilder LinkNav = new StringBuilder();

			if(Context==null)
			{
				LinkNav.AppendFormat("<a href='#'>{0}</a>", NextText);
				if(ShowFirstLast)
				{
					LinkNav.AppendFormat("&nbsp;<a href='#'>{0}</a>", LastText);
				}
			}
			else if(currentPage>=MaxPages || currentPage==_PagedDataSet.PageCount)
			{
				LinkNav.Append(NextText);
				if(ShowFirstLast)
				{
					LinkNav.Append("&nbsp;" + LastText);
				}
			}
			else
			{
				//Set up PostBack link.
				if(PagingMode==PagingModeType.PostBack)
				{
					LinkNav.AppendFormat("<a id=\"{0}\" href=\"javascript:{1}\">{2}</a>"
						, this.UniqueID
						, Page.GetPostBackEventReference(this,Convert.ToString(currentPage+1))
						, NextText);
					if(ShowFirstLast)
					{
						LinkNav.AppendFormat("&nbsp;<a id=\"{0}\" href=\"javascript:{1}\">{2}</a>"
							, this.UniqueID
							, Page.GetPostBackEventReference(this,Convert.ToString(pageCount))
							, LastText);
					}
				}
				else
				{
					LinkNav.AppendFormat("<a href='{0}'>{1}</A>"
						,UpdateQueryStringItem(Context.Request, QueryStringKey, Convert.ToString(currentPage+1))
						, NextText);
					if(ShowFirstLast)
					{
						LinkNav.AppendFormat("&nbsp;<a href='{0}'>{1}</A>"
							,UpdateQueryStringItem(Context.Request, QueryStringKey, Convert.ToString(pageCount))
							, LastText);
					}
				}
			}

			return LinkNav.ToString();
		}

		#endregion
		
		#region Build Back/Next button Nav

		protected virtual string BuildButtonNavBack(int currentPage)
		{
			StringBuilder ButtonNav = new StringBuilder();

			//Back Buttons
			if(_PagedDataSet.IsFirstPage || _PagedDataSet.DataSourceCount==0)
			{
				if(ShowFirstLast)
				{
					ButtonNav.AppendFormat("<INPUT TYPE=button VALUE=\"{0}\" DISABLED=true>&nbsp;", FirstText);
				}
				ButtonNav.AppendFormat("<INPUT TYPE=button VALUE=\"{0}\" DISABLED=true>", BackText);
			}
			else
			{
				if(PagingMode==PagingModeType.PostBack)
				{
					if(ShowFirstLast)
					{
						ButtonNav.AppendFormat("<INPUT TYPE=button id=\"{0}\" onclick=\"javascript:{1}\" VALUE=\"{2}\">"
							, this.UniqueID
							, Page.GetPostBackEventReference(this,Convert.ToString(1))
							, FirstText);
					}
					ButtonNav.AppendFormat("<INPUT TYPE=button id=\"{0}\" onclick=\"javascript:{1}\" VALUE=\"{2}\">"
						, this.UniqueID
						, Page.GetPostBackEventReference(this,Convert.ToString(currentPage-1))
						, BackText);
				}
				else
				{
					if(ShowFirstLast)
					{
						ButtonNav.AppendFormat("<INPUT TYPE=button onclick=\"window.location='{0}';\" VALUE=\"{1}\">&nbsp;"
							, UpdateQueryStringItem(Context.Request, QueryStringKey, Convert.ToString(1))
							, FirstText);
					}
					ButtonNav.AppendFormat("<INPUT TYPE=button onclick=\"window.location='{0}';\" VALUE=\"{1}\">"
						, UpdateQueryStringItem(Context.Request, QueryStringKey, Convert.ToString(currentPage-1))
						, BackText);

				}
			}
			return ButtonNav.ToString();
		}

		protected virtual string BuildButtonNavNext(int currentPage, int pageCount)
		{
			StringBuilder ButtonNav = new StringBuilder();

			//Next Buttons
			if(currentPage>=MaxPages || currentPage>= _PagedDataSet.Count|| Context==null)
			{
				ButtonNav.AppendFormat("<INPUT TYPE=button VALUE=\"{0}\" DISABLED=true>", NextText);
				if(ShowFirstLast)
				{
					ButtonNav.AppendFormat("&nbsp;<INPUT TYPE=button VALUE=\"{0}\" DISABLED=true>", LastText);
				}
			}
			else
			{
				if(PagingMode==PagingModeType.PostBack)
				{
					ButtonNav.AppendFormat("<INPUT TYPE=button id=\"{0}\" onclick=\"javascript:{1}\" VALUE=\"{2}\">"
						, this.UniqueID
						, Page.GetPostBackEventReference(this,Convert.ToString(currentPage+1))
						, NextText);
					if(ShowFirstLast)
					{
						ButtonNav.AppendFormat("&nbsp;<INPUT TYPE=button id=\"{0}\" onclick=\"javascript:{1}\" VALUE=\"{2}\">"
							, this.UniqueID
							, Page.GetPostBackEventReference(this,Convert.ToString(pageCount))
							, LastText);
					}
				}
				else
				{
					ButtonNav.AppendFormat("<INPUT TYPE=button onclick=\"window.location='{0}';\" VALUE=\"{1}\">"
						, UpdateQueryStringItem(Context.Request, QueryStringKey, Convert.ToString(currentPage+1))
						, NextText);
					if(ShowFirstLast)
					{
						ButtonNav.AppendFormat("<INPUT TYPE=button onclick=\"window.location='{0}';\" VALUE=\"{1}\">"
							, UpdateQueryStringItem(Context.Request, QueryStringKey, Convert.ToString(pageCount))
							, LastText);
					}
				}
			}
			return ButtonNav.ToString();
		}
		#endregion

		#region Build Results

		public virtual string BuildResults(string resultsFormat)
		{
			if(PageCount==0 || Context==null)
			{
				if(ResultsLocation != ResultsLocationType.None)
				{
					return string.Format(resultsFormat, "1", PageSize.ToString(), (MaxPages*PageSize).ToString());
				}
			}
			else if(ResultsLocation != ResultsLocationType.None)
			{
				return string.Format(resultsFormat
					, RecordStart
					, RecordEnd
					, TotalRecords);
			}

			return "";
		}

		#endregion

		//Methods (Public)

		#region Utility: UpdateQueryStringItem and IsNumeric

		//This method takes in a HttpRequest, QueryString Key, and new QueryStringValue.
		//What it does is replaces the value of a specific QueryString key if it exists... if it
		//does not exist then it adds it. It also keeps preserves of the other Querystring values.
		static public string UpdateQueryStringItem(System.Web.HttpRequest httpRequest, string queryStringKey, string newQueryStringValue)
		{
			string NewURL = httpRequest.RawUrl;

			//QueryString CONTAINS the Key...
			if(httpRequest.QueryString[queryStringKey]!=null)
			{
				string OrignalSet = String.Format("{0}={1}", queryStringKey, httpRequest.QueryString[queryStringKey]);
				string NewSet = String.Format("{0}={1}", queryStringKey, newQueryStringValue);
				
				//REMOVE the key/value completely since the NewValue is blank
				if(newQueryStringValue.Trim()=="")
				{
					//Replace Key/Value down the line...
					NewURL = Regex.Replace(NewURL, "&"+OrignalSet, "", RegexOptions.IgnoreCase);

					//Replace Key/Value at the beginning When there is other
					//key/values after...
					NewURL = Regex.Replace(NewURL, "?"+OrignalSet+"&", "?", RegexOptions.IgnoreCase);

					//Replace Key/Value at the beginning when there 
					//are NO other Key/Values.
					NewURL = Regex.Replace(NewURL, "?"+OrignalSet, "", RegexOptions.IgnoreCase);
				}
					//REPLACE old value with new value.
				else
				{
					NewURL = Regex.Replace(NewURL, OrignalSet, NewSet, RegexOptions.IgnoreCase);
				}
			}
				//Only add the key/value IF the new value is not blank.
			else if(newQueryStringValue.Trim()!="")
			{
				//QueryString DOES NOT CONTAIN the Key... and DOES NOT HAVE other key/value pairs.
				if(httpRequest.QueryString.Count==0)
				{
					NewURL += string.Format("?{0}={1}", queryStringKey, newQueryStringValue);
				}
					//QueryString DOES NOT CONTAIN the Key... and HAS other key/value pairs.
				else
				{
					NewURL += string.Format("&{0}={1}", queryStringKey, newQueryStringValue);
				}
			}
			return NewURL;
		}

		// This method returns a true if the passed in string contains only alphanumeric characters
		public static bool IsNumeric(string Text)
		{
			if (Text == "" || Text == null)
			{
				return false;
			}
			Text = Text.Trim();
			bool bResult = Regex.IsMatch(Text,@"^\d+$");
			return bResult;
		}
		#endregion

		#region Handle PostBacks
		
		// Defines the Click event.
		public event EventHandler Click;
            
		// Invokes delegates registered with the Click event.

		protected virtual void OnClick(EventArgs e) 
		{
                  
			if (Click != null) 
			{
				Click(this, e);
			}     
		}            
            
		// Method of IPostBackEventHandler that raises change events.
		//
		public virtual void RaisePostBackEvent(string eventArgument)
		{
			if(IsNumeric(eventArgument))
			{
				ChangePage(int.Parse(eventArgument));
			}
			OnClick(new EventArgs());
		}
		#endregion

	}
}
