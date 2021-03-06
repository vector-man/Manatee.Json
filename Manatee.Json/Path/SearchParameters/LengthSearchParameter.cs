/***************************************************************************************

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
 
	File Name:		LengthSearchParameter.cs
	Namespace:		Manatee.Json.Path.SearchParameters
	Class Name:		LengthSearchParameter
	Purpose:		Indicates that a search should return the length of all
					object and array values.

***************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;

namespace Manatee.Json.Path.SearchParameters
{
	internal class LengthSearchParameter : IJsonPathSearchParameter, IEquatable<LengthSearchParameter>
	{
		public static LengthSearchParameter Instance { get; } = new LengthSearchParameter();

		public IEnumerable<JsonValue> Find(IEnumerable<JsonValue> json, JsonValue root)
		{
			return new JsonArray(json.SelectMany(v =>
				{
					var contents = new List<JsonValue> {v.Array.Count};
					switch (v.Type)
					{
						case JsonValueType.Object:
							contents.AddRange(v.Object.Values.SelectMany(jv => Find(new JsonArray {jv}, root)));
							break;
						case JsonValueType.Array:
							contents.AddRange(v.Array.SelectMany(jv => Find(new JsonArray {jv}, root)));
							break;
					}
					return contents;
				}));
		}
		public override string ToString()
		{
			return "length";
		}
		public bool Equals(LengthSearchParameter other)
		{
			return !ReferenceEquals(null, other);
		}
		public override bool Equals(object obj)
		{
			return Equals(obj as LengthSearchParameter);
		}
		public override int GetHashCode()
		{
			// ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
			return base.GetHashCode();
		}
	}
}