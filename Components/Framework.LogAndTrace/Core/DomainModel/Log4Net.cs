using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace Framework.Components.LogAndTrace
{

    [Serializable]
    public class Log4NetDataModel : BaseDataModel
    {
        
        public int?			Id					 { get; set; }
		public string		LogUser				 { get; set; }
		public DateTime?	Date				 { get; set; }
		public string		Level				 { get; set; }
		public string		Thread				 { get; set; }
		public string		Logger				 { get; set; }
		public string		Message				 { get; set; }
		public string		Exception			 { get; set; }		
		public int?			NoOfRecords			 { get; set; }
		public int?			ExcludeApplicationId { get; set; }
        public DateTime?    CleanupDate          { get; set; }
        public string       ConnectionKey        { get; set; }
		public string		Computer		 { get; set; }

        public static readonly Log4NetDataModel Empty = new Log4NetDataModel();
        
        public class DataColumns : BaseDataColumns
        {

            public const string Id                   = "Id";
            public const string LogUser              = "LogUser";
            public const string Date                 = "Date";
            public const string Level                = "Level";
            public const string Thread               = "Thread";
            public const string Logger               = "Logger";
            public const string Message              = "Message";
            public const string Exception            = "Exception";
            public const string NoOfRecords          = "NoOfRecords";
            public const string ExcludeApplicationId = "ExcludeApplicationId";
            public const string CleanupDate          = "CleanupDate";
            public const string ConnectionKey        = "ConnectionKey";
			public const string Computer			 = "Computer";
			public const string ApplicationUser		 = "ApplicationUser";
			public const string ElapsedTime			 = "ElapsedTime";
			public const string RecordCount			 = "RecordCount";

        }

    }

} 
