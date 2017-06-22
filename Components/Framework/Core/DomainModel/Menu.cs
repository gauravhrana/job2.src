using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{
    [Serializable]
    public class MenuDataModel : StandardDataModel
    {
        public class DataColumns : StandardDataColumns
        {
            public const string MenuId            = "MenuId";
            public const string Value             = "Value";
            public const string ParentMenuId      = "ParentMenuId";
            public const string PrimaryDeveloper  = "PrimaryDeveloper";
            public const string ParentMenu        = "ParentMenu";
            public const string IsChecked         = "IsChecked";
            public const string IsVisible         = "IsVisible";            
            public const string Application       = "Application";
            public const string NavigateURL       = "NavigateURL";
            public const string MenuDisplayName   = "MenuDisplayName";
            public const string ApplicationModule = "ApplicationModule";
        }

		public static readonly MenuDataModel Empty = new MenuDataModel();

        public int?     MenuId              { get; set; }
        public string   Value               { get; set; }        
        public string   Application         { get; set; }
        public string   NavigateURL         { get; set; }
        public int?     IsChecked           { get; set; }
        public int?     IsVisible           { get; set; }
        public int?     ParentMenuId        { get; set; }
        public string   PrimaryDeveloper    { get; set; }
        public string   MenuDisplayName     { get; set; }
        public string   ParentMenu          { get; set; }
        public string   ApplicationModule   { get; set; }
    }
}
