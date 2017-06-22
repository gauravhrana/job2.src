using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Framework.Components.DataAccess;

namespace DataModel.Framework.DataAccess
{
    
    [Serializable]
	public class BaseDataModel
	{
		public class BaseDataColumns
		{			
			public const string ApplicationId		   = "ApplicationId";
			public const string AuditId				   = "AuditId";
			public const string TraceId				   = "TraceId";

			public const string ApplicationMode        = "ApplicationMode";
			public const string AddAuditInfo           = "AddAuditInfo";
			public const string AddTraceInfo           = "AddTraceInfo";
			public const string ReturnAuditInfo        = "ReturnAuditInfo";

			public const string CreatedDateId		   = "CreatedDateId";
			public const string UpdatedDateId		   = "UpdatedDateId";

			public const string CreatedDate			   = "CreatedDate";
			public const string UpdatedDate			   = "UpdatedDate";
			public const string UpdatedBy			   = "UpdatedBy";
			public const string LastAction			   = "LastAction";

			public const string PageIndex			   = "PageIndex";
			public const string PageSize			   = "PageSize";
			public const string OrderBy				   = "OrderBy";
			public const string OrderByDirection	   = "OrderByDirection";

			public const string ModifiedDate		   = "ModifiedDate";
			public const string CreatedByAuditId	   = "CreatedByAuditId";
			public const string ModifiedByAuditId	   = "ModifiedByAuditId";

		}

		[OnlyProperty]
		public float?		CreatedDateId	{ get; set; }
		[OnlyProperty]
		public float?		UpdatedDateId	{ get; set; }

		[OnlyProperty]
		public DateTime?	CreatedDate		{ get; set; }
		[OnlyProperty, AuditProperty]
		public DateTime?	UpdatedDate		{ get; set; }
		[AuditProperty, OnlyProperty]
		public string		UpdatedBy		{ get; set; }
		[AuditProperty, OnlyProperty]
		public string		LastAction		{ get; set; }
        
        [JsonConverter(typeof(NullableIntConverter))]
		public int?			ApplicationId	{ get; set; }

		[OnlyProperty]
		public DateTime?	ModifiedDate	  { get; set; }
		[OnlyProperty]
		public int?			CreatedByAuditId  { get; set; }
		[OnlyProperty]
		public int?			ModifiedByAuditId { get; set; }

		virtual public string ToURLQuery()
		{
			var answer = string.Empty;

			foreach (var prop in GetType().GetProperties())
			{
				answer += string.Format("{0}={1}", prop.Name, prop.GetValue(this, null));
			}

			return answer;
		}

		virtual public string ToSQLTraceInfo()
		{
			var answer = string.Empty;

			foreach (var prop in GetType().GetProperties())
			{
				if (prop.PropertyType == typeof(string))
				{
					var o = prop.GetValue(this, null);

					if(o == null)
					{
						answer += string.Format("\n@{0} = {1}", prop.Name, "NULL");	
					}
					else
					{
						answer += string.Format("\n@{0} = '{1}'", prop.Name, o);	
					}					
				}
				else
				{
					answer += string.Format("\n@{0} = {1}", prop.Name, prop.GetValue(this, null));	
				}								
			}

			return answer;
		}

		
	}

}
