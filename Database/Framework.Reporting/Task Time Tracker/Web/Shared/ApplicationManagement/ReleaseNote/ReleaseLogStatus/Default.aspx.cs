using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Text;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Framework.Components.ReleaseLog;
using DataModel.Framework.ReleaseLog;

namespace Shared.UI.Web.ApplicationManagement.ReleaseLogStatus
{
	public partial class Default : PageDefault

	{
		protected override DataTable GetData()
		{
			var dt = ReleaseLogStatusDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
			return dt;
		}		   

		[System.Web.Script.Services.ScriptMethod()]

		[System.Web.Services.WebMethod]

		public static string[] GetModuleNames(string prefixText, int count)
		{
			var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleDataManager.GetList(SessionVariables.RequestProfile);

			var uerNames = new List<string>();

			for (var i = 0; i < dt.Rows.Count; i++)
			{
				uerNames.Add(dt.Rows[i][2].ToString());
			}

			return uerNames.ToArray();
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity			 = Framework.Components.DataAccess.SystemEntity.TaskXActivityInstance;
			PrimaryEntityKey		 = "ReleaseLogStatus";
			PrimaryEntityIdColumn	 = "ReleaseLogStatusId";

			MasterPageCore			 = Master;
			SubMenuCore				 = Master.SubMenuObject;
			BreadCrumbObject		 = Master.BreadCrumbObject;

			SearchFilterCore		 = oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey); ;
			GroupListCore			 = oGroupList;

            IsDynamicSearchControl	 = true;

			VisibilityManagerCore	 = oVC;
		}		

	}
}