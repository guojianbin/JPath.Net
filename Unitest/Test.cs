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
		}
	}
}

