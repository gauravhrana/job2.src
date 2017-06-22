using DataModel.Framework.DataAccess;

namespace DataModel.Framework.AuthenticationAndAuthorization
{

    [System.Serializable]
	public class ApplicationUserProfileImageDataModel : BaseDataModel
    {

        public class DataColumns : BaseDataColumns
        {
            public const string ApplicationUserProfileImageId = "ApplicationUserProfileImageId";
            public const string ApplicationUserId             = "ApplicationUserId";
            public const string Image                         = "Image";            
            public const string ApplicationUserName           = "ApplicationUserName";
            public const string Application                   = "Application";
        }

		public static readonly ApplicationUserProfileImageDataModel Empty = new ApplicationUserProfileImageDataModel();

        public int?     ApplicationUserProfileImageId   { get; set; }
        public int?     ApplicationUserId               { get; set; }
        public byte[]   Image                           { get; set; }
        public string   ApplicationUserName             { get; set; }
        public string   Application                     { get; set; }

    }
}
