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

namespace Shared.UI.Web.Configuration.SystemForeignRelationshipDatabase.Controls
{
	public partial class Generic : ControlGenericStandard
	{
		#region private methods

		public override int? Save(string action)
		{
			var data = new SystemForeignRelationshipDatabaseDataModel();

			data.SystemForeignRelationshipDatabaseId = SystemKeyId;
			data.Name = Name;
			data.Description = Description;
			data.SortOrder = SortOrder;

			if (action == "Insert")
			{
				var dtSystemForeignRelationshipDatabase = Framework.Components.Core.SystemForeignRelationshipDatabaseDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtSystemForeignRelationshipDatabase.Rows.Count == 0)
				{
					Framework.Components.Core.SystemForeignRelationshipDatabaseDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				Framework.Components.Core.SystemForeignRelationshipDatabaseDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of ClientID ?
			return data.SystemForeignRelationshipDatabaseId;
		}

		public override void SetId(int setId, bool chkSystemForeignRelationshipDatabaseId)
		{
			ViewState["SetId"] = setId;

			//load data
			LoadData((int)ViewState["SetId"], chkSystemForeignRelationshipDatabaseId);
			CoreSystemKey.Enabled = chkSystemForeignRelationshipDatabaseId;
			//txtDescription.Enabled = !chkSystemForeignRelationshipDatabaseId;
			//txtName.Enabled = chkSystemForeignRelationshipDatabaseId;
			//txtSortOrder.Enabled = !chkSystemForeignRelationshipDatabaseId;
		}

		public void LoadData(int SystemForeignRelationshipDatabaseId, bool showId)
		{
			Clear();

			var data = new SystemForeignRelationshipDatabaseDataModel();
			data.SystemForeignRelationshipDatabaseId = SystemForeignRelationshipDatabaseId;

			var items = Framework.Components.Core.SystemForeignRelationshipDatabaseDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			SetData(item);

			if (!showId)
			{
				SystemKeyId = item.SystemForeignRelationshipDatabaseId;
				oHistoryList.Setup(PrimaryEntity, SystemForeignRelationshipDatabaseId, PrimaryEntityKey);
			}
			else
			{
				CoreSystemKey.Text = String.Empty;
			}
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new SystemForeignRelationshipDatabaseDataModel();

			SetData(data);
		}

		public void SetData(SystemForeignRelationshipDatabaseDataModel data)
		{
			SystemKeyId = data.SystemForeignRelationshipDatabaseId;

			base.SetData(data);
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			CoreSystemKey.Visible = isTesting;
			lblSystemForeignRelationshipDatabaseId.Visible = isTesting;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "SystemForeignRelationshipDatabase";
			FolderLocationFromRoot = "SystemForeignRelationshipDatabase";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SystemForeignRelationshipDatabase;

			PlaceHolderCore = dynSystemForeignRelationshipDatabaseId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey = txtSystemForeignRelationshipDatabaseId;
			CoreControlName = txtName;
			CoreControlDescription = txtDescription;
			CoreControlSortOrder = txtSortOrder;

			CoreUpdateInfo = oUpdateInfo;
		}

		#endregion
	}
}