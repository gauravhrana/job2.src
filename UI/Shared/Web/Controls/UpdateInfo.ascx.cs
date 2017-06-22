using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Configuration;

namespace Shared.UI.Web.Controls
{
    public partial class UpdateInfo : System.Web.UI.UserControl
    {

        #region Methods

        private string GetActivityTimeText(DateTime createdDate, double appTimeZoneDifference)
        {
            TimeSpan ts = DateTime.UtcNow.AddHours(appTimeZoneDifference) - createdDate;
            var addTimeToAlert = string.Empty;

            if (ts.Days >= 31 && ts.Days <= 365)
            {
                //int monthsAgo = 0;
                addTimeToAlert = " About " + (ts.Days / 31) + " months ago ";
            }

            else if (ts.Days < 31 && ts.Days > 0)
            {
                addTimeToAlert = " About " + ts.Days + " days ago";
            }

            else if (ts.Hours < 24 && ts.Hours > 0)
            {
                addTimeToAlert = " About " + ts.Hours + " hours ago";
            }

            else if (ts.Minutes < 60 && ts.Minutes > 0)
            {
                addTimeToAlert = " About " + ts.Minutes + " minutes ago";
            }

            else if (ts.Seconds < 60 && ts.Seconds > 0)
            {
                addTimeToAlert = " About " + ts.Seconds + " seconds ago";
            }

            return addTimeToAlert;

        }

        public void LoadText(DataRow dr)
        {
            try
            {
                var userUpdateinfoStyle = ApplicationCommon.GetUserPreferenceByKey(ApplicationCommon.UpdateInfoStyle);

                if (userUpdateinfoStyle == "UpdateInfoStyle1")
                {
                    updateStyle1.Visible = true;
                    lblUpdatedDate1.Text = ApplicationCommon.GetUserTimeZoneDateAsString(dr[ApplicationCommon.Columns.UpdatedDate]);
                    lblUpdatedBy1.Text = Convert.ToString(dr[ApplicationCommon.Columns.UpdatedBy]);
                    lblLastAction1.Text = Convert.ToString(dr[ApplicationCommon.Columns.LastAction]);
                }
                else if (userUpdateinfoStyle == "UpdateInfoStyle2")
                {
                    updateStyle2.Visible = true;
                    lblUpdatedDate2.Text = ApplicationCommon.GetUserTimeZoneDateAsString(dr[ApplicationCommon.Columns.UpdatedDate]);
                    lblUpdatedBy2.Text = Convert.ToString(dr[ApplicationCommon.Columns.UpdatedBy]);
                    lblLastAction2.Text = Convert.ToString(dr[ApplicationCommon.Columns.LastAction]);
                }
                else
                {
                    updateStyle3.Visible = true;
                    double appTimeZoneDifference = 0;
                    try
                    {
                        appTimeZoneDifference = Convert.ToDouble(ConfigurationManager.AppSettings["UTCTimeZoneDifference"]);
                    }
                    catch { }

                    lblUpdatedDate3.Text = " - " + GetActivityTimeText(Convert.ToDateTime(dr[ApplicationCommon.Columns.UpdatedDate]), appTimeZoneDifference);
                    lblUpdatedBy3.Text = Convert.ToString(dr[ApplicationCommon.Columns.UpdatedBy]);
                    lblLastAction3.Text = Convert.ToString(dr[ApplicationCommon.Columns.LastAction]);

                    if (lblLastAction3.Text == "Update")
                    {
                        lblLastAction3.Text = "updated";
                    }
                    else
                    {
                        lblLastAction3.Text = "inserted";
                    }
                }
            }
            catch { }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #endregion

    }
}