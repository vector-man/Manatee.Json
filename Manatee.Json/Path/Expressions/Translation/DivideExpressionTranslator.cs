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
 
	File Name:		DivideExpressionTranslator.cs
	Namespace:		Manatee.Json.Path.Expressions.Translation
	Class Name:		DivideExpressionTranslator
	Purpose:		Translates from a LINQ Binary Expression with a DIVIDE
					operation to a DivideExpression.

***************************************************************************************/
using System;
using System.Linq.Expressions;

namespace Manatee.Json.Path.Expressions.Translation
{
	internal class DivideExpressionTranslator : IExpressionTranslator
	{
		public ExpressionTreeNode<T> Translate<T>(Expression body)
		{
			var add = body as BinaryExpression;
			if (add == null)
				throw new InvalidOperationException();
			return new DivideExpression<T>
				{
					Left = ExpressionTranslator.TranslateNode<T>(add.Left),
					Right = ExpressionTranslator.TranslateNode<T>(add.Right)
				};
		}
	}
}