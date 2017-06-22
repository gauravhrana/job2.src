using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.ReleaseLog
{
    [Serializable]
	public class ReleaseLogDetailDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string ReleaseLogDetailId			= "ReleaseLogDetailId";
			public const string ReleaseLogId				= "ReleaseLogId";
			public const string ApplicationId				= "ApplicationId";
			public const string Application					= "Application";
			public const string ItemNo						= "ItemNo";
			public const string Description					= "Description";
			public const string SortOrder					= "SortOrder";
			public const string RequestedBy					= "RequestedBy";
			public const string PrimaryDeveloper			= "PrimaryDeveloper";
			public const string RequestedDate				= "RequestedDate";
			public const string ReleaseDate					= "ReleaseDate";
			public const string TimeSpent					= "TimeSpent";

			public const string ReleaseIssueTypeId			= "ReleaseIssueTypeId";
			public const string JIRA						= "JIRA";
			public const string Feature						= "Feature";
			public const string ModuleId					= "ModuleId";
			public const string ReleaseFeatureId			= "ReleaseFeatureId";
			public const string PrimaryEntity				= "PrimaryEntity";
			public const string SystemEntityTypeId          = "SystemEntityTypeId";
			public const string SystemEntityType            = "SystemEntityType";
			public const string ReleasePublishCategoryId	= "ReleasePublishCategoryId";
			
			public const string Module						= "Module";
			public const string ReleaseFeature				= "ReleaseFeature";
			public const string ReleaseIssueType			= "ReleaseIssueType";
			public const string ReleasePublishCategory		= "ReleasePublishCategory";
			public const string ReleaseLog					= "ReleaseLog";
			public const string ReleaseDateMin				= "ReleaseDateMin";
			public const string ReleaseDateMax				= "ReleaseDateMax";			
			public const string UpdatedDateRangeMin			= "UpdatedDateRangeMin";
			public const string UpdatedDateRangeMax			= "UpdatedDateRangeMax";
			public const string UpdatedRange				= "UpdatedRange";
			public const string IsUpdated					= "IsUpdated";
			public const int ReleaseNotesTimeSpentConstant  = 1;
		}

		public static readonly ReleaseLogDetailDataModel Empty = new ReleaseLogDetailDataModel();
		
		public int? ReleaseLogDetailId			{ get; set; }
		public int? ReleaseLogId				{ get; set; }
		public int? ItemNo						{ get; set; }
		public string Description				{ get; set; }
		public int? SortOrder					{ get; set; }
		public string RequestedBy				{ get; set; }
		public string PrimaryDeveloper			{ get; set; }
		public DateTime? RequestedDate			{ get; set; }
		public int? ReleaseIssueTypeId			{ get; set; }
		public string JIRA						{ get; set; }
		public string Feature					{ get; set; }
		public int? ModuleId					{ get; set; }
		public int? ReleaseFeatureId			{ get; set; }
		public string PrimaryEntity				{ get; set; }
		public int? SystemEntityTypeId			{ get; set; }

		public int? ReleasePublishCategoryId	{ get; set; }
		public string ReleasePublishCategory	{ get; set; }
		public string TimeSpent					{ get; set; }
		
		public string Application				{ get; set; }
		public string ReleaseIssueType			{ get; set; }
		public string ReleaseLog				{ get; set; }
		public string Module					{ get; set; }
		public string ReleaseFeature			{ get; set; }
		public string SystemEntityType			{ get; set; }
		public DateTime? ReleaseDateMin			{ get; set; }
		public DateTime? ReleaseDateMax			{ get; set; }
		public DateTime? ReleaseDate			{ get; set; }
		public DateTime? UpdatedDateRangeMin	{ get; set; }
		public DateTime? UpdatedDateRangeMax	{ get; set; }

	}
}
