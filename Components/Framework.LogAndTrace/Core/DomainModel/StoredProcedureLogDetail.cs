using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace Framework.Components.LogAndTrace
{

    [Serializable]
    public class StoredProcedureLogDetailDataModel : StandardDataModel
    {

        public int?		StoredProcedureLogDetailId	 { get; set; }
		public int?		StoredProcedureLogId		 { get; set; }
		public string	ParameterName				 { get; set; }
		public string	ParameterValue				 { get; set; }

        public static readonly StoredProcedureLogDetailDataModel Empty = new StoredProcedureLogDetailDataModel();

        public class DataColumns : StandardDataColumns
        {
            public const string StoredProcedureLogDetailId = "StoredProcedureLogDetailId";
            public const string StoredProcedureLogId       = "StoredProcedureLogId";
            public const string ParameterName              = "ParameterName";
            public const string ParameterValue             = "ParameterValue";
        }

    }

}
 