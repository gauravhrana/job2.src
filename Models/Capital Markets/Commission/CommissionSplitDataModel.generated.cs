using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class CommissionSplitDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string CommissionSplitId = "CommissionSplitId";
			public const string CommissionSplitCode = "CommissionSplitCode";
			public const string CommissionSplitDescription = "CommissionSplitDescription";
			public const string FullRate = "FullRate";
			public const string NoneCCA = "NoneCCA";
			public const string CCA = "CCA";
			public const string StartDate = "StartDate";
			public const string EndDate = "EndDate";
			public const string LastModifiedBy = "LastModifiedBy";
			public const string LastModifiedOn = "LastModifiedOn";
			public const string CommissionCodeId = "CommissionCodeId";
			public const string CommissionCode = "CommissionCode";
		}

		public static readonly CommissionSplitDataModel Empty = new CommissionSplitDataModel();

	}
}
