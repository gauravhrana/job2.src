using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.ReportCategory.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {
        #region properties

        public int? ApplicationId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtApplication.Text.Trim());
                else
                    return int.Parse(drpApplication.SelectedItem.Value);
            }
            set
            {
                txtApplication.Text = (value == null) ? String.Empty : value.ToString();
            }

        }

        public int? CreatedByAuditId
        {
            get
            {
                return int.Parse(txtCreatedByAuditId.Text);
            }
            set
            {
                txtCreatedByAuditId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public int? ModifiedByAuditId
        {
            get
            {
                return int.Parse(txtModifiedByAuditId.Text);
            }
            set
            {
                txtModifiedByAuditId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public DateTime? CreatedDate
        {
            get
            {
                return DateTime.ParseExact(txtCreatedDate.Text, SessionVariables.UserDateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
                //return DateTime.Parse(txtCreatedDate.Text.Trim());
            }
            set
            {

                txtCreatedDate.Text = (value == null) ? String.Empty : value.Value.ToString(SessionVariables.UserDateFormat);
            }

        }

        public DateTime? ModifiedDate
        {
            get
            {
                DateTime dt = Convert.ToDateTime(txtModifiedDate.Text.Trim());
                return DateTime.ParseExact(dt.ToString(SessionVariables.UserDateFormat), SessionVariables.UserDateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
            }
            set
            {
                txtModifiedDate.Text = (value == null) ? String.Empty : value.Value.ToString(SessionVariables.UserDateFormat);
            }
        }

        #endregion

        #region methods

        public override int? Save(string action)
        {
            var data = new ReportCategoryDataModel();

            data.ReportCategoryId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;            
            data.SortOrder = SortOrder;            
            data.ApplicationId = ApplicationId;


            if (action == "Insert")
            {
				var dtReportCategory = Framework.Components.Core.ReportCategoryDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtReportCategory.Rows.Count == 0)
                {
					Framework.Components.Core.ReportCategoryDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                data.CreatedDate = CreatedDate;
                Framework.Components.Core.ReportCategoryDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.ReportCategoryId;
        }

        public override void SetId(int setId, bool chkReportCategoryId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkReportCategoryId);
            CoreSystemKey.Enabled = chkReportCategoryId;
            //txtDescription.Enabled = !chkReportCategoryId;
            //txtName.Enabled = !chkReportCategoryId;
            //txtSortOrder.Enabled = !chkReportCategoryId;
        }

        public void LoadData(int ReportCategoryId, bool showId)
        {

            Clear();

            var data = new ReportCategoryDataModel();
            data.ReportCategoryId = ReportCategoryId;

			var items = Framework.Components.Core.ReportCategoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            CreatedByAuditId = item.CreatedByAuditId;
            ModifiedByAuditId = item.ModifiedByAuditId;
            CreatedDate = item.CreatedDate;
            ModifiedDate = item.ModifiedDate;

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.ReportCategoryId;               
                ApplicationId = item.ApplicationId;                
                oHistoryList.Setup(PrimaryEntity, ReportCategoryId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new ReportCategoryDataModel();

            CreatedDate = data.CreatedDate;
            ModifiedDate = data.ModifiedDate;
            CreatedByAuditId = data.CreatedByAuditId;
            ModifiedByAuditId = data.ModifiedByAuditId;                       
            ApplicationId = data.ApplicationId;

            SetData(data);

        }

        public void SetData(ReportCategoryDataModel data)
        {
            SystemKeyId = data.ReportCategoryId;

            base.SetData(data);
        }

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;
			var Applicationdata = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(Applicationdata, drpApplication,
               StandardDataModel.StandardDataColumns.Name,
               BaseDataModel.BaseDataColumns.ApplicationId);

            if (isTesting)
            {
                drpApplication.AutoPostBack = true;
                if (drpApplication.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtApplication.Text.Trim()))
                    {
                        drpApplication.SelectedValue = txtApplication.Text;
                    }
                    else
                    {
                        txtApplication.Text = drpApplication.SelectedItem.Value;
                    }
                }
                txtApplication.Visible = false;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtApplication.Text.Trim()))
                {
                    drpApplication.SelectedValue = txtApplication.Text;
                }
            }
        }

        protected void drpApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtApplication.Text = drpApplication.SelectedItem.Value;
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            txtReportCategoryId.Visible = isTesting;
            lblReportCategoryId.Visible = SessionVariables.IsTesting;

            if (!IsPostBack)
            {
                SetupDropdown();
            }
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ReportCategory;
            PrimaryEntityKey = "ReportCategory";
            FolderLocationFromRoot = "ReportCategory";

            PlaceHolderCore = dynReportCategoryId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtReportCategoryId;
            CoreControlName = txtName;
            CoreControlDescriptionKendoEditor = txtDescription2;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}