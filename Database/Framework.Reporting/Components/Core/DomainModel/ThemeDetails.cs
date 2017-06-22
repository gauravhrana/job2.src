using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{
    public class ThemeDetailsDataModel : BaseDataModel
    {
        
        public class DataColumns : BaseDataColumns
        {
            public const string ThemeDetailId   = "ThemeDetailId";
            public const string ThemeKeyId      = "ThemeKeyId";
            public const string Value           = "Value";
            public const string ThemeId         = "ThemeId";
			public const string Theme			= "Theme";
			public const string ThemeCategory	= "ThemeCategory";
			public const string ThemeKey		= "ThemeKey";
            public const string ThemeCategoryId = "ThemeCategoryId";
        }

		public static readonly ThemeDetailsDataModel Empty = new ThemeDetailsDataModel();

        public int?     ThemeDetailId   { get; set; }
        public int?     ThemeKeyId      { get; set; }
        public string   Value           { get; set; }
        public int?     ThemeId         { get; set; }
        public int?     ThemeCategoryId { get; set; }
		public string	ThemeDetail		{ get; set; }
		public string	Theme			{ get; set; }
		public string	ThemeCategory	{ get; set; }

    }
}
