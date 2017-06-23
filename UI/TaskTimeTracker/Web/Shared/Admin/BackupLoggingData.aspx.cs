using Framework.Components.LogAndTrace;
using Shared.WebCommon.UI.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shared.UI.Web.Admin
{
    public partial class BackupLoggingData : Framework.UI.Web.BaseClasses.PageBasePage
    {

        public IFormatProvider DateFormatInfo { get; set; }

        public string ConvertDateTimeFormat
        {
            get
            {
                return DateTimeHelper.CovertDateFormatToJavascript();
            }
        }

        public void RefreshCounts()
        {
            //lblLog4NetCount.Text = " (" + Log4NetDataManager.GetRecordCount().ToString() + ")";
            //lblUserLoginCount.Text = " (" + UserLoginDataManager.GetRecordCount().ToString() + ")";
            //lblUserLoginHistoryCount.Text = " (" + UserLoginHistoryDataManager.GetRecordCount().ToString() + ")";

            lstEntities.Items.Clear();
            var dtEntityCounts = BackupLogginDataManager.GetTableRecords();

            foreach (DataRow row in dtEntityCounts.Rows)
            {
                var entityName = row["Name"].ToString();
                var entityRecordCount = int.Parse(row["rowcnt"].ToString());


                var entityItemText = entityName + " (" + entityRecordCount.ToString("0,0") + ")";

                lstEntities.Items.Add(entityItemText);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDate.Text = DateTime.Now.AddDays(-1).ToString(SessionVariables.UserDateFormat);
                RefreshCounts();
            }
            lblUserDateTimeFormat.Text = "( " + SessionVariables.UserDateFormat + ")";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDate.Text.Trim()))
            {
                var selectedEntities = new List<string>();
                foreach (ListItem item in lstBackupEntity.Items)
                {
                    if (item.Selected)
                    {
                        selectedEntities.Add(item.Text);
                    }
                }
                if (selectedEntities.Count > 0)
                {
                    var backupDate = DateTimeHelper.FromUserDateFormatToDate(txtDate.Text.Trim());

                    foreach (var entity in selectedEntities)
                    {
                        var sourceEntity = entity;
                        if (sourceEntity == "Log4Net")
                        {
                            Log4NetDataManager.BackupLog4Net(backupDate.Value);
                        }
                        else if (sourceEntity == "UserLogin")
                        {
                            UserLoginDataManager.BackupUserLogin(backupDate.Value);
                        }
                        else if (sourceEntity == "UserLoginHistory")
                        {
                            UserLoginHistoryDataManager.BackupUserLoginHistory(backupDate.Value);
                        }

                        var backupTableName = sourceEntity + "_bkp_" + backupDate.Value.ToString("yyyyMMdd");
                        lblMessage.Text += "<br>Moved Records from " + sourceEntity + " to " + backupTableName;
                    }

                    RefreshCounts();
                }
                else
                {
                    lblMessage.Text = "No Entity Selected";
                }
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshCounts();
        }

        protected void btnGetCount_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDate.Text.Trim()))
            {
                var selectedEntities = new List<string>();
                foreach (ListItem item in lstBackupEntity.Items)
                {
                    if (item.Selected)
                    {
                        selectedEntities.Add(item.Text);
                    }
                }
                if (selectedEntities.Count > 0)
                {
                    var backupDate = DateTimeHelper.FromUserDateFormatToDate(txtDate.Text.Trim());
                    lblMessage.Text += "<br><b>Status on " + txtDate.Text.ToString() + "</b>";

                    foreach (var entity in selectedEntities)
                    {
                        var sourceEntity = entity;

                        var totalCnt = BackupLogginDataManager.GetRecordCount(sourceEntity, null);
                        var dateBasedCnt = BackupLogginDataManager.GetRecordCount(sourceEntity, backupDate);

                        lblMessage.Text += "<br>" + sourceEntity + " Counts: " + dateBasedCnt.ToString("0,0") + "/" + totalCnt.ToString("0,0");
                    }
                }
            }
            //
        }

    }
}