using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{
    [Serializable]
    public class TabChildStructureDataModel : BaseDataModel
    {
        
        public class DataColumns : BaseDataColumns
        {
            public const string TabChildStructureId  = "TabChildStructureId";
            public const string Name                 = "Name";
            public const string EntityName           = "EntityName";
            public const string SortOrder            = "SortOrder";
            public const string TabParentStructureId = "TabParentStructureId";
            public const string TabParentStructure   = "TabParentStructure";
            public const string InnerControlPath     = "InnerControlPath";
        }

		public static readonly TabChildStructureDataModel Empty = new TabChildStructureDataModel();

        public int?		TabChildStructureId		{ get; set; }
        public string	Name					{ get; set; }
		public string	EntityName				{ get; set; }
        public int?		SortOrder				{ get; set; }
		public decimal? TabParentStructureId	{ get; set; }
		public string	InnerControlPath		{ get; set; }

    }
}
