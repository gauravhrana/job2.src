using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UrlAutoTester
{
    public partial class frmUrlTesterV1 : Form
    {

        #region Constructor
        
        public frmUrlTesterV1()
        {
            InitializeComponent();
        }


        #endregion

        #region Events

        private void btn_Click(object sender, EventArgs e)
        {
            var tagControlName = Convert.ToString(((Button)sender).Tag);
            if (!string.IsNullOrEmpty(tagControlName))
            {
                var objControl = this.Controls.Find(tagControlName, true);
                if (objControl != null && objControl.Length > 0)
                { 
                    var txtControl = (TextBox)objControl[0];
                    var url = txtControl.Text;
                    Process.Start(url);
                }
            }
        }
        
        #endregion

    }
}
