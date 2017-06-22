using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Need.Controls
{
	public partial class Generic : ControlGenericStandard
	{
		#region methods

		public override int? Save(string action)
		{
			var data = new NeedDataModel();

			data.NeedId = SystemKeyId;
			data.Name = Name;
			data.Description = Description;
			data.SortOrder = SortOrder;

			if (action == "Insert")
			{
                var dtNeed = NeedDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtNeed.Rows.Count == 0)
				{
                    NeedDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
                NeedDataManager.Update(data, SessionVariables.RequestProfile);
			}

			return data.NeedId;
		}

		public override void SetId(int setId, bool chkNeedId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkNeedId);
			CoreSystemKey.Enabled = chkNeedId;
			//txtDescription.Enabled = !chkNeedId;
			//txtName.Enabled = !chkNeedId;
			//txtSortOrder.Enabled = !chkNeedId;
		}

		public void LoadData(int needId, bool showId)
		{
			Clear();

			var data = new NeedDataModel();
			data.NeedId = needId;

            var items = NeedDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			SetData(item);

			if (!showId)
			{
				SystemKeyId = item.NeedId;
				oHistoryList.Setup(PrimaryEntity, needId, PrimaryEntityKey);
			}
			else
			{
				CoreSystemKey.Text = String.Empty;
			}
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new NeedDataModel();

			SetData(data);
		}

		public void SetData(NeedDataModel data)
		{
			SystemKeyId = data.NeedId;

			base.SetData(data);
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			CoreSystemKey.Visible = isTesting;
			lblNeedId.Visible = isTesting;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.Need;
			PrimaryEntityKey = "Need";
			FolderLocationFromRoot = "/RequirementAnalysis";

			PlaceHolderCore = dynNeedId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey = txtNeedId;
			CoreControlName = txtName;
			CoreControlDescription = txtDescription;
			CoreControlSortOrder = txtSortOrder;

			CoreUpdateInfo = oUpdateInfo;
		}

		#endregion

	}
}