using DataModel.Framework.DataAccess;

namespace DataModel.Framework.AuthenticationAndAuthorization
{

    [System.Serializable]
	public class ApplicationOperationXApplicationRoleDataModel : BaseDataModel
    {
        public class DataColumns : BaseDataColumns
        {
            public const string ApplicationOperationXApplicationRoleId = "ApplicationOperationXApplicationRoleId";
            public const string ApplicationOperationId                 = "ApplicationOperationId";
            public const string ApplicationRoleId                      = "ApplicationRoleId";
        }

		public static readonly ApplicationOperationXApplicationRoleDataModel Empty = new ApplicationOperationXApplicationRoleDataModel();

        public int?		ApplicationOperationXApplicationRoleId	 { get; set; }
        public int?		ApplicationOperationId					 { get; set; }
        public int?		ApplicationRoleId						 { get; set; }

    }
}
