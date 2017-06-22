using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace TaskTimeTracker.UI.Web.Prototype.Controllers
{
	public class ReleaseNotesController: ApiController
	{
		public string[] GetModuleNames(string prefixText, int count)
		{
			var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleDataManager.GetList(prefixText, 100012);

			List<string> UserNames = new List<string>();
			for (var i = 0; i < dt.Rows.Count; i++)
			{
				UserNames.Add(dt.Rows[i][2].ToString());
			}
			return UserNames.ToArray();

		}
	}
}