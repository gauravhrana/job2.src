using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{
    [Serializable]
    public class ReportCategoryDataModel : StandardDataModel
    {
        public class DataColumns : StandardDataColumns
        {
            public const string ReportCategoryId = "ReportCategoryId";
            public const string Application = "Application";
        }

		public static readonly ReportCategoryDataModel Empty = new ReportCategoryDataModel();

        public int? ReportCategoryId { get; set; }
        public string Application { get; set; }
    }
}
