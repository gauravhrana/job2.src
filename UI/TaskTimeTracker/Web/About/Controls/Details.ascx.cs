using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.ReleaseLog;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.About.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadReleaseNotes();
            }
        }
        private void LoadReleaseNotes()
        {



			var dtReleaseLog = Framework.Components.ReleaseLog.ReleaseLogDataManager.GetList(SessionVariables.RequestProfile);
            rptrReleaseLog.DataSource = dtReleaseLog;
            rptrReleaseLog.DataBind();

            FillInnerRepeater();
        }

        protected void FillInnerRepeater()
        {

			var dtReleaseLog = Framework.Components.ReleaseLog.ReleaseLogDataManager.GetList(SessionVariables.RequestProfile);
            for (var i = 0; i < dtReleaseLog.Count; i++)
            {
                var releaselogid = dtReleaseLog[i].ReleaseLogId;
                var data = new ReleaseLogDetailDataModel();
                data.ReleaseLogId = releaselogid;
				var dtreleaselogdetails = Framework.Components.ReleaseLog.ReleaseLogDetailDataManager.GetDetails(data, SessionVariables.RequestProfile);

                Repeater rptr = (Repeater)rptrReleaseLog.Items[i].FindControl("rptrReleaseLogDetails");
                if (rptr != null)
                {
                    rptr.DataSource = dtreleaselogdetails;
                    rptr.DataBind();
                }
            }


        }
    }
}