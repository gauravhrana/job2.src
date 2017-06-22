using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference.DomainModel
{
    
    public class FieldConfigurationModeAccessMode : StandardModel
    {
        
        public class DataColumns : StandardDataColumns
        {
            public const string FieldConfigurationModeAccessModeId = "FieldConfigurationModeAccessModeId";
        }

        public int? FieldConfigurationModeAccessModeId { get; set; }

        public string ToURLQuery()
        {
            return String.Empty;
        }

    }
}
