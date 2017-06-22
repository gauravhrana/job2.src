using DataModel.Framework.DataAccess;

namespace DataModel.Framework.AuthenticationAndAuthorization
{

    [System.Serializable]
	public class ApplicationOperationDataModel : StandardDataModel
    {
        public class DataColumns : StandardDataColumns
        {
            public const string ApplicationOperationId = "ApplicationOperationId";
            public const string SystemEntityTypeId     = "SystemEntityTypeId";
            public const string SystemEntityId         = "SystemEntityId";
            public const string OperationValue         = "OperationValue";
            public const string Application            = "Application";
            public const string SystemEntityType       = "SystemEntityType";
        }

		public static readonly ApplicationOperationDataModel Empty = new ApplicationOperationDataModel();

        public int?			ApplicationOperationId	 { get; set; }
		public int?			SystemEntityTypeId		 { get; set; }
		public int?			SystemEntityId			 { get; set; }
		public string		OperationValue			 { get; set; }		
		public string		Application				 { get; set; }
		public string		SystemEntityType		 { get; set; }
    }
}
