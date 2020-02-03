using System.Text;

namespace System
{
	public static partial class Extensions
	{
		public static string Substring(this StringBuilder @this, int startIndex)
		{
			return @this.ToString(startIndex, @this.Length-startIndex);
		}

		public static string Substring(this StringBuilder @this, int startIndex, int length)
		{
			return @this.ToString(startIndex, length);
		}
	}
}