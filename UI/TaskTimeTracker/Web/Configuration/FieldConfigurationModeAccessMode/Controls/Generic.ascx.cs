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

namespace Shared.UI.Web.Configuration.FieldConfigurationModeAccessMode.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {        

		#region methods

		public override int? Save(string action)
		{
			var data = new FieldConfigurationModeAccessModeDataModel();

			data.FieldConfigurationModeAccessModeId = SystemKeyId;
			data.Name                               = Name;
			data.Description                        = Description;
			data.SortOrder                          = SortOrder;

			if (action == "Insert")
			{
				if(!FieldConfigurationModeAccessModeDataManager.DoesExist(data, SessionVariables.RequestProfile))
				{
					FieldConfigurationModeAccessModeDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				FieldConfigurationModeAccessModeDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of FieldConfigurationModeAccessModeID ?
			return data.FieldConfigurationModeAccessModeId;
		}

		public override void SetId(int setId, bool chkFieldConfigurationModeAccessModeId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkFieldConfigurationModeAccessModeId);
			CoreSystemKey.Enabled = chkFieldConfigurationModeAccessModeId;
			//txtDescription.Enabled = !chkFieldConfigurationModeAccessModeId;
			//txtName.Enabled = !chkFieldConfigurationModeAccessModeId;
			//txtSortOrder.Enabled = !chkFieldConfigurationModeAccessModeId;
		}

		public void LoadData(int FieldConfigurationModeAccessModeId, bool showId)
		{
			// clear UI				
			Clear();

			// set up parameters
			var data = new FieldConfigurationModeAccessModeDataModel();
			data.FieldConfigurationModeAccessModeId = FieldConfigurationModeAccessModeId;

			// get data
			var items = FieldConfigurationModeAccessModeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];

			SetData(item);

			if (!showId)
			{
				SystemKeyId = item.FieldConfigurationModeAccessModeId;
				oHistoryList.Setup(PrimaryEntity, FieldConfigurationModeAccessModeId, PrimaryEntityKey);
			}
			else
			{
				CoreSystemKey.Text = String.Empty;
			}		
			
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new FieldConfigurationModeAccessModeDataModel();

			SetData(data);
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
        {

            var isTesting = SessionVariables.IsTesting;
			CoreSystemKey.Visible = isTesting;
            lblFieldConfigurationModeAccessModeId.Visible = isTesting;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity                   = Framework.Components.DataAccess.SystemEntity.FieldConfigurationModeAccessMode;
			PrimaryEntityKey                = "FieldConfigurationModeAccessMode";
			FolderLocationFromRoot          = "FieldConfigurationModeAccessMode";

			// set object variable reference            
			PlaceHolderCore                 = dynFieldConfigurationModeAccessModeId;
			PlaceHolderAuditHistory         = dynAuditHistory;
			BorderDiv                       = borderdiv;

			PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey                   = txtFieldConfigurationModeAccessModeId;
			CoreControlName                 = txtName;
			CoreControlDescription          = txtDescription;
			CoreControlSortOrder            = txtSortOrder;

			CoreUpdateInfo                  = oUpdateInfo;
		}

		#endregion

	}
}