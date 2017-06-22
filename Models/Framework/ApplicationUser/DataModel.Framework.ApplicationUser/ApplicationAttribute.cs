using DataModel.Framework.DataAccess;

namespace DataModel.Framework.AuthenticationAndAuthorization
{

    [System.Serializable]
    public class ApplicationAttributeDataModel : StandardDataModel
    {
        public class DataColumns : BaseDataColumns
        {
            public const string Application             = "Application";
            public const string RenderApplicationFilter = "RenderApplicationFilter";
        }

        public static readonly ApplicationAttributeDataModel Empty = new ApplicationAttributeDataModel();

        public string   Application                 { get; set; }
        public int?     RenderApplicationFilter     { get; set; }
    }
}
