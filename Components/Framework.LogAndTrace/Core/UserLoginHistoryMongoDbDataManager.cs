using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.LogAndTrace;
using Framework.Components.DataAccess;
using System.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoRepository;


namespace Framework.Components.LogAndTrace
{

	public partial class UserLoginHistoryMongoDbDataManager : BaseDataManager
	{
		static IMongoDatabase database = null;

		static UserLoginHistoryMongoDbDataManager()
		{
			var client = new MongoClient("mongodb://IVR-DEV-MONGO");

            database = client.GetDatabase("LoggingAndTrace");

		}

		public static void Create(UserLoginHistoryDataModel data, RequestProfile requestProfile)
		{
			var userLogin = database.GetCollection<BsonDocument>("UserLoginHistory");

			BsonDocument dataCollection = new BsonDocument {
                    { UserLoginHistoryDataModel.DataColumns.Application, data.Application },
                    { UserLoginHistoryDataModel.DataColumns.UserName, data.UserName },
                    { UserLoginHistoryDataModel.DataColumns.URL, data.URL },
                    { UserLoginHistoryDataModel.DataColumns.DateVisited, DateTime.Now },             
  				    { UserLoginHistoryDataModel.DataColumns.ServerName, data.ServerName },               
                };

			userLogin.InsertOneAsync(dataCollection);

		}

        public static List<UserLoginHistoryDataModel> GetList()
        {
            var resultList = new List<UserLoginHistoryDataModel>();

            //var data = database.GetCollection<UserLoginHistoryDataModel>("UserLoginHistory");
            //foreach (UserLoginHistoryDataModel userLoginHistory in data.FindAll())
            //{
            //    listHistory.Add(userLoginHistory);
            //} 

            var filter = new BsonDocument();

            var userLogin = database.GetCollection<BsonDocument>("UserLoginHistory");
            foreach (BsonDocument obj in userLogin.Find(filter).ToListAsync().Result) // Loop thru all Bson documents returned from the query.
            {
                resultList.Add(MapToUserLoginHistoryRecord(obj));
            }

            return resultList;
            

        }

        public static List<UserLoginHistoryDataModel> GetList(UserLoginHistoryDataModel data, RequestProfile requestProfile)
		{
			var listUserLoginHistory = database.GetCollection<BsonDocument>("UserLoginHistory");
            var listUserHistoryData = new List<BsonDocument>();

			
            var builder = Builders<BsonDocument>.Filter;
            var resultList = new List<UserLoginHistoryDataModel>();

			if (string.IsNullOrEmpty(data.UserName) && string.IsNullOrEmpty(data.ServerName))
			{
                resultList = GetList();
			}
			else
			{
                if (!string.IsNullOrEmpty(data.UserName) && !string.IsNullOrEmpty(data.ServerName))
                {
                    var filter = builder.Eq(UserLoginHistoryDataModel.DataColumns.UserName, data.UserName) &
                           builder.Eq(UserLoginHistoryDataModel.DataColumns.ServerName, data.ServerName);
                    listUserHistoryData = listUserLoginHistory.Find(filter).ToListAsync().Result;
                }
                else if (!string.IsNullOrEmpty(data.UserName) || !string.IsNullOrEmpty(data.ServerName))
                {
                    var filter = builder.Eq(UserLoginHistoryDataModel.DataColumns.UserName, data.UserName) |
                               builder.Eq(UserLoginHistoryDataModel.DataColumns.ServerName, data.ServerName);
                    listUserHistoryData = listUserLoginHistory.Find(filter).ToListAsync().Result;
                }                

				if (listUserHistoryData != null)
				{
					foreach (BsonDocument obj in listUserHistoryData) // Loop thru all Bson documents returned from the query.
					{
                        resultList.Add(MapToUserLoginHistoryRecord(obj));
					}
				}
			}
            return resultList;
		}

        public static UserLoginHistoryDataModel MapToUserLoginHistoryRecord(BsonDocument obj)
        {
            var userLoginHistory = new UserLoginHistoryDataModel();
            try
            {
                if (obj.Contains("ApplicationId"))
                {
                    userLoginHistory.ApplicationId = obj["ApplicationId"].AsInt32;
                } 
                if (obj.Contains("Application"))
                {
                    userLoginHistory.Application = obj["Application"].AsString;
                }
                userLoginHistory.URL           = obj["URL"].AsString;
                userLoginHistory.ServerName    = obj["ServerName"].AsString;
                userLoginHistory.UserName      = obj["UserName"].AsString;
                userLoginHistory.DateVisited   = obj["DateVisited"].ToUniversalTime();
            }
            catch { }

            return userLoginHistory;
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

	}
}
