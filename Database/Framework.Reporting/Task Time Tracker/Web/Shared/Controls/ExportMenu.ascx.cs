using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Text;

namespace Shared.UI.Web.Controls
{
    public partial class ExportMenu : BaseControl
    {

        #region private variables

        private ListControl.GetDataDelegate _getData;
        private ListControl.GetColumnDelegate _getColumnDelegate;

        public string TableName
        {
            get
            {
                if (ViewState["TableName"] == null)
                {
                    ViewState["TableName"] = String.Empty;
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
                    ViewState["TableFolder"] = String.Empty;
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
                    ViewState["SearchCondition"] = String.Empty;
                }

                return Convert.ToString(ViewState["SearchCondition"]);
            }
            set
            {
                ViewState["SearchCondition"] = value;
            }
        }

        private string GridActionBarForegroundColor
        {
            get
            {
                if (ViewState["GridActionBarForegroundColor"] != null)
                {
                    return Convert.ToString(ViewState["GridActionBarForegroundColor"]);
                }
                return String.Empty;
            }
            set
            {
                ViewState["GridActionBarForegroundColor"] = value;
            }
        }

        private string GridActionBarFontFamily
        {
            get
            {
                if (ViewState["GridActionBarFontFamily"] != null)
                {
                    return Convert.ToString(ViewState["GridActionBarFontFamily"]);
                }
                return String.Empty;
            }
            set
            {
                ViewState["GridActionBarFontFamily"] = value;
            }
        }

        private string GridActionBarFontSize
        {
            get
            {
                if (ViewState["GridActionBarFontSize"] != null)
                {
                    return Convert.ToString(ViewState["GridActionBarFontSize"]);
                }
                return String.Empty;
            }
            set
            {
                ViewState["GridActionBarFontSize"] = value;
            }
        }

        #endregion

        #region private methods

        private void SetImagePaths()
        {
            var strUserTheme = PerferenceUtility.GetUserTheme();
            //imgExport.ImageUrl = strUserTheme + "/Images/export.jpg";
            //lnkExport.Style.Add("background-image", strUserTheme + "/Images/export_html.jpg");
            //lnkExportToCSV.Style.Add("background-image", strUserTheme + "/Images/export_csv.jpg");
            //lnkExportToXML.Style.Add("background-image", strUserTheme + "/Images/export_xml.jpg");
            //refreshimg.ImageUrl = strUserTheme + "/Images/Refresh.png";
            //refreshimg.ImageAlign = ImageAlign.Middle;
            //helpimg.ImageAlign = ImageAlign.Middle;
            //helpimg.ImageUrl = strUserTheme + "/Images/help.png";
            //imgSettings.Style.Add("background-image", strUserTheme + "/Images/settings.png");
            ////lnkimgExport.ImageUrl = ;
            //imgSettings2.ImageUrl = strUserTheme + "/Images/settings.png";
            //imgSettings2.ImageAlign = ImageAlign.Middle;
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

            var sb = new StringBuilder();
            foreach (var strColumn in strColumns)
            {
                //add separator
                sb.Append(strColumn + ',');
            }

            //append new line
            sb.Append("\r\n");
            foreach (DataRow dr in dt.Rows)
            {
                foreach (var strColumn in strColumns)
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

            var sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine("<" + strTableName.Replace(" ", "") + "s>");

            //append new line
            sb.Append("\r\n");
            foreach (DataRow dr in dt.Rows)
            {
                sb.AppendLine("<" + strTableName.Replace(" ", "") + ">");
                foreach (var strColumn in strColumns)
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

        public void Setup(string tableName, string tableFolder, ListControl.GetDataDelegate getDataDelegate, ListControl.GetColumnDelegate getColumnDelegate, string searchCondition)
        {
            TableName = tableName;
            TableFolder = tableFolder;
            SearchCondition = searchCondition;
            _getData = getDataDelegate;
            _getColumnDelegate = getColumnDelegate;

            CreateMenuItems();
        }

        private void CreateMenuItems()
        {
            var divider = "----------------------------------";

            var menuItem = new MenuItem("Export  " + "&#9660;");
            //menuItem.ImageUrl = "~/Content/images/downarrow.jpg";
            var menuItem1 = new MenuItem("HTML");
            var menuItem2 = new MenuItem("XLS");
            var menuItem3 = new MenuItem("XML");
            var menuItem4 = new MenuItem("Excel (All fields)");
            var menuItem5 = new MenuItem("Excel (Current fields)");
            var menuItem6 = new MenuItem("Charts");
            var divider1 = new MenuItem(divider);
            var divider2 = new MenuItem(divider);
            var divider3 = new MenuItem(divider);
            divider1.Enabled = false;
            divider2.Enabled = false;
            divider3.Enabled = false;

            menuItem.ChildItems.Add(menuItem1);
            menuItem.ChildItems.Add(divider1);
            menuItem.ChildItems.Add(menuItem2);
            menuItem.ChildItems.Add(menuItem3);
            menuItem.ChildItems.Add(divider2);
            menuItem.ChildItems.Add(menuItem4);
            menuItem.ChildItems.Add(menuItem5);
            menuItem.ChildItems.Add(divider3);
            menuItem.ChildItems.Add(menuItem6);

            if(ExportParentMenu.Items.Count == 0)
                ExportParentMenu.Items.Add(menuItem);
            ExportParentMenu.CssClass = "newClass1";
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //    if (!string.IsNullOrEmpty(this.TableFolder))
                //    {
                //        lnkExport.NavigateUrl = "~/" + this.TableFolder + "/" + this.TableName + "/Export.aspx?SearchCondition=" + SearchCondition;
                //    }
                //    else
                //    {
                //        lnkExport.NavigateUrl = "~/" + this.TableName + "/Export.aspx?SearchCondition=" + SearchCondition;
                //    }
                GridActionBarFontFamily = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.GridActionBarFontFamily);
                GridActionBarForegroundColor = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.GridActionBarForegroundColor);
                GridActionBarFontSize = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.GridActionBarFontSize);

				//RLink.Style.Add("font-family", GridActionBarFontFamily);
				//RLink.Style.Add("color", GridActionBarForegroundColor);
				//RLink.Style.Add("font-size", GridActionBarFontSize);

				//imgSettings.Style.Add("font-family", GridActionBarFontFamily);
				//imgSettings.Style.Add("color", GridActionBarForegroundColor);
				//imgSettings.Style.Add("font-size", GridActionBarFontSize);

                imgSettings.PostBackUrl = "~/ListSettings/" + TableName;
                SetImagePaths();
				
            }


						
        }

