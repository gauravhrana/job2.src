using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using System.Data;
using DataModel.TaskTimeTracker.ApplicationDevelopment;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityXFunctionalityImage.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {
        #region properties

		public string ConvertDateTimeFormat
		{
			get
			{
				return DateTimeHelper.CovertDateFormatToJavascript();
			}
		}

        public int? FunctionalityXFunctionalityImageId
        {
            get
            {
                if (txtFunctionalityXFunctionalityImageId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtFunctionalityXFunctionalityImageId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtFunctionalityXFunctionalityImageId.Text);
                }
            }
        }

        public int FunctionalityId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtFunctionalityId.Text.Trim());
                else
                    return int.Parse(drpFunctionalityList.SelectedItem.Value);
            }
        }

        public int FunctionalityImageId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtFunctionalityImageId.Text.Trim());
                else
                    return int.Parse(drpFunctionalityImageList.SelectedItem.Value);
            }
        }

        public string KeyString
        {
            get
            {
                return txtKeyString.Text;
            }
        }

        public string Title
        {
            get
            {
                return txtTitle.Text;
            }
        }

        public string Description
        {
            get
            {
                return txtDescription.Text;
            }
            set
            {
                txtDescription.Text = value;
            }
        }

        public int SortOrder
        {
            get
            {
                return int.Parse(txtSortOrder.Text.Trim());
            }
        }

        public string CreatedBy
        {
            get
            {
                return txtCreatedBy.Text;
            }
        }

        public string UpdatedBy
        {
            get
            {
                return txtUpdatedBy.Text;
            }
        }

        public int CreatedDate
        {
            get
            {
                return int.Parse(DateTimeHelper.FromUserDateFormatToDate(txtCreatedDate.Text.Trim()).Value.ToString("yyyyMMdd"));
            }
        }

        public int UpdatedDate
        {
            get
            {
                return int.Parse(DateTimeHelper.FromUserDateFormatToDate(txtUpdatedDate.Text.Trim()).Value.ToString("yyyyMMdd"));
            }
        }

        protected override string ValidationConfigFile
        {
            get
            {
                return Server.MapPath("~/ApplicationDevelopment/FunctionalityXFunctionalityImage/Controls/Validation.xml"); //"R:\Layers\Task Time Tracker\Dev\UI\Web\ValidationDataFile\dvc.xml";
            }
        }

        #endregion properties

        #region private methods

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;

			var functionalityData = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(functionalityData, drpFunctionalityList,
                StandardDataModel.StandardDataColumns.Name,
                FunctionalityDataModel.DataColumns.FunctionalityId);

			var functionalityImageData = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityImageDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(functionalityImageData, drpFunctionalityImageList,
                FunctionalityImageDataModel.DataColumns.Title,
                FunctionalityImageDataModel.DataColumns.FunctionalityImageId);

            if (isTesting)
            {
                drpFunctionalityList.AutoPostBack = true;
                drpFunctionalityImageList.AutoPostBack = true;

                if (drpFunctionalityList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtFunctionalityId.Text.Trim()))
                    {
                        drpFunctionalityList.SelectedValue = txtFunctionalityId.Text;
                    }
                    else
                    {
                        txtFunctionalityId.Text = drpFunctionalityList.SelectedItem.Value;
                    }
                }
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

                txtFunctionalityId.Visible = true;
                txtFunctionalityImageId.Visible = true;

            }
            else
            {

                if (!string.IsNullOrEmpty(txtFunctionalityId.Text.Trim()))
                {
                    drpFunctionalityList.SelectedValue = txtFunctionalityId.Text;
                }
                if (!string.IsNullOrEmpty(txtFunctionalityImageId.Text.Trim()))
                {
                    drpFunctionalityImageList.SelectedValue = txtFunctionalityImageId.Text;
                }

                txtFunctionalityId.Visible = false;
                txtFunctionalityImageId.Visible = false;

            }
        }

        public override void SetId(int setId, bool chkApplicationId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkApplicationId);
            txtFunctionalityXFunctionalityImageId.Enabled = chkApplicationId;

        }

        public void LoadData(int functionalityXFunctionalityImageid, bool showId)
        {
            var data = new FunctionalityXFunctionalityImageDataModel();
            data.FunctionalityXFunctionalityImageId = functionalityXFunctionalityImageid;
			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityImageDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                if (!showId)
                {
                    txtFunctionalityXFunctionalityImageId.Text = item.FunctionalityXFunctionalityImageId.ToString();
                    oHistoryList.Setup(PrimaryEntity, functionalityXFunctionalityImageid, PrimaryEntityKey);
                }
                else
                {
                    txtFunctionalityXFunctionalityImageId.Text = String.Empty;
                }


                txtFunctionalityId.Text                 = item.FunctionalityId.ToString();
                txtFunctionalityImageId.Text            = item.FunctionalityImageId.ToString();
                txtKeyString.Text                       = item.KeyString.ToString();
                txtTitle.Text                           = item.Title.ToString();
                txtDescription.Text                     = item.Description;
                txtCreatedDate.Text                     = DateTime.ParseExact(item.CreatedDate.ToString(), "yyyyMMdd", DateTimeFormatInfo.InvariantInfo).ToString(SessionVariables.UserDateFormat);
                txtUpdatedDate.Text                     = DateTime.ParseExact(item.UpdatedDate.ToString(), "yyyyMMdd", DateTimeFormatInfo.InvariantInfo).ToString(SessionVariables.UserDateFormat);
                txtSortOrder.Text                       = item.SortOrder.ToString();
                txtCreatedBy.Text                       = item.CreatedBy.ToString();
                txtUpdatedBy.Text                       = item.UpdatedBy;

                drpFunctionalityList.SelectedValue      = item.FunctionalityId.ToString();
                drpFunctionalityImageList.SelectedValue = item.FunctionalityImageId.ToString();


                oUpdateInfo.LoadText(DateTime.Parse(item.UpdatedDate.ToString()), item.UpdatedBy, item.LastAction);
            }
            else
            {
                txtFunctionalityId.Text = String.Empty;
                txtFunctionalityImageId.Text = String.Empty;
                txtKeyString.Text = String.Empty;
                txtTitle.Text = String.Empty;
                txtDescription.Text = String.Empty;
                txtSortOrder.Text = String.Empty;
                txtCreatedBy.Text = String.Empty;
                txtCreatedDate.Text = String.Empty;
                txtUpdatedBy.Text = String.Empty;
                txtUpdatedDate.Text = String.Empty;

                drpFunctionalityList.SelectedValue = "-1";
                drpFunctionalityImageList.SelectedValue = "-1";


            }
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetupDropdown();

                //CalendarExtenderCreatedDate.Format = SessionVariables.UserDateFormat;
                lblUserDateTimeFormat.Text = "Date Format: " + SessionVariables.UserDateFormat;

                //CalendarExtenderUpdatedDate.Format = SessionVariables.UserDateFormat;
                lblUserDateTimeFormat2.Text = "Date Format: " + SessionVariables.UserDateFormat;
            }
            var isTesting = SessionVariables.IsTesting;
            txtFunctionalityXFunctionalityImageId.Visible = isTesting;
            lblFunctionalityXFunctionalityImageId.Visible = isTesting;
        }


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey = "FunctionalityXFunctionalityImage";
            FolderLocationFromRoot = "/Shared/QualityAssurance";
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityXFunctionalityImage;

            // set object variable reference            
            PlaceHolderCore = dynFunctionalityImageId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
        }


        protected void drpFunctionalityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFunctionalityId.Text = drpFunctionalityList.SelectedItem.Value;
        }

        protected void drpFunctionalityImageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFunctionalityImageId.Text = drpFunctionalityImageList.SelectedItem.Value;
        }

        #endregion

    }
}