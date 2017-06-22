using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace Framework.UI.ReleaseLog.Desktop
{
    public partial class frmDetailReleaseLog : Form
    {

        #region Variables

        int applicationId = Convert.ToInt32(ConfigurationManager.AppSettings["ApplicationId"]);
        int applicationUserId = Convert.ToInt32(ConfigurationManager.AppSettings["ApplicationUserId"]);
        public string formMode = "insert";

        #endregion

        #region Constructor

        public frmDetailReleaseLog()
        {
            InitializeComponent();
        }

        public frmDetailReleaseLog(int releaseLogId)
        {
            InitializeComponent();
            BindReleaseLog(releaseLogId);
        }

        #endregion

        #region Methods

        private void BindReleaseLog(int releaseLogId)
        {
            var data = new Framework.Components.ReleaseLog.ReleaseLog.Data();
            data.ReleaseLogId = releaseLogId;
            var dt = Framework.Components.ReleaseLog.ReleaseLog.GetDetails(data, applicationUserId);
            if (dt.Rows.Count > 0)
            {
                txtReleaseLogId.Text = Convert.ToString(dt.Rows[0][Framework.Components.ReleaseLog.ReleaseLog.DataColumns.ReleaseLogId]);
                txtName.Text = Convert.ToString(dt.Rows[0][Framework.Components.ReleaseLog.ReleaseLog.DataColumns.Name]);
                txtVersionNo.Text = Convert.ToString(dt.Rows[0][Framework.Components.ReleaseLog.ReleaseLog.DataColumns.VersionNo]);
                txtDescription.Text = Convert.ToString(dt.Rows[0][Framework.Components.ReleaseLog.ReleaseLog.DataColumns.Description]);
                txtSortOrder.Text = Convert.ToString(dt.Rows[0][Framework.Components.ReleaseLog.ReleaseLog.DataColumns.SortOrder]);
                dtmReleaseDate.Value = Convert.ToDateTime(dt.Rows[0][Framework.Components.ReleaseLog.ReleaseLog.DataColumns.ReleaseDate]);
                SetControlState(false);
                btnEdit.Enabled = true;
            }
        }

        private void SetControlState(bool isEnabled)
        {
            txtName.Enabled = isEnabled;
            txtVersionNo.Enabled = isEnabled;
            txtDescription.Enabled = isEnabled;
            txtSortOrder.Enabled = isEnabled;
            dtmReleaseDate.Enabled = isEnabled;
            btnSave.Enabled = isEnabled;
            btnDelete.Enabled = isEnabled;
        }

        private bool CheckFormValidation()
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Please Enter Name");
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtVersionNo.Text))
            {
                MessageBox.Show("Please Enter Version No");
                txtVersionNo.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtSortOrder.Text))
            {
                MessageBox.Show("Please Enter Sort Order");
                txtSortOrder.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                MessageBox.Show("Please Enter Description");
                txtDescription.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(Convert.ToString(dtmReleaseDate.Value)))
            {
                MessageBox.Show("Please Enter Release Date");
                dtmReleaseDate.Focus();
                return false;
            }
            return true;
        }

        #endregion

        #region Events

        private void frmDetailReleaseLog_Load(object sender, EventArgs e)
        {
            txtName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckFormValidation())
            {
                var data = new Framework.Components.ReleaseLog.ReleaseLog.Data();

                data.Name = txtName.Text.Trim();
                data.VersionNo = txtVersionNo.Text.Trim();
                data.ReleaseDate = Convert.ToDateTime(dtmReleaseDate.Value);
                data.SortOrder = Convert.ToInt32(txtSortOrder.Text.Trim());
                data.Description = txtDescription.Text.Trim();

                if (formMode == "edit")
                {
                    data.ReleaseLogId = Convert.ToInt32(txtReleaseLogId.Text.Trim());
                    Framework.Components.ReleaseLog.ReleaseLog.Update(data, applicationUserId);
                }
                else
                {
                    Framework.Components.ReleaseLog.ReleaseLog.Create(data, applicationUserId);
                }

                MessageBox.Show("Record Saved Successfully");

                if (this.Owner != null)
                {
                    try
                    {
                        ((frmReleaseLog)(this.Owner)).BindReleaseLog();

                    }
                    catch { }
                }

                Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            SetControlState(true);
        }

        private void frmDetailReleaseLog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var data = new Framework.Components.ReleaseLog.ReleaseLog.Data();
            data.ReleaseLogId = Convert.ToInt32(txtReleaseLogId.Text.Trim());
            Framework.Components.ReleaseLog.ReleaseLog.Delete(data, applicationUserId);

            MessageBox.Show("Record Deleted Successfully");

            if (this.Owner != null)
            {
                try
                {
                    ((frmReleaseLog)(this.Owner)).BindReleaseLog();

                }
                catch { }
            }

            Close();
        }

        #endregion

    }
}
