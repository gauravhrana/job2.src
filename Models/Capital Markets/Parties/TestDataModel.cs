using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Framework.Components.DataAccess;


namespace DataModel.CapitalMarkets
{
	public partial class TestDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string TestId = "TestId";
		}

		public static readonly TestDataModel Empty = new TestDataModel();

		[PrimaryKey, IncludeInSearch]
		public int? TestId { get; set; }

	}
}
