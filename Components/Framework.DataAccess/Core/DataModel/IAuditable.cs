using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Components.DataAccess.DataModel
{
	interface IAuditable
	{
		DateTime CreatedDate { get; set; }
		int CreatedByAuditId { get; set; }
		DateTime UpdatedDate { get; set; }
		int ModifiedByAuditId { get; set; }
	}
}