		public void ExportParentMenu_MenuItemClick(Object sender, MenuEventArgs e)
		{
			if (e.Item.Text == "HTML")
			{
				if (!string.IsNullOrEmpty(TableFolder))
				{
					Response.Redirect("~/" + TableFolder + "/" + TableName + "/Export.aspx?SearchCondition=" + SearchCondition);
				}
				else
				{
					Response.Redirect("~/" + TableName + "/Export.aspx?SearchCondition=" + SearchCondition);
				}
			}
			if (e.Item.Text == "XLS")
				ExportToCSV(_getData(), _getColumnDelegate(), TableName + "s");
			if (e.Item.Text == "XML")
				ExportToXML(_getData(), _getColumnDelegate(), TableName);
		}

        //protected void lnkExportToCSV_Click(object sender, EventArgs e)
        //{
        //    ExportToCSV(_getData(), _getColumnDelegate(), TableName + "s");
        //}

        //protected void lnkExportToXML_Click(object sender, EventArgs e)
        //{
        //    ExportToXML(_getData(), _getColumnDelegate(), TableName);
        //}

        //protected void lnkHelp_Click(object sender, EventArgs e)
        //{
        //    var helpPageRelativePath = "~/Shared/ApplicationManagement/HelpPage/";

        //    var data = new HelpPage();
        //    data.SystemEntityTypeId = ApplicationCommon.GetSystemEntityTypeId(TableName);
        //    data.HelpPageContextId = ApplicationCommon.GetHelpPageContextId("Default");

        //    var dt = Framework.Components.Core.HelpPage.Search(data, SessionVariables.RequestProfile.AuditId);

        //    if (dt.Rows.Count > 0)
        //    {
        //        var helpPageId = Convert.ToInt32(dt.Rows[0][HelpPage.DataColumns.HelpPageId]);
        //        Response.Redirect(helpPageRelativePath + "Details.aspx?SetId=" + helpPageId, false);
        //    }
        //    else
        //    {
        //        data.Name = "Default";
        //        data.Content = "Help Page Content";
        //        data.SortOrder = 1;
        //        var helpPageId = Framework.Components.Core.HelpPage.Create(data, SessionVariables.RequestProfile.AuditId);
        //        Response.Redirect(helpPageRelativePath + "Update.aspx?SetId=" + helpPageId, false);
        //    }
        //}

        //protected void lnkColumnChooser_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/Shared/Admin/ColumnChooser.aspx?entity=" + TableName, true);
        //}

		//protected void drpExportOption_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//    if (drpExportOption.SelectedItem.Text == "Export to HTML")
		//    {
		//        if (!string.IsNullOrEmpty(this.TableFolder))
		//        {
		//            Response.Redirect("~/" + this.TableFolder + "/" + this.TableName + "/Export.aspx?SearchCondition=" + SearchCondition);
		//        }
		//        else
		//        {
		//            Response.Redirect("~/" + this.TableName + "/Export.aspx?SearchCondition=" + SearchCondition);
		//        }
		//    }
		//    if (drpExportOption.SelectedItem.Text == "Export to XLS")
		//        ExportToCSV(_getData(), _getColumnDelegate(), TableName + "s");
		//    if (drpExportOption.SelectedItem.Text == "Export to XML")
		//        ExportToXML(_getData(), _getColumnDelegate(), TableName);

		//}

        #endregion

    }

}