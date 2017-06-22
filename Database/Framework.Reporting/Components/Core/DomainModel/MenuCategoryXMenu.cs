using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{
    public class MenuCategoryXMenuDataModel : BaseDataModel
    {
        public class DataColumns : BaseDataColumns
        {
            public const string MenuCategoryXMenuId = "MenuCategoryXMenuId";
            public const string MenuId              = "MenuId";
            public const string MenuCategoryId      = "MenuCategoryId";
            public const string MenuCategory        = "MenuCategory";
        }

		public static readonly MenuCategoryXMenuDataModel Empty = new MenuCategoryXMenuDataModel();

        public int?     MenuCategoryXMenuId { get; set; }
        public int?     MenuId              { get; set; }
        public int?     MenuCategoryId      { get; set; }
        public string   MenuCategory        { get; set; }

    }
}
