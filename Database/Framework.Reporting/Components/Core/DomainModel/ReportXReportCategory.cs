using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{
    public class ReportXReportCategoryDataModel : BaseDataModel
    {
        public class DataColumns : BaseDataColumns
        {
            public const string ReportXReportCategoryId = "ReportXReportCategoryId";
            public const string ReportId                = "ReportId";
            public const string ReportCategoryId        = "ReportCategoryId";

            public const string Report                  = "Report";
            public const string ReportCategory          = "ReportCategory";
        }

		public static readonly ReportXReportCategoryDataModel Empty = new ReportXReportCategoryDataModel();

        public int?     ReportXReportCategoryId { get; set; }
        public int?     ReportId                { get; set; }
        public int?     ReportCategoryId        { get; set; }

        public string   Report                  { get; set; }
        public string   ReportCategory          { get; set; }

    }
}
