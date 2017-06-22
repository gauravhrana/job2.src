using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace Shared.UI.Web.Configuration.FieldConfigurationMode.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {        

		#region methods

		public override int? Save(string action)
		{
			var data = new FieldConfigurationModeDataModel();

			data.FieldConfigurationModeId = SystemKeyId;
			data.Name = Name;
			data.Description = Description;
			data.SortOrder = SortOrder;

			if (action == "Insert")
			{
				var dtFieldConfiguration = FieldConfigurationModeDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtFieldConfiguration.Rows.Count == 0)
				{
					FieldConfigurationModeDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				FieldConfigurationModeDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of FieldConfigurationModeID ?
			return data.FieldConfigurationModeId;
		}

		public override void SetId(int setId, bool chkFieldConfigurationModeId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkFieldConfigurationModeId);
			CoreSystemKey.Enabled = chkFieldConfigurationModeId;
			//txtDescription.Enabled = !chkFieldConfigurationModeId;
			//txtName.Enabled = !chkFieldConfigurationModeId;
			//txtSortOrder.Enabled = !chkFieldConfigurationModeId;
		}

		public void LoadData(int fieldConfigurationModeId, bool showId)
		{
			// clear UI				
			Clear();

			// set up parameters
			var data = new FieldConfigurationModeDataModel();
			data.FieldConfigurationModeId = fieldConfigurationModeId;

			// get data
			var items = FieldConfigurationModeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];

			SetData(item);

			if (!showId)
			{
				SystemKeyId = item.FieldConfigurationModeId;
				oHistoryList.Setup(PrimaryEntity, fieldConfigurationModeId, PrimaryEntityKey);
			}
			else
			{
				CoreSystemKey.Text = String.Empty;
			}		
			
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new FieldConfigurationModeDataModel();

			SetData(data);
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
        {

            var isTesting = SessionVariables.IsTesting;
			CoreSystemKey.Visible = isTesting;
            lblFieldConfigurationModeId.Visible = isTesting;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FieldConfigurationMode;
			PrimaryEntityKey = "FieldConfigurationMode";
			FolderLocationFromRoot = "FieldConfigurationMode";

			// set object variable reference            
			PlaceHolderCore = dynFieldConfigurationModeId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey = txtFieldConfigurationModeId;
			CoreControlName = txtName;
			CoreControlDescription = txtDescription;
			CoreControlSortOrder = txtSortOrder;

			CoreUpdateInfo = oUpdateInfo;
		}

		#endregion

	}
}