using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis;
using DataModel.TaskTimeTracker.RequirementAnalysis;

namespace ApplicationContainer.UI.Web.ProjectUseCaseStatus.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
	{

		#region methods

		public override int? Save(string action)
		{
			var data = new ProjectUseCaseStatusDataModel();

			data.ProjectUseCaseStatusId  = SystemKeyId;
			data.Name                    = Name;
			data.Description             = Description;
			data.SortOrder               = SortOrder;

			if (action == "Insert")
			{
                var dtProjectUseCaseStatus = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectUseCaseStatusDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtProjectUseCaseStatus.Rows.Count == 0)
				{
                    TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectUseCaseStatusDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{                
                TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectUseCaseStatusDataManager.Update(data, SessionVariables.RequestProfile);
			}

			return data.ProjectUseCaseStatusId;
		}

		public override void SetId(int setId, bool chkProjectUseCaseStatusId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkProjectUseCaseStatusId);
			CoreSystemKey.Enabled = chkProjectUseCaseStatusId;
			//txtDescription.Enabled = !chkProjectUseCaseStatusId;
			//txtName.Enabled = !chkProjectUseCaseStatusId;
			//txtSortOrder.Enabled = !chkProjectUseCaseStatusId;
		}

		public void LoadData(int projectUseCaseStatusId, bool showId)
		{
			Clear();

			var data = new ProjectUseCaseStatusDataModel();
			data.ProjectUseCaseStatusId = projectUseCaseStatusId;

            var items = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectUseCaseStatusDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			SetData(item);

			if (!showId)
			{
				SystemKeyId = item.ProjectUseCaseStatusId;
				oHistoryList.Setup(PrimaryEntity, projectUseCaseStatusId, PrimaryEntityKey);
			}
			else
			{
				CoreSystemKey.Text = String.Empty;
			}
		}

		protected override void Clear()
		{
			base.Clear();

            var data = new ProjectUseCaseStatusDataModel();

			SetData(data);
		}

		public void SetData(ProjectUseCaseStatusDataModel data)
		{
			SystemKeyId = data.ProjectUseCaseStatusId;

			base.SetData(data);
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			CoreSystemKey.Visible = isTesting;
			lblProjectUseCaseStatusId.Visible = isTesting;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity            = Framework.Components.DataAccess.SystemEntity.ProjectUseCaseStatus;
			PrimaryEntityKey         = "ProjectUseCaseStatus";
			FolderLocationFromRoot   = "/RequirementAnalysis";

			PlaceHolderCore          = dynProjectUseCaseStatusId;
			PlaceHolderAuditHistory  = dynAuditHistory;
			BorderDiv                = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey            = txtProjectUseCaseStatusId;
			CoreControlName          = txtName;
            CoreControlDescription   = txtDescription;
			CoreControlSortOrder     = txtSortOrder;

			CoreUpdateInfo = oUpdateInfo;
		}

		#endregion

	}
}