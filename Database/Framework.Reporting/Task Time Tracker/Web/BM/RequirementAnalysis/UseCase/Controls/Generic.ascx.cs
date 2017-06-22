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

namespace ApplicationContainer.UI.Web.UseCase.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
	{

		#region methods

		public override int? Save(string action)
		{
			var data = new UseCaseDataModel();

			data.UseCaseId		= SystemKeyId;
			data.Name			= Name;
			data.Description	= Description;
			data.SortOrder		= SortOrder;

			if (action == "Insert")
			{
                var dtUseCase = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtUseCase.Rows.Count == 0)
				{
                    TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{                
                TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.Update(data, SessionVariables.RequestProfile);
			}

			return data.UseCaseId;
		}

		public override void SetId(int setId, bool chkUseCaseId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkUseCaseId);
			CoreSystemKey.Enabled = chkUseCaseId;
			//txtDescription.Enabled = !chkUseCaseId;
			//txtName.Enabled = !chkUseCaseId;
			//txtSortOrder.Enabled = !chkUseCaseId;
		}

		public void LoadData(int useCaseId, bool showId)
		{
			Clear();

			var data = new UseCaseDataModel();
			data.UseCaseId = useCaseId;

            var items = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			SetData(item);

			if (!showId)
			{
				SystemKeyId = item.UseCaseId;
				oHistoryList.Setup(PrimaryEntity, useCaseId, PrimaryEntityKey);
			}
			else
			{
				CoreSystemKey.Text = String.Empty;
			}
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new UseCaseDataModel();

			SetData(data);
		}

		public void SetData(UseCaseDataModel data)
		{
			SystemKeyId = data.UseCaseId;

			base.SetData(data);
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			CoreSystemKey.Visible = isTesting;
			lblUseCaseId.Visible = isTesting;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity			= Framework.Components.DataAccess.SystemEntity.UseCase;
			PrimaryEntityKey		= "UseCase";
			FolderLocationFromRoot  = "RequirementAnalysis/";

			PlaceHolderCore			= dynUseCaseId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv				= borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey			= txtUseCaseId;
			CoreControlName			= txtName;
            CoreControlDescription  = txtDescription;
			CoreControlSortOrder	= txtSortOrder;

			CoreUpdateInfo			= oUpdateInfo;
		}

		#endregion

	}
}