using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Configuration
{

    [Serializable]
    public class FieldConfigurationDataModel : StandardDataModel
	{
		
        public class DataColumns
		{
			public const string FieldConfigurationId          = "FieldConfigurationId";
			public const string ApplicationId				  = "ApplicationId";
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
            public const string DisplayColumn                 = "DisplayColumn";
			public const string CellCount                     = "CellCount";
			public const string FieldConfigurationMode        = "FieldConfigurationMode";

		}

		public static readonly FieldConfigurationDataModel Empty = new FieldConfigurationDataModel();

		public int? FieldConfigurationId            { get; set; }
		public int? ApplicationId					{ get; set; }
		public string Name                          { get; set; }
		public string Value                         { get; set; }
		public int? SystemEntityTypeId              { get; set; }
		public string SystemEntityType				{ get; set; }
		public Decimal? Width                       { get; set; }
		public string Formatting                    { get; set; }
		public string ControlType                   { get; set; }
		public string HorizontalAlignment           { get; set; }
		public int? GridViewPriority                { get; set; }
		public int? DetailsViewPriority             { get; set; }
		public int? FieldConfigurationModeId        { get; set; }
		public string FieldConfigurationDisplayName { get; set; }
        public int? DisplayColumn                   { get; set; }
		public int? CellCount						{ get; set; }
        public string FieldConfigurationMode        { get; set; }

	}

}
