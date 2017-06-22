using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace Framework.Components.UserPreference
{

    [Serializable]
    public class FieldConfigurationDisplayNameDataModel : BaseDataModel
    {

        public class DataColumns : BaseDataColumns
        {
            public const string FieldConfigurationDisplayNameId = "FieldConfigurationDisplayNameId";
            public const string LanguageId                      = "LanguageId";
            public const string FieldConfigurationId            = "FieldConfigurationId";
            public const string FieldConfiguration              = "FieldConfiguration";
            public const string Value                           = "Value";
            public const string IsDefault                       = "IsDefault";
            public const string Language                        = "Language";
        }

		public static readonly FieldConfigurationDisplayNameDataModel Empty = new FieldConfigurationDisplayNameDataModel();

        public int?     FieldConfigurationDisplayNameId { get; set; }
        public string   Value                           { get; set; }
        public int?     LanguageId                      { get; set; }
        public int?     IsDefault                       { get; set; }
        public int?     FieldConfigurationId            { get; set; }
        public string   FieldConfiguration              { get; set; }
        public string   Language                        { get; set; }

    }

}
