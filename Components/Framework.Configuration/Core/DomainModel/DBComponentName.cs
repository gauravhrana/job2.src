using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Configuration
{
	public class DBComponentNameDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string DBComponentNameId = "DBComponentNameId";
		}

		public int? DBComponentNameId { get; set; }

	}

}