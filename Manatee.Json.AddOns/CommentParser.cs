using System;
using System.IO;
using Manatee.Json.Parsing;

namespace Manatee.Json.AddOns
{
    public class CommentParser : IJsonParser
    {
	    public bool Handles(char c)
	    {
		    return c == '/';
	    }
	    public string TryParse(string source, ref int index, out JsonValue value)
	    {
		    value = null;
		    var text = source.Substring(index);
		    if (text.Length < 2)
			    return "Unexpected end of input.";
			if (text.StartsWith("//"))
				return ParseComment(text, ref index, "\n");
			if (text.StartsWith("/*"))
				return ParseComment(text, ref index, "*/");
		    return $"Sequence '{text.Substring(0, 2)}' not recognized.";
	    }
	    public string TryParse(StreamReader stream, out JsonValue value)
	    {
			value = null;
		    var start = new char[2];
			var count = stream.Read(start, 0, 2);
			if (count < 2)
				return "Unexpected end of input.";
			var text = new string(start);
			if (text == "//")
				return ParseComment(stream, "\n");
			if (text == "/*")
				return ParseComment(stream, "*/");
			return $"Sequence '{text}' not recognized.";
		}

		private static string ParseComment(string source, ref int index, string terminator)
		{
			var end = source.IndexOf(terminator, StringComparison.InvariantCulture);
			if (end == -1)
				return "Unexpected end of input.";
			var comment = source.Substring(2, end - 2);
			index += comment.Length + 2 + terminator.Length;
			return null;
		}
		private static string ParseComment(StreamReader stream, string terminator)
		{
			var comment = string.Empty;
			while (!stream.EndOfStream && !comment.EndsWith(terminator))
			{
				var c = (char)stream.Read();
				comment += c;
			}
			if (stream.EndOfStream)
				return "Unexpected end of input.";
			return null;
		}
	}
}
