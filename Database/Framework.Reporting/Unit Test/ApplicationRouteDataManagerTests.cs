using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.Core;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework.Components.Core.Tests
{
	[TestClass()]
	public class ApplicationRouteDataManagerTests
	{
		static RequestProfile RequestProfileItem = new RequestProfile();

		[ClassInitialize()]
		public static void ClassInit(TestContext context)
		{
			var systemAuditId = 100006;

			SetupConfiguration.SetConnectionList(systemAuditId);

			RequestProfileItem.ApplicationId = 100;
			RequestProfileItem.AuditId = 5;
		}

		[TestInitialize()]
		public void Initialize() {}

		[TestCleanup()]
		public void Cleanup() {}

		[ClassCleanup()]
		public static void ClassCleanup() {}


		[TestMethod()]
		public void ToSQLParameterTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void GetListTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void GetDetailsTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void GetEntityDetailsTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void CreateTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void UpdateTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void DeleteTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void SearchTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void UpdateRoute()
		{
			var oApplicationRouteDataModel = new ApplicationRouteDataModel();

			oApplicationRouteDataModel.EntityName = "Client";

			var items = ApplicationRouteDataManager.GetEntityDetails(oApplicationRouteDataModel, RequestProfileItem);

			foreach (var dataItem in items)
			{
				if(dataItem.RouteName.Equals(dataItem.EntityName + "EntityRoute"))
				{
					dataItem.RelativeRoute = string.Format("~/PMO/{0}/{{action}}.aspx", dataItem.EntityName);
				}
				else if(dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSuperKey"))
				{
					dataItem.RelativeRoute = string.Format("~/PMO/{0}/{{action}}.aspx", dataItem.EntityName);
				}
				else if(dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSearch"))
				{
					dataItem.RelativeRoute = string.Format("~/PMO/{0}/Default.aspx", dataItem.EntityName);
				}
			
				ApplicationRouteDataManager.Update(dataItem, RequestProfileItem);
			}
		}

		[TestMethod()]
		public void DoesExistTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void IsDeletableTest()
		{
			Assert.Fail();
		}
	}
}
