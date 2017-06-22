using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference.DomainModel
{
	public class FieldConfiguration : BaseModel
	{
		public class DataColumns
		{
			public const string FieldConfigurationId          = "FieldConfigurationId";
			public const string FieldConfigurationModeId      = "FieldConfigurationModeId";
			public const string Name                          = "Name";
			public const string Value                         = "Value";
			public const string SystemEntityTypeId            = "SystemEntityTypeId";
			public const string SystemEntityType              = "SystemEntityType";
			public const string Width                         = "Width";
			public const string Formatting                    = "Formatting";
			public const string ControlType                   = "ControlType";
			public const string HorizontalAlignment           = "HorizontalAlignment";
			public const string GridViewPriority              = "GridViewPriority";
			public const string DetailsViewPriority           = "DetailsViewPriority";
			public const string FieldConfigurationDisplayName = "FieldConfigurationDisplayName";
		}

		public int? FieldConfigurationId            { get; set; }
		public string Name                          { get; set; }
		public string Value                         { get; set; }
		public int? SystemEntityTypeId              { get; set; }
		public Decimal? Width                       { get; set; }
		public string Formatting                    { get; set; }
		public string ControlType                   { get; set; }
		public string HorizontalAlignment           { get; set; }
		public int? GridViewPriority                { get; set; }
		public int? DetailsViewPriority             { get; set; }
		public int? FieldConfigurationModeId        { get; set; }
		public string FieldConfigurationDisplayName { get; set; }

		public string ToURLQuery()
		{
			return String.Empty; 
		}
	}
}
