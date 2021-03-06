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
 
	File Name:		DictionarySerializationDelegateProvider.cs
	Namespace:		Manatee.Json.Serialization.Internal.AutoRegistration
	Class Name:		DictionarySerializationDelegateProvider
	Purpose:		Provides delegates for serializing Nullable types.

***************************************************************************************/
using System;
using Manatee.Json.Internal;

namespace Manatee.Json.Serialization.Internal.AutoRegistration
{
	internal class NullableSerializationDelegateProvider : SerializationDelegateProviderBase
	{
		public override bool CanHandle(Type type)
		{
			return type.TypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		private static JsonValue Encode<T>(T? nullable, JsonSerializer serializer)
			where T : struct
		{
			if (!nullable.HasValue) return JsonValue.Null;
			var encodeDefaultValues = serializer.Options.EncodeDefaultValues;
			serializer.Options.EncodeDefaultValues = Equals(nullable.Value, default (T));
			var json = serializer.Serialize(nullable.Value);
			serializer.Options.EncodeDefaultValues = encodeDefaultValues;
			return json;
		}
		private static T? Decode<T>(JsonValue json, JsonSerializer serializer)
			where T : struct
		{
			if (json == JsonValue.Null)
				return null;
			T? nullable = serializer.Deserialize<T>(json);
			return nullable;
		}
	}
}