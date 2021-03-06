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
 
	File Name:		IsGreaterThanExpression.cs
	Namespace:		Manatee.Json.Path.Expressions
	Class Name:		IsGreaterThanExpression
	Purpose:		Expresses the intent to compare two values.

***************************************************************************************/

using System;

namespace Manatee.Json.Path.Expressions
{
	internal class IsGreaterThanExpression<T> : ExpressionTreeBranch<T>, IEquatable<IsGreaterThanExpression<T>>
	{
		protected override int BasePriority => 1;

		public override object Evaluate(T json, JsonValue root)
		{
			var left = Left.Evaluate(json, root);
			var right = Right.Evaluate(json, root);
			return ValueComparer.GreaterThan(left, right);
		}
		public override string ToString()
		{
			return $"{Left} > {Right}";
		}
		public bool Equals(IsGreaterThanExpression<T> other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return base.Equals(other);
		}
		public override bool Equals(object obj)
		{
			if (obj.GetType() != this.GetType()) return false;
			return Equals((IsGreaterThanExpression<T>) obj);
		}
		public override int GetHashCode()
		{
			unchecked
			{
				int hashCode = base.GetHashCode();
				hashCode = (hashCode * 397) ^ GetType().GetHashCode();
				return hashCode;
			}
		}
	}
}