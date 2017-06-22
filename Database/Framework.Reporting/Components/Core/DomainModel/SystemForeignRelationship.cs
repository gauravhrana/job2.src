using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{
	public class SystemForeignRelationshipDataModel : BaseDataModel
	{
		public class DataColumns : BaseDataColumns
		{
			public const string SystemForeignRelationshipId					= "SystemForeignRelationshipId";			
			public const string PrimaryDatabaseId							= "PrimaryDatabaseId";
			public const string PrimaryEntityId								= "PrimaryEntityId";
			public const string ForeignDatabaseId							= "ForeignDatabaseId";
			public const string ForeignEntityId								= "ForeignEntityId";
			public const string FieldName									= "FieldName";
			public const string Source										= "Source";
			public const string SystemForeignRelationshipTypeId				= "SystemForeignRelationshipTypeId";

			public const string SystemForeignRelationshipType				= "SystemForeignRelationshipType";
			public const string PrimaryDatabase								= "PrimaryDatabase";
			public const string PrimaryEntity								= "PrimaryEntity";
			public const string ForeignDatabase								= "ForeignDatabase";
			public const string ForeignEntity								= "ForeignEntity";
		}

		public static readonly SystemForeignRelationshipDataModel Empty = new SystemForeignRelationshipDataModel();

		public int? SystemForeignRelationshipId		{ get; set; }
		public int? PrimaryDatabaseId				{ get; set; }
		public int? PrimaryEntityId					{ get; set; }
		public int? ForeignDatabaseId				{ get; set; }
		public int? ForeignEntityId					{ get; set; }
		public string FieldName						{ get; set; }
		public string Source						{ get; set; }
		public int? SystemForeignRelationshipTypeId { get; set; }

		public string SystemForeignRelationshipType { get; set; }
		public string PrimaryDatabase				{ get; set; }
		public string PrimaryEntity					{ get; set; }
		public string ForeignDatabase				{ get; set; }
		public string ForeignEntity					{ get; set; }

	}
}
