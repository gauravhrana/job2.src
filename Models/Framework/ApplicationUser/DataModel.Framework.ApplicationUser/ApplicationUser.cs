using DataModel.Framework.DataAccess;

namespace DataModel.Framework.AuthenticationAndAuthorization
{


    [System.Serializable]
	public class ApplicationUserDataModel : BaseDataModel
    {

        public class DataColumns : BaseDataColumns
        {
           
			public const string ApplicationUserId      = "ApplicationUserId";
            public const string ApplicationUserName    = "ApplicationUserName";
            public const string FirstName              = "FirstName";
            public const string LastName               = "LastName";
            public const string MiddleName             = "MiddleName";
            public const string FullName               = "FullName";
            public const string ApplicationUserTitleId = "ApplicationUserTitleId";
            public const string ApplicationUserTitle   = "ApplicationUserTitle";
			public const string EmailAddress           = "EmailAddress";
            public const string Application            = "Application";
			public const string ApplicationCode		   = "ApplicationCode";

        }

		public static readonly ApplicationUserDataModel Empty = new ApplicationUserDataModel();

        public int?		ApplicationUserId       { get; set; }
        public string   ApplicationUserName     { get; set; }
        public string	FirstName               { get; set; }
        public string	LastName                { get; set; }
        public string	MiddleName              { get; set; }
		public string   FullName                { get; set; }
		public string	EmailAddress			{ get; set; }
		public int?     ApplicationUserTitleId  { get; set; }
		public string   ApplicationUserTitle    { get; set; }
        public string   Application             { get; set; }
		public string	ApplicationCode			{ get; set; }
		public string	ApplicationUserFullName { get; set; }	
		
    }
}
