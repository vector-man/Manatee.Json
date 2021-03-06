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
 
	File Name:		JsonSerializationException.cs
	Namespace:		Manatee.Json.Serialization
	Class Name:		JsonSerializationException
	Purpose:		Thrown when an error occurs during serialization or deserialization.

***************************************************************************************/

using System;

namespace Manatee.Json.Serialization
{
	/// <summary>
	/// Thrown when an error occurs during serialization or deserialization.
	/// </summary>
	public class JsonSerializationException : Exception
	{
		/// <summary>
		/// Creates a new instance of the <see cref="JsonSerializationException"/> class.
		/// </summary>
		public JsonSerializationException(string message)
			: base(message){}
	}
}
