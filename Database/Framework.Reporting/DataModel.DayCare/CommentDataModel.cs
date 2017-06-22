using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{
	public partial class CommentDataModel : StandardModel
	{
		public class DataColumns : StandardColumns
		{
			public const string CommentId = "CommentId";

		}

		public static readonly CommentDataModel Empty = new CommentDataModel();

		[PrimaryKey]
		public int? CommentId { get; set; }
	}
}
