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
    public partial class frmReleaseLogDetail : Form
    {

        #region Variables
        
        int? ReleaseLogId;

        #endregion

        #region Constructor

        public frmReleaseLogDetail()
        {
            InitializeComponent();
        }

        public frmReleaseLogDetail(int releaseLogId)
        {
            InitializeComponent();
            ReleaseLogId = releaseLogId;
        }

        #endregion

        #region Events

        private void frmReleaseLogDetail_Load(object sender, EventArgs e)
        {
            ReleaseLogDetail detail = null;
            if (ReleaseLogId != null)
            {
                detail = new ReleaseLogDetail(ReleaseLogId.Value);
            }
            else
            {
                detail = new ReleaseLogDetail();
            }
            this.Controls.Add(detail);
            detail.Dock = DockStyle.Fill;
        }

        #endregion


    }
}
