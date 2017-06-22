using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
//using MongoDB.Driver.Core.Configuration;
using MongoRepository;

namespace Framework.Components.LogAndTrace
{
	public class SampleMongoDbDataManager
	{
		// Connect to the database.
		const string connectionString = "mongodb://IVR-DEV-MONGO";

		public class Person
		{
			[BsonId()]
			public ObjectId MyID { get; set; }
			public string Name { get; set; }
			[BsonIgnore()]
			public string NotPersisted { get; set; }
			public Address Address { get; set; }
		}

		public class Address
		{
			public string AddressLine { get; set; }
			public string PostalCode { get; set; }
		}

		public static void Test()
		{
			var client = new MongoClient(connectionString);
			var database = client.GetDatabase("testdb");

			// Store a person.
			//var persons = database.GetCollection("person");
			//var p1 = new Person() { Name = "Sander" };
			//persons.Save(p1);

		
			//Console.WriteLine(p1.MyID.ToString());
			Console.ReadKey();

		}

		public static void Test2()
		{

			var client = new MongoClient(connectionString);

			var database = client.GetDatabase("test");
			var collection = database.GetCollection<Person>("entities");

			//var entity = new Person { Name = "Tom" };
			//collection.Insert(entity);
			//var id = entity.MyID;
			//collection.Save(entity);

			//Console.Write(entity);

			//var query = Query<Person>.EQ(e2 => e2.MyID, id);
			//entity = collection.FindOne(query);

			//entity.Name = "Dick";
			//collection.Save(entity);

			//Console.Write(entity);

			//var update = Update<Person>.Set(e2 => e2.Name, "Harry");
			//update = Update<Person>.Set(e2 => e2.Address.AddressLine, "12,Ist street");
			//collection.Update(query, update);

			//Console.Write(entity);
			
		}

	}
}
