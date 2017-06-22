using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Import;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace Shared.UI.Web.FileType.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {
        #region methods

        public override int? Save(string action)
        {
            var data = new FileTypeDataModel();

            data.FileTypeId  = SystemKeyId;
            data.Name        = Name;
            data.Description = Description;
            data.SortOrder   = SortOrder;

            if (action == "Insert")
            {
				var dtFileType = Framework.Components.Import.FileTypeDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtFileType.Rows.Count == 0)
                {
					Framework.Components.Import.FileTypeDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
				Framework.Components.Import.FileTypeDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.FileTypeId;
        }

        public override void SetId(int setId, bool chkFileTypeId)
        {
            ViewState["SetId"] = setId;
            LoadData((int)ViewState["SetId"], chkFileTypeId);
            CoreSystemKey.Enabled = chkFileTypeId;
        }

        public void LoadData(int fileTypeId, bool showId)
        {         			
            Clear();
            var data = new FileTypeDataModel();
            data.FileTypeId = fileTypeId;
			var items = Framework.Components.Import.FileTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.FileTypeId;               
                oHistoryList.Setup(PrimaryEntity, fileTypeId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();
            var data = new FileTypeDataModel();
            SetData(data);
        }

        public void SetData(FileTypeDataModel data)
        {
            SystemKeyId = data.FileTypeId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblFileTypeId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FileType;
            PrimaryEntityKey = "FileType";
            FolderLocationFromRoot = "FileType";
          
            PlaceHolderCore = dynFileTypeId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtFileTypeId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}