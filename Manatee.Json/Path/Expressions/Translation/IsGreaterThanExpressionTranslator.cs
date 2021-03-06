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
 
	File Name:		IsGreaterThanExpressionTranslator.cs
	Namespace:		Manatee.Json.Path.Expressions.Translation
	Class Name:		IsGreaterThanExpressionTranslator
	Purpose:		Translates from a LINQ Binary Expression with a GREATER THAN
					operation to an IsGreaterThanExpression.

***************************************************************************************/
using System;
using System.Linq.Expressions;

namespace Manatee.Json.Path.Expressions.Translation
{
	internal class IsGreaterThanExpressionTranslator : IExpressionTranslator
	{
		public ExpressionTreeNode<T> Translate<T>(Expression body)
		{
			var equal = body as BinaryExpression;
			if (equal == null)
				throw new InvalidOperationException();
			return new IsGreaterThanExpression<T>
				{
					Left = ExpressionTranslator.TranslateNode<T>(equal.Left),
					Right = ExpressionTranslator.TranslateNode<T>(equal.Right)
				};
		}
	}
}