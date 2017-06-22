using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace Framework.Components.UserPreference
{

    [Serializable]
    public class FieldConfigurationModeCategoryXFCModeDataModel : BaseDataModel
    {

        public int? FieldConfigurationModeCategoryXFCModeId     { get; set; }
        public int? FieldConfigurationModeCategoryId            { get; set; }
        public int? FieldConfigurationModeId                    { get; set; }

        public class DataColumns : BaseDataColumns
        {
            public const string FieldConfigurationModeCategoryXFCModeId = "FieldConfigurationModeCategoryXFCModeId";
            public const string FieldConfigurationModeCategoryId        = "FieldConfigurationModeCategoryId";
            public const string FieldConfigurationModeId                = "FieldConfigurationModeId";
        }

		public static readonly FieldConfigurationModeCategoryXFCModeDataModel Empty = new FieldConfigurationModeCategoryXFCModeDataModel();

    }

}
