namespace Symbolism
{
	public static partial class Extensions
	{
		public static MathObject ExpandProduct(this MathObject r, MathObject s)
		{
			var sum = r as Sum;
			if (sum != null)
			{
				var f = sum.Elements[0];

				return f.ExpandProduct(s) + (r - f).ExpandProduct(s);
			}

			if (s is Sum) return s.ExpandProduct(r);

			return r*s;
		}
	}
}
