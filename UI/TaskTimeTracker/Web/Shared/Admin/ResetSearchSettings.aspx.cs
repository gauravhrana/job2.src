using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Framework.Components.Core;
using Framework.Components.UserPreference;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using System.Text;

namespace Shared.UI.Web.Admin
{
	public partial class ResetSearchSettings : Framework.UI.Web.BaseClasses.PageBasePage
	{
		#region Events

		protected override void OnInit(EventArgs e)
		{
			SettingCategory = "ResetSearchSettingsDefaultView";			
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			var bcControl = Master.BreadCrumbObject;
			bcControl.SettingCategory = SettingCategory + "BreadCrumbControl";
			bcControl.Setup(string.Empty);
			bcControl.GenerateMenu();			
		}

		protected void ddlApplication_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Remove Application User dropdownlist items 
			drpApplicationUser.Items.Clear();
			drpSystemEntity.Items.Clear();

			string strApplication = string.Empty;
			strApplication = ddlApplication.SelectedValue;			

			if (ddlApplication.SelectedIndex != 0)
			{
				var appUserdata = new DataModel.Framework.AuthenticationAndAuthorization.ApplicationUserDataModel();
				appUserdata.ApplicationId = Convert.ToInt32(strApplication);
				var auData = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetDetails(appUserdata, SessionVariables.RequestProfile);
				drpApplicationUser.DataSource = auData;
				drpApplicationUser.DataValueField = "ApplicationUserId";
				drpApplicationUser.DataTextField = "FullName";
				drpApplicationUser.DataBind();

				drpApplicationUser.Items.Insert(0, new ListItem("All", "-1"));

				var data = new SystemEntityTypeDataModel();
				data.ApplicationId = Convert.ToInt32(strApplication);
				var dt = SystemEntityTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
                dt = dt.OrderBy(x => x.EntityDescription).ToList();

                //var upcdata = new UserPreferenceCategoryDataModel();
                //upcdata.ApplicationId = Convert.ToInt32(strApplication);
                //var dt2 = UserPreferenceCategoryDataManager.Search(upcdata, SessionVariables.RequestProfile);

                //dt2.DefaultView.RowFilter = "Name LIKE '%DefaultViewSearchControl'";
                //var dv = dt2.DefaultView;
                //dv.Sort = "Name ASC";

				UIHelper.LoadDropDown(dt,
					drpSystemEntity, SystemEntityTypeDataModel.DataColumns.EntityName,
					SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);
				drpSystemEntity.Items.Insert(0, new ListItem("All", "-1"));
				DataBind("All");
			}
		
			
		}

