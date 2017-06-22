using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{
    public class SampleNonStdEntityDataModel : BaseModel
    {
        public class DataColumns
        {
            public const string SampleNonStdEntityId = "SampleNonStdEntityId";
            public const string Name                 = "Name";
            public const string SortOrder            = "SortOrder";
            public const string BathRoomId           = "BathRoomId";
            public const string BathRoom             = "BathRoom";
            public const string CommentId            = "CommentId";
            public const string Comment              = "Comment";
        }

        public static readonly SampleNonStdEntityDataModel Empty = new SampleNonStdEntityDataModel();

        [PrimaryKey]
        public int? SampleNonStdEntityId { get; set; }

        public string Name { get; set; }
        public int SortOrder { get; set; }

        [ForeignKey("BathRoom")]
        public int? BathRoomId { get; set; }
        //[ForeignKeyName("BathRoom", "BathRoomId")]
        public string BathRoom { get; set; }

        [ForeignKey("Comment")]
        public int? CommentId { get; set; }
        //[ForeignKeyName("Comment", "CommentId")]
        public string Comment { get; set; }

    }
}

