using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{
    [Serializable]
    public class MenuDisplayNameDataModel : BaseDataModel
    {
        public class DataColumns : BaseDataColumns
        {
            public const string MenuDisplayNameId = "MenuDisplayNameId";
            public const string LanguageId        = "LanguageId";
            public const string MenuId            = "MenuId";
            public const string Menu              = "Menu";
            public const string Value             = "Value";
            public const string IsDefault         = "IsDefault";
            public const string Language          = "Language";
        }

		public static readonly MenuDisplayNameDataModel Empty = new MenuDisplayNameDataModel();

        public int?     MenuDisplayNameId   { get; set; }
        public string   Value               { get; set; }
        public int?     LanguageId          { get; set; }
        public int?     IsDefault           { get; set; }
        public int?     MenuId              { get; set; }
        public string   Menu                { get; set; }
        public string   Language            { get; set; }

    }
}
