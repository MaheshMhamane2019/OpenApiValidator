using System;
using System.Text;

namespace System
{
	public static partial class Extensions
	{
		public static StringBuilder AppendLineIf<T>(this StringBuilder @this, Func<T, bool> predicate, params T[] values)
		{
			foreach(var value in values)
			{
				if(predicate(value))
				{
					@this.AppendLine(value.ToString());
				}
			}

			return @this;
		}
	}
}