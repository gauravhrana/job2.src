using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Components.DataAccess
{
    
    public class PrimaryKey : Attribute
    {
    }

    public class ForeignKey : Attribute
    {
        public string SourceTable;
        public string SourceDatabase;

        public ForeignKey(string srcTable)
        {
            this.SourceTable = srcTable;
        }

        public ForeignKey(string srcTable, string srcDatabase)
        {
            this.SourceTable = srcTable;
            this.SourceDatabase = srcDatabase;
        }
    }

    public class ForeignKeyName : Attribute
    {
        public string SourceTable;
        public string SourceDatabase;
        public string SourceTextColumn;
        public string JoinColumn;
        public string ForiegnKeyColumn;

        public ForeignKeyName(string srcTable, string foreignKeyColumn, string joinColumn)
        {
            this.SourceTable = srcTable;
            this.ForiegnKeyColumn = foreignKeyColumn;
            this.JoinColumn = joinColumn;
        }

        public ForeignKeyName(string srcTable, string foreignKeyColumn, string joinColumn, string srcColumn)
        {
            this.SourceTable = srcTable;
            this.ForiegnKeyColumn = foreignKeyColumn;
            this.SourceTextColumn = srcColumn;
            this.JoinColumn = joinColumn;
        }

        public ForeignKeyName(string srcTable, string foreignKeyColumn, string joinColumn, string srcColumn, string srcDatabase)
        {
            this.SourceTable = srcTable;
            this.ForiegnKeyColumn = foreignKeyColumn;
            this.SourceDatabase = srcDatabase;
            this.JoinColumn = joinColumn;
            this.SourceTextColumn = srcColumn;
        }

    }

    public class IncludeInSearch : Attribute
    {
        public string SearchFilterCondition;
        public string SearchColumn;

        public IncludeInSearch()
        {
            this.SearchFilterCondition = string.Empty;
            this.SearchColumn = string.Empty;
        }

        public IncludeInSearch(string searchColumn, string searchFilterCondition)
        {
            this.SearchColumn = searchColumn;
            this.SearchFilterCondition = searchFilterCondition;
        }
    }

    public class DateRange : Attribute
    {
        public string SearchColumn;
        public string FromSearchColumn;
        public string ToSearchColumn;

        public DateRange()
        {
            this.SearchColumn = string.Empty;
            this.FromSearchColumn = string.Empty;
            this.ToSearchColumn = string.Empty;
        }

        public DateRange(string searchColumn, string fromSearchColumn, string toSearchColumn)
        {
            this.SearchColumn = searchColumn;
            this.FromSearchColumn = fromSearchColumn;
            this.ToSearchColumn = toSearchColumn;
        }

        public DateRange(string fromSearchColumn, string toSearchColumn)
        {
            this.FromSearchColumn = fromSearchColumn;
            this.ToSearchColumn = toSearchColumn;
        }
    }

	public enum DateTimeTypeEnum
	{
		DateTime
	 ,  Time
	 ,  Date
	}

    public class IncludeInUnique : Attribute
    {
    }

    public class OnlyProperty : Attribute
    {
    }

	public class AuditProperty : Attribute
	{
	}

	public class SearchProperty : Attribute
	{
	}

	public class DateTimeType : Attribute
	{
		public DateTimeTypeEnum Type;
		public DateTimeType(DateTimeTypeEnum type)
        {
			this.Type = type;
		 }
	}
}

