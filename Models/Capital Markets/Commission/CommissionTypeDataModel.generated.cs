using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class CommissionTypeDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string CommissionTypeId = "CommissionTypeId";
			public const string CommissionTypeDescription = "CommissionTypeDescription";
			public const string LastModifiedBy = "LastModifiedBy";
			public const string LastModifiedOn = "LastModifiedOn";
			public const string ShowInFilter = "ShowInFilter";
		}

		public static readonly CommissionTypeDataModel Empty = new CommissionTypeDataModel();

	}
}
