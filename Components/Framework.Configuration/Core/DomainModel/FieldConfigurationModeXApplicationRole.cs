using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Configuration
{

    [Serializable]
	public class FieldConfigurationModeXApplicationRoleDataModel : BaseDataModel
    {

        public class DataColumns : BaseDataColumns
        {
            public const string FieldConfigurationModeXApplicationRoleId = "FieldConfigurationModeXApplicationRoleId";
            public const string FieldConfigurationModeId                 = "FieldConfigurationModeId";
            public const string ApplicationRoleId                        = "ApplicationRoleId";
            public const string FieldConfigurationModeAccessModeId       = "FieldConfigurationModeAccessModeId";
            public const string FieldConfigurationMode                   = "FieldConfigurationMode";
            public const string ApplicationRole                          = "ApplicationRole";
            public const string FieldConfigurationModeAccessMode         = "FieldConfigurationModeAccessMode";
        }

		public static readonly FieldConfigurationModeXApplicationRoleDataModel Empty = new FieldConfigurationModeXApplicationRoleDataModel();

        public int?     FieldConfigurationModeXApplicationRoleId    { get; set; }
        public int?     FieldConfigurationModeId                    { get; set; }
        public int?     ApplicationRoleId                           { get; set; }
        public int?     FieldConfigurationModeAccessModeId          { get; set; }
        public string   FieldConfigurationMode                      { get; set; }
        public string   ApplicationRole                             { get; set; }
        public string   FieldConfigurationModeAccessMode            { get; set; }

    }
}
