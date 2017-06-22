using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace Shared.UI.Web.Configuration.DateRangeTitle.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
	{		

		#region private methods

		public override int? Save(string action)
		{
			var data = new DateRangeTitleDataModel();

			data.DateRangeTitleId = SystemKeyId;
			data.Name = Name;
			data.Description = Description;
			data.SortOrder = SortOrder;

			if (action == "Insert")
			{
                if(!Framework.Components.UserPreference.DateRangeTitleDataManager.DoesExist(data, SessionVariables.RequestProfile))
				{
                    Framework.Components.UserPreference.DateRangeTitleDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
                Framework.Components.UserPreference.DateRangeTitleDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of ClientID ?
			return data.DateRangeTitleId;
		}

		public override void SetId(int setId, bool chkDateRangeTitleId)
		{
			ViewState["SetId"] = setId;

			//load data
			LoadData((int)ViewState["SetId"], chkDateRangeTitleId);
			CoreSystemKey.Enabled = chkDateRangeTitleId;
			//txtDescription.Enabled = !chkDateRangeTitleId;
			//txtName.Enabled = chkDateRangeTitleId;
			//txtSortOrder.Enabled = !chkDateRangeTitleId;
		}

		public void LoadData(int dateRangeTitleId, bool showId)
		{
			// clear UI
			Clear();

			// set up parameters
			var data = new DateRangeTitleDataModel();
			data.DateRangeTitleId = dateRangeTitleId;

			// get data
            var items = Framework.Components.UserPreference.DateRangeTitleDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count != 1) return;

			var item = items[0];

			SetData(item);

			if (!showId)
			{
				SystemKeyId = item.DateRangeTitleId;				
				oHistoryList.Setup(PrimaryEntity, dateRangeTitleId, PrimaryEntityKey);
			}
			else
			{
				CoreSystemKey.Text = String.Empty;
			}
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new DateRangeTitleDataModel();

			SetData(data);
		}

		public void SetData(DateRangeTitleDataModel data)
		{
			SystemKeyId = data.DateRangeTitleId;

			base.SetData(data);
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			CoreSystemKey.Visible = isTesting;
			lblDateRangeTitleId.Visible = isTesting;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "DateRangeTitle";
			FolderLocationFromRoot = "DateRangeTitle";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.DateRangeTitle;

			// set object variable reference            
			PlaceHolderCore = dynDateRangeTitleId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey = txtDateRangeTitleId;
			CoreControlName = txtName;
			CoreControlDescription = txtDescription;
			CoreControlSortOrder = txtSortOrder;

			CoreUpdateInfo = oUpdateInfo;	
		}        

		#endregion
	}
}