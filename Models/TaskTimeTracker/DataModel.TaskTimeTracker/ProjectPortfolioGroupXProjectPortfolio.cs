using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataModel.TaskTimeTracker
{
    [Serializable]
    [Table("ProjectPortfolioGroupXProjectPortfolio")]
	public class ProjectPortfolioGroupXProjectPortfolioDataModel : BaseDataModel
	{
		public class DataColumns : BaseDataColumns
		{
			public const string ProjectPortfolioGroupXProjectPortfolioId = "ProjectPortfolioGroupXProjectPortfolioId";
			public const string ProjectPortfolioGroupId                  = "ProjectPortfolioGroupId";
			public const string ProjectPortfolioId                       = "ProjectPortfolioId";
			public const string Description                              = "Description";
			public const string SortOrder                                = "SortOrder";			
			
			public const string ProjectPortfolioGroup                    = "ProjectPortfolioGroup";
			public const string ProjectPortfolio                         = "ProjectPortfolio";
		}

        public static readonly ProjectPortfolioGroupXProjectPortfolioDataModel Empty = new ProjectPortfolioGroupXProjectPortfolioDataModel();

        [Key]
		public int?     ProjectPortfolioGroupXProjectPortfolioId { get; set; }
		public int?     ProjectPortfolioGroupId                  { get; set; }
		public int?     ProjectPortfolioId                       { get; set; }
		public string   Description                              { get; set; }
		public int?     SortOrder                                { get; set; }		
		
		public string   ProjectPortfolioGroup                    { get; set; }
		public string   ProjectPortfolio                         { get; set; }

	}
}
