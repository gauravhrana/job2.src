using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataModel.TaskTimeTracker.TimeTracking
{

    [Serializable]
    public class JiraDataModel : BaseDataModel
    {
        public class DataColumns
        {
            public const string Title              = "Title";
            public const string Type               = "Type";
            public const string Status             = "Status";
            public const string WorkTicket         = "WorkTicket";
            public const string Priority           = "Priority";
            public const string Description        = "Description";
            public const string TotalHoursByPerson = "TotalHoursByPerson";
            public const string TotalHoursByTicket = "TotalHoursByTicket";
            public const string DueDate            = "DueDate";
            public const string Assignee           = "Assignee";
            public const string Project            = "Project";
			public const string PersonId		   = "PersonId";

            public const string FromUpdatedDate    = "FromUpdatedDate";
            public const string ToUpdatedDate      = "ToUpdatedDate";

					}

        public static readonly JiraDataModel Empty = new JiraDataModel();

        //Work Ticket, Priority,  Description,  Total Hours By Person, Total Hours By Ticket, DueDate

        public string       Title               { get; set; }
        public string       Assignee            { get; set; }
        public string       Type                { get; set; }
        public string       Status              { get; set; }
        public string       WorkTicket          { get; set; }
        public string       Priority            { get; set; }
        public string       Project             { get; set; }
        public string       Description         { get; set; }
        public string       TotalHoursByPerson  { get; set; }
        public string       TotalHoursByTicket  { get; set; }        
        public DateTime?    DueDate             { get; set; }
		public int			PersonId			{ get; set; }

        public DateTime?    FromUpdatedDate     { get; set; }
        public DateTime?    ToUpdatedDate       { get; set; }

    }
}
