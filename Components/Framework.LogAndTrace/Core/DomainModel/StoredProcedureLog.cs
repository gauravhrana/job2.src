using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace Framework.Components.LogAndTrace
{

    [Serializable]
    public class StoredProcedureLogDataModel : StandardDataModel
    {

        public int?			StoredProcedureLogId	{ get; set; }       
		public DateTime?	TimeOfExecution			{ get; set; }
		public string		ExecutedBy				{ get; set; }

        public static readonly StoredProcedureLogDataModel Empty = new StoredProcedureLogDataModel();

        public class DataColumns : StandardDataColumns
        {
            public const string StoredProcedureLogId = "StoredProcedureLogId";
            public const string TimeOfExecution      = "TimeOfExecution";
            public const string ExecutedBy           = "ExecutedBy";
        }

    }

}


 