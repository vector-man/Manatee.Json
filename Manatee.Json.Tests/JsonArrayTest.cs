﻿/***************************************************************************************

	Copyright 2016 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		JsonArrayTest.cs
	Namespace:		Manatee.Json.Tests
	Class Name:		JsonArrayTest
	Purpose:		Tests for JsonArray.

***************************************************************************************/

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Json.Tests
{
	[TestClass]
	public class JsonArrayTest
	{
		[TestMethod]
		public void Equals_SameValuesSameOrder_ReturnsTrue()
		{
			var json1 = new JsonArray { false, 42, "a string" };
			var json2 = new JsonArray { false, 42, "a string" };
			Assert.IsTrue(json1.Equals(json2));
		}
		[TestMethod]
		public void Equals_SameValuesDifferentOrder_ReturnsFalse()
		{
			var json1 = new JsonArray { false, 42, "a string" };
			var json2 = new JsonArray { 42, false, "a string" };
			Assert.IsFalse(json1.Equals(json2));
		}
		[TestMethod]
		public void Equals_DifferentValues_ReturnsFalse()
		{
			var json1 = new JsonArray { false, 42, "a string" };
			var json2 = new JsonArray { false, "42", "a string" };
			Assert.IsFalse(json1.Equals(json2));
		}
		[TestMethod]
		public void Parse_ValidString_ReturnsCorrectArray()
		{
			var s = "[false,42,\"a string\"]";
			var expected = new JsonArray { false, 42, "a string" };
			var actual = JsonValue.Parse(s);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod]
		public void Parse_EmptyArray_ReturnsEmptyArray()
		{
			var s = "[]";
			var expected = new JsonArray();
			var actual = JsonValue.Parse(s);
			Assert.AreEqual(expected, actual);
		}
		[TestMethod]
		[ExpectedException(typeof(JsonSyntaxException))]
		public void Parse_StringMissingValue_ThrowsJsonSyntaxException()
		{
			var s = "[false,,\"a string\"]";
			var actual = JsonValue.Parse(s);
		}
		[TestMethod]
		public void Parse_StringMissingOpenBracket_ReturnsFirstElement()
		{
			var s = "false,42,\"a string\"]";
			var actual = JsonValue.Parse(s);
			Assert.AreEqual(false, actual);
		}
		[TestMethod]
		[ExpectedException(typeof(JsonSyntaxException))]
		public void Parse_StringMissingCloseBracket_ThrowsJsonSyntaxException()
		{
			var s = "[false,42,\"a string\"";
			var actual = JsonValue.Parse(s);
		}
		[TestMethod]
		[ExpectedException(typeof(JsonSyntaxException))]
		public void Parse_IncompleteEndingWithWhitespaceBetweenTokens_ThrowsJsonSyntaxException()
		{
			var s = "[  false,  42,  \"a string\"  ";
			var actual = JsonValue.Parse(s);
		}
		[TestMethod]
		public void Ctor_InitializationIsSuccessful()
		{
			var expected = new JsonArray {5, "a string", false, JsonValue.Null};
			var actual = new JsonArray(expected.ToArray());
			Assert.AreEqual(expected, actual);
		}
	}
}
