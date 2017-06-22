using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Configuration
{

    [Serializable]
    public class UserPreferenceKeyDataModel : StandardDataModel
    {
        
        public class DataColumns : StandardDataColumns
        {
            public const string UserPreferenceKeyId = "UserPreferenceKeyId";
            public const string Value               = "Value";
            public const string DataTypeId          = "DataTypeId";
            public const string DataType            = "DataType";
        }

		public static readonly UserPreferenceKeyDataModel Empty = new UserPreferenceKeyDataModel();

        public int?     UserPreferenceKeyId     { get; set; }
        public string   Value                   { get; set; }
        public int?     DataTypeId              { get; set; }
        public string   DataType                { get; set; }

    }

}
