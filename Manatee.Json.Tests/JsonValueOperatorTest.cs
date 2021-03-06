﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Json.Tests
{
	[TestClass]
	public class JsonValueOperatorTest
	{
		[TestMethod]
		public void CastOperator_Bool_AssignsCorrectValue()
		{
			var json = new JsonValue();
			Assert.AreEqual(JsonValueType.Null, json.Type);
			json = false;
			var expected = false;
			Assert.AreEqual(JsonValueType.Boolean, json.Type);
			Assert.AreEqual(expected, json.Boolean);
		}
		[TestMethod]
		public void CastOperator_Number_AssignsCorrectValue()
		{
			var json = new JsonValue();
			Assert.AreEqual(JsonValueType.Null, json.Type);
			json = 42;
			var expected = 42;
			Assert.AreEqual(JsonValueType.Number, json.Type);
			Assert.AreEqual(expected, json.Number);
		}
		[TestMethod]
		public void CastOperator_String_AssignsCorrectValue()
		{
			var json = new JsonValue();
			Assert.AreEqual(JsonValueType.Null, json.Type);
			json = "a string";
			var expected = "a string";
			Assert.AreEqual(JsonValueType.String, json.Type);
			Assert.AreEqual(expected, json.String);
		}
		[TestMethod]
		public void CastOperator_Array_AssignsCorrectValue()
		{
			var json = new JsonValue();
			Assert.AreEqual(JsonValueType.Null, json.Type);
			json = new JsonArray { false, 42, "a string", "another string" };
			var expected = new JsonArray { false, 42, "a string", "another string" };
			Assert.AreEqual(JsonValueType.Array, json.Type);
			Assert.AreEqual(expected, json.Array);
		}
		[TestMethod]
		public void CastOperator_Object_AssignsCorrectValue()
		{
			var json = new JsonValue();
			Assert.AreEqual(JsonValueType.Null, json.Type);
			json = new JsonObject { { "bool", false }, { "int", 42 }, { "string", "a string" } };
			var expected = new JsonObject { { "bool", false }, { "int", 42 }, { "string", "a string" } };
			Assert.AreEqual(JsonValueType.Object, json.Type);
			Assert.AreEqual(expected, json.Object);
		}
	}
}