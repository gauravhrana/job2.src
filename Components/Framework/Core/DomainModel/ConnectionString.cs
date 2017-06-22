using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{
    [Serializable]
	public class ConnectionStringDataModel : BaseDataModel
	{

		public class DataColumns : BaseDataColumns
		{
			public const string ConnectionStringId = "ConnectionStringId";
			public const string Name               = "Name";
			public const string Description        = "Description";
			public const string DataSource         = "DataSource";
			public const string InitialCatalog     = "InitialCatalog";
			public const string UserName           = "UserName";
			public const string Password           = "Password";
			public const string ProviderName       = "ProviderName";
		}

		public static readonly ConnectionStringDataModel Empty = new ConnectionStringDataModel();

		public int?		ConnectionStringId	{ get; set; }
		public string	Name				{ get; set; }
		public string	Description			{ get; set; }
		public string	DataSource			{ get; set; }
		public string	InitialCatalog		{ get; set; }
		public string	UserName			{ get; set; }
		public string	Password			{ get; set; }
		public string	ProviderName		{ get; set; }
	
	}
}

