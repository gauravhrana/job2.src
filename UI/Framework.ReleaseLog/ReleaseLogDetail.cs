using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace Framework.UI.ReleaseLog.Desktop
{
    public partial class ReleaseLogDetail : UserControl
    {

        #region Variables

        int applicationId = Convert.ToInt32(ConfigurationManager.AppSettings["ApplicationId"]);
        int applicationUserId = Convert.ToInt32(ConfigurationManager.AppSettings["ApplicationUserId"]);

        string formMode = "insert";
        int? ReleaseLogId;

        #endregion

        #region Constructor

        public ReleaseLogDetail()
        {
            InitializeComponent();
        }

        public ReleaseLogDetail(int releaseLogId)
        {
            InitializeComponent();
            ReleaseLogId = releaseLogId;
        }

        #endregion

        #region Methods

        public void BindReleaseLogDetail()
        {
			var data = new Framework.Components.ReleaseLog.DomainModel.ReleaseLogDetail();
            data.ReleaseLogId = ReleaseLogId;
            var dt = Framework.Components.ReleaseLog.ReleaseLogDetail.Search(data, applicationUserId);
            ultraGridReleaseLogDetail.DataSource = dt;
            if (ultraGridReleaseLogDetail.DisplayLayout.Bands[0].Columns["ReleaseLog"] != null)
            {
                var dtReleaseLog = Framework.Components.ReleaseLog.ReleaseLog.GetList(applicationUserId);
                var layout = ultraGridReleaseLogDetail.DisplayLayout;
                // Create a ValueList. 
                var vl = layout.ValueLists.Add("ReleaseLogValues");

                foreach (DataRow dr in dtReleaseLog.Rows)
                {
                    vl.ValueListItems.Add(Convert.ToString(dr[Framework.Components.ReleaseLog.ReleaseLog.DataColumns.ReleaseLogId]), Convert.ToString(dr[Framework.Components.ReleaseLog.ReleaseLog.DataColumns.Name]));
                }

                // Attach the ValueList to the grid column.
                ultraGridReleaseLogDetail.DisplayLayout.Bands[0].Columns["ReleaseLog"].ValueList = vl;
            }
        }

        public void BindReleaseLog()
        {
            var dt = Framework.Components.ReleaseLog.ReleaseLog.GetList(applicationUserId);
            cmbReleaseLog.DataSource = dt;
            cmbReleaseLog.ValueMember = "ReleaseLogId";
            cmbReleaseLog.DisplayMember = "Name";
            ultraGridReleaseLogDetail.DataSource = dt;
        }

        private bool CheckFormValidation()
        {
            if (string.IsNullOrEmpty(cmbReleaseLog.Text))
            {
                MessageBox.Show("Please Select Release Log");
                cmbReleaseLog.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtItemNo.Text))
            {
                MessageBox.Show("Please Enter Item No");
                txtItemNo.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtRequestedBy.Text))
            {
                MessageBox.Show("Please Enter Requested By");
                txtRequestedBy.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                MessageBox.Show("Please Enter Description");
                txtDescription.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtPrimaryDeveloper.Text))
            {
                MessageBox.Show("Please Enter Primary Developer");
                txtPrimaryDeveloper.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtRequestedDate.Text))
            {
                MessageBox.Show("Please Enter Requested Date");
                txtRequestedDate.Focus();
                return false;
            }
            return true;
        }

        private void SetControlState(bool isEnabled)
        {
            cmbReleaseLog.Enabled = isEnabled;
            txtItemNo.Enabled = isEnabled;
            txtDescription.Enabled = isEnabled;
            txtSortOrder.Enabled = isEnabled;
            txtRequestedDate.Enabled = isEnabled;
            txtRequestedBy.Enabled = isEnabled;
            txtPrimaryDeveloper.Enabled = isEnabled;
            btnSave.Enabled = isEnabled;
            btnDelete.Enabled = isEnabled;
        }

        private void ClearControls()
        {
            txtReleaseLogDetailId.Text = string.Empty;
            cmbReleaseLog.Text = string.Empty;
            txtItemNo.Text = string.Empty;
            txtPrimaryDeveloper.Text = string.Empty;
            txtRequestedBy.Text = string.Empty;
            txtRequestedDate.Text = string.Empty;
            txtSortOrder.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }

        #endregion

        #region Events

        private void ReleaseLogDetail_Load(object sender, EventArgs e)
        {
            BindReleaseLog();
            BindReleaseLogDetail();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckFormValidation())
            {
				var data = new Framework.Components.ReleaseLog.DomainModel.ReleaseLogDetail();

                data.ReleaseLogId = Convert.ToInt32(cmbReleaseLog.SelectedValue);
                data.ItemNo = Convert.ToInt32(txtItemNo.Text.Trim());
                data.RequestedBy = txtRequestedBy.Text.Trim();
                data.PrimaryDeveloper = txtPrimaryDeveloper.Text.Trim();
                data.RequestedDate = Convert.ToInt32(txtRequestedDate.Text.Trim());
                data.SortOrder = Convert.ToInt32(txtSortOrder.Text.Trim());
                data.Description = txtDescription.Text.Trim();

                if (formMode == "edit")
                {
                    data.ReleaseLogDetailId = Convert.ToInt32(txtReleaseLogDetailId.Text.Trim());
                    Framework.Components.ReleaseLog.ReleaseLogDetail.Update(data, applicationUserId);
                }
                else
                {
                    Framework.Components.ReleaseLog.ReleaseLogDetail.Create(data, applicationUserId);
                }

                MessageBox.Show("Record Saved Successfully");

                BindReleaseLogDetail();

                splitContainer1.Panel1Collapsed = false;
                splitContainer1.Panel2Collapsed = true;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            formMode = "edit";
            SetControlState(true);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
			var data = new Framework.Components.ReleaseLog.DomainModel.ReleaseLogDetail();
            data.ReleaseLogDetailId = Convert.ToInt32(txtReleaseLogDetailId.Text.Trim());
            Framework.Components.ReleaseLog.ReleaseLogDetail.Delete(data, applicationUserId);

            MessageBox.Show("Record Deleted Successfully");

            BindReleaseLogDetail();

            splitContainer1.Panel1Collapsed = false;
            splitContainer1.Panel2Collapsed = true;
        }

        private void btnCloseEdit_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = false;
            splitContainer1.Panel2Collapsed = true;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            ClearControls();
            formMode = "insert";

            SetControlState(true);
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;

            splitContainer1.Panel2Collapsed = false;
            splitContainer1.Panel1Collapsed = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (this.ParentForm != null)
            {
                this.ParentForm.Close();
            }
        }

        private void ultraGridReleaseLogDetail_ClickCellButton(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
        {
            if (e.Cell != null)
            {
                if (e.Cell.Text == "Detail View")
                {
					var data = new Framework.Components.ReleaseLog.DomainModel.ReleaseLogDetail();
                    data.ReleaseLogDetailId = Convert.ToInt32(e.Cell.Row.Cells["ReleaseLogDetailId"].Value); ;
                    var dt = Framework.Components.ReleaseLog.ReleaseLogDetail.GetDetails(data, applicationUserId);
                    if (dt.Rows.Count > 0)
                    {
                        txtReleaseLogDetailId.Text = Convert.ToString(dt.Rows[0][Framework.Components.ReleaseLog.DomainModel.ReleaseLogDetail.DataColumns.ReleaseLogDetailId]);
						cmbReleaseLog.SelectedValue = Convert.ToString(dt.Rows[0][Framework.Components.ReleaseLog.DomainModel.ReleaseLogDetail.DataColumns.ReleaseLogId]);
						txtItemNo.Text = Convert.ToString(dt.Rows[0][Framework.Components.ReleaseLog.DomainModel.ReleaseLogDetail.DataColumns.ItemNo]);
						txtDescription.Text = Convert.ToString(dt.Rows[0][Framework.Components.ReleaseLog.DomainModel.ReleaseLogDetail.DataColumns.Description]);
						txtSortOrder.Text = Convert.ToString(dt.Rows[0][Framework.Components.ReleaseLog.DomainModel.ReleaseLogDetail.DataColumns.SortOrder]);
						txtRequestedDate.Text = Convert.ToString(dt.Rows[0][Framework.Components.ReleaseLog.DomainModel.ReleaseLogDetail.DataColumns.RequestedDate]);
						txtRequestedBy.Text = Convert.ToString(dt.Rows[0][Framework.Components.ReleaseLog.DomainModel.ReleaseLogDetail.DataColumns.RequestedBy]);
						txtPrimaryDeveloper.Text = Convert.ToString(dt.Rows[0][Framework.Components.ReleaseLog.DomainModel.ReleaseLogDetail.DataColumns.PrimaryDeveloper]);
                        SetControlState(false);
                        btnEdit.Enabled = true;

                        splitContainer1.Panel2Collapsed = false;
                        splitContainer1.Panel1Collapsed = true;
                    }

                }
            }
        }

        private void ultraGridReleaseLogDetail_AfterExitEditMode(object sender, EventArgs e)
        {
            if (ultraGridReleaseLogDetail.ActiveCell != null)
            {
				var data = new Framework.Components.ReleaseLog.DomainModel.ReleaseLogDetail();
                try
                {
                    data.ReleaseLogId = Convert.ToInt32(ultraGridReleaseLogDetail.ActiveCell.Row.Cells["ReleaseLog"].Value);
                }
                catch { }
                data.ItemNo             = Convert.ToInt32(ultraGridReleaseLogDetail.ActiveCell.Row.Cells["ItemNo"].Text);
                data.RequestedBy        = ultraGridReleaseLogDetail.ActiveCell.Row.Cells["RequestedBy"].Text;
                data.PrimaryDeveloper   = ultraGridReleaseLogDetail.ActiveCell.Row.Cells["PrimaryDeveloper"].Text;
                data.RequestedDate      = Convert.ToInt32(ultraGridReleaseLogDetail.ActiveCell.Row.Cells["RequestedDate"].Text);
                data.SortOrder          = Convert.ToInt32(ultraGridReleaseLogDetail.ActiveCell.Row.Cells["SortOrder"].Text);
                data.Description        = ultraGridReleaseLogDetail.ActiveCell.Row.Cells["Description"].Text;
                data.ReleaseLogDetailId = Convert.ToInt32(ultraGridReleaseLogDetail.ActiveCell.Row.Cells["ReleaseLogDetailId"].Text);
                Framework.Components.ReleaseLog.ReleaseLogDetail.Update(data, applicationUserId);
            }
        }

        #endregion


    }
}
