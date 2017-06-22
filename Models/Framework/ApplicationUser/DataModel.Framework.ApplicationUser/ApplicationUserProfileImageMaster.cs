using DataModel.Framework.DataAccess;

namespace DataModel.Framework.AuthenticationAndAuthorization
{

    [System.Serializable]
	public class ApplicationUserProfileImageMasterDataModel : BaseDataModel
    {

        public class DataColumns : BaseDataColumns
        {
            public const string ApplicationUserProfileImageMasterId = "ApplicationUserProfileImageMasterId";
            public const string Title                               = "Title";
            public const string Image                               = "Image";
            public const string Application                         = "Application";
        }

		public static readonly ApplicationUserProfileImageMasterDataModel Empty = new ApplicationUserProfileImageMasterDataModel();

        public int?     ApplicationUserProfileImageMasterId { get; set; }
        public string   Title                               { get; set; }
        public byte[]   Image                               { get; set; }
        public string   Application                         { get; set; }

    }
}
