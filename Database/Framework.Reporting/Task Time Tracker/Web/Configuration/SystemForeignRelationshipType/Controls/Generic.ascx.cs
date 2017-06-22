using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Configuration.SystemForeignRelationshipType.Controls
{
	public partial class Generic : ControlGenericStandard
	{
		#region private methods

		public override int? Save(string action)
		{
			var data = new SystemForeignRelationshipTypeDataModel();

			data.SystemForeignRelationshipTypeId = SystemKeyId;
			data.Name = Name;
			data.Description = Description;
			data.SortOrder = SortOrder;

			if (action == "Insert")
			{
				var dtSystemForeignRelationshipType = Framework.Components.Core.SystemForeignRelationshipTypeDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtSystemForeignRelationshipType.Rows.Count == 0)
				{
					Framework.Components.Core.SystemForeignRelationshipTypeDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				Framework.Components.Core.SystemForeignRelationshipTypeDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of ClientID ?
			return data.SystemForeignRelationshipTypeId;
		}

		public override void SetId(int setId, bool chkSystemForeignRelationshipTypeId)
		{
			ViewState["SetId"] = setId;

			//load data
			LoadData((int)ViewState["SetId"], chkSystemForeignRelationshipTypeId);
			CoreSystemKey.Enabled = chkSystemForeignRelationshipTypeId;
			//txtDescription.Enabled = !chkSystemForeignRelationshipTypeId;
			//txtName.Enabled = chkSystemForeignRelationshipTypeId;
			//txtSortOrder.Enabled = !chkSystemForeignRelationshipTypeId;
		}

		public void LoadData(int SystemForeignRelationshipTypeId, bool showId)
		{
			Clear();

			var data = new SystemForeignRelationshipTypeDataModel();
			data.SystemForeignRelationshipTypeId = SystemForeignRelationshipTypeId;

			var items = Framework.Components.Core.SystemForeignRelationshipTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			SetData(item);

			if (!showId)
			{
				SystemKeyId = item.SystemForeignRelationshipTypeId;
				oHistoryList.Setup(PrimaryEntity, SystemForeignRelationshipTypeId, PrimaryEntityKey);
			}
			else
			{
				CoreSystemKey.Text = String.Empty;
			}
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new SystemForeignRelationshipTypeDataModel();

			SetData(data);
		}

		public void SetData(SystemForeignRelationshipTypeDataModel data)
		{
			SystemKeyId = data.SystemForeignRelationshipTypeId;

			base.SetData(data);
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			CoreSystemKey.Visible = isTesting;
			lblSystemForeignRelationshipTypeId.Visible = isTesting;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "SystemForeignRelationshipType";
			FolderLocationFromRoot = "SystemForeignRelationshipType";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SystemForeignRelationshipType;

			PlaceHolderCore = dynSystemForeignRelationshipTypeId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey = txtSystemForeignRelationshipTypeId;
			CoreControlName = txtName;
			CoreControlDescription = txtDescription;
			CoreControlSortOrder = txtSortOrder;

			CoreUpdateInfo = oUpdateInfo;
		}

		#endregion
	}
}