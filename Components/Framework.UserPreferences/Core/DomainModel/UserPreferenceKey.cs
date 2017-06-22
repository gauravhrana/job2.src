using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference.DomainModel
{
    public class UserPreferenceKey : StandardModel
    {
        public class DataColumns : StandardDataColumns
        {
            public const string UserPreferenceKeyId = "UserPreferenceKeyId";
            public const string Value = "Value";
            public const string DataTypeId = "DataTypeId";
            public const string DataType = "DataType";
        }

        public int? UserPreferenceKeyId { get; set; }
        public string Value { get; set; }
        public int? DataTypeId { get; set; }
        public string DataType { get; set; }

        public string ToURLQuery()
        {
            return String.Empty; //"ClientId=" + ClientId
        }
    }
}
