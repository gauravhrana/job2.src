using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace Framework.Components.UserPreference
{
    [Serializable]
    public class ApplicationModeXFieldConfigurationModeDataModel : BaseDataModel
    {
        public int? ApplicationModeXFieldConfigurationModeId    { get; set; }
        public int? ApplicationModeId                           { get; set; }
        public int? FieldConfigurationModeId                    { get; set; }

        public class DataColumns : BaseDataColumns
        {
            public const string ApplicationModeXFieldConfigurationModeId = "ApplicationModeXFieldConfigurationModeId";
            public const string ApplicationModeId                        = "ApplicationModeId";
            public const string FieldConfigurationModeId                 = "FieldConfigurationModeId";
        }

		public static readonly ApplicationModeXFieldConfigurationModeDataModel Empty = new ApplicationModeXFieldConfigurationModeDataModel();

    }
}
