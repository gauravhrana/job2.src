using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.ReleaseLog;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.About.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {
        #region properties

        public int? ReleaseLogId
        {
            get
            {
                if (txtReleaseLogId.Text == "")
                {
                    return null;
                }
                else
                {
                    return int.Parse(txtReleaseLogId.Text);
                }
            }
        }

        public string Name
        {
            get
            {
                return txtName.Text;
            }
        }
        public string VersionNo
        {
            get
            {
                return txtVersionNo.Text;
            }
        }
        public string ReleaseDate
        {
            get
            {
                return txtReleaseDate.Text;
            }
        }

        public string Description
        {
            get
            {
                return Framework.Components.DefaultDataRules.CheckAndGetDescription(txtName.Text, txtDescription.Text);
            }
        }

        public int SortOrder
        {
            get
            {
                return Framework.Components.DefaultDataRules.CheckAndGetSortOrder(txtSortOrder.Text);
            }
        }

        //protected override string ValidationConfigFile
        //{
        //    get
        //    {
        //        return Server.MapPath("~/Shared/ApplicationManagement/ReleaseLog/Controls/Validation.xml"); //"R:\ReleaseLogs\Task Time Tracker\Dev\UI\Web\ValidationDataFile\dvc.xml";
        //    }
        //}

        #endregion properties

        #region private methods

        public override void SetId(int setId, bool chkReleaseLogId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkReleaseLogId);
            txtReleaseLogId.Enabled = chkReleaseLogId;
            txtDescription.Enabled = !chkReleaseLogId;
            txtName.Enabled = !chkReleaseLogId;
            txtSortOrder.Enabled = !chkReleaseLogId;
        }

        public void LoadData(int ReleaseLogId, bool showId)
        {
            var data = new ReleaseLogDataModel();
            var AuditId = SessionVariables.RequestProfile.AuditId;
            data.ReleaseLogId = ReleaseLogId;
			var oReleaseLogTable = Framework.Components.ReleaseLog.ReleaseLogDataManager.GetDetails(data, SessionVariables.RequestProfile);

            if (oReleaseLogTable.Rows.Count == 1)
            {
                var row = oReleaseLogTable.Rows[0];

                if (!showId)
                {
                    txtReleaseLogId.Text = Convert.ToString(row["ReleaseLogId"]);
                }
                else
                {
                    txtReleaseLogId.Text = String.Empty;
                }

                txtDescription.Text		= Convert.ToString(row["Description"]);
                txtName.Text			= Convert.ToString(row["Name"]);
                txtDescription.Text		= Convert.ToString(row["VersionNo"]);
                txtName.Text			= Convert.ToString(row["ReleaseDate"]);
                txtSortOrder.Text		= Convert.ToString(row["SortOrder"]);
            }
            else
            {
                txtReleaseLogId.Text	= String.Empty;
                txtReleaseDate.Text		= String.Empty;
                txtVersionNo.Text		= String.Empty;
                txtDescription.Text		= String.Empty;
                txtName.Text			= String.Empty;
                txtSortOrder.Text		= String.Empty;
            }
        }
        private void InsertDetails()
        {
            var data = new ReleaseLogDetailDataModel();
            var AuditId = SessionVariables.RequestProfile.AuditId;
            data.ReleaseLogDetailId = int.Parse(txtReleaseLogDetailId.Text);
            data.ReleaseLogId = int.Parse(txtReleaseLogId.Text);
            data.ItemNo = int.Parse(txtItemNo.Text);
            data.Description = txt_Description.Text;
            data.SortOrder = int.Parse(txt_SortOrder.Text);

			Framework.Components.ReleaseLog.ReleaseLogDetailDataManager.Save(data, SessionVariables.RequestProfile, "Insert");
            lblMessage.Text = "Record Saved";
            lblMessage.Visible = true;
            txtReleaseLogDetailId.Text = String.Empty;
            txtItemNo.Text = String.Empty;
            txt_Description.Text = String.Empty;
            txt_SortOrder.Text = String.Empty;
        }

        #endregion

        #region Events
        protected void btnInsert_Click(object sender, EventArgs e)
        {
           
                pnlDetails.Visible = true;
           
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            InsertDetails();

        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            txtReleaseLogId.Visible = isTesting;
            lblReleaseLogId.Visible = isTesting;
        }

        

        #endregion
    }
}