using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference.DomainModel
{
    
    public class FieldConfigurationModeXApplicationRole : BaseModel
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

        public int?     FieldConfigurationModeXApplicationRoleId    { get; set; }
        public int?     FieldConfigurationModeId                    { get; set; }
        public int?     ApplicationRoleId                           { get; set; }
        public int?     FieldConfigurationModeAccessModeId          { get; set; }
        public string   FieldConfigurationMode                      { get; set; }
        public string   ApplicationRole                             { get; set; }
        public string   FieldConfigurationModeAccessMode            { get; set; }

        public string ToURLQuery()
        {
            return String.Empty;
        }

    }
}
