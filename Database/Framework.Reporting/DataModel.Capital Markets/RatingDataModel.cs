using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public class RatingDataModel : BaseModel
	{
		public class DataColumns
		{
			public const string RatingId	= "RatingId";
			public const string Date		= "Date ";
			public const string Analyst		= "Analyst ";
			public const string Rating		= "Rating ";
			public const string Notes		= "Notes ";			
		}

		public static readonly RatingDataModel Empty = new RatingDataModel();

		[PrimaryKey, IncludeInSearch]
		public int? RatingId { get; set; }
		
		public DateTime? Date	{ get; set; }
		public string Analyst	{ get; set; }
		public decimal? Rating	{ get; set; }
		public string Notes		{ get; set; }		

	}
}
