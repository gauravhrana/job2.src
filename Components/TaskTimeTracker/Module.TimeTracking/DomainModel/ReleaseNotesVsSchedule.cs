using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTimeTracker.Components.Module.TimeTracking.DomainModel
{
    [Serializable]
	public class ReleaseNotesVsSchedule
	{
		public class DataColumns : DataModel.Framework.DataAccess.StandardDataModel.StandardDataColumns
		{

			public const string User = "User";
			public const string ProjectManagement = "ProjectManagement";
			public const string ReferenceData = "ReferenceData";
			public const string TimeEntry = "TimeEntry";
			public const string ProductManagementTracker = "ProductManagementTracker";
			public const string SystemAdministrator = "SystemAdministrator";
			public const string Prototype = "Prototype";
			public const string CapitalMarkets = "CapitalMarkets";
			public const string DayCare = "DayCare";
			public const string SQLQueryTool = "SQLQueryTool";
			public const string DataCreation = "DataCreation";
			public const string Demo = "Demo";
			public const string TotalRN = "TotalRN";
			public const string TotalScheduleWorkedHours = "TotalScheduleWorkedHours";
			public const string SystemAdministration = "SystemAdministration";
			public const string CaptialMarkets = "CaptialMarkets";

		}

		public string User { get; set; }
		public double? ProjectManagement { get; set; }
		public double? ReferenceData { get; set; }
		public double? TimeEntry { get; set; }
		public double? ProductManagementTracker { get; set; }
		public double? SystemAdministrator { get; set; }
		public double? Prototype { get; set; }
		public double? CapitalMarkets { get; set; }
		public double? DayCare { get; set; }
		public double? SQLQueryTool { get; set; }
		public double? DataCreation { get; set; }
		public double? Demo { get; set; }
		public double? TotalRN { get; set; }
		public double? TotalScheduleWorkedHours { get; set; }
		public double? SystemAdministration { get; set; }
		public double? CaptialMarkets { get; set; }
	}

}
