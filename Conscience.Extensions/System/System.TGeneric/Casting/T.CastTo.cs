using System;

namespace System
{
	public static partial class Extensions
	{
		public static T CastTo<T>(this object @this)
		{
			return (T)@this;
		}
	}
}