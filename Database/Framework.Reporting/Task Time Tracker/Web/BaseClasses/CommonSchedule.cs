using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace TaskTimeTracker.UI.Web.BaseClasses
{
	public class CommonSchedule : System.Web.UI.MasterPage
	{

		public const int applicationId = 100047;

		public int ApplicationId
		{
			get
			{
				return applicationId;
			}
		}

		public string TableName { get; set; }

		public Menu MainMenu { get; set; }

		public Shared.UI.Web.Controls.SubMenu.SubMenu SubMenuObject { get; set; }

        public Shared.UI.Web.Controls.BreadCrumb.BreadCrumb BreadCrumbObject { get; set; }

        

        public virtual void Setup(string tableName) { }
	}
}