using static Symbolism.Constants;

namespace Symbolism
{
	public static partial class Extensions
	{
		public static MathObject ToRadians(this MathObject n) { return n * pi / 180; }

		public static MathObject ToDegrees(this MathObject n) { return 180 * n / pi; }

		public static MathObject ToRadians(this int n) { return new Integer(n) * pi / 180; }

		public static MathObject ToDegrees(this int n) { return 180 * new Integer(n) / pi; }

		internal static MathObject Coefficient(this MathObject u)
		{
			var product = u as Product;
			return product != null && product.Elements[0] is Number ? product.Elements[0] : 1;
		}

	}
}
