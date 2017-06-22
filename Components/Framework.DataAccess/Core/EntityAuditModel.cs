using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Framework.Components.DataAccess
{
	public class EntityAuditModel
	{
		public class EntityAuditDataColumns
		{			
			public const string CreatedDate					= "CreatedDate";
			public const string UpdatedDate					= "UpdatedDate";
			public const string CreatedDateId				= "CreatedDateId";
			public const string UpdatedDateId				= "UpdatedDateId";
			public const string CreatedByAuditId			= "CreatedByAuditId";
			public const string ModifiedByAuditId			= "ModifiedByAuditId";
			public const string EntityKey					= "EntityKey";

		}
		
		
		public DateTime? CreatedDate	{ get; set; }
		public DateTime? UpdatedDate	{ get; set; }
		public float? CreatedDateId		{ get; set; }
		public float? UpdatedDateId		{ get; set; }
		public int? CreatedByAuditId	{ get; set; }
		public int? ModifiedByAuditId	{ get; set; }
		public int? EntityKey			{ get; set; }	

	}
}
