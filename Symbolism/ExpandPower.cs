namespace Symbolism
{
	public static partial class Extensions
	{
		private static int Factorial(int n)
		{
			var result = 1;

			for (var i = 1; i <= n; i++)
			{
				result *= i;
			}

			return result;
			// return Enumerable.Range(1, n).Aggregate((acc, elt) => acc * elt);
		}

		public static MathObject ExpandPower(this MathObject u, int n)
		{
			var sum = u as Sum;
			if (sum != null)
			{
				var f = sum.Elements[0];

				var r = u - f;

				MathObject s = 0;

				var k = 0;

				while (true)
				{
					if (k > n) return s;

					var c = Factorial(n)/(Factorial(k)*Factorial(n - k));

					s = s + (c*(f ^ (n - k))).ExpandProduct(r.ExpandPower(k));

					k++;
				}
			}

			return u ^ n;
		}
	}
}
