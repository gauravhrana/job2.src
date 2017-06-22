using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.EntityDateRangeStateType.Controls
{
	public partial class Generic : ControlGenericStandard
	{
		#region methods

		public override int? Save(string action)
		{
			var data = new EntityDateRangeStateTypeDataModel();

			data.EntityDateRangeStateTypeId  = SystemKeyId;
			data.Name                        = Name;
			data.Description                 = Description;
			data.SortOrder                   = SortOrder;

			if (action == "Insert")
			{
                var dtEntityDateRangeStateType = EntityDateRangeStateTypeDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtEntityDateRangeStateType.Rows.Count == 0)
				{
                    EntityDateRangeStateTypeDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
                EntityDateRangeStateTypeDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of EntityDateRangeStateTypeID ?
			return data.EntityDateRangeStateTypeId;
		}

		public override void SetId(int setId, bool chkEntityDateRangeStateTypeId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkEntityDateRangeStateTypeId);

			CoreSystemKey.Enabled = chkEntityDateRangeStateTypeId;
		}

		public void LoadData(int entityDateRangeStateTypeId, bool showId)
		{
			// clear UI				
			Clear();

			// set up parameters
			var data = new EntityDateRangeStateTypeDataModel();
			data.EntityDateRangeStateTypeId = entityDateRangeStateTypeId;

			// get data
            var items = EntityDateRangeStateTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];

			SetData(item);

			if (!showId)
			{
				SystemKeyId = item.EntityDateRangeStateTypeId;

				//PlaceHolderAuditHistory.Visible = true;
				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup(PrimaryEntity, entityDateRangeStateTypeId, PrimaryEntityKey);
			}
			else
			{
				CoreSystemKey.Text = String.Empty;
			}
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new EntityDateRangeStateTypeDataModel();

			SetData(data);
		}

		public void SetData(EntityDateRangeStateTypeDataModel data)
		{
			SystemKeyId = data.EntityDateRangeStateTypeId;

			base.SetData(data);
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			CoreSystemKey.Visible = isTesting;
			lblEntityDateRangeStateTypeId.Visible = isTesting;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.EntityDateRangeStateType;
			PrimaryEntityKey = "EntityDateRangeStateType";
			FolderLocationFromRoot = "EntityDateRangeStateType";

			// set object variable reference            
			PlaceHolderCore = dynEntityDateRangeStateTypeId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey = txtEntityDateRangeStateTypeId;
			CoreControlName = txtName;
			CoreControlDescription = txtDescription;
			CoreControlSortOrder = txtSortOrder;

			CoreUpdateInfo = oUpdateInfo;
		}

		#endregion
	}
}