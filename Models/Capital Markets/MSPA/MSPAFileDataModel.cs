using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
    public partial class MSPAFileDataModel : BaseModel
    {

        [PrimaryKey, IncludeInSearch]
        public int? MSPAFileId { get; set; }

        [IncludeInSearch] 
        public string Filename                  { get; set; }
        public DateTime? DropDate               { get; set; }
        public DateTime? BusinessDate           { get; set; }
        public int MSPAExtractTaskRunId         { get; set; }
        public int MSPAHoldingTaskRunId         { get; set; }
        public int MSPATradeTaskRunId           { get; set; }
        public int MSPASecurityTaskRunId        { get; set; }       

    }
}
