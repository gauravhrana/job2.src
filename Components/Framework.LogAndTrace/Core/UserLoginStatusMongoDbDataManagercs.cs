using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using DataModel.Framework.LogAndTrace;
using Framework.Components.DataAccess;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoRepository;


namespace Framework.Components.LogAndTrace
{

	public partial class UserLoginStatusMongoDbDataManager : BaseDataManager
	{
		static IMongoDatabase database = null;

		static UserLoginStatusMongoDbDataManager()
		{
			var client = new MongoClient("mongodb://IVR-DEV-MONGO");

            database = client.GetDatabase("LoggingAndTrace");

		}

        public static List<UserLoginStatusDataModel> GetList()
		{
            var resultList = new List<UserLoginStatusDataModel>();
			var userLogin = database.GetCollection<BsonDocument>("UserLoginStatus");

            var filter = new BsonDocument();
						
			DataTable dt = new DataTable();
            foreach (BsonDocument obj in userLogin.Find(filter).ToListAsync().Result) 
			{
                resultList.Add(UserLoginStatusMongoDbDataManager.MapToUserLoginStatusRecord(obj));
			}
            return resultList; 

		}

        public static List<UserLoginStatusDataModel> GetList(UserLoginStatusDataModel data, RequestProfile requestProfile)
        {
            var resultList = new List<UserLoginStatusDataModel>();
			var statusData = database.GetCollection<BsonDocument>("UserLoginStatus");
            var listUserLoginStatus = new List<BsonDocument>();

            var builder = Builders<BsonDocument>.Filter;

			if (string.IsNullOrEmpty(data.Name) && string.IsNullOrEmpty(data.ApplicationId.ToString()))
			{
                resultList = GetList();
			}
			else
			{
                if (!string.IsNullOrEmpty(data.Name) && !string.IsNullOrEmpty(data.ApplicationId.ToString()))
                {
                    var filter = builder.Eq(UserLoginStatusDataModel.DataColumns.Name, data.Name) & 
                        builder.Eq(UserLoginStatusDataModel.DataColumns.ApplicationId, data.ApplicationId);

                    listUserLoginStatus = statusData.Find(filter).ToListAsync().Result;
                }
                else if (!string.IsNullOrEmpty(data.Name) || !string.IsNullOrEmpty(data.ApplicationId.ToString()))
                {
                    var filter = builder.Eq(UserLoginStatusDataModel.DataColumns.Name, data.Name) |
                        builder.Eq(UserLoginStatusDataModel.DataColumns.ApplicationId, data.ApplicationId);
                    listUserLoginStatus = statusData.Find(filter).ToListAsync().Result;
                }

                if (listUserLoginStatus != null)
				{
                    foreach (BsonDocument obj in listUserLoginStatus) // Loop thru all Bson documents returned from the query.
					{
                        resultList.Add(UserLoginStatusMongoDbDataManager.MapToUserLoginStatusRecord(obj));
					}
				}
			}
            return resultList;		

		}

		public static void ExecuteFillDataTable(BsonDocument doc, DataTable dt, DataRow dr, string parent)
		{
			// arrays means 1:M relation to parent, meaning we will have to fake multi levels by adding 1 more row foreach item in array.
			// i created the here because i want to add all new array rows after our main row.
			List<KeyValuePair<string, BsonArray>> arrays = new List<KeyValuePair<string, BsonArray>>();

			foreach (string key in doc.Names) // this will loop thru all our json attributes.
			{
				object value = doc[key]; // get the value of the current json attribute.

				string x; // for my specific needs, i need all values to be save in datatable as strings. you can implument to match your needs.

				// if our attribute is BsonDocument, means relation is 1:1. we can add values to current datarow and call the data column "parent.current".
				// we will use this recursive method to run thru all the child document.
				if (value is BsonDocument)
				{
					string newParent = string.IsNullOrEmpty(parent) ? key : parent + "." + key;
					ExecuteFillDataTable((BsonDocument)value, dt, dr, newParent);
				}
				// if our attribute is BsonArray, means relation is 1:N. we will need to add new rows, but not now.
				// we will save it in queue for later use.
				else if (value is BsonArray)
				{
					// Save array to queue for later loop.
					arrays.Add(new KeyValuePair<string, BsonArray>(key, (BsonArray)value));


				}
				// if our attribute is datatime i needed it in a spesific string format.
				else if (value is BsonTimestamp)
				{
					x = doc[key].AsBsonTimestamp.ToLocalTime().ToString("s");
				}

				else if (value is BsonNull)
				{
					x = string.Empty;
				}


				else
				{
					// for all other cases, just .ToString() it.
					x = value.ToString();

					// Make sure our datatable already contains column with the right name. if not - add it.
					string colName = string.IsNullOrEmpty(parent) ? key : parent + "." + key;
					if (!dt.Columns.Contains(colName))
						dt.Columns.Add(colName);

					// Add the value to the datarow in the right column.
					dr[colName] = value;

				}

			}

			// loop thru all arrays when finish with standart fields.
			foreach (KeyValuePair<string, BsonArray> array in arrays)
			{
				// create column name that contains the parent name + child name.
				string newParent = string.IsNullOrEmpty(parent) ? array.Key : parent + "." + array.Key;
				// save the old - we will need it so we can add it existing values to the new row.
				DataRow drOld = dr;

				// loop thru all the BsonDocuments in the array
				foreach (BsonDocument doc2 in array.Value)
				{
					// Create new datarow for each item in array.
					dr = dt.NewRow();
					dr.ItemArray = drOld.ItemArray; // this will copy all the main row values to the new row - might not be needed for your use.
					dt.Rows.Add(dr); // the the new row to the datatable
					ExecuteFillDataTable(doc2, dt, dr, newParent); // fill the new datarow withh all the values for the BsonDocument in the array.
				}

				dr = drOld; // set the main data row back so we can use it values again.
			}
		}

        public static UserLoginStatusDataModel MapToUserLoginStatusRecord(BsonDocument obj)
        {
            var userLoginStatus = new UserLoginStatusDataModel();
            try
            {
                userLoginStatus.ApplicationId = obj["ApplicationId"].AsInt32;
                userLoginStatus.Name          = obj["Name"].AsString;
                userLoginStatus.Description   = obj["Description"].AsString;
                userLoginStatus.SortOrder     = obj["SortOrder"].AsInt32;

                try
                {
                    userLoginStatus.UserLoginStatusId = int.Parse(obj["_id"].ToString());
                }
                catch { }
            }
            catch { }

            return userLoginStatus;
        }

	}
}
