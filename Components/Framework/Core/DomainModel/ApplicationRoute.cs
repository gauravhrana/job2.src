using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{
    [Serializable]
    public class ApplicationRouteDataModel : BaseDataModel
    {
        public class DataColumns : BaseDataColumns
        {
            public const string ApplicationRouteId = "ApplicationRouteId";
            public const string RouteName          = "RouteName";
            public const string EntityName         = "EntityName";
            public const string ProposedRoute      = "ProposedRoute";
            public const string RelativeRoute      = "RelativeRoute";
            public const string Description        = "Description";
        }

		public static readonly ApplicationRouteDataModel Empty = new ApplicationRouteDataModel();

        public int? ApplicationRouteId  { get; set; }
        public string RouteName         { get; set; }
        public string EntityName        { get; set; }
        public string ProposedRoute     { get; set; }
        public string RelativeRoute     { get; set; }
        public string Description       { get; set; }

    }
}
