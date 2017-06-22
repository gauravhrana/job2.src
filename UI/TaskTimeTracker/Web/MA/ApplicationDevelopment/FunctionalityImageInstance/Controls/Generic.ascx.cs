using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityImageInstance.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties   

        public int? FunctionalityImageInstanceId
        {
            get
            {
                if (txtFunctionalityImageInstanceId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtFunctionalityImageInstanceId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtFunctionalityImageInstanceId.Text);
                }
            }
            set
            {
                txtFunctionalityImageInstanceId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public int? FunctionalityImageId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtFunctionalityImageId.Text.Trim());
                else
                    return int.Parse(drpFunctionalityImageList.SelectedItem.Value);
            }
            set
            {
                txtFunctionalityImageId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public int? FunctionalityImageAttributeId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtFunctionalityImageAttributeId.Text.Trim());
                else
                    return int.Parse(drpFunctionalityImageAttributeList.SelectedItem.Value);
            }
            set
            {
                txtFunctionalityImageAttributeId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        #endregion properties

        #region private methods

        public override int? Save(string action)
        {
            var data = new FunctionalityImageInstanceDataModel();

            data.FunctionalityImageInstanceId = FunctionalityImageInstanceId;
            data.FunctionalityImageId = FunctionalityImageId;
            data.FunctionalityImageAttributeId = FunctionalityImageAttributeId;

            if (action == "Insert")
            {
				//var dtApplicationMode = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityImageInstanceDataManager.DoesExist(data, AuditId);

				//if (dtApplicationMode.Rows.Count == 0)
				//{
                    TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityImageInstanceDataManager.Create(data, SessionVariables.RequestProfile);
				//}
				//else
				//{
				//	throw new Exception("Record with given ID already exists.");
				//}
            }
            else
            {
				TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityImageInstanceDataManager.Update(data, SessionVariables.RequestProfile);
            }

            // not correct ... when doing insert, we didn't get/change the value of ClientID ?
            return data.FunctionalityImageInstanceId;
        }

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;

			var functionalityImageData = FunctionalityImageDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(functionalityImageData, drpFunctionalityImageList,
                FunctionalityImageDataModel.DataColumns.Title,
                FunctionalityImageDataModel.DataColumns.FunctionalityImageId);

			var functionalityImageAttributeData = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityImageAttributeDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(functionalityImageAttributeData, drpFunctionalityImageAttributeList,
                StandardDataModel.StandardDataColumns.Name,
                FunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageAttributeId);


            if (isTesting)
            {

                drpFunctionalityImageList.AutoPostBack = true;
                drpFunctionalityImageAttributeList.AutoPostBack = true;

                if (drpFunctionalityImageList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtFunctionalityImageId.Text.Trim()))
                    {
                        drpFunctionalityImageList.SelectedValue = txtFunctionalityImageId.Text;
                    }
                    else
                    {
                        txtFunctionalityImageId.Text = drpFunctionalityImageList.SelectedItem.Value;
                    }
                }

                if (drpFunctionalityImageAttributeList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtFunctionalityImageAttributeId.Text.Trim()))
                    {
                        drpFunctionalityImageAttributeList.SelectedValue = txtFunctionalityImageAttributeId.Text;
                    }
                    else
                    {
                        txtFunctionalityImageAttributeId.Text = drpFunctionalityImageAttributeList.SelectedItem.Value;
                    }
                }

                txtFunctionalityImageId.Visible = true;
                txtFunctionalityImageAttributeId.Visible = true;

            }
            else
            {

                if (!string.IsNullOrEmpty(txtFunctionalityImageId.Text.Trim()))
                {
                    drpFunctionalityImageList.SelectedValue = txtFunctionalityImageId.Text;
                }

                if (!string.IsNullOrEmpty(txtFunctionalityImageAttributeId.Text.Trim()))
                {
                    drpFunctionalityImageAttributeList.SelectedValue = txtFunctionalityImageAttributeId.Text;
                }

                txtFunctionalityImageId.Visible = false;
                txtFunctionalityImageAttributeId.Visible = false;

            }
        }

        public override void SetId(int setId, bool chkFunctionalityImageInstanceId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkFunctionalityImageInstanceId);
            txtFunctionalityImageInstanceId.Enabled = chkFunctionalityImageInstanceId;
            //txtDescription.Enabled = !chkFunctionalityImageInstanceId;
            //txtName.Enabled = !chkFunctionalityImageInstanceId;
            //txtSortOrder.Enabled = !chkFunctionalityImageInstanceId;
        }

        public void LoadData(int FunctionalityImageInstanceId, bool showId)
        {
            // clear UI				
            Clear();

            // set up parameters
            var data = new FunctionalityImageInstanceDataModel();
            data.FunctionalityImageInstanceId = FunctionalityImageInstanceId;

            // get data
			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityImageInstanceDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            // should only have single match -- should log exception.
            if (items.Count != 1) return;

            var item = items[0];

            txtFunctionalityImageInstanceId.Text = item.FunctionalityImageInstanceId.ToString();
            txtFunctionalityImageAttributeId.Text = item.FunctionalityImageAttributeId.ToString();
            txtFunctionalityImageId.Text = item.FunctionalityImageId.ToString();
            imgApplicationUserImage.Visible = true;
            imgApplicationUserImage.ImageUrl = "~/ApplicationDevelopment/FunctionalityImage/ShowImage.aspx?imageid=" + item.FunctionalityImageId;
            imgApplicationUserImage1.ImageUrl = "~/ApplicationDevelopment/FunctionalityImage/ShowImage.aspx?imageid=" + item.FunctionalityImageId;

            if (!showId)
            {
                txtFunctionalityImageInstanceId.Text = item.FunctionalityImageInstanceId.ToString();
                //PlaceHolderAuditHistory.Visible = true;
                // only show Audit History in case of Update page, not for Clone.
                oHistoryList.Setup(PrimaryEntity, FunctionalityImageInstanceId, PrimaryEntityKey);
            }
            else
            {
                txtFunctionalityImageInstanceId.Text = String.Empty;
            }

            oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

        }

        protected override void Clear()
        {
            base.Clear();

            var data = new FunctionalityImageInstanceDataModel();

            FunctionalityImageInstanceId = data.FunctionalityImageInstanceId;
            FunctionalityImageAttributeId = data.FunctionalityImageAttributeId;
            FunctionalityImageId = data.FunctionalityImageId;

        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetupDropdown();
            }
            var isTesting = SessionVariables.IsTesting;
            txtFunctionalityImageInstanceId.Visible = isTesting;
            lblFunctionalityImageInstanceId.Visible = isTesting;
        }

        protected void drpFunctionalityImageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFunctionalityImageId.Text = drpFunctionalityImageList.SelectedItem.Value;
            imgApplicationUserImage.Visible = true;
            imgApplicationUserImage.ImageUrl = "~/ApplicationDevelopment/FunctionalityImage/ShowImage.aspx?imageid=" + drpFunctionalityImageList.SelectedItem.Value.ToString();
            imgApplicationUserImage1.ImageUrl = "~/ApplicationDevelopment/FunctionalityImage/ShowImage.aspx?imageid=" + drpFunctionalityImageList.SelectedItem.Value.ToString();
        }

        protected void drpFunctionalityImageAttributeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFunctionalityImageAttributeId.Text = drpFunctionalityImageAttributeList.SelectedItem.Value;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey = "FunctionalityImageInstance";
            FolderLocationFromRoot = "/Shared/QualityAssurance";
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityImageInstance;

            // set object variable reference            
            PlaceHolderCore = dynFunctionalityImageId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
        }

        #endregion
    }
}