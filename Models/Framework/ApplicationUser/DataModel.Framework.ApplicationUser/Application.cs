using DataModel.Framework.DataAccess;

namespace DataModel.Framework.AuthenticationAndAuthorization
{
	public class ApplicationDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string Code                    = "Code";
            public const string RenderApplicationFilter = "RenderApplicationFilter";	
		}

		public static readonly ApplicationDataModel Empty = new ApplicationDataModel();

		public string   Code                    { get; set; }
        public int?     RenderApplicationFilter { get; set; }

	}
}
