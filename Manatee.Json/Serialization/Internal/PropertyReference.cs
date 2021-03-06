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
 
	File Name:		PropertyReference.cs
	Namespace:		Manatee.Json.Serialization.Internal
	Class Name:		PropertyReference
	Purpose:		Defines a property on an object for the purpose of cataloging
					references.

***************************************************************************************/

using System.Reflection;

namespace Manatee.Json.Serialization.Internal
{

	internal class PropertyReference : MemberReference<PropertyInfo>
	{
		public override object GetValue(object instance)
		{
			return Info.GetValue(instance, null);
		}
		public override void SetValue(object instance, object value)
		{
			Info.SetValue(instance, value, null);
		}
	}
}
