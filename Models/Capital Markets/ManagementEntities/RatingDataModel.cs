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
	public partial class RatingDataModel : BaseModel
	{
		
		[PrimaryKey, IncludeInSearch]
		public int? RatingId { get; set; }
		
		public DateTime? Date	{ get; set; }
		public string Analyst	{ get; set; }
		public decimal? Rating	{ get; set; }
		public string Notes		{ get; set; }		

	}
}
