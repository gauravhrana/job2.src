using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.ApplicationDevelopment
{

    [Serializable]
	public class FunctionalityStatusDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string FunctionalityStatusId = "FunctionalityStatusId";
            public const string Application = "Application";
		}

		public static readonly FunctionalityStatusDataModel Empty = new FunctionalityStatusDataModel();

		public int? FunctionalityStatusId { get; set; }
        public string Application { get; set; }   

	}
}
