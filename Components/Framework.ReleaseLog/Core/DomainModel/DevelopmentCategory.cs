using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace Framework.Components.ReleaseLog.DomainModel
{
    [Serializable]
	public class DevelopmentCategoryDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string DevelopmentCategoryId	= "DevelopmentCategoryId";
			public const string DateCreated				= "DateCreated";
			public const string DateModified			= "DateModified";			
		}

		public static readonly DevelopmentCategoryDataModel Empty = new DevelopmentCategoryDataModel();

		public int? DevelopmentCategoryId { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateModified { get; set; }		
	}
}
