using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using DataModel.Framework.ReleaseLog;

namespace Shared.UI.Web.ApplicationManagement.ReleaseIssueType
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {
		#region Methods

		protected override Control GetTabControl(int setId, Control detailsControl)
		{
			var tabControl = ApplicationCommon.GetNewDetailTabControl();

			tabControl.Setup("ReleaseIssueUpdateView");
			tabControl.ID = "ReleaseIssueUpdateView";

			var selected = false;

			if (string.IsNullOrEmpty(Request.QueryString["tab"]) || Request.QueryString["tab"] == "1")
			{
				selected = true;
			}

			tabControl.AddTab("Release Issue Type", detailsControl, String.Empty, true);

			// not making sense ?
			selected = false;

			if (Request.QueryString["tab"] == "2")
			{
				selected = true;
			}

			var listControl2 = (Shared.UI.Web.Controls.GroupList)Page.LoadControl(ApplicationCommon.GroupListControl);
			SetId = setId;
			var plcControlHolder = new PlaceHolder();
			plcControlHolder.ID = "plcControlHolder";
			//var dropDown = new DropDownList();
			//dropDown.SelectedIndexChanged += dropdownlist_SelectedIndexChanged;
			//dropDown.Width = 300;
			//dropDown.ID = "dropdownlist";
			//plcControlHolder.Controls.Add(dropDown);

			// Add Direction Combos Dynamically, check flag whether the corresponding record exists in FC or not.
			plcControlHolder.Controls.Add(new Literal() { Text = "<br />" });
			plcControlHolder.Controls.Add(new Label() { Text = " &nbsp;&nbsp; &nbsp;&nbsp; Group By: &nbsp;&nbsp;" });

			var dropDownOrderBy = new DropDownList();
			dropDownOrderBy.ID = "dropdownlistOrderBy1";
			dropDownOrderBy.Items.Add("ReleaseLog");			
			dropDownOrderBy.Items.Add("PrimaryDeveloper");
			dropDownOrderBy.Items.Add("ReleaseIssueType");
			dropDownOrderBy.Items.Add("ReleasePublishCategory");
			dropDownOrderBy.Items.Add("PrimaryEntity");
			dropDownOrderBy.Items.Add("Feature");
			dropDownOrderBy.Items.Add("Module");
			dropDownOrderBy.Items.Add("JIRA");
			dropDownOrderBy.SelectedValue = "ReleaseIssueType";
			plcControlHolder.Controls.Add(dropDownOrderBy);
			plcControlHolder.Controls.Add(new Literal() { Text = "<br /><br />" });

			plcControlHolder.Controls.Add(new Label() { Text = "Sub Group By: &nbsp;&nbsp;" });
			dropDownOrderBy = new DropDownList();
			dropDownOrderBy.ID = "dropdownlistOrderBy2";			
			dropDownOrderBy.Items.Add("ReleaseLog");			
			dropDownOrderBy.Items.Add("PrimaryDeveloper");
			dropDownOrderBy.Items.Add("ReleaseIssueType");
			dropDownOrderBy.Items.Add("ReleasePublishCategory");
			dropDownOrderBy.Items.Add("PrimaryEntity");
			dropDownOrderBy.Items.Add("Feature");
			dropDownOrderBy.Items.Add("Module");
			dropDownOrderBy.Items.Add("JIRA");
			dropDownOrderBy.SelectedValue = "ReleaseLog";
			plcControlHolder.Controls.Add(dropDownOrderBy);
			plcControlHolder.Controls.Add(new Literal() { Text = "<br /><br />" });

			var btnGroup = new Button();
			btnGroup.ID = "btnGroup";
			btnGroup.Text = "Group";
			btnGroup.Click += btnGroup_Click;
			plcControlHolder.Controls.Add(btnGroup);
			plcControlHolder.Controls.Add(new Literal() { Text = "<br /><br />" });

			listControl2.SettingCategory = "ReleaseLogDetailUpdateTabControl";
			listControl2.ID = "GroupListControl";
			listControl2.Setup("ReleaseIssueType", "ASC", "ReleaseLog", "ASC", "ReleaseLogDetail", string.Empty, "ReleaseLogDetailId", true, GetData, GetReleaseLogDetailColumns, "ReleaseLogDetail", String.Empty, null, true);
			
			plcControlHolder.Controls.Add(listControl2);
			
			tabControl.AddTab("Release Log Detail", plcControlHolder, String.Empty, selected);			

			return tabControl;
		}

		protected void btnGroup_Click(object sender, EventArgs e)
		{

			var tabcontrol = (Shared.UI.Web.Controls.DetailTabControl)plcDetailsList.FindControl("ReleaseIssueUpdateView");
			var plcControlHolder = (PlaceHolder)tabcontrol.FindControl("plcControlHolder");
			var groupListControl = (Shared.UI.Web.Controls.GroupList)tabcontrol.FindControl("GroupListControl");
			var ddlgroupBy = (DropDownList)plcControlHolder.FindControl("dropdownlistOrderBy1");
			var ddlSubgroupBy = (DropDownList)plcControlHolder.FindControl("dropdownlistOrderBy2");
			groupListControl.Setup(ddlgroupBy.SelectedValue, "ASC", ddlSubgroupBy.SelectedValue, "ASC", "ReleaseLogDetail", string.Empty, "ReleaseLogDetailId", true, GetData, GetReleaseLogDetailColumns, "ReleaseLogDetail", String.Empty, null, true);
			
		}

		private DataTable GetData()
		{
			return GetReleaseLogDetailData(SetId);
		}
		
		private DataTable GetReleaseLogDetailData(int releaseIssueTypeId)
		{
			//var data = new ReleaseLogDetailDataModel();
			//data.ReleaseIssueTypeId = releaseIssueTypeId;
			//var dt = Framework.Components.ReleaseLog.ReleaseLogDetail.GetByReleaseLogIssueType(releaseIssueTypeId, AuditId);
			//return dt;

			var dt = Framework.Components.ReleaseLog.ReleaseLogDetailDataManager.GetByReleaseLogIssueType(releaseIssueTypeId, AuditId);
			var releaseLogDetaildt = Framework.Components.ReleaseLog.ReleaseLogDetailDataManager.GetList(AuditId);
			var resultdt = releaseLogDetaildt.Clone();

			foreach (DataRow row in dt.Rows)
			{
				var rows = releaseLogDetaildt.Select("ReleaseLogDetailId = " + row[ReleaseLogDetailDataModel.DataColumns.ReleaseLogDetailId]);
				resultdt.ImportRow(rows[0]);
			}

			return resultdt;
		}

		private string[] GetReleaseLogDetailColumns()
		{
			return Framework.Components.ApplicationSecurity.GetReleaseLogDetailColumns("DBColumns", AuditId);
		}



		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ReleaseIssueType;
			PrimaryEntityKey = "ReleaseIssueType";
			DetailsControlPath = ApplicationCommon.GetControlPath("ReleaseIssueType", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion

    }
}