using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{
    public class SampleNonStdEntity2DataModel : BaseModel
    {

        public class DataColumns
        {
            public const string SampleNonStdEntity2Id    = "SampleNonStdEntity2Id";
            public const string Name                     = "Name";
            public const string SortOrder                = "SortOrder";

            public const string BathRoomId               = "BathRoomId";
            public const string BathRoom                 = "BathRoom";

            public const string CommentId                = "CommentId";
            public const string Comment                  = "Comment";

            public const string FieldConfigurationModeId = "FieldConfigurationModeId";
            public const string FieldConfigurationMode   = "FieldConfigurationMode";

            public const string RecordDate               = "RecordDate";
            public const string FromRecordDate           = "FromRecordDate";
            public const string ToRecordDate             = "ToRecordDate";
        }

        public static readonly SampleNonStdEntity2DataModel Empty = new SampleNonStdEntity2DataModel();

        [PrimaryKey, IncludeInSearch]
        public int? SampleNonStdEntity2Id { get; set; }

        [IncludeInSearch, IncludeInUnique]
        public string Name { get; set; }
        public int SortOrder { get; set; }

        [ForeignKey("BathRoom"), IncludeInSearch]
        public int? BathRoomId { get; set; }
        [ForeignKeyName("BathRoom", "BathRoomId", "BathRoomId", "Name"), OnlyProperty]
        public string BathRoom { get; set; }

        [ForeignKey("Comment"), IncludeInSearch]
        public int? CommentId { get; set; }
        [ForeignKeyName("Comment", "CommentId", "CommentId", "Name"), OnlyProperty]
        public string Comment { get; set; }

        [ForeignKey("FieldConfigurationMode", "Configuration"), IncludeInSearch]
        public int? FieldConfigurationModeId { get; set; }
        [ForeignKeyName("FieldConfigurationMode", "FieldConfigurationModeId", "FieldConfigurationModeId", "Name", "Configuration"), OnlyProperty]
        public string FieldConfigurationMode { get; set; }

        public DateTime? RecordDate { get; set; }

        [IncludeInSearch("RecordDate", "GreaterOrEqual"), OnlyProperty]
        public DateTime? FromRecordDate { get; set; }

        [IncludeInSearch("RecordDate", "LessOrEqual"), OnlyProperty]
        public DateTime? ToRecordDate { get; set; }
        
    }
}
