using System;
using System.IO;
using Generators.Generator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestCommon
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			var t = typeof (DataModel.TaskTimeTracker.ClientDataModel);

			var o = Demo.GenerateInsertStoredProcedure(t);

			using (var outputFile = new StreamWriter(@"C:\temp\" + System.DateTime.Now.ToFileTimeUtc() + "." + t.Name + ".sql"))
			{
				outputFile.Write(o);
			}
		}
	}
}
