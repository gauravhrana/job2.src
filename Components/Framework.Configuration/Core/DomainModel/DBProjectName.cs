using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Configuration
{
	public class DBProjectNameDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string DBProjectNameId = "DBProjectNameId";
		}

		public int? DBProjectNameId { get; set; }

	}

}