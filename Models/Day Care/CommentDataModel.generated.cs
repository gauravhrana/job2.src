using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	[Serializable]
	public partial class CommentDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string CommentId = "CommentId";
		}

		public static readonly CommentDataModel Empty = new CommentDataModel();

	}
}
