using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;
using DataModel.Framework.LogAndTrace;
using Framework.Components.DataAccess;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoRepository;


namespace Framework.Components.LogAndTrace
{
	
	public partial class UserLoginMongoDbDataManager : BaseDataManager
	{
		static readonly IMongoDatabase Database = null;

		static UserLoginMongoDbDataManager()
		{
			var client = new MongoClient("mongodb://IVR-DEV-MONGO");

			Database = client.GetDatabase("LoggingAndTrace");
		}

		public static void Create(UserLoginDataModel data, RequestProfile requestProfile)
		{
            var userLogin = Database.GetCollection<BsonDocument>("UserLogin");

		    var dataCollection = new BsonDocument {
				    { UserLoginDataModel.DataColumns.Application, data.Application },
				    { UserLoginDataModel.DataColumns.UserName, data.UserName },
				    { UserLoginDataModel.DataColumns.UserLoginStatus, data.UserLoginStatus },
				    { UserLoginDataModel.DataColumns.RecordDate, DateTime.Now }               
			    };

		    userLogin.InsertOneAsync(dataCollection);
		}

        public static List<UserLoginDataModel> GetList()
		{
            var resultList = new List<UserLoginDataModel>();            
			
			var list = new List<BsonDocument>();
			var userLogin = Database.GetCollection<BsonDocument>("UserLogin");
			var filter = new BsonDocument();

			var resultsList = userLogin.Find(filter).ToListAsync();

			foreach (BsonDocument obj in resultsList.Result) // Loop thru all Bson documents returned from the query.
			{
                resultList.Add(MapToUserLoginRecord(obj));
			}

            return resultList; // return the table with all it rows inside.

		}

        public static List<UserLoginDataModel> GetList(UserLoginDataModel data, RequestProfile requestProfile)
		{
			var userData = Database.GetCollection<BsonDocument>("UserLogin");
            var resultList = new List<UserLoginDataModel>();

			if (string.IsNullOrEmpty(data.UserName))
			{
                resultList = GetList();
			}
			else if (!string.IsNullOrEmpty(data.UserName))
			{   

				var builder = Builders<BsonDocument>.Filter;

				var filter = builder.Eq(UserLoginHistoryDataModel.DataColumns.UserName, data.UserName);
				var resultUserHistoryData = userData.Find(filter).ToListAsync();

				var userHistoryData = resultUserHistoryData.Result;

				if (userHistoryData != null)
				{
					foreach (var obj in userHistoryData) // Loop thru all Bson documents returned from the query.
					{
                        resultList.Add(MapToUserLoginRecord(obj));
					}
				}
			}

            return resultList;
		}

        public static UserLoginDataModel MapToUserLoginRecord(BsonDocument obj)
        {
            var userLogin = new UserLoginDataModel();
            
            userLogin.UserName          = obj["UserName"].AsString;
            userLogin.RecordDate        = obj["RecordDate"].ToUniversalTime();
            if (obj.Contains("ApplicationId"))
            {
                userLogin.ApplicationId = obj["ApplicationId"].AsInt32;
            }
            if (obj.Contains("Application"))
            {
                userLogin.Application = obj["Application"].AsString;
            }
            if (obj.Contains("UserLoginStatusId"))
            {
                if (obj["UserLoginStatusId"].IsInt32)
                {
                    userLogin.UserLoginStatusId = obj["UserLoginStatusId"].AsInt32;
                }
            }

            if (obj.Contains("UserLoginStatus"))
            {
                userLogin.UserLoginStatus = obj["UserLoginStatus"].AsString;
            }

            return userLogin;
        }

		public static void ExecuteFillDataTable(BsonDocument doc, DataTable dt, DataRow dr, string parent)
		{
			// arrays means 1:M relation to parent, meaning we will have to fake multi levels by adding 1 more row foreach item in array.
			// i created the here because i want to add all new array rows after our main row.
			var arrays = new List<KeyValuePair<string, BsonArray>>();

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
				var newParent = string.IsNullOrEmpty(parent) ? array.Key : parent + "." + array.Key;
				// save the old - we will need it so we can add it existing values to the new row.
				var drOld = dr;

				// loop thru all the BsonDocuments in the array
				foreach (var bsonValue in array.Value)
				{
					var doc2 = (BsonDocument) bsonValue;
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
