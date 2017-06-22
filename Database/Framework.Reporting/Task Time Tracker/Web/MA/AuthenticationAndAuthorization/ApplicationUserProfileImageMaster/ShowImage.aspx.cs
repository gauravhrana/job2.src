using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUserProfileImageMaster
{
    public partial class ShowImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int imageId = Convert.ToInt32(Request.QueryString["imageid"]);
            var data = new ApplicationUserProfileImageMasterDataModel();

            data.ApplicationUserProfileImageMasterId = imageId;
			var dt = Framework.Components.ApplicationUser.ApplicationUserProfileImageMasterDataManager.GetDetails(data, SessionVariables.RequestProfile);
            if (dt.Rows.Count > 0)
            {
                var imageData = ((byte[])(dt.Rows[0][ApplicationUserProfileImageMasterDataModel.DataColumns.Image]));

                Response.Clear();
                Response.ContentType = "image/pjpeg";
                Response.BinaryWrite(imageData);
                Response.End();
            }
        }
    }
}