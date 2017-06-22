using DataModel.Framework.DataAccess;

namespace DataModel.Framework.AuthenticationAndAuthorization
{

    [System.Serializable]
	public class ApplicationRoleDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string ApplicationRoleId = "ApplicationRoleId";			
			public const string Application = "Application";
		}

		public static readonly ApplicationRoleDataModel Empty = new ApplicationRoleDataModel();

		public int? ApplicationRoleId { get; set; }
		public string Application { get; set; }

	}
}
