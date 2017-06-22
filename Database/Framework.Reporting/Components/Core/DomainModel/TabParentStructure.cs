using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{
    public class TabParentStructureDataModel : StandardDataModel
    {
        public class DataColumns : StandardDataColumns
        {
            public const string TabParentStructureId = "TabParentStructureId";
            public const string TabParentStructure = "TabParentStructure"; 
            public const string IsAllTab = "IsAllTab";
        }

		public static readonly TabParentStructureDataModel Empty = new TabParentStructureDataModel();

        public int? TabParentStructureId { get; set; }
        public decimal? IsAllTab { get; set; }

    }
}
