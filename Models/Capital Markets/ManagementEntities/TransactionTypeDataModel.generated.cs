using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class TransactionTypeDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string TransactionTypeId = "TransactionTypeId";
			public const string Name = "Name";
			public const string Description = "Description";
			public const string SortOrder = "SortOrder";
			public const string Code = "Code";
		}

		public static readonly TransactionTypeDataModel Empty = new TransactionTypeDataModel();

	}
}
