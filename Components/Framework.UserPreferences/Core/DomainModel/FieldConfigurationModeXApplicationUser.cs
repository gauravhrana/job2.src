using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference.DomainModel
{
    public class FieldConfigurationModeXApplicationUser : BaseModel
    {

        public class DataColumns : BaseDataColumns
        {
            public const string FieldConfigurationModeXApplicationUserId = "FieldConfigurationModeXApplicationUserId";
            public const string FieldConfigurationModeId                 = "FieldConfigurationModeId";
            public const string ApplicationUserId                        = "ApplicationUserId";
            public const string FieldConfigurationModeAccessModeId       = "FieldConfigurationModeAccessModeId";
            public const string FieldConfigurationMode                   = "FieldConfigurationMode";
            public const string ApplicationUser                          = "ApplicationUser";
            public const string FieldConfigurationModeAccessMode         = "FieldConfigurationModeAccessMode";
        }

        public int?     FieldConfigurationModeXApplicationUserId    { get; set; }
        public int?     FieldConfigurationModeId                    { get; set; }
        public int?     ApplicationUserId                           { get; set; }
        public int?     FieldConfigurationModeAccessModeId          { get; set; }
        public string   FieldConfigurationMode                      { get; set; }
        public string   ApplicationUser                             { get; set; }
        public string   FieldConfigurationModeAccessMode            { get; set; }

        public string ToURLQuery()
        {
            return String.Empty;
        }

    }

}
