using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Configuration;
using DataModel.Framework.DataAccess;

namespace Shared.UI.Web.Controls
{
    public partial class UpdateInfo : BaseControl
    {

        #region Methods

        private string GetActivityTimeText(DateTime createdDate, double appTimeZoneDifference)
        {
            var ts = DateTime.UtcNow.AddHours(appTimeZoneDifference) - createdDate;
            var addTimeToAlert = String.Empty;

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

		public void LoadText(DateTime? updatedDate, string updatedBy, string lastAction)
	    {
			var userUpdateinfoStyle = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.UpdateInfoStyle);

			if (userUpdateinfoStyle == "UpdateInfoStyle1")
			{
				updateStyle1.Visible = true;
                //lblUpdatedDate1.Text = updatedDate == null ? String.Empty : PreferenceUtility.GetUserTimeZoneDateAsString(updatedDate);
				lblUpdatedBy1.Text = updatedBy;
				lblLastAction1.Text = lastAction;
			}
			else if (userUpdateinfoStyle == "UpdateInfoStyle2")
			{
				updateStyle2.Visible = true;
				//lblUpdatedDate2.Text = updatedDate == null ? String.Empty : PreferenceUtility.GetUserTimeZoneDateAsString(updatedDate);
				lblUpdatedBy2.Text = updatedBy;
				lblLastAction2.Text = lastAction;
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

				lblUpdatedDate3.Text = " - " + (updatedDate == null ? String.Empty : GetActivityTimeText((DateTime)updatedDate, appTimeZoneDifference));
				lblUpdatedBy3.Text = updatedBy;
				lblLastAction3.Text = lastAction;

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

	    public void LoadText(DataRow dr)
        {
            try
            {
	            var updatedDate = Convert.ToDateTime(dr[DataModel.Framework.DataAccess.BaseDataModel.BaseDataColumns.UpdatedDate]);
                var updatedBy = Convert.ToString(dr[DataModel.Framework.DataAccess.BaseDataModel.BaseDataColumns.UpdatedBy]);
                var lastAction = Convert.ToString(dr[DataModel.Framework.DataAccess.BaseDataModel.BaseDataColumns.LastAction]);

	            LoadText(updatedDate, updatedBy, lastAction);

            }
            catch { }
        }

        public void LoadText(BaseDataModel obj)
        {
            try
            {
                LoadText(obj.UpdatedDate, obj.UpdatedBy, obj.LastAction);
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