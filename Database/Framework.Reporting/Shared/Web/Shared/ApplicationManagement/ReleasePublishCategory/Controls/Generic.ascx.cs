using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.ReleaseLog;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace Shared.UI.Web.ApplicationManagement.ReleasePublishCategory.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {        

        #region private methods

		public override int? Save(string action)
		{
			var data = new ReleasePublishCategoryDataModel();

			data.ReleasePublishCategoryId	= SystemKeyId;
			data.Name						= Name;
			data.Description				= Description;
			data.SortOrder					= SortOrder;

			if (action == "Insert")
			{
				var dtReleasePublishCategory = Framework.Components.ReleaseLog.ReleasePublishCategoryDataManager.DoesExist(data, AuditId);

				if (dtReleasePublishCategory.Rows.Count == 0)
				{
					Framework.Components.ReleaseLog.ReleasePublishCategoryDataManager.Create(data, AuditId);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				Framework.Components.ReleaseLog.ReleasePublishCategoryDataManager.Update(data, AuditId);
			}

			// not correct ... when doing insert, we didn't get/change the value of ReleasePublishCategoryId ?
			return data.ReleasePublishCategoryId;
		}

        public override void SetId(int setId, bool chkReleasePublishCategoryId)
        {
            ViewState["SetId"] = setId;

            //load data
            LoadData((int)ViewState["SetId"], chkReleasePublishCategoryId);
            txtReleasePublishCategoryId.Enabled = chkReleasePublishCategoryId;
            //txtDescription.Enabled = !chkReleasePublishCategoryId;
            //txtName.Enabled = chkReleasePublishCategoryId;
            //txtSortOrder.Enabled = !chkReleasePublishCategoryId;
        }

        public void LoadData(int releasePublishCategoryId, bool showId)
        {
			// clear UI				
			Clear();

			// set up parameters
			var data = new ReleasePublishCategoryDataModel();
			data.ReleasePublishCategoryId = releasePublishCategoryId;

			// get data
			var items = Framework.Components.ReleaseLog.ReleasePublishCategoryDataManager.GetEntityDetails(data, AuditId);

			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];

			SetData(item);

			if (!showId)
			{
				SystemKeyId = item.ReleasePublishCategoryId;
				
				oHistoryList.Setup(PrimaryEntity, releasePublishCategoryId, PrimaryEntityKey);
			}
			else
			{
				CoreSystemKey.Text = String.Empty;
			}			
			
        }

		protected override void Clear()
		{
			base.Clear();

			var data = new ReleasePublishCategoryDataModel();

			SetData(data);
			
		}

		public void SetData(ReleasePublishCategoryDataModel data)
		{
			SystemKeyId = data.ReleasePublishCategoryId;			

			base.SetData(data);
		}

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            txtReleasePublishCategoryId.Visible = isTesting;
            lblReleasePublishCategoryId.Visible = isTesting;
        }

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ReleasePublishCategory;
			PrimaryEntityKey = "ReleasePublishCategory";
			FolderLocationFromRoot = "ReleasePublishCategory";

			// set object variable reference            
			PlaceHolderCore = dynReleasePublishCategoryId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey			= txtReleasePublishCategoryId;
			CoreControlName			= txtName;
			CoreControlDescription	= txtDescription;
			CoreControlSortOrder	= txtSortOrder;

			CoreUpdateInfo = oUpdateInfo;

		}

        #endregion
    }
}