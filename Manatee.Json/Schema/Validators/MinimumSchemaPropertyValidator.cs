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
 
	File Name:		MinimumSchemaPropertyValidator.cs
	Namespace:		Manatee.Json.Schema.Validators
	Class Name:		MinimumSchemaPropertyValidator
	Purpose:		Validates schema with "minimum" or "exclusiveMinimum" properties.

***************************************************************************************/
namespace Manatee.Json.Schema.Validators
{
	internal class MinimumSchemaPropertyValidator : IJsonSchemaPropertyValidator
	{
		public bool Applies(JsonSchema schema, JsonValue json)
		{
			return (schema.Minimum.HasValue || (schema.ExclusiveMinimum ?? false)) &&
			       json.Type == JsonValueType.Number;
		}
		public SchemaValidationResults Validate(JsonSchema schema, JsonValue json, JsonValue root)
		{
			if (schema.ExclusiveMinimum ?? false)
			{
				if (json.Number <= schema.Minimum)
					return new SchemaValidationResults(string.Empty, $"Expected: > {schema.Minimum}; Actual: {json.Number}.");
			}
			else
			{
				if (json.Number < schema.Minimum)
					return new SchemaValidationResults(string.Empty, $"Expected: >= {schema.Minimum}; Actual: {json.Number}.");
			}
			return new SchemaValidationResults();
		}
	}
}
