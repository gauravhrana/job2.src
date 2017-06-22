using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Web;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataModel.CapitalMarkets;
using System.Reflection;
using DayCare.Components.BusinessLayer;
using Framework.Components.DataAccess;
using System.Text.RegularExpressions;

namespace CSVFileImportUtility
{
	public partial class ImportLegal : Form
	{
		public ImportLegal()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog1 = new OpenFileDialog();

			DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
			if (result == DialogResult.OK) // Test result.
			{
				textBox1.Text = openFileDialog1.FileName;				
			}			
		}

		private void button2_Click(object sender, EventArgs e)
		{
			SetupConfiguration.SetConnectionList(5);

			var result = new List<string>();

			string[] header = null;

			var EntityName = Path.GetFileNameWithoutExtension(textBox1.Text);

			var entityDataModel = EntityName + "DataModel";

			var objDataModel = CreateClassInstance(entityDataModel);

            if (objDataModel == null)
            {
                MessageBox.Show("No such entity exists in legal aplication");
                return;
            }

			var objType = objDataModel.GetType();
			var objProps = objType.GetProperties();

			var entityDataManager = EntityName + "DataManager";
			var objDataManager = CreateClassInstance(entityDataManager);

			MethodInfo methodInfo = objDataManager.GetType().GetMethod("Create");

            var requestProfile = new RequestProfile(37480, 37481, 100072);			

			using (var reader = new System.IO.StreamReader(System.IO.File.OpenRead(textBox1.Text)))
			{
				var linecount = 0;

				var sortOrder = 1;

				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();

					// skip header
					if (linecount == 0)
					{
						header = line.Split(',');
						linecount++;
						continue;
					}

					var values = line.Split(',');

					var data = CreateClassInstance(entityDataModel);

					foreach (var propInfo in objProps)
					{
                        if (propInfo.Name == "SortOrder")
                        {
                            propInfo.SetValue(data, sortOrder);
                            sortOrder++;
                        }
                        else
                        {
                            for (var j = 0; j < header.Count(); j++)
                            {
                                if (propInfo.Name == header[j] && propInfo.Name != "SortOrder")
                                {
                                    if (propInfo.PropertyType.ToString() == "System.Nullable`1[System.Int32]")
                                    {
                                        propInfo.SetValue(data, Convert.ToInt32(values[j]));
                                    }
                                    else
                                    {
                                        propInfo.SetValue(data, Regex.Replace(values[j], "['\"]+", "", RegexOptions.Compiled));
                                    }
                                }
                                else if ((header[j].Equals(EntityName + "Code") && propInfo.Name.Equals("Name")) || (header[j].Equals(EntityName + "Code") && propInfo.Name.Equals("Code")))
                                {
                                    propInfo.SetValue(data, values[j]);
                                }
                                else if (header[j].Equals(EntityName + "Desc") && propInfo.Name.Equals("Description"))
                                {
                                    propInfo.SetValue(data, Regex.Replace(values[j], "['\"]+", "", RegexOptions.Compiled));
                                }
                                else if (propInfo.Name.Equals("Url"))
                                {
                                    propInfo.SetValue(data, "http://localhost:53331/Legal/Home#/");
                                }
                            }
                        }
                        if (propInfo.Name.Equals("Description"))
                        {
                            var propValue = propInfo.GetValue(data);
                            if (string.IsNullOrEmpty(Convert.ToString(propValue)))
                            {
                                if (data is StandardModel)
                                {
                                    if (string.IsNullOrEmpty(((StandardModel)data).Description) && !string.IsNullOrEmpty(((StandardModel)data).Name))
                                    {
                                        ((StandardModel)data).Description = ((StandardModel)data).Name;
                                    }
                                }
                                else
                                {
                                    var x = header.ToList().IndexOf("Name");
                                    if (x != -1)
                                    {
                                        propInfo.SetValue(data, Regex.Replace(values[x], "['\"]+", "", RegexOptions.Compiled));
                                    }
                                }
                            }
                        }
					}
					
					object[] dataObj = new object[2];

					dataObj[0] = Convert.ChangeType(data, data.GetType());

					dataObj[1] = Convert.ChangeType(requestProfile, requestProfile.GetType());

					methodInfo.Invoke(objDataManager, dataObj);					
				}

			}

            MessageBox.Show("Upload Completed");

		}

		private static object CreateClassInstance(string className)
		{
			Type type = null;

			var currentAssembly = Assembly.GetExecutingAssembly();
			var assembliyNames = currentAssembly.GetReferencedAssemblies();

			foreach (var aName in assembliyNames)
			{
				try
				{
					var assembly = Assembly.Load(aName);
					type = assembly.GetTypes().First(t => t.Name == className);
					return Activator.CreateInstance(type);
				}
				catch { }
			}

			return null;
		}

		void ReferenceAssemblies()
		{
            Legal.Components.BusinessLayer.CounselDataManager.DoesExist(DataModel.Legal.CounselDataModel.Empty, null);
		}

	}
	
}
