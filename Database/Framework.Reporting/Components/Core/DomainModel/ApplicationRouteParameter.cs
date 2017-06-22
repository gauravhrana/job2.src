using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{
    public class ApplicationRouteParameterDataModel : BaseDataModel
    {        
        public class DataColumns : BaseDataColumns
        {
            public const string ApplicationRouteParameterId = "ApplicationRouteParameterId";
            public const string ApplicationRouteId          = "ApplicationRouteId";
            public const string ApplicationRoute            = "ApplicationRoute";
            public const string ParameterName               = "ParameterName";
            public const string ParameterValue              = "ParameterValue";
        }

		public static readonly ApplicationRouteParameterDataModel Empty = new ApplicationRouteParameterDataModel();

        public int? ApplicationRouteParameterId { get; set; }
        public int? ApplicationRouteId          { get; set; }
        public string ApplicationRoute          { get; set; }
        public string ParameterName             { get; set; }
        public string ParameterValue            { get; set; }

    }
}
