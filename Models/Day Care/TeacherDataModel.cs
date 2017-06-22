using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{
    public partial class TeacherDataModel : BaseModel
    {
        [PrimaryKey, IncludeInSearch]    
        public int? TeacherId { get; set; }

        [IncludeInSearch, IncludeInUnique]
        public string Name { get; set; }

        [IncludeInSearch]
        public string Description { get; set; }

        public int? SortOrder { get; set; }
    }
}