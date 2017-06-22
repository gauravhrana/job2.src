using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace Framework.Components.LogAndTrace
{

    [Serializable]
    public class DatabaseChangeLogDataModel : BaseDataModel
    {

        public int?         Id              { get; set; }
        public string       DataBaseName    { get; set; }
		public string       SchemaName      { get; set; }	
		public string       ObjectName	    { get; set; }
		public string       ObjectType      { get; set; }	
		public string       EventType       { get; set; }	
		public DateTime?    RecordDate      { get; set; }
		public DateTime?	FromSearchDate { get; set; }
		public DateTime?	ToSearchDate { get; set; }	
		public string       SystemUser      { get; set; }	
		public string       CurrentUser	    { get; set; }
		public string       OriginalUser    { get; set; }
		public string       CommandText	    { get; set; }
		public string       EventData       { get; set; }	
		public string       HostName        { get; set; }

        public class DataColumns : BaseDataColumns 
        {

            public const string Id                  = "Id";         
            public const string DataBaseName        = "DataBaseName"; 
            public const string SchemaName          = "SchemaName";  
            public const string ObjectName	        = "ObjectName";	 
            public const string ObjectType          = "ObjectType";   
            public const string EventType           = "EventType";    
            public const string RecordDate          = "RecordDate";   
            public const string SystemUser          = "SystemUser";   
            public const string CurrentUser	        = "CurrentUser";	 
            public const string OriginalUser        = "OriginalUser"; 
            public const string CommandText	        = "CommantText";	 
            public const string EventData           = "EventData";    
            public const string HostName            = "HostName";
			public const string FromSearchDate		= "FromSearchDate";
			public const string ToSearchDate		= "ToSearchDate";

        }
    }

}
