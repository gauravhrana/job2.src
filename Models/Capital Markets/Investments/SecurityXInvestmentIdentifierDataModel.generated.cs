using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class SecurityXInvestmentIdentifierDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string SecurityXInvestmentIdentifierId = "SecurityXInvestmentIdentifierId";
			public const string SecurityId = "SecurityId";
			public const string Security = "Security";
			public const string Ticker = "Ticker";
			public const string CUSIP = "CUSIP";
			public const string SEDOL = "SEDOL";
			public const string ISIN = "ISIN";
			public const string WKN = "WKN";
			public const string AltID1 = "AltID1";
			public const string AltID2 = "AltID2";
			public const string AltID3 = "AltID3";
			public const string AltID4 = "AltID4";
			public const string AltID5 = "AltID5";
		}

		public static readonly SecurityXInvestmentIdentifierDataModel Empty = new SecurityXInvestmentIdentifierDataModel();

	}
}
