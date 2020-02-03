using System;

namespace System
{
	public static partial class Extensions
	{
		public static bool IsInArray<T>(this T @this, params T[] values)
		{
			return Array.IndexOf(values, @this)!=-1;
		}
	}
}