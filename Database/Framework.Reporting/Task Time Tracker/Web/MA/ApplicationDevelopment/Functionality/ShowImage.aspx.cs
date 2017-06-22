using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationContainer.UI.Web.BaseUI;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.Functionality
{
	public partial class ShowImage : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			int imageId = Convert.ToInt32(Request.QueryString["imageid"]);

			var data = new FunctionalityDataModel();

			data.FunctionalityId = imageId;
			var dt = FunctionalityDataManager.GetDetails(data, SessionVariables.RequestProfile);

			if (dt.Rows.Count > 0)
			{
				if (!dt.Rows[0][FunctionalityImageDataModel.DataColumns.Image].Equals(DBNull.Value))
				{
					var imageData = ((byte[])(dt.Rows[0][FunctionalityImageDataModel.DataColumns.Image]));

					Response.Clear();
					Response.ContentType = "image/pjpeg";
					Response.BinaryWrite(imageData);
					Response.End();
				}

			}
		}
	}
}