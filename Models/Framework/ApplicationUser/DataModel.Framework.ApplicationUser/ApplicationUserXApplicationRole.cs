using DataModel.Framework.DataAccess;

namespace DataModel.Framework.AuthenticationAndAuthorization
{

    [System.Serializable]
	public class ApplicationUserXApplicationRoleDataModel : BaseDataModel
    {

        public class DataColumns : BaseDataColumns
        {
            public const string ApplicationUserXApplicationRoleId	 = "ApplicationUserXApplicationRoleId";
            public const string ApplicationRoleId					 = "ApplicationRoleId";
            public const string ApplicationUserId					 = "ApplicationUserId";
            public const string ApplicationRole						 = "ApplicationRole";
            public const string ApplicationUser						 = "ApplicationUser";
        }

		public static readonly ApplicationUserXApplicationRoleDataModel Empty = new ApplicationUserXApplicationRoleDataModel();

        public int?		ApplicationUserXApplicationRoleId	 { get; set; }
		public int?		ApplicationRoleId					 { get; set; }
		public int?		ApplicationUserId					 { get; set; }
		public string	ApplicationRole						 { get; set; }
		public string	ApplicationUser						 { get; set; }

    }
}
