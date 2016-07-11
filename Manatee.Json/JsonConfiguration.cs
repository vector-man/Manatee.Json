using Manatee.Json.Parsing;

namespace Manatee.Json
{
	public static class JsonConfiguration
	{
		public static void AddParser(IJsonParser parser)
		{
			JsonParser.AddParser(parser);
		}
		public static void RemoveParser<T>()
			where T : IJsonParser
		{
			JsonParser.RemoveParser<T>();
		}
	}
}
