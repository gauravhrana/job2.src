using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.Framework.Import;
using Framework.Components.Import;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;

namespace Shared.UI.Web.BatchFile.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

        public int? BatchFileId
        {
            get
            {
                if (txtBatchFileId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtBatchFileId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtBatchFileId.Text);
                }
            }
        }

        public int? BatchFileSetId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtBatchFileSetId.Text.Trim());
                else
                    return int.Parse(drpBatchFileSetList.SelectedItem.Value);
            }
        }

        public int? SystemEntityTypeId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtSystemEntityTypeId.Text.Trim());
                else
                    return int.Parse(drpSystemEntityTypeList.SelectedItem.Value);
            }
        }

        public int? FileTypeId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtFileTypeId.Text.Trim());
                else
                    return int.Parse(drpFileTypeList.SelectedItem.Value);
            }
        }

        public int? BatchFileStatusId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtBatchFileStatusId.Text.Trim());
                else
                    return int.Parse(drpBatchFileStatusList.SelectedItem.Value);
            }
        }

        public string Name
        {
            get
            {
                if (FileUpload.HasFile)
                {
                    return this.FileUpload.FileName;
                }
                else if (!string.IsNullOrEmpty(txtName.Text.Trim()))
                {
                    return txtName.Text.Trim();
                }
                else
                    return null;
            }
        }

        public string Folder
        {
            get
            {
                return "~/Import/Files/";
            }
        }

        public FileUpload BatchFile
        {
            get
            {
                return this.FileUpload;
            }
        }

        public string Description
        {
            get
            {
                return txtDescription.Text;
            }
        }

        protected override string ValidationConfigFile
        {
            get
            {
                return Server.MapPath("~/Shared/Admin/Import/Controls/Validation.xml"); //"R:\Layers\Task Time Tracker\Dev\UI\Web\ValidationDataFile\dvc.xml";
            }
        }

        #endregion properties

        #region private methods

        public override void SetId(int setId, bool chkBatchFileId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkBatchFileId);
            txtBatchFileId.Enabled = !chkBatchFileId;
            //FileUpload.Enabled = !chkBatchFileId;
            //txtBatchFileSetId.Enabled = !chkBatchFileId;
            //txtSystemEntityTypeId.Enabled = !chkBatchFileId;
            //txtFileTypeId.Enabled = !chkBatchFileId;
            //txtBatchFileStatusId.Enabled = !chkBatchFileId;
            //txtDescription.Enabled = !chkBatchFileId;
            //txtName.Enabled = !chkBatchFileId;

            //drpSystemEntityTypeList.Enabled = !chkBatchFileId;
            //drpFileTypeList.Enabled = !chkBatchFileId;
            //drpBatchFileStatusList.Enabled = !chkBatchFileId;
            //drpBatchFileSetList.Enabled = !chkBatchFileId;
        }

		public override int? Save(string action)
		{
			var data = new BatchFileDataModel();

			data.BatchFileId		= BatchFileId;
			data.BatchFileSetId		= BatchFileSetId;
			data.BatchFileStatusId	= BatchFileStatusId;
			data.FileTypeId			= FileTypeId;
			data.SystemEntityTypeId = SystemEntityTypeId;			
			data.Name				= Name;
			data.Description		= Description;

			if (action == "Insert")
			{
				if(!Framework.Components.Import.BatchFileDataManager.DoesExist(data, SessionVariables.RequestProfile))
				{
					Framework.Components.Import.BatchFileDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				Framework.Components.Import.BatchFileDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of BatchFileSetID ?
			return BatchFileSetId;
		}

        public void LoadData(int batchFileId, bool showId)
        {
			var data = new BatchFileDataModel();
            data.BatchFileId = batchFileId;
			var oDetail = Framework.Components.Import.BatchFileDataManager.GetDetails(data, SessionVariables.RequestProfile);

            if (oDetail != null)
            {

                if (!showId)
                {

                    txtBatchFileId.Text = oDetail.BatchFileId.ToString();

                    // only show Audit History in case of Update page, not for Clone.
                    oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.BatchFile, batchFileId, "BatchFile");
                    dynAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "BatchFile");

                    oUpdateInfo.LoadText(oDetail);
				}
                else
                {
                    txtBatchFileId.Text = String.Empty;
                }

				txtName.Text                          = oDetail.Name;
				txtDescription.Text                   = oDetail.Description;
				txtBatchFileSetId.Text                = oDetail.BatchFileSetId.ToString();
				txtFileTypeId.Text                    = oDetail.FileTypeId.ToString();
				txtSystemEntityTypeId.Text            = oDetail.SystemEntityTypeId.ToString();
				txtBatchFileStatusId.Text             = oDetail.BatchFileStatusId.ToString();
                                                        
				drpSystemEntityTypeList.SelectedValue = oDetail.SystemEntityTypeId.ToString();
				drpFileTypeList.SelectedValue         = oDetail.FileTypeId.ToString();
				drpBatchFileStatusList.SelectedValue  = oDetail.BatchFileStatusId.ToString();
				drpBatchFileSetList.SelectedValue     = oDetail.BatchFileSetId.ToString();

                oUpdateInfo.LoadText(oDetail);
			}
            else
            {
                txtName.Text               = String.Empty;
                txtBatchFileId.Text        = String.Empty;
                txtDescription.Text        = String.Empty;
                txtBatchFileSetId.Text     = String.Empty;
                txtFileTypeId.Text         = String.Empty;
                txtSystemEntityTypeId.Text = String.Empty;
                txtBatchFileStatusId.Text  = String.Empty;
            }
        }

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;

			var fileTypeData = FileTypeDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(fileTypeData, drpFileTypeList, StandardDataModel.StandardDataColumns.Name, FileTypeDataModel.DataColumns.FileTypeId);

            var batchFileSetData = Framework.Components.Import.BatchFileSetDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(batchFileSetData, drpBatchFileSetList, StandardDataModel.StandardDataColumns.Name, BatchFileSetDataModel.DataColumns.BatchFileSetId);

            var batchFileStatusData = Framework.Components.Import.BatchFileStatusDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(batchFileStatusData, drpBatchFileStatusList, StandardDataModel.StandardDataColumns.Name, BatchFileStatusDataModel.DataColumns.BatchFileStatusId);

			var systemEntityTypeData = Framework.Components.Core.SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(systemEntityTypeData, drpSystemEntityTypeList, SystemEntityTypeDataModel.DataColumns.EntityName, SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);

            if (isTesting)
            {
                drpSystemEntityTypeList.AutoPostBack = true;
                drpBatchFileSetList.AutoPostBack = true;
                drpBatchFileStatusList.AutoPostBack = true;
                drpFileTypeList.AutoPostBack = true;
                if (drpSystemEntityTypeList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtSystemEntityTypeId.Text.Trim()))
                    {
                        drpSystemEntityTypeList.SelectedValue = txtSystemEntityTypeId.Text;
                    }
                    else
                    {
                        txtSystemEntityTypeId.Text = drpSystemEntityTypeList.SelectedItem.Value;
                    }
                }
                if (drpFileTypeList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtFileTypeId.Text.Trim()))
                    {
                        drpFileTypeList.SelectedValue = txtFileTypeId.Text;
                    }
                    else
                    {
                        txtFileTypeId.Text = drpFileTypeList.SelectedItem.Value;
                    }
                }
                if (drpBatchFileSetList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtBatchFileSetId.Text.Trim()))
                    {
                        drpBatchFileSetList.SelectedValue = txtBatchFileSetId.Text;
                    }
                    else
                    {
                        txtBatchFileSetId.Text = drpBatchFileSetList.SelectedItem.Value;
                    }
                }
                if (drpBatchFileStatusList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtBatchFileStatusId.Text.Trim()))
                    {
                        drpBatchFileStatusList.SelectedValue = txtBatchFileStatusId.Text;
                    }
                    else
                    {
                        txtBatchFileStatusId.Text = drpBatchFileStatusList.SelectedItem.Value;
                    }
                }
                txtSystemEntityTypeId.Visible = true;
                txtFileTypeId.Visible = true;
                txtBatchFileSetId.Visible = true;
                txtBatchFileStatusId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtSystemEntityTypeId.Text.Trim()))
                {
                    drpSystemEntityTypeList.SelectedValue = txtSystemEntityTypeId.Text;
                }
                if (!string.IsNullOrEmpty(txtFileTypeId.Text.Trim()))
                {
                    drpFileTypeList.SelectedValue = txtFileTypeId.Text;
                }
                if (!string.IsNullOrEmpty(txtBatchFileSetId.Text.Trim()))
                {
                    drpBatchFileSetList.SelectedValue = txtBatchFileSetId.Text;
                }
                if (!string.IsNullOrEmpty(txtBatchFileStatusId.Text.Trim()))
                {
                    drpBatchFileStatusList.SelectedValue = txtBatchFileStatusId.Text;
                }
            }
        }

        #endregion

        #region Events
	
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "BatchFile";
			FolderLocationFromRoot = "Shared/Admin/BatchFile";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.BatchFile;

			// set object variable reference            
			PlaceHolderCore = dynBatchFileId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}
		
        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                txtBatchFileId.Visible = isTesting;
                lblBatchFileId.Visible = isTesting;

                SetupDropdown();
            }
        }        

        protected void drpBatchFileStatusList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBatchFileStatusId.Text = drpBatchFileStatusList.SelectedItem.Value;
        }

        protected void drpBatchFileSetList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBatchFileSetId.Text = drpBatchFileSetList.SelectedItem.Value;
        }

        protected void drpFileTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFileTypeId.Text = drpFileTypeList.SelectedItem.Value;
        }

        protected void drpSystemEntityTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSystemEntityTypeId.Text = drpSystemEntityTypeList.SelectedItem.Value;
        }

        #endregion

    }
}