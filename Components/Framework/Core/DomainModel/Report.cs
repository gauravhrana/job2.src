using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{
    [Serializable]
    public class ReportDataModel : StandardDataModel
    {
        public class DataColumns : StandardDataColumns
        {
            public const string ReportId = "ReportId";
            public const string Title = "Title";
            public const string Application = "Application";
        }

		public static readonly ReportDataModel Empty = new ReportDataModel();

        public int? ReportId { get; set; }
        public string Title { get; set; }
        public string Application { get; set; }        
       
    }
}
