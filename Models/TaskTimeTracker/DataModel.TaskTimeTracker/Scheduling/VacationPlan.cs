using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Framework.Components.DataAccess;
using Newtonsoft.Json;

namespace DataModel.TaskTimeTracker
{
    [Serializable]
    public class VacationPlanDataModel : StandardDataModel
    {
        public class DataColumns : StandardDataColumns
        {
            public const string VacationPlanId = "VacationPlanId";
            public const string ApplicationUserId = "ApplicationUserId";
            public const string StartDate = "StartDate";
            public const string EndDate = "EndDate";
            public const string EndDate2 = "EndDate2";
            public const string StartDate2 = "StartDate2";
        }

        public static readonly VacationPlanDataModel Empty = new VacationPlanDataModel();

        [PrimaryKey, IncludeInSearch]
        public int? VacationPlanId { get; set; }

        [IncludeInUnique]
        public int? ApplicationUserId { get; set; }

        [IncludeInUnique]
        public string ApplicationUser { get; set; }

        [JsonConverter(typeof(NullableDateConverter)), OnlyProperty]
        public DateTime? StartDate { get; set; }

		[JsonConverter(typeof(NullableDateConverter)), SearchProperty]
        public DateTime? StartDateR2 { get; set; }

        [JsonConverter(typeof(NullableDateConverter)), OnlyProperty]
        public DateTime? EndDate { get; set; }

		[JsonConverter(typeof(NullableDateConverter)), SearchProperty]
        public DateTime? EndDateR2 { get; set; }

    }
}
