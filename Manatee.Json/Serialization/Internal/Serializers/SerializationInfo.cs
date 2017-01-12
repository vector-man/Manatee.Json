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
 
	File Name:		SerializationInfo.cs
	Namespace:		Manatee.Json.Serialization.Internal.Serializers
	Class Name:		SerializationInfo
	Purpose:		Caches reflection-based serialization information.

***************************************************************************************/

using System;
using System.Linq.Expressions;
using System.Reflection;
using Manatee.Json.Internal;

namespace Manatee.Json.Serialization.Internal.Serializers
{
	internal abstract class SerializationInfo
	{
		public MemberInfo MemberInfo { get; }
		public string SerializationName { get; }

		public SerializationInfo(MemberInfo memberInfo, string serializationName)
		{
			MemberInfo = memberInfo;
			SerializationName = serializationName;
		}

		public abstract object Get(object source);
		public abstract void Set(object source, object value);

		public override bool Equals(object obj)
		{
			return obj != null && MemberInfo.Equals(((SerializationInfo) obj).MemberInfo);
		}
		public override int GetHashCode()
		{
			return MemberInfo.GetHashCode();
		}
	}

	internal class SerializationInfo<TObject, TMember> : SerializationInfo
	{
		private readonly Func<TObject, TMember> _getter;
		private readonly Action<TObject, TMember> _setter;

		public SerializationInfo(MemberInfo info, Func<TObject, TMember> getter, Action<TObject, TMember> setter, string serializationName)
			: base(info, serializationName)
		{
			_getter = getter;
			_setter = setter;
		}

		public override object Get(object source)
		{
			return _getter((TObject) source);
		}

		public override void Set(object source, object value)
		{
			_setter((TObject) source, (TMember) value);
		}
	}

	internal static class SerializationInfoFactory
	{
		public static SerializationInfo Get(MemberInfo info, string serializationName)
		{
			Type memberType;
			string factoryMethodName;
			var propertyInfo = info as PropertyInfo;
			if (propertyInfo != null)
			{
				memberType = propertyInfo.PropertyType;
				factoryMethodName = nameof(GetProperty);
			}
			else
			{
				memberType = ((FieldInfo) info).FieldType;
				factoryMethodName = nameof(GetField);
			}

#if IOS
			var method = typeof(SerializationInfoFactory).GetMethod(factoryMethodName)
														 .MakeGenericMethod(info.DeclaringType, memberType);
#else
			var method = typeof(SerializationInfoFactory).GetMethod(factoryMethodName, BindingFlags.Static | BindingFlags.NonPublic)
														 .MakeGenericMethod(info.DeclaringType, memberType);
#endif

			return (SerializationInfo) method.Invoke(null, new object[] {info, serializationName});
		}

		private static SerializationInfo GetProperty<TObject, TMember>(MemberInfo info, string serializationName)
		{
			var getter = MemberExpression.GetPropGetter<TObject, TMember>(info.Name);
			var setter = MemberExpression.GetPropSetter<TObject, TMember>(info.Name);

			return new SerializationInfo<TObject, TMember>(info, getter, setter, serializationName);
		}

		private static SerializationInfo GetField<TObject, TMember>(MemberInfo info, string serializationName)
		{
			var getter = MemberExpression.GetFieldGetter<TObject, TMember>(info.Name);
			var setter = MemberExpression.GetFieldSetter<TObject, TMember>(info.Name);

			return new SerializationInfo<TObject, TMember>(info, getter, setter, serializationName);
		}
	}
}