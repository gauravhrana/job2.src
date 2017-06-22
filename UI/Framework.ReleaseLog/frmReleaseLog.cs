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
    public partial class frmReleaseLog : Form
    {

        #region Variables

        int applicationId = Convert.ToInt32(ConfigurationManager.AppSettings["ApplicationId"]);
        int applicationUserId = Convert.ToInt32(ConfigurationManager.AppSettings["ApplicationUserId"]);

        #endregion

        #region Constructor

        public frmReleaseLog()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        public void BindReleaseLog()
        {
            var data = new Framework.Components.ReleaseLog.ReleaseLog.Data();
            var dt = Framework.Components.ReleaseLog.ReleaseLog.Search(data, applicationUserId);
            ultraGridReleaseLog.DataSource = dt;
        }

        #endregion

        #region Events

        private void frmReleaseLog_Load(object sender, EventArgs e)
        {
            BindReleaseLog();
        }

        private void frmReleaseLog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void ultraGridReleaseLog_ClickCellButton(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
        {
            if (e.Cell != null)
            {
                var releaseLogId = Convert.ToInt32(e.Cell.Row.Cells["ReleaseLogId"].Value);
                if (e.Cell.Text == "Detail View")
                {

                    var detail = new frmDetailReleaseLog(releaseLogId);
                    detail.formMode = "edit";
                    detail.Owner = this;
                    detail.ShowDialog();
                }
                else
                {
                    var detail = new frmReleaseLogDetail(releaseLogId);
                    detail.Owner = this;
                    detail.ShowDialog();
                }
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            var detail = new frmDetailReleaseLog();
            detail.Owner = this;
            detail.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ultraGridReleaseLog_AfterExitEditMode(object sender, EventArgs e)
        {
            if (ultraGridReleaseLog.ActiveCell != null)
            {
                var data = new Framework.Components.ReleaseLog.ReleaseLog.Data();

                data.Name = ultraGridReleaseLog.ActiveCell.Row.Cells["Name"].Text;
                data.VersionNo = ultraGridReleaseLog.ActiveCell.Row.Cells["VersionNo"].Text;
                data.ReleaseDate = Convert.ToDateTime(ultraGridReleaseLog.ActiveCell.Row.Cells["ReleaseDate"].Value);
                data.SortOrder = Convert.ToInt32(ultraGridReleaseLog.ActiveCell.Row.Cells["SortOrder"].Text);
                data.Description = ultraGridReleaseLog.ActiveCell.Row.Cells["Description"].Text;
                data.ReleaseLogId = Convert.ToInt32(ultraGridReleaseLog.ActiveCell.Row.Cells["ReleaseLogId"].Text);
                Framework.Components.ReleaseLog.ReleaseLog.Update(data, applicationUserId);
            }
        }

        #endregion

    }
}
