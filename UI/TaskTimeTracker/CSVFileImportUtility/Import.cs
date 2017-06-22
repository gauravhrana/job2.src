using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;
using Microsoft.VisualBasic.FileIO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataModel.CapitalMarkets;
using System.Reflection;
using System.Collections;
using System.Diagnostics;
using Shared.UI.WebFramework;
using System.Configuration;
using Newtonsoft.Json;
using DayCare.Components.BusinessLayer;
using Framework.Components.ApplicationUser;
using Framework.Components.DataAccess;

namespace CSVFileImportUtility
{
	public partial class Import : Form
	{
		public Import()
		{
			InitializeComponent();

			SetupConfiguration.SetConnectionList(5);

			var requestProfile = new RequestProfile(5, 1, 200);

			var items = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(requestProfile);

			cbApplication.DataSource	= items.ToArray();
			cbApplication.ValueMember	="ApplicationId";
			cbApplication.DisplayMember	="Name" + "Code";
			
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
			if (!CheckCsvIsValid())
			{
				lblStatus.Text = "Invalid CSV file";
				return;
			}
			SetupConfiguration.SetConnectionList(5);

			var result = new List<string>();

			string[] header = null;

			var appfilepath = GetApplicationStartUpPath();

			var EntityName = Path.GetFileNameWithoutExtension(textBox1.Text);

			var entityDataModel = EntityName + "DataModel";

			var objDataModel = CreateClassInstance(entityDataModel);

			var objType = objDataModel.GetType();

			var objProps = objType.GetProperties();

			var entityDataManager = EntityName + "DataManager";

			var objDataManager = CreateClassInstance(entityDataManager);

			MethodInfo methodInfoInsert = objDataManager.GetType().GetMethod("Create");

			MethodInfo methodInfoUpdate = objDataManager.GetType().GetMethod("Update");

			MethodInfo methodInfoDelete = objDataManager.GetType().GetMethod("Delete");

			MethodInfo methodInfoDoesExist = objDataManager.GetType().GetMethod("DoesExist");
			
			var applicationCode = ((DataModel.Framework.AuthenticationAndAuthorization.ApplicationDataModel)(cbApplication.SelectedItem)).Code;

			var applicationId = Convert.ToInt32(cbApplication.SelectedValue);

			var auditId = Convert.ToInt32(ConfigurationManager.AppSettings[applicationCode + ".SystemAuditId"]);

			var requestProfile = new RequestProfile(auditId, 1, applicationId);

			RemoveDuplicateRows(EntityName);

			using (var reader = new System.IO.StreamReader(System.IO.File.OpenRead(Path.Combine(Application.StartupPath, "Upload", EntityName + ".CSV"))))
			{
				var linecount = 0;

				var incrementSort = 1;
				var incrementId = 1;

				StringBuilder sb = new StringBuilder();

				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();

					// skip header
					if (linecount == 0)
					{
						header = line.Split(',');

						sb.AppendLine(string.Join(",", header) + ",Exception");

						linecount++;
						if (header[0].Contains("Id") && !header[0].Equals("Id"))
						{
							var entityNametoReplace = header[0].Substring(0, header[0].Length - 2);

							header = header.Select(s => s.Replace(entityNametoReplace, EntityName)).ToArray();
						}

						continue;
					}

					var values = line.Split(',');

					var data = CreateClassInstance(entityDataModel);

					foreach (var propInfo in objProps)
					{
						for (var j = 0; j < header.Count(); j++)
						{
							// executes when CSV file header is equal to DataModel columns names

							if (propInfo.Name == header[j] && propInfo.Name != "SortOrder")
							{
                                if (propInfo.PropertyType.ToString() == "System.Nullable`1[System.Int32]" || propInfo.PropertyType.ToString() == "System.Int32")
								{
									propInfo.SetValue(data, Convert.ToInt32(values[j]));
								}
                                else if (propInfo.PropertyType.ToString() == "System.Nullable`1[System.Decimal]" || propInfo.PropertyType.ToString() == "System.Decimal")
                                {
                                    propInfo.SetValue(data, Convert.ToDecimal(values[j]));
                                }

								else if (propInfo.PropertyType.ToString() == "System.Nullable`1[System.DateTime]" || propInfo.PropertyType.ToString() == "System.DateTime")
								{
									propInfo.SetValue(data, Convert.ToDateTime(values[j]));
								}
								else
								{
									propInfo.SetValue(data, Regex.Replace(values[j], "['\"]+", "", RegexOptions.Compiled).Trim());
								}
							}
							// executes when CSV file header is not equal to DataModel columns names

							else if (header[j].Equals(EntityName + "Id") && propInfo.Name.Equals(EntityName + "Id"))
							{
								propInfo.SetValue(data, Convert.ToInt32(values[j]));
							}

							else if ((header[j].Equals(EntityName + "Code") && propInfo.Name.Equals("Name")) || (header[j].Equals(EntityName + "Code") && propInfo.Name.Equals("Code")) || (propInfo.Name.Equals("Name") && header[j].Equals("Code") && !header.Equals("Name")) || (propInfo.Name.Equals("Description") && header[j].Equals("Name") && !header.Equals("Description")) || (propInfo.Name.Equals("Code") && header[j].Equals("Name") && !header.Equals("Code")))
							{
								propInfo.SetValue(data, values[j].Trim());
							}
							else if (header[j].Equals(EntityName + "Desc") && propInfo.Name.Equals("Description"))
							{
								propInfo.SetValue(data, Regex.Replace(values[j], "['\"]+", "", RegexOptions.Compiled).Trim());
							}
							else if (propInfo.Name.Equals("Url"))
							{
								propInfo.SetValue(data, "http://localhost:53331/CapitalMarkets/Home#/");
							}
							else if ((header[j].Equals("NameSort") && propInfo.Name.Equals("Code")))
							{
								propInfo.SetValue(data, values[j].Trim());
							}
							else if ((header[j].Equals("NameLine1") && propInfo.Name.Equals("Name")) || header[j].Equals("NameLine1") && propInfo.Name.Equals("Description"))
							{
								propInfo.SetValue(data, values[j].Trim());
							}

						}
						if (propInfo.Name == "SortOrder")
						{
							propInfo.SetValue(data, incrementSort);
							incrementSort++;
						}

						if (propInfo.Name == EntityName + "Id")
						{
							if (!header.Contains(EntityName + "Id") && values[0].Equals("Insert"))
							{
								propInfo.SetValue(data, incrementId);
								incrementId++;
							}
							
						}
					}

					try
					{
						object[] dataObject = new object[2];

						dataObject[0] = Convert.ChangeType(data, data.GetType());

						dataObject[1] = Convert.ChangeType(requestProfile, requestProfile.GetType());

						if (header[0].Equals(ImportUtility.Constants.ApplicationConstants.Action))
						{
							if (values[0].Equals("Insert"))
							{
								methodInfoInsert.Invoke(objDataManager, dataObject);
							}
							else if (values[0].Equals("Update"))
							{
								var dtData = methodInfoDoesExist.Invoke(objDataManager, dataObject);

								if (((System.Data.DataTable)(dtData)).Rows.Count == 1)
								{
									var entityID = ((System.Data.DataTable)(dtData)).Rows[0].ItemArray[0];

									var propId = dataObject[0].GetType().GetProperties()[0];

									propId.SetValue(dataObject[0], entityID);

									methodInfoUpdate.Invoke(objDataManager, dataObject);
								}

								methodInfoUpdate.Invoke(objDataManager, dataObject);
							}
							else if (values[0].Equals("Delete"))
							{
								methodInfoDelete.Invoke(objDataManager, dataObject);
							}
						}
						else
						{
							methodInfoInsert.Invoke(objDataManager, dataObject);
						}
					}
					catch (Exception ex)
					{
						sb.AppendLine(string.Join(",", values) + "," + ex.InnerException.Message.Substring(0, ex.InnerException.Message.IndexOf("\r\n")));
					}

				}

				string exceptionPath = Path.Combine(Application.StartupPath, "Exception");

				bool exists = System.IO.Directory.Exists(exceptionPath);

				if (!exists)
					System.IO.Directory.CreateDirectory(exceptionPath);
				

				var filepath = Path.Combine(exceptionPath, EntityName + "_Exception.CSV");

				if (File.Exists(filepath))
					File.Delete(filepath);

				if (Regex.Matches(sb.ToString(), Environment.NewLine).Count > 1)
				{
					File.WriteAllText(Path.Combine(exceptionPath, EntityName + "_Exception.CSV"), sb.ToString(), Encoding.Default);
				}

				lblStatus.Text = "Upload Completed";
			}
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

