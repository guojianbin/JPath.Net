using NUnit.Framework;
using System;
using org.lmatt;

namespace Unitest
{
	[TestFixture ()]
	public class Test
	{
		[Test ()]
		public void TestParseJson ()
		{
			var jsonObj = org.lmatt.JBase.ParseJson ("{\"key1\" : [], \"key2\": {}, \"key3\": [1, 2, 3], \"key4\": {\"subkey1\": \"value1\"}, \"key5\": true, \"key6\": null}");
			Assert.IsNotNull (jsonObj);

			Assert.IsAssignableFrom<JObject> (jsonObj);

			Assert.IsAssignableFrom<JArray> (((JObject)jsonObj)["key1"]);

			Assert.AreEqual (0, ((JArray)(((JObject)jsonObj) ["key1"])).Count);

			Assert.IsTrue (((JBoolean)(((JObject)jsonObj) ["key5"])).Value);

			Assert.IsNull (((((JObject)jsonObj) ["key6"])));


			var jsonArray = org.lmatt.JBase.ParseJson ("[1, 2, 3]");

			Assert.IsNotNull (jsonArray);

			Assert.AreEqual (3, ((JArray)(jsonArray)).Count);

			Assert.AreEqual (2, ((JNumber)((JArray)jsonArray) [1]).Value);
		}

		[Test()]
		public void TestJPath() 
		{
			var jsonObj = org.lmatt.JBase.ParseJson ("{\"key1\" : [], \"key2\": {}, \"key3\": [1, 2, 3], \"key4\": {\"key5\": \"value1\"}, \"key5\": true, \"key6\": 1}");

			var testJPath = new JPath ("//key5");

			var eveluateResults = testJPath.Evaluate (jsonObj);

			Assert.AreEqual (2, eveluateResults.Count);

			testJPath = new JPath ("//notexist");

			eveluateResults = testJPath.Evaluate (jsonObj);

			Assert.AreEqual (0, eveluateResults.Count);


			testJPath = new JPath ("/key3/0");

			eveluateResults = testJPath.Evaluate (jsonObj);

			Assert.AreEqual (1, eveluateResults.Count);
			Assert.IsInstanceOf<JNumber> (eveluateResults [0]);
		}

		[Test()]
		public void TestJPathSelect()
		{
			var jsonObj = org.lmatt.JBase.ParseJson ("{\"key1\" : [], \"key2\": {}, \"key3\": [1, 2, 3], \"key4\": {\"key5\": \"value1\"}, \"key5\": true, \"key6\": 1}");
			var eveluateResults = jsonObj.JPathSelects ("//key5|//key6");

			Assert.AreEqual (3, eveluateResults.Count);

			var result = jsonObj.JPathSelect ("//key5");
			Assert.IsInstanceOf<JBoolean> (result);
			Assert.AreEqual (true, ((JBoolean)result).Value);
		}
	}
}

