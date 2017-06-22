using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using System.Globalization;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Admin
{
	public partial class AdminTasks : Framework.UI.Web.BaseClasses.PageBasePage
    {
         
        #region Methods

        private void LoadExpiredSuperKeys(int ApplicationId)
        {
            var selectedDate = DateTime.ParseExact(txtDate.Text.Trim(), SessionVariables.UserDateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
        	var systementitytypedt = GetApplicableSystemEntities(ApplicationId);
            var data = new SuperKeyDataModel();
            data.ExpirationDate = DateTime.Parse(selectedDate.ToString());

			var dt = Framework.Components.Core.SuperKeyDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
			var dtfinal = new List<SuperKeyDataModel>();
			var IsRequired = false;
            for (var i = 0; i < dt.Count; i++)
			{
                var systementitytypeId = dt[i].SystemEntityTypeId.Value;
                for (int j = 0; j < systementitytypedt.Rows.Count; j++)
				{

					if (systementitytypeId.ToString().Equals
                        (systementitytypedt.Rows[j][SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId].ToString()))
					{
						IsRequired = true;
					}
				}
				if (IsRequired)
				{
					dtfinal.Add(dt[i]);
					IsRequired = false;
				}

			}
            dgvSuperKey.DataSource = dtfinal;
            dgvSuperKey.DataBind();
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDate.Text = DateTime.Now.AddDays(-1).ToString(SessionVariables.UserDateFormat);
				var dt2 = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.RequestProfile);
				ddlApplication.DataSource = dt2;
				ddlApplication.DataTextField = "Name";
				ddlApplication.DataValueField = "ApplicationId";
				ddlApplication.DataBind();
				ddlApplication.SelectedValue = SessionVariables.RequestProfile.ApplicationId.ToString();
				LoadExpiredSuperKeys(SessionVariables.RequestProfile.ApplicationId);

				if (System.Diagnostics.Debugger.IsAttached)
				{
					ddlApplication.Visible = true;
					lblApplicationId.Visible = true;
				}
            }
            //CalendarExtenderDate.Format = SessionVariables.UserDateFormat;
			lblUserDateTimeFormat.Text = "( " + SessionVariables.UserDateFormat +")";
        }

		protected override void OnInit(EventArgs e)
		{
			SettingCategory = "AdminTasksDefaultView";

			var bcControl = Master.BreadCrumbObject;
			bcControl.SettingCategory = SettingCategory + "BreadCrumbControl";
			bcControl.Setup(string.Empty);
			bcControl.GenerateMenu();
			//BreadCrumbObject = Master.BreadCrumbObject;
		}

		protected void ddlApplication_SelectedIndexChanged(object sender, EventArgs e)
		{
			var applicationId = Convert.ToInt32(ddlApplication.SelectedValue);
			LoadExpiredSuperKeys(applicationId);

		} 

		public DataTable GetApplicableSystemEntities(int ApplicationId)
		{
			var dt3 = Framework.Components.Core.SystemEntityTypeDataManager.Search(SystemEntityTypeDataModel.Empty, SessionVariables.RequestProfile);
			var entitiesdt = dt3.Clone();

            var dt = FieldConfigurationUtility.GetFieldConfigurations(null, null, string.Empty);
			
            var IsRequired = false;
			for (var i = 0; i < dt3.Rows.Count; i++)
			{
				var systementitytypeId = int.Parse(dt3.Rows[i][SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId].ToString());
				for (int j = 0; j < dt.Rows.Count; j++)
				{

					if (systementitytypeId.ToString().Equals
						(dt.Rows[j][FieldConfigurationDataModel.DataColumns.SystemEntityTypeId].ToString()))
					{
						IsRequired = true;
					}
				}
				if (IsRequired)
				{
					entitiesdt.ImportRow(dt3.Rows[i]);
					IsRequired = false;
				}

			}

			return entitiesdt;
		}

        protected void btnDeleteSuperKey_Click(object sender, EventArgs e)
        {
            var data = new SuperKeyDataModel();
            var selectedDate = DateTime.ParseExact(txtDate.Text.Trim(), SessionVariables.UserDateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
            data.ExpirationDate = DateTime.Parse(selectedDate.ToString());
			Framework.Components.Core.SuperKeyDataManager.DeleteExpired(data, SessionVariables.RequestProfile);
            lblMessage.Text = "Expired SuperKey Records deleted.";

            LoadExpiredSuperKeys(int.Parse(SessionVariables.RequestProfile.ApplicationId.ToString()));
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
			LoadExpiredSuperKeys(int.Parse(SessionVariables.RequestProfile.ApplicationId.ToString()));
        }

        protected void drpCalendarDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDate.Text = DateTime.Now.AddDays((Convert.ToInt32(drpCalendarDate.SelectedValue) + 1) * -1).ToString(SessionVariables.UserDateFormat);
        }

		

        #endregion

        public IFormatProvider DateFormatInfo { get; set; }

		public string ConvertDateTimeFormat
		{
			get
			{
				return DateTimeHelper.CovertDateFormatToJavascript();
			}
		}
    }
}