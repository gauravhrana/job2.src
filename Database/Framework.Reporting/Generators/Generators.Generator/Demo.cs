using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generators.Generator.SQLTemplate;

namespace Generators.Generator
{
	public class Demo
	{
		public static string GenerateInsertStoredProcedure(Type t)
		{
			var pageContent = new TemplateInsert();
		
			//pageContent.SetU(t);
			//pageContent.DoMagic();

			//pageContent.Session = new Microsoft.VisualStudio.TextTemplating.TextTemplatingSession();
			//pageContent.Session["TimesToRepeat"] = 5;

			//return pageContent.result;

			//pageContent.Session = new Dictionary<string, object>();
			//pageContent.Session.Add("MyType", t);
			//pageContent.Initialize();

			pageContent.MyType = t;

			return pageContent.TransformText();

			//return string.Empty;

		}
	}
}
