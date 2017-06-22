using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{
    public class HelpPageDataModel : BaseDataModel
    {        
        public class DataColumns : BaseDataColumns
        {
            public const string HelpPageId         = "HelpPageId";
            public const string Name               = "Name";
            public const string Content            = "Content";
            public const string SortOrder          = "SortOrder";
            public const string SystemEntityTypeId = "SystemEntityTypeId";
            public const string SystemEntityType   = "SystemEntityType";
            public const string HelpPageContext    = "HelpPageContext";
            public const string HelpPageContextId  = "HelpPageContextId";
        }

		public static readonly HelpPageDataModel Empty = new HelpPageDataModel();

        public int?     HelpPageId          { get; set; }
        public string   Name                { get; set; }
        public string   Content             { get; set; }
        public int?     SortOrder           { get; set; }
        public int?     SystemEntityTypeId  { get; set; }
        public int?     HelpPageContextId   { get; set; }
        public string   SystemEntityType    { get; set; }
        public string   HelpPageContext     { get; set; }

    }
}
