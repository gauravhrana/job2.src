using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Text;

namespace Shared.UI.Web.Controls
{
    public partial class ExportMenu : System.Web.UI.UserControl
    {

        #region private variables

        public delegate DataTable GetDataDelegate();
        public delegate string[] GetColumnDelegate();

        private GetDataDelegate _getData;
        private GetColumnDelegate _getColumnDelegate;

        public string TableName
        {
            get
            {
                if (ViewState["TableName"] == null)
                {
                    ViewState["TableName"] = string.Empty;
                }

                return Convert.ToString(ViewState["TableName"]);
            }
            set
            {
                ViewState["TableName"] = value;
            }
        }

        public string TableFolder
        {
            get
            {
                if (ViewState["TableFolder"] == null)
                {
                    ViewState["TableFolder"] = string.Empty;
                }

                return Convert.ToString(ViewState["TableFolder"]);
            }
            set
            {
                ViewState["TableFolder"] = value;
            }
        }

        public string SearchCondition
        {
            get
            {
                if (ViewState["SearchCondition"] == null)
                {
                    ViewState["SearchCondition"] = string.Empty;
                }

                return Convert.ToString(ViewState["SearchCondition"]);
            }
            set
            {
                ViewState["SearchCondition"] = value;
            }
        }

        #endregion

        #region private methods

        private void SetImagePaths()
        {
            string strUserTheme = ApplicationCommon.GetUserTheme();
            //imgExport.ImageUrl = strUserTheme + "/Images/export.jpg";
			lnkExport.Style.Add("background-image", strUserTheme + "/Images/export_html.jpg");
			lnkExportToCSV.Style.Add("background-image", strUserTheme + "/Images/export_csv.jpg");
			lnkExportToXML.Style.Add("background-image", strUserTheme + "/Images/export_xml.jpg");
			refreshimg.ImageUrl = strUserTheme + "/Images/Refresh.png";
        	refreshimg.ImageAlign = ImageAlign.Middle;
        	helpimg.ImageAlign = ImageAlign.Middle;
        	helpimg.ImageUrl = strUserTheme + "/Images/help.png";
			imgSettings.Style.Add("background-image", strUserTheme + "/Images/settings.png");
        	//lnkimgExport.ImageUrl = ;
			imgSettings2.ImageUrl = strUserTheme + "/Images/settings.png";
        	imgSettings2.ImageAlign = ImageAlign.Middle;
        	//imgXML.ImageUrl = strUserTheme + "/Images/export_xml.jpg";
        }

        private void ExportToCSV(DataTable dt, string[] strColumns, string strTableName)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
             "attachment;filename=" + strTableName + ".csv");
            Response.Charset = "";
            Response.ContentType = "application/text";

            StringBuilder sb = new StringBuilder();
            foreach (string strColumn in strColumns)
            {
                //add separator
                sb.Append(strColumn + ',');
            }

            //append new line
            sb.Append("\r\n");
            foreach (DataRow dr in dt.Rows)
            {
                foreach (string strColumn in strColumns)
                    sb.Append(Convert.ToString(dr[strColumn]) + ',');
                sb.Append("\r\n");
            }

            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }

        private void ExportToXML(DataTable dt, string[] strColumns, string strTableName)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
             "attachment;filename=" + strTableName + ".xml");
            Response.Charset = "";
            Response.ContentType = "application/download";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine("<" + strTableName.Replace(" ", "") + "s>");

            //append new line
            sb.Append("\r\n");
            foreach (DataRow dr in dt.Rows)
            {
                sb.AppendLine("<" + strTableName.Replace(" ", "") + ">");
                foreach (string strColumn in strColumns)
                {
                    sb.AppendLine("<" + strColumn + ">" + Convert.ToString(dr[strColumn]) + "</" + strColumn + ">");
                }
                sb.AppendLine("</" + strTableName.Replace(" ", "") + ">");
                sb.Append("\r\n");
            }
            sb.AppendLine("</" + strTableName.Replace(" ", "") + "s>");
            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }

        #endregion

        #region public methods

        public void Setup(string tableName, string tableFolder, GetDataDelegate getDataDelegate, GetColumnDelegate getColumnDelegate, string searchCondition)
        {
            TableName = tableName;
            TableFolder = tableFolder;
            SearchCondition = searchCondition;
            _getData = getDataDelegate;
            _getColumnDelegate = getColumnDelegate;
        }

        #endregion

        #region Events


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.TableFolder))
                {
                    lnkExport.NavigateUrl = "~/" + this.TableFolder + "/" + this.TableName + "/Export.aspx?SearchCondition=" + SearchCondition;
                }
                else
                {
                    lnkExport.NavigateUrl = "~/" + this.TableName + "/Export.aspx?SearchCondition=" + SearchCondition;
                }
				imgSettings.PostBackUrl = "~/Settings.aspx?EN=" + this.TableName + "&EF=" + this.TableFolder;
                SetImagePaths();
				
            }
        }

        protected void lnkExportToCSV_Click(object sender, EventArgs e)
        {
            ExportToCSV(_getData(), _getColumnDelegate(), this.TableName + "s");
        }

        protected void lnkExportToXML_Click(object sender, EventArgs e)
        {
            ExportToXML(_getData(), _getColumnDelegate(), this.TableName);
        }

        #endregion
    }
}