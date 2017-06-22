using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.TaskTimeTracker
{
    [Table("VacationPlan")]
	public class VacationPlanDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string VacationPlanId			= "VacationPlanId";
			public const string ApplicationUserId		= "ApplicationUserId";
			public const string StartDate				= "StartDate";
			public const string EndDate					= "EndDate";
			public const string EndDate2				= "EndDate2";
			public const string StartDate2				= "StartDate2";			
		}

        public static readonly VacationPlanDataModel Empty = new VacationPlanDataModel();

        [Key]
		public int?         VacationPlanId      { get; set; }
		public int?         ApplicationUserId   { get; set; }
		public string		ApplicationUser		{ get; set; }
		public DateTime?    StartDate           { get; set; }
		public DateTime?    StartDateR2         { get; set; }
		public DateTime?    EndDate             { get; set; }
		public DateTime?    EndDateR2           { get; set; }
		
	}
}
