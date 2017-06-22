using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using System.Collections.Specialized;
using System.Collections;

namespace Shared.WebCommon.UI.Web
{
	public class UIHelper
	{

		public static StringCollection ArrayToCollection(string[] array)
		{
			var sc = new StringCollection();

			foreach (var str in array)
			{
				if (!string.IsNullOrEmpty(str))
				{
					sc.Add(str);
				}
			}

			return sc;
		}

		public static void PopulateLabelsText(Dictionary<string, string> columns, SystemEntity systemEntityTypeId, int auditId, List<Label> labelslist)
		{
			PopulateLabelsText(columns, (int)systemEntityTypeId, auditId, labelslist);
		}

		public static void PopulateLabelsText(Dictionary<string, string> columns, int systemEntityTypeId, int auditId, List<Label> labelslist)
		{
			if (columns == null)
			{
				columns = GetLabelDictonaryObject(systemEntityTypeId, auditId);
			}

            foreach (var label in labelslist)
            {
                SetLabelText(columns, label);
            }
		}

		public static void LoadDropDown(IEnumerable source, DropDownList drpSource)
		{
			drpSource.SelectedIndex = -1;
			drpSource.SelectedValue = null;
			drpSource.ClearSelection();

			drpSource.DataSource = source;
			drpSource.DataTextField = StandardListDataModel.DataColumns.Name;
			drpSource.DataValueField = StandardListDataModel.DataColumns.Value;
			drpSource.DataBind();
		}

        public static void LoadDropDown(IEnumerable source, DropDownList drpSource, string textField, string valueField)
        {
            drpSource.SelectedIndex = -1;
            drpSource.SelectedValue = null;
            drpSource.ClearSelection();

            drpSource.DataSource = source;

            drpSource.DataTextField = textField;

            drpSource.DataValueField = valueField;

            drpSource.DataBind();
        }

        public static void LoadDropDown(IEnumerable source, ListBox drpSource, string textField, string valueField)
        {
            drpSource.SelectedIndex = -1;
            drpSource.SelectedValue = null;

            drpSource.ClearSelection();

            drpSource.DataSource = source;

            drpSource.DataTextField = textField;
            drpSource.DataValueField = valueField;

            drpSource.DataBind();
        }

        public static void LoadDropDown(System.Data.DataTable dt, DropDownList drpSource, string textField, string valueField)
        {
            drpSource.SelectedIndex = -1;
            drpSource.SelectedValue = null;
            drpSource.ClearSelection();

            drpSource.DataSource = dt;

            drpSource.DataTextField = textField;

            drpSource.DataValueField = valueField;

            drpSource.DataBind();
        }

		private static void SetLabelText(Dictionary<string, string> dictionary, Label label)
		{
			try
			{
				label.Text = dictionary[label.Text] + ": ";
			}
			catch (Exception exception)
			{
				//Framework.Components.DataAccess.Audit.LogError(exception.Message, exception);
			}
		}