		public string GetApplicationStartUpPath()
		{
			string subPath = Path.Combine(Application.StartupPath, "Upload");

			bool exists = System.IO.Directory.Exists(subPath);

			if (!exists)
				System.IO.Directory.CreateDirectory(subPath);

			return subPath;  
		}

		public bool CheckCsvIsValid()
		{
			using (var parser = new TextFieldParser(textBox1.Text)) 
			{
				parser.TextFieldType = FieldType.Delimited;
				parser.SetDelimiters(",");

				string[] line;
				while (!parser.EndOfData)
				{
					try
					{
						line = parser.ReadFields();
						if(line.Equals(""))
						{
							return false;
						}
					}
					catch (MalformedLineException ex)
					{
						
					}
				}
				return true;
			}
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

			string colName=string.Empty;

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
				//else if (column.ColumnName.Contains("Code"))
				//{
				//	colName = EntityName + "Code";
				//	break;
				//}

			}
			if (colName == "")
			{
				colName = dTable.Columns[1].ColumnName;
			}

			if (Lines[0].Split(',').First().Equals(ImportUtility.Constants.ApplicationConstants.Action))
			{
				var groups = from r in dTable.AsEnumerable()
							 group r by new
							 {
								 Col1 = r.Field<String>(ImportUtility.Constants.ApplicationConstants.Action).ToLower().Trim(),
								 Col2 = r.Field<String>(colName).ToLower().Trim(),
							 };

				dTable = groups.Select(g => g.First()).CopyToDataTable();
			}
			else
			{
				var groups = from r in dTable.AsEnumerable()
							 group r by new
							 {							 
								 Col1 = r.Field<String>(colName).ToLower().Trim(),
							 };

				dTable = groups.Select(g => g.First()).CopyToDataTable();
			}

