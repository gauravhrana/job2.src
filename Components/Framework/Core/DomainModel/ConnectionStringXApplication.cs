using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{
    [Serializable]
	public class ConnectionStringXApplicationDataModel : BaseDataModel
	{
		public class DataColumns : BaseDataColumns 
		{
			public const string ConnectionStringXApplicationId = "ConnectionStringXApplicationId";			
			public const string ConnectionStringId             = "ConnectionStringId";
			public const string ConnectionString               = "ConnectionString";
			public const string Application                    = "Application";
		}

		public static readonly ConnectionStringXApplicationDataModel Empty = new ConnectionStringXApplicationDataModel();

		public int?		ConnectionStringXApplicationId	{ get; set; }		
		public int?		ConnectionStringId				{ get; set; }
		public string	ConnectionString				{ get; set; }
		public string	Application						{ get; set; }

	}
}
