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

namespace Shared.UI.Web.Configuration.FieldConfigurationModeCategory.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {
        
        #region private methods

		public override int? Save(string action)
		{
			var data = new FieldConfigurationModeCategoryDataModel();

			data.FieldConfigurationModeCategoryId = SystemKeyId;
			data.Name = Name;
			data.Description = Description;
			data.SortOrder = SortOrder;

			if (action == "Insert")
			{
				var dtFieldConfigurationModeCategory = FieldConfigurationModeCategoryDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtFieldConfigurationModeCategory.Rows.Count == 0)
				{
					FieldConfigurationModeCategoryDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				FieldConfigurationModeCategoryDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of FieldConfigurationModeCategoryId ?
			return data.FieldConfigurationModeCategoryId;
		}

        public override void SetId(int setId, bool chkFieldConfigurationModeCategoryId)
        {
            ViewState["SetId"] = setId;

            //load data
            LoadData((int)ViewState["SetId"], chkFieldConfigurationModeCategoryId);
			CoreSystemKey.Enabled = chkFieldConfigurationModeCategoryId;
            //txtDescription.Enabled = !chkFieldConfigurationModeCategoryId;
            //txtName.Enabled = chkFieldConfigurationModeCategoryId;
            //txtSortOrder.Enabled = !chkFieldConfigurationModeCategoryId;
        }

        public void LoadData(int fieldConfigurationModeCategoryId, bool showId)
        {
			// clear UI				
			Clear();

			// set up parameters
			var data = new FieldConfigurationModeCategoryDataModel();
			data.FieldConfigurationModeCategoryId = fieldConfigurationModeCategoryId;

			// get data
			var items = FieldConfigurationModeCategoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];

			SetData(item);

			if (!showId)
			{
				SystemKeyId = item.FieldConfigurationModeCategoryId;
				oHistoryList.Setup(PrimaryEntity, fieldConfigurationModeCategoryId, PrimaryEntityKey);
			}
			else
			{
				CoreSystemKey.Text = String.Empty;
			}
        }

		protected override void Clear()
		{
			base.Clear();

			var data = new FieldConfigurationModeCategoryDataModel();

			SetData(data);
		}

		public void SetData(FieldConfigurationModeCategoryDataModel data)
		{
			SystemKeyId = data.FieldConfigurationModeCategoryId;

			base.SetData(data);
		}

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
			CoreSystemKey.Visible = isTesting;
            lblFieldConfigurationModeCategoryId.Visible = isTesting;
        }

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FieldConfigurationModeCategory;
			PrimaryEntityKey = "FieldConfigurationModeCategory";
			FolderLocationFromRoot = "FieldConfigurationModeCategory";

			// set object variable reference            
			PlaceHolderCore = dynFieldConfigurationModeCategoryId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey = txtFieldConfigurationModeCategoryId;
			CoreControlName = txtName;
			CoreControlDescription = txtDescription;
			CoreControlSortOrder = txtSortOrder;

			CoreUpdateInfo = oUpdateInfo;
		}

        #endregion
    }
}