			// if you only want the first row of each group:
			

			//foreach (DataRow drow in dTable.Rows)
			//{
			//	if (hTable.Contains(drow[colName].ToString().ToLower()))
			//		duplicateList.Add(drow);
			//	else
			//		hTable.Add(drow[colName].ToString().ToLower(), string.Empty);

			//}

			//foreach (DataRow dRow in duplicateList)
			//	dTable.Rows.Remove(dRow);

			StringBuilder sb = new StringBuilder();

			var columnNames = dTable.Columns.Cast<DataColumn>().Select(column =>  column.ColumnName).ToArray();
			sb.AppendLine(string.Join(",", columnNames));

			foreach (DataRow row in dTable.Rows)
			{
				var fields = row.ItemArray.Select(field => field.ToString()).ToArray();
				sb.AppendLine(string.Join(",", fields));
			}

			var appfilepath = GetApplicationStartUpPath();

			File.WriteAllText(Path.Combine(appfilepath, EntityName + ".CSV"), sb.ToString(), Encoding.Default);

			//string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(csv);
			//File.WriteAllText("C:\\Upload\\"+EntityName+".CSV", sb.ToString(), Encoding.Default);

			
		}		
		
		void ReferenceAssemblies()
		{
			CapitalMarkets.Components.BusinessLayer.MediumOfExchangeDataManager.DoesExist(null, null);
			Legal.Components.BusinessLayer.CounselDataManager.DoesExist(DataModel.Legal.CounselDataModel.Empty, null);
            ReferenceData.Components.BusinessLayer.AirportDataManager.DoesExist(null, null);
			Framework.Components.ApplicationUser.ApplicationDataManager.GetList(null);
			DayCare.Components.BusinessLayer.AccidentPlaceDataManager.GetList(null);
		}

