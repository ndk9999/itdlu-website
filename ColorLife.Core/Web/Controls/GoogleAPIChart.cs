// -----------------------------------------------------------------------------------
// Copyright (C) Chris Pietschmann (http://pietschsoft.com) 2007. All rights reserved.
// This source is subject to the Creative Commons Attribution 3.0 United States License
// See http://creativecommons.org/licenses/by/3.0/us/
// If you create derivative works of this, please leave the above copyright notice in place.
// -----------------------------------------------------------------------------------
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

///
/// Google Chart API Documentation: http://code.google.com/apis/chart
/// 
namespace ColorLife.Core.Web
{
    /// <summary>
    /// Summary description for Chart
    /// </summary>
    public class GoogleAPIChart : System.Web.UI.WebControls.WebControl
    {
        protected override void CreateChildControls()
        {
            Image img = new Image();
            img.ImageUrl = "http://chart.apis.google.com/chart?" +
                "chs=" + this.Width.Value.ToString() + "x" + this.Height.Value.ToString() + "" +
                "&cht=lc" +
                "&chd=s:" + this.EncodeDataValues() + "" +
                "&chxt=x,y" +
                "&chxl=0:" + this.GetDataTitles() + "1:" + this.GetYAxisLabels() + "" +
                "";

            if (!string.IsNullOrEmpty(this.LineColor))
                img.ImageUrl += "&chco=" + this.LineColor;

            if (!string.IsNullOrEmpty(this.BackgroundColor))
                img.ImageUrl += "&chf=bg,s," + this.BackgroundColor;

            if (this.ShowGridLines)
                img.ImageUrl += "&chg=" + this.GridLinesXAxisStepSize + "," + this.GridLinesYAxisStepSize;

            img.ToolTip = this.ToolTip;
            this.Controls.Add(img);

            base.CreateChildControls();
        }

        private bool _ShowGridLines = false;
        public bool ShowGridLines
        {
            get { return _ShowGridLines; }
            set { _ShowGridLines = value; }
        }

        private int _GridLinesXAxisStepSize = 10;
        public int GridLinesXAxisStepSize
        {
            get { return _GridLinesXAxisStepSize; }
            set { _GridLinesXAxisStepSize = value; }
        }

        private int _GridLinesYAxisStepSize = 10;
        public int GridLinesYAxisStepSize
        {
            get { return _GridLinesYAxisStepSize; }
            set { _GridLinesYAxisStepSize = value; }
        }


        private string _BackgroundColor = null;
        public string BackgroundColor
        {
            get { return _BackgroundColor; }
            set { _BackgroundColor = value; }
        }

        private int _MaxValue = 100;
        public int MaxValue
        {
            get { return _MaxValue; }
            set { _MaxValue = value; }
        }

        private Dictionary<string, int> _Values = new Dictionary<string,int>();
        public Dictionary<string, int> Values
        {
            get { return _Values; }
            set { _Values = value; }
        }

        private List<string> _YAxisLabels = new List<string>();
        public List<string> YAxisLabels
        {
            get { return _YAxisLabels; }
            set { _YAxisLabels = value; }
        }

        private string _LineColor = null;
        public string LineColor
        {
            get { return _LineColor; }
            set { _LineColor = value; }
        }


        string dataEncodingValues = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private string EncodeDataValues()
        {
            string retVal = "";
            foreach (string s in this.Values.Keys)
            {
                retVal += dataEncodingValues.Substring(((dataEncodingValues.Length - 1) * this.Values[s] / this.MaxValue), 1);
            }
            return retVal;
        }

        private string GetDataTitles()
        {
            string retVal = "|";
            foreach (string s in this.Values.Keys)
            {
                retVal += s + "|";
            }
            return retVal;
        }

        private string GetYAxisLabels()
        {
            string retVal = "";
            foreach (string s in this.YAxisLabels)
            {
                retVal += "|" + s;
            }
            return retVal;
        }

    }
}