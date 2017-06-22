using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Framework.Components.DataAccess
{
	public class RequestProfile
	{
		public int AuditId { get; set; }
		public int ApplicationModeId { get; set; }
		public int ApplicationId { get; set; }

		public RequestProfile()
		{
			AuditId = -1;
			ApplicationModeId = -1;
			ApplicationId = -1;
		}

		public RequestProfile(int auditId, int applicationModeId, int applicationId)
		{
			AuditId = auditId;
			ApplicationModeId = applicationModeId;
			ApplicationId = applicationId;
		}
	}
}