		public static string[] GetGridViewColumns(int systemEntityTypeId, RequestProfile requestProfile)
		{
			var obj = new FieldConfigurationDataModel();
			
            obj.SystemEntityTypeId = systemEntityTypeId;

			var odt = FieldConfigurationDataManager.GetEntityDetails(obj, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
			
            var validColumns = new string[odt.Count];
			
            for (var i = 0; i < odt.Count; i++)
			{
				validColumns[i] = odt[i].Name;
			}
			
            return validColumns;
		}

        public static int GetDropDownSelectedIndex(DropDownList ddlObject, string key, string category)
        {
            return ddlObject.Items.IndexOf(ddlObject.Items.FindByText(PreferenceUtility.GetUserPreferenceByKey(key, category)));            
        }

		public static string[] GetDetailsViewColumns(int systemEntityTypeId, int auditId)
		{
			var obj = new FieldConfigurationDataModel();
			
            obj.SystemEntityTypeId = systemEntityTypeId;
			
            var odt = FieldConfigurationDataManager.GetDetailsViewColumns(obj, SessionVariables.RequestProfile);
			
            var validColumns = new string[odt.Rows.Count];
			
            for (var i = 0; i < odt.Rows.Count; i++)
			{
				validColumns[i] = odt.Rows[i][FieldConfigurationDataModel.DataColumns.Name].ToString();
			}
			
            return validColumns;
		}

		public static Dictionary<string, string> GetLabelDictonaryObject(SystemEntity systemEntityType, int auditId)
		{
			return GetLabelDictonaryObject((int)systemEntityType, auditId);
		}

		public static Dictionary<string, string> GetLabelDictonaryObject(int systemEntityTypeId, int auditId)
        {
            var validColumns = new Dictionary<string, string>();

            var obj = new FieldConfigurationDataModel();

            obj.SystemEntityTypeId = systemEntityTypeId;

            var odt = FieldConfigurationDataManager.GetDetailsViewColumns(obj, SessionVariables.RequestProfile);

            for (var i = 0; i < odt.Rows.Count; i++)
            {
                var key = Convert.ToString(odt.Rows[i][FieldConfigurationDataModel.DataColumns.Name]);
                var value = Convert.ToString(odt.Rows[i][FieldConfigurationDataModel.DataColumns.Value]);

                if (!validColumns.ContainsKey(key))
                {
                    validColumns.Add(key, value);
                }
            }

            return validColumns;
        }

        public static string RefineAndGetSearchText(string value, string category)
        {

            value = UPreference.GetWildCardSearchPrefix(category) + value + UPreference.GetWildCardSearchPostfix(category);
          
            return value;
        }

        public static List<string> GetUserLoginHistoryUrls()
        {
             new List<string>();

            var data = new Framework.Components.LogAndTrace.UserLoginHistoryDataModel();
            data.UserId = SessionVariables.RequestProfile.AuditId;
			var lst = Framework.Components.LogAndTrace.UserLoginHistoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);

            var lstURls = lst.OrderByDescending(x => x.DateVisited).Select(x1 => x1.URL).Distinct().Take(7).ToList();

            return lstURls;
        }

		public static string SplitByCapitalLetters(string strCheckURL)
		{
			var strURL = string.Empty;
            
			for (var i = 0; i < strCheckURL.Length; i++)
			{
				if (char.IsUpper(strCheckURL[i]))
					strURL += " ";

				strURL += strCheckURL[i].ToString();
			}

			return strURL;
		}

		public static string CreateRecentURL(string strLink)
		{
			var strURL = strLink;
			var strIndex1 = 0;
			var strIndex2 = 0;
			var lastIndex = strURL.LastIndexOf("/");

			if (lastIndex == 1)
			{
				strURL = "Home";
			}
			else
			{
				if (lastIndex != -1)
				{
					strURL = strURL.Substring(0, lastIndex);
					strIndex1 = strURL.LastIndexOf("/", lastIndex);
					strIndex2 = strURL.LastIndexOf("/", strIndex1);
				}

				if (strIndex2 != -1 && strIndex2 != 1)
				{
					strURL = strURL.Substring(strIndex2, strURL.Length - strIndex2);					
				}

				strURL = strURL.Replace("/", " - ");
				strURL = strURL.Remove(0, 3);
			}	

			return strURL;			
		}

        public static string AddMultipleDetailControls(PlaceHolder plcDetailsHolder, int systemEntityTypeId, 
            string superKey, string detailControlPath, Type type)
        {
            var DeleteIds = String.Empty;

            var data = new SuperKeyDetailDataModel();
            data.SuperKeyId = Convert.ToInt32(superKey);

            var page = HttpContext.Current.Handler as Page;

            // Change System Entity Type
            data.SystemEntityTypeId = systemEntityTypeId;
			var dt = Framework.Components.Core.SuperKeyDetailDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
            if (dt != null && dt.Count > 0)
            {
                foreach (var dr in dt)
                {
                    var key = dr.EntityKey.Value;
                    if (string.IsNullOrEmpty(DeleteIds))
                    {
                        DeleteIds = key.ToString();
                    }
                    else
                    {
                        DeleteIds += "," + key.ToString();
                    }

                    var detailsControl = page.LoadControl(detailControlPath);
                    //detailsControl.SetId = key;
                    //detailsControl.BorderClass = ApplicationCommon.DetailsBorderClassName;

                    plcDetailsHolder.Controls.Add(detailsControl);
                    plcDetailsHolder.Controls.Add(new LiteralControl("<br />"));

                    //chkVisible.Checked = detailsControl.IsHistoryVisible;
                }
            }
            return DeleteIds;
        }

	}
}