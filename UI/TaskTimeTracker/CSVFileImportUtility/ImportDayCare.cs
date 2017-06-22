using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataModel.CapitalMarkets;
using System.Reflection;
using System.Collections;
using Framework.Components.DataAccess;


namespace CSVFileImportUtility
{
	public partial class ImportDayCare : Form
	{
		public ImportDayCare()
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
				MessageBox.Show("No such entity exists in DayCare aplication");
				return;
			}

			var objType = objDataModel.GetType();

			var objProps = objType.GetProperties();

			var entityDataManager = EntityName + "DataManager";

			var objDataManager = CreateClassInstance(entityDataManager);

			MethodInfo methodInfo = null;

			if (EntityName == "ScheduleState")
			{
				methodInfo = objDataManager.GetType().GetMethod("CreateCSVData");
			}
			else
			{
				methodInfo = objDataManager.GetType().GetMethod("Create");
			}			
			
			var requestProfile = new RequestProfile(17883, 17884, 200);

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
									propInfo.SetValue(data, "http://localhost:53331/DayCare/Home#/");
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

					object[] dataObj=null;

					if (EntityName == "ScheduleState")
					{
						dataObj = new object[3];

						dataObj[0] = Convert.ChangeType(data, data.GetType());

						dataObj[1] = Convert.ChangeType(requestProfile, requestProfile.GetType());

						dataObj[2] = Convert.ChangeType("DayCare", typeof(String));
					}

					else
					{
						dataObj = new object[2];

						dataObj[0] = Convert.ChangeType(data, data.GetType());

						dataObj[1] = Convert.ChangeType(requestProfile, requestProfile.GetType());
					}

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


		public static List<string[]> parseCSV(string path)
		{
			List<string[]> parsedData = new List<string[]>(); ;

			try
			{
				using (StreamReader readFile = new StreamReader(path))
				{
					string line;

					string[] row;

					while ((line = readFile.ReadLine()) != null)
					{
						row = line.Split(',');

						parsedData.Add(row);
					}
				}
			}

			catch (Exception e)
			{
				Console.Out.WriteLine(e);
			}

			return parsedData;
		}

		public void RemoveDuplicateRows(string EntityName)
		{
			DataTable dTable = new DataTable();

			string[] Lines = File.ReadAllLines(textBox1.Text);

			string[] Fields;

			Fields = Lines[0].Split(new char[] { ',' });

			if (Fields[0].Contains("Id") && !Fields[0].Equals("Id"))
			{
				var entityNametoReplace1 = Lines[0].Substring(0, Fields[0].Length - 2);

				Fields = Fields.Select(s => s.Replace(entityNametoReplace1, EntityName)).ToArray();
			}

			int Cols = Fields.GetLength(0);

			//1st row must be column names; force lower case to ensure matching later on.
			for (int i = 0; i < Cols; i++)

				dTable.Columns.Add(Regex.Replace(Fields[i], "['\"]+", "", RegexOptions.Compiled), typeof(string));

			DataRow Row;

			for (int i = 1; i < Lines.GetLength(0); i++)
			{
				Fields = Lines[i].Split(new char[] { ',' });
				Row = dTable.NewRow();
				for (int f = 0; f < Cols; f++)
					Row[f] = Regex.Replace(Fields[f], "['\"]+", "", RegexOptions.Compiled);
				dTable.Rows.Add(Row);

			}

			Hashtable hTable = new Hashtable();

			ArrayList duplicateList = new ArrayList();

			DataColumnCollection columns = dTable.Columns;

			string colName = string.Empty;

			foreach (DataColumn column in columns)
			{
				if (column.ColumnName.Equals("Name"))
				{
					colName = "Name";
					break;
				}
				else if (column.ColumnName.Equals("Code"))
				{
					colName = "Code";
					break;
				}
				else if (column.ColumnName.Contains("Code"))
				{
					colName = EntityName + "Code";
					break;
				}

			}

			foreach (DataRow drow in dTable.Rows)
			{
				if (hTable.Contains(drow[colName].ToString().ToLower()))
					duplicateList.Add(drow);
				else
					hTable.Add(drow[colName].ToString().ToLower(), string.Empty);

			}

			foreach (DataRow dRow in duplicateList)
				dTable.Rows.Remove(dRow);

			StringBuilder sb = new StringBuilder();

			var columnNames = dTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToArray();
			sb.AppendLine(string.Join(",", columnNames));

			foreach (DataRow row in dTable.Rows)
			{
				var fields = row.ItemArray.Select(field => field.ToString()).ToArray();
				sb.AppendLine(string.Join(",", fields));
			}

			File.WriteAllText("C:\\Upload\\" + EntityName + ".CSV", sb.ToString(), Encoding.Default);

		}

		void ReferenceAssemblies()
		{
		     TaskTimeTracker.Components.Module.TimeTracking.ScheduleStateDataManager.DoesExist(DataModel.TaskTimeTracker.TimeTracking.ScheduleStateDataModel.Empty, null);
		}

	}

}
