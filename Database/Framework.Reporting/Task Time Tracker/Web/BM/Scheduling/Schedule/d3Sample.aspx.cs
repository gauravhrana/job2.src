using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.TimeTracking;
using System.IO;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.Scheduling.Schedule
{
	public partial class d3Sample : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			var UpdatedData = new DataTable();
            UpdatedData = ScheduleDataManager.GetSampleSearch();
			StreamWriter sw = new StreamWriter(HttpContext.Current.Server.MapPath("myfile.csv"));
			
			foreach (var col in UpdatedData.Columns)
			{
				sw.Write(col.ToString() + ",");
				
			}
			sw.WriteLine();

			for (int i = 0; i < UpdatedData.Rows.Count; i++)
			{
				DataRow row = UpdatedData.Rows[i];

				for (int j = 0; j < row.ItemArray.Length; j++)
				{
					if (row.ItemArray[j] != null && !Convert.IsDBNull(row.ItemArray[j]))
					{
						sw.Write(row.ItemArray[j].ToString());
					}
					else
					{
						sw.Write("");
					}

					if (j < row.ItemArray.Length - 1)
					{
						sw.Write(",");
					}
					else if (i < UpdatedData.Rows.Count)
					{
						sw.WriteLine();
					}
				}
			}
			sw.Close(); 

		}
	}
}