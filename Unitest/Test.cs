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
	}
}