        protected void Page_Load(object sender, EventArgs e)
        {
			
            if (!IsPostBack)
            {
				var appData = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(appData, ddlApplication, DataModel.Framework.AuthenticationAndAuthorization.ApplicationDataModel.DataColumns.Name,
					DataModel.Framework.AuthenticationAndAuthorization.ApplicationDataModel.DataColumns.ApplicationId);

				ddlApplication.Items.Insert(0, new ListItem("All", "-1"));
				
				var auData = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(auData, drpApplicationUser, DataModel.Framework.AuthenticationAndAuthorization.ApplicationUserDataModel.DataColumns.FullName,
				DataModel.Framework.AuthenticationAndAuthorization.ApplicationUserDataModel.DataColumns.ApplicationUserId);
				
				drpApplicationUser.Items.Insert(0, new ListItem("All", "-1"));

				var data = new SystemEntityTypeDataModel();
				data.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
				var dt = SystemEntityTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
                dt = dt.OrderBy(x => x.EntityName).ToList();

                //var upcdata = new UserPreferenceCategoryDataModel();
                //upcdata.ApplicationId = SessionVariables.RequestProfile.ApplicationId;				
                //var dt2 = UserPreferenceCategoryDataManager.Search(upcdata, SessionVariables.RequestProfile);

                //dt2.DefaultView.RowFilter = "Name LIKE '%DefaultViewSearchControl'";
                //var dv = dt2.DefaultView;
                //dv.Sort = "Name ASC";

				UIHelper.LoadDropDown(dt,
					drpSystemEntity, SystemEntityTypeDataModel.DataColumns.EntityName,
					SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);
				drpSystemEntity.Items.Insert(0, new ListItem("All", "-1"));
				DataBind("All");
				//UIHelper.LoadDropDown(dt2,
				//	drpSearchUserCategory, UserPreferenceCategoryDataModel.DataColumns.Name,
				//	UserPreferenceCategoryDataModel.DataColumns.UserPreferenceCategoryId);

				//bool isvalidcategory = false;

				//dv.Table.Columns.Add("EntityName", typeof(System.String));

				//for (int j = 0; j < dv.Table.Rows.Count; j++)
				//{
				//	var name = dv.Table.Rows[j]["Name"];
				//	var entityname = name.ToString().Replace("DefaultViewSearchControl", " Search Control");
				//	dv.Table.Rows[j]["EntityName"] = entityname;
				//}

				//for (int i = 0; i < dt.Rows.Count; i++)
				//{
				//	var ename = dt.Rows[i]["EntityName"].ToString();
				//	var category = ename + "DefaultViewSearchControl";
				//	for (int j = 0; j < dv.Table.Rows.Count; j++)
				//	{
				//		if (dv.Table.Rows[j]["Name"].ToString().Equals(category))
				//		{
				//			isvalidcategory = true;
				//			break;
				//		}
				//	}
				//	if (!isvalidcategory)
				//	{
				//		dt.Rows.RemoveAt(i);
				//		dt.AcceptChanges();
				//	}
				//}

				//SearchSettings.DataSource = dv;
				//SearchSettings.DataBind();
            }
        }

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			var usrCategory = drpSystemEntity.SelectedItem.ToString();
			DataBind(usrCategory);
		}

		#endregion

		#region Methods

		private void DataBind(string systemEntity)
		{
			lblParamCaption.Visible = false;
			lblParams.Visible = false;
			lblParamValues.Visible = false;
			lblParamValuesList.Visible = false;
			var data = new SystemEntityTypeDataModel();
			data.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
			var dt = SystemEntityTypeDataManager.Search(data, SessionVariables.RequestProfile);

			var upcdata = new UserPreferenceCategoryDataModel();
			if (!ddlApplication.SelectedValue.Equals("-1"))
				upcdata.ApplicationId = int.Parse(ddlApplication.SelectedValue);
			else
				upcdata.ApplicationId = SessionVariables.RequestProfile.ApplicationId;

			upcdata.Name = "%SearchControl";
			if (systemEntity != "All")
			{
				var category = systemEntity + "DefaultViewSearchControl";
				upcdata.Name = category;

			}
			var dt2 = UserPreferenceCategoryDataManager.Search(upcdata, SessionVariables.RequestProfile);

			dt2.DefaultView.RowFilter = "Name LIKE '%DefaultViewSearchControl'";
			var dv = dt2.DefaultView;
			dv.Sort = "Name ASC";

			bool isvalidcategory = false;

			dv.Table.Columns.Add("EntityName", typeof(System.String));

			for (int j = 0; j < dv.Table.Rows.Count; j++)
			{
				var name = dv.Table.Rows[j]["Name"];
				var entityname = name.ToString().Replace("DefaultViewSearchControl", " Search Control");
				dv.Table.Rows[j]["EntityName"] = entityname;
			}

			for (int i = 0; i < dt.Rows.Count; i++)
			{
				var ename = dt.Rows[i]["EntityName"].ToString();
				var category = ename + "DefaultViewSearchControl";
				for (int j = 0; j < dv.Table.Rows.Count; j++)
				{
					if (dv.Table.Rows[j]["Name"].ToString().Equals(category))
					{
						isvalidcategory = true;
						break;
					}
				}
				if (!isvalidcategory)
				{
					dt.Rows.RemoveAt(i);
					dt.AcceptChanges();
				}
			}
			CheckBoxList1.Visible = true;
			CheckBoxList1.DataSource = dv;
			CheckBoxList1.DataTextField = "EntityName";
			CheckBoxList1.DataValueField = "Name";
			CheckBoxList1.DataBind();
		}

		#endregion

		protected void lnkReset_Click(object sender, EventArgs e)
		{
			CheckBoxList1.Visible = false;
			string Message = string.Empty;
			StringBuilder upParamList=new StringBuilder();
			StringBuilder upParamValues = new StringBuilder();
			var applicationuserId = int.Parse(drpApplicationUser.SelectedValue);

			var category = string.Empty;

			var selectedItems = CheckBoxList1.Items.Cast<ListItem>().Where(x => x.Selected).Select(li => li.Value).ToList();

			for (int i = 0; i < selectedItems.Count; i++)
			{
					category = selectedItems[i];
					var upcdata = new UserPreferenceCategoryDataModel();
					upcdata.Name = category;
					var dt = UserPreferenceCategoryDataManager.Search(upcdata, SessionVariables.RequestProfile);
					var upcid = 0;
					var upkeyid = 0;
					var upkdata = new UserPreferenceKeyDataModel();
					var upkdt = new DataTable();

					if (dt.Rows.Count == 1)
					{
						upcid = int.Parse(dt.Rows[0][UserPreferenceCategoryDataModel.DataColumns.UserPreferenceCategoryId].ToString());
						var updata = new UserPreferenceDataModel();
						updata.UserPreferenceCategoryId = upcid;
						updata.ApplicationUserId = SessionVariables.RequestProfile.AuditId;
						//updata.ApplicationUserId = applicationuserId;
						var updt = UserPreferenceDataManager.Search(updata, SessionVariables.RequestProfile);

						for (int j = 0; j < updt.Rows.Count; j++)
						{
							upkeyid = int.Parse(updt.Rows[j][UserPreferenceDataModel.DataColumns.UserPreferenceKeyId].ToString());
							upkdata.UserPreferenceKeyId = upkeyid;
							upkdt = UserPreferenceKeyDataManager.Search(upkdata, SessionVariables.RequestProfile);

							var upKey = upkdt.Rows[0][StandardDataModel.StandardDataColumns.Name].ToString();
							var upValue = upkdt.Rows[0][UserPreferenceKeyDataModel.DataColumns.Value].ToString();
							upParamList.Append(upKey + "<br/>");
							upParamValues.Append(upValue + "<br/>");
							PreferenceUtility.UpdateUserPreference(category, upKey, upValue);

						}
					}
					lblParamCaption.Visible = true;
					lblParams.Visible = true;
					lblParams.Text = upParamList.ToString();

					lblParamValues.Visible = true;
					lblParamValuesList.Visible = true;
					lblParamValuesList.Text = upParamValues.ToString();
			}
			
		}

        protected void lnkResetAll_Click(object sender, EventArgs e)
        {
            var applicationuserId = int.Parse( drpApplicationUser.SelectedValue);

            var upcdata = new UserPreferenceCategoryDataModel();
            upcdata.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
            var dt2 = UserPreferenceCategoryDataManager.Search(upcdata, SessionVariables.RequestProfile);
            dt2.DefaultView.RowFilter = "Name LIKE '%SearchControl'";
            var filtereddt = dt2.DefaultView.ToTable();

            for (int i = 0; i < filtereddt.Rows.Count; i++)
            {
                var category = filtereddt.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();

                if (category.EndsWith("SearchControl"))
                {
                    upcdata = new UserPreferenceCategoryDataModel();
                    upcdata.Name = category;
                    var dt = UserPreferenceCategoryDataManager.Search(upcdata, SessionVariables.RequestProfile);
                    var upcid = 0;
                    var upkeyid = 0;
                    var upkdata = new UserPreferenceKeyDataModel();
                    var upkdt = new DataTable();

                    if (dt.Rows.Count == 1)
                    {
                        upcid = int.Parse(dt.Rows[0][UserPreferenceCategoryDataModel.DataColumns.UserPreferenceCategoryId].ToString());
                        var updata = new UserPreferenceDataModel();
                        updata.UserPreferenceCategoryId = upcid;
                        updata.ApplicationUserId = applicationuserId;
						
						var updt = UserPreferenceDataManager.Search(updata, SessionVariables.RequestProfile);

                        for (int j = 0; j < updt.Rows.Count; j++)
                        {
                            upkeyid = int.Parse(updt.Rows[j][UserPreferenceDataModel.DataColumns.UserPreferenceKeyId].ToString());
                            upkdata.UserPreferenceKeyId = upkeyid;
							upkdt = UserPreferenceKeyDataManager.Search(upkdata, SessionVariables.RequestProfile);

                            updata.UserPreferenceId = int.Parse(updt.Rows[j][UserPreferenceDataModel.DataColumns.UserPreferenceId].ToString());
                            updata.Value = upkdt.Rows[0][UserPreferenceKeyDataModel.DataColumns.Value].ToString();
                            updata.ApplicationUserId = applicationuserId;
                            updata.UserPreferenceKeyId = upkeyid;
                            updata.UserPreferenceCategoryId = upcid;
                            updata.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
                            updata.DataTypeId = int.Parse(updt.Rows[j][UserPreferenceDataModel.DataColumns.DataTypeId].ToString());
                            //UserPreferenceDataManager.Update(updata, SessionVariables.RequestProfile);
                        }
                    }
                }
            }

        }

        protected void SearchSettings_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("click"))
            {
                var category = e.CommandArgument.ToString();

                var upcdata = new UserPreferenceCategoryDataModel();
                upcdata.Name = category;
                var dt = UserPreferenceCategoryDataManager.Search(upcdata, SessionVariables.RequestProfile);
                var upcid = 0;
                var upkeyid = 0;
                var upkdata = new UserPreferenceKeyDataModel();
                var upkdt = new DataTable();

                if (dt.Rows.Count == 1)
                {
					upcid = int.Parse (dt.Rows[0][UserPreferenceCategoryDataModel.DataColumns.UserPreferenceCategoryId].ToString());
                    var updata = new UserPreferenceDataModel();
                    updata.UserPreferenceCategoryId = upcid;
					updata.ApplicationUserId = SessionVariables.RequestProfile.AuditId;
                    var updt = UserPreferenceDataManager.Search(updata, SessionVariables.RequestProfile);

                    for (int i = 0; i < updt.Rows.Count; i++)
					{
                        upkeyid                     = int.Parse(updt.Rows[i][UserPreferenceDataModel.DataColumns.UserPreferenceKeyId].ToString());
                        upkdata.UserPreferenceKeyId = upkeyid;
                        upkdt                       = UserPreferenceKeyDataManager.Search(upkdata, SessionVariables.RequestProfile);

						var upKey                   = upkdt.Rows[0][StandardDataModel.StandardDataColumns.Name].ToString();
                        var upValue                 = upkdt.Rows[0][UserPreferenceKeyDataModel.DataColumns.Value].ToString();

						PreferenceUtility.UpdateUserPreference(category, upKey, upValue);

                    }
                }

            }
        }		
		
	}
}