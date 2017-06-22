using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace Framework.Components.LogAndTrace
{

    [Serializable]
    public class RenumberMigrationDataModel : BaseDataModel
    {

        public int?      RenumberMigrationId    { get; set; }
        public int?      SystemEntityTypeId     { get; set; }
        public string    SystemEntityType       { get; set; }
        public int?      OriginalKey            { get; set; }
        public int?      MigratedKey            { get; set; }       
        public DateTime? RecordDate             { get; set; }
        

        public class DataColumns : BaseDataColumns
        {

            public const string RenumberMigrationId  = "RenumberMigrationId";
            public const string SystemEntityTypeId   = "SystemEntityTypeId";
            public const string SystemEntityType     = "SystemEntityType";
            public const string OriginalKey          = "OriginalKey";
            public const string MigratedKey          = "MigratedKey";           
            public const string RecordDate           = "RecordDate";        

        }

    }

} 
