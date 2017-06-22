using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Components.DataAccess.DataModel
{
	public class BaseAuditDataModel : BaseModel, IAuditable
	{
		public DateTime CreatedDate { get; set; }
		public int  CreatedByAuditId { get; set; }
		public DateTime UpdatedDate { get; set; }
		public int  ModifiedByAuditId { get; set; }
	}
}