		private void button4_Click(object sender, EventArgs e)
		{
			var EntityName = Path.GetFileNameWithoutExtension(textBox1.Text);

			//RemoveDuplicateRows(EntityName);

			string[] Lines = File.ReadAllLines(textBox1.Text);

			var appfilepath = GetApplicationStartUpPath();

			if (Lines.Length < 2) throw new InvalidDataException("Must have header line.");

			// Get headers.
			string[] headers = Lines.First().Split(new char[] { ',' });

			// Build JSON array.
			StringBuilder sb1 = new StringBuilder();
			sb1.AppendLine("[");
			for (int i = 1; i < Lines.Length; i++)
			{
				string[] fields = Lines[i].Split(new char[] { ',' });
				if (fields.Length != headers.Length) throw new InvalidDataException("Field count must match header count.");
				var jsonElements = headers.Zip(fields, (header, field) => string.Format("\"{0}\": \"{1}\"", header, field)).ToArray();
				string jsonObject = "{" + string.Format("{0}", string.Join(",", jsonElements)) + "}";
				if (i < Lines.Length - 1)
					jsonObject += ",";
				sb1.AppendLine(jsonObject);
			}
			sb1.AppendLine("]");

			File.WriteAllText(Path.Combine(appfilepath, EntityName + "_JSON.txt"), sb1.ToString(), Encoding.Default);

			lblStatus.Text = "File created";

		}

		private void button3_Click(object sender, EventArgs e)
		{
			SetupConfiguration.SetConnectionList(5);

			var result = new List<string>();

			string[] header = null;

			var appfilepath = GetApplicationStartUpPath();

			var Entity = Path.GetFileNameWithoutExtension(textBox1.Text);

			var EntityName = Entity.Substring(0, Entity.IndexOf("_"));

			var entityDataModel = EntityName + "DataModel";

			var objDataModel = CreateClassInstance(entityDataModel);

			var objType = objDataModel.GetType();

			var objProps = objType.GetProperties();

			var entityDataManager = EntityName + "DataManager";

			var objDataManager = CreateClassInstance(entityDataManager);

			MethodInfo methodInfoInsert = objDataManager.GetType().GetMethod("Create");

			var requestProfile = new RequestProfile(5, 1, 100068);

			var incrementSort = 1;

			using (StreamReader r = new StreamReader(System.IO.File.OpenRead(Path.Combine(appfilepath, EntityName + "_JSON.txt"))))
			{
				var data = CreateClassInstance(entityDataModel);
				string json = r.ReadToEnd();

				dynamic array = JsonConvert.DeserializeObject(json);

				foreach (var item in array)
				{
					foreach (var propInfo in objProps)
					{
						var name = propInfo.Name;

						if (((item as Newtonsoft.Json.Linq.JObject))[name] != null && ((item as Newtonsoft.Json.Linq.JObject))[name].ToString() != "SortOrder")
						{

							if (propInfo.PropertyType.ToString() == "System.Nullable`1[System.Int32]" || propInfo.PropertyType.ToString() == "System.Int32")
							{
								propInfo.SetValue(data, Convert.ToInt32(((item as Newtonsoft.Json.Linq.JObject))[name]));
							}
							else if (propInfo.PropertyType.ToString() == "System.String")
							{
								propInfo.SetValue(data, ((item as Newtonsoft.Json.Linq.JObject))[name].ToString());
							}
							else
							{
								propInfo.SetValue(data, ((item as Newtonsoft.Json.Linq.JObject))[name]);
							}

						}
						if (propInfo.Name == "SortOrder")
						{
							propInfo.SetValue(data, incrementSort);
							incrementSort++;
						}

					}

					object[] dataObject = new object[2];

					dataObject[0] = Convert.ChangeType(data, data.GetType());

					dataObject[1] = Convert.ChangeType(requestProfile, requestProfile.GetType());

					methodInfoInsert.Invoke(objDataManager, dataObject);

					lblStatus.Text = "Upload Completed";

				}
				
			}

		}

		private void ConcatComboValues(object sender, ListControlConvertEventArgs e)
		{
			string appName = ((DataModel.Framework.AuthenticationAndAuthorization.ApplicationDataModel)e.ListItem).Name;
			string appCode = ((DataModel.Framework.AuthenticationAndAuthorization.ApplicationDataModel)e.ListItem).Code;
			e.Value = appName + " - " + appCode;

		}

	}
	
}
