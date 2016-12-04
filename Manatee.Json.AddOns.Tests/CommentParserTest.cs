using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Json.AddOns.Tests
{
	[TestClass]
	public class CommentParserTest
	{
		[TestMethod]
		public void LineCommentIsIgnored_String()
		{
			JsonConfiguration.AddParser(new CommentParser());
			var s = "{\"bool\":false,// this is a comment\n\"int\":42,\"string\":\"a string\"}";
			var expected = new JsonObject { { "bool", false }, { "int", 42 }, { "string", "a string" } };
			var actual = JsonValue.Parse(s);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void LineCommentIsIgnoredAfterColorOfKeyValuePair_String()
		{
			JsonConfiguration.AddParser(new CommentParser());
			var s = "{\"bool\":// this is a comment\nfalse,\"int\":42,\"string\":\"a string\"}";
			var expected = new JsonObject { { "bool", false }, { "int", 42 }, { "string", "a string" } };
			var actual = JsonValue.Parse(s);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		[ExpectedException(typeof(JsonSyntaxException))]
		public void LineCommentIsNotAllowedBeforeColonOfKeyValuePair_String()
		{
			JsonConfiguration.AddParser(new CommentParser());
			var s = "{\"bool\"// this is a comment\n:false,\"int\":42,\"string\":\"a string\"}";
			var expected = new JsonObject { { "bool", false }, { "int", 42 }, { "string", "a string" } };
			var actual = JsonValue.Parse(s);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		[ExpectedException(typeof(JsonSyntaxException))]
		public void SpanCommentIsNotAllowedBeforeColonOfKeyValuePair_String()
		{
			JsonConfiguration.AddParser(new CommentParser());
			var s = "{\"bool\"/* this is a comment */:false,\"int\":42,\"string\":\"a string\"}";
			var expected = new JsonObject { { "bool", false }, { "int", 42 }, { "string", "a string" } };
			var actual = JsonValue.Parse(s);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void SpanCommentIsIgnored_String()
		{
			JsonConfiguration.AddParser(new CommentParser());
			var s = "{\"bool\":false,/* this is a comment */\"int\":42,\"string\":\"a string\"}";
			var expected = new JsonObject {{"bool", false}, {"int", 42}, {"string", "a string"}};
			var actual = JsonValue.Parse(s);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		[ExpectedException(typeof(JsonSyntaxException))]
		public void InvalidSequence_String()
		{
			JsonConfiguration.AddParser(new CommentParser());
			var s = "{\"bool\":false,/( this is invalid\n\"int\":42,\"string\":\"a string\"}";
			var actual = JsonValue.Parse(s);
		}

		[TestMethod]
		[ExpectedException(typeof(JsonSyntaxException))]
		public void ParserNotRegistered_String()
		{
			JsonConfiguration.RemoveParser<CommentParser>();
			var s = "{\"bool\":false,// this is invalid\n\"int\":42,\"string\":\"a string\"}";
			var actual = JsonValue.Parse(s);
		}

		[TestMethod]
		public void LineCommentIsIgnored_Stream()
		{
			JsonConfiguration.AddParser(new CommentParser());
			var s = new StreamReader("{\"bool\":false,// this is a comment\n\"int\":42,\"string\":\"a string\"}".ToStream());
			var expected = new JsonObject {{"bool", false}, {"int", 42}, {"string", "a string"}};
			var actual = JsonValue.Parse(s);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void SpanCommentIsIgnored_Stream()
		{
			JsonConfiguration.AddParser(new CommentParser());
			var s = new StreamReader("{\"bool\":false,/* this is a comment */\"int\":42,\"string\":\"a string\"}".ToStream());
			var expected = new JsonObject {{"bool", false}, {"int", 42}, {"string", "a string"}};
			var actual = JsonValue.Parse(s);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		[ExpectedException(typeof(JsonSyntaxException))]
		public void InvalidSequence_Stream()
		{
			JsonConfiguration.AddParser(new CommentParser());
			var s = new StreamReader("{\"bool\":false,/( this is invalid\n\"int\":42,\"string\":\"a string\"}".ToStream());
			var actual = JsonValue.Parse(s);
		}

		[TestMethod]
		[ExpectedException(typeof(JsonSyntaxException))]
		public void ParserNotRegistered_Stream()
		{
			JsonConfiguration.RemoveParser<CommentParser>();
			var s = new StreamReader("{\"bool\":false,// this is invalid\n\"int\":42,\"string\":\"a string\"}".ToStream());
			var actual = JsonValue.Parse(s);
		}
	}
}
