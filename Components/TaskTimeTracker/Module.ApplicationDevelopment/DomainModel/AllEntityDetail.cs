using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.ApplicationDevelopment
{
	public class AllEntityDetailDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string AllEntityDetailId		= "AllEntityDetailId";
			public const string EntityName				="EntityName";
			public const string DB_Name					="DB_Name";
			public const string DB_Project_Name			="DB_Project_Name";
			public const string Component_Project_Name	= "Component_Project_Name";
            public const string DBName = "DBName";
            public const string DBProjectName = "DBProjectName";
            public const string DBComponentName = "DBComponentName";
			public const string DBNameId = "DBNameId";
			public const string DBProjectNameId = "DBProjectNameId";
			public const string DBComponentNameId = "DBComponentNameId";
		}

		public static readonly AllEntityDetailDataModel Empty = new AllEntityDetailDataModel();

		public int? AllEntityDetailId		 { get; set; }
		public string EntityName			 { get; set; }
		public string DB_Name				 { get; set; }
		public string DB_Project_Name		 { get; set; }
		public string Component_Project_Name { get; set; }
		public int? DBNameId { get; set; }
		public int? DBProjectNameId { get; set; }
		public int? DBComponentNameId { get; set; }
	}
}
