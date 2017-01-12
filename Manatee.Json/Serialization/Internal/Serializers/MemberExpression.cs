using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Manatee.Json.Serialization.Internal.Serializers
{
	internal class MemberExpression
	{
		// returns property getter
		public static Func<TObject, TProperty> GetPropGetter<TObject, TProperty>(string propertyName)
		{
			var paramExpression = Expression.Parameter(typeof(TObject), "value");
			Expression propertyGetterExpression = Expression.Property(paramExpression, propertyName);
			var result = Expression.Lambda<Func<TObject, TProperty>>(propertyGetterExpression, paramExpression).Compile();

			return result;
		}

		// returns property setter:
		public static Action<TObject, TProperty> GetPropSetter<TObject, TProperty>(string propertyName)
		{
			var paramExpression = Expression.Parameter(typeof(TObject), null);
			var paramExpression2 = Expression.Parameter(typeof(TProperty), propertyName);
			var propertyGetterExpression = Expression.Property(paramExpression, propertyName);
			var assignmentExpression = GetAssignmentExpression(propertyGetterExpression, paramExpression2);
			var result = Expression.Lambda<Action<TObject, TProperty>>(assignmentExpression, paramExpression, paramExpression2)
								   .Compile();

			return result;
		}
		// returns property getter
		public static Func<TObject, TProperty> GetFieldGetter<TObject, TProperty>(string fieldName)
		{
			var paramExpression = Expression.Parameter(typeof(TObject), null);
			Expression propertyGetterExpression = Expression.Field(paramExpression, fieldName);
			var result = Expression.Lambda<Func<TObject, TProperty>>(propertyGetterExpression, paramExpression).Compile();

			return result;
		}

		// returns property setter:
		public static Action<TObject, TProperty> GetFieldSetter<TObject, TProperty>(string fieldName)
		{
			var paramExpression = Expression.Parameter(typeof(TObject), null);
			var paramExpression2 = Expression.Parameter(typeof(TProperty), fieldName);
			var propertyGetterExpression = Expression.Field(paramExpression, fieldName);
			var assignmentExpression = GetAssignmentExpression(propertyGetterExpression, paramExpression2);
			var result = Expression.Lambda<Action<TObject, TProperty>>(assignmentExpression, paramExpression, paramExpression2)
								   .Compile();

			return result;
		}

		private static Expression GetAssignmentExpression(Expression left, Expression right)
		{
#if NET35
			return Expression.Call(null, typeof(MemberExpression).GetMethod(nameof(AssignTo), BindingFlags.NonPublic | BindingFlags.Static)
			                                                    .MakeGenericMethod(left.Type),
			                       left, right);
#else
			return Expression.Assign(left, right);
#endif
		}

#if NET35
		private static void AssignTo<T>(out T left, T right)  // note the 'ref', which is
		{                                                     // important when assigning
			left = right;                                     // to value types!
		}
#endif
	}
}
